using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using FredCK.FCKeditorV2;

namespace MOBOT.BHL.AdminWeb
{
    public partial class CollectionEdit : System.Web.UI.Page
    {
        protected BHLProvider bhlProvider = new BHLProvider();
        protected string statsMessageFormat = "<p>This collection contains <span style='font-weight:bolder'>{0}</span> volumes from <span style='font-weight:bolder'>{1}</span> titles, containing <span style='font-weight:bolder'>{2}</span> pages.</p>";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtHtml.ToolbarSet = "BHL";

                CustomGenericList<Institution> institutions = bhlProvider.InstituationSelectAll();
                ddlInstitution.DataSource = institutions;
                ddlInstitution.DataBind();
                ddlInstitution.Items.Insert(0, new ListItem("(select contributor)", ""));

                CustomGenericList<Language> languages = bhlProvider.LanguageSelectAll();
                ddlLanguage.DataSource = languages;
                ddlLanguage.DataBind();
                ddlLanguage.Items.Insert(0, new ListItem("(select language)", ""));

                populateCollectionList();

                string idString = Request.QueryString["id"];
                int id = 0;
                if (idString != null && int.TryParse(idString, out id))
                {
                    ddlCollections.SelectedValue = idString;
                    populateForm();
                }
            }

            deleteButton.Visible = Helper.IsUserAuthorized(new HttpRequestWrapper(Request), Helper.SecurityRole.BHLAdminUserAdvanced);
            litMessage.Text = "";
            errorControl.Visible = false;
        }

        private void clearForm(ControlCollection controls)
        {
            lblImageUrl.Text = string.Empty;
            lbliTunesImageUrl.Text = string.Empty;

            foreach (Control c in controls)
            {
                if (c is TextBox && !c.ID.Equals("idTextBox"))
                {
                    TextBox textBox = (TextBox)c;
                    textBox.Text = "";
                }
                else if (c.HasControls())
                {
                    clearForm(c.Controls);
                }
                else if (c is FCKeditor)
                {
                    FCKeditor fck = (FCKeditor)c;
                    fck.Value = "";
                }
            }
        }

        private bool validate(bool isNew)
        {
            bool flag = false;
            if (txtCollectionName.Text.Trim().Length == 0)
            {
                flag = true;
                errorControl.AddErrorText("Collection name is missing");
            }

            if (chkFeatured.Checked)
            {
                if (!chkActive.Checked)
                {
                    flag = true;
                    errorControl.AddErrorText("The Featured collection must be Active.");
                }
                else if (chkITunes.Checked && !chkBHL.Checked)
                {
                    flag = true;
                    errorControl.AddErrorText("The Featured collection cannot be an iTunes-only collection.");
                }

                if ((lblImageUrl.Text == "(none)" || lblImageUrl.Text == string.Empty) && 
                    !imageUpload.HasFile)
                {
                    flag = true;
                    errorControl.AddErrorText("The Featured collection must be assigned an Image URL.");
                }
            }

            BHLProvider bp = new BHLProvider();
            if (isNew)
            {
                CustomGenericList<Collection> collections = bp.CollectionSelectByNameAndAllowedContent(
                    txtCollectionName.Text.Trim(),
                    (short)(rdoContents.SelectedValue == "T" ? 1 : 0),
                    (short)(rdoContents.SelectedValue == "I" ? 1 : 0));

                if (collections.Count > 0)
                {
                    flag = true;
                    errorControl.AddErrorText("The combination of name and content type is not unique.");
                }

                collections = bp.CollectionSelectByUrl(txtCollectionURL.Text.Trim());
                if (collections.Count > 0)
                {
                    flag = true;
                    errorControl.AddErrorText(
                        string.Format("The URL is in use by the \"{0}\" collection.  Please choose another.", collections[0].CollectionName));
                }

            }
            else
            {
                CustomGenericList<Collection> collections = bp.CollectionSelectByUrl(txtCollectionURL.Text.Trim());
                if ((collections.Count > 0) && (collections[0].CollectionID != Convert.ToInt32(lblID.Text)))
                {
                    flag = true;
                    errorControl.AddErrorText(
                        string.Format("The URL is in use by the \"{0}\" collection.  Please choose another.", collections[0].CollectionName));
                }
            }

            errorControl.Visible = flag;
            return !flag;
        }

        private void populateCollectionList()
        {
            CustomGenericList<Collection> collections = bhlProvider.CollectionSelectAll();

            Collection emptyCollection = new Collection();
            emptyCollection.CollectionID = -1;
            emptyCollection.CollectionName = "";
            collections.Insert(0, emptyCollection);

            ddlCollections.DataSource = collections;
            ddlCollections.DataTextField = "CollectionNameDetail";
            ddlCollections.DataValueField = "CollectionID";
            ddlCollections.DataBind();
        }

        private void populateForm()
        {
            bool flag = false;
            clearForm(this.Controls);
            int collectionID = int.Parse(ddlCollections.SelectedValue);
            if (collectionID > 0)
            {
                Collection c = bhlProvider.CollectionSelectAuto(collectionID);
                if (c != null)
                {
                    lblID.Text = c.CollectionID.ToString();
                    lnkLandingPage.NavigateUrl = string.Format(ConfigurationManager.AppSettings["CollectionPageUrl"], c.CollectionID.ToString());
                    lnkLandingPage.Visible = true;
                    txtCollectionName.Text = c.CollectionName;
                    txtCollectionDescription.Text = c.CollectionDescription;
                    lblImageUrl.Text = (c.ImageURL == string.Empty ? "(none)" : c.ImageURL);
                    lbliTunesImageUrl.Text = (c.ITunesImageURL == string.Empty ? "(none)" : c.ITunesImageURL);
                    txtCollectionURL.Enabled = string.IsNullOrEmpty(c.CollectionURL);
                    txtCollectionURL.Text = c.CollectionURL;
                    txtHtml.Value = c.HtmlContent ?? string.Empty;
                    if (c.CanContainTitles == 1) rdoContents.SelectedValue = "T";
                    if (c.CanContainItems == 1) rdoContents.SelectedValue = "I";
                    rdoContents.Enabled = false;
                    ddlInstitution.SelectedValue = c.InstitutionCode;
                    ddlLanguage.SelectedValue = c.LanguageCode;

                    switch (c.CollectionTarget)
                    {
                        case "All": chkBHL.Checked = true; chkITunes.Checked = true; break;
                        case "BHL": chkBHL.Checked = true; chkITunes.Checked = false; break;
                        case "iTunes": chkBHL.Checked = false; chkITunes.Checked = true; break;
                        case "": chkBHL.Checked = false; chkITunes.Checked = false; break;
                    }

                    if (!chkBHL.Checked) txtCollectionURL.Enabled = false;
                    if (chkITunes.Checked) iTunesImageUpload.Enabled = true;

                    ddlInstitution.Enabled = false;
                    ddlLanguage.Enabled = false;
                    chkActive.Checked = (c.Active == 1);
                    chkFeatured.Checked = (c.Featured == 1);
                    ddlCollections.SelectedValue = c.CollectionID.ToString();
                    flag = true;

                    // Show the collection statistics
                    MOBOT.BHL.DataObjects.Stats stats = bhlProvider.StatsSelectForCollection(collectionID);
                    ltlCollectionStats.Text = string.Format(statsMessageFormat,
                        stats.VolumeCount.ToString(), stats.TitleCount.ToString(), stats.PageCount.ToString());
                    ltlCollectionStats.Visible = true;
                }
            }
            else
            {
                lblID.Text = "";
                lnkLandingPage.Visible = false;
                txtCollectionURL.Enabled = true;
                rdoContents.Enabled = true;
                ddlInstitution.Enabled = true;
                ddlLanguage.Enabled = true;
                chkBHL.Checked = true;
                chkITunes.Checked = false;
                iTunesImageUpload.Enabled = false;
                ltlCollectionStats.Visible = false;
            }

            contentsButton.Text = "Show Contents";
            contentPanel.Visible = false;
            contentsButton.Enabled = flag;
            bulkAddButton.Enabled = flag;
            refreshContentsButton.Enabled = flag;
            clearContentsButton.Enabled = flag;
        }

        #region Event handlers

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (validate(false))
            {
                if (lblID.Text.Trim().Length == 0)
                {
                    errorControl.AddErrorText("Please select a collection before saving");
                    errorControl.Visible = true;
                    return;
                }

                Collection collection = bhlProvider.CollectionSelectAuto(Convert.ToInt32(lblID.Text));
                collection.CollectionName = txtCollectionName.Text.Trim();
                collection.CollectionDescription = txtCollectionDescription.Text.Trim();
                collection.CollectionURL = txtCollectionURL.Text.Trim();
                txtCollectionURL.Enabled = string.IsNullOrEmpty(collection.CollectionURL);
                collection.HtmlContent = txtHtml.Value.Trim();

                short canContainTitles = (short)(rdoContents.SelectedValue == "T" ? 1 : 0);
                short canContainItems = (short)(rdoContents.SelectedValue == "I" ? 1 : 0);
                if (collection.CanContainTitles == 1 && canContainTitles == 0)
                {
                    // Allowed collection contents have changed, so delete any associated titles
                    bhlProvider.TitleCollectionDeleteForCollection(collection.CollectionID);
                }
                if (collection.CanContainItems == 1 && canContainItems == 0)
                {
                    // Allowed collection contents have changed, so delete any associated items
                    bhlProvider.ItemCollectionDeleteForCollection(collection.CollectionID);
                }
                collection.CanContainTitles = canContainTitles;
                collection.CanContainItems = canContainItems;

                if (chkBHL.Checked && chkITunes.Checked) collection.CollectionTarget = "All";
                if (chkBHL.Checked && !chkITunes.Checked) collection.CollectionTarget = "BHL";
                if (!chkBHL.Checked && chkITunes.Checked) collection.CollectionTarget = "iTunes";
                if (!chkBHL.Checked && !chkITunes.Checked) collection.CollectionTarget = "";

                if (iTunesImageUpload.HasFile)
                {
                    string iTunesImagePath = string.Format(ConfigurationManager.AppSettings["iTunesImagePath"], iTunesImageUpload.FileName);
                    string iTunesImageUploadPath = string.Format(ConfigurationManager.AppSettings["iTunesImageUploadPath"], iTunesImageUpload.FileName);
                    if (collection.ITunesImageURL != iTunesImagePath)
                    {
                        collection.ITunesImageURL = iTunesImagePath;
                        lbliTunesImageUrl.Text = iTunesImagePath;
                        MOBOT.FileAccess.IFileAccessProvider fileAccessProvider = bhlProvider.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
                        fileAccessProvider.SaveFile(iTunesImageUpload.FileBytes, iTunesImageUploadPath);
                    }
                }

                // If this collection is designated for iTunesU, then remove the auto-add criteria
                if (chkITunes.Checked)
                {
                    collection.InstitutionCode = null;
                    collection.LanguageCode = null;
                }

                if (imageUpload.HasFile)
                {
                    string imagePath = string.Format(ConfigurationManager.AppSettings["CollectionImagePath"], imageUpload.FileName);
                    string imageUploadPath = string.Format(ConfigurationManager.AppSettings["CollectionImageUploadPath"], imageUpload.FileName);
                    if (collection.ImageURL != imagePath)
                    {
                        collection.ImageURL = imagePath;
                        lblImageUrl.Text = imagePath;
                        MOBOT.FileAccess.IFileAccessProvider fileAccessProvider = bhlProvider.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
                        fileAccessProvider.SaveFile(imageUpload.FileBytes, imageUploadPath);
                    }
                }

                collection.Active = (short)(chkActive.Checked ? 1 : 0);
                collection.Featured = (short)(chkFeatured.Checked ? 1 : 0);
                collection.IsNew = false;

                bhlProvider.SaveCollection(collection);
            }
            else
            {
                return;
            }

            litMessage.Text = "<span class='liveData'>Item Saved.</span>";
            //Response.Redirect("/");
        }

        protected void saveAsNewButton_Click(object sender, EventArgs e)
        {
            if (validate(true))
            {
                Collection collection = new Collection();
                collection.CollectionName = txtCollectionName.Text.Trim();
                collection.CollectionDescription = txtCollectionDescription.Text.Trim();
                collection.CollectionURL = txtCollectionURL.Text;
                collection.HtmlContent = txtHtml.Value.Trim();
                collection.CanContainTitles = (short)(rdoContents.SelectedValue == "T" ? 1 : 0);
                collection.CanContainItems = (short)(rdoContents.SelectedValue == "I" ? 1 : 0);
                collection.InstitutionCode = (ddlInstitution.SelectedValue == string.Empty ? null : ddlInstitution.SelectedValue);
                collection.LanguageCode = (ddlLanguage.SelectedValue == string.Empty ? null : ddlLanguage.SelectedValue);
                collection.Active = (short)(chkActive.Checked ? 1 : 0);
                collection.Featured = (short)(chkFeatured.Checked ? 1 : 0);
                collection.IsNew = true;

                if (imageUpload.HasFile)
                {
                    string imagePath = string.Format(ConfigurationManager.AppSettings["CollectionImagePath"], imageUpload.FileName);
                    string imageUploadPath = string.Format(ConfigurationManager.AppSettings["CollectionImageUploadPath"], imageUpload.FileName);
                    collection.ImageURL = imagePath;
                    lblImageUrl.Text = imagePath;
                    MOBOT.FileAccess.IFileAccessProvider fileAccessProvider = bhlProvider.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
                    fileAccessProvider.SaveFile(imageUpload.FileBytes, imageUploadPath);
                }

                if (iTunesImageUpload.HasFile)
                {
                    string iTunesImagePath = string.Format(ConfigurationManager.AppSettings["iTunesImagePath"], iTunesImageUpload.FileName);
                    string iTunesImageUploadPath = string.Format(ConfigurationManager.AppSettings["iTunesImageUploadPath"], iTunesImageUpload.FileName);
                    collection.ITunesImageURL = iTunesImagePath;
                    lbliTunesImageUrl.Text = iTunesImagePath;
                    MOBOT.FileAccess.IFileAccessProvider fileAccessProvider = bhlProvider.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
                    fileAccessProvider.SaveFile(iTunesImageUpload.FileBytes, iTunesImageUploadPath);
                }

                bhlProvider.SaveCollection(collection);


                // Update the drop-down list of collections
                populateCollectionList();

                // Find the id of the just-inserted collection
                string selectedValue = string.Empty;
                foreach (Collection c in (CustomGenericList<Collection>)ddlCollections.DataSource)
                {
                    if (c.CollectionName == txtCollectionName.Text.Trim() &&
                        c.CanContainTitles == (short)(rdoContents.SelectedValue == "T" ? 1 : 0) &&
                        c.CanContainItems == (short)(rdoContents.SelectedValue == "I" ? 1 : 0))
                        selectedValue = c.CollectionID.ToString();
                }

                lblID.Text = selectedValue;
                ddlCollections.SelectedValue = selectedValue;

                contentsButton.Enabled = true;
                bulkAddButton.Enabled = true;
                refreshContentsButton.Enabled = true;
                clearContentsButton.Enabled = true;

                // Disable fields that cannot be changed after the new collection is saved
                ddlInstitution.Enabled = false;
                ddlLanguage.Enabled = false;
                rdoContents.Enabled = false;

                populateForm();
            }
            else
            {
                return;
            }

            litMessage.Text = "<span class='liveData'>New Item Saved.</span>";
            //Response.Redirect("/");
        }

        protected void clearButton_Click(object sender, EventArgs e)
        {
            clearForm(this.Controls);
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            bhlProvider.DeleteCollection(int.Parse(ddlCollections.SelectedValue));
            populateCollectionList();
            ddlCollections.SelectedIndex = 0;
            lblID.Text = "";
            populateForm();
        }

        protected void ddlCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateForm();
        }

        protected void contentsButton_Click(object sender, EventArgs e)
        {
            if (contentsButton.Text.Equals("Show Contents"))
            {
                int collectionID = int.Parse(ddlCollections.SelectedValue);

                // search titles
                CustomGenericList<Title> titles = bhlProvider.TitleSelectByCollection(collectionID);
                gvwTitles.DataSource = titles;
                gvwTitles.DataBind();

                // search items
                CustomGenericList<Item> items = bhlProvider.ItemSelectByCollection(collectionID);
                gvwItems.DataSource = items;
                gvwItems.DataBind();

                contentPanel.Visible = true;
                contentsButton.Text = "Hide Contents";
            }
            else
            {
                contentPanel.Visible = false;
                contentsButton.Text = "Show Contents";
            }
        }

        protected void clearContentsButton_Click(object sender, EventArgs e)
        {
            int collectionID = int.Parse(ddlCollections.SelectedValue);
            if (collectionID > 0)
            {
                if (rdoContents.SelectedValue == "T")
                {
                    bhlProvider.TitleCollectionDeleteForCollection(collectionID);
                }
                else
                {
                    bhlProvider.ItemCollectionDeleteForCollection(collectionID);
                }
                populateForm();
            }
        }

        protected void refreshContentsButton_Click(object sender, EventArgs e)
        {
            int collectionID = int.Parse(ddlCollections.SelectedValue);
            if (collectionID > 0)
            {
                if (rdoContents.SelectedValue == "T")
                {
                    bhlProvider.TitleCollectionInsertTitlesForCollection(collectionID);
                }
                else
                {
                    bhlProvider.ItemCollectionInsertItemsForCollection(collectionID);
                }
                populateForm();
            }
        }

        protected void bulkAddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CollectionBulkAdd.aspx?id=" + ddlCollections.SelectedValue, true);
        }

        #endregion Event handlers

    }
}
