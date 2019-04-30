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
	public partial class LanguageEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				fillLanguages();
			}
			errorControl.Visible = false;
		}

		private void fillLanguages()
		{
			BHLProvider bp = new BHLProvider();
			CustomGenericList<Language> languages = bp.LanguageSelectAll();

			Language emptyLanguage = new Language();
			emptyLanguage.LanguageCode = "";
			emptyLanguage.LanguageName = "";
			languages.Insert( 0, emptyLanguage );

			ddlLanguages.DataSource = languages;
			ddlLanguages.DataTextField = "LanguageName";
			ddlLanguages.DataValueField = "LanguageCode";
			ddlLanguages.DataBind();
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
				errorControl.AddErrorText( "Language name is missing" );
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
					errorControl.AddErrorText( "Please select an language before saving" );
					errorControl.Visible = true;
					return;
				}

				Language language = new Language( hidCode.Value, nameTextBox.Text.Trim(), noteTextBox.Text.Trim() );

				language.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SaveLanguage( language );
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
					errorControl.AddErrorText( "Please include a unique language code before saving" );
					errorControl.Visible = true;
					return;
				}
				else
				{
					CustomGenericList<Language> languages = bp.LanguageSelectAll();
					string code = codeTextBox.Text.Trim().ToUpper();
					foreach ( Language language in languages )
					{
						if ( language.LanguageCode.ToUpper().Equals( code ) )
						{
							errorControl.AddErrorText( "The language code " + language.LanguageCode.ToUpper() +
								" is not a unique code, please use another one." );
							errorControl.Visible = true;
							return;
						}
					}
				}

				Language lang = new Language( codeTextBox.Text.Trim().ToUpper(), nameTextBox.Text.Trim(),
					noteTextBox.Text.Trim() );

				lang.IsNew = true;

				bp.SaveLanguage( lang );
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

		protected void ddlLanguages_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );
			string code = ddlLanguages.SelectedValue;
			if ( code.Length > 0 )
			{
				BHLProvider bp = new BHLProvider();
				Language language = bp.LanguageSelectAuto( code );
				if ( language != null )
				{
					codeTextBox.Text = language.LanguageCode;
					hidCode.Value = language.LanguageCode;
					nameTextBox.Text = language.LanguageName;
					noteTextBox.Text = MOBOT.BHL.DataObjects.Utility.EmptyIfNull( language.Note );

					ddlLanguages.SelectedValue = language.LanguageCode;

                    editHistoryControl.EntityName = "language";
                    editHistoryControl.EntityId = language.LanguageCode;
                }
            }
		}

		#endregion
	}
}
