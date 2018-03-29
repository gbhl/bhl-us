using BHL.QueueUtility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHL.SearchIndexer
{
    public class IndexMessageProcessor : IMessageProcessor
    {
        private string _searchConnectionString { get; set; }
        private string _dbConnectionstring { get; set; }
        private string _ocrLocation { get; set; }

        public IndexMessageProcessor(string searchConnectionString, string dbConnectionString, string ocrLocation)
        {
            _searchConnectionString = searchConnectionString;
            _dbConnectionstring = dbConnectionString;
            _ocrLocation = ocrLocation;
        }

        public bool ProcessMessage(string message)
        {
            // TODO:  Determine what to do with a failed message (Submit a NACK? Requeue? Add message to an error queue?)

            bool messageProcessed = true;

            // Parse Operation, Entity Type, and ID from the message
            string operation = string.Empty;
            string indexEntity = string.Empty;
            string id = string.Empty;

            string[] messageParts = message.Split('|');
            if (messageParts.Length == 3)
            {
                operation = messageParts[0];
                indexEntity = messageParts[1];
                id = messageParts[2];

                // Validate the message elements
                string[] operations = { "put", "delete" };
                string[] indexEntities = { "item", "segment", "author", "keyword", "nameresolved" };

                if (Array.IndexOf(operations, operation) >= 0 &&
                    Array.IndexOf(indexEntities, indexEntity) >= 0)
                {
                    bool idValid = true;
                    int idInt;
                    if (indexEntity == "item")
                    {
                        if (id.Contains("-"))
                        {
                            if (!Int32.TryParse(id.Split('-')[0], out idInt) ||
                                !Int32.TryParse(id.Split('-')[1], out idInt)) idValid = false;
                        }
                        else
                        {
                            idValid = false;
                        }
                    }
                    else
                    {
                        if (!Int32.TryParse(id, out idInt)) idValid = false;
                    }

                    if (idValid)
                    {
                        if (operation == "put")
                        {
                            AddToIndex(indexEntity, id);
                        }
                        else  // delete
                        {
                            DeleteFromIndex(indexEntity, id);
                        }
                    }
                    else
                    {
                        // Invalid Id in message
                    }
                }
                else
                {
                    // Invalid Operation or Index Entity in message
                }
            }
            else
            {
                // Improper message format
            }

            return messageProcessed;
        }

        /// <summary>
        /// Add/Update the specified record in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="indexEntity"></param>
        /// <param name="id"></param>
        private void AddToIndex(string indexEntity, string id)
        {
            switch (indexEntity)
            {
                case "item":
                    IndexItem(id);
                    break;
                case "segment":
                    IndexSegment(id);
                    break;
                case "author":
                    IndexAuthor(id);
                    break;
                case "keyword":
                    IndexKeyword(id);
                    break;
                case "nameresolved":
                    IndexName(id);
                    break;
            }
        }

        /// <summary>
        /// Delete the specified record from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="indexEntity"></param>
        /// <param name="id"></param>
        private void DeleteFromIndex(string indexEntity, string id)
        {
            switch (indexEntity)
            {
                case "item":
                    DeleteItem(id);
                    break;
                case "segment":
                    DeleteSegment(id);
                    break;
                case "author":
                    DeleteAuthor(id);
                    break;
                case "keyword":
                    DeleteKeyword(id);
                    break;
                case "name":
                    DeleteName(id);
                    break;
            }
        }

        /// <summary>
        /// Get metadata for the specified item and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexItem(string id)
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionstring);
            List<Item> items = dataAccess.GetItemDocuments(Convert.ToInt32(id));

            if (items.Count > 0)
            {
                // Get the pages for the item
                List<Page> pages = dataAccess
                    .GetPageDocuments(ElasticSearch.PageSource.Book, Convert.ToInt32(id), _ocrLocation == "remote");

                // Add the full text of the book to the item documents
                StringBuilder fullText = new StringBuilder();
                foreach (Page page in pages) fullText.AppendLine(page.text);
                foreach (Item item in items) item.text = fullText.ToString();

                // Update the SearchCatalog table
                foreach (Item item in items)
                {
                    dataAccess.UpsertItem(item.titleId, item.itemId, item.title, item.uniformTitle,
                        item.publicationDetails, item.publicationPlace, item.publisher, item.volume,
                        item.editionStatement, 
                        string.Join("-", item.dates.ToArray()),
                        string.Join("|", item.keywords.ToArray()),
                        string.Join("|", item.associations.ToArray()),
                        string.Join("|", item.variants.ToArray()),
                        string.Join("|", item.authors.ToArray()),
                        string.Join("|", item.searchAuthors.ToArray()),
                        string.Join("|", item.titleContributors.ToArray()),
                        string.Join("|", item.contributors.ToArray()),
                        item.firstPageId,
                        Convert.ToInt32(item.hasSegments),
                        Convert.ToInt16(item.hasLocalContent),
                        Convert.ToInt16(item.hasExternalContent),
                        Convert.ToInt16(item.hasIllustrations));
                }

                // Update the search indexes
                ElasticSearch es = new ElasticSearch(_searchConnectionString);
                es.IndexMany(items);
                es.IndexMany(pages);
            }
        }

        /// <summary>
        /// Delete the specified item from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteItem(string id)
        {
            string titleId = id.Split('-')[0];
            string itemId = id.Split('-')[1];

            DataAccess dataAccess = new DataAccess(_dbConnectionstring);

            // Delete from SearchCatalog table
            dataAccess.DeleteItem(Convert.ToInt32(titleId), Convert.ToInt32(itemId));

            // Delete from search index
            Item item = new Item { id = string.Format("i-{0}-{1}", titleId, itemId) };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.ITEMS).Delete(item);

            // Deleted pages and segments
            List<int> pages = dataAccess.GetPagesForItem(Convert.ToInt32(itemId));
            List<int> segments = dataAccess.GetSegmentsForItem(Convert.ToInt32(itemId));
            foreach (int page in pages) DeletePage(page);
            foreach (int segment in segments) DeleteSegment(segment.ToString());
        }

        /// <summary>
        /// Get metadata for the specified segment and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexSegment(string id)
        {
            DataAccess dataAccess = new DataAccess(_searchConnectionString);
            List<Item> segments = dataAccess.GetSegmentDocuments(null, id);

            if (segments.Count > 0)
            {
                // Get the pages for the segment
                List<Page> pages = dataAccess
                    .GetPageDocuments(ElasticSearch.PageSource.Segment, Convert.ToInt32(id), _ocrLocation == "remote");

                // Add the full text of the book to the segment document
                StringBuilder fullText = new StringBuilder();
                foreach (Page page in pages) fullText.AppendLine(page.text);
                foreach (Item segment in segments) segment.text = fullText.ToString();

                // Update the SearchCatalog table
                foreach(Item segment in segments)
                {
                    dataAccess.UpsertSegment(segment.segmentId, segment.itemId, segment.title, 
                        segment.translatedTitle, segment.container, 
                        (segment.publicationPlace + " " + segment.publisher).Trim(),
                        segment.volume, segment.series, segment.issue, 
                        string.Join("-", segment.dates.ToArray()),
                        string.Join("|", segment.keywords.ToArray()),
                        string.Join("|", segment.authors.ToArray()),
                        string.Join("|", segment.searchAuthors.ToArray()),
                        string.Join("|", segment.contributors.ToArray()),
                        Convert.ToInt16(segment.hasLocalContent), 
                        Convert.ToInt16(segment.hasExternalContent), 
                        Convert.ToInt16(segment.hasIllustrations));
                }

                // Update the search indexes
                ElasticSearch es = new ElasticSearch(_searchConnectionString);
                es.IndexMany(segments);
                es.IndexMany(pages);
            }
        }

        /// <summary>
        /// Delete the specified segment from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteSegment(string id)
        {
            // Delete from SearchCatalog table
            new DataAccess(_dbConnectionstring).DeleteSegment(Convert.ToInt32(id));

            // Delete from search index
            Item item = new Item { id = string.Format("s-{0}", id) };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.ITEMS).Delete(item);
        }

        /// <summary>
        /// Get metadata for the specified author and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexAuthor(string id)
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionstring);
            List<Author> authors = dataAccess.GetAuthorDocumentsFromDatabase(Convert.ToInt32(id), Convert.ToInt32(id));

            // Update the SearchCatalog tables
            foreach (Author author in authors)
            {
                dataAccess.UpsertAuthor(author.id, string.Join(" ", author.authorNames.ToArray()));
            }

            // Update the search indexes
            if (authors.Count > 0) new ElasticSearch(_searchConnectionString).IndexMany(authors);
        }

        /// <summary>
        /// Delete the specified author from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteAuthor(string id)
        {
            // Delete from SearchCatalogCreator table
            new DataAccess(_dbConnectionstring).DeleteAuthor(Convert.ToInt32(id));

            // Delete from search index
            Author author = new Author { id = Convert.ToInt32(id) };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.AUTHORS).Delete(author);
        }

        /// <summary>
        /// Get metadata for the specified keyword and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexKeyword(string id)
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionstring);
            List<Keyword> keywords = dataAccess.GetKeywordDocumentsFromDatabase(Convert.ToInt32(id), Convert.ToInt32(id));

            // Update the SearchCatalog tables
            foreach (Keyword keyword in keywords) dataAccess.UpsertKeyword(keyword.id, keyword.keyword);

            // Update the search indexes
            if (keywords.Count > 0) new ElasticSearch(_searchConnectionString).IndexMany(keywords);
        }

        /// <summary>
        /// Delete the specified keyword from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteKeyword(string id)
        {
            // Delete from SearchCatalogKeyword table
            new DataAccess(_dbConnectionstring).DeleteKeyword(Convert.ToInt32(id));

            // Delete from search index
            Keyword keyword = new Keyword { id = Convert.ToInt32(id) };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.KEYWORDS).Delete(keyword);
        }

        /// <summary>
        /// Get metadata for the specified name and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexName(string id)
        {
            List<Name> names = new DataAccess(_dbConnectionstring)
                .GetNameDocumentsFromDatabase(Convert.ToInt32(id), Convert.ToInt32(id));

            // Update the search indexes
            if (names.Count > 0) new ElasticSearch(_searchConnectionString).IndexMany(names);
        }

        /// <summary>
        /// Delete the specified name from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteName(string id)
        {
            // Delete from search index
            Name name = new Name { id = Convert.ToInt32(id) };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.NAMES).Delete(name);
        }

        /// <summary>
        /// Delete the specified name from the search indexes
        /// </summary>
        /// <param name="id"></param>
        private void DeletePage(int id)
        {
            // Delete from searchindex
            Page page = new Page { id = id };
            new ElasticSearch(_searchConnectionString, ElasticSearch.ESIndex.PAGES).Delete(page);
        }
    }
}
