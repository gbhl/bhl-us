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
	public partial class VaultEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				BHLProvider bp = new BHLProvider();
				CustomGenericList<Vault> vaults = bp.VaultSelectAll();

				Vault emptyVault = new Vault();
				vaults.Insert( 0, emptyVault );

				ddlVaults.DataSource = vaults;
				ddlVaults.DataTextField = "Description";
				ddlVaults.DataValueField = "VaultID";
				ddlVaults.DataBind();
			}

			errorControl.Visible = false;
		}

		private void clearForm( ControlCollection controls )
		{
			foreach ( Control c in controls )
			{
				if ( c is TextBox && !c.ID.Equals( "idTextBox" ) )
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

		private bool validate( bool isNew )
		{
			bool flag = false;

			if ( serverTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "Server is missing" );
			}
			if ( folderShareTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "Folder share is missing" );
			}
			if ( webVirtualFolderTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "Web virtual folder is missing" );
			}
            if (OCRFolderShareTextBox.Text.Trim().Length == 0)
            {
                flag = true;
                errorControl.AddErrorText("OCR folder share is missing");
            }

			if ( idTextBox.Text.Trim().Length > 0 )
			{
				int id = 0;
				if ( int.TryParse( idTextBox.Text, out id ) )
				{
					if ( isNew )
					{
						foreach ( ListItem item in ddlVaults.Items )
						{
							int idv = int.Parse( item.Value );
							if ( id == idv )
							{
								flag = true;
								errorControl.AddErrorText( "Vault ID " + idTextBox.Text.Trim() + " is not a unique id" );
							}
						}
					}
				}
				else
				{
					flag = true;
					errorControl.AddErrorText( "Vault ID must be numeric" );
				}
			}

			errorControl.Visible = flag;

			return !flag;
		}

		#region Event handlers

		protected void saveButton_Click( object sender, EventArgs e )
		{
			if ( validate( false ) )
			{
				if ( idHid.Value == null || idHid.Value == "" )
				{
					errorControl.AddErrorText( "Please select a vault before saving" );
					errorControl.Visible = true;
					return;
				}

				Vault vault = new Vault( int.Parse( idHid.Value ), serverTextBox.Text.Trim(), folderShareTextBox.Text.Trim(),
					webVirtualFolderTextBox.Text.Trim(), OCRFolderShareTextBox.Text.Trim() );

				vault.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SaveVault( vault );
			}
			else
			{
				return;
			}

			Response.Redirect( "/" );
		}

		protected void saveAsNewButton_Click( object sender, EventArgs e )
		{
			if ( validate( true ) )
			{
				int id = 0;
				if ( idTextBox.Text.Trim().Length > 0 )
				{
					id = int.Parse( idTextBox.Text );
				}

				Vault vault = new Vault( id, serverTextBox.Text.Trim(), folderShareTextBox.Text.Trim(),
					webVirtualFolderTextBox.Text.Trim(), OCRFolderShareTextBox.Text.Trim() );

				vault.IsNew = true;

				BHLProvider bp = new BHLProvider();
				bp.SaveVault( vault );
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

		protected void ddlVaults_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );

			int vaultId = int.Parse( ddlVaults.SelectedValue );
			if ( vaultId > 0 )
			{
				BHLProvider bp = new BHLProvider();
				Vault vault = bp.VaultSelect( vaultId );
				if ( vault != null )
				{
					idTextBox.Text = vault.VaultID.ToString();
					idHid.Value = vault.VaultID.ToString();
					serverTextBox.Text = vault.Server;
					folderShareTextBox.Text = vault.FolderShare;
					webVirtualFolderTextBox.Text = vault.WebVirtualDirectory;
                    OCRFolderShareTextBox.Text = vault.OCRFolderShare;
					ddlVaults.SelectedValue = vault.VaultID.ToString();
				}
			}
		}

		#endregion

	}
}
