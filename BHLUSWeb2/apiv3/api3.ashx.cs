using MOBOT.BHL.API.BHLApi;
using MOBOT.BHL.API.BHLApiDataObjects3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;

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
                    ServiceResponse<List<Page>> serviceResponse = new ServiceResponse<List<Page>>();
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
                    ServiceResponse<List<Item>> serviceResponse = new ServiceResponse<List<Item>>();
                    serviceResponse.Result = this.GetItemMetadata(id, idType, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    String id = context.Request.QueryString["id"];
                    String idType = context.Request.QueryString["idType"];
                    String includeItems = context.Request.QueryString["items"] ?? "f";
                    ServiceResponse<List<Title>> serviceResponse = new ServiceResponse<List<Title>>();
                    serviceResponse.Result = this.GetTitleMetadata(id, idType, includeItems, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    String id = context.Request.QueryString["id"];
                    String idType = context.Request.QueryString["idType"];
                    String includePages = context.Request.QueryString["pages"] ?? "f";
                    String includeNames = context.Request.QueryString["names"] ?? "f";
                    ServiceResponse<List<Part>> serviceResponse = new ServiceResponse<List<Part>>();
                    serviceResponse.Result = this.GetPartMetadata(id, idType, includePages, includeNames, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Subject operations -------

                if (String.Compare(operation, "GetSubjectMetadata", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    string includePubs = context.Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<Subject>> serviceResponse = new ServiceResponse<List<Subject>>();
                    serviceResponse.Result = this.GetSubjectMetadata(subject, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (string.Compare(operation, "GetAuthorMetadata", true) == 0)
                {
                    string id = context.Request.QueryString["id"];
                    string idType = context.Request.QueryString["idType"];
                    string includePubs = context.Request.QueryString["pubs"] ?? "f";
                    ServiceResponse<List<Author>> serviceResponse = new ServiceResponse<List<Author>>();
                    serviceResponse.Result = this.GetAuthorMetadata(id, idType, includePubs, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (string.Compare(operation, "GetNameMetadata", true) == 0)
                {
                    String nameConfirmed = context.Request.QueryString["name"];
                    String idType = context.Request.QueryString["idtype"];
                    String id = context.Request.QueryString["id"];
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.GetNameMetadata(nameConfirmed, idType, id, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Language operations -------

                if (String.Compare(operation, "GetLanguages", true) == 0)
                {
                    ServiceResponse<List<Language>> serviceResponse = new ServiceResponse<List<Language>>();
                    serviceResponse.Result = this.GetLanguages(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Collection operations -------

                if (String.Compare(operation, "GetCollections", true) == 0)
                {
                    ServiceResponse<List<Collection>> serviceResponse = new ServiceResponse<List<Collection>>();
                    serviceResponse.Result = this.GetCollections(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Institution operations -------

                if (String.Compare(operation, "GetInstitutions", true) == 0)
                {
                    ServiceResponse<List<Institution>> serviceResponse = new ServiceResponse<List<Institution>>();
                    serviceResponse.Result = this.GetInstitutions(key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Search operations -------

                if (string.Compare(operation, "PublicationSearch", true) == 0)
                {
                    ServiceResponse<List<Publication>> serviceResponse = new ServiceResponse<List<Publication>>();
                    string searchTerm = context.Request.QueryString["searchterm"];
                    string searchType = context.Request.QueryString["searchtype"] ?? "f"; // "F" or "C"
                    string page = context.Request.QueryString["page"];
                    string pageSize = context.Request.QueryString["pageSize"];

                    serviceResponse.Result = this.PublicationSearch((searchTerm ?? string.Empty),
                        searchType, (page ?? "1"), (pageSize ?? Api3.DefaultPubSearchPageSize.ToString()),
                        Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), 
                        key);

                    response = serviceResponse.Serialize(outputType);
                }

                if (string.Compare(operation, "PublicationSearchAdvanced", true) == 0)
                {
                    ServiceResponse<List<Publication>> serviceResponse = new ServiceResponse<List<Publication>>();
                    string title = context.Request.QueryString["title"];
                    string titleOp = context.Request.QueryString["titleop"];
                    string authorLastName = context.Request.QueryString["authorname"];
                    string year = context.Request.QueryString["year"];
                    string subject = context.Request.QueryString["subject"];
                    string language = context.Request.QueryString["language"];
                    string collection = context.Request.QueryString["collection"];
                    string notes = context.Request.QueryString["notes"];
                    string notesOp = context.Request.QueryString["notesop"];
                    string text = context.Request.QueryString["text"];
                    string textOp = context.Request.QueryString["textop"];
                    string page = context.Request.QueryString["page"];
                    string pageSize = context.Request.QueryString["pageSize"];

                    serviceResponse.Result = this.PublicationSearchAdvanced((title ?? string.Empty), 
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
                    string text = context.Request.QueryString["text"];
                    string idType = context.Request.QueryString["idType"];
                    idType = string.IsNullOrWhiteSpace(idType) ? "Item" : idType;
                    string id = context.Request.QueryString["id"];
                    id = string.IsNullOrWhiteSpace(id) ? context.Request.QueryString["itemID"] : id;
                    serviceResponse.Result = this.PageSearch(idType, id, text, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    String name = context.Request.QueryString["name"];
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.NameSearch(name, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<List<Subject>> serviceResponse = new ServiceResponse<List<Subject>>();
                    serviceResponse.Result = this.SubjectSearch(subject, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    String name = context.Request.QueryString["authorname"];
                    ServiceResponse<List<Author>> serviceResponse = new ServiceResponse<List<Author>>();
                    serviceResponse.Result = this.AuthorSearch(name, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                response = GetErrorResponse("unauthorized", ex, outputType);
                context.Server.ClearError();
                context.Response.TrySkipIisCustomErrors = true;
                context.Response.Status = "401 Unauthorized";
                context.Response.StatusCode = 401;
                context.Response.StatusDescription = "Unauthorized";
            }
            catch (InvalidApiParamException ex)
            {
                response = GetErrorResponse("bad request", ex, outputType);
                context.Server.ClearError();
                context.Response.TrySkipIisCustomErrors = true;
                context.Response.Status = "400 Bad Request";
                context.Response.StatusCode = 400;
                context.Response.StatusDescription = "Bad Request";
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
            //context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
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

        private List<Page> GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private List<Item> GetItemMetadata(string id, string idType, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetItemMetadata, apiKey, 
                id + "|" + idType + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api3 api = new Api3();
            return api.GetItemMetadata(id, idType, includePages, includeOcr, includeParts);
        }

        private List<Title> GetTitleMetadata(string id, string idType, string includeItems, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetTitleMetadata, apiKey, id + "|" + idType + "|" + includeItems);
            Api3 api = new Api3();
            return api.GetTitleMetadata(id, idType, includeItems);
        }

        private List<Part> GetPartMetadata(string id, string idType, string includePages, string includeNames, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPartMetadata, apiKey, id + "|" + idType + "|" + includePages + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetSegmentMetadata(id, idType, includePages, includeNames);
        }

        private List<Subject> SubjectSearch(string subject, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.SubjectSearch, apiKey, subject);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SubjectSearch(subject, fullText);
        }

        private List<Subject> GetSubjectMetadata(string subject, string includePubs, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetSubjectMetadata, apiKey, subject + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetSubjectMetadata(subject, includePubs);
        }

        private List<Author> AuthorSearch(string name, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.AuthorSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.AuthorSearch(name, fullText);
        }

        private List<Page> PageSearch(string idType, string id, string text, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.PageSearch, apiKey, string.Format("{0}|{1}|{2}", idType, id, text));
            Api3 api = new Api3();
            return api.PageSearch(idType, id, text);
        }

        private List<Author> GetAuthorMetadata(string id, string idType, string includePubs, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetAuthorMetadata, apiKey, id + "|" + idType + "|" + includePubs);
            Api3 api = new Api3();
            return api.GetAuthorMetadata(id, idType, includePubs);
        }

        private List<Name> GetNameMetadata(string nameConfirmed, string idType, string id, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetNameMetadata, apiKey, nameConfirmed + "|" + idType+ "|" + id);
            Api3 api = new Api3();
            return api.GetNameMetadata(nameConfirmed, idType, id);
        }

        private List<Name> NameSearch(string name, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.NameSearch, apiKey, name);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.NameSearch(name);
        }

        private List<Language> GetLanguages(string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetLanguages, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetLanguages();
        }

        private List<Collection> GetCollections(string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetCollections, apiKey, string.Empty);
            Api3 api = new Api3();
            return api.GetCollections();
        }

        private List<Publication> PublicationSearch(string searchTerm, string searchType,
            string page, string pageSize, bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}", searchTerm, searchType, page.ToString(), pageSize.ToString());
            ValidateUser(Api3.APIRequestType.PublicationSearch, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(searchTerm, searchType, page, pageSize, fullText);
        }

        private List<Publication> PublicationSearchAdvanced(string title, string titleOp,
            string authorLastName, string year, string subject, string languageCode, string collectionID, 
            string notes, string notesOp, string text, string textOp, string page, string pageSize, 
            bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}", title, titleOp, 
                authorLastName, (year == null ? "" : year.ToString()), subject, languageCode, 
                (collectionID == null ? "" : collectionID.ToString()), notes, notesOp, text, textOp, page.ToString(), 
                pageSize.ToString());
            ValidateUser(Api3.APIRequestType.PublicationSearchAdvanced, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(title, titleOp, authorLastName, year, subject, languageCode, 
                collectionID, notes, notesOp, text, textOp, page, pageSize, fullText);
        }

        private List<Institution> GetInstitutions(string apiKey)
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
