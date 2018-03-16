using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BHL.SearchIndexer
{
    /// <summary>
    /// Much of this class should be moved to the BHL DAL assemblies if/when they are
    /// verified/modified to work with .NET Core.
    /// </summary>
    public class DataAccess
    {
        private string _connectionString = null;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<int> GetItems(int startItem = 1, bool readFromFile = false)
        {
            List<int> items = new List<int>();

            if (readFromFile)
            {
                // Read the items from a file
                items = GetItemsFromFile(startItem);
            }
            else
            {
                // Read the items from the database
                items = GetItemsFromDatabase(startItem);
            }

            // Sort the item IDs in ascending order
            items.Sort();

            return items;
        }

        public List<int> GetItemsFromFile(int startItem = 1)
        {
            List<int> items = new List<int>();

            string inputFile = @"data/items.txt";
            if (File.Exists(inputFile))
            {
                string[] itemsToIndex = File.ReadAllLines(inputFile);
                foreach (string itemToIndex in itemsToIndex)
                {
                    int item = Convert.ToInt32(itemToIndex);
                    if (item >= startItem) items.Add(item);
                }
            }

            return items;
        }

        public List<int> GetItemsFromDatabase(int startItem = 1)
        {
            List<int> items = new List<int>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.ItemSelectIDs";
                    sqlCommand.Parameters.AddWithValue("@StartID", startItem);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read()) items.Add(reader.GetInt32(reader.GetOrdinal("ItemID")));
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return items;
        }

        public Item GetItemDocument(int itemId)
        {
            Item item = null;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.ItemSelectDocumentForIndex";
                    sqlCommand.Parameters.AddWithValue("@ItemID", itemId);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new Item();
                            item.titleId = reader.GetInt32(reader.GetOrdinal("TitleID"));
                            item.itemId = reader.GetInt32(reader.GetOrdinal("ItemID"));
                            item.id = string.Format("i-{0}-{1}", item.titleId.ToString(), item.itemId.ToString());
                            item.barcode = reader.GetString(reader.GetOrdinal("Barcode"));
                            string partNumber = reader.GetString(reader.GetOrdinal("PartNumber"));
                            string partName = reader.GetString(reader.GetOrdinal("PartName"));
                            string fullTitle = reader.GetString(reader.GetOrdinal("FullTitle"));
                            item.title = (fullTitle + " " + partNumber + " " + partName).Trim();
                            item.uniformTitle = reader.GetString(reader.GetOrdinal("UniformTitle"));
                            item.sortTitle = reader.GetString(reader.GetOrdinal("SortTitle"));
                            string languageName = reader.GetString(reader.GetOrdinal("LanguageName"));
                            if (!string.IsNullOrWhiteSpace(languageName)) item.language = languageName;
                            string genre = reader.GetString(reader.GetOrdinal("BibliographicLevelName"));
                            if (!string.IsNullOrWhiteSpace(genre)) item.genre = genre;
                            string materialType = reader.GetString(reader.GetOrdinal("MaterialTypeLabel"));
                            if (!string.IsNullOrWhiteSpace(materialType)) item.materialType = materialType;
                            item.oclc = GetFieldList(reader, "OCLC", '|');
                            item.issn = GetFieldList(reader, "ISSN", '|');
                            item.isbn = GetFieldList(reader, "ISBN", '|');
                            item.doi = reader.GetString(reader.GetOrdinal("DOIName"));
                            item.collections = GetFieldList(reader, "Collections", '|');
                            item.authors = GetFieldList(reader, "Authors", '|');
                            item.facetAuthors = GetFieldList(reader, "FacetAuthors", '|');
                            item.searchAuthors = GetFieldList(reader, "SearchAuthors", '|');
                            item.keywords = GetFieldList(reader, "Subjects", '|');
                            item.associations = GetFieldList(reader, "Associations", '|');
                            item.variants = GetFieldList(reader, "Variants", '|');
                            item.contributors = GetFieldList(reader, "Contributors", '|');
                            item.volume = reader.GetString(reader.GetOrdinal("Volume"));
                            item.publisher = reader.GetString(reader.GetOrdinal("PublisherName"));
                            item.publicationPlace = GetCleanPublisherPlace(reader.GetString(reader.GetOrdinal("PublisherPlace")));
                            item.dates = GetCleanDates(reader.GetString(reader.GetOrdinal("Date")));
                            item.dateRanges = GetDateRanges(item.dates);
                            item.url = reader.GetString(reader.GetOrdinal("Url"));
                            item.hasSegments = reader.GetInt16(reader.GetOrdinal("HasSegments")) == 1;
                            item.hasLocalContent = reader.GetInt16(reader.GetOrdinal("HasLocalContent")) == 1;
                            item.hasExternalContent = reader.GetInt16(reader.GetOrdinal("HasExternalContent")) == 1;
                            item.hasIllustrations = reader.GetInt16(reader.GetOrdinal("HasIllustrations")) == 1;
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return item;
        }

        public List<int> GetSegments(int startSegment = 1, bool readFromFile = false)
        {
            List<int> segments = new List<int>();

            if (readFromFile)
            {
                // Read the segments from a file
                segments = GetSegmentsFromFile(startSegment);
            }
            else
            {
                // Read the segments from the database
                segments = GetSegmentsFromDatabase(startSegment);
            }

            // Sort the segment IDs in ascending order
            segments.Sort();

            return segments;
        }

        public List<int> GetSegmentsFromFile(int startSegment = 1)
        {
            List<int> segments = new List<int>();

            string inputFile = @"data/segments.txt";
            if (File.Exists(inputFile))
            {
                string[] segmentsToIndex = File.ReadAllLines(inputFile);
                foreach (string segmentToIndex in segmentsToIndex)
                {
                    int segment = Convert.ToInt32(segmentToIndex);
                    if (segment >= startSegment) segments.Add(segment);
                }
            }

            return segments;
        }

        public List<int> GetSegmentsFromDatabase(int startSegment = 1)
        {
            List<int> segments = new List<int>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SegmentSelectIDs";
                    sqlCommand.Parameters.AddWithValue("@StartID", startSegment);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read()) segments.Add(reader.GetInt32(reader.GetOrdinal("SegmentID")));
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return segments;
        }

        public List<Item> GetSegmentDocuments(string itemId, string segmentId)
        {
            List<Item> segments = new List<Item>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SegmentSelectDocumentForIndex";
                    sqlCommand.Parameters.AddWithValue("@ItemID", itemId);
                    sqlCommand.Parameters.AddWithValue("@SegmentID", segmentId);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item segment = new Item();
                            segment.segmentId = reader.GetInt32(reader.GetOrdinal("SegmentID"));
                            segment.id = string.Format("s-{0}", segment.segmentId.ToString());
                            segment.title = reader.GetString(reader.GetOrdinal("Title"));
                            segment.translatedTitle = reader.GetString(reader.GetOrdinal("TranslatedTitle"));
                            segment.sortTitle = reader.GetString(reader.GetOrdinal("SortTitle"));
                            segment.container = reader.GetString(reader.GetOrdinal("ContainerTitle"));
                            string languageName = reader.GetString(reader.GetOrdinal("LanguageName"));
                            if (!string.IsNullOrWhiteSpace(languageName)) segment.language = languageName;
                            string genre = reader.GetString(reader.GetOrdinal("GenreName"));
                            if (!string.IsNullOrWhiteSpace(genre)) segment.genre = genre;
                            string materialType = reader.GetString(reader.GetOrdinal("MaterialTypeLabel"));
                            if (!string.IsNullOrWhiteSpace(materialType)) segment.materialType = materialType;
                            segment.doi = reader.GetString(reader.GetOrdinal("DOIName"));
                            segment.authors = GetFieldList(reader, "Authors", '|');
                            segment.facetAuthors = GetFieldList(reader, "FacetAuthors", '|');
                            segment.searchAuthors = GetFieldList(reader, "SearchAuthors", '|');
                            segment.keywords = GetFieldList(reader, "Subjects", '|');
                            segment.contributors = GetFieldList(reader, "Contributors", '|');
                            segment.volume = reader.GetString(reader.GetOrdinal("Volume"));
                            segment.issue = reader.GetString(reader.GetOrdinal("Issue"));
                            segment.series = reader.GetString(reader.GetOrdinal("Series"));
                            segment.publisher = reader.GetString(reader.GetOrdinal("PublisherName"));
                            segment.publicationPlace = GetCleanPublisherPlace(reader.GetString(reader.GetOrdinal("PublisherPlace")));
                            segment.dates = GetCleanDates(reader.GetString(reader.GetOrdinal("Date")));
                            segment.dateRanges = GetDateRanges(segment.dates);
                            segment.pageRange = reader.GetString(reader.GetOrdinal("PageRange"));
                            segment.url = reader.GetString(reader.GetOrdinal("Url"));
                            if (!reader.IsDBNull(reader.GetOrdinal("StartPageID")))
                            {
                                segment.startPageId = reader.GetInt32(reader.GetOrdinal("StartPageID"));
                            }
                            segment.hasLocalContent = reader.GetInt16(reader.GetOrdinal("HasLocalContent")) == 1;
                            segment.hasExternalContent = reader.GetInt16(reader.GetOrdinal("HasExternalContent")) == 1;
                            segment.hasIllustrations = reader.GetInt16(reader.GetOrdinal("HasIllustrations")) == 1;

                            segments.Add(segment);
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return segments;
        }

        public Dictionary<int, Page> GetPages(ElasticSearch.PageSource pageSource, int sourceId)
        {
            Dictionary<int, Page> pages = new Dictionary<int, Page>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    if (pageSource == ElasticSearch.PageSource.Book)
                    {
                        sqlCommand.CommandText = "srchindex.PageSelectToIndexForItem";
                        sqlCommand.Parameters.AddWithValue("@ItemID", sourceId);
                    }
                    else
                    {
                        sqlCommand.CommandText = "srchindex.PageSelectToIndexForSegment";
                        sqlCommand.Parameters.AddWithValue("@SegmentID", sourceId);
                    }

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Page page = new Page();
                            page.id = reader.GetInt32(reader.GetOrdinal("PageID"));
                            page.sequence = reader.GetInt32(reader.GetOrdinal("SequenceOrder"));
                            page.itemId = reader.GetInt32(reader.GetOrdinal("ItemID"));
                            page.pageIndicators = GetFieldList(reader, "PageIndicators", ',');
                            page.pageTypes = GetFieldList(reader, "PageTypes", ',');

                            string ocrFolderShare = reader.GetString(reader.GetOrdinal("OcrFolderShare")).Replace("\\\\bhl\\", "//bhl.mobot.org/").Replace("\\", "/");
                            string fileRootFolder = reader.GetString(reader.GetOrdinal("FileRootFolder")).Replace("\\", "/");
                            string barcode = reader.GetString(reader.GetOrdinal("BarCode")).Replace("\\", "/");
                            string fileNamePrefix = reader.GetString(reader.GetOrdinal("FileNamePrefix")).Replace("\\", "/");
                            string ocrTextLocation = string.Format("{0}/{1}/{2}/{3}.txt", ocrFolderShare, fileRootFolder, barcode, fileNamePrefix);
                            page.textPath = ocrTextLocation;

                            if (!pages.ContainsKey(page.id)) pages.Add(page.id, page);
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return pages;
        }

        public List<Page> GetPageDocuments(ElasticSearch.PageSource pageSource, int sourceId, bool remoteOcr)
        {
            List<Page> pages = new List<Page>();

            // Get the pages for the item/segment
            Dictionary<int, Page> itemPages = GetPages(pageSource, sourceId);

            // Get names for each page
            Dictionary<int, List<string>> itemNames = GetNameStrings(pageSource, sourceId);

            // Merge the names with the pages
            MergeNames(itemPages, itemNames);

            // Transfer pages to final list
            foreach (KeyValuePair<int, Page> kvp in itemPages) pages.Add(kvp.Value);

            // Get text for each page
            foreach (Page page in pages)
            {
                if (remoteOcr)  // OCRLocation = "remote"
                    page.text = GetText(page.id);     // Use URLs
                else // OCRLocation = "local"
                    page.text = GetText(page.textPath);     // Use direct paths
            }

            return pages;
        }

        public List<Author> GetAuthorDocuments(int startAuthor = 1, bool readFromFile = false)
        {
            List<Author> authors = new List<Author>();

            if (readFromFile)
            {
                // Read the author information from a file
                authors = GetAuthorDocumentsFromFile(startAuthor);
            }
            else
            {
                // Read the author information from the database
                authors = GetAuthorDocumentsFromDatabase(startAuthor);
            }

            return authors;
        }

        public List<Author> GetAuthorDocumentsFromFile(int startAuthor = 1)
        {
            List<Author> authors = new List<Author>();

            // Read the author information from a file
            string inputFile = @"data/authors.txt";
            if (File.Exists(inputFile))
            {
                string[] authorsToIndex = File.ReadAllLines(inputFile);
                foreach (string authorToIndex in authorsToIndex)
                {
                    string[] authorData = authorToIndex.Split('\t');
                    Author author = new Author();
                    int id = Convert.ToInt32(authorData[0]);
                    author.id = id;
                    author.authorNames = GetFieldList(authorData[1], '|');
                    author.primaryAuthorName = authorData[2];
                    if (id > startAuthor) authors.Add(author);
                }
            }

            return authors;
        }

        public List<Author> GetAuthorDocumentsFromDatabase(int startAuthor = 1)
        {
            List<Author> authors = new List<Author>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    sqlCommand.CommandText = "srchindex.AuthorSelectDocumentsForIndex";
                    sqlCommand.Parameters.AddWithValue("@StartID", startAuthor);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Author author = new Author();
                            author.id = reader.GetInt32(reader.GetOrdinal("AuthorID"));
                            author.authorNames = GetFieldList(reader, "AuthorNames", '|');
                            author.primaryAuthorName = reader.GetString(reader.GetOrdinal("PrimaryAuthorName"));
                            authors.Add(author);
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return authors;
        }

        public List<Keyword> GetKeywordDocuments(int startKeyword = 1, bool readFromFile = false)
        {
            List<Keyword> keywords = new List<Keyword>();

            if (readFromFile)
            {
                // Read the keyword information from a file
                keywords = GetKeywordDocumentsFromFile(startKeyword);
            }
            else
            {
                // Read the keyword information from the database
                keywords = GetKeywordDocumentsFromDatabase(startKeyword);
            }

            return keywords;
        }

        public List<Keyword> GetKeywordDocumentsFromFile(int startKeyword = 1)
        {
            List<Keyword> keywords = new List<Keyword>();

            string inputFile = @"data/keywords.txt";
            if (File.Exists(inputFile))
            {
                string[] keywordsToIndex = File.ReadAllLines(inputFile);
                foreach (string keywordToIndex in keywordsToIndex)
                {
                    string[] keywordData = keywordToIndex.Split('\t');
                    Keyword keyword = new Keyword();
                    int id = Convert.ToInt32(keywordData[0]);
                    keyword.id = id;
                    keyword.keyword = keywordData[1];
                    if (id > startKeyword) keywords.Add(keyword);
                }
            }

            return keywords;
        }

        public List<Keyword> GetKeywordDocumentsFromDatabase(int startKeyword = 1)
        {
            List<Keyword> keywords = new List<Keyword>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    sqlCommand.CommandText = "srchindex.KeywordSelectDocumentsForIndex";
                    sqlCommand.Parameters.AddWithValue("@StartID", startKeyword);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Keyword keyword = new Keyword();
                            keyword.id = reader.GetInt32(reader.GetOrdinal("KeywordID"));
                            keyword.keyword = reader.GetString(reader.GetOrdinal("Keyword"));
                            keywords.Add(keyword);
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return keywords;
        }

        public List<Name> GetNameDocuments(int startName = 1, bool readFromFile = false)
        {
            List<Name> names = new List<Name>();

            // If a file containing a list of names to index is provided, use it.
            // Otherwise read the list of names from the database.
            if (readFromFile)
            {
                // Get name information from a file
                names = GetNameDocumentsFromFile(startName);
            }
            else
            {
                // Get name information from the database
                names = GetNameDocumentsFromDatabase(startName);
            }

            // Sort the names by ID in ascending order
            NameComparer comp = new NameComparer();
            names.Sort(comp.Compare);

            return names;
        }

        public List<Name> GetNameDocumentsFromFile(int startName = 1)
        {
            List<Name> names = new List<Name>();

            string inputFile = @"data/names.txt";
            if (File.Exists(inputFile))
            {
                string[] namesToIndex = File.ReadAllLines(inputFile);
                foreach (string nameToIndex in namesToIndex)
                {
                    string[] nameData = nameToIndex.Split('\t');
                    Name name = new Name();
                    int id = Convert.ToInt32(nameData[0]);
                    name.id = id;
                    name.name = nameData[1];
                    name.count = Convert.ToInt32(nameData[2]);
                    if (id > startName) names.Add(name);
                }
            }

            return names;
        }

        public List<Name> GetNameDocumentsFromDatabase(int startName = 1)
        {
            List<Name> names = new List<Name>();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    sqlCommand.CommandText = "srchindex.NameSelectDocumentsForIndex";
                    sqlCommand.Parameters.AddWithValue("@StartID", startName);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name name = new Name();
                            name.id = reader.GetInt32(reader.GetOrdinal("NameResolvedID"));
                            name.name = reader.GetString(reader.GetOrdinal("ResolvedNameString"));
                            name.count = reader.GetInt32(reader.GetOrdinal("NameCount"));
                            names.Add(name);
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return names;
        }

        private Dictionary<int, List<string>> GetNameStrings(ElasticSearch.PageSource pageSource, int sourceID)
        {
            Dictionary<int, List<string>> names = new Dictionary<int, List<string>>();

            /*
             * Pages will not be faceted on Names for now, so do not include them in the index.
             * If Names are added to the Pages index in the future, performance of the following
             * queries will need to be addressed.
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandTimeout = 600;
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    if (pageSource == ElasticSearch.PageSource.Book)
                    {
                        sqlCommand.CommandText =
                            "SELECT p.PageID, nr.ResolvedNameString " +
                            "FROM dbo.NamePage np " +
                                "INNER JOIN dbo.Name n WITH (NOLOCK) ON np.NameID = n.NameID " +
                                "INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID " +
                                "INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID " +
                            "WHERE p.ItemID = " + sourceID + 
                            " AND p.Active = 1";
                    }
                    else
                    {
                        sqlCommand.CommandText =
                            "SELECT p.PageID, nr.ResolvedNameString " +
                            "FROM dbo.NamePage np " +
                                "INNER JOIN dbo.Name n WITH (NOLOCK) ON np.NameID = n.NameID " +
                                "INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID " +
                                "INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID " +
                                "INNER JOIN dbo.SegmentPage sp WITH (NOLOCK) ON p.PageID = sp.PageID " +
                            "WHERE sp.SegmentID = " + sourceID + 
                            " AND p.Active = 1";
                    }

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int key = reader.GetInt32(reader.GetOrdinal("PageID"));
                            List<string> pageNames = (names.ContainsKey(key) ? names[key] : new List<string>());
                            pageNames.Add(reader.GetString(reader.GetOrdinal("ResolvedNameString")));
                            names[key] = pageNames;
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }
            */

            return names;
        }

        /// <summary>
        /// Get the specified field from the data reader and split it at the specified delimiter
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        private List<string> GetFieldList(SqlDataReader reader, string fieldName, char delimiter)
        {
            string fieldValue = reader.GetString(reader.GetOrdinal(fieldName));
            return GetFieldList(fieldValue, delimiter);
        }

        /// <summary>
        /// Split the specified value at the specified delimiter
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        private List<string> GetFieldList(string fieldValue, char delimiter)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(fieldValue))
            {
                string[] values = fieldValue.Split(delimiter);
                foreach (string value in values) if (!string.IsNullOrWhiteSpace(value)) list.Add(value.Trim());
            }
            return list;
        }

        /// <summary>
        /// Merge the list of names with the list of pages
        /// </summary>
        /// <param name="listPages"></param>
        /// <param name="listNames"></param>
        private void MergeNames(Dictionary<int, Page> listPages, Dictionary<int, List<string>> listNames)
        {
            foreach (KeyValuePair<int, List<string>> kvp in listNames)
            {
                if (listPages.ContainsKey(kvp.Key)) listPages[kvp.Key].names = kvp.Value;
            }
        }

        /// <summary>
        /// Get OCR text using the BHL website.  Can be used anywhere with Internet access.
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetText(int pageId)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://www.biodiversitylibrary.org/pageocr/" + pageId.ToString()).Result;
            byte[] textBytes = response.Content.ReadAsByteArrayAsync().Result;
            return Encoding.UTF8.GetString(textBytes);
        }

        /// <summary>
        /// Get OCR text directly from the source files.  Can only be used from the network on which BHL is running.
        /// </summary>
        /// <param name="textPath"></param>
        /// <returns></returns>
        private string GetText(string textPath)
        {
            string ocrText = string.Empty;
            if (File.Exists(textPath))
            {
                ocrText = File.ReadAllText(textPath, Encoding.UTF8);
            }
            else
            {
                // If on the Red Hat server, replace the drive letter with a folder name
                if (textPath.Contains("B:"))
                {
                    textPath = textPath.Replace("B:", "/data");
                    if (File.Exists(textPath))
                    {
                        ocrText = File.ReadAllText(textPath, Encoding.UTF8);
                    }
                }
            }

            return ocrText;
        }

        private string GetCleanPublisherPlace(string place)
        {
            place = place.Replace("?", "");
            place = place.Replace(":", "");
            place = place.Replace(".", "");
            place = place.Replace(",", "");
            place = place.Replace(";", "");
            place = place.Replace("[ect]", "");
            place = place.Replace("[", "");
            place = place.Replace("]", "");
            place = place.Replace(" etc", "");
            place = place.Trim();

            return place;
        }

        private List<string> GetCleanDates(string date)
        {
            List<string> dates = new List<string>();

            if (!string.IsNullOrWhiteSpace(date))
            {
                // The goal of the following is to reduce the date value to one of the following forms:
                //      XXXX
                //      XXXX-XXXX
                //      XXXX,XXXX
                if (date.Length >= 4)
                {
                    date = (date.Substring(date.Length - 1) == ".") ? date.Substring(0, date.Length - 1) : date;
                    date = date.Replace("?", "");
                    date = date.Replace("(", "");
                    date = date.Replace(")", "");
                    date = date.Replace("[", "");
                    date = date.Replace("]", "");
                    date = date.Replace(" - ", "-");
                    date = date.Replace(" -", "-");
                    date = date.Replace("- ", "-");
                    date = date.Replace(":", "");
                    date = date.Replace("spring", "");
                    date = date.Replace("summer", "");
                    date = date.Replace("fall", "");
                    date = date.Replace("winter", "");
                    date = date.Replace(", ", ",");
                    date = (date.Substring(date.Length - 2) == "--") ? date.Substring(0, date.Length - 2) : date;
                    date = (date.Substring(date.Length - 1) == "-") ? date.Substring(0, date.Length - 1) : date;
                    date = (date.Substring(date.Length - 1) == "/") ? date.Substring(0, date.Length - 1) : date;
                    date = date.Trim();

                    // Now determine the all of the dates represented by the date string
                    int dateValue;
                    if ((date.Length == 4) && Int32.TryParse(date, out dateValue))  // XXXX
                    {
                        // Add the date
                        dates.Add(date);
                    }

                    if (date.Length == 9)
                    {
                        int firstDateValue;
                        int secondDateValue;
                        if (date.Substring(4, 1) == ",")    // XXXX,XXXX
                        {
                            // Add the two comma-separated dates
                            if (Int32.TryParse(date.Substring(0, 4), out firstDateValue)) dates.Add(firstDateValue.ToString());
                            if (Int32.TryParse(date.Substring(5, 4), out secondDateValue)) dates.Add(secondDateValue.ToString());
                        }

                        if (date.Substring(4, 1) == "-")    // XXXX-XXXX
                        {
                            // Add all dates in the range
                            if (Int32.TryParse(date.Substring(0, 4), out firstDateValue) &&
                                Int32.TryParse(date.Substring(5, 4), out secondDateValue))
                            {
                                while (firstDateValue <= secondDateValue)
                                {
                                    dates.Add(firstDateValue.ToString());
                                    firstDateValue++;
                                }
                            }
                        }
                    }
                }
            }

            return dates;
        }

        private List<string> GetDateRanges(List<string> dates)
        {
            HashSet<string> ranges = new HashSet<string>();

            foreach (string date in dates)
            {
                int dateValue;
                if (Int32.TryParse(date, out dateValue))
                {
                    if (dateValue <= 1600) { ranges.Add("1600 and Earlier"); }
                    if (dateValue > 1600 && dateValue <= 1700) { ranges.Add("1601-1700"); }
                    if (dateValue > 1700 && dateValue <= 1800) { ranges.Add("1701-1800"); }
                    if (dateValue > 1800 && dateValue <= 1825) { ranges.Add("1801-1825"); }
                    if (dateValue > 1825 && dateValue <= 1850) { ranges.Add("1826-1850"); }
                    if (dateValue > 1850 && dateValue <= 1875) { ranges.Add("1851-1875"); }
                    if (dateValue > 1875 && dateValue <= 1900) { ranges.Add("1876-1900"); }
                    if (dateValue > 1900 && dateValue <= 1925) { ranges.Add("1901-1925"); }
                    if (dateValue > 1925 && dateValue <= 1950) { ranges.Add("1926-1950"); }
                    if (dateValue > 1950 && dateValue <= 1975) { ranges.Add("1951-1975"); }
                    if (dateValue > 1975 && dateValue <= 2000) { ranges.Add("1976-2000"); }
                    if (dateValue > 2000) { ranges.Add("2000 and Later"); }
                }
            }

            return ranges.ToList();
        }
    }
}
