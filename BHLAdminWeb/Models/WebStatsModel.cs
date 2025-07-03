using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class WebStatsModel
    {
        public string StartDate { get; set; } = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
        public string EndDate { get; set; } = DateTime.Now.Date.ToString("yyyy-MM-dd");
        public string Stem { get; set; } = null;
        public string Status { get; set; } = null;

        public string AppPath { get; set; }

        public List<string> statusList = new List<string>();
        public List<string> statusCompleteList = new List<string>();
        public List<string> stemList = new List<string>();
        public List<string> stemCompleteList = new List<string>();
        public List<WebStat> overallTraffic { get; set; } = new List<WebStat>();
        public SortedDictionary<DateTime, SortedDictionary<string, int>> statusTraffic { get; set; } = new SortedDictionary<DateTime, SortedDictionary<string, int>>();
        public SortedDictionary<DateTime, SortedDictionary<string, int>> stemTraffic { get; set; } = new SortedDictionary<DateTime, SortedDictionary<string, int>>();
        public List<WebStat> stemstatusTraffic { get; set; } = new List<WebStat>();

        public WebStatsModel() { }
        public WebStatsModel(string appPath)
        {
            AppPath = appPath;
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
            Dictionary<DateTime, int> dailyStats = GetCombinedTrafficData(logFiles);

            // Add the log file stats to the traffic stats
            foreach (var dailyStat in dailyStats)
            {
                // Only include stats within the specified date range
                DateTime sDate = DateTime.Parse(StartDate ?? "1980-01-01");
                DateTime eDate = DateTime.Parse(EndDate ?? "2100-01-01");

                if (dailyStat.Key >= sDate && dailyStat.Key <= eDate)
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
        }

        public Dictionary<DateTime, int> GetCombinedTrafficData(List<string> logFiles)
        {
            // Combine the stats from the log files
            Dictionary<DateTime, int> dailyStats = new Dictionary<DateTime, int>();
            foreach (string logFile in logFiles)
            {
                string[] logLines = File.ReadAllLines(AppPath + logFile);
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

            return dailyStats;
        }

        private void GetStatusTrafficStatsFromLogFile(List<string> logFiles, SortedDictionary<DateTime, SortedDictionary<string, int>> trafficStats)
        {
            DateTime sDate = DateTime.Parse(StartDate ?? "1980-01-01");
            DateTime eDate = DateTime.Parse(EndDate ?? "2100-01-01");

            // Combine the stats from the log files
            List<DateTime> dates = new List<DateTime>();
            SortedDictionary<string, int> statusStats = new SortedDictionary<string, int>();
            List<string> allStatuses = new List<string>();
            foreach (string logFile in logFiles)
            {
                // First pass through log files to collect all unique date and status values
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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
                        string statusKey = dailyStat[1].Trim();

                        // Only include stats within the specified date range and the specified status
                        if (dailyKey >= sDate && dailyKey <= eDate && (statusKey == Status || Status == null))
                        {
                            if (!dates.Contains(dailyKey)) dates.Add(dailyKey);
                            if (!statusStats.ContainsKey(statusKey)) statusStats.Add(statusKey, 0);
                        }
                        // Keep a list of all possible statuses, regardless of searched date range and status
                        if (!allStatuses.Contains(statusKey)) allStatuses.Add(statusKey);   
                    }
                }
            }

            // Get the sorted lists of unique statuses
            statusCompleteList = allStatuses.OrderBy(s => s).ToList();  // all possible statuses
            foreach(var s in statusStats) statusList.Add(s.Key);        // selected status(es)

            // Add the dictionary of all possible status values to each date in the dictionary of all dates
            foreach (DateTime date in dates)
            {
                if (!trafficStats.ContainsKey(date)) trafficStats.Add(date, new SortedDictionary<string, int>(statusStats));
            }

            foreach (string logFile in logFiles)
            {
                // Second pass through the log files to assign values to each element in the dictionary of traffic
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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
                        string statusKey = dailyStat[1].Trim();
                        int statusRequests = Int32.Parse(dailyStat[2]);

                        if (trafficStats.ContainsKey(dailyKey) && trafficStats[dailyKey].ContainsKey(statusKey)) 
                            trafficStats[dailyKey][statusKey] += statusRequests;
                    }
                }
            }
        }

        public Dictionary<string, int> GetCombinedTrafficByStatusData(List<string> logFiles)
        {
            // Combine the stats from the log files
            Dictionary<string, int> statusStats = new Dictionary<string, int>();
            foreach (string logFile in logFiles)
            {
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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
                        string dailyKey = DateTime.Parse(dailyStat[0]).ToString("yyyy-MM-dd") + "," + dailyStat[1];
                        int requests = Int32.Parse(dailyStat[2]);

                        if (!statusStats.ContainsKey(dailyKey))
                            statusStats.Add(dailyKey, requests);
                        else
                            statusStats[dailyKey] += requests;
                    }
                }
            }

            return statusStats;
        }

        private void GetStemTrafficStatsFromLogFile(List<string> logFiles, SortedDictionary<DateTime, SortedDictionary<string, int>> trafficStats)
        {
            DateTime sDate = DateTime.Parse(StartDate ?? "1980-01-01");
            DateTime eDate = DateTime.Parse(EndDate ?? "2100-01-01");

            // Combine the stats from the log files
            List<DateTime> dates = new List<DateTime>();
            SortedDictionary<string, int> stemStats = new SortedDictionary<string, int>();
            List<string> allStems = new List<string>();
            foreach (string logFile in logFiles)
            {
                // First pass through log files to collect all unique date and status values
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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

                        // Exclude some URL stems
                        if (stemKey != "/cgi-bin" &&
                            stemKey != "/robots.txt" &&
                            stemKey != "/scripts" &&
                            stemKey != "/wp-admin" &&
                            stemKey != "/wp-content" &&
                            stemKey != "/wp-includes")
                        {
                            // Only include stats within the specified date range and the specified Stem
                            if (dailyKey >= sDate && dailyKey <= eDate && (stemKey == Stem || Stem == null))
                            {
                                if (!dates.Contains(dailyKey)) dates.Add(dailyKey);
                                if (!stemStats.ContainsKey(stemKey)) stemStats.Add(stemKey, 0);
                            }
                            // Keep a list of all possible stems, regardless of searched date range and stem
                            if (!allStems.Contains(stemKey)) allStems.Add(stemKey);
                        }
                    }
                }
            }

            // Get the sorted lists of unique stems
            stemCompleteList = allStems.OrderBy(s => s).ToList();   // all possible stems
            foreach (var s in stemStats) stemList.Add(s.Key);       // selected stem(s)

            // Add the dictionary of all possible stem values to each date in the dictionary of all dates
            foreach (DateTime date in dates)
            {
                if (!trafficStats.ContainsKey(date)) trafficStats.Add(date, new SortedDictionary<string, int>(stemStats));
            }

            foreach (string logFile in logFiles)
            {
                // Second pass through the log files to assign values to each element in the dictionary of traffic
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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
                            if (trafficStats.ContainsKey(dailyKey) && trafficStats[dailyKey].ContainsKey(stemKey))
                                trafficStats[dailyKey][stemKey] += stemRequests;
                        }
                    }
                }
            }
        }

        public Dictionary<string, int> GetCombinedTrafficByStemData(List<string> logFiles)
        {
            // Combine the stats from the log files
            Dictionary<string, int> stemStats = new Dictionary<string, int>();
            foreach (string logFile in logFiles)
            {
                string[] logLines = File.ReadAllLines(AppPath + logFile);

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
                        string dailyKey = DateTime.Parse(dailyStat[0]).ToString("yyyy-MM-dd") + "," + dailyStat[1];
                        int requests = Int32.Parse(dailyStat[2]);

                        // Only include stats within the specified date range
                        // Exclude some URL stems
                        string stemKey = dailyStat[1].Trim();
                        if (stemKey != "/cgi-bin" &&
                            stemKey != "/robots.txt" &&
                            stemKey != "/scripts" &&
                            stemKey != "/wp-admin" &&
                            stemKey != "/wp-content" &&
                            stemKey != "/wp-includes")
                        {
                            if (!stemStats.ContainsKey(dailyKey))
                                stemStats.Add(dailyKey, requests);
                            else
                                stemStats[dailyKey] += requests;
                        }
                    }
                }
            }

            return stemStats;
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