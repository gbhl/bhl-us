using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
	public partial class PageTypeEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				BHLProvider bp = new BHLProvider();
				List<PageType> pageTypes = bp.PageTypeSelectAll();

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

				int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

				BHLProvider bp = new BHLProvider();
				PageType pageType = bp.PageTypeSelectAuto(int.Parse(idLabel.Text));
				pageType.PageTypeName = nameTextBox.Text.Trim();
				pageType.Active = (byte)(chkActive.Checked ? 1 : 0);
				pageType.LastModifiedDate = DateTime.Now;
				pageType.IsNew = false;
				bp.SavePageType(pageType, userId);
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
				int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
				PageType pageType = new PageType {
					PageTypeID = 0,
					PageTypeName = nameTextBox.Text.Trim(),
					PageTypeDescription = "",
					Active = (byte)(chkActive.Checked ? 1 : 0),
					CreationDate = DateTime.Now,
					LastModifiedDate = DateTime.Now
				};
				pageType.IsNew = true;

				BHLProvider bp = new BHLProvider();
				bp.SavePageType( pageType, userId );
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
					chkActive.Checked = (pageType.Active == 1);
					ddlPageTypes.SelectedValue = pageType.PageTypeID.ToString();

                    editHistoryControl.EntityName = "pagetype";
                    editHistoryControl.EntityId = pageType.PageTypeID.ToString();
                }
            }
		}

		#endregion

	}
}
