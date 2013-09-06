using System;
using CustomDataAccess;
using MOBOT.BHL.Server;
using Data = MOBOT.BHL.DataObjects;

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
            CustomGenericList<Data.ItemSource> list = null;
            try
            {
                BHLProvider provider = new BHLProvider();
                list = provider.ItemSourceSelectAll();
                Response.Write("completed successfully at " + DateTime.Now.ToString("HH:mm:ss:fffffff") + "</li>");
                Response.Flush();
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