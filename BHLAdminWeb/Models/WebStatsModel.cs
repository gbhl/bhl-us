using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class WebStatsModel
    {
        string _appPath;
        public SortedList<string, string> statusList = new SortedList<string, string>();
        public SortedList<string, string> stemList = new SortedList<string, string>();
        public List<WebStat> overallTraffic { get; set; } = new List<WebStat>();
        public SortedDictionary<DateTime, SortedDictionary<string, int>> statusTraffic { get; set; } = new SortedDictionary<DateTime, SortedDictionary<string, int>>();
        public SortedDictionary<DateTime, SortedDictionary<string, int>> stemTraffic { get; set; } = new SortedDictionary<DateTime, SortedDictionary<string, int>>();
        public List<WebStat> stemstatusTraffic { get; set; } = new List<WebStat>();

        public WebStatsModel(string appPath)
        {
            _appPath = appPath;
        }

        public void GetWebStats()
        {
            GetTrafficStatsFromLogFile(new List<string> { ConfigurationManager.AppSettings["DailyStatsServer1"], ConfigurationManager.AppSettings["DailyStatsServer2"] }, overallTraffic);
            GetStatusTrafficStatsFromLogFile(new List<string> { ConfigurationManager.AppSettings["DailyByStatusStatsServer1"], ConfigurationManager.AppSettings["DailyByStatusStatsServer2"] }, statusTraffic);
            GetStemTrafficStatsFromLogFile(new List<string> { ConfigurationManager.AppSettings["DailyByStemStatsServer1"], ConfigurationManager.AppSettings["DailyByStemStatsServer2"] }, stemTraffic);
        }

        private void GetTrafficStatsFromLogFile(List<string> logFiles, List<WebStat> trafficStats)
        {
            // Combine the stats from the log files
            Dictionary<DateTime, int> dailyStats = new Dictionary<DateTime, int>();
            foreach (string logFile in logFiles)
            {
                string[] logLines = File.ReadAllLines(_appPath + logFile);
                bool firstLine = true;
                foreach (string logLine in logLines)
                {
                    if (firstLine)
                    {
                        firstLine = false;  // Skip the first line
                    }
                    else
                    {
                        string[] dailyStat = logLine.Split(',');
                        DateTime dailyKey = DateTime.Parse(dailyStat[0]);
                        int dailyRequests = Int32.Parse(dailyStat[1]);
                        if (dailyStats.ContainsKey(dailyKey))
                            dailyStats[dailyKey] += dailyRequests;
                        else
                            dailyStats.Add(dailyKey, dailyRequests);
                    }
                }
            }

            // Add the log file stats to the traffic stats
            foreach (var dailyStat in dailyStats)
            {
                trafficStats.Add(new WebStat
                {
                    Year = dailyStat.Key.Year,
                    Month = dailyStat.Key.Month,
                    Day = dailyStat.Key.Day,
                    Requests = dailyStat.Value,
                });
            }
        }

        private void GetStatusTrafficStatsFromLogFile(List<string> logFiles, SortedDictionary<DateTime, SortedDictionary<string, int>> trafficStats)
        {
            // Combine the stats from the log files
            List<DateTime> dates = new List<DateTime>();
            Dictionary<string, int> statusStats = new Dictionary<string, int>();
            foreach (string logFile in logFiles)
            {
                // First pass through log files to collect all unique date and status values
                string[] logLines = File.ReadAllLines(_appPath + logFile);

                bool firstLine = true;
                foreach (string logLine in logLines)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        string[] dailyStat = logLine.Split(',');
                        DateTime dailyKey = DateTime.Parse(dailyStat[0]);
                        string statusKey = dailyStat[1];
                        if (!dates.Contains(dailyKey)) dates.Add(dailyKey);
                        if (!statusStats.ContainsKey(statusKey)) statusStats.Add(statusKey, 0);
                    }
                }
            }

            // Get the list of unique statuses
            foreach(var s in statusStats) statusList.Add(s.Key, s.Key);

            // Add the dictionary of all possible status values to each date in the dictionary of all dates
            foreach (DateTime date in dates)
            {
                if (!trafficStats.ContainsKey(date)) trafficStats.Add(date, new SortedDictionary<string, int>(statusStats));
            }

            foreach (string logFile in logFiles)
            {
                // Second pass through the log files to assign values to each element in the dictionary of traffic
                string[] logLines = File.ReadAllLines(_appPath + logFile);

                bool firstLine = true;
                foreach (string logLine in logLines)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        string[] dailyStat = logLine.Split(',');
                        DateTime dailyKey = DateTime.Parse(dailyStat[0]);
                        string statusKey = dailyStat[1];
                        int statusRequests = Int32.Parse(dailyStat[2]);
                        trafficStats[dailyKey][statusKey] += statusRequests;
                    }
                }
            }
        }

        private void GetStemTrafficStatsFromLogFile(List<string> logFiles, SortedDictionary<DateTime, SortedDictionary<string, int>> trafficStats)
        {
            // Combine the stats from the log files
            List<DateTime> dates = new List<DateTime>();
            Dictionary<string, int> stemStats = new Dictionary<string, int>();
            foreach (string logFile in logFiles)
            {
                // First pass through log files to collect all unique date and status values
                string[] logLines = File.ReadAllLines(_appPath + logFile);

                bool firstLine = true;
                foreach (string logLine in logLines)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        string[] dailyStat = logLine.Split(',');
                        DateTime dailyKey = DateTime.Parse(dailyStat[0]);
                        string stemKey = dailyStat[1].Trim();
                        if (stemKey != "/cgi-bin" &&
                            stemKey != "/robots.txt" &&
                            stemKey != "/scripts" &&
                            stemKey != "/wp-admin" &&
                            stemKey != "/wp-content" &&
                            stemKey != "/wp-includes")
                        {
                            if (!dates.Contains(dailyKey)) dates.Add(dailyKey);
                            if (!stemStats.ContainsKey(stemKey)) stemStats.Add(stemKey, 0);
                        }
                    }
                }
            }

            // Get the list of unique stems
            foreach (var s in stemStats) stemList.Add(s.Key, s.Key);

            // Add the dictionary of all possible stem values to each date in the dictionary of all dates
            foreach (DateTime date in dates)
            {
                if (!trafficStats.ContainsKey(date)) trafficStats.Add(date, new SortedDictionary<string, int>(stemStats));
            }

            foreach (string logFile in logFiles)
            {
                // Second pass through the log files to assign values to each element in the dictionary of traffic
                string[] logLines = File.ReadAllLines(_appPath + logFile);

                bool firstLine = true;
                foreach (string logLine in logLines)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        string[] dailyStat = logLine.Split(',');
                        DateTime dailyKey = DateTime.Parse(dailyStat[0]);
                        string stemKey = dailyStat[1].Trim();
                        int stemRequests = Int32.Parse(dailyStat[2]);
                        if (stemKey != "/cgi-bin" &&
                            stemKey != "/robots.txt" &&
                            stemKey != "/scripts" &&
                            stemKey != "/wp-admin" &&
                            stemKey != "/wp-content" &&
                            stemKey != "/wp-includes")
                        {
                            trafficStats[dailyKey][stemKey] += stemRequests;
                        }
                    }
                }
            }
        }

        public class WebStat
        {
            int _year;
            int _month;
            int _day;
            int _requests;

            public int Year { get => _year; set => _year = value; }
            public int Month { get => _month; set => _month = value; }
            public int Day { get => _day; set => _day = value; }
            public int Requests { get => _requests; set => _requests = value; }
        }
    }
}