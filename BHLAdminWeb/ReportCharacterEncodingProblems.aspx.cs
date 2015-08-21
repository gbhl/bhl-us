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
    public partial class ReportCharacterEncodingProblems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider bp = new BHLProvider();

                CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

                Institution emptyInstitution = new Institution();
                emptyInstitution.InstitutionName = "(select institution)";
                emptyInstitution.InstitutionCode = "";
                institutions.Insert(0, emptyInstitution);
                listInstitutions.DataSource = institutions;
                listInstitutions.DataBind();
            }
            else
            {
                spanTitle.Visible = chkTitle.Checked;
                spanSubject.Visible = chkSubject.Checked;
                spanAssociation.Visible = chkAssociation.Checked;
                spanAuthor.Visible = chkAuthor.Checked;
                spanItem.Visible = chkItem.Checked;
            }

            Page.SetFocus(listInstitutions);
            Page.Title = "BHL Admin - Character Encoding Problems";
        }

        protected void buttonShow_Click(object sender, EventArgs e)
        {
            string institutionCode = listInstitutions.SelectedValue;
            int maxAge = Convert.ToInt32(listAddedSince.SelectedValue);

            BHLProvider bp = new BHLProvider();

            // Load gridviews with data
            if (chkTitle.Checked)
            {
                CustomGenericList<TitleSuspectCharacter> titles = bp.TitleSelectWithSuspectCharacters(institutionCode, maxAge);
                gvwTitles.DataSource = titles;
                gvwTitles.DataBind();
                litNoTitles.Visible = (titles.Count == 0) ? true : false;
                divTitle.Visible = !litNoTitles.Visible;
            }
            else
            {
                litNoTitles.Visible = false;
                divTitle.Visible = false;
            }

            if (chkSubject.Checked)
            {
                CustomGenericList<KeywordSuspectCharacter> titleKeywords = bp.KeywordSelectWithSuspectCharacters(institutionCode, maxAge);
                gvwTitleKeywords.DataSource = titleKeywords;
                gvwTitleKeywords.DataBind();
                litNoTitleKeywords.Visible = (titleKeywords.Count == 0) ? true : false;
                divTitleKeyword.Visible = !litNoTitleKeywords.Visible;
            }
            else
            {
                litNoTitleKeywords.Visible = false;
                divTitleKeyword.Visible = false;
            }

            if (chkAssociation.Checked)
            {
                CustomGenericList<TitleAssociationSuspectCharacter> titleAssociations = bp.TitleAssociationSelectWithSuspectCharacters(institutionCode, maxAge);
                gvwAssociations.DataSource = titleAssociations;
                gvwAssociations.DataBind();
                litNoAssociations.Visible = (titleAssociations.Count == 0) ? true : false;
                divAssociation.Visible = !litNoAssociations.Visible;
            }
            else
            {
                litNoAssociations.Visible = false;
                divAssociation.Visible = false;
            }

            if (chkAuthor.Checked)
            {
                CustomGenericList<AuthorSuspectCharacter> authors = bp.AuthorSelectWithSuspectCharacters(institutionCode, maxAge);
                gvwAuthors.DataSource = authors;
                gvwAuthors.DataBind();
                litNoAuthors.Visible = (authors.Count == 0) ? true : false;
                divAuthor.Visible = !litNoAuthors.Visible;
            }
            else
            {
                litNoAuthors.Visible = false;
                divAuthor.Visible = false;
            }

            if (chkItem.Checked)
            {
                CustomGenericList<ItemSuspectCharacter> items = bp.ItemSelectWithSuspectCharacters(institutionCode, maxAge);
                gvwItems.DataSource = items;
                gvwItems.DataBind();
                litNoItems.Visible = (items.Count == 0) ? true : false;
                divItem.Visible = !litNoItems.Visible;
            }
            else
            {
                litNoItems.Visible = false;
                divItem.Visible = false;
            }
        }
    }
}
