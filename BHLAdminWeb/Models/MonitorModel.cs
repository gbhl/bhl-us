using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            GetMQStats();
        }

        private void GetSearchStats()
        {
            searchMonitor.ErrorMessage = string.Empty;

            try
            {
                JObject jsonResponse = CallSearchApi(string.Format("{0}{1}",
                        ConfigurationManager.AppSettings["SearchServerAddress"],
                        ConfigurationManager.AppSettings["SearchServerStatsUrl"]));

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
                    double usedMemoryBytes = Convert.ToDouble(nodes["jvm"]["mem"]["heap_used_in_bytes"]);
                    double totalMemoryBytes = Convert.ToDouble(nodes["jvm"]["mem"]["heap_max_in_bytes"]);
                    searchMonitor.MemoryUsagePercent = Math.Ceiling((usedMemoryBytes / totalMemoryBytes) * 100);
                    searchMonitor.TotalDisk = nodes["fs"]["total"].ToString();
                    searchMonitor.FreeDisk = nodes["fs"]["free"].ToString();
                    double freeDiskBytes = Convert.ToDouble(nodes["fs"]["free_in_bytes"]);
                    double totalDiskBytes = Convert.ToDouble(nodes["fs"]["total_in_bytes"]);
                    searchMonitor.DiskUsagePercent = Math.Ceiling(((totalDiskBytes - freeDiskBytes) / totalDiskBytes) * 100);
                }

                foreach (string indexName in ConfigurationManager.AppSettings["SearchServerIndexes"].Split('|'))
                {
                    searchMonitor.Indexes.Add(GetSearchIndexStats(indexName));
                }
            }
            catch (Exception ex)
            {
                searchMonitor.ErrorMessage = "Error retrieving Search Server statistics.";
            }
        }

        private SearchIndex GetSearchIndexStats(string indexName)
        {
            JObject jsonResponse = CallSearchApi(string.Format("{0}/{1}{2}", 
                ConfigurationManager.AppSettings["SearchServerAddress"],
                indexName,
                ConfigurationManager.AppSettings["SearchIndexStatsUrl"]));

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

        private void GetMQStats()
        {
            mqMonitor.ErrorMessage = string.Empty;

            try
            {
                SiteService.SiteServiceSoapClient client = new SiteService.SiteServiceSoapClient();
                string apiResponse = client.GetMQInfo();
                JArray jsonResponse = JArray.Parse(apiResponse);

                List<string> queueNames = ConfigurationManager.AppSettings["MessageQueues"]
                    .ToLower()
                    .Split('|')
                    .ToList<string>();

                foreach (JToken queue in jsonResponse)
                {
                    string name = queue["name"].ToString();

                    if (queueNames.Contains(name.ToLower()))
                    {
                        MessageQueue mq = new MessageQueue();
                        mq.Name = name;
                        mq.Messages = Convert.ToInt32(queue["messages"]);
                        mq.Memory = Convert.ToInt32(queue["memory"]);
                        mq.State = queue["state"].ToString();
                        mqMonitor.Queues.Add(mq);
                    }
                }
            }
            catch (Exception ex)
            {
                mqMonitor.ErrorMessage = "Error retrieving Message Queue statistics.";
            }
        }

        public class SearchMonitor
        {
            string _errorMessage = string.Empty;

            string _name = string.Empty;
            int _documents = 0;
            string _storeSize = string.Empty;
            string _uptime = string.Empty;

            int _cpuPercent = 0;
            double _memoryUsagePercent = 0;
            string _totalMemory = string.Empty;
            string _usedMemory = string.Empty;
            double _diskUsagePercent = 0;
            string _totalDisk = string.Empty;
            string _freeDisk = string.Empty;

            List<SearchIndex> _indexes = new List<SearchIndex>();

            public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
            public string Name { get => _name; set => _name = value; }
            public int Documents { get => _documents; set => _documents = value; }
            public string StoreSize { get => _storeSize; set => _storeSize = value; }
            public string Uptime { get => _uptime; set => _uptime = value; }
            public int CpuPercent { get => _cpuPercent; set => _cpuPercent = value; }
            public double MemoryUsagePercent { get => _memoryUsagePercent; set => _memoryUsagePercent = value; }
            public string TotalMemory { get => _totalMemory; set => _totalMemory = value; }
            public string UsedMemory { get => _usedMemory; set => _usedMemory = value; }
            public double DiskUsagePercent { get => _diskUsagePercent; set => _diskUsagePercent = value; }
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
            string _errorMessage = string.Empty;

            List<MessageQueue> _queues = new List<MessageQueue>();

            public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
            public List<MessageQueue> Queues{ get => _queues; set => _queues = value; }
        }

        public class MessageQueue
        {
            string _name = string.Empty;
            int _messages = 0;
            int _memory = 0;
            string _state = string.Empty;

            public string Name { get => _name; set => _name = value; }
            public int Messages { get => _messages; set => _messages = value; }
            public int Memory { get => _memory; set => _memory = value; }
            public string State { get => _state; set => _state = value; }
        }
    }
}