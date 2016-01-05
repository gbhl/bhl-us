﻿using System;
using System.Configuration;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using BHL.SiteServices;

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
            MOBOT.BHL.Web2.SiteService.ArrayOfString recipients = new MOBOT.BHL.Web2.SiteService.ArrayOfString();
            recipients.Add(recipient);
            string message = this.GetEmailMessage(apiKey, contactName);
            if (message != String.Empty)
            {
                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                service.SendEmail("noreply@biodiversitylibrary.org", recipients, null, null,
                    "BHL API Key", message);
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