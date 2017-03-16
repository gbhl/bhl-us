using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.API.BHLApi;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects2;

namespace MOBOT.BHL.Web2.api2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class httpQuery : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Get the API key
            String key = (context.Request.QueryString["apikey"] ?? String.Empty);

            // Get the output type for the operation
            String format = context.Request.QueryString["format"];
            OutputType outputType = OutputType.Xml;
            if (format == "json") outputType = OutputType.Json;

            String operation = context.Request.QueryString["op"];
            String callback = context.Request.QueryString["callback"];

            try
            {
                // Invoke the operations

                // ------- Page operations -------

                if (String.Compare(operation, "GetPageMetadata", true) == 0)
                {
                    String pageID = context.Request.QueryString["pageid"];
                    String includeOcr = context.Request.QueryString["ocr"];
                    String includeNames = context.Request.QueryString["names"];
                    ServiceResponse<Page> serviceResponse = new ServiceResponse<Page>();
                    serviceResponse.Result = this.GetPageMetadata(pageID, includeOcr, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPageOcrText", true) == 0)
                {
                    String pageID = context.Request.QueryString["pageid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.GetPageOcrText(pageID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPageNames", true) == 0)
                {
                    String pageID = context.Request.QueryString["pageid"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.GetPageNames(pageID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Item operations -------

                if (String.Compare(operation, "GetItemMetadata", true) == 0)
                {
                    String itemID = context.Request.QueryString["itemid"];
                    String includePages = context.Request.QueryString["pages"];
                    String includeOcr = context.Request.QueryString["ocr"];
                    String includeParts = context.Request.QueryString["parts"];
                    ServiceResponse<Item> serviceResponse = new ServiceResponse<Item>();
                    serviceResponse.Result = this.GetItemMetadata(itemID, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemByIdentifier", true) == 0)
                {
                    String identifierType = context.Request.QueryString["type"];
                    String identifierValue = context.Request.QueryString["value"];
                    ServiceResponse<Item> serviceResponse = new ServiceResponse<Item>();
                    serviceResponse.Result = this.GetItemByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemPages", true) == 0)
                {
                    String itemID = context.Request.QueryString["itemID"];
                    String includeOcr = context.Request.QueryString["ocr"];
                    ServiceResponse<CustomGenericList<Page>> serviceResponse = new ServiceResponse<CustomGenericList<Page>>();
                    serviceResponse.Result = this.GetItemPages(itemID, includeOcr, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemParts", true) == 0)
                {
                    String itemID = context.Request.QueryString["itemID"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetItemParts(itemID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedItems", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Item>> serviceResponse = new ServiceResponse<CustomGenericList<Item>>();
                    serviceResponse.Result = this.GetUnpublishedItems(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    String titleID = context.Request.QueryString["titleid"];
                    String includeItems = context.Request.QueryString["items"];
                    ServiceResponse<Title> serviceResponse = new ServiceResponse<Title>();
                    serviceResponse.Result = this.GetTitleMetadata(titleID, includeItems, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleItems", true) == 0)
                {
                    String titleID = context.Request.QueryString["titleid"];
                    ServiceResponse<CustomGenericList<Item>> serviceResponse = new ServiceResponse<CustomGenericList<Item>>();
                    serviceResponse.Result = this.GetTitleItems(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleByIdentifier", true) == 0)
                {
                    String identifierType = context.Request.QueryString["type"];
                    String identifierValue = context.Request.QueryString["value"];
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.GetTitleByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "TitleSearchSimple", true) == 0)
                {
                    String title = context.Request.QueryString["title"];
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.TitleSearchSimple(title, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleBibTex", true) == 0)
                {
                    String titleID = context.Request.QueryString["titleid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.GetTitleBibTex(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleRIS", true) == 0)
                {
                    String titleID = context.Request.QueryString["titleid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.GetTitleRIS(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedTitles", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.GetUnpublishedTitles(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    String partID = context.Request.QueryString["partid"];
                    ServiceResponse<Part> serviceResponse = new ServiceResponse<Part>();
                    serviceResponse.Result = this.GetPartMetadata(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartNames", true) == 0)
                {
                    String partID = context.Request.QueryString["partid"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.GetPartNames(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartByIdentifier", true) == 0)
                {
                    String identifierType = context.Request.QueryString["type"];
                    String identifierValue = context.Request.QueryString["value"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetPartByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedParts", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetUnpublishedParts(key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartBibTex", true) == 0)
                {
                    String partID = context.Request.QueryString["partid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.GetPartBibTex(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartRIS", true) == 0)
                {
                    String partID = context.Request.QueryString["partid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.GetPartRIS(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Subject>> serviceResponse = new ServiceResponse<CustomGenericList<Subject>>();
                    serviceResponse.Result = this.SubjectSearch(subject, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetSubjectTitles", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.GetSubjectTitles(subject, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetSubjectParts", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetSubjectParts(subject, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    String name = context.Request.QueryString["name"];
                    ServiceResponse<CustomGenericList<Creator>> serviceResponse = new ServiceResponse<CustomGenericList<Creator>>();
                    serviceResponse.Result = this.AuthorSearch(name, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetAuthorTitles", true) == 0)
                {
                    String creatorID = context.Request.QueryString["creatorID"];
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.GetAuthorTitles(creatorID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetAuthorParts", true) == 0)
                {
                    String creatorID = context.Request.QueryString["creatorID"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetAuthorParts(creatorID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (String.Compare(operation, "NameCount", true) == 0)
                {
                    String startDate = context.Request.QueryString["startDate"];
                    String endDate = context.Request.QueryString["endDate"];
                    ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
                    serviceResponse.Result = this.NameCount(startDate, endDate, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameList", true) == 0)
                {
                    String startRow = context.Request.QueryString["startRow"];
                    String batchSize = context.Request.QueryString["batchSize"];
                    String startDate = context.Request.QueryString["startDate"];
                    String endDate = context.Request.QueryString["endDate"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.NameList(startRow, batchSize, startDate, endDate, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameGetDetail", true) == 0)
                {
                    String nameBankID = context.Request.QueryString["nameBankID"];
                    String nameConfirmed = context.Request.QueryString["name"];
                    ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
                    serviceResponse.Result = this.NameGetDetail(nameBankID, nameConfirmed, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    String name = context.Request.QueryString["name"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Language operations -------

                if (String.Compare(operation, "GetLanguages", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Language>> serviceResponse = new ServiceResponse<CustomGenericList<Language>>();
                    serviceResponse.Result = this.GetLanguages(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Collection operations -------

                if (String.Compare(operation, "GetCollections", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Collection>> serviceResponse = new ServiceResponse<CustomGenericList<Collection>>();
                    serviceResponse.Result = this.GetCollections(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (String.Compare(operation, "BookSearch", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    String title = context.Request.QueryString["title"];
                    String authorLastName = context.Request.QueryString["lname"];
                    String volume = context.Request.QueryString["volume"];
                    String edition = context.Request.QueryString["edition"];
                    String year = context.Request.QueryString["year"];
                    String subject = context.Request.QueryString["subject"];
                    String language = context.Request.QueryString["language"];
                    String collection = context.Request.QueryString["collectionid"];
                    serviceResponse.Result = this.SearchBook((title ?? string.Empty), (authorLastName ?? string.Empty), 
                        (volume ?? string.Empty), (edition ?? string.Empty), (year ?? string.Empty), (subject ?? string.Empty),
                        (language ?? string.Empty), (collection ?? string.Empty), 
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "PartSearch", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    String title = context.Request.QueryString["title"];
                    String containerTitle = context.Request.QueryString["containerTitle"];
                    String author = context.Request.QueryString["author"];
                    String date = context.Request.QueryString["date"];
                    String volume = context.Request.QueryString["volume"];
                    String series = context.Request.QueryString["series"];
                    String issue = context.Request.QueryString["issue"];
                    serviceResponse.Result = this.SearchPart((title ?? string.Empty), (containerTitle ?? string.Empty),
                        (author ?? string.Empty), (date ?? string.Empty), (volume ?? string.Empty), (series ?? string.Empty),
                        (issue ?? string.Empty), Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), 
                        key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Stats operations -------

                if (String.Compare(operation, "GetStats", true) == 0)
                {
                    ServiceResponse<Stats> serviceResponse = new ServiceResponse<Stats>();
                    serviceResponse.Result = this.GetStats(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Institution>> serviceResponse = new ServiceResponse<CustomGenericList<Institution>>();
                    serviceResponse.Result = this.GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                response = GetErrorResponse("unauthorized", ex, outputType);
                context.Response.Status = "401 Unauthorized";
                context.Response.StatusCode = 401;
                context.Response.StatusDescription = "Unauthorized";
            }
            catch (Exception ex)
            {
                response = GetErrorResponse("error", ex, outputType);
                context.Response.Status = "500 Internal Server Error";
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Internal Server Error";
            }

            // Include any specified callback function in JSON responses
            if ((callback != null) && (callback != String.Empty) && outputType == OutputType.Json)
            {
                response = callback + "(" + response + ");";
            }

            switch (outputType)
            {
                case OutputType.Json:
                    context.Response.ContentType = "application/json";
                    break;
                case OutputType.Xml:
                    context.Response.ContentType = "text/xml";
                    break;
            }
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.Write(response);
        }

        private string GetErrorResponse(string status, Exception ex, OutputType outputType)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Status = status;
            serviceResponse.ErrorMessage = ex.Message;
            serviceResponse.Result = ex.Message;
            return serviceResponse.Serialize(outputType);
        }

        private void ValidateUser(Api2.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (ConfigurationManager.AppSettings["IsProduction"] == "true")
            {
                if (!new Api2().ValidateApiUser(requestType, apiKey, HttpContext.Current.Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #region API Methods

        private CustomGenericList<Name> GetPageNames(string pageID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPageNames, apiKey, pageID);
            Api2 api = new Api2();
            return api.GetPageNames(pageID);
        }

        private Page GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api2 api = new Api2();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private string GetPageOcrText(string pageID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPageOcrText, apiKey, pageID);
            Api2 api = new Api2();
            return api.GetPageOcrText(pageID);
        }

        private Item GetItemMetadata(string itemID, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetItemMetadata, apiKey, 
                itemID + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api2 api = new Api2();
            return api.GetItemMetadata(itemID, includePages, includeOcr, includeParts);
        }

        private Item GetItemByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetItemByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetItemByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Page> GetItemPages(string itemID, string includeOcr, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetItemPages, apiKey, itemID + "|" + includeOcr);
            Api2 api = new Api2();
            return api.GetItemPages(itemID, includeOcr);
        }

        private CustomGenericList<Part> GetItemParts(string itemID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetItemParts, apiKey, itemID);
            Api2 api = new Api2();
            return api.GetItemSegments(itemID);
        }

        private CustomGenericList<Item> GetUnpublishedItems(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetUnpublishedItems, apiKey, string.Empty);
            return new Api2().ItemSelectUnpublished();
        }

        private Title GetTitleMetadata(string titleID, string includeItems, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetTitleMetadata, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleMetadata(titleID, includeItems);
        }

        private CustomGenericList<Item> GetTitleItems(string titleID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetTitleItems, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleItems(titleID);
        }

        private CustomGenericList<Title> GetTitleByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetTitleByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetTitleByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Title> TitleSearchSimple(string title, bool fullText, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.TitleSearchSimple, apiKey, title);
            Api2 api = new Api2();
            return api.TitleSearchSimple(title, fullText);
        }

        private string GetTitleBibTex(string titleID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetTitleBibTex, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleBibTex(titleID);
        }

        private string GetTitleRIS(string titleID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetTitleRIS, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleRIS(titleID);
        }

        private CustomGenericList<Title> GetUnpublishedTitles(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetUnpublishedTitles, apiKey, string.Empty);
            return new Api2().TitleSelectUnpublished();
        }

        private Part GetPartMetadata(string partID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPartMetadata, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentMetadata(partID);
        }

        private CustomGenericList<Name> GetPartNames(string partID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPartNames, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentNames(partID);
        }

        private CustomGenericList<Part> GetPartByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPartByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetSegmentByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Part> GetUnpublishedParts(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetUnpublishedParts, apiKey, string.Empty);
            return new Api2().SegmentSelectUnpublished();
        }

        private string GetPartBibTex(string partID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPartBibTeX, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentBibTex(partID);
        }

        private string GetPartRIS(string partID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetPartRIS, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentRIS(partID);
        }

        private CustomGenericList<Subject> SubjectSearch(string subject, bool fullText, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.SubjectSearch, apiKey, subject);
            Api2 api = new Api2();
            return api.SubjectSearch(subject, fullText);
        }

        private CustomGenericList<Title> GetSubjectTitles(string subject, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetSubjectTitles, apiKey, subject);
            Api2 api = new Api2();
            return api.GetSubjectTitles(subject);
        }

        private CustomGenericList<Part> GetSubjectParts(string subject, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetSubjectParts, apiKey, subject);
            Api2 api = new Api2();
            return api.GetSubjectSegments(subject);
        }

        private CustomGenericList<Creator> AuthorSearch(string name, bool fullText, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.AuthorSearch, apiKey, name);
            Api2 api = new Api2();
            return api.AuthorSearch(name, fullText);
        }

        private CustomGenericList<Title> GetAuthorTitles(string creatorID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetAuthorTitles, apiKey, creatorID);
            Api2 api = new Api2();
            return api.GetAuthorTitles(creatorID);
        }

        private CustomGenericList<Part> GetAuthorParts(string creatorID, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetAuthorParts, apiKey, creatorID);
            Api2 api = new Api2();
            return api.GetAuthorSegments(creatorID);
        }

        private int NameCount(string startDate, string endDate, string apiKey)
        {
            if ((startDate != null) || (endDate != null))
            {
                ValidateUser(Api2.APIRequestType.NameCountBetweenDates, apiKey, startDate + "|" + endDate);
                return new Api2().NameCountBetweenDates(startDate, endDate);
            }
            else
            {
                ValidateUser(Api2.APIRequestType.NameCount, apiKey, "");
                return new Api2().NameCount();
            }
        }

        private CustomGenericList<Name> NameList(string startRow, string batchSize, string startDate, string endDate, string apiKey)
        {
            if ((startDate != null) || (endDate != null))
            {
                ValidateUser(Api2.APIRequestType.NameListBetweenDates, apiKey, startRow + "|" + batchSize + "|" + startDate + "|" + endDate);
                return new Api2().NameListBetweenDates(startRow, batchSize, startDate, endDate);
            }
            else
            {
                ValidateUser(Api2.APIRequestType.NameList, apiKey, startRow + "|" + batchSize);
                return new Api2().NameList(startRow, batchSize);
            }
        }

        private Name NameGetDetail(string nameBankID, string nameConfirmed, string apiKey)
        {
            Api2.APIRequestType requestType;
            requestType = string.IsNullOrEmpty(nameConfirmed) ? Api2.APIRequestType.NameGetDetailForNameBankID : Api2.APIRequestType.NameGetDetailForName;
            ValidateUser(requestType, apiKey, (string.IsNullOrEmpty(nameConfirmed) ? nameBankID : nameConfirmed));
            Api2 api = new Api2();
            return api.NameGetDetail(nameBankID, nameConfirmed);
        }

        private CustomGenericList<Name> NameSearch(string name, string apiKey)
        {
            ValidateUser(Api2.APIRequestType.NameSearch, apiKey, name);
            Api2 api = new Api2();
            return api.NameSearch(name);
        }

        private CustomGenericList<Language> GetLanguages(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetLanguages();
        }

        private CustomGenericList<Collection> GetCollections(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetCollections, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetCollections();
        }

        private CustomGenericList<Title> SearchBook(string title, string authorLastName, string volume, string edition,
            string year, string subject, string languageCode, string collectionID, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", title, authorLastName, volume, edition,
                (year == null ? "" : year.ToString()), subject, languageCode, (collectionID == null ? "" : collectionID.ToString()));
            ValidateUser(Api2.APIRequestType.SearchBook, apiKey, args);
            Api2 api = new Api2();
            return api.SearchBook(title, authorLastName, volume, edition, year, subject, languageCode, collectionID, 500, fullText);
        }

        private CustomGenericList<Part> SearchPart(string title, string containerTitle, string author, string date, string volume,
            string series, string issue, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", title, containerTitle, author, date, volume, series, issue);
            ValidateUser(Api2.APIRequestType.SearchPart, apiKey, args);
            Api2 api = new Api2();
            return api.SearchSegment(title, containerTitle, author, date, volume, series, issue, 500, "Title", fullText);
        }

        private Stats GetStats(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetStats, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetStats();
        }

        private CustomGenericList<Institution> GetInstitutions(string apiKey)
        {
            ValidateUser(Api2.APIRequestType.GetInstitutions, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetInstitutions();
        }

        #endregion API Methods

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
