using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.API.BHLApi;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects3;

namespace MOBOT.BHL.Web2.api3
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class api3 : IHttpHandler
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
                    String includeOcr = context.Request.QueryString["ocr"] ?? "f";
                    String includeNames = context.Request.QueryString["names"] ?? "f";
                    ServiceResponse<CustomGenericList<Page>> serviceResponse = new ServiceResponse<CustomGenericList<Page>>();
                    serviceResponse.Result = this.GetPageMetadata(pageID, includeOcr, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Item operations -------

                if (String.Compare(operation, "GetItemMetadata", true) == 0)
                {
                    String id = context.Request.QueryString["id"];
                    String idType = context.Request.QueryString["idType"];
                    String includePages = context.Request.QueryString["pages"] ?? "f";
                    String includeOcr = context.Request.QueryString["ocr"] ?? "f";
                    String includeParts = context.Request.QueryString["parts"] ?? "f";
                    ServiceResponse<CustomGenericList<Item>> serviceResponse = new ServiceResponse<CustomGenericList<Item>>();
                    serviceResponse.Result = this.GetItemMetadata(id, idType, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    String id = context.Request.QueryString["id"];
                    String idType = context.Request.QueryString["idType"];
                    String includeItems = context.Request.QueryString["items"] ?? "f";
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
                    serviceResponse.Result = this.GetTitleMetadata(id, idType, includeItems, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    String id = context.Request.QueryString["id"];
                    String idType = context.Request.QueryString["idType"];
                    String includeNames = context.Request.QueryString["names"] ?? "f";
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetPartMetadata(id, idType, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "GetSubjectMetadata", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    string includePubs = context.Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<CustomGenericList<Subject>> serviceResponse = new ServiceResponse<CustomGenericList<Subject>>();
                    serviceResponse.Result = this.GetSubjectMetadata(subject, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (string.Compare(operation, "GetAuthorMetadata", true) == 0)
                {
                    string id = context.Request.QueryString["id"];
                    string idType = context.Request.QueryString["idType"];
                    string includePubs = context.Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<CustomGenericList<Author>> serviceResponse = new ServiceResponse<CustomGenericList<Author>>();
                    serviceResponse.Result = this.GetAuthorMetadata(id, idType, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (string.Compare(operation, "GetNameMetadata", true) == 0)
                {
                    String nameConfirmed = context.Request.QueryString["name"];
                    String idType = context.Request.QueryString["idtype"];
                    String id = context.Request.QueryString["id"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.GetNameMetadata(nameConfirmed, idType, id, key);
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

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Institution>> serviceResponse = new ServiceResponse<CustomGenericList<Institution>>();
                    serviceResponse.Result = this.GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (string.Compare(operation, "PublicationSearch", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Publication>> serviceResponse = new ServiceResponse<CustomGenericList<Publication>>();
                    string searchTerm = context.Request.QueryString["searchterm"];
                    string searchType = context.Request.QueryString["searchtype"] ?? "f"; // "F" or "C"
                    string page = context.Request.QueryString["page"];

                    serviceResponse.Result = this.PublicationSearch((searchTerm ?? string.Empty),
                        searchType, (page ?? "1"),
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), 
                        key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PublicationSearchAdvanced", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Publication>> serviceResponse = new ServiceResponse<CustomGenericList<Publication>>();
                    string title = context.Request.QueryString["title"];
                    string titleOp = context.Request.QueryString["titleop"];
                    string authorLastName = context.Request.QueryString["authorname"];
                    string year = context.Request.QueryString["year"];
                    string subject = context.Request.QueryString["subject"];
                    string language = context.Request.QueryString["language"];
                    string collection = context.Request.QueryString["collection"];
                    string text = context.Request.QueryString["text"];
                    string page = context.Request.QueryString["page"];

                    serviceResponse.Result = this.PublicationSearchAdvanced((title ?? string.Empty), 
                        (titleOp ?? string.Empty), (authorLastName ?? string.Empty), (year ?? string.Empty), 
                        (subject ?? string.Empty), (language ?? string.Empty), (collection ?? string.Empty),
                        (text ?? string.Empty), (page ?? "1"),
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PageSearch", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Page>> serviceResponse = new ServiceResponse<CustomGenericList<Page>>();
                    String text = context.Request.QueryString["text"];
                    String itemID = context.Request.QueryString["itemID"];
                    serviceResponse.Result = this.PageSearch(itemID, text, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    String name = context.Request.QueryString["name"];
                    ServiceResponse<CustomGenericList<Name>> serviceResponse = new ServiceResponse<CustomGenericList<Name>>();
                    serviceResponse.Result = this.NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Subject>> serviceResponse = new ServiceResponse<CustomGenericList<Subject>>();
                    serviceResponse.Result = this.SubjectSearch(subject, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    String name = context.Request.QueryString["authorname"];
                    ServiceResponse<CustomGenericList<Author>> serviceResponse = new ServiceResponse<CustomGenericList<Author>>();
                    serviceResponse.Result = this.AuthorSearch(name, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
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

        private void ValidateUser(Api3.APIRequestType requestType, string apiKey, string detail)
        {
            // Only validate users in production
            if (ConfigurationManager.AppSettings["IsProduction"] == "true")
            {
                if (!new Api3().ValidateApiUser(requestType, apiKey, HttpContext.Current.Request.UserHostAddress, detail))
                {
                    throw new UnauthorizedAccessException("'" + apiKey + "' is an invalid or unauthorized API key.");
                }
            }
        }

        #region API Methods

        private CustomGenericList<Page> GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private CustomGenericList<Item> GetItemMetadata(string id, string idType, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetItemMetadata, apiKey, 
                id + "|" + idType + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api3 api = new Api3();
            return api.GetItemMetadata(id, idType, includePages, includeOcr, includeParts);
        }

        private CustomGenericList<Title> GetTitleMetadata(string id, string idType, string includeItems, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetTitleMetadata, apiKey, id + "|" + idType + "|" + includeItems);
            Api3 api = new Api3();
            return api.GetTitleMetadata(id, idType, includeItems);
        }

        private CustomGenericList<Part> GetPartMetadata(string id, string idType, string includeNames, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPartMetadata, apiKey, id + "|" + idType + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetSegmentMetadata(id, idType, includeNames);
        }

        private CustomGenericList<Subject> SubjectSearch(string subject, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.SubjectSearch, apiKey, subject);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SubjectSearch(subject, fullText);
        }

        private CustomGenericList<Subject> GetSubjectMetadata(string subject, string includePubs, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetSubjectMetadata, apiKey, subject + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetSubjectMetadata(subject, includePubs);
        }

        private CustomGenericList<Author> AuthorSearch(string name, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.AuthorSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.AuthorSearch(name, fullText);
        }

        private CustomGenericList<Page> PageSearch(string itemID, string text, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.PageSearch, apiKey, string.Format("{0}|{1}", itemID, text));
            Api3 api = new Api3();
            return api.PageSearch(itemID, text);
        }

        private CustomGenericList<Author> GetAuthorMetadata(string id, string idType, string includePubs, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetAuthorMetadata, apiKey, id + "|" + idType + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetAuthorMetadata(id, idType, includePubs);
        }

        private CustomGenericList<Name> GetNameMetadata(string nameConfirmed, string idType, string id, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetNameMetadata, apiKey, nameConfirmed + "|" + idType+ "|" + id);
            Api3 api = new Api3();
            return api.GetNameMetadata(nameConfirmed, idType, id);
        }

        private CustomGenericList<Name> NameSearch(string name, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.NameSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.NameSearch(name);
        }

        private CustomGenericList<Language> GetLanguages(string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetLanguages();
        }

        private CustomGenericList<Collection> GetCollections(string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetCollections, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetCollections();
        }

        private CustomGenericList<Publication> PublicationSearch(string searchTerm, string searchType,
            string page, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}", searchTerm, searchType, page.ToString());
            ValidateUser(Api3.APIRequestType.PublicationSearch, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(searchTerm, searchType, page, fullText);
        }

        private CustomGenericList<Publication> PublicationSearchAdvanced(string title, string titleOp,
            string authorLastName, string year, string subject, string languageCode, string collectionID, 
            string text, string page, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", title, titleOp, 
                authorLastName, (year == null ? "" : year.ToString()), subject, languageCode, 
                (collectionID == null ? "" : collectionID.ToString()), text, page.ToString());
            ValidateUser(Api3.APIRequestType.PublicationSearchAdvanced, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(title, titleOp, authorLastName, year, subject, languageCode, 
                collectionID, text, page, fullText);
        }

        private CustomGenericList<Institution> GetInstitutions(string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetInstitutions, apiKey, string.Empty);
            Api3 api = new Api3();
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
