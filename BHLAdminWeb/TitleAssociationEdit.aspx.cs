using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleAssociationEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectTitle", "<script language='javascript'>function selectTitle(titleId) { document.getElementById('" + selectedTitle.ClientID + "').value=titleId; overlay(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearAssoc", "<script language='javascript'>function clearAssociatedTitle() { document.getElementById('spnAssociatedTitle').innerHTML='Not associated';document.getElementById('" + hidAssociatedTitleID.ClientID + "').value=''; }</script>");

            if (!IsPostBack)
            {
                String titleIDString = Request.QueryString["tid"] as String;
                String associationIDString = Request.QueryString["id"] as String;
                String editType = Request.QueryString["type"] as String;

                int titleID = 0;
                int associationID = 0;
                if (associationIDString != null && int.TryParse(associationIDString, out associationID) &&
                    titleIDString != null && int.TryParse(titleIDString, out titleID))
                {
                    if (associationID == 0 && editType == "new") addTitleAssociation(titleID);
                    fillCombos();
                    fillUI(associationID, titleID);
                }
                else
                {
                    // TODO: Inform user that title association does not exist -- Perhaps redirect to unknown.aspx?type=title
                }
            }
            else
            {
                String selectedTitleId = this.selectedTitle.Value;
                if (selectedTitleId != "")
                {
                    // Get details for "selectedTitleId" from database
                    BHLProvider provider = new BHLProvider();
                    Title title = provider.TitleSelect(Convert.ToInt32(selectedTitleId));
                    hidAssociatedTitleID.Value = selectedTitleId;
                    litAssociatedTitle.Text = selectedTitleId + ": " + title.ShortTitle;
                    this.selectedTitle.Value = "";
                }
            }

            errorControl.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
            Page.SetFocus(ddlType);
        }

        private void addTitleAssociation(int titleID)
        {
            Title title = (Title)Session["Title" + titleID.ToString()];
            TitleAssociation ta = new TitleAssociation();
            ta.TitleID = title.TitleID;
            ta.Active = true;
            ta.IsNew = true;
            title.TitleAssociations.Add(ta);
        }

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            CustomGenericList<TitleAssociationType> types = bp.TitleAssociationTypeSelectAll();

            foreach (TitleAssociationType type in types)
            {
                type.TitleAssociationName = type.TitleAssociationName + " (MARC " + (type.MARCTag + " " + type.MARCIndicator2).Trim() + ")";
            }
            
            TitleAssociationType emptyType = new TitleAssociationType();
            emptyType.TitleAssociationTypeID = 0;
            emptyType.TitleAssociationName = "";
            types.Insert(0, emptyType);

            ddlType.DataSource = types;
            ddlType.DataTextField = "TitleAssociationName";
            ddlType.DataValueField = "TitleAssociationTypeID";
            ddlType.DataBind();
        }

        private void fillUI(int id, int titleID)
        {
            //Session["TitleAssociation"] = id;
            hidTitleID.Value = titleID.ToString();
            hidTitleAssociationID.Value = id.ToString();

            TitleAssociation association = this.FindTitleAssociation(id);

            if (association != null)
            {
                if (association.TitleAssociationTypeID != 0)
                {
                    ddlType.SelectedValue = ddlType.Items.FindByValue(association.TitleAssociationTypeID.ToString()).Value;
                }
                else
                {
                    ddlType.SelectedIndex = 0;
                }
                titleTextBox.Text = association.Title;
                sectionTextBox.Text = association.Section;
                volumeTextBox.Text = association.Volume;
                headingTextBox.Text = association.Heading;
                publicationTextBox.Text = association.Publication;
                relationshipTextBox.Text = association.Relationship;
                activeCheckBox.Checked = association.Active;

                if (association.AssociatedTitleID != null)
                {
                    Title title = new BHLProvider().TitleSelectAuto((int)association.AssociatedTitleID);
                    litAssociatedTitle.Text = association.AssociatedTitleID.ToString() + ": " + title.ShortTitle;
                    hidAssociatedTitleID.Value = association.AssociatedTitleID.ToString();
                }

                this.bindTitleIdentifierData();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            //int titleAssociationId = (int)Session["TitleAssociation"];
            int titleAssociationId = Convert.ToInt32(hidTitleAssociationID.Value);

            if (validate())
            {
                TitleAssociation association = this.FindTitleAssociation(titleAssociationId);

                // Gather up data on form
                association.TitleAssociationTypeID = Convert.ToInt32(ddlType.SelectedValue);
                association.TitleAssociationName = ddlType.SelectedItem.Text;
                association.Title = titleTextBox.Text.Trim();
                association.Section = sectionTextBox.Text.Trim();
                association.Volume = volumeTextBox.Text.Trim();
                association.Heading = headingTextBox.Text.Trim();
                association.Publication = publicationTextBox.Text.Trim();
                association.Relationship = relationshipTextBox.Text.Trim();
                if (hidAssociatedTitleID.Value.Trim() == String.Empty)
                    association.AssociatedTitleID = null;
                else 
                    association.AssociatedTitleID = Convert.ToInt32(hidAssociatedTitleID.Value);
                association.Active = activeCheckBox.Checked;
                association.IsNew = (association.TitleAssociationID == 0);

                // Forces deletes to happen first
                association.TitleAssociationIdentifiers.Sort(SortOrder.Descending, "IsDeleted");
            }
            else
            {
                return;
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptDone", "<script language='javascript'>parent.updateAssociations();</script>");
        }

        /// <summary>
        /// Get the title association from the Title stored in the Session object
        /// </summary>
        /// <param name="titleAssociationId"></param>
        /// <returns></returns>
        private TitleAssociation FindTitleAssociation(int titleAssociationId)
        {
            TitleAssociation titleAssociation = null;

            Title title = (Title)Session["Title" + hidTitleID.Value];
            if (title != null)
            {
                foreach (TitleAssociation ta in title.TitleAssociations)
                {
                    if (ta.IsDeleted)
                    {
                        continue;
                    }
                    if (titleAssociationId == 0 && ta.TitleAssociationID == 0)
                    {
                        titleAssociation = ta;
                        break;
                    }
                    else if (titleAssociationId > 0 && ta.TitleAssociationID == titleAssociationId)
                    {
                        titleAssociation = ta;
                        break;
                    }
                }
            }

            return titleAssociation;
        }

        private bool validate()
        {
            bool flag = false;

            if (ddlType.SelectedValue == "0")
            {
                flag = true;
                errorControl.AddErrorText("Please select an association Type.");
            }

            // Check that all edits were completed
            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            errorControl.Visible = flag;
            Page.MaintainScrollPositionOnPostBack = !flag;

            return !flag;
        }

        #region IdentifiersList Events

        protected void identifiersList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            identifiersList.EditIndex = e.NewEditIndex;
            bindTitleIdentifierData();
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
                    //int titleAssociationId = (int)Session["TitleAssociation"];
                    int titleAssociationId = Convert.ToInt32(hidTitleAssociationID.Value);
                    TitleAssociation association = this.FindTitleAssociation(titleAssociationId);

                    TitleAssociation_TitleIdentifier taTitleIdentifier = FindTitleAssociation_TitleIdentifier(
                        association.TitleAssociationIdentifiers,
                        (int)identifiersList.DataKeys[e.RowIndex].Values[0],
                        (int)identifiersList.DataKeys[e.RowIndex].Values[1],
                        identifiersList.DataKeys[e.RowIndex].Values[2].ToString());

                    int titleIdentifierId = int.Parse(ddlIdentifierName.SelectedValue);
                    String identifierValue = txtIdentifierValue.Text;

                    taTitleIdentifier.TitleAssociationID = association.TitleAssociationID;
                    taTitleIdentifier.TitleIdentifierID = titleIdentifierId;
                    taTitleIdentifier.IdentifierName = ddlIdentifierName.SelectedItem.Text;
                    taTitleIdentifier.IdentifierValue = identifierValue;
                }
            }

            identifiersList.EditIndex = -1;
            bindTitleIdentifierData();
        }

        protected void identifiersList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            identifiersList.EditIndex = -1;
            bindTitleIdentifierData();
        }

        protected void addTitleIdentifierButton_Click(object sender, EventArgs e)
        {
            //int titleAssociationId = (int)Session["TitleAssociation"];
            int titleAssociationId = Convert.ToInt32(hidTitleAssociationID.Value);
            TitleAssociation association = this.FindTitleAssociation(titleAssociationId);

            TitleAssociation_TitleIdentifier ti = new TitleAssociation_TitleIdentifier();
            ti.TitleAssociationID = association.TitleAssociationID;
            association.TitleAssociationIdentifiers.Add(ti);
            identifiersList.EditIndex = identifiersList.Rows.Count;
            bindTitleIdentifierData();
        }

        protected void identifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());

                //int titleAssociationId = (int)Session["TitleAssociation"];
                int titleAssociationId = Convert.ToInt32(hidTitleAssociationID.Value);
                TitleAssociation association = this.FindTitleAssociation(titleAssociationId);

                TitleAssociation_TitleIdentifier taTitleIdentifier = FindTitleAssociation_TitleIdentifier(
                    association.TitleAssociationIdentifiers,
                    (int)identifiersList.DataKeys[rowNum].Values[0],
                    (int)identifiersList.DataKeys[rowNum].Values[1],
                    identifiersList.DataKeys[rowNum].Values[2].ToString());

                taTitleIdentifier.IsDeleted = true;
                identifiersList.EditIndex = -1;
                bindTitleIdentifierData();
            }
        }

        #endregion IdentifiersList Events

        #region Identifier methods

        CustomGenericList<Identifier> _identifiers = null;
        protected CustomGenericList<Identifier> GetIdentifiers()
        {
            BHLProvider bp = new BHLProvider();
            _identifiers = bp.IdentifierSelectAll();

            return _identifiers;
        }

        protected int GetIdentifierIndex(object dataItem)
        {
            string identifierIdString = DataBinder.Eval(dataItem, "TitleIdentifierID").ToString();

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

        private void bindTitleIdentifierData()
        {
            //int titleAssociationId = (int)Session["TitleAssociation"];
            int titleAssociationId = Convert.ToInt32(hidTitleAssociationID.Value);
            TitleAssociation association = this.FindTitleAssociation(titleAssociationId);

            // filter out deleted items
            CustomGenericList<TitleAssociation_TitleIdentifier> titleIdentifiers = new CustomGenericList<TitleAssociation_TitleIdentifier>();
            foreach (TitleAssociation_TitleIdentifier ti in association.TitleAssociationIdentifiers)
            {
                if (ti.IsDeleted == false)
                {
                    titleIdentifiers.Add(ti);
                }
            }

            identifiersList.DataSource = titleIdentifiers;
            identifiersList.DataBind();
        }

        private TitleAssociation_TitleIdentifier FindTitleAssociation_TitleIdentifier(
            CustomGenericList<TitleAssociation_TitleIdentifier> titleTitleIdentifiers,
            int titleAssociationTitleIdentifierId, int titleIdentifierID, string identifierValue)
        {
            foreach (TitleAssociation_TitleIdentifier tati in titleTitleIdentifiers)
            {
                if (tati.IsDeleted)
                {
                    continue;
                }
                if (titleAssociationTitleIdentifierId == 0 && tati.TitleAssociation_TitleIdentifierID == 0 &&
                    titleIdentifierID == tati.TitleIdentifierID &&
                    identifierValue == tati.IdentifierValue)
                {
                    return tati;
                }
                else if (titleAssociationTitleIdentifierId > 0 && tati.TitleAssociation_TitleIdentifierID == titleAssociationTitleIdentifierId)
                {
                    return tati;
                }
            }

            return null;
        }

        #endregion TitleIdentifier methods

    }
}
