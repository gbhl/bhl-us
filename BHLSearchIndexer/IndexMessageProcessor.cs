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

        private string _catalogIndex { get; set; }
        private string _itemsIndex { get; set; }
        private string _pagesIndex { get; set; }
        private string _authorsIndex { get; set; }
        private string _keywordsIndex { get; set; }
        private string _namesIndex { get; set; }

        public IndexMessageProcessor(string searchConnectionString, string dbConnectionString, string ocrLocation,
            string catalogIndex, string itemsIndex, string pagesIndex, string authorsIndex, string keywordsIndex,
            string namesIndex)
        {
            _searchConnectionString = searchConnectionString;
            _dbConnectionstring = dbConnectionString;
            _ocrLocation = ocrLocation;

            _catalogIndex = catalogIndex;
            _itemsIndex = itemsIndex;
            _pagesIndex = pagesIndex;
            _authorsIndex = authorsIndex;
            _keywordsIndex = keywordsIndex;
            _namesIndex = namesIndex;
        }

        public bool ProcessMessage(string message)
        {
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
                        throw new Exception(string.Format("Invalid id '{0}' in message '{1}'", 
                            id, message));
                    }
                }
                else
                {
                    // Invalid Operation or Index Entity in message
                    throw new Exception(
                        string.Format("Invalid operation '{0}' or entity '{1}' in message '{2}'", 
                        operation, indexEntity, message));
                }
            }
            else
            {
                // Improper message format
                throw new Exception(string.Format("Improperly formatted message '{0}'", message));
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
                case "nameresolved":
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
            string itemId = id.Split('-')[1];

            DataAccess dataAccess = new DataAccess(_dbConnectionstring);
            List<Item> items = dataAccess.GetItemDocuments(Convert.ToInt32(itemId));

            if (items.Count > 0)
            {
                List<CatalogItem> catalogItems = new List<CatalogItem>();

                // Get the pages for the item
                List<Page> pages = dataAccess
                    .GetPageDocuments(ElasticSearch.PageSource.Book, Convert.ToInt32(itemId), _ocrLocation == "remote");

                // Add the full text of the book to the item documents
                StringBuilder fullText = new StringBuilder();
                foreach (Page page in pages) fullText.AppendLine(page.text);
                foreach (Item item in items) item.text = fullText.ToString();

                // Update the SearchCatalog table
                foreach (Item item in items)
                {
                    // NOTE:  Notes are not included here.  They are not part of the SQL-based SearchCatalog tables.
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

                    catalogItems.Add(dataAccess.GetCatalogItemDocument(item));
                }

                // Update the search indexes
                new ElasticSearch(_searchConnectionString, _catalogIndex).IndexMany(catalogItems);
                new ElasticSearch(_searchConnectionString, _itemsIndex).IndexMany(items);
                new ElasticSearch(_searchConnectionString, _pagesIndex).IndexMany(pages);
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

            // Get statuses of the Title and Item
            Tuple<bool, bool> statuses = dataAccess.GetItemAndTitleStatus(Convert.ToInt32(titleId), Convert.ToInt32(itemId));

            // Delete index entries for the title-item pair
            DeleteItemFromIndexes(titleId, itemId);

            if (!statuses.Item2)    // If Item not active
            {
                // Delete pages and segments related to the item
                DeletePagesForItem(itemId);
                DeleteSegmentsFromIndexForItem(itemId);
            }

            if (statuses.Item1)    // If Title active
            {
                // Get count of remaining items for title
                if (dataAccess.ItemCountForTitle(Convert.ToInt32(titleId)) == 0)
                {
                    // No remaining active items for this title, so remove it from the catalog index
                    DeleteTitleFromIndexes(titleId);
                }
                else
                {
                    // There are additional active items for this title, so reindex it in the catalog index
                    List<Item> items = dataAccess.GetItemDocuments((int)dataAccess.SelectFirstItem(Convert.ToInt32(titleId)));
                    foreach (Item item in items)
                    {
                        if (item.titleId == Convert.ToInt32(titleId))
                        {
                            CatalogItem catalogItem = dataAccess.GetCatalogItemDocument(item);
                            new ElasticSearch(_searchConnectionString, _catalogIndex).Index(catalogItem);
                            break;
                        }
                    }
                }
            }
            else  // Title not active
            {
                // Delete items related to the title
                //  We do not delete related pages or segments here because the items may be attached
                //  to other titles.  We only want to remove the title-item relationships.
                DeleteItemsFromIndexForTitle(titleId);

                // Remove title from the catalog index
                DeleteTitleFromIndexes(titleId);
            }
        }

        /// <summary>
        /// Delete the specified title-item combination from search indexes
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="itemId"></param>
        private void DeleteItemFromIndexes(string titleId, string itemId)
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionstring);

            // Delete from SearchCatalog table
            dataAccess.DeleteItem(Convert.ToInt32(titleId), Convert.ToInt32(itemId));

            // Delete from search index
            Item deleteItem = new Item { id = string.Format("i-{0}-{1}", titleId, itemId) };
            new ElasticSearch(_searchConnectionString, _itemsIndex).Delete(deleteItem);
        }

        private void DeleteItemsFromIndexForTitle(string id)
        {
            // Delete from SeachCatalog table
            new DataAccess(_dbConnectionstring).DeleteItemsFromIndexForTitle(Convert.ToInt32(id));

            // Delete from search indexes
            new ElasticSearch(_searchConnectionString, _itemsIndex).DeleteAllItems(id);
        }

        /// <summary>
        /// Delete the specified title from search indexes
        /// </summary>
        /// <param name="titleId"></param>
        private void DeleteTitleFromIndexes(string titleId)
        {
            // Remove title from the catalog index
            CatalogItem catalogItem = new CatalogItem { id = string.Format("t-{0}", titleId) };
            new ElasticSearch(_searchConnectionString, _catalogIndex).Delete(catalogItem);
        }

        /// <summary>
        /// Get metadata for the specified segment and add/update it in the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void IndexSegment(string id)
        {
            DataAccess dataAccess = new DataAccess(_dbConnectionstring);
            List<Item> segments = dataAccess.GetSegmentDocuments(null, id);

            if (segments.Count > 0)
            {
                // Get the catalogitems for the segment
                List<CatalogItem> catalogItems = new List<CatalogItem>();
                foreach(Item segment in segments)
                {
                    catalogItems.Add(dataAccess.GetCatalogItemDocument(segment));
                }

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
                new ElasticSearch(_searchConnectionString, _catalogIndex).IndexMany(catalogItems);
                new ElasticSearch(_searchConnectionString, _itemsIndex).IndexMany(segments);
                new ElasticSearch(_searchConnectionString, _pagesIndex).IndexMany(pages);
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

            // Delete from search indexes
            CatalogItem catalogItem = new CatalogItem { id = string.Format("s-{0}", id) };
            Item item = new Item { id = string.Format("s-{0}", id) };
            new ElasticSearch(_searchConnectionString, _catalogIndex).Delete(catalogItem);
            new ElasticSearch(_searchConnectionString, _itemsIndex).Delete(item);

            // Delete pages related to the segment from search indexes
            DeletePagesForSegment(id);
        }

        private void DeleteSegmentsFromIndexForItem(string id)
        {
            List<int> segmentIds = new DataAccess(_dbConnectionstring).GetSegmentsForItem(Convert.ToInt32(id));

            // Delete from SeachCatalog table
            new DataAccess(_dbConnectionstring).DeleteSegmentsFromIndexForItem(Convert.ToInt32(id));

            // Delete from search indexes
            new ElasticSearch(_searchConnectionString, _catalogIndex).DeleteAllSegments(id);

            // Delete segment pages from search indexes
            foreach (int segmentId in segmentIds) DeletePagesForSegment(segmentId.ToString());
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
            if (authors.Count > 0) new ElasticSearch(_searchConnectionString, _authorsIndex).IndexMany(authors);
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
            new ElasticSearch(_searchConnectionString, _authorsIndex).Delete(author);
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
            if (keywords.Count > 0) new ElasticSearch(_searchConnectionString, _keywordsIndex).IndexMany(keywords);
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
            new ElasticSearch(_searchConnectionString, _keywordsIndex).Delete(keyword);
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
            if (names.Count > 0) new ElasticSearch(_searchConnectionString, _namesIndex).IndexMany(names);
        }

        /// <summary>
        /// Delete the specified name from the search indexes and SearchCatalog tables
        /// </summary>
        /// <param name="id"></param>
        private void DeleteName(string id)
        {
            // Delete from search index
            Name name = new Name { id = Convert.ToInt32(id) };
            new ElasticSearch(_searchConnectionString, _namesIndex).Delete(name);
        }

        /// <summary>
        /// Delete all pages associated with the specified item from the search indexes
        /// </summary>
        /// <param name="id"></param>
        private void DeletePagesForItem(string id)
        {
            // Get the Book.ItemID related to this id (Book.BookID)
            int itemId = new DataAccess(_dbConnectionstring).GetItemIDForBook(Convert.ToInt32(id));

            // Delete from search index
            new ElasticSearch(_searchConnectionString, _pagesIndex).DeleteAllPages(itemId.ToString());
        }

        /// <summary>
        /// Delete all pages associated with the specified segment from the search indexes
        /// </summary>
        /// <param name="id"></param>
        private void DeletePagesForSegment(string id)
        {
            // Get the Segment.ItemID related to this id (Segment.SegmentID)
            int itemId = new DataAccess(_dbConnectionstring).GetItemIDForSegment(Convert.ToInt32(id));

            // Delete from search index
            new ElasticSearch(_searchConnectionString, _pagesIndex).DeleteAllPages(itemId.ToString());
        }
    }
}
