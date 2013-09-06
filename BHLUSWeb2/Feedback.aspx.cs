using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using CounterSoft.Gemini.Commons.Entity;
using CounterSoft.Gemini.WebServices;
using CounterSoft.Gemini.Commons;

namespace MOBOT.BHL.Web2
{
    public partial class Feedback : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.registerValidationScripts();

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
                if (page != null)
                {
                    ViewState["PageID"] = page;
                }

                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Feedback");
            //To do    ((Main)Page.Master).SetTweetMessage(String.Format(ConfigurationManager.AppSettings["TweetMessage"], "Feedback"));
            }
        }

        private void registerValidationScripts()
        {
            // Define the name and type of the client scripts on the page.
            String csname1 = "FeedbackValidationScript";
            String csname2 = "ScanRequestValidationScript";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsClientScriptBlockRegistered(cstype, csname1))
            {
                StringBuilder cstext1 = new StringBuilder();

                cstext1.Append("function ValidateFeedbackForm() {\n");
                cstext1.Append("var errMsg = '';\n");
                cstext1.Append("if (document.getElementById('" + this.ddlList.ClientID + "').selectedIndex == 0) errMsg = errMsg + 'Please choose a subject.<br>';\n");
                cstext1.Append("if (document.getElementById('" + this.commentTextBox.ClientID + "').value == '') errMsg = errMsg + 'Please supply a comment.<br>';\n");
                cstext1.Append("if (errMsg != '') document.getElementById('spanErrorText').innerHTML = errMsg;");
                cstext1.Append("return (errMsg == '');\n");
                cstext1.Append("}\n");

                cs.RegisterClientScriptBlock(cstype, csname1, cstext1.ToString(), true);
            }

            if (!cs.IsClientScriptBlockRegistered(cstype, csname2))
            {
                StringBuilder cstext2 = new StringBuilder();

                cstext2.Append("function ValidateRequestForm() {\n");
                cstext2.Append("var errMsg = '';\n");
                cstext2.Append("if (document.getElementById('" + this.srNameTextBox.ClientID + "').value == '') errMsg = errMsg + 'Please supply your name.<br>';\n");
                cstext2.Append("if (document.getElementById('" + this.srEmailTextBox.ClientID + "').value == '') errMsg = errMsg + 'Please supply your email address.<br>';\n");
                cstext2.Append("if (document.getElementById('" + this.srTitleTextBox.ClientID + "').value == '') errMsg = errMsg + 'Please supply the book title.<br>';\n");
                cstext2.Append("if (document.getElementById('" + this.srYearTextBox.ClientID + "').value == '') errMsg = errMsg + 'Please supply the year of publication.<br>';\n");
                cstext2.Append("var type = document.getElementById('" + this.srTypeList.ClientID + "');\n");
                cstext2.Append("if (document.getElementById('" + this.srVolumeTextBox.ClientID + "').value == '' && type.options[type.selectedIndex].text == 'Journal') errMsg = errMsg + 'Please supply the volume.<br>';\n");
                cstext2.Append("if (errMsg != '') document.getElementById('srSpanErrorText').innerHTML = errMsg;");
                cstext2.Append("return (errMsg == '');\n");
                cstext2.Append("}\n");

                cs.RegisterClientScriptBlock(cstype, csname2, cstext2.ToString(), true);
            }
        }

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();
            CustomGenericList<Language> languages = bp.LanguageSelectAll();

            srLanguageList.DataSource = languages;
            srLanguageList.DataTextField = "LanguageName";
            srLanguageList.DataValueField = "LanguageCode";
            srLanguageList.DataBind();
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
            // Get Gemini data from web.config file
            string geminiWebServiceURL = ConfigurationManager.AppSettings.GetValues("GeminiURL")[0];
            string geminiUserName = ConfigurationManager.AppSettings.GetValues("GeminiUser")[0];
            string geminiUserPassword = ConfigurationManager.AppSettings.GetValues("GeminiPassword")[0];
            string issueSummary = ConfigurationManager.AppSettings.GetValues("GeminiDesc")[0];
            if (emailTextBox.Text.Trim().Length > 0)
            {
                issueSummary = emailTextBox.Text.Trim();
            }
            int projectId = int.Parse(ConfigurationManager.AppSettings.GetValues("GeminiProjectId")[0]);
            string issueLongDesc = getComment();

            ServiceManager serviceManager = new ServiceManager(geminiWebServiceURL, geminiUserName, geminiUserPassword, "", false);
            UserEN user = serviceManager.UsersService.WhoAmI();
            IssueEN data = new IssueEN();

            IssueComponentEN[] iceComps = new IssueComponentEN[1];
            iceComps[0] = new IssueComponentEN();
            iceComps[0].ComponentID = 56;	// Web-Other
            data.Components = iceComps;

            data.IssueLongDesc = issueLongDesc;
            data.IssuePriority = 1;		    // 1=Trivial, 2=Minor, 3=Major, 4=Show Stopper
            data.IssueResolution = 1;	    // 1=Unresolved
            data.IssueStatus = 1;			// 1=Unassigned
            data.IssueSeverity = 1;
            data.IssueSummary = issueSummary;
            data.IssueType = int.Parse(ddlList.SelectedValue); // 1=Technical Issues, 5=Suggestion, 6=Bibliographic Issues
            data.ReportedBy = user.UserID;
            data.RiskLevel = 1;
            data.ProjectID = projectId;

            try
            {
                if (!emailTextBox.Text.Trim().ToLower().Contains("kelev.biz"))  // ignore spam from kelev.biz
                {
                    data = serviceManager.IssuesService.CreateIssue(data);
                    if (emailTextBox.Text.Trim().Length > 0) this.SendEmail(emailTextBox.Text, "BHL Feedback Received", Server.HtmlDecode(issueLongDesc));
                }

                string returnUrl = createReturnUrl();
                if (returnUrl.ToLower().Contains("/contact"))
                {
                    rcvdMsg.Style.Add("display", "block");
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            catch
            {
                errorPanel.Visible = true;
                errorLabel.Text = "There was a problem sending your comment. Your feedback is important to us, we apologize.";
            }
        }

        protected void srSubmitButton_Click(object sender, EventArgs e)
        {
            // Get Gemini data from web.config file
            string geminiWebServiceURL = ConfigurationManager.AppSettings.GetValues("GeminiURL")[0];
            string geminiUserName = ConfigurationManager.AppSettings.GetValues("GeminiUser")[0];
            string geminiUserPassword = ConfigurationManager.AppSettings.GetValues("GeminiPassword")[0];
            string issueSummary = ConfigurationManager.AppSettings.GetValues("GeminiDesc")[0];
            if (srEmailTextBox.Text.Trim().Length > 0)
            {
                issueSummary = "Scan Request from " + srEmailTextBox.Text.Trim();
            }
            int projectId = int.Parse(ConfigurationManager.AppSettings.GetValues("GeminiProjectId")[0]);
            string issueLongDesc = getScanRequest();

            ServiceManager serviceManager = new ServiceManager(geminiWebServiceURL, geminiUserName, geminiUserPassword, "", false);
            UserEN user = serviceManager.UsersService.WhoAmI();
            IssueEN data = new IssueEN();

            IssueComponentEN[] iceComps = new IssueComponentEN[1];
            iceComps[0] = new IssueComponentEN();
            iceComps[0].ComponentID = 78;	// Collections
            data.Components = iceComps;

            data.IssueLongDesc = issueLongDesc;
            data.IssuePriority = 1;		    // 1=Trivial, 2=Minor, 3=Major, 4=Show Stopper
            data.IssueResolution = 1;	    // 1=Unresolved
            data.IssueStatus = 1;			// 1=Unassigned
            data.IssueSeverity = 1;
            data.IssueSummary = issueSummary;
            data.IssueType = 6;             // 6=Bibliographic Issue
            data.ReportedBy = user.UserID;
            data.RiskLevel = 1;
            data.ProjectID = projectId;

            try
            {
                if (!srEmailTextBox.Text.Trim().ToLower().Contains("kelev.biz"))  // ignore spam from kelev.biz
                {
                    data = serviceManager.IssuesService.CreateIssue(data);
                    if (srEmailTextBox.Text.Trim().Length > 0) this.SendEmail(srEmailTextBox.Text, "BHL Scanning Request (# " + data.IssueID.ToString() + ") Received", Server.HtmlDecode(issueLongDesc));

                    try
                    {
                        new BHLProvider().ScanRequestInsertAuto(data.IssueID, srTitleTextBox.Text.Trim(),
                            srYearTextBox.Text.Trim(), srTypeList.SelectedValue, srVolumeTextBox.Text.Trim(),
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

                string returnUrl = createReturnUrl();
                if (returnUrl.ToLower().Contains("/contact"))
                {
                    rcvdMsg.Style.Add("display", "block");
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            catch
            {
                errorPanel.Visible = true;
                errorLabel.Text = "There was a problem sending your comment. Your feedback is important to us, we apologize.";
            }
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(createReturnUrl());
        }

        private string createReturnUrl()
        {
            string fburl = ViewState["FeedbackRefererURL"].ToString();

            if (ViewState["TitleID"] != null)
            {
                fburl = String.Format(ConfigurationManager.AppSettings["BibPageUrl"], ViewState["TitleID"].ToString());
            }
            else if (ViewState["PageID"] != null)
            {
                // if it comes from the names page then just redirect to orig fburl
                int x = fburl.IndexOf("/", 10) + 1;
                int y = fburl.IndexOf("/", x);

                if (y > 0)
                {
                    string name = fburl.Substring(x, y - x);
                    if (name.ToLower().Equals("name"))
                    {
                        return fburl;
                    }
                }

                fburl = fburl.Substring(0, fburl.IndexOf('/', 10)) + "/page/" + ViewState["PageID"].ToString();
            }

            return fburl;
        }

        private string getComment()
        {
            StringBuilder sb = new StringBuilder();

            if (nameTextBox.Text.Trim().Length > 0)
            {
                sb.Append("<b>Name: </b>");
                sb.Append(Server.HtmlEncode(nameTextBox.Text.Trim()));
            }

            sb.Append("<br>");
            sb.Append("<b>URL: </b>");
            sb.Append(ViewState["FeedbackRefererURL"].ToString());

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

            if (sb.Length > 0)
            {
                sb.Append("<br><br>");
            }
            sb.Append(Server.HtmlEncode(commentTextBox.Text.Trim()));

            return sb.ToString();
        }

        private string getScanRequest()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<b>Name: </b>");
            sb.Append(Server.HtmlEncode(srNameTextBox.Text.Trim()));
            sb.Append("<br><b>Email: </b>");
            sb.Append(Server.HtmlEncode(srEmailTextBox.Text.Trim()));
            sb.Append("<br><br><b>Type: </b>");
            sb.Append(srTypeList.SelectedValue);
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
                String[] recipients = new String[1];
                recipients[0] = recipient;
                string message = this.GetReceivedMessage();
                message = message.Replace("[Feedback]", this.CleanStringForEmail(feedbackReceived));
                if (message != String.Empty)
                {
                    MOBOT.BHL.Web2.BHLWebService.BHLWSSoapClient service = new MOBOT.BHL.Web2.BHLWebService.BHLWSSoapClient();
                    service.SendEmail("noreply@biodiversitylibrary.org", recipients, null, null,
                        subject, message);
                }

            }
            catch
            {
                // Do nothing if email fails
            }
        }

    }
}
