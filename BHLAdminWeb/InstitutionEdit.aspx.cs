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
	public partial class InstitutionEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				fillInstitutions();
			}
			errorControl.Visible = false;
		}

		private void fillInstitutions()
		{
			BHLProvider bp = new BHLProvider();
			CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

			Institution emptyInstitution = new Institution();
			emptyInstitution.InstitutionCode = "";
			emptyInstitution.InstitutionName = "";
			institutions.Insert( 0, emptyInstitution );

			ddlInstitutions.DataSource = institutions;
			ddlInstitutions.DataTextField = "InstitutionName";
			ddlInstitutions.DataValueField = "InstitutionCode";
			ddlInstitutions.DataBind();
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
				errorControl.AddErrorText( "Name is missing" );
			}

			errorControl.Visible = flag;

			return !flag;
		}

		#region Event handlers

		protected void saveButton_Click( object sender, EventArgs e )
		{
			if ( validate() )
			{
				if ( hidCode.Value.Length == 0 )
				{
					errorControl.AddErrorText("Please select a content provider before saving" );
					errorControl.Visible = true;
					return;
				}

                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                Institution institution = new Institution();
                institution.InstitutionCode = hidCode.Value;
                institution.InstitutionName = nameTextBox.Text.Trim();
                institution.Note = noteTextBox.Text.Trim();
                institution.InstitutionUrl = urlTextBox.Text.Trim();
                institution.BHLMemberLibrary = chkIsMemberLibrary.Checked;

				institution.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SaveInstitution( institution, userId );
			}
			else
			{
				return;
			}

			Response.Redirect( "/" );
		}

		protected void saveAsNewButton_Click( object sender, EventArgs e )
		{
			BHLProvider bp = new BHLProvider();

			if ( validate() )
			{
				if ( codeTextBox.Text.Trim().Length == 0 )
				{
					errorControl.AddErrorText( "Please include a unique code before saving" );
					errorControl.Visible = true;
					return;
				}
				else
				{
					CustomGenericList<Institution> institutions = bp.InstituationSelectAll();
					string code = codeTextBox.Text.Trim().ToUpper();
					foreach ( Institution inst in institutions )
					{
						if ( inst.InstitutionCode.ToUpper().Equals( code ) )
						{
							errorControl.AddErrorText( "The code " + inst.InstitutionCode.ToUpper() +
								" is not a unique code, please use another one." );
							errorControl.Visible = true;
							return;
						}
					}
				}

                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                Institution institution = new Institution();
                institution.InstitutionCode = codeTextBox.Text.Trim().ToUpper();
                institution.InstitutionName = nameTextBox.Text.Trim();
                institution.Note = noteTextBox.Text.Trim();
                institution.InstitutionUrl = urlTextBox.Text.Trim();
                institution.BHLMemberLibrary = chkIsMemberLibrary.Checked;
                institution.IsNew = true;

				bp.SaveInstitution( institution, userId );
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

		protected void ddlInstitutions_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );
			string code = ddlInstitutions.SelectedValue;
			if ( code.Length > 0 )
			{
				BHLProvider bp = new BHLProvider();
				Institution inst = bp.InstitutionSelectAuto( code );
				if ( inst != null )
				{
					codeTextBox.Text = inst.InstitutionCode;
					hidCode.Value = inst.InstitutionCode;
					nameTextBox.Text = inst.InstitutionName;
					urlTextBox.Text = BHL.DataObjects.Utility.EmptyIfNull( inst.InstitutionUrl );
					noteTextBox.Text = BHL.DataObjects.Utility.EmptyIfNull( inst.Note );
                    chkIsMemberLibrary.Checked = inst.BHLMemberLibrary;

					ddlInstitutions.SelectedValue = inst.InstitutionCode;

                    editHistoryControl.EntityName = "institution";
                    editHistoryControl.EntityId = inst.InstitutionCode;
                }
            }
		}

		#endregion
	}
}
