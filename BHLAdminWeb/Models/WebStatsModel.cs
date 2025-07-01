using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class WebStatsModel
    {
        string _appPath;
        public List<WebStat> overallTraffic { get; set; } = new List<WebStat>();
        public List<WebStat> statusTraffic { get; set; } = new List<WebStat>();
        public List<WebStat> stemTraffic { get; set; } = new List<WebStat>();
        public List<WebStat> stemstatusTraffic { get; set; } = new List<WebStat>();

        public WebStatsModel(string appPath)
        {
            _appPath = appPath;
        }

        public void GetWebStats()
        {
            GetTrafficStatsFromLogFile(new List<string> { ConfigurationManager.AppSettings["DailyStatsServer1"], ConfigurationManager.AppSettings["DailyStatsServer2"] }, overallTraffic);
        }

        private void GetTrafficStatsFromLogFile(List<string> logFiles, List<WebStat> trafficStats)
        {
            // Combine the stats from the log files
            Dictionary<DateTime, int> hourlyStats = new Dictionary<DateTime, int>();
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
                        string[] hourlyStat = logLine.Split(',');
                        DateTime hourlyKey = DateTime.Parse(hourlyStat[0]);
                        int hourlyRequests = Int32.Parse(hourlyStat[1]);
                        if (hourlyStats.ContainsKey(hourlyKey))
                            hourlyStats[hourlyKey] += hourlyRequests;
                        else
                            hourlyStats.Add(hourlyKey, hourlyRequests);
                    }
                }
            }

            // Add the log file stats to the traffic stats
            foreach (var hourlyStat in hourlyStats)
            {
                trafficStats.Add(new WebStat
                {
                    Year = hourlyStat.Key.Year,
                    Month = hourlyStat.Key.Month,
                    Day = hourlyStat.Key.Day,
                    Requests = hourlyStat.Value,
                });
            }
        }

        public class WebStat
        {
            int _year;
            int _month;
            int _day;
            string _status;
            string _stem;
            int _requests;

            public int Year { get => _year; set => _year = value; }
            public int Month { get => _month; set => _month = value; }
            public int Day { get => _day; set => _day = value; }
            public string Status { get => _status; set => _status = value; }
            public string Stem { get => _stem; set => _stem = value; }
            public int Requests { get => _requests; set => _requests = value; }
        }
    }
}