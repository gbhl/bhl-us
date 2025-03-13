using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AuthorSearch : System.Web.UI.Page
    {
        private readonly string _redirectUrl = "/AuthorEdit.aspx?id=";

        protected void Page_Load(object sender, EventArgs e)
        {
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
            search();
        }

        #endregion Event Handlers

    }
}