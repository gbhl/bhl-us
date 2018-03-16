using BHL.QueueUtility;
using System;

namespace BHL.SearchIndexer
{
    public class IndexMessageProcessor : IMessageProcessor
    {
        public bool ProcessMessage(string message)
        {

            // TODO: Parse the message, get the metadata from the database, and submit it to the search server

            /*
             * 1) Parse Action, Type, and ID from the message
             * 2) If Action = delete, remove Type+ID from search index
             * 3) If Action = put
             *      a) Get metadata for Type+ID from database
             *      b) Submit metadata to search index
             */

            throw new NotImplementedException();
        }
    }
}
