using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.AdminWeb
{
    public partial class PdfWeeklyStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get page params.  First check the querystring, then session state
                String year = Request.QueryString["y"] as String;
                String week = Request.QueryString["w"] as String;
                String statusId = Request.QueryString["s"] as String;
                String statusName = Request.QueryString["sn"] as String;

                /*
                if (String.IsNullOrEmpty(year)) year = Session["PDFWeeklyYear"] as String;
                if (String.IsNullOrEmpty(week)) week = Session["PDFWeeklyWeek"] as String;
                if (String.IsNullOrEmpty(statusId)) statusId = Session["PDFWeeklyStatus"] as String;
                if (String.IsNullOrEmpty(statusName)) statusName = Session["PDFWeeklyStatusName"] as String;

                // Save parameters in session in case we return to this page later
                Session["PDFWeeklyYear"] = year;
                Session["PDFWeeklyWeek"] = week;
                Session["PDFWeeklyStatus"] = statusId;
                Session["PDFWeeklyStatusName"] = statusName;
                */

                // Display params on page
                if (!String.IsNullOrEmpty(year)) litYear.Text = year;
                if (!String.IsNullOrEmpty(week)) litWeek.Text = week;
                if (!String.IsNullOrEmpty(statusName)) litStatusName.Text = statusName;

                // Populate the page with PDF data
                int yearParam = 0;
                int weekParam = 0;
                int statusIdParam = 0;

                Int32.TryParse(year, out yearParam);
                Int32.TryParse(week, out weekParam);
                Int32.TryParse(statusId, out statusIdParam);

                BHLProvider provider = new BHLProvider();
                gvWeeklyStats.DataSource = provider.PDFSelectForWeekAndStatus(yearParam, weekParam, statusIdParam);
                gvWeeklyStats.DataBind();
            }
        }
    }
}
