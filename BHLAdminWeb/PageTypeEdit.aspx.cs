using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
	public partial class PageTypeEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				BHLProvider bp = new BHLProvider();
				CustomGenericList<PageType> pageTypes = bp.PageTypeSelectAll();

				pageTypes.Sort();

				PageType emptyPageType = new PageType();
				emptyPageType.PageTypeID = -1;
				emptyPageType.PageTypeName = "";
				pageTypes.Insert( 0, emptyPageType );

				ddlPageTypes.DataSource = pageTypes;
				ddlPageTypes.DataTextField = "PageTypeName";
				ddlPageTypes.DataValueField = "PageTypeID";
				ddlPageTypes.DataBind();
			}

			errorControl.Visible = false;
		}

		private void clearForm( ControlCollection controls )
		{
			foreach ( Control c in controls )
			{
				if ( c is TextBox )
				{
					TextBox textBox = (TextBox)c;
					textBox.Text = "";
				}
				else if ( c.HasControls() )
				{
					clearForm( c.Controls );
				}
			}
		}

		private bool validate()
		{
			bool flag = false;
			if ( nameTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "Page type name is missing" );
			}

			errorControl.Visible = flag;

			return !flag;
		}

		#region Event handlers

		protected void saveButton_Click( object sender, EventArgs e )
		{
			if ( validate() )
			{
				if ( idLabel.Text.Length == 0 )
				{
					errorControl.AddErrorText( "Please select a page type before saving" );
					errorControl.Visible = true;
					return;
				}

				PageType pageType = new PageType( int.Parse( idLabel.Text ), nameTextBox.Text.Trim(),
					descriptionTextBox.Text.Trim() );

				pageType.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SavePageType( pageType );
			}
			else
			{
				return;
			}

			Response.Redirect( "/" );
		}

		protected void saveAsNewButton_Click( object sender, EventArgs e )
		{
			if ( validate() )
			{
				PageType pageType = new PageType( 0, nameTextBox.Text.Trim(),
					descriptionTextBox.Text.Trim() );

				pageType.IsNew = true;

				BHLProvider bp = new BHLProvider();
				bp.SavePageType( pageType );
			}
			else
			{
				return;
			}

			Response.Redirect( "/" );
		}

		protected void clearButton_Click( object sender, EventArgs e )
		{
			clearForm( this.Controls );
		}

		protected void ddlPageTypes_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );
			int pageTypeId = int.Parse( ddlPageTypes.SelectedValue );
			if ( pageTypeId > 0 )
			{
				BHLProvider bp = new BHLProvider();
				PageType pageType = bp.PageTypeSelectAuto( pageTypeId );
				if ( pageType != null )
				{
					idLabel.Text = pageType.PageTypeID.ToString();
					nameTextBox.Text = pageType.PageTypeName;
					descriptionTextBox.Text = BHL.DataObjects.Utility.EmptyIfNull( pageType.PageTypeDescription );
					ddlPageTypes.SelectedValue = pageType.PageTypeID.ToString();

                    editHistoryControl.EntityName = "pagetype";
                    editHistoryControl.EntityId = pageType.PageTypeID.ToString();
                }
            }
		}

		#endregion

	}
}
