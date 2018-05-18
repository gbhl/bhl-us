using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class MonitorModel
    {
        public SearchMonitor searchMonitor = new SearchMonitor();
        public MQMonitor mqMonitor = new MQMonitor();

        public MonitorModel()
        {
            GetSearchStats();
        }

        private void GetSearchStats()
        {
            JObject jsonResponse = CallSearchApi("http://localhost:9200/_cluster/stats?human");

            // Parse the data from the response
            searchMonitor.Name = (jsonResponse["cluster_name"] == null ? string.Empty : jsonResponse["cluster_name"].ToString());
            JToken indices = jsonResponse["indices"];
            if (indices != null)
            {
                searchMonitor.Documents = Convert.ToInt32(indices["docs"]["count"]);
                searchMonitor.StoreSize = indices["store"]["size"].ToString();
            }

            JToken nodes = jsonResponse["nodes"];
            if (nodes != null)
            {
                searchMonitor.Uptime = nodes["jvm"]["max_uptime"].ToString();
                searchMonitor.CpuPercent = Convert.ToInt32(nodes["process"]["cpu"]["percent"]);
                searchMonitor.TotalMemory = nodes["jvm"]["mem"]["heap_max"].ToString();
                searchMonitor.UsedMemory = nodes["jvm"]["mem"]["heap_used"].ToString();
                searchMonitor.TotalDisk = nodes["fs"]["total"].ToString();
                searchMonitor.FreeDisk = nodes["fs"]["free"].ToString();
            }

            searchMonitor.Indexes.Add(GetSearchIndexStats("items"));
            searchMonitor.Indexes.Add(GetSearchIndexStats("authors"));
            searchMonitor.Indexes.Add(GetSearchIndexStats("keywords"));
            searchMonitor.Indexes.Add(GetSearchIndexStats("names"));
            searchMonitor.Indexes.Add(GetSearchIndexStats("pages"));
        }

        private SearchIndex GetSearchIndexStats(string indexName)
        {
            JObject jsonResponse = CallSearchApi(string.Format("http://localhost:9200/{0}/_stats?human", indexName));
            JToken root = jsonResponse["_all"]["primaries"];

            SearchIndex searchIndex = new SearchIndex();
            if (root != null)
            {
                searchIndex.Name = indexName;
                searchIndex.Documents = Convert.ToInt32(root["docs"]["count"]);
                searchIndex.StoreSize = root["store"]["size"].ToString();
            }
            return searchIndex;
        }

        private JObject CallSearchApi(string uri)
        {
            WebClient webClient = new WebClient();
            string apiResponse = webClient.DownloadString(uri);
            return JObject.Parse(apiResponse);
        }

        public class SearchMonitor
        {
            string _name = string.Empty;
            int _documents = 0;
            string _storeSize = string.Empty;
            string _uptime = string.Empty;

            int _cpuPercent = 0;
            string _totalMemory = string.Empty;
            string _usedMemory = string.Empty;
            string _totalDisk = string.Empty;
            string _freeDisk = string.Empty;

            List<SearchIndex> _indexes = new List<SearchIndex>();

            public string Name { get => _name; set => _name = value; }
            public int Documents { get => _documents; set => _documents = value; }
            public string StoreSize { get => _storeSize; set => _storeSize = value; }
            public string Uptime { get => _uptime; set => _uptime = value; }
            public int CpuPercent { get => _cpuPercent; set => _cpuPercent = value; }
            public string TotalMemory { get => _totalMemory; set => _totalMemory = value; }
            public string UsedMemory { get => _usedMemory; set => _usedMemory = value; }
            public string TotalDisk { get => _totalDisk; set => _totalDisk = value; }
            public string FreeDisk { get => _freeDisk; set => _freeDisk = value; }
            public List<SearchIndex> Indexes { get => _indexes; set => _indexes = value; }
        }

        public class SearchIndex
        {
            string _name = string.Empty;
            int _documents = 0;
            string _storeSize = string.Empty;

            public string Name { get => _name; set => _name = value; }
            public int Documents { get => _documents; set => _documents = value; }
            public string StoreSize { get => _storeSize; set => _storeSize = value; }
        }

        public class MQMonitor
        {

        }
    }
}