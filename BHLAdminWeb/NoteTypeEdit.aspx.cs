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
	public partial class NoteTypeEdit : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				BHLProvider bp = new BHLProvider();
				CustomGenericList<NoteType> noteTypes = bp.NoteTypeSelectAll();

                NoteType emptyType = new NoteType();
				noteTypes.Insert(0, emptyType);

                ddlNoteTypes.DataSource = noteTypes;
                ddlNoteTypes.DataTextField = "NoteTypeNameExtended";
                ddlNoteTypes.DataValueField = "NoteTypeID";
                ddlNoteTypes.DataBind();
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

			if ( noteTypeNameTextBox.Text.Trim().Length == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "Note Type Name is missing" );
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
					errorControl.AddErrorText( "Please select a Note Type before saving" );
					errorControl.Visible = true;
					return;
				}

                // Set the id of the editing user
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                NoteType noteType = new BHLProvider().NoteTypeSelectAuto(int.Parse(idLabel.Text));
                noteType.NoteTypeName = noteTypeNameTextBox.Text.Trim();
                noteType.NoteTypeDisplay = noteTypeDisplayTextBox.Text.Trim();
                noteType.MarcDataFieldTag = marcDataFieldTagTextBox.Text.Trim();
                noteType.MarcIndicator1 = marcIndicator1TextBox.Text.Trim();
                noteType.LastModifiedDate = DateTime.Now;
				noteType.IsNew = false;

				BHLProvider bp = new BHLProvider();
				bp.SaveNoteType( noteType, (int)userId );
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
                // Set the id of the editing user
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                NoteType noteType = new NoteType();
                noteType.NoteTypeName = noteTypeNameTextBox.Text.Trim();
                noteType.NoteTypeDisplay = noteTypeDisplayTextBox.Text.Trim();
                noteType.MarcDataFieldTag = marcDataFieldTagTextBox.Text.Trim();
                noteType.MarcIndicator1 = marcIndicator1TextBox.Text.Trim();
                noteType.IsNew = true;

				BHLProvider bp = new BHLProvider();
				bp.SaveNoteType( noteType, (int)userId );
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

		protected void ddlNoteTypes_SelectedIndexChanged( object sender, EventArgs e )
		{
			clearForm( this.Controls );

			int noteTypeID = int.Parse( ddlNoteTypes.SelectedValue );
			if ( noteTypeID > 0 )
			{
				BHLProvider bp = new BHLProvider();
                NoteType noteType = bp.NoteTypeSelectAuto(noteTypeID);
				if ( noteType != null )
				{
                    idLabel.Text = noteType.NoteTypeID.ToString();
                    idHid.Value = noteType.NoteTypeID.ToString();
                    noteTypeNameTextBox.Text = noteType.NoteTypeName;
                    noteTypeDisplayTextBox.Text = noteType.NoteTypeDisplay;
                    marcDataFieldTagTextBox.Text = noteType.MarcDataFieldTag;
                    marcIndicator1TextBox.Text = noteType.MarcIndicator1;
                    ddlNoteTypes.SelectedValue = noteType.NoteTypeID.ToString();
				}
			}
		}

		#endregion

	}
}
