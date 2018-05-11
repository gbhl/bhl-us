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
                    String includePages = context.Request.QueryString["pages"] ?? "f";
                    String includeOcr = context.Request.QueryString["ocr"] ?? "f";
                    String includeParts = context.Request.QueryString["parts"] ?? "f";
                    ServiceResponse<CustomGenericList<Item>> serviceResponse = new ServiceResponse<CustomGenericList<Item>>();
                    serviceResponse.Result = this.GetItemMetadata(itemID, includePages, includeOcr, includeParts, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemByIdentifier", true) == 0)
                {
                    String identifierType = context.Request.QueryString["type"];
                    String identifierValue = context.Request.QueryString["value"];
                    ServiceResponse<CustomGenericList<Item>> serviceResponse = new ServiceResponse<CustomGenericList<Item>>();
                    serviceResponse.Result = this.GetItemByIdentifier(identifierType, identifierValue, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetItemParts", true) == 0)
                {
                    String itemID = context.Request.QueryString["itemid"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
                    serviceResponse.Result = this.GetItemParts(itemID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Title operations -------

                if (String.Compare(operation, "GetTitleMetadata", true) == 0)
                {
                    String titleID = context.Request.QueryString["titleid"];
                    String includeItems = context.Request.QueryString["items"] ?? "f";
                    ServiceResponse<CustomGenericList<Title>> serviceResponse = new ServiceResponse<CustomGenericList<Title>>();
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

                // ------- Segment operations -------

                if (String.Compare(operation, "GetPartMetadata", true) == 0)
                {
                    String partID = context.Request.QueryString["partid"];
                    ServiceResponse<CustomGenericList<Part>> serviceResponse = new ServiceResponse<CustomGenericList<Part>>();
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

                // ------- Subject operations -------

                if (String.Compare(operation, "SubjectSearch", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Subject>> serviceResponse = new ServiceResponse<CustomGenericList<Subject>>();
                    serviceResponse.Result = this.SubjectSearch(subject, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetSubjectPublications", true) == 0)
                {
                    String subject = context.Request.QueryString["subject"];
                    ServiceResponse<CustomGenericList<Publication>> serviceResponse = new ServiceResponse<CustomGenericList<Publication>>();
                    serviceResponse.Result = this.GetSubjectPublications(subject, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Author operations -------

                if (String.Compare(operation, "AuthorSearch", true) == 0)
                {
                    String name = context.Request.QueryString["authorname"];
                    ServiceResponse<CustomGenericList<Author>> serviceResponse = new ServiceResponse<CustomGenericList<Author>>();
                    serviceResponse.Result = this.AuthorSearch(name, Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]), key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetAuthorPublications", true) == 0)
                {
                    String creatorID = context.Request.QueryString["creatorID"];
                    ServiceResponse<CustomGenericList<Publication>> serviceResponse = new ServiceResponse<CustomGenericList<Publication>>();
                    serviceResponse.Result = this.GetAuthorPublications(creatorID, key);
                    response = serviceResponse.Serialize(outputType);
                }

                // ------- Name operations -------

                if (String.Compare(operation, "GetNameDetail", true) == 0)
                {
                    String nameConfirmed = context.Request.QueryString["name"];
                    ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
                    serviceResponse.Result = this.GetNameDetail(nameConfirmed, key);
                    response = serviceResponse.Serialize(outputType);
                }

                if (String.Compare(operation, "GetNameDetailByIdentifier", true) == 0)
                {
                    String identifierType = context.Request.QueryString["type"];
                    String identifierValue = context.Request.QueryString["value"];
                    ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
                    serviceResponse.Result = this.GetNameDetailByIdentifier(identifierType, identifierValue, key);
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

                if (string.Compare(operation, "PublicationSearch", true) == 0)
                {
                    ServiceResponse<CustomGenericList<Publication>> serviceResponse = new ServiceResponse<CustomGenericList<Publication>>();
                    string title = context.Request.QueryString["title"];
                    string authorLastName = context.Request.QueryString["authorname"];
                    string volume = context.Request.QueryString["volume"];
                    string year = context.Request.QueryString["year"];
                    string subject = context.Request.QueryString["subject"];
                    string language = context.Request.QueryString["language"];
                    string collection = context.Request.QueryString["collection"];
                    string searchTerm = context.Request.QueryString["searchTerm"];
                    string page = context.Request.QueryString["page"];

                    serviceResponse.Result = this.PublicationSearch((title ?? string.Empty), 
                        (authorLastName ?? string.Empty), (volume ?? string.Empty), (year ?? string.Empty), 
                        (subject ?? string.Empty), (language ?? string.Empty), (collection ?? string.Empty),
                        (searchTerm ?? string.Empty), (page ?? "1"),
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

        private CustomGenericList<Name> GetPageNames(string pageID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPageNames, apiKey, pageID);
            Api3 api = new Api3();
            return api.GetPageNames(pageID);
        }

        private Page GetPageMetadata(string pageID, string includeOcr, string includeNames, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPageMetadata, apiKey, pageID + "|" + includeOcr + "|" + includeNames);
            Api3 api = new Api3();
            return api.GetPageMetadata(pageID, includeOcr, includeNames);
        }

        private string GetPageOcrText(string pageID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPageOcrText, apiKey, pageID);
            Api3 api = new Api3();
            return api.GetPageOcrText(pageID);
        }

        private CustomGenericList<Item> GetItemMetadata(string itemID, string includePages, string includeOcr, string includeParts, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetItemMetadata, apiKey, 
                itemID + "|" + includePages + "|" + includeOcr + "|" + includeParts);
            Api3 api = new Api3();
            return api.GetItemMetadata(itemID, includePages, includeOcr, includeParts);
        }

        private CustomGenericList<Item> GetItemByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetItemByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api3 api = new Api3();
            return api.GetItemByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Part> GetItemParts(string itemID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetItemParts, apiKey, itemID);
            Api3 api = new Api3();
            return api.GetItemSegments(itemID);
        }

        private CustomGenericList<Title> GetTitleMetadata(string titleID, string includeItems, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetTitleMetadata, apiKey, titleID);
            Api3 api = new Api3();
            return api.GetTitleMetadata(titleID, includeItems);
        }

        private CustomGenericList<Item> GetTitleItems(string titleID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetTitleItems, apiKey, titleID);
            Api3 api = new Api3();
            return api.GetTitleItems(titleID);
        }

        private CustomGenericList<Title> GetTitleByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetTitleByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api3 api = new Api3();
            return api.GetTitleByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Title> TitleSearchSimple(string title, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.TitleSearchSimple, apiKey, title);
            Api3 api = new Api3();
            return api.TitleSearchSimple(title, fullText);
        }

        private CustomGenericList<Part> GetPartMetadata(string partID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPartMetadata, apiKey, partID);
            Api3 api = new Api3();
            return api.GetSegmentMetadata(partID);
        }

        private CustomGenericList<Name> GetPartNames(string partID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPartNames, apiKey, partID);
            Api3 api = new Api3();
            return api.GetSegmentNames(partID);
        }

        private CustomGenericList<Part> GetPartByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetPartByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api3 api = new Api3();
            return api.GetSegmentByIdentifier(identifierType, identifierValue);
        }

        private CustomGenericList<Subject> SubjectSearch(string subject, bool fullText, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.SubjectSearch, apiKey, subject);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SubjectSearch(subject, fullText);
        }

        private CustomGenericList<Publication> GetSubjectPublications(string subject, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetSubjectPublications, apiKey, subject);
            Api3 api = new Api3();
            return api.GetSubjectPublications(subject);
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

        private CustomGenericList<Publication> GetAuthorPublications(string creatorID, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetAuthorPublications, apiKey, creatorID);
            Api3 api = new Api3();
            return api.GetAuthorPublications(creatorID);
        }

        private Name GetNameDetail(string nameConfirmed, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetNameDetail, apiKey, nameConfirmed);
            Api3 api = new Api3();
            return api.GetNameDetail(nameConfirmed);
        }

        private Name GetNameDetailByIdentifier(string identifierType, string identifierValue, string apiKey)
        {
            ValidateUser(Api3.APIRequestType.GetNameDetailByIdentifier, apiKey, identifierType + "|" + identifierValue);
            Api3 api = new Api3();
            return api.GetNameDetailByIdentifier(identifierType, identifierValue);
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

        private CustomGenericList<Publication> PublicationSearch(string title, string authorLastName, string volume,
            string year, string subject, string languageCode, string collectionID, string searchTerm, string page,
            bool fullText, string apiKey)
        {
            string args = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", title, authorLastName, volume,
                (year == null ? "" : year.ToString()), subject, languageCode, 
                (collectionID == null ? "" : collectionID.ToString()), searchTerm, page.ToString());
            ValidateUser(Api3.APIRequestType.PublicationSearch, apiKey, args);
            Api3 api = new Api3(ConfigurationManager.AppSettings["UseElasticSearch"] == "true");
            return api.SearchPublication(title, authorLastName, volume, year, subject, languageCode, 
                collectionID, searchTerm, page, fullText);
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
