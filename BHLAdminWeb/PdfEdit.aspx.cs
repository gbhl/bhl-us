using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using System.Globalization;

namespace MOBOT.BHL.AdminWeb
{
    public partial class PdfEdit : System.Web.UI.Page
    {
        private string weeklyStatsLinkTemplate = "/PdfWeeklyStats.aspx?y={0}&w={1}&s={2}&sn={3}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCombos();

                string idString = Request.QueryString["id"];
                int id = 0;
                if (idString != null && int.TryParse(idString, out id))
                {
                    pdfIdTextBox.Text = id.ToString();
                    search(id);
                }
                else
                {
                    // TODO: Inform user that pdf does not exist -- Perhaps redirect to unknown.aspx?type=title
                }
            }

            errorControl.Visible = false;

            Page.SetFocus(articleTitleTextBox);
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            int pdfId = 0;
            if (int.TryParse(pdfIdTextBox.Text.Trim(), out pdfId))
            {
                search(pdfId);
            }
        }

        private void search(int id)
        {
            BHLProvider bp = new BHLProvider();
            PDF pdf = bp.PDFSelectAuto(id);
            Session["Pdf" + id.ToString()] = pdf;
            fillUI(id);
        }

        private void fillUI(int id)
        {
            PDF pdf = (PDF)Session["Pdf" + id.ToString()];

            if (pdf != null)
            {
                pdfIdLabel.Text = pdf.PdfID.ToString();
                hypItemID.Text = pdf.ItemID.ToString();
                hypItemID.NavigateUrl = "/ItemEdit.aspx?id=" + pdf.ItemID.ToString();
                emailAddressLabel.Text = pdf.EmailAddress;
                shareWithLabel.Text = pdf.ShareWithEmailAddresses;
                includeOCRLabel.Text = (pdf.ImagesOnly ? "No" : "Yes");
                articleTitleTextBox.Text = pdf.ArticleTitle;
                articleAuthorsTextBox.Text = pdf.ArticleCreators;
                articleSubjectsTextBox.Text = pdf.ArticleTags;
                fileLocationLabel.Text = pdf.FileLocation;
                hypFileUrl.Text = pdf.FileUrl;
                hypFileUrl.NavigateUrl = pdf.FileUrl;
                creationDateLabel.Text = pdf.CreationDate.ToShortDateString() + " " + pdf.CreationDate.ToShortTimeString();
                fileGenerationDateLabel.Text = pdf.FileGenerationDate.ToString();
                if (pdf.FileGenerationDate != null)
                {
                    TimeSpan ts = Convert.ToDateTime(pdf.FileGenerationDate) - pdf.CreationDate;
                    timeToGenerateLabel.Text = ((ts.Days * 1440) + (ts.Hours * 60) + ts.Minutes).ToString();
                }
                fileDeletionDateLabel.Text = pdf.FileDeletionDate.ToString();
                missingImagesLabel.Text = pdf.NumberImagesMissing.ToString();
                missingOCRLabel.Text = pdf.NumberOcrMissing.ToString();
                ddlPdfStatus.SelectedValue = pdf.PdfStatusID.ToString();
                commentTextBox.Text = pdf.Comment;

                gvPages.DataSource = new BHLProvider().PDFPageSelectForPdfID(pdf.PdfID);
                gvPages.DataBind();

                weeklyStatsLink.HRef = GetWeeklyStatsLink();
                weeklyStatsLink.Visible = true;

                editHistoryControl.EntityName = "pdf";
                editHistoryControl.EntityId = pdf.PdfID.ToString();
            }
            else
            {
                weeklyStatsLink.Visible = false;
            }

        }

        private string GetWeeklyStatsLink()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime dt = Convert.ToDateTime(creationDateLabel.Text);
            System.Globalization.Calendar cal = dfi.Calendar;
            int year = dt.Year;
            int week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int statusID = Convert.ToInt32(ddlPdfStatus.SelectedValue);
            string statusName = ddlPdfStatus.SelectedItem.Text;
            return string.Format(weeklyStatsLinkTemplate,
                year.ToString(), week.ToString(), statusID.ToString(), statusName);
        }

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            CustomGenericList<PDFStatus> pdfStatuses = bp.PDFStatusSelectAll();

            ddlPdfStatus.DataSource = pdfStatuses;
            ddlPdfStatus.DataTextField = "PdfStatusName";
            ddlPdfStatus.DataValueField = "PdfStatusID";
            ddlPdfStatus.DataBind();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            PDF pdf = (PDF)Session["Pdf" + pdfIdLabel.Text];

            // Gather up data on form
            pdf.ArticleTitle = articleTitleTextBox.Text;
            pdf.ArticleCreators = articleAuthorsTextBox.Text;
            pdf.ArticleTags = articleSubjectsTextBox.Text;
            pdf.Comment = commentTextBox.Text;
            pdf.PdfStatusID = int.Parse(ddlPdfStatus.SelectedValue);
            pdf.IsNew = false;

            BHLProvider bp = new BHLProvider();
            // Don't catch... allow global error handler to take over
            //try
            //{
                bp.PDFSave(pdf);
            //}
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex;
            //    Response.Redirect("/Error.aspx");
            //}

            Response.Redirect(GetWeeklyStatsLink());
        }
    }
}
