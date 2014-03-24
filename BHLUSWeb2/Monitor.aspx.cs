using System;
using CustomDataAccess;
using MOBOT.BHL.Server;
using Data = MOBOT.BHL.DataObjects;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class Monitor : System.Web.UI.Page
    {
        private bool exceptionsOccurred = false;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                provider.ItemSelectByTitleId(4);
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
            Response.Flush();
        }

        private void WriteExceptionInfo(Exception ex)
        {
            this.WriteError(ex.Message, ex.StackTrace);
        }

        private void WriteExceptionInfo(String message)
        {
            this.WriteError(message, "");
        }

        private void WriteError(String message, String stackTrace)
        {
            Response.Write("<font color=\"red\">The following exception occurred:<br>");
            Response.Write("<b>Message:</b> " + message + "<br>");
            Response.Write("<b>Stack Trace:</b>" + stackTrace.Replace("\n", "<br>") + "</font></li>");
        }
    }
}