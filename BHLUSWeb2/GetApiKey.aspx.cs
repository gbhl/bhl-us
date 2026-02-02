using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2
{
    public partial class GetApiKey : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetApiKey_Click(object sender, EventArgs e)
        {
            spanErrorText.InnerText = String.Empty;

            if (validateInput())
            {
                String apiKeyValue = String.Empty;

                // Get the key value
                apiKeyValue = "testkeyvalue";
                APIKey apiKey = new BHLProvider().GetApiKey(txtContactName.Text, txtEmailAddress.Text);
                apiKeyValue = apiKey.ApiKeyValue.ToString();

                // Email the key value
                this.SendEmail(txtEmailAddress.Text, apiKeyValue, txtContactName.Text);

                // Display feedback to user
                litEmail.Text = txtEmailAddress.Text;
                FeedbackForm.Visible = false;
                divDone.Visible = true;
            }
        }

        private bool validateInput()
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(txtContactName.Text.Trim()))
            {
                isValid = false;
                spanErrorText.InnerText = "Please supply a Contact Name.<br/>";
            }

            if (String.IsNullOrEmpty(txtEmailAddress.Text.Trim()))
            {
                isValid = false;
                spanErrorText.InnerText += "Please supply an Email Address.<br/>";
            }

            return isValid;
        }

        private void SendEmail(String recipient, String apiKey, String contactName)
        {
            // Don't catch errors here... if the email doesn't go, we want the user to know 
            // that something went wrong
            string message = this.GetEmailMessage(apiKey, contactName);
            if (message != String.Empty)
            {
                Client client = new Client(AppConfig.SiteServicesURL);
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.From = "noreply@biodiversitylibrary.org";
                mailRequest.To = new List<string>();
                mailRequest.To.Add(recipient);
                mailRequest.Subject = "BHL API Key";
                mailRequest.Body = message;
                client.SendEmail(mailRequest);
            }
        }

        private string GetEmailMessage(String apiKey, String contactName)
        {
            string message = String.Empty;

            try
            {
                message = System.IO.File.ReadAllText(Request.PhysicalApplicationPath + "\\apikeymsg.txt").Replace("{key}", apiKey).Replace("{contact}", contactName);
            }
            catch
            {
                // If file missing, just return a simple message
                message = String.Format("Your BHL API key is: {0}", apiKey);
            }

            return message;
        }
    }
}