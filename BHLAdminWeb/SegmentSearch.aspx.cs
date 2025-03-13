using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentSearch : System.Web.UI.Page
    {
        private String _redirectUrl = "/SegmentEdit.aspx?id=";

        protected void Page_Load(object sender, EventArgs e)
        {
            errorControl.Visible = false;
            Page.SetFocus(txtTitle);
            Page.Title = "BHL Admin - Segment Search";
        }

        private void search()
        {
            if (txtSegmentID.Text.Trim().Length == 0 && txtSourceID.Text.Trim().Length == 0 && txtTitle.Text.Trim().Length == 0) return;

            BHLProvider bp = new BHLProvider();
            List<Segment> results = new List<Segment>();
            if (txtSegmentID.Text.Trim().Length > 0)
            {
                Segment result = bp.SegmentSelectForSegmentID(Convert.ToInt32(txtSegmentID.Text));
                if (result != null) results.Add(result);
            }
            else if (txtSourceID.Text.Trim().Length > 0)
            {
                Segment result = bp.SegmentSelectByBarCode(txtSourceID.Text);
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
            search();
        }

        #endregion Event Handlers

    }
}