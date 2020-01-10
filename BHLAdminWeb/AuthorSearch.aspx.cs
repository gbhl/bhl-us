using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AuthorSearch : System.Web.UI.Page
    {
        private bool _refreshSearch = false;
        private SortOrder _sortOrder = SortOrder.Ascending;
        private int _sortColumnIndex = 1;
        private String _redirectUrl = "/AuthorEdit.aspx?id=";

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
            Page.SetFocus(txtName);
            Page.Title = "BHL Admin - Author Search";
        }

        private void search()
	    {
		    if ( txtAuthorID.Text.Trim().Length == 0 && txtName.Text.Trim().Length == 0  ) return;

		    BHLProvider bp = new BHLProvider();
            List<Author> results = new List<Author>();
            if (txtAuthorID.Text.Trim().Length > 0)
            {
                Author result = bp.AuthorSelectWithNameByAuthorId(Convert.ToInt32(txtAuthorID.Text));
                if (result != null) results.Add(result);
            }
            else
            {
                results = bp.SearchAuthorComplete(txtName.Text.Trim());
            }
		    if ( results.Count == 1 )
		    {
			    Response.Redirect( _redirectUrl + results[ 0 ].AuthorID.ToString() );
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