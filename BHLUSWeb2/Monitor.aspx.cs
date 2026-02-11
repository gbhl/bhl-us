using MOBOT.BHL.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MOBOT.BHL.Web2
{
    public partial class Monitor : System.Web.UI.Page
    {
        private bool exceptionsOccurred = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;

            Response.Expires = -1;
            Response.Write("Starting tests on " + DateTime.Now.ToString("MM/dd/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss.fffffff"));
            Response.Flush();
            Response.Write("<ul>");

            //query the database
            Response.Write("<li>Querying database:  ");
            Response.Flush();

            try
            {
                BHLProvider provider = new BHLProvider();

                DateTime startTime = DateTime.Now;

                // If it takes too long for the following methods to complete, then 
                // it is likely that the database is responding unusually slowly.
                provider.AuthorSelectByTitleId(4);
                provider.BookSelectByTitleId(4);
                provider.TitleKeywordSelectKeywordByTitle(4);
                provider.NamePageSelectByPageID(1000000);
                provider.NameResolvedSelectByPageID(1000000);
                provider.PageSelectFirstPageForItem(1000);
                provider.PageSummarySelectByItemId(1000, true);
                provider.SearchBookFullText("annual missouri botanical", 100, "Rank");
                provider.SearchSegmentFullText("bird island", 100, "Title");
                provider.SegmentSelectForSegmentID(100);
                provider.TitleSelectByAuthor(93);

                DateTime endTime = DateTime.Now;
                double queryTime = endTime.Subtract(startTime).TotalSeconds;

                if (queryTime >= Convert.ToDouble(ConfigurationManager.AppSettings["MonitorThreshold"]))
                {
                    WriteError(string.Format("Slow database response - It took {0} seconds to process database queries", queryTime), string.Empty);
                    exceptionsOccurred = true;
                }
                else
                {

                    Response.Write("completed successfully at " + DateTime.Now.ToString("HH:mm:ss:fffffff") + "</li>");
                }
            }
            catch (Exception ex)
            {
                WriteExceptionInfo(ex);
                exceptionsOccurred = true;
                Response.Flush();
            }
            Response.Write("</ul>");

            if (!exceptionsOccurred)
            {
                Response.Write("All tests completed successfully at " + DateTime.Now.ToString("HH:mm:ss:fffffff"));
            }
            else
            {
                Response.Write("<font color=\"red\"><b>Exceptions occurred!</b></font>");
            }

            Response.Write("<p>&nbsp</p>");
            Response.Write("<p>CACHED OBJECTS");
            Response.Write("<div style='border:1px;border-style:solid;border-color:#d3d3d3;width:50%;height:200px;overflow:auto'>");
            Response.Write("<table style='width:100%'>");

            // Retrieve cache keys and values into a dictionary
            Dictionary<string, object> cacheItems = new Dictionary<string, object>();
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Filter out internal system cache entries if needed
                if (!enumerator.Key.ToString().StartsWith("System."))
                {
                    cacheItems.Add(enumerator.Key.ToString(), enumerator.Value);
                }
            }

            // Sort the dictionary by key (e.g., alphabetically)
            var sortedCacheItems = cacheItems.OrderBy(item => item.Key);

            // Display the sorted list of cache keys and their sizes in KB
            double totalCacheSize = 0;
            foreach (var cacheItem in sortedCacheItems)
            {
                string key = cacheItem.Key.ToString();
                double numBytes = cacheItem.Value.ToString().Length;
                totalCacheSize += numBytes;
                Response.Write(string.Format("<tr><td style=\"white-space:nowrap\">{0}</td><td style=\"white-space:nowrap\">{1} KB</td></tr>", key, Math.Round(numBytes / 1024, 3).ToString()));
            }
            Response.Write("</table></div>");
            Response.Write(string.Format("Total Cache Size: {0} KB", Math.Round(totalCacheSize / 1024, 3).ToString()));
            Response.Write("</p>");
        }

        private void WriteExceptionInfo(Exception ex)
        {
            WriteError(ex.Message, ex.StackTrace);
        }

        private void WriteError(String message, String stackTrace)
        {
            Response.Write("<font color=\"red\">The following exception occurred:<br>");
            Response.Write("<b>Message:</b> " + message + "<br>");
            Response.Write("<b>Stack Trace:</b>" + stackTrace.Replace("\n", "<br>") + "</font></li>");
        }
    }
}