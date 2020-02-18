using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentSearch : System.Web.UI.Page
    {
        private bool _refreshSearch = false;
        private String _redirectUrl = "/SegmentEdit.aspx?id=";

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlLink cssLnk = new HtmlLink();
            cssLnk.Attributes.Add("rel", "stylesheet");
            cssLnk.Attributes.Add("type", "text/css");
            cssLnk.Href = ConfigurationManager.AppSettings["jQueryUICSSPath"];
            Page.Header.Controls.Add(cssLnk);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            errorControl.Visible = false;
            Page.SetFocus(txtTitle);
            Page.Title = "BHL Admin - Segment Search";
        }

        private void search()
        {
            if (txtSegmentID.Text.Trim().Length == 0 && txtTitle.Text.Trim().Length == 0) return;

            BHLProvider bp = new BHLProvider();
            List<Segment> results = new List<Segment>();
            if (txtSegmentID.Text.Trim().Length > 0)
            {
                Segment result = bp.SegmentSelectForSegmentID(Convert.ToInt32(txtSegmentID.Text));
                if (result != null) results.Add(result);
            }
            else
            {
                results = bp.SearchSegmentComplete(txtTitle.Text.Trim());
            }
            if (results.Count == 1)
            {
                Response.Redirect(_redirectUrl + results[0].SegmentID.ToString());
            }
            else
            {
                gvwResults.DataSource = results;
                gvwResults.DataBind();
            }
        }

        #region Event Handlers

        protected void searchButton_Click(object sender, EventArgs e)
        {
            _refreshSearch = true;
            search();
        }

        #endregion Event Handlers

    }
}