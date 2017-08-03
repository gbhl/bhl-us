using System;
using System.Reflection;

namespace BHL.Search
{
    public class SearchFactory
    {
        public ISearch GetSearch(string types)
        {
            ISearch searchInstance = null;
            bool online = false;

            string[] searchProviders = types.Split('|');
            // Try each search provider in the list
            foreach(string searchProvider in searchProviders)
            {
                try
                {
                    // Create an instance of the search provider
                    Assembly searchAssembly = System.Reflection.Assembly.Load(searchProvider);
                    Type searchType = searchAssembly.GetType(searchProvider + ".Search");
                    searchInstance = (ISearch)Activator.CreateInstance(searchType);

                    // If the search provider is online, then exit the loop
                    if ((bool)searchType.InvokeMember("IsOnline",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                        null, searchInstance, null))
                    {
                        online = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Log the error 
                    // and try the next provider
                    //ExceptionUtility.LogException(ex, "SearchFactory.GetSearch");
                }
            }

            if (online) return searchInstance;

            throw new SearchException("No search provider available");
        }
    }
}
