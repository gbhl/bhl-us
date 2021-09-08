using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Web.Services;
using Config = System.Configuration;

namespace MOBOT.BHL.WebService
{
    /// <summary>
    /// Summary description for BHLWS
    /// </summary>
    [WebService(Namespace = "http://www.mobot.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public partial class BHLWS : System.Web.Services.WebService
    {
        #region UBio Methods

        [WebMethod]
        public List<NameFinderResponse> GetNamesFromOcr(int pageID)
        {
            return new BHLProvider().GetNamesFromOcr(
                ConfigurationManager.AppSettings["NameFinderService"],
                pageID,
                ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true",
                ConfigurationManager.AppSettings["UsePreferredNameResults"] == "true",
                Convert.ToInt32(ConfigurationManager.AppSettings["MaxReadAttempts"]));
        }

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

        #endregion

        #region Title Methods

        [WebMethod]
        public List<Title> TitleSelectAllPublished()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.TitleSelectAllPublished();
        }

        [WebMethod]
        public Title TitleSelectByTitleID(int titleID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.TitleSelectAuto(titleID);
        }

        [WebMethod]
        public Title TitleSelectDetailByTitleID(int titleID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.TitleSelectExtended(titleID);
        }

        [WebMethod]
        public List<TitleBibTeX> TitleBibTeXSelectAllTitleCitations()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.TitleBibTeXSelectAllTitleCitations();
        }

        [WebMethod]
        public List<TitleBibTeX> TitleBibTeXSelectAllItemCitations()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.TitleBibTeXSelectAllItemCitations();
        }

        [WebMethod]
        public List<TitleBibTeX> SegmentSelectAllBibTeXCitations()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.SegmentSelectAllBibTeXCitations();
        }

        [WebMethod]
        public List<RISCitation> TitleSelectAllRISCitations()
        {
            return new BHLProvider().TitleSelectAllRISCitations();
        }

        [WebMethod]
        public List<Title_Identifier> Title_IdentifierSelectByTitleID(int titleID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.Title_IdentifierSelectByTitleID(titleID);
        }

        #endregion Title Methods

        #region Item Methods

        [WebMethod]
        public Item ItemSelectByBarCode(string barCode)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemSelectByBarCode(barCode);
        }

        [WebMethod]
        public List<Book> BookSelectByTitleID(int titleID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.BookSelectByTitleId(titleID);
        }

        [WebMethod]
        public Book BookSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.BookSelectByItemID(itemID);
        }

        [WebMethod]
        public Item ItemSelectFilenames(ItemType itemType, int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemSelectFilenames(itemType, itemID);
        }

        [WebMethod]
        public Item ItemUpdateStatus(int itemID, int itemStatusID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemUpdateStatus(itemID, itemStatusID);
        }

        [WebMethod]
        public Book BookUpdatePaginationStatus(int bookID, int paginationStatusID, int userID)
        {
            BHLProvider provider = new BHLProvider();
            return provider.BookUpdatePaginationStatus(bookID, paginationStatusID, userID);
        }

        [WebMethod]
        public Item ItemSelectAuto(int itemID)
        {
            BHLProvider provider = new BHLProvider();
            return provider.ItemSelectAuto(itemID);
        }

        [WebMethod]
        public Book BookSelectAuto(int bookID)
        {
            BHLProvider provider = new BHLProvider();
            return provider.BookSelectAuto(bookID);
        }

        [WebMethod]
        public Segment SegmentSelectAuto(int segmentID)
        {
            BHLProvider provider = new BHLProvider();
            return provider.SegmentSelectAuto(segmentID);
        }

        [WebMethod]
        public Segment SegmentSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.SegmentSelectByItemID(itemID);
        }

        [WebMethod]
        public List<Item> ItemSelectWithExpiredPageNames(int maxAge)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemSelectWithExpiredPageNames(maxAge);
        }

        [WebMethod]
        public List<Item> ItemSelectWithoutPageNames()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemSelectWithoutPageNames();
        }

        [WebMethod]
        public Item ItemUpdateLastPageNameLookupDate(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemUpdateLastPageNameLookupDate(itemID);
        }

        [WebMethod]
        public bool ItemCheckForOcrText(int itemID, string ocrTextPath)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemCheckForOcrText(itemID, ocrTextPath, ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
        }

        [WebMethod]
        public string ItemGetNamesXMLByItemID(int itemID, string barcode)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemGetNamesXMLByItemID(itemID, barcode);
        }

        [WebMethod]
        public List<Item> ItemSelectPublished()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ItemSelectPublished();
        }

        [WebMethod]
        public List<Book> BookSelectRecentlyChanged(string startDate)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.BookSelectRecentlyChanged(startDate);
        }

        [WebMethod]
        public List<string> ExportIAIdentifiers()
        {
            BHLProvider bhlServer = new BHLProvider();
            List<Item> items = bhlServer.ItemSelectBarcodes();

            List<string> barcodes = new List<string>();
            foreach(Item item in items)
            {
                barcodes.Add(item.BarCode);
            }
            return barcodes;
        }

        [WebMethod]
        public List<KBART> ExportKBART(string urlRoot)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.ExportKBART(urlRoot);
        }

        [WebMethod]
        public List<RISCitation> ItemSelectAllRISCitations()
        {
            return new BHLProvider().ItemSelectAllRISCitations();
        }

        #endregion Item Methods

        #region Segment Methods

        [WebMethod]
        public List<Segment> SegmentSelectPublished()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.SegmentSelectPublished();
        }

        [WebMethod]
        public Segment SegmentSelectExtended(int segmentID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.SegmentSelectExtended(segmentID);
        }

        [WebMethod]
        public List<RISCitation> SegmentSelectAllRISCitations()
        {
            return new BHLProvider().SegmentSelectAllRISCitations();
        }

        [WebMethod]
        public List<Segment> SegmentSelectRecentlyChanged(string startDate)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.SegmentSelectRecentlyChanged(startDate);
        }

        [WebMethod]
        public string GenerateRISCitation(RISCitation citation)
        {
            return new BHLProvider().GenerateRISCitation(citation);
        }

        #endregion Segment Methods

        #region ItemNameFileLog Methods

        [WebMethod]
        public void ItemNameFileLogRefreshSinceDate(DateTime startDate)
        {
            new BHLProvider().ItemNameFileLogRefreshSinceDate(startDate);
        }

        [WebMethod]
        public List<ItemNameFileLog> ItemNameFileLogSelectForCreate()
        {
            return new BHLProvider().ItemNameFileLogSelectForCreate();
        }

        [WebMethod]
        public List<ItemNameFileLog> ItemNameFileLogSelectForUpload()
        {
            return new BHLProvider().ItemNameFileLogSelectForUpload();
        }

        [WebMethod]
        public void ItemNameFileLogUpdateCreateDate(int logID)
        {
            new BHLProvider().ItemNameFileLogUpdateCreateDate(logID);
        }

        [WebMethod]
        public void ItemNameFileLogUpdateUploadDate(int logID)
        {
            new BHLProvider().ItemNameFileLogUpdateUploadDate(logID);
        }

        #endregion ItemNameFileLog Methods

        #region Vault Methods

        [WebMethod]
        public Vault VaultSelect(int vaultID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.VaultSelect(vaultID);
        }

        #endregion Vault Methods

        #region Institution Methods

        [WebMethod]
        public Institution InstitutionSelectAuto(String institutionCode)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.InstitutionSelectAuto(institutionCode);
        }

        [WebMethod]
        public List<Institution> InstitutionSelectByItemIDAndRole(int itemID, string role)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.InstitutionSelectByItemIDAndRole(itemID, role);
        }

        [WebMethod]
        public List<Institution> InstitutionSelectBySegmentIDAndRole(int segmentID, string role)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.InstitutionSelectBySegmentIDAndRole(segmentID, role);
        }

        #endregion

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
                Config.ConfigurationManager.AppSettings["SMTPHost"]);
            return emailSupport.Send(toAddresses, fromAddress, subject, body, false, null, 
                bccAddresses, ccAddresses, System.Net.Mail.MailPriority.Normal, null);
        }

        #endregion Email Methods

        #region MODS Methods

        [WebMethod]
        public string GetMODSRecordForTitle(int titleId)
        {
            OAI2.OAIRecord record = new OAI2.OAIRecord("oai:biodiversitylibrary.org:title/" + titleId.ToString());
            OAIMODS.Convert mods = new OAIMODS.Convert(record);
            return mods.ToString();
        }

        [WebMethod]
        public string GetMODSRecordForItem(int itemId)
        {
            OAI2.OAIRecord record = new OAI2.OAIRecord("oai:biodiversitylibrary.org:item/" + itemId.ToString());
            OAIMODS.Convert mods = new OAIMODS.Convert(record);
            return mods.ToString();
        }

        [WebMethod]
        public string GetMODSRecordForSegment(int segmentId)
        {
            OAI2.OAIRecord record = new OAI2.OAIRecord("oai:biodiversitylibrary.org:part/" + segmentId.ToString());
            OAIMODS.Convert mods = new OAIMODS.Convert(record);
            return mods.ToString();
        }

        #endregion MODS Methods

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
    }
}
