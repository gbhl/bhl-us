using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BHL.SiteServices
{
    /// <summary>
    /// Summary description for SiteService
    /// </summary>
    [WebService(Namespace = "https://biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SiteService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetOcrText(int pageID)
        {
            try
            {
                return new BHLProvider().GetOcrText(pageID);
            }
            catch (Exception ex)
            {
                return new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).GetErrorInfo(this.Context.Request, ex);
            }
        }

        #region DOI Methods

        [WebMethod]
        public string DOIGetFileContents(string batchId, string type)
        {
            return new BHLProvider().DOIGetFileContents(batchId, type);
        }

        #endregion DOI Methods

        #region OCR Job File Methods

        [WebMethod]
        public bool OcrJobExists(int itemID)
        {
            return new BHLProvider().OcrJobExists(itemID);
        }

        [WebMethod]
        public void OcrCreateJob(int itemID)
        {
            new BHLProvider().OcrCreateJob(itemID);
        }

        #endregion OCR Job File Methods

        #region MARC File Methods

        [WebMethod]
        public bool MARCFileExists(int id, string type)
        {
            string filePath = new BHLProvider().MarcFileExists(id, type);
            return !string.IsNullOrWhiteSpace(filePath);
        }

        [WebMethod]
        public string MARCGetFileContents(int id, string type)
        {
            return new BHLProvider().MarcGetFileContents(id, type);
        }

        [WebMethod]
        public void MarcCreateFile(string marcBibID, string content)
        {
            new BHLProvider().MarcCreateFile(marcBibID, content);
        }

        #endregion MARC File Methods

        #region Email Methods

        [WebMethod]
        public bool SendEmail(String from, String[] to, String[] cc, String[] bcc, String subject,
            String body)
        {
            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(from);
            System.Net.Mail.MailAddress[] toAddresses = new System.Net.Mail.MailAddress[to.Length];
            int x = 0;
            foreach (String toAddress in to)
            {
                toAddresses[x] = new System.Net.Mail.MailAddress(toAddress);
                x++;
            }
            System.Net.Mail.MailAddress[] ccAddresses = null;
            if (cc != null)
            {
                ccAddresses = new System.Net.Mail.MailAddress[cc.Length];
                x = 0;
                foreach (string ccAddress in cc)
                {
                    ccAddresses[x] = new System.Net.Mail.MailAddress(ccAddress);
                    x++;
                }
            }
            System.Net.Mail.MailAddress[] bccAddresses = null;
            if (bcc != null)
            {
                bccAddresses = new System.Net.Mail.MailAddress[bcc.Length];
                x = 0;
                foreach (string bccAddress in bcc)
                {
                    bccAddresses[x] = new System.Net.Mail.MailAddress(bccAddress);
                    x++;
                }
            }

            EmailSupport emailSupport = new EmailSupport(
                ConfigurationManager.AppSettings["SMTPHost"]);
            return emailSupport.Send(toAddresses, fromAddress, subject, body, false, null,
                bccAddresses, ccAddresses, System.Net.Mail.MailPriority.Normal, null);
        }

        #endregion Email Methods
    }
}
