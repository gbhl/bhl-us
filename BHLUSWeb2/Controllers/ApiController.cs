﻿using MOBOT.BHL.API.BHLApi;
using MOBOT.BHL.API.BHLApiDataObjects3;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ApiController : Controller
    {
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
                    ServiceResponse<List<Page>> serviceResponse = new ServiceResponse<List<Page>>();
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
                    ServiceResponse<List<Item>> serviceResponse = new ServiceResponse<List<Item>>();
                    serviceResponse.Result = this.Api3_GetItemMetadata(id, idType, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includeItems = Request.QueryString["items"] ?? "f";
                    ServiceResponse<List<Title>> serviceResponse = new ServiceResponse<List<Title>>();
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
                    ServiceResponse<List<Part>> serviceResponse = new ServiceResponse<List<Part>>();
                    serviceResponse.Result = this.Api3_GetPartMetadata(id, idType, includePages, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "GetSubjectMetadata", true) == 0)
                {
                    string subject = Request.QueryString["subject"];
                    string includePubs = Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<Subject>> serviceResponse = new ServiceResponse<List<Subject>>();
                    serviceResponse.Result = this.Api3_GetSubjectMetadata(subject, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (string.Compare(operation, "GetAuthorMetadata", true) == 0)
                {
                    string id = Request.QueryString["id"];
                    string idType = Request.QueryString["idType"];
                    string includePubs = Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<Author>> serviceResponse = new ServiceResponse<List<Author>>();
                    serviceResponse.Result = this.Api3_GetAuthorMetadata(id, idType, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (string.Compare(operation, "GetNameMetadata", true) == 0)
                {
                    string nameConfirmed = Request.QueryString["name"];
                    string idType = Request.QueryString["idtype"];
                    string id = Request.QueryString["id"];
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.Api3_GetNameMetadata(nameConfirmed, idType, id, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Language operations -------

                if (String.Compare(operation, "GetLanguages", true) == 0)
                {
                    ServiceResponse<List<Language>> serviceResponse = new ServiceResponse<List<Language>>();
                    serviceResponse.Result = this.Api3_GetLanguages(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Collection operations -------

                if (String.Compare(operation, "GetCollections", true) == 0)
                {
                    ServiceResponse<List<Collection>> serviceResponse = new ServiceResponse<List<Collection>>();
                    serviceResponse.Result = this.Api3_GetCollections(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<List<Institution>> serviceResponse = new ServiceResponse<List<Institution>>();
                    serviceResponse.Result = this.Api3_GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (string.Compare(operation, "PublicationSearch", true) == 0)
                {
                    ServiceResponse<List<Publication>> serviceResponse = new ServiceResponse<List<Publication>>();
                    string searchTerm = Request.QueryString["searchterm"];
                    string searchType = Request.QueryString["searchtype"] ?? "f"; // "F" or "C"
                    string page = Request.QueryString["page"];
                    string pageSize = Request.QueryString["pageSize"];

                    serviceResponse.Result = this.Api3_PublicationSearch((searchTerm ?? string.Empty),
                        searchType, (page ?? "1"), (pageSize ?? Api3.DefaultPubSearchPageSize.ToString()),
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]),
                        key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PublicationSearchAdvanced", true) == 0)
                {
                    ServiceResponse<List<Publication>> serviceResponse = new ServiceResponse<List<Publication>>();
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
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PageSearch", true) == 0)
                {
                    ServiceResponse<List<Page>> serviceResponse = new ServiceResponse<List<Page>>();
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
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.Api3_NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    string subject = Request.QueryString["subject"];
                    ServiceResponse<List<Subject>> serviceResponse = new ServiceResponse<List<Subject>>();
                    serviceResponse.Result = this.Api3_SubjectSearch(subject, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    string name = Request.QueryString["authorname"];
                    ServiceResponse<List<Author>> serviceResponse = new ServiceResponse<List<Author>>();
                    serviceResponse.Result = this.Api3_AuthorSearch(name, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
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

        private string GetErrorResponse(string status, Exception ex, OutputType outputType)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Status = status;
            serviceResponse.ErrorMessage = ex.Message;
            serviceResponse.Result = ex.Message;
            return serviceResponse.Serialize(outputType);
        }

        private void Api3_ValidateUser(Api3.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (ConfigurationManager.AppSettings["IsProduction"] == "true")
            {
                if (!new Api3().ValidateApiUser(requestType, apiKey, Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #region API Methods

        private List<Page> Api3_GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private List<Item> Api3_GetItemMetadata(string id, string idType, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetItemMetadata, apiKey,
                id + "|" + idType + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api3 api = new Api3();
            return api.GetItemMetadata(id, idType, includePages, includeOcr, includeParts);
        }

        private List<Title> Api3_GetTitleMetadata(string id, string idType, string includeItems, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetTitleMetadata, apiKey, id + "|" + idType + "|" + includeItems);
            Api3 api = new Api3();
            return api.GetTitleMetadata(id, idType, includeItems);
        }

        private List<Part> Api3_GetPartMetadata(string id, string idType, string includePages, string includeNames, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetPartMetadata, apiKey, id + "|" + idType + "|" + includePages + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetSegmentMetadata(id, idType, includePages, includeNames);
        }

        private List<Subject> Api3_SubjectSearch(string subject, bool fullText, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.SubjectSearch, apiKey, subject);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SubjectSearch(subject, fullText);
        }

        private List<Subject> Api3_GetSubjectMetadata(string subject, string includePubs, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetSubjectMetadata, apiKey, subject + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetSubjectMetadata(subject, includePubs);
        }

        private List<Author> Api3_AuthorSearch(string name, bool fullText, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.AuthorSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.AuthorSearch(name, fullText);
        }

        private List<Page> Api3_PageSearch(string idType, string id, string text, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.PageSearch, apiKey, string.Format("{0}|{1}|{2}", idType, id, text));
            Api3 api = new Api3();
            return api.PageSearch(idType, id, text);
        }

        private List<Author> Api3_GetAuthorMetadata(string id, string idType, string includePubs, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetAuthorMetadata, apiKey, id + "|" + idType + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetAuthorMetadata(id, idType, includePubs);
        }

        private List<Name> Api3_GetNameMetadata(string nameConfirmed, string idType, string id, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetNameMetadata, apiKey, nameConfirmed + "|" + idType + "|" + id);
            Api3 api = new Api3();
            return api.GetNameMetadata(nameConfirmed, idType, id);
        }

        private List<Name> Api3_NameSearch(string name, string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.NameSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.NameSearch(name);
        }

        private List<Language> Api3_GetLanguages(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetLanguages();
        }

        private List<Collection> Api3_GetCollections(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetCollections, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetCollections();
        }

        private List<Publication> Api3_PublicationSearch(string searchTerm, string searchType,
            string page, string pageSize, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}", searchTerm, searchType, page.ToString(), pageSize.ToString());
            Api3_ValidateUser(Api3.APIRequestType.PublicationSearch, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(searchTerm, searchType, page, pageSize, fullText);
        }

        private List<Publication> Api3_PublicationSearchAdvanced(string title, string titleOp,
            string authorLastName, string year, string subject, string languageCode, string collectionID,
            string notes, string notesOp, string text, string textOp, string page, string pageSize,
            bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}", title, titleOp,
                authorLastName, (year == null ? "" : year.ToString()), subject, languageCode,
                (collectionID == null ? "" : collectionID.ToString()), notes, notesOp, text, textOp, page.ToString(),
                pageSize.ToString());
            Api3_ValidateUser(Api3.APIRequestType.PublicationSearchAdvanced, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(title, titleOp, authorLastName, year, subject, languageCode,
                collectionID, notes, notesOp, text, textOp, page, pageSize, fullText);
        }

        private List<Institution> Api3_GetInstitutions(string apiKey)
        {
            Api3_ValidateUser(Api3.APIRequestType.GetInstitutions, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetInstitutions();
        }

        #endregion API Methods
    }
}