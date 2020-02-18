using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            editHistoryControl.EntityName = "author";
            editHistoryControl.EntityId = lblID.Text;

            litMessage.Text = "";
            errorControl.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill Methods

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            List<AuthorType> authorTypes = bp.AuthorTypeSelectAll();

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
                author.AuthorNames = new List<AuthorName>();
                author.AuthorIdentifiers = new List<AuthorIdentifier>();
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
            txtNote.Text = author.Note;

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
            List<AuthorName> authorNames = new List<AuthorName>();
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

        private AuthorName findAuthorName(List<AuthorName> authorNames,
            int authorNameId, int authorId, string fullName, string lastName, 
            string firstName, string fullerForm)
        {
            foreach (AuthorName ai in authorNames)
            {
                if (ai.IsDeleted)
                {
                    continue;
                }
                if (authorNameId == 0 && ai.AuthorNameID == 0 && authorId == ai.AuthorID &&
                    fullName == ai.FullName && lastName == ai.LastName && firstName == ai.FirstName &&
                    fullerForm == ai.FullerForm)
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
            List<AuthorIdentifier> authorIdentifiers = new List<AuthorIdentifier>();
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

        List<Identifier> _identifiers = null;
        protected List<Identifier> GetIdentifiers()
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

        private AuthorIdentifier findAuthorIdentifier(List<AuthorIdentifier> authorIdentifiers,
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
                        (int)namesList.DataKeys[e.RowIndex].Values[1],
                        namesList.DataKeys[e.RowIndex].Values[2].ToString(),
                        namesList.DataKeys[e.RowIndex].Values[3].ToString(),
                        namesList.DataKeys[e.RowIndex].Values[4].ToString(),
                        namesList.DataKeys[e.RowIndex].Values[5].ToString());

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
                    (int)namesList.DataKeys[rowNum].Values[1],
                    namesList.DataKeys[rowNum].Values[2].ToString(),
                    namesList.DataKeys[rowNum].Values[3].ToString(),
                    namesList.DataKeys[rowNum].Values[4].ToString(),
                    namesList.DataKeys[rowNum].Values[5].ToString());

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
                var user = Helper.GetCurrentUserDetail(new HttpRequestWrapper(Request));
                userId = user.Id;

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
                author.Note = txtNote.Text;
                author.IsNew = (author.AuthorID == 0);

                // Forces deletes to happen first
                //author.AuthorNames.Sort(SortOrder.Descending, "IsDeleted");
                //author.AuthorIdentifiers.Sort(SortOrder.Descending, "IsDeleted");
                author.AuthorNames.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                author.AuthorIdentifiers.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));

                BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
                int authorID = bp.SaveAuthor(author, (int)userId, 
                    string.Format("{0} {1} ({2})", user.FirstName, user.LastName, user.Email));

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
            bool isValid = true;

            // Check that all edits were completed
            if (namesList.EditIndex != -1)
            {
                isValid = false;
                errorControl.AddErrorText("Names has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (identifiersList.EditIndex != -1)
            {
                isValid = false;
                errorControl.AddErrorText("Identifiers has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
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
                isValid = false;
                errorControl.AddErrorText("At least one name must be entered.");
            }
            if (countPreferred != 1)
            {
                isValid = false;
                errorControl.AddErrorText("Use the \"Preferred\" checkbox to mark one (and only one) name as the preferred name for the author.");
            }

            // Make sure that no identifiers have been assigned to this author more than once
            Dictionary<string, int> idDict = new Dictionary<string, int>();
            foreach(AuthorIdentifier aid in author.AuthorIdentifiers)
            {
                if (!aid.IsDeleted)
                {
                    string idKey = aid.IdentifierName + ":" + aid.IdentifierValue;
                    if (idDict.ContainsKey(idKey))
                        idDict[idKey]++;
                    else
                        idDict.Add(idKey, 1);
                }
            }
            foreach(KeyValuePair<string,int> kvp in idDict)
            {
                if (kvp.Value > 1)
                {
                    isValid = false;
                    errorControl.AddErrorText(string.Format("The identifier {0} has been assigned to this author more than once.",
                        kvp.Key));
                }
            }

            // Make sure that no identifiers assigned to this author are duplicates of identifiers assigned to other authors
            foreach(AuthorIdentifier aid in author.AuthorIdentifiers)
            {
                if (!aid.IsDeleted && aid.IsDirty)
                {
                    List<Author> dupIDs = new BHLProvider().AuthorSelectByIdentifier(aid.IdentifierID, aid.IdentifierValue);
                    foreach (Author dupID in dupIDs)
                    {
                        if (dupID.AuthorID != author.AuthorID)
                        {
                            isValid = false;
                            errorControl.AddErrorText(string.Format("The identifier {0}:{1} has already been assigned to \"{2}\" (BHL Author ID: {3}).",
                                aid.IdentifierName, aid.IdentifierValue, dupID.NameExtended.Trim(), dupID.AuthorID));
                        }
                    }
                }
            }

            // Validate the "replaced by" settings
            if (txtReplacedBy.Text.Trim().Length > 0)
            {
                if (chkIsActive.Checked)
                {
                    isValid = false;
                    errorControl.AddErrorText("'Replaced By' identifier not applicable when the current Author is active.  Before saving, remove the 'Replaced By' identifier or uncheck the 'Is Active' checkbox.");
                }

                int authorID;
                if (Int32.TryParse(txtReplacedBy.Text, out authorID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().AuthorSelectAuto(authorID) == null)
                    {
                        isValid = false;
                        errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Author ID.");
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    isValid = false;
                    errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Author ID.");
                }
            }

            errorControl.Visible = !isValid;

            return isValid;
        }

    }
}