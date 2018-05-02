using BHL.Search;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDAL;
using MOBOT.BHL.API.BHLApiDataObjects3;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MOBOT.BHL.API.BHLApi
{
    public class Api3
    {
        private int _apiApplicationID = 5;  // application ID 5 corresponds to "BHL API v3";
        private bool _useElasticSearch = true;  // if this is set to false, some API methods will not return data

        #region Constructor

        public Api3()
        {
        }

        public Api3(bool useElasticSearch)
        {
            _useElasticSearch = useElasticSearch;
        }

        public Api3(int applicationID)
        {
            _apiApplicationID = applicationID;
        }

        #endregion Constructor

        #region Page methods

        public CustomGenericList<Name> GetPageNames(string pageID)
        {
            // Validate the page identifier
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new Exception("pageID (" + pageID + ") must be a valid integer value.");
            }

            // Get the names from the DAL
            return new Api3DAL().NamePageSelectByPageID(null, null, pageIDInt);
        }

        public Page GetPageMetadata(string pageID, string includeOcr, string includeNames)
        {
            // Validate the parameters
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new Exception("pageID (" + pageID + ") must be a valid integer value.");
            }

            // "t" or "true" are acceptable values for the "include" arguments; anything else
            // is considering a value of "false"
            includeOcr = (includeOcr ?? "");
            includeNames = (includeNames ?? "");
            bool ocr = (includeOcr.ToLower() == "t" || includeOcr.ToLower() == "true");
            bool names = (includeNames.ToLower() == "t" || includeNames.ToLower() == "true");

            Api3DAL dal = new Api3DAL();
            Page page = dal.PageSelectAuto(null, null, pageIDInt);
            if (page != null)
            {
                page.PageUrl = "https://www.biodiversitylibrary.org/page/" + page.PageID.ToString();
                page.ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + page.PageID.ToString();
                page.FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + page.PageID.ToString();
                page.OcrUrl = "https://www.biodiversitylibrary.org/pageocr/" + page.PageID.ToString();
                page.PageNumbers = dal.IndicatedPageSelectByPageID(null, null, pageIDInt);
                page.PageTypes = dal.PageTypeSelectByPageID(null, null, pageIDInt);

                if (ocr) page.OcrText = this.GetPageOcrText(pageID);
                if (names) page.Names = this.GetPageNames(pageID);
            }

            return page;
        }

        public string GetPageOcrText(string pageID)
        {
            // Validate the parameters
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new Exception("pageID (" + pageID + ") must be a valid integer value.");
            }

            System.Net.WebClient client = new System.Net.WebClient();
            string text = string.Empty;
            try
            {
                client.Encoding = Encoding.UTF8;
                text = client.DownloadString("https://www.biodiversitylibrary.org/pageocr/" + pageID);
            }
            finally
            {
                client.Dispose();
                client = null;
            }
            return text;            
        }

        #endregion Page methods

        #region Item methods

        public CustomGenericList<Item> GetItemMetadata(string itemID, string includePages)
        {
            return this.GetItemMetadata(itemID, includePages, "f", "f");
        }

        public CustomGenericList<Item> GetItemMetadata(string itemID, string includePages, string includeOcr, string includeSegments)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new Exception("itemID (" + itemID + ") must be a valid integer value.");
            }

            // "t" or "true" are acceptable values for the "includePages" argument; anything else
            // is considering a value of "false"
            includePages = (includePages ?? "");
            bool pages = (includePages.ToLower() == "t" || includePages.ToLower() == "true");

            // "t" or "true" are acceptable values for the "includeSegments" argument; anything else
            // is considering a value of "false"
            includeSegments = (includeSegments ?? "");
            bool segments = (includeSegments.ToLower() == "t" || includeSegments.ToLower() == "true");

            Item item = new Api3DAL().ItemSelectByItemID(null, null, itemIDint);
            if (item != null)
            {
                item.ItemUrl = "https://www.biodiversitylibrary.org/item/" + item.ItemID.ToString();
                item.TitleUrl = (item.PrimaryTitleID == null) ? null : "https://www.biodiversitylibrary.org/bibliography/" + item.PrimaryTitleID.ToString();
                item.ItemThumbUrl = (item.ThumbnailPageID == null) ? null : "https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();

                CustomGenericList<Contributor> scanningInstitutions = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Scanning Institution");
                if (scanningInstitutions.Count > 0) item.ScanningInstitution = scanningInstitutions[0].ContributorName;
                CustomGenericList<Contributor> rightsHolders = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Rights Holder");
                if (rightsHolders.Count > 0) item.RightsHolder = rightsHolders[0].ContributorName;

                if (pages) item.Pages = this.GetItemPages(itemID, includeOcr);
                if (segments) item.Parts = this.GetItemSegments(itemID);
            }

            return new CustomGenericList<Item> { item };
        }

        private CustomGenericList<Page> GetItemPages(string itemID, string includeOcr)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new Exception("itemID (" + itemID + ") must be a valid integer value.");
            }

            // "t" or "true" are acceptable values for the "includeOcr" argument; anything else
            // is considering a value of "false"
            includeOcr = (includeOcr ?? "");
            bool ocr = (includeOcr.ToLower() == "t" || includeOcr.ToLower() == "true");

            // Get the pages
            CustomGenericList<Page> pages = new CustomGenericList<Page>();
            CustomGenericList<PageDetail> pageDetails = new Api3DAL().PageSelectByItemID(null, null, itemIDint);
            foreach (PageDetail pageDetail in pageDetails)
            {
                Page page = new Page();
                page.PageID = pageDetail.PageID;
                page.ItemID = pageDetail.ItemID;
                page.Issue = pageDetail.Issue;
                page.Year = pageDetail.Year;
                page.Volume = pageDetail.Volume;
                page.PageUrl = "https://www.biodiversitylibrary.org/page/" + page.PageID.ToString();
                page.ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + page.PageID.ToString();
                page.FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + page.PageID.ToString();
                page.OcrUrl = "https://www.biodiversitylibrary.org/pageocr/" + page.PageID.ToString();
                page.PageTypes = new CustomGenericList<PageType>();
                page.PageNumbers = new CustomGenericList<PageNumber>();

                if (ocr) page.OcrText = this.GetPageOcrText(page.PageID.ToString());

                if (pageDetail.PageTypeName != string.Empty)
                {
                    string[] pageTypes = pageDetail.PageTypeName.Split(',');
                    foreach (string pageType in pageTypes)
                    {
                        if (pageType != string.Empty) page.PageTypes.Add(new PageType(pageType));
                    }
                }

                if (pageDetail.PageNumbers != string.Empty)
                {
                    string[] pageNumbers = pageDetail.PageNumbers.Split(',');
                    foreach (string pageNumber in pageNumbers)
                    {
                        string pageNumberTrimmed = pageNumber.Trim();
                        if (pageNumberTrimmed != string.Empty)
                        {
                            PageNumber number = new PageNumber();
                            int pos = pageNumberTrimmed.IndexOf(' ');
                            if (pos > 0)
                            {
                                number.Prefix = pageNumberTrimmed.Substring(0, pos).Trim();
                                number.Number = pageNumberTrimmed.Substring(pos).Trim();
                            }
                            else
                            {
                                number.Number = pageNumberTrimmed;
                            }

                            page.PageNumbers.Add(number);
                        }
                    }
                }


                pages.Add(page);
            }

            return pages;
        }

        public CustomGenericList<Item> GetItemByIdentifier(string identifierType, string identifierValue)
        {
            Item item;

            switch (identifierType.ToLower())
            {
                case "barcode":
                case "ia":
                    {
                        Api3DAL dal = new Api3DAL();
                        item = dal.ItemSelectByBarcode(null, null, identifierValue);
                        if (item != null)
                        {
                            CustomGenericList<Contributor> scanningInstitutions = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Scanning Institution");
                            if (scanningInstitutions.Count > 0) item.ScanningInstitution = scanningInstitutions[0].ContributorName;
                            CustomGenericList<Contributor> rightsHolders = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Rights Holder");
                            if (rightsHolders.Count > 0) item.RightsHolder = rightsHolders[0].ContributorName;

                            item.ItemUrl = "https://www.biodiversitylibrary.org/item/" + item.ItemID.ToString();
                            item.TitleUrl = (item.PrimaryTitleID == null ) ? null : "https://www.biodiversitylibrary.org/bibliography/" + item.PrimaryTitleID.ToString();
                            item.ItemThumbUrl = (item.ThumbnailPageID == null) ? null : "https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();
                        }

                        break;
                    }
                default:
                    throw new Exception("identifierType must be one of the following values: barcode, ia");
            }

            return new CustomGenericList<Item> { item };
        }

        public CustomGenericList<Part> GetItemSegments(string itemID)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new Exception("itemID (" + itemID + ") must be a valid integer value.");
            }

            Api3DAL dal = new Api3DAL();
            CustomGenericList<Part> parts = dal.SegmentSelectByItemID(null, null, itemIDint);
            foreach (Part part in parts)
            {
                part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
            }

            return parts;
        }

        #endregion Item methods

        #region Title methods

        public CustomGenericList<Title> GetTitleMetadata(string titleID, string includeItems)
        {
            // Validate the parameters
            int titleIDint;
            if (!Int32.TryParse(titleID, out titleIDint))
            {
                throw new Exception("titleID (" + titleID + ") must be a valid integer value.");
            }

            // "t" or "true" are acceptable values for the "includeItems" argument; anything else
            // is considering a value of "false"
            bool items = (includeItems.ToLower() == "t" || includeItems.ToLower() == "true");

            Api3DAL dal = new Api3DAL();
            Title title = dal.TitleSelectAuto(null, null, titleIDint);
            if (title != null)
            {
                title.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
                title.Identifiers = dal.TitleIdentifierSelectByTitleID(null, null, title.TitleID);
                title.Variants = dal.TitleVariantSelectByTitleID(null, null, title.TitleID);
                title.Subjects = dal.SubjectSelectByTitleID(null, null, title.TitleID);
                title.Notes = dal.TitleNoteSelectByTitleID(null, null, title.TitleID);

                if (items)
                {
                    title.Items = this.GetTitleItems(title.TitleID.ToString());
                }
            }

            return new CustomGenericList<Title> { title };
        }

        public CustomGenericList<Item> GetTitleItems(string titleID)
        {
            // Validate the parameters
            int titleIDint;
            if (!Int32.TryParse(titleID, out titleIDint))
            {
                throw new Exception("titleID (" + titleID + ") must be a valid integer value.");
            }

            // Get the items
            CustomGenericList<Item> items = new Api3DAL().ItemSelectByTitleID(null, null, titleIDint);
            foreach (Item item in items)
            {
                CustomGenericList<Contributor> scanningInstitutions = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Scanning Institution");
                if (scanningInstitutions.Count > 0) item.ScanningInstitution = scanningInstitutions[0].ContributorName;
                CustomGenericList<Contributor> rightsHolders = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Rights Holder");
                if (rightsHolders.Count > 0) item.RightsHolder = rightsHolders[0].ContributorName;

                item.ItemUrl = "https://www.biodiversitylibrary.org/item/" + item.ItemID.ToString();
                item.TitleUrl = (item.PrimaryTitleID == null) ? null : "https://www.biodiversitylibrary.org/bibliography/" + item.PrimaryTitleID.ToString();
                item.ItemThumbUrl = (item.ThumbnailPageID == null) ? null : "https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();
            }

            return items;
        }

        public CustomGenericList<Title> GetTitleByIdentifier(string identifierType, string identifierValue)
        {
            CustomGenericList<Title> titles = new CustomGenericList<Title>();

            switch (identifierType.ToLower())
            {
                case "oclc":
                case "issn":
                case "isbn":
                case "lccn":
                case "ddc":
                case "nal":
                case "nlm":
                case "coden":
                case "soulsby":
                    {
                        if (identifierType.ToLower() == "lccn") identifierType = "dlc";

                        Api3DAL dal = new Api3DAL();
                        titles = dal.TitleSelectByIdentifier(null, null, identifierType, identifierValue);
                        foreach (Title title in titles)
                        {
                            title.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                            title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
                            title.Identifiers = dal.TitleIdentifierSelectByTitleID(null, null, title.TitleID);
                            title.Variants = dal.TitleVariantSelectByTitleID(null, null, title.TitleID);
                            title.Subjects = dal.SubjectSelectByTitleID(null, null, title.TitleID);
                            title.Notes = dal.TitleNoteSelectByTitleID(null, null, title.TitleID);
                        }

                        break;
                    }
                case "doi":
                    {
                        Api3DAL dal = new Api3DAL();
                        titles = dal.TitleSelectByDOI(null, null, identifierValue);
                        foreach (Title title in titles)
                        {
                            title.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                            title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
                            title.Identifiers = dal.TitleIdentifierSelectByTitleID(null, null, title.TitleID);
                            title.Variants = dal.TitleVariantSelectByTitleID(null, null, title.TitleID);
                            title.Subjects = dal.SubjectSelectByTitleID(null, null, title.TitleID);
                            title.Notes = dal.TitleNoteSelectByTitleID(null, null, title.TitleID);
                        }

                        break;
                    }
                default:
                    throw new Exception("identifierType must be one of the following values: doi, oclc, issn, isbn, lccn, ddc, nal, nlm, coden");
            }

            return titles;
        }

        public CustomGenericList<Title> TitleSearchSimple(string title, bool fullText)
        {
            if (fullText)
                return new Api3DAL().SearchTitleSimple(null, null, title);
            else
                return new Api3DAL().TitleSelectSearchSimple(null, null, title);
        }

        #endregion Title methods

        #region Segment methods

        public CustomGenericList<Part> GetSegmentMetadata(string segmentID)
        {
            // Validate the parameters
            int segmentIDint;
            if (!Int32.TryParse(segmentID, out segmentIDint))
            {
                throw new Exception("segmentID (" + segmentID + ") must be a valid integer value.");
            }

            Api3DAL dal = new Api3DAL();
            Part part = dal.SegmentSelectForSegmentID(null, null, segmentIDint);
            if (part != null)
            {
                part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                part.Identifiers = dal.SegmentIdentifierSelectBySegmentID(null, null, part.PartID);
                part.Subjects = dal.SubjectSelectBySegmentID(null, null, part.PartID);
                part.Pages = this.GetSegmentPages(part.PartID);
                part.RelatedParts = dal.SegmentSelectRelated(null, null, part.PartID);
                foreach(Part relatedPart in part.RelatedParts)
                {
                    relatedPart.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, relatedPart.PartID, InstitutionRole.Contributor);
                }
                part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
            }

            return new CustomGenericList<Part> { part };
        }

        public CustomGenericList<Name> GetSegmentNames(string segmentID)
        {
            // Validate the page identifier
            int segmentIDInt;
            if (!Int32.TryParse(segmentID, out segmentIDInt))
            {
                throw new Exception("segmentID (" + segmentID + ") must be a valid integer value.");
            }

            // Get the names from the DAL
            return new Api3DAL().NameSegmentSelectBySegmentID(null, null, segmentIDInt);
        }

        public CustomGenericList<Part> GetSegmentByIdentifier(string identifierType, string identifierValue)
        {
            CustomGenericList<Part> parts = new CustomGenericList<Part>();

            switch (identifierType.ToLower())
            {
                case "oclc":
                case "issn":
                case "isbn":
                case "lccn":
                case "ddc":
                case "nal":
                case "nlm":
                case "coden":
                case "biostor":
                case "soulsby":
                    {
                        if (identifierType.ToLower() == "lccn") identifierType = "dlc";

                        Api3DAL dal = new Api3DAL();
                        parts = dal.SegmentSelectByIdentifier(null, null, identifierType, identifierValue);
                        foreach (Part part in parts)
                        {
                            part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                            part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                            part.Identifiers = dal.SegmentIdentifierSelectBySegmentID(null, null, part.PartID);
                            part.Subjects = dal.SubjectSelectBySegmentID(null, null, part.PartID);
                            part.Pages = this.GetSegmentPages(part.PartID);
                            part.RelatedParts = dal.SegmentSelectRelated(null, null, part.PartID);
                            foreach (Part relatedPart in part.RelatedParts)
                            {
                                relatedPart.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, relatedPart.PartID, InstitutionRole.Contributor);
                            }
                            part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
                        }

                        break;
                    }
                case "doi":
                    {
                        Api3DAL dal = new Api3DAL();
                        parts = dal.SegmentSelectByDOI(null, null, identifierValue);
                        foreach (Part part in parts)
                        {
                            part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                            part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                            part.Identifiers = dal.SegmentIdentifierSelectBySegmentID(null, null, part.PartID);
                            part.Subjects = dal.SubjectSelectBySegmentID(null, null, part.PartID);
                            part.Pages = this.GetSegmentPages(part.PartID);
                            part.RelatedParts = dal.SegmentSelectRelated(null, null, part.PartID);
                            foreach (Part relatedPart in part.RelatedParts)
                            {
                                relatedPart.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, relatedPart.PartID, InstitutionRole.Contributor);
                            }
                            part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
                        }

                        break;
                    }
                default:
                    throw new Exception("identifierType must be one of the following values: doi, oclc, issn, isbn, lccn, ddc, nal, nlm, coden, biostor");
            }

            return parts;
        }

        private CustomGenericList<Page> GetSegmentPages(int segmentID)
        {
            // Get the pages
            CustomGenericList<Page> pages = new CustomGenericList<Page>();
            CustomGenericList<PageDetail> pageDetails = new Api3DAL().PageSelectBySegmentID(null, null, segmentID);
            foreach (PageDetail pageDetail in pageDetails)
            {
                Page page = new Page();
                page.PageID = pageDetail.PageID;
                page.ItemID = pageDetail.ItemID;
                page.Issue = pageDetail.Issue;
                page.Year = pageDetail.Year;
                page.Volume = pageDetail.Volume;
                page.PageUrl = "https://www.biodiversitylibrary.org/page/" + page.PageID.ToString();
                page.ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + page.PageID.ToString();
                page.FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + page.PageID.ToString();
                page.OcrUrl = "https://www.biodiversitylibrary.org/pageocr/" + page.PageID.ToString();
                page.PageTypes = new CustomGenericList<PageType>();
                page.PageNumbers = new CustomGenericList<PageNumber>();

                if (pageDetail.PageTypeName != string.Empty)
                {
                    string[] pageTypes = pageDetail.PageTypeName.Split(',');
                    foreach (string pageType in pageTypes)
                    {
                        if (pageType != string.Empty) page.PageTypes.Add(new PageType(pageType));
                    }
                }

                if (pageDetail.PageNumbers != string.Empty)
                {
                    string[] pageNumbers = pageDetail.PageNumbers.Split(',');
                    foreach (string pageNumber in pageNumbers)
                    {
                        string pageNumberTrimmed = pageNumber.Trim();
                        if (pageNumberTrimmed != string.Empty)
                        {
                            PageNumber number = new PageNumber();
                            int pos = pageNumberTrimmed.IndexOf(' ');
                            if (pos > 0)
                            {
                                number.Prefix = pageNumberTrimmed.Substring(0, pos).Trim();
                                number.Number = pageNumberTrimmed.Substring(pos).Trim();
                            }
                            else
                            {
                                number.Number = pageNumberTrimmed;
                            }

                            page.PageNumbers.Add(number);
                        }
                    }
                }


                pages.Add(page);
            }

            return pages;
        }

        #endregion Segment methods

        #region Subject methods

        public CustomGenericList<Subject> SubjectSearch(string subject, bool fullText)
        {
            CustomGenericList<Subject> subjects = new CustomGenericList<Subject>();

            if (_useElasticSearch)
            {
                // Submit the request to ElasticSearch
                ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
                search.StartPage = 1;
                search.NumResults = 10000;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["KeywordResultDefaultSort"]);
                ISearchResult result = search.SearchKeyword(subject);

                // Build the list of results
                foreach(KeywordHit hit in result.Keywords)
                {
                    subjects.Add(new Subject { SubjectText = hit.Keyword });
                }
            }
            else
            {
                Api3DAL dal = new Api3DAL();
                if (fullText)
                    subjects = dal.SearchTitleKeyword(null, null, subject);
                else
                    subjects = dal.TitleKeywordSelectLikeTag(null, null, subject);
            }

            return subjects;
        }

        public CustomGenericList<Publication> GetSubjectPublications(string subject)
        {
            throw new NotImplementedException();
        }

        #endregion Subject methods

        #region Author methods

        public CustomGenericList<Creator> AuthorSearch(string name, bool fullText)
        {
            CustomGenericList<Creator> creators = new CustomGenericList<Creator>();

            if (_useElasticSearch)
            {
                // Submit the request to ElasticSearch
                ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
                search.StartPage = 1;
                search.NumResults = 10000;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["AuthorResultDefaultSort"]);
                ISearchResult result = search.SearchAuthor(name);

                // Build the list of results
                List<int> creatorIds = new List<int>();
                foreach (AuthorHit hit in result.Authors)
                {
                    creatorIds.Add(Convert.ToInt32(hit.Id));
                }
                creators = new Api3DAL().AuthorSelectForList(null, null, creatorIds);
            }
            else
            {
                if (fullText)
                    creators = new Api3DAL().SearchAuthor(null, null, name);
                else
                    creators = new Api3DAL().AuthorSelectNameStartsWith(null, null, name);
            }

            foreach (Creator creator in creators)
            {
                creator.CreatorUrl = "https://www.biodiversitylibrary.org/creator/" + creator.CreatorID.ToString();
            }

            return creators;
        }

        public CustomGenericList<Publication> GetAuthorPublications(string creatorID)
        {
            throw new NotImplementedException();
        }


        #endregion Author methods

        #region Name Services

        // These methods are parallels of the original Name Service methods, though the method
        // signatures and response formats have changed somewhat.

        public Name GetNameDetail(string nameConfirmed)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(nameConfirmed)) throw new Exception("Please supply a Name.");

            CustomGenericList<PageDetail> pageDetails = null;
            pageDetails = new Api3DAL().PageSelectByNameConfirmed(null, null, nameConfirmed);

            return GetNameDetailFromPageDetails(pageDetails);
        }

        public Name GetNameDetailByIdentifier(string identifierType, string identifierValue)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(identifierType) || string.IsNullOrWhiteSpace(identifierValue)) throw new Exception("Please supply an identifier Type and Name.");

            string identifierName = string.Empty;
            switch (identifierType)
            {
                case "namebank":
                    identifierName = "NameBank"; break;
                case "eol":
                    identifierName = "EOL"; break;
                case "gni":
                    identifierName = "GNI"; break;
                case "ion":
                    identifierName = "Index to Organism Names"; break;
                case "col":
                    identifierName = "Catalogue of Life"; break;
                case "gbif":
                    identifierName = "GBIF Taxonomic Backbone"; break;
                case "itis":
                    identifierName = "ITIS"; break;
                case "ipni":
                    identifierName = "The International Plant Names Index"; break;
                case "worms":
                    identifierName = "WoRMS"; break;
            }

            if (string.IsNullOrWhiteSpace(identifierName)) throw new Exception("Please supply one of the following identifier Types: namebank, eol, gni, ion, col, gbif, itis, ipni, worms.");

            CustomGenericList<PageDetail> pageDetails = null;
            pageDetails = new Api3DAL().PageSelectByNameIdentifier(null, null, identifierName, identifierValue);

            return GetNameDetailFromPageDetails(pageDetails);
        }

        private Name GetNameDetailFromPageDetails(CustomGenericList<PageDetail> pageDetails)
        {
            Name name = null;
            Title currentTitle = null;
            Item currentItem = null;
            Page currentPage = null;

            if (pageDetails.Count > 0)
            {
                // Get the name information
                name = new Name();
                name.Identifiers.Add(new Identifier("NameBank", pageDetails[0].NameBankID));
                name.NameConfirmed = pageDetails[0].NameConfirmed;
                name.Titles = new CustomGenericList<Title>();

                currentTitle = new Title();
                currentItem = new Item();
                currentPage = new Page();

                // Get the title, item, and page information
                foreach (PageDetail pageDetail in pageDetails)
                {
                    if (pageDetail.TitleID != currentTitle.TitleID)
                    {
                        // Add a new title
                        Title title = new Title();
                        title.TitleID = pageDetail.TitleID;
                        title.ShortTitle = pageDetail.PublicationTitle;
                        title.PublisherPlace = pageDetail.PublisherPlace;
                        title.PublisherName = pageDetail.PublisherName;
                        title.PublicationDate = pageDetail.PublicationDate;
                        title.CallNumber = pageDetail.CallNumber;
                        title.TitleUrl = pageDetail.TitleUrl;
                        title.Items = new CustomGenericList<Item>();
                        name.Titles.Add(title);
                        currentTitle = title;
                    }

                    if (pageDetail.ItemID != currentItem.ItemID)
                    {
                        // Add a new item
                        Item item = new Item();
                        item.ItemID = pageDetail.ItemID;
                        item.Source = pageDetail.Source;
                        item.SourceIdentifier = pageDetail.SourceIdentifier;
                        item.Volume = pageDetail.VolumeInfo;
                        item.HoldingInstitution = pageDetail.HoldingInstitution;
                        item.ItemUrl = pageDetail.ItemUrl;
                        item.Pages = new CustomGenericList<Page>();
                        currentTitle.Items.Add(item);
                        currentItem = item;
                    }

                    if (pageDetail.PageID != currentPage.PageID)
                    {
                        // Add a new page
                        Page page = new Page();
                        page.PageID = pageDetail.PageID;
                        page.ItemID = pageDetail.ItemID;
                        page.Year = pageDetail.Year;
                        page.Volume = pageDetail.Volume;
                        page.Issue = pageDetail.Issue;
                        page.PageUrl = pageDetail.PageUrl;
                        page.ThumbnailUrl = pageDetail.ThumbnailUrl;
                        page.FullSizeImageUrl = pageDetail.FullSizeImageUrl;
                        page.OcrUrl = pageDetail.OcrUrl;
                        page.PageNumbers = new CustomGenericList<PageNumber>();
                        page.PageNumbers.Add(new PageNumber(pageDetail.Prefix, pageDetail.Number));

                        // Get the page types
                        page.PageTypes = new CustomGenericList<PageType>();
                        if (pageDetail.PageTypeName != String.Empty)
                        {
                            string[] pageTypes = pageDetail.PageTypeName.Split(',');
                            foreach (string pageType in pageTypes)
                            {
                                if (pageType != string.Empty)
                                {
                                    PageType pageTypeItem = new PageType();
                                    pageTypeItem.PageTypeName = pageType;
                                    page.PageTypes.Add(pageTypeItem);
                                }
                            }
                        }

                        currentItem.Pages.Add(page);
                        currentPage = page;
                    }
                    else
                    {
                        currentPage.PageNumbers.Add(new PageNumber(pageDetail.Prefix, pageDetail.Number));
                    }
                }
            }

            return name;
        }

        public CustomGenericList<Name> NameSearch(string name)
        {
            if (name == String.Empty)
            {
                throw new Exception("Please supply a name for which to search.");
            }

            CustomGenericList<Name> names = new CustomGenericList<Name>();

            if (_useElasticSearch)
            {
                // Submit the request to ElasticSearch
                ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
                search.StartPage = 1;
                search.NumResults = 10000;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["NameResultDefaultSort"]);
                ISearchResult result = search.SearchName(name);

                // Build the list of results
                foreach (NameHit hit in result.Names)
                {
                    names.Add(new Name { NameConfirmed = hit.Name });
                }
            }
            else
            {
                names = new Api3DAL().NameResolvedSelectByNameLike(null, null, name);
            }

            return names;
        }

        public CustomGenericList<Page> PageSearch(string itemID, string text)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new Exception("itemID (" + itemID + ") must be a valid integer value.");
            }
            if (text == String.Empty)
            {
                throw new Exception("Please supply text for which to search.");
            }

            CustomGenericList<Page> pages = new CustomGenericList<Page>();

            if (_useElasticSearch)
            {
                // Submit the request to ElasticSearch
                ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
                search.StartPage = 1;
                search.NumResults = 10000;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PageResultDefaultSort"]);

                List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
                Tuple<SearchField, string> itemLimit = new Tuple<SearchField, string>(SearchField.ItemID, itemID.ToString());
                limits.Add(itemLimit);
                ISearchResult result = search.SearchPage(text ?? "", limits, true);

                // Build the list of results
                foreach (PageHit hit in result.Pages)
                {
                    Page page = new Page {
                        PageID = Convert.ToInt32(hit.Id),
                        ItemID = itemIDint,
                        PageUrl = "https://www.biodiversitylibrary.org/pageocr/" + hit.Id,
                        ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + hit.Id,
                        FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + hit.Id,
                        OcrUrl = "https://www.biodiversitylibrary.org/pageocr/" + hit.Id,
                        OcrText = hit.Text
                    };

                    if (hit.PageTypes.Count > 0) page.PageTypes = new CustomGenericList<PageType>();
                    foreach(string pageType in hit.PageTypes)
                    {
                        page.PageTypes.Add(new PageType { PageTypeName = pageType });
                    }

                    if (hit.pageIndicators.Count > 0) page.PageNumbers = new CustomGenericList<PageNumber>();
                    foreach(string pageIndicator in hit.pageIndicators)
                    {
                        page.PageNumbers.Add(new PageNumber { Number = pageIndicator });
                    }

                    pages.Add(page);
                }
            }
            else
            {
                // There is no non-ElasticSearch implementation
                throw new NotImplementedException();
            }

            return pages;
        }

        /// <summary>
        /// Validate the startRow and batchSize parameters used by the NameList* web methods.
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="batchSize"></param>
        /// <param name="startRowValid"></param>
        /// <param name="batchSizeValid"></param>
        private void ValidateNameListStartAndBatch(string startRow, string batchSize, out int startRowValid, out int batchSizeValid)
        {
            // Validate the input
            double startRowDouble;
            double batchSizeDouble;
            if (!Double.TryParse(startRow, out startRowDouble))
            {
                throw new Exception("startRow (" + startRow + ") must be a valid integer value.");
            }
            else
            {
                startRowValid = (int)Math.Floor(startRowDouble);
            }
            if (!Double.TryParse(batchSize, out batchSizeDouble))
            {
                throw new Exception("batchSize (" + batchSize + ") must be a valid integer value.");
            }
            else
            {
                batchSizeValid = (int)Math.Floor(batchSizeDouble);
            }

            if (batchSizeValid > 1000)
            {
                throw new Exception("batchSize (" + batchSize + ") must be between 1 and 1000.");
            }
        }

        #endregion Name Services

        #region Language methods

        public CustomGenericList<Language> GetLanguages()
        {
            // Get the languages from the DAL
            return new Api3DAL().LanguageSelectWithPublishedItems(null, null);
        }

        #endregion Language methods

        #region Collection methods

        public CustomGenericList<Collection> GetCollections()
        {
            // Get the collections from the DAL
            return new Api3DAL().CollectionSelectActive(null, null);
        }

        #endregion Collection methods

        #region Search methods

        public CustomGenericList<Publication> SearchPublication(string title, string authorLastName, string volume,
            string year, string subject, string languageCode, string collectionID, bool fullText)
        {
            throw new NotImplementedException();
        }

        /*
        public CustomGenericList<Title> SearchBook(string title, string authorLastName, string volume, string edition,
            string year, string subject, string languageCode, string collectionID, int returnCount, bool fullText)
        {
            // Validate the parameters
            if (title.Trim() == string.Empty && authorLastName.Trim() == string.Empty && collectionID.Trim() == string.Empty)
            {
                throw new Exception("Please specific a title, author last name, or collection ID for which to search.");
            }

            int yearInt = 0;
            if (year != string.Empty)
            {
                if (!Int32.TryParse(year, out yearInt))
                {
                    throw new Exception("year (" + year + ") must be a valid integer value.");
                }
            }

            int collectionIDint = 0;
            if (collectionID != string.Empty)
            {
                if (!Int32.TryParse(collectionID, out collectionIDint))
                {
                    throw new Exception("collection (" + collectionID + ") must be a valid integer value.");
                }
            }

            Api2DAL dal = new Api2DAL();

            // Get the list of books
            CustomGenericList<SearchBookResult> books = null;
            if (fullText)
            {
                books = dal.SearchBookFullText(null, null, title, authorLastName, volume, edition,
                    (year == string.Empty ? null : (int?)yearInt), subject, languageCode,
                    (collectionID == string.Empty ? null : (int?)collectionIDint), returnCount);
            }
            else
            {
                books = dal.SearchBook(null, null, title, authorLastName, volume, edition,
                    (year == string.Empty ? null : (int?)yearInt), subject, languageCode,
                    (collectionID == string.Empty ? null : (int?)collectionIDint), returnCount);
            }

            // Build the list of titles (with item, creator, and collection information) to return
            CustomGenericList<Title> titles = new CustomGenericList<Title>();
            int prevTitleID = 0;
            Title currentTitle = null;
            foreach (SearchBookResult book in books)
            {
                if (book.TitleID != prevTitleID)
                {
                    prevTitleID = book.TitleID;
                    currentTitle = new Title();
                    currentTitle.Items = new CustomGenericList<Item>();

                    currentTitle.TitleID = book.TitleID;
                    currentTitle.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + book.TitleID.ToString();
                    currentTitle.FullTitle = book.FullTitle;
                    currentTitle.PartNumber = book.PartNumber;
                    currentTitle.PartName = book.PartName;
                    currentTitle.Edition = book.EditionStatement;
                    currentTitle.PublisherPlace = book.PublisherPlace;
                    currentTitle.PublisherName = book.PublisherName;
                    currentTitle.PublicationDate = book.PublicationDate;
                    currentTitle.Authors = dal.AuthorSelectByTitleID(null, null, book.TitleID);

                    CustomGenericList<Collection> titleCollections = dal.CollectionSelectByTitleID(null, null, book.TitleID);
                    foreach (Collection collection in titleCollections)
                    {
                        if (currentTitle.Collections == null) currentTitle.Collections = new CustomGenericList<Collection>();
                        Collection newCollection = new Collection();
                        newCollection.CollectionID = collection.CollectionID;
                        newCollection.CollectionName = collection.CollectionName;
                        currentTitle.Collections.Add(newCollection);
                    }

                    titles.Add(currentTitle);
                }

                Item newItem = new Item();

                newItem.ItemID = book.ItemID;
                newItem.ItemUrl = "https://www.biodiversitylibrary.org/item/" + book.ItemID.ToString();
                newItem.Volume = book.Volume;
                newItem.HoldingInstitution = book.HoldingInstitution;

                CustomGenericList<Collection> itemCollections = dal.CollectionSelectByItemID(null, null, book.ItemID);
                foreach (Collection collection in itemCollections)
                {
                    if (newItem.Collections == null) newItem.Collections = new CustomGenericList<Collection>();
                    Collection newCollection = new Collection();
                    newCollection.CollectionID = collection.CollectionID;
                    newCollection.CollectionName = collection.CollectionName;
                    newItem.Collections.Add(newCollection);
                }

                currentTitle.Items.Add(newItem);
            }

            return titles;
        }

        public CustomGenericList<Part> SearchSegment(string title, string containerTitle, string author, string date, 
            string volume, string series, string issue, int returnCount, string sortBy, bool fullText)
        {
            // Validate the parameters
            if (title.Trim() == string.Empty && author.Trim() == string.Empty)
            {
                throw new Exception("Please specific a title or author last name for which to search.");
            }

            Api2DAL dal = new Api2DAL();

            // Get the list of segments
            CustomGenericList<Part> parts = null;
            if (fullText)
            {
                parts = dal.SearchSegmentFullText(null, null, title, containerTitle, author, date, volume, 
                    series, issue, returnCount, sortBy);
            }
            else
            {
                parts = dal.SearchSegment(null, null, title, containerTitle, author, date, volume, 
                    series, issue, returnCount, sortBy);
            }

            foreach (Part part in parts)
            {
                part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                part.Identifiers = dal.SegmentIdentifierSelectBySegmentID(null, null, part.PartID);
                part.Subjects = dal.SubjectSelectBySegmentID(null, null, part.PartID);
                part.Pages = this.GetSegmentPages(part.PartID);
                part.RelatedParts = dal.SegmentSelectRelated(null, null, part.PartID);
                foreach (Part relatedPart in part.RelatedParts)
                {
                    relatedPart.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, relatedPart.PartID, InstitutionRole.Contributor);
                }
                part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
            }

            return parts;
        }
        */

        #endregion Search methods

        #region Validation methods

        // Numeric values assigned to the enum values should correspond to the values
        // in the RequestTypeID column of the MOBOTAdmin.dbo.RequestType database table
        public enum APIRequestType
        {
            GetTitleMetadata = 400,
            GetTitleByIdentifier = 401,
            GetTitleItems = 402,
            GetItemMetadata = 410,
            GetItemByIdentifier = 411,
            GetItemParts = 412,
            GetPageMetadata = 420,
            GetPageNames = 421,
            GetPageOcrText = 422,
            GetPartMetadata = 430,
            GetPartByIdentifier = 431,
            GetPartNames = 432,
            GetAuthorPublications = 440,
            GetSubjectPublications = 450,
            GetNameDetail = 460,
            GetNameDetailByIdentifier = 461,
            AuthorSearch = 470,
            SubjectSearch = 471,
            NameSearch = 472,
            TitleSearchSimple = 473,
            PublicationSearch = 474,
            PageSearch = 475,
            GetLanguages = 480,
            GetInstitutions = 481,
            GetCollections = 482
        }

        /// <summary>
        /// Validate the user
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ValidateApiUser(string key)
        {
            int userID;
            bool isValid = ValidateUser(key, out userID);
            return isValid;
        }

        /// <summary>
        /// Validate the user and log the request
        /// </summary>
        /// <param name="requestTypeID"></param>
        /// <param name="key"></param>
        /// <param name="ipAddress"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public bool ValidateApiUser(APIRequestType requestTypeID, string key, string ipAddress, string detail)
        {
            int userID = 0;

            // Validate the API user, and get the associated user ID
            bool isValid = ValidateUser(key, out userID);
            if (!isValid) detail = "[INVALID USER] " + detail;

            // Log the request.
            new RequestLog().SaveRequestLog(_apiApplicationID, ipAddress, userID, (int)requestTypeID, detail);

            return isValid;
        }

        private bool ValidateUser(string key, out int userID)
        {
            bool isValid = false;
            userID = 0;

            // Validate the API user, and get the associated user ID
            ApiKey apiKey = null;
            try
            {
                Guid apiKeyValue = new Guid(key);
                apiKey = new Api3DAL().ApiKeySelectByKey(null, null, apiKeyValue);
            }
            catch
            {
                // Do nothing... most likely an invalid key value... just allow apiKey to remain null
            }

            if (apiKey != null)
            {
                isValid = (apiKey.IsActive == 1);   // make sure user is active
                if (isValid) userID = apiKey.ApiKeyID;
            }

            return isValid;
        }

        #endregion Validation methods

        #region Institution methods

        public CustomGenericList<Institution> GetInstitutions()
        {
            Api3DAL dal = new Api3DAL();
            CustomGenericList<Institution> institutions = dal.InstitutionSelectAll(null, null);
            return institutions;
        }

        #endregion Institution methods
    }
}
