using Nest;
using System;
using System.Collections.Generic;

namespace BHL.SearchIndexer
{
    public class ElasticSearch
    {
        public enum PageSource
        {
            Book,
            Segment
        }

        public static class ESIndex
        {
            public const string CATALOG = "catalog";
            public const string ITEMS = "items";
            public const string PAGES = "pages";
            public const string AUTHORS = "authors";
            public const string KEYWORDS = "keywords";
            public const string NAMES = "names";
        }

        private ElasticClient _es = null;
        private string _indexName = string.Empty;
        private bool _debug = false;

        public ElasticSearch(string connectionString, string indexName = "items", bool debug = false)
        {
            _indexName = indexName;
            _debug = debug;

            // Establish a connection to an ElasticSearch server
            ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(connectionString));
            connectionSettings.DefaultIndex(_indexName);

            // In debug mode, add req/resp strings to response.debuginformation
            if (_debug) connectionSettings.DisableDirectStreaming();

            // Red Hat Enterprise Linux does not support the MaxConnectionsPerServer property
            // https://github.com/elastic/elasticsearch-net/issues/2758
            connectionSettings.ConnectionLimit(-1);

            _es = new ElasticClient(connectionSettings);

            CheckServerStatus();
        }

        public void Index(Item document)
        {
            if (document != null) _es.Index(document);
        }

        public void Index(CatalogItem document)
        {
            if (document != null) _es.Index(document);
        }

        public void IndexMany(List<CatalogItem> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void IndexMany(List<Page> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void IndexMany(List<Item> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void IndexMany(List<Author> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void IndexMany(List<Keyword> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void IndexMany(List<Name> documents)
        {
            if (documents.Count > 0)
            {
                var br = _es.IndexMany(documents, _indexName);
                if (!br.IsValid && br.Errors) throw new Exception(GetIndexErrorString(br));
            }
        }

        public void Update(Item document)
        {
            if (document != null)
            {
                IUpdateResponse<Item> response = _es.Update<Item, object>(document, u => u
                    .Doc(document)
                    .DocAsUpsert()  // Insert document if it doesn't already exist
                    .RetryOnConflict(3)
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Update(Page document)
        {
            if (document != null)
            {
                IUpdateResponse<Page> response = _es.Update<Page, object>(document.id, u => u
                     .Doc(document)
                     .DocAsUpsert()  // Insert document if it doesn't already exist
                     .RetryOnConflict(3)
                     .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }
        
        public void Delete(CatalogItem document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<CatalogItem>(document, d => d
                                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Delete(Item document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<Item>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Delete(Author document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<Author>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Delete(Keyword document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<Keyword>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Delete(Name document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<Name>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        public void Delete(Page document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<Page>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                    );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }

        /*
        public void Delete(object document)
        {
            if (document != null)
            {
                IDeleteResponse response = _es.Delete<object>(document, d => d
                    .Refresh(Elasticsearch.Net.Refresh.WaitFor)
                );
                if (!response.IsValid) throw new Exception(GetIndexErrorString(response));
            }
        }
        */

        public void DeleteAll(string id)
        {
            IDeleteByQueryRequest dq = new DeleteByQueryRequest(_indexName);
            dq.Query = new QueryContainer(new MatchQuery { Field = "itemId", Query = id });
            _es.DeleteByQuery(dq);
        }

        private string GetIndexErrorString(IResponse response)
        {
            return string.Format(
                "Original Exception: {0}\r\nServer Error: {1}\r\nDebug Information: {2}",
                response.OriginalException == null ? string.Empty : response.OriginalException.Message,
                response.ServerError == null ? string.Empty : response.ServerError.Error.ToString(),
                response.DebugInformation);
        }

        public void OptimizeIndex()
        {
            // Clean up the index
            ForceMergeRequest fmRequest = new ForceMergeRequest(_indexName);
            fmRequest.OnlyExpungeDeletes = true;
            _es.ForceMerge(fmRequest);
        }

        /// <summary>
        /// Check the current status of the ElasticSearch server 
        /// </summary>
        public void CheckServerStatus()
        {
            ClusterHealthRequest healthRequest = new ClusterHealthRequest();
            healthRequest.Timeout = new Time("30s");
            healthRequest.WaitForStatus = Elasticsearch.Net.WaitForStatus.Yellow;
            var healthResponse = _es.ClusterHealth(healthRequest);
            if (!healthResponse.IsValid) ProcessError(healthResponse);
        }

        /// <summary>
        /// Parse the error information from the specified response and throw an exception.
        /// </summary>
        /// <param name="response"></param>
        private void ProcessError(IResponse response)
        {
            string errorMessage = "Error reported by ElasticSearch server.\n\r";
            if (response.OriginalException != null)
            {
                errorMessage += response.OriginalException.Message + "\n\r";
            }
            else if (response.ServerError != null)
            {
                errorMessage += response.ServerError.Error.Reason + "\n\r";
            }
            errorMessage += response.DebugInformation + "\n\r";

            throw new Exception(errorMessage, response.OriginalException);
        }
    }
}
