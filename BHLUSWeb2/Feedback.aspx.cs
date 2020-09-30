using Countersoft.Gemini.Api;
using Countersoft.Gemini.Commons.Dto;
using Countersoft.Gemini.Commons.Entity;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2
{
    public partial class Feedback : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCombos();

                if (Page.PreviousPage != null)
                {
                    string previousPageName = Page.PreviousPage.AppRelativeVirtualPath.ToLower();
                    if (previousPageName.Contains("bibliography"))
                    {
                        HiddenField titleIDField = (HiddenField)this.myFindControl(Page.PreviousPage.Controls, "hidTitleID");
                        if (titleIDField.Value != string.Empty) ViewState["TitleID"] = titleIDField.Value;
                    }
                    else if (previousPageName.Contains("titlepage"))
                    {
                        HiddenField pageIDField = (HiddenField)this.myFindControl(Page.PreviousPage.Controls, "hidPageID");
                        if (pageIDField.Value != string.Empty) ViewState["PageID"] = pageIDField.Value;
                    }
                }

                ViewState["FeedbackRefererURL"] = (Request.UrlReferrer != null) ? Request.UrlReferrer.ToString() : "/";

                string page = Request.QueryString["page"];
                if (page != null) ViewState["PageID"] = page;

                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Feedback");
            }
        }

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();
            List<Language> languages = bp.LanguageSelectAll();

            srLanguageList.DataSource = languages;
            srLanguageList.DataTextField = "LanguageName";
            srLanguageList.DataValueField = "LanguageCode";
            srLanguageList.DataBind();

            srLanguageList.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Replaces the built-in "FindControl" method by doing a "contains" search instead
        /// of an exact match.
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        protected Control myFindControl(ControlCollection controls, string searchTerm)
        {
            Control found = null;

            foreach (Control control in controls)
            {
                if ((control.ID == null ? "" : control.ID).Contains(searchTerm))
                {
                    found = control;
                }
                else
                {
                    if (control.Controls != null) found = this.myFindControl(control.Controls, searchTerm);
                }
                if (found != null) break;
            }

            return found;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            errorPanel.Visible = false;
            string issueLongDesc = string.Empty;

            // Get Gemini data from web.config file
            string geminiWebServiceURL = ConfigurationManager.AppSettings["GeminiURL"];
            string geminiUserName = ConfigurationManager.AppSettings["GeminiUser"];
            string geminiUserPassword = ConfigurationManager.AppSettings["GeminiPassword"];
            string issueSummary = ConfigurationManager.AppSettings["GeminiDesc"];
            int projectId = int.Parse(ConfigurationManager.AppSettings["GeminiProjectId"]);
            int scanReqComponentId = int.Parse(ConfigurationManager.AppSettings["GeminiComponentIdScanRequest"]);
            int feedbackComponentId = int.Parse(ConfigurationManager.AppSettings["GeminiComponentIdFeedback"]);
            int scanReqTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdScanRequest"]);
            int techFeedTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdTechFeedback"]);
            int suggestTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdSuggestion"]);
            int bibIssueTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdBiblioIssue"]);
            int statusId = int.Parse(ConfigurationManager.AppSettings["GeminiStatusId"]);
            int priorityId = int.Parse(ConfigurationManager.AppSettings["GeminiPriorityId"]);
            int severityId = int.Parse(ConfigurationManager.AppSettings["GeminiSeverityId"]);
            int resolutionId = int.Parse(ConfigurationManager.AppSettings["GeminiResolutionId"]);

            ServiceManager serviceManager = new ServiceManager(geminiWebServiceURL, geminiUserName, geminiUserPassword, "", false);
            UserDto user = serviceManager.Admin.WhoAmI();
            Issue data = new Issue();

            if (subjectScanReq.Checked)
            {
                // Scanning request
                if (srTitleTextBox.Text.Trim().Length > 0)
                {
                    issueSummary = srTitleTextBox.Text.Trim();
                }
                else
                {
                    issueSummary = "Scan Request";
                }
                issueLongDesc = getScanRequest();

                data.AddComponent(scanReqComponentId);  // Collections
                data.TypeId = scanReqTypeId;    // 60=Scan Request
            }
            else
            {
                // Feedback
                if (!string.IsNullOrWhiteSpace(emailTextBox.Text)) issueSummary = emailTextBox.Text.Trim();
                issueLongDesc = getComment();

                data.AddComponent(feedbackComponentId);  // Web-Other
                if (subjectTech.Checked) data.TypeId = techFeedTypeId;    // 22=Tech Issue
                if (subjectSuggest.Checked) data.TypeId = suggestTypeId;    // 36=Suggestion
                if (subjectBibIssue.Checked) data.TypeId = bibIssueTypeId;    // 55=Bib Issue
            }

            data.Description = issueLongDesc;
            data.Title = (issueSummary.Length > 245) ? (issueSummary.Substring(0, 245) + "...") : issueSummary;
            data.PriorityId = priorityId;       // 17=Low, 18=Medium, 19=High
            data.ResolutionId = resolutionId;      // 15=Unresolved
            data.StatusId = statusId;         // 28=Unassigned
            data.SeverityId = severityId;        // 19=Null
            data.ReportedBy = user.Entity.Id;
            data.ProjectId = projectId;

            try
            {
                // Ignore spam from kelev.biz and email.tst.
                // Only a bot can fill Foo with a value, so ignore that as well.
                if (!emailTextBox.Text.Trim().ToLower().Contains("kelev.biz") &&
                    !emailTextBox.Text.Trim().ToLower().Contains("email.tst") &&
                    string.IsNullOrWhiteSpace(fooTextBox.Text.Trim()) &&
                    ValidateCaptcha(Request.Form["g-recaptcha-response"]))
                {
                    IssueDto newIssue = serviceManager.Item.Create(data);

                    string subject = "BHL Feedback (# " + newIssue.Id.ToString() + ") Received";
                    if (subjectScanReq.Checked) subject = "BHL Scanning Request (# " + newIssue.Id.ToString() + ") Received";
                    if (emailTextBox.Text.Trim().Length > 0) this.SendEmail(emailTextBox.Text, subject, Server.HtmlDecode(issueLongDesc));
                    this.ShowConfirmationMessage(subject, Server.HtmlDecode(issueLongDesc));

                    if (subjectScanReq.Checked)
                    {
                        try
                        {
                            var type = typeBook.Checked ? typeBook.Value : typeJournal.Checked ? typeJournal.Value : typeUnsure.Value;
                            new BHLProvider().ScanRequestInsertAuto(newIssue.Id, srTitleTextBox.Text.Trim(),
                                srYearTextBox.Text.Trim(), type, srVolumeTextBox.Text.Trim(),
                                srEditionTextBox.Text.Trim(), srOCLCTextBox.Text.Trim(), srISBNTextBox.Text.Trim(),
                                srISSNTextBox.Text.Trim(), srAuthorTextBox.Text.Trim(), srPublisherTextBox.Text.Trim(),
                                srLanguageList.SelectedItem.Text, srNoteTextBox.Text.Trim());
                        }
                        catch
                        {
                            // Do nothing, we're just catching to prevent database errors from stopping us.
                            // Database insert is a 'nice-to-have', not a necessity.
                        }
                    }
                }
                else
                {
                    errorPanel.Visible = true;
                    errorLabel.Text = "There was a problem sending your comment. Please try again.";
                }
            }
            catch
            {
                errorPanel.Visible = true;
                errorLabel.Text = "There was a problem sending your comment. Your feedback is important to us, we apologize.";
            }
        }

        /// <summary>
        /// Validate the ReCaptcha submission
        /// </summary>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns></returns>
        private bool ValidateCaptcha(string gRecaptchaResponse)
        {
            bool isValid = true;

            string verifyUrl = ConfigurationManager.AppSettings["ReCaptchaVerifyUrl"];
            string secretKey = ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            string verifyParams = string.Format("secret={0}&response={1}", secretKey, gRecaptchaResponse);

            string postResponse = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                postResponse = webClient.UploadString(verifyUrl, verifyParams);
            }

            JObject jsonResponse = null;
            try
            {
                jsonResponse = JObject.Parse(postResponse);
                bool status = (bool)jsonResponse.SelectToken("success");
                if (status != true) isValid = false;
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        private string getComment()
        {
            StringBuilder sb = new StringBuilder();

            if (nameTextBox.Text.Trim().Length > 0)
            {
                sb.Append("<b>Name: </b>");
                sb.Append(Server.HtmlEncode(nameTextBox.Text.Trim()));
            }

            string url = string.Empty;
            if (urlTextBox.Text.Trim().Length > 0)
            {
                // Display the user-supplied URL, if it exists
                url = Server.HtmlEncode(urlTextBox.Text.Trim());
            }
            else if (!string.IsNullOrWhiteSpace((string)ViewState["FeedbackRefererURL"]))
            {
                // Else display the referrer URL
                url = ViewState["FeedbackRefererURL"].ToString();
            }

            if (url.Length > 0)
            {
                sb.Append("<br>");
                sb.Append("<b>URL: </b>");
                sb.Append(url);
            }

            if (ViewState["PageID"] != null)
            {
                sb.Append("<br>");
                sb.Append("<b>Viewed Page: </b>");
                sb.Append(ViewState["PageID"].ToString());
            }

            if (ViewState["TitleID"] != null)
            {
                sb.Append("<br>");
                sb.Append("<b>Viewed Title:</b>");
                sb.Append(ViewState["TitleID"].ToString());
            }

            if (sb.Length > 0) sb.Append("<br><br>");
            sb.Append(Server.HtmlEncode(commentTextBox.Text.Trim()));

            return sb.ToString();
        }

        private string getScanRequest()
        {
            StringBuilder sb = new StringBuilder();

            if (nameTextBox.Text.Trim() != string.Empty)
            {
                sb.Append("<b>Name: </b>");
                sb.Append(Server.HtmlEncode(nameTextBox.Text.Trim()));
            }
            if (emailTextBox.Text.Trim() != string.Empty)
            {
                if (sb.Length > 0) sb.Append("<br>");
                sb.Append("<b>Email: </b>");
                sb.Append(Server.HtmlEncode(emailTextBox.Text.Trim()));
            }
            if (sb.Length > 0) sb.Append("<br><br>");
            sb.Append("<b>Type: </b>");
            sb.Append(typeBook.Checked ? typeBook.Value : typeJournal.Checked ? typeJournal.Value : typeUnsure.Value);
            sb.Append("<br><b>Title: </b>");
            sb.Append(Server.HtmlEncode(srTitleTextBox.Text.Trim()));
            sb.Append("<br><b>Year: </b>");
            sb.Append(Server.HtmlEncode(srYearTextBox.Text.Trim()));
            if (srVolumeTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>Volume: </b>");
                sb.Append(Server.HtmlEncode(srVolumeTextBox.Text.Trim()));
            }
            if (srEditionTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>Edition: </b>");
                sb.Append(Server.HtmlEncode(srEditionTextBox.Text.Trim()));
            }
            if (srOCLCTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>OCLC: </b>");
                sb.Append(Server.HtmlEncode(srOCLCTextBox.Text.Trim()));
            }
            if (srISBNTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>ISBN: </b>");
                sb.Append(Server.HtmlEncode(srISBNTextBox.Text.Trim()));
            }
            if (srISSNTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>ISSN: </b>");
                sb.Append(Server.HtmlEncode(srISSNTextBox.Text.Trim()));
            }
            if (srAuthorTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>Author: </b>");
                sb.Append(Server.HtmlEncode(srAuthorTextBox.Text.Trim()));
            }
            if (srPublisherTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><b>Publisher: </b>");
                sb.Append(Server.HtmlEncode(srPublisherTextBox.Text.Trim()));
            }
            if (srLanguageList.SelectedValue != String.Empty)
            {
                sb.Append("<br><b>Language: </b>");
                sb.Append(srLanguageList.SelectedItem.Text);
            }
            if (srNoteTextBox.Text.Trim() != String.Empty)
            {
                sb.Append("<br><br><b>Note: </b>");
                sb.Append(Server.HtmlEncode(srNoteTextBox.Text.Trim()));
            }

            return sb.ToString();
        }

        private string GetReceivedMessage()
        {
            string alertMessage = String.Empty;

            try
            {
                alertMessage = System.IO.File.ReadAllText(Request.PhysicalApplicationPath + "\\feedbackmsg.txt");
            }
            catch
            {
                // do nothing if file missing... just return the empty string
            }

            return alertMessage;
        }

        private string CleanStringForEmail(string message)
        {
            message = message.Replace("<br>", "\n");
            message = message.Replace("<b>", "");
            message = message.Replace("</b>", "");
            return message;
        }

        private void SendEmail(String recipient, String subject, String feedbackReceived)
        {
            try
            {
                MOBOT.BHL.Web2.SiteService.ArrayOfString recipients = new MOBOT.BHL.Web2.SiteService.ArrayOfString();
                recipients.Add(recipient);
                string message = this.GetReceivedMessage();
                string faqLink = System.Configuration.ConfigurationManager.AppSettings["WikiPageFAQ"];
                message = message.Replace("[FAQLink]", faqLink);
                message = message.Replace("[Feedback]", this.CleanStringForEmail(feedbackReceived));
                if (message != String.Empty)
                {
                    SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                    service.SendEmail("noreply@biodiversitylibrary.org", recipients, null, null,
                        subject, message);
                }
            }
            catch
            {
                // Do nothing if email fails
            }
        }

        private void ShowConfirmationMessage(string subject, string feedbackReceived)
        {
            divSubmit.Visible = false;
            divConfirm.Visible = true;
            litConfirmationSubject.Text = subject;
            litConfirmationText.Text = feedbackReceived;
        }

    }
}
