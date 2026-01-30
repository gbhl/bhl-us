using MOBOT.BHL.API.BHLApi;
using MOBOT.BHL.API.BHLApiDataObjects2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MOBOT.BHL.Web2.api2
{
    /// <summary>
    /// Summary description for soap
    /// </summary>
    [WebService(Namespace = "https://www.biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class soap : System.Web.Services.WebService
    {
        #region Name Services

        // The methods in this region are updates to the previously existing Name Services.  The previous
        // methods are still available at the /api/soap.asmx endpoint, and at their original location 
        // (/services/name/). For this version, method signatures are unchanged from the original version, 
        // EXCEPT for the addition of the APIKEY parameter, but the properties of the returned classes 
        // have been modified somewhat.   MWL 2010/1/6

        [WebMethod]
        public int NameCount(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameCount, apiKey, "");
                return (new Api2().NameCount());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public int NameCountBetweenDates(string startDate, string endDate, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameCountBetweenDates, apiKey, startDate + "|" + endDate);
                return (new Api2().NameCountBetweenDates(startDate, endDate));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }
        
        [WebMethod]
        public Name NameGetDetailForNameBankID(string nameBankID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameGetDetailForNameBankID, apiKey, nameBankID);
                return (new Api2().NameGetDetail(nameBankID, ""));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public Name NameGetDetailForName(string nameConfirmed, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameGetDetailForName, apiKey, nameConfirmed);
                return (new Api2().NameGetDetail("", nameConfirmed));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Name> NameList(string startRow, string batchSize, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameList, apiKey, startRow + "|" + batchSize);
                return (new Api2().NameList(startRow, batchSize));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Name> NameListBetweenDates(string startRow, string batchSize, string startDate, string endDate, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameListBetweenDates, apiKey, startRow + "|" + batchSize + "|" + startDate + "|" + endDate);
                return (new Api2().NameListBetweenDates(startRow, batchSize, startDate, endDate));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }
        
        [WebMethod]
        public List<Name> NameSearch(string name, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.NameSearch, apiKey, name);
                return (new Api2().NameSearch(name));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }
        
        #endregion Name Services

        #region Page methods

        [WebMethod]
        public Page GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
                return (new Api2().GetPageMetadata(pageID, includeOcr, includeNames));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public string GetPageOcrText(string pageID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPageOcrText, apiKey, pageID);
                return (new Api2().GetPageOcrText(pageID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Name> GetPageNames(string pageID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPageNames, apiKey, pageID);
                return (new Api2().GetPageNames(pageID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Page methods

        #region Item methods

        [WebMethod]
        public Item GetItemMetadata(string itemID, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetItemMetadata, apiKey, 
                    itemID + "|" + includePages + "|" + includeOcr + "|" + includeParts);
                return (new Api2().GetItemMetadata(itemID, includePages, includeOcr, includeParts));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public Item GetItemByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetItemByIdentifier, apiKey, identifierType + "|" + identifierValue);
                return (new Api2().GetItemByIdentifier(identifierType, identifierValue));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Page> GetItemPages(string itemID, string includeOcr, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetItemPages, apiKey, itemID + "|" + includeOcr);
                return (new Api2().GetItemPages(itemID, includeOcr));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> GetItemParts(string itemID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetItemParts, apiKey, itemID);
                return (new Api2().GetItemSegments(itemID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Item> GetUnpublishedItems(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetUnpublishedItems, apiKey, string.Empty);
                return (new Api2().ItemSelectUnpublished());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Item methods

        #region Title methods

        [WebMethod]
        public Title GetTitleMetadata(string titleID, string includeItems, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetTitleMetadata, apiKey, titleID + "|" + includeItems);
                return (new Api2().GetTitleMetadata(titleID, includeItems));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Item> GetTitleItems(string titleID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetTitleItems, apiKey, titleID);
                return (new Api2().GetTitleItems(titleID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Title> GetTitleByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetTitleByIdentifier, apiKey, identifierType + "|" + identifierValue);
                return (new Api2().GetTitleByIdentifier(identifierType, identifierValue));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Title> TitleSearchSimple(string title, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.TitleSearchSimple, apiKey, title);
                return (new Api2().TitleSearchSimple(title, AppConfig.EnableFullTextSearch));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public string GetTitleBibTex(string titleID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetTitleBibTex, apiKey, titleID);
                return (new Api2().GetTitleBibTex(titleID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public string GetTitleRIS(string titleID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetTitleRIS, apiKey, titleID);
                return (new Api2().GetTitleRIS(titleID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Title> GetUnpublishedTitles(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetUnpublishedTitles, apiKey, string.Empty);
                return (new Api2().TitleSelectUnpublished());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Title methods

        #region Segment methods

        [WebMethod]
        public Part GetPartMetadata(string partID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPartMetadata, apiKey, partID);
                return (new Api2().GetSegmentMetadata(partID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Name> GetPartNames(string partID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPartNames, apiKey, partID);
                return (new Api2().GetSegmentNames(partID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> GetPartByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPartByIdentifier, apiKey, identifierType + "|" + identifierValue);
                return (new Api2().GetSegmentByIdentifier(identifierType, identifierValue));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> GetUnpublishedParts(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetUnpublishedParts, apiKey, string.Empty);
                return (new Api2().SegmentSelectUnpublished());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public string GetPartBibTex(string partID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPartBibTeX, apiKey, partID);
                return (new Api2().GetSegmentBibTex(partID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public string GetPartRIS(string partID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetPartRIS, apiKey, partID);
                return (new Api2().GetSegmentRIS(partID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Segment methods

        #region Subject methods

        [WebMethod]
        public List<Subject> SubjectSearch(string subject, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.SubjectSearch, apiKey, subject);
                return (new Api2().SubjectSearch(subject, AppConfig.EnableFullTextSearch));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Title> GetSubjectTitles(string subject, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetSubjectTitles, apiKey, subject);
                return (new Api2().GetSubjectTitles(subject));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> GetSubjectParts(string subject, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetSubjectParts, apiKey, subject);
                return (new Api2().GetSubjectSegments(subject));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Subject methods

        #region Author methods

        [WebMethod]
        public List<Creator> AuthorSearch(string name, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.AuthorSearch, apiKey, name);
                return (new Api2().AuthorSearch(name, AppConfig.EnableFullTextSearch));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Title> GetAuthorTitles(string creatorID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetAuthorTitles, apiKey, creatorID);
                return (new Api2().GetAuthorTitles(creatorID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> GetAuthorParts(string creatorID, string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetAuthorParts, apiKey, creatorID);
                return (new Api2().GetAuthorSegments(creatorID));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Author methods

        #region Language methods

        [WebMethod]
        public List<Language> GetLanguages(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetLanguages, apiKey, string.Empty);
                return (new Api2().GetLanguages());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Language methods

        #region Collection methods

        [WebMethod]
        public List<Collection> GetCollections(string apiKey)
        {
            try
            {
                ValidateUser(Api2.APIRequestType.GetCollections, apiKey, string.Empty);
                return (new Api2().GetCollections());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Collection methods

        #region Search methods

        [WebMethod]
        public List<Title> BookSearch(string title, string authorLastName, string volume, string edition,
            string year, string subject, string languageCode, string collectionID, string apiKey)
        {
            try
            {
                string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", title, authorLastName, volume, edition, 
                    (year == null ? "" : year.ToString()), subject, languageCode, (collectionID == null ? "" : collectionID.ToString()));
                ValidateUser(Api2.APIRequestType.SearchBook, apiKey, args);
                return (new Api2().SearchBook(title, authorLastName, volume, edition, year, subject, languageCode, collectionID, 
                    500, AppConfig.EnableFullTextSearch));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public List<Part> PartSearch(string title, string containerTitle, string author, string date, string volume,
            string series, string issue, string apiKey)
        {
            try
            {
                string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", title, containerTitle, author, date, volume, series, issue);
                ValidateUser(Api2.APIRequestType.SearchPart, apiKey, args);
                return (new Api2().SearchSegment(title, containerTitle, author, date, volume, series, issue, 500, "Title",
                    AppConfig.EnableFullTextSearch));
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Search methods

        #region Validation methods

        private void ValidateUser(Api2.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (AppConfig.IsProduction)
            {
                if (!new Api2().ValidateApiUser(requestType, apiKey, HttpContext.Current.Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #endregion Validation methods

        #region Stats methods

        [WebMethod]
        public Stats GetStats(string apiKey)
        {
            try
            {
                string args = string.Empty;
                ValidateUser(Api2.APIRequestType.GetStats, apiKey, args);
                return (new Api2().GetStats());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Stats methods

        #region Institution methods

        [WebMethod]
        public List<Institution> GetInstitutions(string apiKey)
        {
            try
            {
                string args = string.Empty;
                ValidateUser(Api2.APIRequestType.GetInstitutions, apiKey, args);
                return (new Api2().GetInstitutions());
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        #endregion Institution methods

    }
}
