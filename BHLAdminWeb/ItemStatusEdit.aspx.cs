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
using FredCK.FCKeditorV2;

namespace MOBOT.BHL.AdminWeb
{
	public partial class ItemStatusEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				BHLProvider bp = new BHLProvider();
				CustomGenericList<ItemStatus> itemStatuses = bp.ItemStatusSelectAll();

				itemStatuses.Sort();

				ItemStatus emptyItemStatus = new ItemStatus();
				emptyItemStatus.ItemStatusID = -1;
				emptyItemStatus.ItemStatusName = "";
				itemStatuses.Insert( 0, emptyItemStatus );

				ddlItemStatuses.DataSource = itemStatuses;
				ddlItemStatuses.DataTextField = "ItemStatusName";
				ddlItemStatuses.DataValueField = "ItemStatusID";
				ddlItemStatuses.DataBind();
			}
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
			if ( statusTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorPanel.Controls.Add( new LiteralControl( "* Item status name is missing" ) );
			}

			errorPanel.Visible = flag;

			return !flag;
		}

		#region Event handlers

		protected void saveButton_Click( object sender, EventArgs e )
		{
			if ( validate() )
			{
				if ( hidId.Value.Length == 0 )
				{
					errorPanel.Controls.Add( new LiteralControl( "* Please select an item status before saving" ) );
					errorPanel.Visible = true;
					return;
				}

				ItemStatus itemStatus = new ItemStatus( int.Parse( hidId.Value ), statusTextBox.Text.Trim() );

				itemStatus.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SaveItemStatus( itemStatus );
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
				if ( idTextBox.Text.Trim().Length == 0 )
				{
					errorPanel.Controls.Add( new LiteralControl( "* Please include a unique ID before saving" ) );
					errorPanel.Visible = true;
					return;
				}
				else
				{
					int id = 0;
					if ( int.TryParse( idTextBox.Text.Trim(), out id ) )
					{
						BHLProvider bp = new BHLProvider();

						CustomGenericList<ItemStatus> itemStatuses = bp.ItemStatusSelectAll();
						foreach ( ItemStatus its in itemStatuses )
						{
							if ( its.ItemStatusID == id )
							{
								errorPanel.Controls.Add( new LiteralControl( "* The item status id " + id.ToString() +
									" is not a unique id, please use another one." ) );
								errorPanel.Visible = true;
								return;
							}
						}

						ItemStatus itemStatus = new ItemStatus( id, statusTextBox.Text.Trim() );
						itemStatus.IsNew = true;

						bp.SaveItemStatus( itemStatus );
					}
					else
					{
						errorPanel.Controls.Add( new LiteralControl( "* The item status id " + idTextBox.Text.Trim() +
							" is not numeric, please use numbers only." ) );
						errorPanel.Visible = true;
						return;
					}
				}
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

		protected void ddlItemStatuses_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );
			int itemStatusId = int.Parse( ddlItemStatuses.SelectedValue );
			if ( itemStatusId > 0 )
			{
				BHLProvider bp = new BHLProvider();
				ItemStatus itemStatus = bp.ItemStatusSelectAuto( itemStatusId );
				if ( itemStatus != null )
				{
					idTextBox.Text = itemStatus.ItemStatusID.ToString();
					hidId.Value = itemStatus.ItemStatusID.ToString();
					statusTextBox.Text = itemStatus.ItemStatusName;
					ddlItemStatuses.SelectedValue = itemStatus.ItemStatusID.ToString();
				}
			}
		}

		#endregion

	}
}
