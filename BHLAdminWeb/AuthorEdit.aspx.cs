using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AuthorEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // jQuery runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            // jQuery UI library
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            if (!IsPostBack)
            {
                string idString = Request.QueryString["id"];
                int id = 0;
                if (idString != null && int.TryParse(idString, out id))
                {
                    hypTitles.Attributes["onclick"] = String.Format(hypTitles.Attributes["onclick"], idString);
                    hypSegments.Attributes["onclick"] = String.Format(hypSegments.Attributes["onclick"], idString);
                    fillCombos();
                    fillUI(id);
                }
                else
                {
                    // TODO: Inform user that author does not exist -- Perhaps redirect to unknown.aspx?type=title
                }
            }

            litMessage.Text = "";
            errorControl.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill Methods

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            CustomGenericList<AuthorType> authorTypes = bp.AuthorTypeSelectAll();

            ddlAuthorType.DataSource = authorTypes;
            ddlAuthorType.DataTextField = "AuthorTypeName";
            ddlAuthorType.DataValueField = "AuthorTypeID";
            ddlAuthorType.DataBind();
        }

        private void fillUI(int id)
        {
            lblID.Text = id.ToString();

            Author author = new Author();
            if (id == 0)
            {
                // New author
                author.IsActive = 1;
                author.AuthorTypeID = 1;
                author.AuthorNames = new CustomGenericList<AuthorName>();
                author.AuthorIdentifiers = new CustomGenericList<AuthorIdentifier>();
                author.IsNew = true;
            }
            else
            {
                // Find existing author
                BHLProvider bp = new BHLProvider();
                author = bp.AuthorSelectExtended(id);
            }

            Session["Author" + author.AuthorID.ToString()] = author;

            chkIsActive.Checked = (author.IsActive == 1);
            txtReplacedBy.Text = author.RedirectAuthorID.ToString();
            ddlAuthorType.SelectedValue = (author.AuthorTypeID ?? 0).ToString();
            txtStartDate.Text = author.StartDate;
            txtEndDate.Text = author.EndDate;
            txtNumeration.Text = author.Numeration;
            txtTitle.Text = author.Title;
            txtUnit.Text = author.Unit;
            txtLocation.Text = author.Location;

            namesList.DataSource = author.AuthorNames;
            namesList.DataBind();

            identifiersList.DataSource = author.AuthorIdentifiers;
            identifiersList.DataBind();
        }

        #endregion Fill Methods

        #region AuthorName Methods

        private void bindAuthorNameData()
        {
            Author author = (Author)Session["Author" + lblID.Text];

            // filter out deleted items
            CustomGenericList<AuthorName> authorNames = new CustomGenericList<AuthorName>();
            foreach (AuthorName name in author.AuthorNames)
            {
                if (name.IsDeleted == false)
                {
                    authorNames.Add(name);
                }
            }

            namesList.DataSource = authorNames;
            namesList.DataBind();
        }

        private AuthorName findAuthorName(CustomGenericList<AuthorName> authorNames,
            int authorNameId, int authorId)
        {
            foreach (AuthorName ai in authorNames)
            {
                if (ai.IsDeleted)
                {
                    continue;
                }
                if (authorNameId == 0 && ai.AuthorNameID == 0 && authorId == ai.AuthorID)
                {
                    return ai;
                }
                else if (authorNameId > 0 && ai.AuthorNameID == authorNameId)
                {
                    return ai;
                }
            }

            return null;
        }

        #endregion AuthorName Methods

        #region AuthorIdentifier Methods

        private void bindAuthorIdentifierData()
        {
            Author author = (Author)Session["Author" + lblID.Text];

            // filter out deleted items
            CustomGenericList<AuthorIdentifier> authorIdentifiers = new CustomGenericList<AuthorIdentifier>();
            foreach (AuthorIdentifier ai in author.AuthorIdentifiers)
            {
                if (ai.IsDeleted == false)
                {
                    authorIdentifiers.Add(ai);
                }
            }

            identifiersList.DataSource = authorIdentifiers;
            identifiersList.DataBind();
        }

        CustomGenericList<Identifier> _identifiers = null;
        protected CustomGenericList<Identifier> GetIdentifiers()
        {
            BHLProvider bp = new BHLProvider();
            _identifiers = bp.IdentifierSelectAll();

            return _identifiers;
        }

        protected int GetIdentifierIndex(object dataItem)
        {
            string identifierIdString = DataBinder.Eval(dataItem, "IdentifierID").ToString();

            if (!identifierIdString.Equals("0"))
            {
                int identifierId = int.Parse(identifierIdString);
                int ix = 0;
                foreach (Identifier identifier in _identifiers)
                {
                    if (identifier.IdentifierID == identifierId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        private AuthorIdentifier findAuthorIdentifier(CustomGenericList<AuthorIdentifier> authorIdentifiers,
            int authorIdentifierId, int identifierID, string identifierValue)
        {
            foreach (AuthorIdentifier ai in authorIdentifiers)
            {
                if (ai.IsDeleted)
                {
                    continue;
                }
                if (authorIdentifierId == 0 && ai.AuthorIdentifierID == 0 &&
                    identifierID == ai.IdentifierID &&
                    identifierValue == ai.IdentifierValue)
                {
                    return ai;
                }
                else if (authorIdentifierId > 0 && ai.AuthorIdentifierID == authorIdentifierId)
                {
                    return ai;
                }
            }

            return null;
        }

        #endregion  AuthorIdentifier Methods
        
        #region Event Handlers

        #region AuthorName event handlers

        protected void namesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            namesList.EditIndex = e.NewEditIndex;
            bindAuthorNameData();
        }

        protected void namesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = namesList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox txtFullName = row.FindControl("txtFullName") as TextBox;
                TextBox txtLastName = row.FindControl("txtLastName") as TextBox;
                TextBox txtFirstName = row.FindControl("txtFirstName") as TextBox;
                TextBox txtFullerForm = row.FindControl("txtFullerForm") as TextBox;
                CheckBox chkIsPreferred = row.FindControl("chkIsPreferredEdit") as CheckBox;
                if (txtFullName != null)
                {
                    Author author = (Author)Session["Author" + lblID.Text];
                    String fullName = txtFullName.Text;
                    String lastName = txtLastName.Text;
                    String firstName = txtFirstName.Text;
                    String fullerForm = txtFullerForm.Text;
                    bool isPreferred = chkIsPreferred.Checked;

                    AuthorName authorName = findAuthorName(author.AuthorNames,
                        (int)namesList.DataKeys[e.RowIndex].Values[0],
                        (int)namesList.DataKeys[e.RowIndex].Values[1]);

                    authorName.AuthorID = author.AuthorID;
                    authorName.FullName = fullName;
                    authorName.LastName = lastName;
                    authorName.FirstName = firstName;
                    authorName.FullerForm = fullerForm;
                    authorName.IsPreferredName = (short)(isPreferred ? 1 : 0);
                }
            }

            namesList.EditIndex = -1;
            bindAuthorNameData();
        }

        protected void namesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            namesList.EditIndex = -1;
            bindAuthorNameData();
        }

        protected void addAuthorNameButton_Click(object sender, EventArgs e)
        {
            Author author = (Author)Session["Author" + lblID.Text];
            AuthorName authorName = new AuthorName();
            authorName.AuthorID = author.AuthorID;
            author.AuthorNames.Add(authorName);
            namesList.EditIndex = namesList.Rows.Count;
            bindAuthorNameData();
        }

        protected void namesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Author author = (Author)Session["Author" + lblID.Text];

                AuthorName authorName = findAuthorName(author.AuthorNames,
                    (int)namesList.DataKeys[rowNum].Values[0],
                    (int)namesList.DataKeys[rowNum].Values[1]);

                authorName.IsDeleted = true;
                bindAuthorNameData();
            }
        }

        #endregion AuthorName event handlers

        #region AuthorIdentifier event handlers

        protected void identifiersList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            identifiersList.EditIndex = e.NewEditIndex;
            bindAuthorIdentifierData();
        }

        protected void identifiersList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = identifiersList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlIdentifierName = row.FindControl("ddlIdentifierName") as DropDownList;
                TextBox txtIdentifierValue = row.FindControl("txtIdentifierValue") as TextBox;
                if (ddlIdentifierName != null && txtIdentifierValue != null)
                {
                    Author author = (Author)Session["Author" + lblID.Text];
                    int identifierId = int.Parse(ddlIdentifierName.SelectedValue);
                    String identifierValue = txtIdentifierValue.Text;

                    AuthorIdentifier authorIdentifier = findAuthorIdentifier(author.AuthorIdentifiers,
                        (int)identifiersList.DataKeys[e.RowIndex].Values[0],
                        (int)identifiersList.DataKeys[e.RowIndex].Values[1],
                        identifiersList.DataKeys[e.RowIndex].Values[2].ToString());

                    authorIdentifier.AuthorID = author.AuthorID;
                    authorIdentifier.IdentifierID = identifierId;
                    authorIdentifier.IdentifierName = ddlIdentifierName.SelectedItem.Text;
                    authorIdentifier.IdentifierValue = identifierValue;
                }
            }

            identifiersList.EditIndex = -1;
            bindAuthorIdentifierData();
        }

        protected void identifiersList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            identifiersList.EditIndex = -1;
            bindAuthorIdentifierData();
        }

        protected void addAuthorIdentifierButton_Click(object sender, EventArgs e)
        {
            Author author = (Author)Session["Author" + lblID.Text];
            AuthorIdentifier ai = new AuthorIdentifier();
            ai.AuthorID= author.AuthorID;
            author.AuthorIdentifiers.Add(ai);
            identifiersList.EditIndex = identifiersList.Rows.Count;
            bindAuthorIdentifierData();
        }

        protected void identifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Author author = (Author)Session["Author" + lblID.Text];

                AuthorIdentifier authorIdentifier = findAuthorIdentifier(author.AuthorIdentifiers,
                    (int)identifiersList.DataKeys[rowNum].Values[0],
                    (int)identifiersList.DataKeys[rowNum].Values[1],
                    identifiersList.DataKeys[rowNum].Values[2].ToString());

                authorIdentifier.IsDeleted = true;
                bindAuthorIdentifierData();
            }
        }

        #endregion TitleIdentifier event handlers

        protected void saveButton_Click(object sender, EventArgs e)
        {
            Author author = (Author)Session["Author" + lblID.Text];
            int? userId = null;

            if (validate(author))
            {
                // Set the id of the editing user
                userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                // Gather up data on form
                author.IsActive = (short)(chkIsActive.Checked ? 1 : 0);
                author.RedirectAuthorID = (txtReplacedBy.Text.Trim().Length == 0 || author.IsActive == 1 ? (int?)null : Convert.ToInt32(txtReplacedBy.Text));
                author.AuthorTypeID = Convert.ToInt32(ddlAuthorType.SelectedValue.Length == 0 ? null : ddlAuthorType.SelectedValue);
                author.StartDate = txtStartDate.Text;
                author.EndDate = txtEndDate.Text;
                author.Numeration = txtNumeration.Text;
                author.Title = txtTitle.Text;
                author.Unit = txtUnit.Text;
                author.Location = txtLocation.Text;
                author.IsNew = (author.AuthorID == 0);

                // Forces deletes to happen first
                author.AuthorNames.Sort(SortOrder.Descending, "IsDeleted");
                author.AuthorIdentifiers.Sort(SortOrder.Descending, "IsDeleted");
                
                BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
                int authorID = bp.SaveAuthor(author, (int)userId);

                // After a successful save operation, reload the title
                fillUI(authorID);
            }
            else
            {
                return;
            }

            litMessage.Text = "<span class='liveData'>Author Saved.</span>";
            Page.MaintainScrollPositionOnPostBack = false;
        }

        #endregion Event Handlers

        private bool validate(Author author)
        {
            bool flag = false;

            // Check that all edits were completed
            if (namesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Names has an edit pending");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending");
            }

            // Make sure that at least one name has been entered, and that only one preferred name has been identified
            int countNonDeleted = 0;
            int countPreferred = 0;
            foreach (AuthorName name in author.AuthorNames)
            {
                if (!name.IsDeleted)
                {
                    countNonDeleted++;
                    if (name.IsPreferredName == 1) countPreferred++;
                }
            }
            if (countNonDeleted == 0)
            {
                flag = true;
                errorControl.AddErrorText("At least one name must be entered.");
            }
            if (countPreferred != 1)
            {
                flag = true;
                errorControl.AddErrorText("One (and only one) name must be marked as the preferred name for the author.");
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (txtReplacedBy.Text.Trim().Length > 0)
            {
                int authorID;
                if (Int32.TryParse(txtReplacedBy.Text, out authorID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().AuthorSelectAuto(authorID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Author ID.");
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Author ID.");
                }
            }

            errorControl.Visible = flag;
            Page.MaintainScrollPositionOnPostBack = !flag;

            return !flag;
        }

    }
}