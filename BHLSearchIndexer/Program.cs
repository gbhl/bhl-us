using BHL.QueueUtility;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading;

namespace BHL.SearchIndexer
{
    class Program
    {
        private static string _configFile = "AppConfig.xml";

        private static string _connectionKey = string.Empty;
        private static string _dbConnectionString = string.Empty;

        private static string _esConnectionString = string.Empty;

        private static string _mqAddress = string.Empty;
        private static int _mqPort = 0;
        private static string _mqUser = string.Empty;
        private static string _mqPw = string.Empty;
        private static string _mqQueueName = string.Empty;
        private static string _mqErrorExchangeName = string.Empty;
        private static string _mqErrorQueueName = string.Empty;
        private static ushort _mqPrefetchCount = 1;

        private static string _smtpHost = string.Empty;
        private static int _smtpPort = 0;
        private static bool _smtpEnableSsl = false;
        private static bool _smtpDefaultCredentials = false;
        private static string _smtpUser = string.Empty;
        private static string _smtpPw = string.Empty;
        private static string _emailFromAddress = string.Empty;
        private static string _emailToAddresses = string.Empty;

        private static string _ocrLocation = string.Empty;

        private static bool _debug = false;
        private static string _docFolder = string.Empty;

        private static bool _doIndex = true;
        private static bool _metadataOnly = false;

        private static bool _doFullIndex = false;

        private static bool _indexItems = true;
        private static bool _indexSegments = true;
        private static bool _indexAuthors = true;
        private static bool _indexKeywords = true;
        private static bool _indexNames = true;

        private static int _startItem = 1;
        private static int _startSegment = 1;
        private static int _startAuthor = 1;
        private static int _startKeyword = 1;
        private static int _startName = 1;

        private static string _itemSource = INDEXSOURCE.DB;
        private static string _segmentSource = INDEXSOURCE.DB;
        private static string _authorSource = INDEXSOURCE.DB;
        private static string _keywordSource = INDEXSOURCE.DB;
        private static string _nameSource = INDEXSOURCE.DB;

        private static bool _haltIncremental = false;

        private static HashSet<int> _indexedTitles = new HashSet<int>();
        private static HashSet<int> _indexedSegments = new HashSet<int>();
        private static ILogger _logger;

        static void Main(string[] args)
        {
            if (ReadCommandLineArguments(args))
            {
                ReadConfig();

                if (_doFullIndex)
                {
                    // Index everything
                    string logPath = "logs/BHLSearchIndexer-Full-" + _connectionKey + "-{Date}.log";
                    _logger = new LoggerConfiguration()
                        .WriteTo.RollingFile(logPath, shared: true)
                        .CreateLogger();

                    _logger.Information("Full Indexing Started");

                    /*
                    IndexSimple<Author>(ElasticSearch.ESIndex.AUTHORS, "Author", _startAuthor, _authorSource,
                        new DataAccess(_connectionKey).GetAuthorDocuments);
                    */

                    if (_indexItems) IndexItems();
                    if (_indexSegments) IndexSegments();
                    if (_indexAuthors) IndexAuthors();
                    if (_indexKeywords) IndexKeywords();
                    if (_indexNames) IndexNames();

                    if (_doIndex) OptimizeIndexes();

                    _logger.Information("Full Indexing Complete");
                }
                else
                {
                    // Read messages from queue and index appropriate entities
                    string logPath = "logs/BHLSearchIndexer-Incremental-" + _connectionKey + "-{Date}.log";
                    _logger = new LoggerConfiguration()
                        .WriteTo.RollingFile(logPath, shared: true)
                        .CreateLogger();

                    _logger.Information("Index Queue Monitoring Started");

                    try
                    {
                        using (QueueIO queueIO = new QueueIO(_mqAddress, _mqPort, _mqUser, _mqPw, _logger))
                        {
                            queueIO.PrefetchCount = _mqPrefetchCount;
                            queueIO.GetMessage(_mqQueueName, _mqErrorExchangeName, _mqErrorQueueName,
                                new IndexMessageProcessor(_esConnectionString, _dbConnectionString, 
                                    _ocrLocation));
                                    
                            // Register event handlers for SIGTERM and SIGINT (termination signals)
                            AssemblyLoadContext.Default.Unloading += SigTermEventHandler;
                            Console.CancelKeyPress += CancelHandler;

                            while (!_haltIncremental)
                            {
                                Thread.Sleep(10000);
                                //Console.WriteLine("Press enter to stop monitoring the index queue");
                                //Console.Read();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error processing the Search Index Queue.  INDEXING WILL HALT.");
                        SendEmailErrorNotification();
                    }

                    _logger.Information("Index Queue Monitoring Stopped");
                }
            }
        }

        private static void IndexItems()
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            int itemCount = 0;

            _logger.Information("Start Getting Items");
            List<int> itemIds = dataAccess.GetItems(_startItem, _itemSource == INDEXSOURCE.FILE);
            _logger.Information("Done Getting Items");

            foreach (int itemId in itemIds)
            {

                // TODO: Determine if this loop can be async so that multiple instances can run in parallel

                try
                {
                    List<Item> itemDocs = dataAccess.GetItemDocuments(itemId);
                    List<CatalogItem> catalogItemDocs = new List<CatalogItem>();
                    List<Page> pageDocs = new List<Page>();
                    List<Item> segmentDocs = new List<Item>();
                    if (itemDocs.Count > 0)
                    {
                        // Get the catalog entries for this item
                        foreach(Item itemDoc in itemDocs)
                        {
                            if (!_indexedTitles.Contains(itemDoc.titleId))
                            {
                                catalogItemDocs.Add(dataAccess.GetCatalogItemDocument(itemDoc));
                                _indexedTitles.Add(itemDoc.titleId);
                            }
                        }

                        if (!_metadataOnly)
                        {
                            // Get the pages for the item
                            pageDocs = dataAccess.GetPageDocuments(ElasticSearch.PageSource.Book, itemId,
                                _ocrLocation == "remote");

                            // Add the full text of the book to the item documents
                            StringBuilder fullText = new StringBuilder();
                            foreach (Page pageDoc in pageDocs) fullText.AppendLine(pageDoc.text);
                            foreach (Item itemDoc in itemDocs) itemDoc.text = fullText.ToString();

                            // Get the segments for the item
                            if (_indexSegments)
                            {
                                segmentDocs = dataAccess.GetSegmentDocuments(itemId.ToString(), null);

                                // Add the segmentId to each page in the segment.
                                // Get the full text for the segment.
                                foreach (Item segmentDoc in segmentDocs)
                                {
                                    // Get the catalog entry for this segment
                                    catalogItemDocs.Add(dataAccess.GetCatalogItemDocument(segmentDoc));

                                    // Get the pages for this segment
                                    Dictionary<int, Page> segmentPages = dataAccess.GetPages(ElasticSearch.PageSource.Segment, segmentDoc.segmentId);

                                    fullText.Clear();
                                    foreach (Page page in pageDocs)
                                    {
                                        if (segmentPages.ContainsKey(page.id))
                                        {
                                            /* THIS FIELD IS NOT NEEDED */
                                            //page.segments.Add(segmentDoc.segmentId);    // Add segment id to page

                                            fullText.Append(page.text);                 // Get text of page
                                        }
                                    }

                                    segmentDoc.text = fullText.ToString();
                                }
                            }
                        }
                    }

                    if (_debug)
                    {
                        // Write the documents to JSON files
                        ExportDocuments(ElasticSearch.ESIndex.CATALOG, catalogItemDocs, "catalog", itemId);
                        ExportDocuments(ElasticSearch.ESIndex.ITEMS, itemDocs, "item", itemId);
                        ExportDocuments(ElasticSearch.ESIndex.PAGES, pageDocs, "itempages", itemId);
                        if (_indexSegments) ExportDocuments(ElasticSearch.ESIndex.ITEMS, segmentDocs, "segments", itemId);
                    }

                    if (_doIndex)
                    {
                        ElasticSearch esCatalog = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.CATALOG);
                        esCatalog.IndexMany(catalogItemDocs);

                        if (_metadataOnly)
                        {
                            ElasticSearch esItems = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.ITEMS);
                            foreach(Item itemDoc in itemDocs) esItems.Update(itemDoc);
                        }
                        else
                        {
                            // Index the documents
                            ElasticSearch esItems = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.ITEMS);
                            esItems.IndexMany(itemDocs);
                            ElasticSearch esPages = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.PAGES);
                            esPages.IndexMany(pageDocs);
                            if (_indexSegments)
                            {
                                esItems.IndexMany(segmentDocs);
                                foreach (Item segment in segmentDocs) _indexedSegments.Add(segment.segmentId);
                            }
                        }
                    }

                    itemCount++;
                    if ((itemCount % 250) == 0)
                    {
                        _logger.Information("{NumItems} Items Indexed (most recent {ItemID})", itemCount, itemId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "ERROR Indexing Item {ItemID}", itemId);
                }
            }

            _logger.Information("{NumItems} Total Items Indexed", itemCount);
        }

        private static void IndexSegments()
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            int segmentCount = 0;

            _logger.Information("Start Getting Segments");
            List<int> segmentIds = dataAccess.GetSegments(_startSegment, _segmentSource == INDEXSOURCE.FILE);
            _logger.Information("Done Getting Segments");

            foreach (int segmentId in segmentIds)
            {

                // TODO: Determine if this loop can be async so that multiple instances can run in parallel

                try
                {
                    // If this segment has not already been indexed
                    if (!_indexedSegments.Contains(segmentId))
                    {
                        Item segmentDoc = dataAccess.GetSegmentDocuments(null, segmentId.ToString()).First();

                        // Get the catalog entry for this segment
                        CatalogItem catalogItemDoc = dataAccess.GetCatalogItemDocument(segmentDoc);

                        List<Page> pageDocs = new List<Page>();
                        if (segmentDoc != null)
                        {
                            if (!_metadataOnly)
                            {
                                // Get the pages for the segment
                                pageDocs = dataAccess.GetPageDocuments(ElasticSearch.PageSource.Segment, segmentId,
                                    _ocrLocation == "remote");

                                // Add the full text of the book to the segment document
                                StringBuilder fullText = new StringBuilder();
                                foreach (Page pageDoc in pageDocs) fullText.AppendLine(pageDoc.text);
                                segmentDoc.text = fullText.ToString();
                            }
                        }

                        if (_debug)
                        {
                            // Write the documents to JSON files
                            ExportDocuments(ElasticSearch.ESIndex.CATALOG, catalogItemDoc, "catalogitem", segmentId);
                            ExportDocuments(ElasticSearch.ESIndex.ITEMS, segmentDoc, "segment", segmentId);
                            ExportDocuments(ElasticSearch.ESIndex.PAGES, pageDocs, "segmentpages", segmentId);
                        }

                        if (_doIndex)
                        {
                            ElasticSearch esCatalog = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.CATALOG);
                            esCatalog.Index(catalogItemDoc);

                            if (_metadataOnly)
                            {
                                ElasticSearch esItems = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.ITEMS);
                                esItems.Update(segmentDoc);
                            }
                            else
                            {
                                // Index the documents
                                ElasticSearch esItems = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.ITEMS);
                                esItems.Index(segmentDoc);
                                ElasticSearch esPages = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.PAGES);
                                esPages.IndexMany(pageDocs);
                            }

                            _indexedSegments.Add(segmentId);
                        }
                    }

                    segmentCount++;
                    if ((segmentCount % 500) == 0)
                    {
                        _logger.Information("{NumSegments} Segments Indexed (most recent {SegmentID})", segmentCount, segmentId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "ERROR Indexing Segment {SegmentID}", segmentId);
                }
            }

            _logger.Information("{NumSegments} Total Segments Indexed", segmentCount);
        }

        /*
         * 
         *  Following is an attempt to make IndexAuthors, IndexKeywords, IndexNames into a single generic
         *  See https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods
         *
         * NOTE: THIS REQUIRES ADDITION OF THE SYSTEM.REFLECTION NUGET PACKAGE IN .NET CORE 1.1.
         * WOULD BE A MORE ELEGANT SOLUTION, BUT DON'T WORRY ABOUT THIS FOR NOW.
         * 
         * 
        public delegate IEnumerable<object> GetSimpleDocuments(int startName = 1, bool readFromFile = false);

        /// <summary>
        /// Generic method for indexing "simple" entities (authors, keywords, names).
        /// </summary>
        private static void IndexSimple<T> (string indexName, string entityType, int startEntity, 
            string entitySource, GetSimpleDocuments GetDocuments)
        {
            int entityCount = 0;
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            _logger.Information("Start Indexing Authors");

            try
            {
                // Read list of documents to index
                _logger.Information("Start Getting {Type}s", entityType);
                var entities = (List<T>)GetDocuments(startEntity, entitySource == INDEXSOURCE.FILE);
                _logger.Information("Done Getting {Type}s", entityType);

                int nextStartEntity = startEntity;
                {
                    int batchNumber = 1;
                    int numPerBatch = 10000; // Maximum number to be processed at one time
                    int totalBatches = entities.Count / numPerBatch + (entities.Count % numPerBatch == 0 ? 0 : 1);

                    while (batchNumber <= totalBatches)
                    {
                        // Get the documents to process in this batch
                        var entitySelection = entities
                            .Skip(numPerBatch * (batchNumber - 1))
                            .Take(numPerBatch)
                            .ToList<T>();

                        if (_debug)
                        {
                            // Write the documents to JSON files
                            ExportDocuments(indexName, entitySelection, entityType.ToLower(), batchNumber);
                        }

                        if (_doIndex)
                        {
                            ElasticSearch es = new ElasticSearch(_esConnectionString, indexName);
                            es.IndexMany(entitySelection);
                        }

                        _logger.Information("Last {Type} Indexed {ID}", entityType, entitySelection.Last().id);
                        _logger.Information("{NumIndexed} {Type}s Indexed", ((batchNumber - 1) * numPerBatch) + entitySelection.Count, entityType);
                        entityCount += entitySelection.Count;
                        batchNumber++;
                    }

                    _logger.Information("Start Getting {Type}s", entityType);
                    entities = GetDocuments(entities.Last().id, entitySource == INDEXSOURCE.FILE);
                    _logger.Information("Done Getting {Type}s", entityType);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, string.Format("ERROR Indexing {0}s. ABORTING operation.", entityType));
            }

            _logger.Information("{NumIndexed} Total {Type}s Indexed", entityCount, entityType);
        }
        */

        private static void IndexAuthors()
        {
            int authorCount = 0;
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            _logger.Information("Start Indexing Authors");

            try
            {
                // Read list of authors
                _logger.Information("Start Getting Authors");
                List<Author> authors = dataAccess.GetAuthorDocuments(_startAuthor, _authorSource == INDEXSOURCE.FILE);
                _logger.Information("Done Getting Authors");

                int nextStartAuthor = _startAuthor;
                while (authors.Count > 0)
                {
                    int batchNumber = 1;
                    int numPerBatch = 10000; // Maximum number of authors to be processed at one time
                    int totalBatches = authors.Count / numPerBatch + (authors.Count % numPerBatch == 0 ? 0 : 1);

                    while (batchNumber <= totalBatches)
                    {
                        // Get the authors to process in this batch
                        var authorSelection = authors
                            .Skip(numPerBatch * (batchNumber - 1))
                            .Take(numPerBatch)
                            .ToList<Author>();

                        if (_debug)
                        {
                            // Write the documents to JSON files
                            ExportDocuments(ElasticSearch.ESIndex.AUTHORS, authorSelection, "author", batchNumber);
                        }

                        if (_doIndex)
                        {
                            ElasticSearch es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.AUTHORS);
                            es.IndexMany(authorSelection);
                        }

                        _logger.Information("Last Author Indexed {AuthorID}", authorSelection.Last().id);
                        _logger.Information("{NumAuthors} Authors Indexed", ((batchNumber - 1) * numPerBatch) + authorSelection.Count);
                        authorCount += authorSelection.Count;
                        batchNumber++;
                    }

                    _logger.Information("Start Getting Authors");
                    authors = dataAccess.GetAuthorDocuments(authors.Last().id + 1, _authorSource == INDEXSOURCE.FILE);
                    _logger.Information("Done Getting Authors");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR Indexing Authors. ABORTING operation.");
            }

            _logger.Information("{NumAuthors} Total Authors Indexed", authorCount);
        }

        private static void IndexKeywords()
        {
            int keywordCount = 0;
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            _logger.Information("Start Indexing Keywords");

            try
            {
                // Read list of keywords
                _logger.Information("Start Getting Keywords");
                List<Keyword> keywords = dataAccess.GetKeywordDocuments(_startKeyword, _keywordSource == INDEXSOURCE.FILE);
                _logger.Information("Done Getting Keywords");

                int nextStartKeyword = _startKeyword;
                while (keywords.Count > 0)
                {
                    int batchNumber = 1;
                    int numPerBatch = 10000; // Maximum number of keywords to be processed at one time
                    int totalBatches = keywords.Count / numPerBatch + (keywords.Count % numPerBatch == 0 ? 0 : 1);

                    while (batchNumber <= totalBatches)
                    {
                        // Get the keywords to process in this batch
                        var keywordSelection = keywords
                            .Skip(numPerBatch * (batchNumber - 1))
                            .Take(numPerBatch)
                            .ToList<Keyword>();

                        if (_debug)
                        {
                            // Write the documents to JSON files
                            ExportDocuments(ElasticSearch.ESIndex.KEYWORDS, keywordSelection, "keyword", batchNumber);
                        }

                        if (_doIndex)
                        {
                            ElasticSearch es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.KEYWORDS);
                            es.IndexMany(keywordSelection);
                        }

                        _logger.Information("Last Keyword Indexed {KeywordID}", keywordSelection.Last().id);
                        _logger.Information("{NumKeywords} Keywords Indexed", ((batchNumber - 1) * numPerBatch) + keywordSelection.Count);
                        keywordCount += keywordSelection.Count;
                        batchNumber++;
                    }

                    _logger.Information("Start Getting Keywords");
                    keywords = dataAccess.GetKeywordDocuments(keywords.Last().id + 1, _keywordSource == INDEXSOURCE.FILE);
                    _logger.Information("Done Getting Keywords");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR Indexing Keywords. ABORTING operation.");
            }

            _logger.Information("{NumKeywords} Total Keywords Indexed", keywordCount);
        }

        private static void IndexNames()
        {
            int nameCount = 0;
            DataAccess dataAccess = new DataAccess(_dbConnectionString);

            _logger.Information("Start Indexing Names");

            try
            {
                // Read list of names
                _logger.Information("Start Getting Names");
                List<Name> names = dataAccess.GetNameDocuments(_startName, _nameSource == INDEXSOURCE.FILE);
                _logger.Information("Done Getting Names");

                int nextStartName = _startName;
                while (names.Count > 0)
                {
                    int batchNumber = 1;
                    int numPerBatch = 10000; // Maximum number of names to be processed at one time
                    int totalBatches = names.Count / numPerBatch + (names.Count % numPerBatch == 0 ? 0 : 1);

                    while (batchNumber <= totalBatches)
                    {
                        // Get the names to process in this batch
                        var nameSelection = names
                            .Skip(numPerBatch * (batchNumber - 1))
                            .Take(numPerBatch)
                            .ToList<Name>();

                        if (_debug)
                        {
                            // Write the documents to JSON files
                            ExportDocuments(ElasticSearch.ESIndex.NAMES, nameSelection, "name", batchNumber);
                        }

                        if (_doIndex)
                        {
                            ElasticSearch es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.NAMES);
                            es.IndexMany(nameSelection);
                        }

                        _logger.Information("Last Name Indexed {NameID}", nameSelection.Last().id);
                        _logger.Information("{NumNames} Names Indexed", ((batchNumber - 1) * numPerBatch) + nameSelection.Count);
                        nameCount += nameSelection.Count;
                        batchNumber++;
                    }

                    _logger.Information("Start Getting Names");
                    names = dataAccess.GetNameDocuments(names.Last().id + 1, _nameSource == INDEXSOURCE.FILE);
                    _logger.Information("Done Getting Names");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR Indexing Names. ABORTING operation.");
            }

            _logger.Information("{NumNames} Total Names Indexed", nameCount);
        }

        private static void OptimizeIndexes()
        {
            try
            {
                ElasticSearch es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.CATALOG);
                es.OptimizeIndex();
                es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.ITEMS);
                es.OptimizeIndex();
                es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.PAGES);
                es.OptimizeIndex();
                es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.AUTHORS);
                es.OptimizeIndex();
                es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.KEYWORDS);
                es.OptimizeIndex();
                es = new ElasticSearch(_esConnectionString, ElasticSearch.ESIndex.NAMES);
                es.OptimizeIndex();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR Optimizing Indexes.");
            }
        }

        private static void ExportDocuments(string indexName, object document, string documentType, int documentId)
        {
            try
            {
                string docFolder = string.Format(_docFolder, indexName);
                if (!Directory.Exists(docFolder)) Directory.CreateDirectory(docFolder);
                string data = JsonConvert.SerializeObject(document);
                File.WriteAllText(string.Format(@"{0}/{1}{2}.json", docFolder, documentType, documentId.ToString("000000")), data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR Writing Document {0}{1}.json to Disk.", documentType, documentId.ToString("000000"));
            }
        }

        /// <summary>
        ///  Capture SIGTERM signal sent by service termination (systemd on Linux)
        /// </summary>
        /// <param name="obj"></param>
        private static void SigTermEventHandler(AssemblyLoadContext obj)
        {
            _haltIncremental = true;
        }

        /// <summary>
        ///  Capture SIGINT (Ctrl-C) signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CancelHandler(object sender, ConsoleCancelEventArgs e)
        {
            _haltIncremental = true;
        }

        /// <summary>
        /// Read the config file name from the command line
        /// </summary>
        /// <returns></returns>
        private static bool ReadCommandLineArguments(string[] args)
        {
            /*
             The name of the config file should be the only argument.  If no file name
             is specified, AppConfig.xml is used.
             */
            if (args.Length > 0) _configFile = args[0];

            if (_configFile.ToLower() == "-h" || _configFile.ToLower() == "--help")
            {
                Console.WriteLine("Syntax:  BHLSearchIndexer [<CONFIG FILE NAME>]");
                return false;
            }

            if (!File.Exists(_configFile))
            {
                Console.WriteLine(string.Format("Could not read config file {0}", _configFile));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Read the config file settings
        /// </summary>
        /// <returns></returns>
        private static void ReadConfig()
        {
            _connectionKey = new ConfigurationManager(_configFile).AppSettings("ConnectionKey");
            _dbConnectionString = new ConfigurationManager(_configFile).ConnectionStrings(_connectionKey);

            _esConnectionString = new ConfigurationManager(_configFile).AppSettings("ElasticSearchServerAddress");

            _mqAddress = new ConfigurationManager(_configFile).AppSettings("MQAddress");
            _mqPort = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("MQPort"));
            _mqUser = new ConfigurationManager(_configFile).AppSettings("MQUser");
            _mqPw = new ConfigurationManager(_configFile).AppSettings("MQPassword");
            _mqQueueName = new ConfigurationManager(_configFile).AppSettings("MQQueueName");
            _mqErrorExchangeName = new ConfigurationManager(_configFile).AppSettings("MQErrorExchangeName");
            _mqErrorQueueName = new ConfigurationManager(_configFile).AppSettings("MQErrorQueueName");
            _mqPrefetchCount = Convert.ToUInt16(new ConfigurationManager(_configFile).AppSettings("MQPrefetchCount"));

            _smtpHost = new ConfigurationManager(_configFile).AppSettings("SmtpHost");
            _smtpPort = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("SmtpPort"));
            _smtpEnableSsl = new ConfigurationManager(_configFile).AppSettings("SmtpEnableSsl") == "true";
            _smtpDefaultCredentials = new ConfigurationManager(_configFile).AppSettings("SmtpDefaultCredentials") == "true";
            _smtpUser = new ConfigurationManager(_configFile).AppSettings("SmtpUsername");
            _smtpPw = new ConfigurationManager(_configFile).AppSettings("SmtpPassword");
            _emailFromAddress = new ConfigurationManager(_configFile).AppSettings("EmailFromAddress");
            _emailToAddresses = new ConfigurationManager(_configFile).AppSettings("EmailToAddresses");

            _ocrLocation = new ConfigurationManager(_configFile).AppSettings("OCRLocation");

            _debug = new ConfigurationManager(_configFile).AppSettings("Debug") == "true";
            _docFolder = new ConfigurationManager(_configFile).AppSettings("DocFolder");

            _doIndex = new ConfigurationManager(_configFile).AppSettings("DoIndex") == "true";
            _metadataOnly = new ConfigurationManager(_configFile).AppSettings("MetadataOnly") == "true";

            _doFullIndex = new ConfigurationManager(_configFile).AppSettings("DoFullIndex") == "true";

            _indexItems = new ConfigurationManager(_configFile).AppSettings("IndexItems") == "true";
            _indexSegments = new ConfigurationManager(_configFile).AppSettings("IndexSegments") == "true";
            _indexAuthors = new ConfigurationManager(_configFile).AppSettings("IndexAuthors") == "true";
            _indexKeywords = new ConfigurationManager(_configFile).AppSettings("IndexKeywords") == "true";
            _indexNames = new ConfigurationManager(_configFile).AppSettings("IndexNames") == "true";

            _startItem = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("StartItem"));
            _startSegment = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("StartSegment"));
            _startAuthor = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("StartAuthor"));
            _startKeyword = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("StartKeyword"));
            _startName = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("StartName"));

            _itemSource = new ConfigurationManager(_configFile).AppSettings("ItemSource");
            _segmentSource = new ConfigurationManager(_configFile).AppSettings("SegmentSource");
            _authorSource = new ConfigurationManager(_configFile).AppSettings("AuthorSource");
            _keywordSource = new ConfigurationManager(_configFile).AppSettings("KeywordSource");
            _nameSource = new ConfigurationManager(_configFile).AppSettings("NameSource");
        }

        /// <summary>
        /// Send an email notification
        /// </summary>
        /// <param name="message"></param>
        private static void SendEmailErrorNotification()
        {
            try
            {
                string thisComputer = Environment.MachineName;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("", _emailFromAddress));
                string[] toAddresses = _emailToAddresses.Split(',');
                foreach (string toAddress in toAddresses)
                {
                    mimeMessage.To.Add(new MailboxAddress("", toAddress));
                }
                mimeMessage.Subject = "BHLSearchIndexer: Index Queue Processing on " + thisComputer + " halted with errors";
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = "An error occurred while reading messages from the index queue.  See the BHLSearchIndexer logs for detailed information."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpHost, _smtpPort, _smtpEnableSsl);
                    if (!string.IsNullOrWhiteSpace(_smtpUser)) client.Authenticate(_smtpUser, _smtpPw);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Could not send Email");
            }
        }

        /// <summary>
        /// Possible sources of IDs of records to be indexed
        /// </summary>
        public static class INDEXSOURCE
        {
            public const string DB = "DB";
            public const string FILE = "FILE";
        }
    }
}
