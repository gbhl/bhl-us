using MOBOT.BHL.API.BHLApi;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ApiController : Controller
    {
        #region API3

        [EnableThrottling]
        public ActionResult Api3Handler()
        {
            string response = string.Empty;

            // Get the API key
            string key = (Request.QueryString["apikey"] ?? string.Empty);

            // Get the output type for the operation
            string format = Request.QueryString["format"];
            OutputType outputType = OutputType.Xml;
            if (format == "json") outputType = OutputType.Json;

            string operation = Request.QueryString["op"];
            string callback = Request.QueryString["callback"];

            try
            {
                // Invoke the operations

                // ------- Page operations -------

                if (String.Compare(operation, "GetPageMetadata", true) == 0)
                {
                    string pageID = Request.QueryString["pageid"];
                    string includeOcr = Request.QueryString["ocr"] ?? "f";
                    string includeNames = Request.QueryString["names"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Page>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Page>>();
                    serviceResponse.Result = this.Api3_GetPageMetadata(pageID, includeOcr, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Item operations -------

                if (String.Compare(operation, "GetItemMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includePages = Request.QueryString["pages"] ?? "f";
                    string includeOcr = Request.QueryString["ocr"] ?? "f";
                    string includeParts = Request.QueryString["parts"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Item>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Item>>();
                    serviceResponse.Result = this.Api3_GetItemMetadata(id, idType, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includeItems = Request.QueryString["items"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Title>>();
                    serviceResponse.Result = this.Api3_GetTitleMetadata(id, idType, includeItems, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includePages = Request.QueryString["pages"] ?? "f";
                    string includeNames = Request.QueryString["names"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Part>>();
                    serviceResponse.Result = this.Api3_GetPartMetadata(id, idType, includePages, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "GetSubjectMetadata", true) == 0)
                {
                    string subject = Request.QueryString["subject"];
                    string includePubs = Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Subject>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Subject>>();
                    serviceResponse.Result = this.Api3_GetSubjectMetadata(subject, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (string.Compare(operation, "GetAuthorMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includePubs = Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<API.BHLApiDataObjects3.Author>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Author>>();
                    serviceResponse.Result = this.Api3_GetAuthorMetadata(id, idType, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (string.Compare(operation, "GetNameMetadata", true) == 0)
                {
                    string nameConfirmed = Request.QueryString["name"];
                    string idType = Request.QueryString["idtype"];
                    string id = Request.QueryString["id"];
                    ServiceResponse<List<API.BHLApiDataObjects3.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Name>>();
                    serviceResponse.Result = this.Api3_GetNameMetadata(nameConfirmed, idType, id, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Language operations -------

                if (String.Compare(operation, "GetLanguages", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Language>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Language>>();
                    serviceResponse.Result = this.Api3_GetLanguages(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Collection operations -------

                if (String.Compare(operation, "GetCollections", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Collection>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Collection>>();
                    serviceResponse.Result = this.Api3_GetCollections(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Institution>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Institution>>();
                    serviceResponse.Result = this.Api3_GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (string.Compare(operation, "PublicationSearch", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Publication>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Publication>>();
                    string searchTerm = Request.QueryString["searchterm"];
                    string searchType = Request.QueryString["searchtype"] ?? "f"; // "F" or "C"
                    string page = Request.QueryString["page"];
                    string pageSize = Request.QueryString["pageSize"];

                    serviceResponse.Result = this.Api3_PublicationSearch((searchTerm ?? string.Empty),
                        searchType, (page ?? "1"), (pageSize ?? Api3.DefaultPubSearchPageSize.ToString()),
                        AppConfig.EnableFullTextSearch,
                        key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PublicationSearchAdvanced", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Publication>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Publication>>();
                    string title = Request.QueryString["title"];
                    string titleOp = Request.QueryString["titleop"];
                    string authorLastName = Request.QueryString["authorname"];
                    string year = Request.QueryString["year"];
                    string subject = Request.QueryString["subject"];
                    string language = Request.QueryString["language"];
                    string collection = Request.QueryString["collection"];
                    string notes = Request.QueryString["notes"];
                    string notesOp = Request.QueryString["notesop"];
                    string text = Request.QueryString["text"];
                    string textOp = Request.QueryString["textop"];
                    string page = Request.QueryString["page"];
                    string pageSize = Request.QueryString["pageSize"];

                    serviceResponse.Result = this.Api3_PublicationSearchAdvanced((title ?? string.Empty),
                        (titleOp ?? string.Empty), (authorLastName ?? string.Empty), (year ?? string.Empty),
                        (subject ?? string.Empty), (language ?? string.Empty), (collection ?? string.Empty),
                        (notes ?? string.Empty), (notesOp ?? string.Empty),
                        (text ?? string.Empty), (textOp ?? string.Empty), (page ?? "1"), (pageSize ?? Api3.DefaultPubSearchPageSize.ToString()),
                        AppConfig.EnableFullTextSearch, key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PageSearch", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects3.Page>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Page>>();
                    string text = Request.QueryString["text"];
                    string idType = Request.QueryString["idType"];
                    idType = string.IsNullOrWhiteSpace(idType) ? "Item" : idType;
                    string id = Request.QueryString["id"];
                    id = string.IsNullOrWhiteSpace(id) ? Request.QueryString["itemID"] : id;
                    serviceResponse.Result = this.Api3_PageSearch(idType, id, text, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    string name = Request.QueryString["name"];
                    ServiceResponse<List<API.BHLApiDataObjects3.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Name>>();
                    serviceResponse.Result = this.Api3_NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    string subject = Request.QueryString["subject"];
                    ServiceResponse<List<API.BHLApiDataObjects3.Subject>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Subject>>();
                    serviceResponse.Result = this.Api3_SubjectSearch(subject, AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    string name = Request.QueryString["authorname"];
                    ServiceResponse<List<API.BHLApiDataObjects3.Author>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects3.Author>>();
                    serviceResponse.Result = this.Api3_AuthorSearch(name, AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                response = GetErrorResponse("unauthorized", ex, outputType);
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                Response.Status = "401 Unauthorized";
                Response.StatusCode = 401;
                Response.StatusDescription = "Unauthorized";
            }
            catch (InvalidApiParamException ex)
            {
                response = GetErrorResponse("bad request", ex, outputType);
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                Response.Status = "400 Bad Request";
                Response.StatusCode = 400;
                Response.StatusDescription = "Bad Request";
            }
            catch (Exception ex)
            {
                response = GetErrorResponse("error", ex, outputType);
                Response.Status = "500 Internal Server Error";
                Response.StatusCode = 500;
                Response.StatusDescription = "Internal Server Error";
            }

            // Include any specified callback function in JSON responses
            if ((callback != null) && (callback != String.Empty) && outputType == OutputType.Json)
            {
                response = callback + "(" + response + ");";
            }

            string contentType;
            switch (outputType)
            {
                case OutputType.Json:
                    contentType = "application/json";
                    break;
                case OutputType.Xml:
                default:
                    contentType = "text/xml";
                    break;
            }

            return Content(response, contentType);
        }

        private void Api3_ValidateUser(Api3.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (AppConfig.IsProduction)
            {
                if (!new Api3().ValidateApiUser(requestType, apiKey, Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #region API3 Methods

        private List<API.BHLApiDataObjects3.Page> Api3_GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private List<API.BHLApiDataObjects3.Item> Api3_GetItemMetadata(string id, string idType, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetItemMetadata, apiKey,
                id + "|" + idType + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api3 api = new Api3();
            return api.GetItemMetadata(id, idType, includePages, includeOcr, includeParts);
        }

        private List<API.BHLApiDataObjects3.Title> Api3_GetTitleMetadata(string id, string idType, string includeItems, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetTitleMetadata, apiKey, id + "|" + idType + "|" + includeItems);
            Api3 api = new Api3();
            return api.GetTitleMetadata(id, idType, includeItems);
        }

        private List<API.BHLApiDataObjects3.Part> Api3_GetPartMetadata(string id, string idType, string includePages, string includeNames, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetPartMetadata, apiKey, id + "|" + idType + "|" + includePages + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetSegmentMetadata(id, idType, includePages, includeNames);
        }

        private List<API.BHLApiDataObjects3.Subject> Api3_SubjectSearch(string subject, bool fullText, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.SubjectSearch, apiKey, subject);
            Api3 api = new Api3();
            return api.SubjectSearch(subject, AppConfig.KeywordResultDefaultSort, fullText);
        }

        private List<API.BHLApiDataObjects3.Subject> Api3_GetSubjectMetadata(string subject, string includePubs, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetSubjectMetadata, apiKey, subject + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetSubjectMetadata(subject, includePubs);
        }

        private List<API.BHLApiDataObjects3.Author> Api3_AuthorSearch(string name, bool fullText, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.AuthorSearch, apiKey, name);
            Api3 api = new Api3();
            return api.AuthorSearch(name, AppConfig.AuthorResultDefaultSort, fullText);
        }

        private List<API.BHLApiDataObjects3.Page> Api3_PageSearch(string idType, string id, string text, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.PageSearch, apiKey, string.Format("{0}|{1}|{2}", idType, id, text));
            Api3 api = new Api3();
            return api.PageSearch(idType, id, text, AppConfig.PageResultDefaultSort);
        }

        private List<API.BHLApiDataObjects3.Author> Api3_GetAuthorMetadata(string id, string idType, string includePubs, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetAuthorMetadata, apiKey, id + "|" + idType + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetAuthorMetadata(id, idType, includePubs);
        }

        private List<API.BHLApiDataObjects3.Name> Api3_GetNameMetadata(string nameConfirmed, string idType, string id, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetNameMetadata, apiKey, nameConfirmed + "|" + idType + "|" + id);
            Api3 api = new Api3();
            return api.GetNameMetadata(nameConfirmed, idType, id);
        }

        private List<API.BHLApiDataObjects3.Name> Api3_NameSearch(string name, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.NameSearch, apiKey, name);
            Api3 api = new Api3();
            return api.NameSearch(name, AppConfig.NameResultDefaultSort);
        }

        private List<API.BHLApiDataObjects3.Language> Api3_GetLanguages(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetLanguages();
        }

        private List<API.BHLApiDataObjects3.Collection> Api3_GetCollections(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetCollections, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetCollections();
        }

        private List<API.BHLApiDataObjects3.Publication> Api3_PublicationSearch(string searchTerm, string searchType,
            string page, string pageSize, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}", searchTerm, searchType, page.ToString(), pageSize.ToString());
            Api3_ValidateUser(Api3.APIRequestType.PublicationSearch, apiKey, args);
            Api3 api = new Api3();
            return api.SearchPublication(searchTerm, searchType, page, pageSize, AppConfig.PublicationResultDefaultSort, fullText);
        }

        private List<API.BHLApiDataObjects3.Publication> Api3_PublicationSearchAdvanced(string title, string titleOp,
            string authorLastName, string year, string subject, string languageCode, string collectionID,
            string notes, string notesOp, string text, string textOp, string page, string pageSize,
            bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}", title, titleOp,
                authorLastName, (year == null ? "" : year.ToString()), subject, languageCode,
                (collectionID == null ? "" : collectionID.ToString()), notes, notesOp, text, textOp, page.ToString(),
                pageSize.ToString());
            Api3_ValidateUser(Api3.APIRequestType.PublicationSearchAdvanced, apiKey, args);
            Api3 api = new Api3();
            return api.SearchPublication(title, titleOp, authorLastName, year, subject, languageCode, collectionID, 
                notes, notesOp, text, textOp, page, pageSize, AppConfig.PublicationResultDefaultSort, fullText);
        }

        private List<API.BHLApiDataObjects3.Institution> Api3_GetInstitutions(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetInstitutions, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetInstitutions();
        }

        #endregion API Methods

        #endregion API3

        #region API2

        [EnableThrottling]
        public ActionResult Api2Handler()
        {
            String response = String.Empty;

            // Get the API key
            String key = (Request.QueryString["apikey"] ?? String.Empty);

            // Get the output type for the operation
            String format = Request.QueryString["format"];
            OutputType outputType = OutputType.Xml;
            if (format == "json") outputType = OutputType.Json;

            String operation = Request.QueryString["op"];
            String callback = Request.QueryString["callback"];

            try
            {
                // Invoke the operations

                // ------- Page operations -------

                if (String.Compare(operation, "GetPageMetadata", true) == 0)
                {
                    String pageID = Request.QueryString["pageid"];
                    String includeOcr = Request.QueryString["ocr"];
                    String includeNames = Request.QueryString["names"];
                    ServiceResponse<API.BHLApiDataObjects2.Page> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Page>();
                    serviceResponse.Result = this.Api2_GetPageMetadata(pageID, includeOcr, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPageOcrText", true) == 0)
                {
                    String pageID = Request.QueryString["pageid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.Api2_GetPageOcrText(pageID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPageNames", true) == 0)
                {
                    String pageID = Request.QueryString["pageid"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Name>>();
                    serviceResponse.Result = this.Api2_GetPageNames(pageID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Item operations -------

                if (String.Compare(operation, "GetItemMetadata", true) == 0)
                {
                    String itemID = Request.QueryString["itemid"];
                    String includePages = Request.QueryString["pages"];
                    String includeOcr = Request.QueryString["ocr"];
                    String includeParts = Request.QueryString["parts"];
                    ServiceResponse<API.BHLApiDataObjects2.Item> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Item>();
                    serviceResponse.Result = this.Api2_GetItemMetadata(itemID, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemByIdentifier", true) == 0)
                {
                    String identifierType = Request.QueryString["type"];
                    String identifierValue = Request.QueryString["value"];
                    ServiceResponse<API.BHLApiDataObjects2.Item> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Item>();
                    serviceResponse.Result = this.Api2_GetItemByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemPages", true) == 0)
                {
                    String itemID = Request.QueryString["itemID"];
                    String includeOcr = Request.QueryString["ocr"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Page>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Page>>();
                    serviceResponse.Result = this.Api2_GetItemPages(itemID, includeOcr, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemParts", true) == 0)
                {
                    String itemID = Request.QueryString["itemID"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    serviceResponse.Result = this.Api2_GetItemParts(itemID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedItems", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Item>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Item>>();
                    serviceResponse.Result = this.Api2_GetUnpublishedItems(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    String titleID = Request.QueryString["titleid"];
                    String includeItems = Request.QueryString["items"];
                    ServiceResponse<API.BHLApiDataObjects2.Title> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Title>();
                    serviceResponse.Result = this.Api2_GetTitleMetadata(titleID, includeItems, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleItems", true) == 0)
                {
                    String titleID = Request.QueryString["titleid"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Item>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Item>>();
                    serviceResponse.Result = this.Api2_GetTitleItems(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleByIdentifier", true) == 0)
                {
                    String identifierType = Request.QueryString["type"];
                    String identifierValue = Request.QueryString["value"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    serviceResponse.Result = this.Api2_GetTitleByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "TitleSearchSimple", true) == 0)
                {
                    String title = Request.QueryString["title"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    serviceResponse.Result = this.Api2_TitleSearchSimple(title, AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleBibTex", true) == 0)
                {
                    String titleID = Request.QueryString["titleid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.Api2_GetTitleBibTex(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetTitleRIS", true) == 0)
                {
                    String titleID = Request.QueryString["titleid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.Api2_GetTitleRIS(titleID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedTitles", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    serviceResponse.Result = this.Api2_GetUnpublishedTitles(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    String partID = Request.QueryString["partid"];
                    ServiceResponse<API.BHLApiDataObjects2.Part> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Part>();
                    serviceResponse.Result = this.Api2_GetPartMetadata(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartNames", true) == 0)
                {
                    String partID = Request.QueryString["partid"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Name>>();
                    serviceResponse.Result = this.Api2_GetPartNames(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartByIdentifier", true) == 0)
                {
                    String identifierType = Request.QueryString["type"];
                    String identifierValue = Request.QueryString["value"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    serviceResponse.Result = this.Api2_GetPartByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetUnpublishedParts", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    serviceResponse.Result = this.Api2_GetUnpublishedParts(key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartBibTex", true) == 0)
                {
                    String partID = Request.QueryString["partid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.Api2_GetPartBibTex(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetPartRIS", true) == 0)
                {
                    String partID = Request.QueryString["partid"];
                    ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                    serviceResponse.Result = this.Api2_GetPartRIS(partID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    String subject = Request.QueryString["subject"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Subject>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Subject>>();
                    serviceResponse.Result = this.Api2_SubjectSearch(subject, AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetSubjectTitles", true) == 0)
                {
                    String subject = Request.QueryString["subject"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    serviceResponse.Result = this.Api2_GetSubjectTitles(subject, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetSubjectParts", true) == 0)
                {
                    String subject = Request.QueryString["subject"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    serviceResponse.Result = this.Api2_GetSubjectParts(subject, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    String name = Request.QueryString["name"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Creator>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Creator>>();
                    serviceResponse.Result = this.Api2_AuthorSearch(name, AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetAuthorTitles", true) == 0)
                {
                    String creatorID = Request.QueryString["creatorID"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    serviceResponse.Result = this.Api2_GetAuthorTitles(creatorID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetAuthorParts", true) == 0)
                {
                    String creatorID = Request.QueryString["creatorID"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    serviceResponse.Result = this.Api2_GetAuthorParts(creatorID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (String.Compare(operation, "NameCount", true) == 0)
                {
                    String startDate = Request.QueryString["startDate"];
                    String endDate = Request.QueryString["endDate"];
                    ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
                    serviceResponse.Result = this.Api2_NameCount(startDate, endDate, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameList", true) == 0)
                {
                    String startRow = Request.QueryString["startRow"];
                    String batchSize = Request.QueryString["batchSize"];
                    String startDate = Request.QueryString["startDate"];
                    String endDate = Request.QueryString["endDate"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Name>>();
                    serviceResponse.Result = this.Api2_NameList(startRow, batchSize, startDate, endDate, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameGetDetail", true) == 0)
                {
                    String nameBankID = Request.QueryString["nameBankID"];
                    String nameConfirmed = Request.QueryString["name"];
                    ServiceResponse<API.BHLApiDataObjects2.Name> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Name>();
                    serviceResponse.Result = this.Api2_NameGetDetail(nameBankID, nameConfirmed, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    String name = Request.QueryString["name"];
                    ServiceResponse<List<API.BHLApiDataObjects2.Name>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Name>>();
                    serviceResponse.Result = this.Api2_NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Language operations -------

                if (String.Compare(operation, "GetLanguages", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Language>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Language>>();
                    serviceResponse.Result = this.Api2_GetLanguages(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Collection operations -------

                if (String.Compare(operation, "GetCollections", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Collection>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Collection>>();
                    serviceResponse.Result = this.Api2_GetCollections(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (String.Compare(operation, "BookSearch", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Title>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Title>>();
                    String title = Request.QueryString["title"];
                    String authorLastName = Request.QueryString["lname"];
                    String volume = Request.QueryString["volume"];
                    String edition = Request.QueryString["edition"];
                    String year = Request.QueryString["year"];
                    String subject = Request.QueryString["subject"];
                    String language = Request.QueryString["language"];
                    String collection = Request.QueryString["collectionid"];
                    serviceResponse.Result = this.Api2_SearchBook((title ?? string.Empty), (authorLastName ?? string.Empty),
                        (volume ?? string.Empty), (edition ?? string.Empty), (year ?? string.Empty), (subject ?? string.Empty),
                        (language ?? string.Empty), (collection ?? string.Empty),
                        AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "PartSearch", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Part>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Part>>();
                    String title = Request.QueryString["title"];
                    String containerTitle = Request.QueryString["containerTitle"];
                    String author = Request.QueryString["author"];
                    String date = Request.QueryString["date"];
                    String volume = Request.QueryString["volume"];
                    String series = Request.QueryString["series"];
                    String issue = Request.QueryString["issue"];
                    serviceResponse.Result = this.Api2_SearchPart((title ?? string.Empty), (containerTitle ?? string.Empty),
                        (author ?? string.Empty), (date ?? string.Empty), (volume ?? string.Empty), (series ?? string.Empty),
                        (issue ?? string.Empty), AppConfig.EnableFullTextSearch, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Stats operations -------

                if (String.Compare(operation, "GetStats", true) == 0)
                {
                    ServiceResponse<API.BHLApiDataObjects2.Stats> serviceResponse = new ServiceResponse<API.BHLApiDataObjects2.Stats>();
                    serviceResponse.Result = this.Api2_GetStats(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<List<API.BHLApiDataObjects2.Institution>> serviceResponse = new ServiceResponse<List<API.BHLApiDataObjects2.Institution>>();
                    serviceResponse.Result = this.Api2_GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                response = GetErrorResponse("unauthorized", ex, outputType);
                Response.Status = "401 Unauthorized";
                Response.StatusCode = 401;
                Response.StatusDescription = "Unauthorized";
            }
            catch (Exception ex)
            {
                response = GetErrorResponse("error", ex, outputType);
                Response.Status = "500 Internal Server Error";
                Response.StatusCode = 500;
                Response.StatusDescription = "Internal Server Error";
            }

            // Include any specified callback function in JSON responses
            if ((callback != null) && (callback != String.Empty) && outputType == OutputType.Json)
            {
                response = callback + "(" + response + ");";
            }

            string contentType;
            switch (outputType)
            {
                case OutputType.Json:
                    contentType = "application/json";
                    break;
                case OutputType.Xml:
                default:
                    contentType = "text/xml";
                    break;
            }

            return Content(response, contentType);
        }

        private void Api2_ValidateUser(Api2.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (AppConfig.IsProduction)
            {
                if (!new Api2().ValidateApiUser(requestType, apiKey, Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #region API2 Methods

        private List<API.BHLApiDataObjects2.Name> Api2_GetPageNames(string pageID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPageNames, apiKey, pageID);
            Api2 api = new Api2();
            return api.GetPageNames(pageID);
        }

        private API.BHLApiDataObjects2.Page Api2_GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api2 api = new Api2();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private string Api2_GetPageOcrText(string pageID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPageOcrText, apiKey, pageID);
            Api2 api = new Api2();
            return api.GetPageOcrText(pageID);
        }

        private API.BHLApiDataObjects2.Item Api2_GetItemMetadata(string itemID, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetItemMetadata, apiKey,
                itemID + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api2 api = new Api2();
            return api.GetItemMetadata(itemID, includePages, includeOcr, includeParts);
        }

        private API.BHLApiDataObjects2.Item Api2_GetItemByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetItemByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetItemByIdentifier(identifierType, identifierValue);
        }

        private List<API.BHLApiDataObjects2.Page> Api2_GetItemPages(string itemID, string includeOcr, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetItemPages, apiKey, itemID + "|" + includeOcr);
            Api2 api = new Api2();
            return api.GetItemPages(itemID, includeOcr);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_GetItemParts(string itemID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetItemParts, apiKey, itemID);
            Api2 api = new Api2();
            return api.GetItemSegments(itemID);
        }

        private List<API.BHLApiDataObjects2.Item> Api2_GetUnpublishedItems(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetUnpublishedItems, apiKey, string.Empty);
            return new Api2().ItemSelectUnpublished();
        }

        private API.BHLApiDataObjects2.Title Api2_GetTitleMetadata(string titleID, string includeItems, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetTitleMetadata, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleMetadata(titleID, includeItems);
        }

        private List<API.BHLApiDataObjects2.Item> Api2_GetTitleItems(string titleID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetTitleItems, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleItems(titleID);
        }

        private List<API.BHLApiDataObjects2.Title> Api2_GetTitleByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetTitleByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetTitleByIdentifier(identifierType, identifierValue);
        }

        private List<API.BHLApiDataObjects2.Title> Api2_TitleSearchSimple(string title, bool fullText, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.TitleSearchSimple, apiKey, title);
            Api2 api = new Api2();
            return api.TitleSearchSimple(title, fullText);
        }

        private string Api2_GetTitleBibTex(string titleID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetTitleBibTex, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleBibTex(titleID);
        }

        private string Api2_GetTitleRIS(string titleID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetTitleRIS, apiKey, titleID);
            Api2 api = new Api2();
            return api.GetTitleRIS(titleID);
        }

        private List<API.BHLApiDataObjects2.Title> Api2_GetUnpublishedTitles(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetUnpublishedTitles, apiKey, string.Empty);
            return new Api2().TitleSelectUnpublished();
        }

        private API.BHLApiDataObjects2.Part Api2_GetPartMetadata(string partID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPartMetadata, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentMetadata(partID);
        }

        private List<API.BHLApiDataObjects2.Name> Api2_GetPartNames(string partID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPartNames, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentNames(partID);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_GetPartByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPartByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api2 api = new Api2();
            return api.GetSegmentByIdentifier(identifierType, identifierValue);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_GetUnpublishedParts(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetUnpublishedParts, apiKey, string.Empty);
            return new Api2().SegmentSelectUnpublished();
        }

        private string Api2_GetPartBibTex(string partID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPartBibTeX, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentBibTex(partID);
        }

        private string Api2_GetPartRIS(string partID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetPartRIS, apiKey, partID);
            Api2 api = new Api2();
            return api.GetSegmentRIS(partID);
        }

        private List<API.BHLApiDataObjects2.Subject> Api2_SubjectSearch(string subject, bool fullText, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.SubjectSearch, apiKey, subject);
            Api2 api = new Api2();
            return api.SubjectSearch(subject, fullText);
        }

        private List<API.BHLApiDataObjects2.Title> Api2_GetSubjectTitles(string subject, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetSubjectTitles, apiKey, subject);
            Api2 api = new Api2();
            return api.GetSubjectTitles(subject);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_GetSubjectParts(string subject, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetSubjectParts, apiKey, subject);
            Api2 api = new Api2();
            return api.GetSubjectSegments(subject);
        }

        private List<API.BHLApiDataObjects2.Creator> Api2_AuthorSearch(string name, bool fullText, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.AuthorSearch, apiKey, name);
            Api2 api = new Api2();
            return api.AuthorSearch(name, fullText);
        }

        private List<API.BHLApiDataObjects2.Title> Api2_GetAuthorTitles(string creatorID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetAuthorTitles, apiKey, creatorID);
            Api2 api = new Api2();
            return api.GetAuthorTitles(creatorID);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_GetAuthorParts(string creatorID, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetAuthorParts, apiKey, creatorID);
            Api2 api = new Api2();
            return api.GetAuthorSegments(creatorID);
        }

        private int Api2_NameCount(string startDate, string endDate, string apiKey)
        {
            if ((startDate != null) || (endDate != null))
            {
                Api2_ValidateUser(Api2.APIRequestType.NameCountBetweenDates, apiKey, startDate + "|" + endDate);
                return new Api2().NameCountBetweenDates(startDate, endDate);
            }
            else
            {
                Api2_ValidateUser(Api2.APIRequestType.NameCount, apiKey, "");
                return new Api2().NameCount();
            }
        }

        private List<API.BHLApiDataObjects2.Name> Api2_NameList(string startRow, string batchSize, string startDate, string endDate, string apiKey)
        {
            if ((startDate != null) || (endDate != null))
            {
                Api2_ValidateUser(Api2.APIRequestType.NameListBetweenDates, apiKey, startRow + "|" + batchSize + "|" + startDate + "|" + endDate);
                return new Api2().NameListBetweenDates(startRow, batchSize, startDate, endDate);
            }
            else
            {
                Api2_ValidateUser(Api2.APIRequestType.NameList, apiKey, startRow + "|" + batchSize);
                return new Api2().NameList(startRow, batchSize);
            }
        }

        private API.BHLApiDataObjects2.Name Api2_NameGetDetail(string nameBankID, string nameConfirmed, string apiKey)
        {
            Api2.APIRequestType requestType;
            requestType = string.IsNullOrEmpty(nameConfirmed) ? Api2.APIRequestType.NameGetDetailForNameBankID : Api2.APIRequestType.NameGetDetailForName;
            Api2_ValidateUser(requestType, apiKey, (string.IsNullOrEmpty(nameConfirmed) ? nameBankID : nameConfirmed));
            Api2 api = new Api2();
            return api.NameGetDetail(nameBankID, nameConfirmed);
        }

        private List<API.BHLApiDataObjects2.Name> Api2_NameSearch(string name, string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.NameSearch, apiKey, name);
            Api2 api = new Api2();
            return api.NameSearch(name);
        }

        private List<API.BHLApiDataObjects2.Language> Api2_GetLanguages(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetLanguages();
        }

        private List<API.BHLApiDataObjects2.Collection> Api2_GetCollections(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetCollections, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetCollections();
        }

        private List<API.BHLApiDataObjects2.Title> Api2_SearchBook(string title, string authorLastName, string volume, string edition,
            string year, string subject, string languageCode, string collectionID, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", title, authorLastName, volume, edition,
                (year == null ? "" : year.ToString()), subject, languageCode, (collectionID == null ? "" : collectionID.ToString()));
            Api2_ValidateUser(Api2.APIRequestType.SearchBook, apiKey, args);
            Api2 api = new Api2();
            return api.SearchBook(title, authorLastName, volume, edition, year, subject, languageCode, collectionID, 500, fullText);
        }

        private List<API.BHLApiDataObjects2.Part> Api2_SearchPart(string title, string containerTitle, string author, string date, string volume,
            string series, string issue, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", title, containerTitle, author, date, volume, series, issue);
            Api2_ValidateUser(Api2.APIRequestType.SearchPart, apiKey, args);
            Api2 api = new Api2();
            return api.SearchSegment(title, containerTitle, author, date, volume, series, issue, 500, "Title", fullText);
        }

        private API.BHLApiDataObjects2.Stats Api2_GetStats(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetStats, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetStats();
        }

        private List<API.BHLApiDataObjects2.Institution> Api2_GetInstitutions(string apiKey)
        {
            Api2_ValidateUser(Api2.APIRequestType.GetInstitutions, apiKey, string.Empty);
            Api2 api = new Api2();
            return api.GetInstitutions();
        }

        #endregion API2 Methods

        #endregion API3

        private string GetErrorResponse(string status, Exception ex, OutputType outputType)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Status = status;
            serviceResponse.ErrorMessage = ex.Message;
            serviceResponse.Result = ex.Message;
            return serviceResponse.Serialize(outputType);
        }
    }
}