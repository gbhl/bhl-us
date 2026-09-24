using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using Countersoft.Gemini.Api;
using Countersoft.Gemini.Commons.Dto;
using Countersoft.Gemini.Commons.Entity;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [EnableThrottling]
        [HttpGet]
        public ActionResult Index()
        {
            ContactModel model = new ContactModel();
            BHLProvider bp = new BHLProvider();

            List<DataObjects.Language> languages = bp.LanguageSelectAll();
            model.LanguageList = new SelectList(languages, "LanguageCode", "LanguageName");
            model.SelectedLanguage = "";
            model.FeedbackRefererURL = (Request.UrlReferrer != null) ? Request.UrlReferrer.AbsoluteUri : "/";
            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], "Feedback");
            return View(model);
        }

        [EnableThrottling]
        [HttpPost]
        public ActionResult Index(ContactModel model)
        {
            BHLProvider bp = new BHLProvider();
            List<DataObjects.Language> languages = bp.LanguageSelectAll();
            model.LanguageList = new SelectList(languages, "LanguageCode", "LanguageName");
            foreach(var language in languages)
            {
                if (language.LanguageCode == model.SelectedLanguage)
                {
                    model.SelectedLanguageName = language.LanguageName;
                    break;
                }
            }

            string issueLongDesc = string.Empty;

            // Get Gemini data from web.config file
            string geminiWebServiceURL = ConfigurationManager.AppSettings["GeminiURL"];
            string geminiUserName = ConfigurationManager.AppSettings["GeminiUser"];
            string geminiUserPassword = ConfigurationManager.AppSettings["GeminiPassword"];
            string issueSummary = ConfigurationManager.AppSettings["GeminiDesc"];
            int projectId = int.Parse(ConfigurationManager.AppSettings["GeminiProjectId"]);
            int scanProjectId = int.Parse(ConfigurationManager.AppSettings["GeminiScanProjectID"]);
            int scanReqComponentId = int.Parse(ConfigurationManager.AppSettings["GeminiComponentIdScanRequest"]);
            int feedbackComponentId = int.Parse(ConfigurationManager.AppSettings["GeminiComponentIdFeedback"]);
            int scanReqTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdScanRequest"]);
            int techFeedTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdTechFeedback"]);
            int suggestTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdSuggestion"]);
            int bibIssueTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdBiblioIssue"]);
            int titleTypeId = int.Parse(ConfigurationManager.AppSettings["GeminiTypeIdTitle"]);
            int statusId = int.Parse(ConfigurationManager.AppSettings["GeminiStatusId"]);
            int priorityId = int.Parse(ConfigurationManager.AppSettings["GeminiPriorityId"]);
            int severityId = int.Parse(ConfigurationManager.AppSettings["GeminiSeverityId"]);
            int resolutionId = int.Parse(ConfigurationManager.AppSettings["GeminiResolutionId"]);
            int requestSourceId = int.Parse(ConfigurationManager.AppSettings["GeminiRequestSourceUserId"]);
            int mailboxId = int.Parse(ConfigurationManager.AppSettings["GeminiMailboxId"]);

            ServiceManager serviceManager = new ServiceManager(geminiWebServiceURL, geminiUserName, geminiUserPassword, "", false);
            UserDto user = serviceManager.Admin.WhoAmI();
            Issue data = new Issue();

            if (model.Subject == 60)    // 60=Scanning Request
            {
                // Scanning request
                if (model.srTitle != null && model.srTitle.Trim().Length > 0)
                {
                    issueSummary = model.srTitle.Trim();
                }
                else
                {
                    issueSummary = "Scan Request";
                }
                issueLongDesc = getScanRequest(model);

                //data.AddComponent(scanReqComponentId);  // Collections
                data.ProjectId = scanProjectId;
                data.TypeId = titleTypeId;    // 80=Title
                data.FixedInVersionId = requestSourceId;    // 22=User Request
            }
            else
            {
                // Feedback
                if (!string.IsNullOrWhiteSpace(model.Email)) issueSummary = model.Email.Trim();
                issueLongDesc = getComment(model);

                data.AddComponent(feedbackComponentId);  // Web-Other
                data.ProjectId = projectId;
                if (model.Subject == 22) data.TypeId = techFeedTypeId;    // 22=Tech Issue
                if (model.Subject == 36) data.TypeId = suggestTypeId;    // 36=Suggestion
                if (model.Subject == 55) data.TypeId = bibIssueTypeId;    // 55=Bib Issue
            }

            data.Description = issueLongDesc;
            data.Title = (issueSummary.Length > 245) ? (issueSummary.Substring(0, 245) + "...") : issueSummary;
            data.PriorityId = priorityId;       // 17=Low, 18=Medium, 19=High
            data.ResolutionId = resolutionId;      // 15=Unresolved
            data.StatusId = statusId;         // 28=Unassigned
            data.SeverityId = severityId;        // 19=Null
            data.ReportedBy = user.Entity.Id;
            data.MailboxId = mailboxId;         // 7=Feedback MS Mailbox

            try
            {
                // Ignore spam from kelev.biz and email.tst.
                // Only a bot can fill Foo with a value, so ignore that as well.
                if (!(model.Email ?? string.Empty).Trim().ToLower().Contains("kelev.biz") &&
                    !(model.Email ?? string.Empty).Trim().ToLower().Contains("email.tst") &&
                    string.IsNullOrWhiteSpace(model.Foo) &&
                    ValidateCaptcha(Request.Form["g-recaptcha-response"]))
                {
                    IssueDto newIssue = serviceManager.Item.Create(data);
                    AddScanRequestCustomFields(model, serviceManager, newIssue, user.Entity.Id);

                    string subject = "BHL Feedback (# " + newIssue.Id.ToString() + ") Received";
                    if (model.Subject == 60) subject = "BHL Scanning Request (# " + newIssue.Id.ToString() + ") Received";
                    if ((model.Email ?? string.Empty).Trim().Length > 0) this.SendEmail(model.Email, subject, Server.HtmlDecode(issueLongDesc));
                    this.ShowConfirmationMessage(model, subject, Server.HtmlDecode(issueLongDesc));

                    if (model.Subject == 60)
                    {
                        try
                        {
                            var type = model.srType == "Book" ? "Book" : model.srType == "Journal" ? "Journal" : "Unsure";
                            new BHLProvider().ScanRequestInsertAuto(newIssue.Id, (model.srTitle ?? string.Empty).Trim(),
                                (model.srYear ?? string.Empty).Trim(), type, (model.srVolume ?? string.Empty).Trim(),
                                (model.srEdition ?? string.Empty).Trim(), (model.srOCLC ?? string.Empty).Trim(), 
                                (model.srISBN ?? string.Empty).Trim(), (model.srISSN ?? string.Empty).Trim(), 
                                (model.srAuthor ?? string.Empty).Trim(), (model.srPublisher ?? string.Empty).Trim(),
                                (model.SelectedLanguageName ?? string.Empty), (model.srNote ?? string.Empty).Trim());
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
                    model.ErrorText = "There was a problem sending your comment. Please try again.";
                }
            }
            catch
            {
                model.ErrorText = "There was a problem sending your comment. Your feedback is important to us, we apologize.";
            }

            return View(model);
        }

        private void AddScanRequestCustomFields(ContactModel model, ServiceManager serviceManager, IssueDto issue, int userId)
        {
            int customFieldOclc = int.Parse(ConfigurationManager.AppSettings["GeminiScanCustomFieldIdOCLC"]);
            int customFieldYearStart = int.Parse(ConfigurationManager.AppSettings["GeminiScanCustomFieldIdYearStart"]);

            if (model.Subject == 60) // 60 is the subject ID for Scan Requests
            {
                if (!string.IsNullOrWhiteSpace(model.srOCLC))
                    serviceManager.Item.CustomFieldDataCreate(GetCustomFieldData(issue, userId, customFieldOclc, model.srOCLC));
                if (!string.IsNullOrWhiteSpace(model.srYear))
                    serviceManager.Item.CustomFieldDataCreate(GetCustomFieldData(issue, userId, customFieldYearStart, model.srYear));
            }
        }

        private CustomFieldData GetCustomFieldData(IssueDto issue, int userId, int customFieldId, string value)
        {
            CustomFieldData customFieldData = new CustomFieldData();
            customFieldData.ProjectId = issue.Project.Id;
            customFieldData.IssueId = issue.Id;
            customFieldData.UserId = userId;
            customFieldData.CustomFieldId = customFieldId;
            customFieldData.Data = value;
            return customFieldData;
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

        private string getComment(ContactModel model)
        {
            StringBuilder sb = new StringBuilder();

            if (model.Name != null && model.Name.Trim().Length > 0)
            {
                sb.Append("<b>Name: </b>");
                sb.Append(Server.HtmlEncode(model.Name.Trim()));
            }
            if (model.Email != null && model.Email.Trim() != string.Empty)
            {
                if (sb.Length > 0) sb.Append("<br>");
                sb.Append("<b>Email: </b>");
                sb.Append(Server.HtmlEncode(model.Email.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(model.FeedbackRefererURL))
            {
                if (sb.Length > 0) sb.Append("<br>");
                sb.Append("<b>URL: </b>");
                sb.Append(model.FeedbackRefererURL);
            }

            if (sb.Length > 0) sb.Append("<br><br>");
            sb.Append(Server.HtmlEncode(model.Comment.Trim()));

            return sb.ToString();
        }

        private string getScanRequest(ContactModel model)
        {
            StringBuilder sb = new StringBuilder();

            if (model.Name != null && model.Name.Trim() != string.Empty)
            {
                sb.Append("<b>Name: </b>");
                sb.Append(Server.HtmlEncode(model.Name.Trim()));
            }
            if (model.Email != null && model.Email.Trim() != string.Empty)
            {
                if (sb.Length > 0) sb.Append("<br>");
                sb.Append("<b>Email: </b>");
                sb.Append(Server.HtmlEncode(model.Email.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(model.FeedbackRefererURL))
            {
                sb.Append("<br>");
                sb.Append("<b>URL: </b>");
                sb.Append(model.FeedbackRefererURL);
            }

            if (sb.Length > 0) sb.Append("<br><br>");
            sb.Append("<b>Type: </b>");
            sb.Append(model.srType == "Book" ? "Book" : model.srType == "Journal" ? "Journal" : "Not Sure");
            sb.Append("<br><b>Title: </b>");
            sb.Append(Server.HtmlEncode(model.srTitle.Trim()));
            sb.Append("<br><b>Year: </b>");
            sb.Append(Server.HtmlEncode(model.srYear.Trim()));
            if (model.srVolume != null && model.srVolume.Trim() != String.Empty)
            {
                sb.Append("<br><b>Volume: </b>");
                sb.Append(Server.HtmlEncode(model.srVolume.Trim()));
            }
            if (model.srEdition != null && model.srEdition.Trim() != String.Empty)
            {
                sb.Append("<br><b>Edition: </b>");
                sb.Append(Server.HtmlEncode(model.srEdition.Trim()));
            }
            if (model.srOCLC != null && model.srOCLC.Trim() != String.Empty)
            {
                sb.Append("<br><b>OCLC: </b>");
                sb.Append(Server.HtmlEncode(model.srOCLC.Trim()));
            }
            if (model.srISBN != null && model.srISBN.Trim() != String.Empty)
            {
                sb.Append("<br><b>ISBN: </b>");
                sb.Append(Server.HtmlEncode(model.srISBN.Trim()));
            }
            if (model.srISSN != null && model.srISSN.Trim() != String.Empty)
            {
                sb.Append("<br><b>ISSN: </b>");
                sb.Append(Server.HtmlEncode(model.srISSN.Trim()));
            }
            if (model.srAuthor != null && model.srAuthor.Trim() != String.Empty)
            {
                sb.Append("<br><b>Author: </b>");
                sb.Append(Server.HtmlEncode(model.srAuthor.Trim()));
            }
            if (model.srPublisher != null && model.srPublisher.Trim() != String.Empty)
            {
                sb.Append("<br><b>Publisher: </b>");
                sb.Append(Server.HtmlEncode(model.srPublisher.Trim()));
            }
            if (model.SelectedLanguage != null && model.SelectedLanguage.Trim() != String.Empty)
            {
                sb.Append("<br><b>Language: </b>");
                sb.Append(model.SelectedLanguage.Trim());
            }
            if (model.srNote != null && model.srNote.Trim() != String.Empty)
            {
                sb.Append("<br><br><b>Note: </b>");
                sb.Append(Server.HtmlEncode(model.srNote.Trim()));
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
                string message = this.GetReceivedMessage();
                string faqLink = ConfigurationManager.AppSettings["WikiPageFAQ"];
                message = message.Replace("[FAQLink]", faqLink);
                message = message.Replace("[Feedback]", this.CleanStringForEmail(feedbackReceived));
                message = message.Replace("[NewsletterLink]", ConfigurationManager.AppSettings["NewsletterSignupUrl"]);
                message = message.Replace("[DonateLink]", ConfigurationManager.AppSettings["DonateUrl"]);

                if (message != String.Empty)
                {
                    Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                    MailRequestModel mailRequest = new MailRequestModel();
                    mailRequest.From = ConfigurationManager.AppSettings["EmailFromAddress"];
                    mailRequest.To = new List<string>();
                    mailRequest.To.Add(recipient);
                    mailRequest.Subject = subject;
                    mailRequest.Body = message;
                    client.SendEmail(mailRequest);
                }
            }
            catch
            {
                // Do nothing if email fails
            }
        }

        private void ShowConfirmationMessage(ContactModel model, string subject, string feedbackReceived)
        {
            model.Submitted = true;
            model.ConfirmationSubject = subject;
            model.ConfirmationText = feedbackReceived;
        }
    }
}