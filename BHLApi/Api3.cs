using BHL.Search;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDAL;
using MOBOT.BHL.API.BHLApiDataObjects3;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public CustomGenericList<Page> GetPageMetadata(string pageID, string includeOcr, string includeNames)
        {
            // Validate the parameters
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new InvalidApiParamException("pageID (" + pageID + ") must be a valid integer value.");
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
                page.OcrUrl = "https://www.biodiversitylibrary.org/pagetext/" + page.PageID.ToString();
                page.PageNumbers = dal.IndicatedPageSelectByPageID(null, null, pageIDInt);
                page.PageTypes = dal.PageTypeSelectByPageID(null, null, pageIDInt);

                if (ocr) page.OcrText = this.GetPageOcrText(pageID);
                if (names) page.Names = this.GetPageNames(pageID);
            }

            return new CustomGenericList<Page> { page };
        }

        private CustomGenericList<Name> GetPageNames(string pageID)
        {
            // Validate the page identifier
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new InvalidApiParamException("pageID (" + pageID + ") must be a valid integer value.");
            }

            // Get the names from the DAL
            return new Api3DAL().NamePageSelectByPageID(null, null, pageIDInt);
        }

        private string GetPageOcrText(string pageID)
        {
            // Validate the parameters
            int pageIDInt;
            if (!Int32.TryParse(pageID, out pageIDInt))
            {
                throw new InvalidApiParamException("pageID (" + pageID + ") must be a valid integer value.");
            }

            System.Net.WebClient client = new System.Net.WebClient();
            string text = string.Empty;
            try
            {
                client.Encoding = Encoding.UTF8;
                text = client.DownloadString("https://www.biodiversitylibrary.org/pagetext/" + pageID);
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

        public CustomGenericList<Item> GetItemMetadata(string id, string idType, string includePages)
        {
            return this.GetItemMetadata(id, idType, includePages, "f", "f");
        }

        public CustomGenericList<Item> GetItemMetadata(string id, string idType, string includePages, string includeOcr, string includeSegments)
        {
            CustomGenericList<Item> items = null;

            // Validate the parameters
            if (string.IsNullOrWhiteSpace(idType)) idType = "bhl";

            // Validate the parameters
            int itemID = 0;
            if (idType.ToLower() == "bhl")
            {
                if (!Int32.TryParse(id, out itemID))
                {
                    throw new InvalidApiParamException("id (" + id + ") of type 'bhl' must be a valid integer value.");
                }
            }

            // "t" or "true" are acceptable values for the "includePages" argument; anything else
            // is considering a value of "false"
            includePages = (includePages ?? "");
            bool pages = (includePages.ToLower() == "t" || includePages.ToLower() == "true");

            // "t" or "true" are acceptable values for the "includeSegments" argument; anything else
            // is considering a value of "false"
            includeSegments = (includeSegments ?? "");
            bool segments = (includeSegments.ToLower() == "t" || includeSegments.ToLower() == "true");

            // Retrieve the basic item metadata
            Api3DAL dal = new Api3DAL();
            switch (idType.ToLower())
            {
                case "bhl":
                    items = dal.ItemSelectByItemID(null, null, itemID);
                    break;
                case "ia":
                    items = dal.ItemSelectByBarcode(null, null, id);
                    break;
                default:
                    throw new InvalidApiParamException("idType must be one of the following values: bhl, ia");
            }

            // Add the extended metadata
            foreach(Item item in items)
            {
                item.ItemUrl = "https://www.biodiversitylibrary.org/item/" + item.ItemID.ToString();
                item.TitleUrl = (item.TitleID == null) ? null : "https://www.biodiversitylibrary.org/bibliography/" + item.TitleID.ToString();
                item.ItemThumbUrl = (item.ThumbnailPageID == null) ? null : "https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();
                item.ItemTextUrl = "https://www.biodiversitylibrary.org/itemtext/" + item.ItemID.ToString();
                item.ItemPDFUrl = "https://www.biodiversitylibrary.org/itempdf/" + item.ItemID.ToString();
                item.ItemImagesUrl = "https://www.biodiversitylibrary.org/itemimages/" + item.ItemID.ToString();

                CustomGenericList<Contributor> scanningInstitutions = dal.InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Scanning Institution");
                if (scanningInstitutions.Count > 0) item.ScanningInstitution = scanningInstitutions[0].ContributorName;
                CustomGenericList<Contributor> rightsHolders = dal.InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Rights Holder");
                if (rightsHolders.Count > 0) item.RightsHolder = rightsHolders[0].ContributorName;

                if (pages) item.Pages = this.GetItemPages(item.ItemID.ToString(), includeOcr);
                if (segments) item.Parts = this.GetItemSegments(item.ItemID.ToString());
            }

            return items;
        }

        private CustomGenericList<Page> GetItemPages(string itemID, string includeOcr)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new InvalidApiParamException("itemID (" + itemID + ") must be a valid integer value.");
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
                page.TextSource = pageDetail.TextSource;
                page.PageUrl = "https://www.biodiversitylibrary.org/page/" + page.PageID.ToString();
                page.ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + page.PageID.ToString();
                page.FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + page.PageID.ToString();
                page.OcrUrl = "https://www.biodiversitylibrary.org/pagetext/" + page.PageID.ToString();
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
                    string[] pageNumbers = pageDetail.PageNumbers.Split('|');
                    foreach (string pageNumber in pageNumbers)
                    {
                        string pageNumberTrimmed = pageNumber.Trim();
                        if (pageNumberTrimmed != string.Empty)
                        {
                            PageNumber number = new PageNumber();
                            int pos = pageNumberTrimmed.IndexOf('%');
                            if (pos > 0)
                            {
                                number.Prefix = pageNumberTrimmed.Substring(0, pos).Trim();
                                number.Number = pageNumberTrimmed.Substring(pos+1).Trim();
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

        private CustomGenericList<Part> GetItemSegments(string itemID)
        {
            // Validate the parameters
            int itemIDint;
            if (!Int32.TryParse(itemID, out itemIDint))
            {
                throw new InvalidApiParamException("itemID (" + itemID + ") must be a valid integer value.");
            }

            Api3DAL dal = new Api3DAL();
            CustomGenericList<Part> detailedParts = dal.SegmentSelectByItemID(null, null, itemIDint);
            CustomGenericList<Part> simpleParts = detailedParts.Count == 0 ? null : new CustomGenericList<Part>();
            foreach (Part detailedPart in detailedParts)
            {
                Part simplePart = new Part();
                simplePart.PartID = detailedPart.PartID;
                simplePart.PartUrl = "https://www.biodiversitylibrary.org/part/" + simplePart.PartID.ToString();
                simplePart.Genre = detailedPart.Genre;
                simplePart.Title = detailedPart.Title;
                simplePart.ContainerTitle = detailedPart.ContainerTitle;
                simplePart.Volume = detailedPart.Volume;
                simplePart.Series = detailedPart.Series;
                simplePart.Issue = detailedPart.Issue;
                simplePart.Date = detailedPart.Date;
                simplePart.PageRange = detailedPart.PageRange;
                simplePart.ExternalUrl = detailedPart.ExternalUrl;
                simplePart.Authors = detailedPart.Authors;
                //part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                //part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
                simpleParts.Add(simplePart);
            }

            return simpleParts;
        }

        #endregion Item methods

        #region Title methods

        public CustomGenericList<Title> GetTitleMetadata(string id, string idType, string includeItems)
        {
            CustomGenericList<Title> titles = null;

            // Validate the parameters
            if (string.IsNullOrWhiteSpace(idType)) idType = "bhl";

            int titleID = 0;
            if (idType.ToLower() == "bhl")
            {
                if (!Int32.TryParse(id, out titleID))
                {
                    throw new InvalidApiParamException("id (" + id + ") of type 'bhl' must be a valid integer value.");
                }
            }

            // "t" or "true" are acceptable values for the "includeItems" argument; anything else
            // is considering a value of "false"
            bool items = (includeItems.ToLower() == "t" || includeItems.ToLower() == "true");

            // Retrieve the basic title metadata
            Api3DAL dal = new Api3DAL();
            switch (idType.ToLower())
            {
                case "bhl":
                    titles = dal.TitleSelectAuto(null, null, titleID);
                    break;
                case "oclc":
                case "issn":
                case "isbn":
                case "lccn":
                case "ddc":
                case "nal":
                case "nlm":
                case "coden":
                case "soulsby":
                    if (idType.ToLower() == "lccn") idType = "dlc";
                    titles = dal.TitleSelectByIdentifier(null, null, idType, id);
                    break;
                case "doi":
                    titles = dal.TitleSelectByDOI(null, null, id);
                    break;
                default:
                    throw new InvalidApiParamException("idType must be one of the following values: bhl, doi, oclc, issn, isbn, lccn, ddc, nal, nlm, coden, soulsby");
            }

            // Add the extended metadata
            foreach (Title title in titles)
            {
                title.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
                title.Identifiers = dal.TitleIdentifierSelectByTitleID(null, null, title.TitleID);
                title.Variants = dal.TitleVariantSelectByTitleID(null, null, title.TitleID);
                title.Subjects = dal.SubjectSelectByTitleID(null, null, title.TitleID);
                title.Notes = dal.TitleNoteSelectByTitleID(null, null, title.TitleID);
                if (items) title.Items = this.GetTitleItems(title.TitleID.ToString());
            }

            return titles;
        }

        private CustomGenericList<Item> GetTitleItems(string titleID)
        {
            // Validate the parameters
            int titleIDint;
            if (!Int32.TryParse(titleID, out titleIDint))
            {
                throw new InvalidApiParamException("titleID (" + titleID + ") must be a valid integer value.");
            }

            // Get the items
            CustomGenericList<Item> detailedItems = new Api3DAL().ItemSelectByTitleID(null, null, titleIDint);
            CustomGenericList<Item> simpleItems = detailedItems.Count == 0 ? null : new CustomGenericList<Item>();
            foreach (Item detailedItem in detailedItems)
            {
                Item simpleItem = new Item();
                simpleItem.ItemID = detailedItem.ItemID;
                simpleItem.ItemUrl = "https://www.biodiversitylibrary.org/item/" + simpleItem.ItemID.ToString();
                simpleItem.Volume = detailedItem.Volume;
                simpleItem.Year = detailedItem.Year;
                simpleItem.ExternalUrl = detailedItem.ExternalUrl;
                //CustomGenericList<Contributor> scanningInstitutions = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Scanning Institution");
                //if (scanningInstitutions.Count > 0) item.ScanningInstitution = scanningInstitutions[0].ContributorName;
                //CustomGenericList<Contributor> rightsHolders = new Api3DAL().InstitutionSelectByItemIDAndRole(null, null, item.ItemID, "Rights Holder");
                //if (rightsHolders.Count > 0) item.RightsHolder = rightsHolders[0].ContributorName;
                //item.TitleUrl = (item.TitleID == null) ? null : "https://www.biodiversitylibrary.org/bibliography/" + item.TitleID.ToString();
                //item.ItemThumbUrl = (item.ThumbnailPageID == null) ? null : "https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();
                simpleItems.Add(simpleItem);
            }

            return simpleItems;
        }

        #endregion Title methods

        #region Segment methods

        public CustomGenericList<Part> GetSegmentMetadata(string id, string idType, string includePages, string includeNames)
        {
            CustomGenericList<Part> parts = null;

            // Validate the parameters
            if (string.IsNullOrWhiteSpace(idType)) idType = "bhl";

            int segmentID = 0;
            if (idType.ToLower() == "bhl")
            {
                if (!Int32.TryParse(id, out segmentID))
                {
                    throw new InvalidApiParamException("id (" + id + ") of type 'bhl' must be a valid integer value.");
                }
            }

            // "t" or "true" are acceptable values for the "includePages" and "includeNames" arguments; 
            // anything else is considered a value of "false"
            includePages = (includePages ?? "");
            bool pages = (includePages.ToLower() == "t" || includePages.ToLower() == "true");
            includeNames = (includeNames ?? "");
            bool names = (includeNames.ToLower() == "t" || includeNames.ToLower() == "true");

            // Retrieve the basic part metadata
            Api3DAL dal = new Api3DAL();
            switch (idType.ToLower())
            {
                case "bhl":
                    parts = dal.SegmentSelectForSegmentID(null, null, segmentID);
                    break;
                //case "oclc":
                //case "issn":
                //case "isbn":
                //case "lccn":
                //case "ddc":
                //case "nal":
                //case "nlm":
                //case "coden":
                case "biostor":
                case "jstor":
                case "soulsby":
                    //if (idType.ToLower() == "lccn") idType = "dlc";
                    parts = dal.SegmentSelectByIdentifier(null, null, idType, id);
                    break;
                case "doi":
                    parts = dal.SegmentSelectByDOI(null, null, id);
                    break;
                default:
                    //throw new InvalidApiParamException("idType  must be one of the following values: bhl, doi, oclc, issn, isbn, lccn, ddc, nal, nlm, coden, biostor, soulsby");
                    throw new InvalidApiParamException("idType  must be one of the following values: bhl, doi, biostor, jstor, soulsby");
            }

            // Add the extended metadata
            foreach (Part part in parts)
            {
                part.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID.ToString();
                part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);
                part.Identifiers = dal.SegmentIdentifierSelectBySegmentID(null, null, part.PartID);
                part.Subjects = dal.SubjectSelectBySegmentID(null, null, part.PartID);
                if (pages) part.Pages = this.GetSegmentPages(part.PartID);
                part.RelatedParts = dal.SegmentSelectRelated(null, null, part.PartID);
                foreach (Part relatedPart in part.RelatedParts)
                {
                    relatedPart.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, relatedPart.PartID, InstitutionRole.Contributor);
                }
                part.Contributors = dal.InstitutionSelectBySegmentIDAndRole(null, null, part.PartID, InstitutionRole.Contributor);
                if (names) part.Names = this.GetSegmentNames(part.PartID.ToString());
            }

            return parts;
        }

        private CustomGenericList<Name> GetSegmentNames(string segmentID)
        {
            // Validate the page identifier
            int segmentIDInt;
            if (!Int32.TryParse(segmentID, out segmentIDInt))
            {
                throw new InvalidApiParamException("segmentID (" + segmentID + ") must be a valid integer value.");
            }

            // Get the names from the DAL
            return new Api3DAL().NameSegmentSelectBySegmentID(null, null, segmentIDInt);
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
                page.TextSource = pageDetail.TextSource;
                page.PageUrl = "https://www.biodiversitylibrary.org/page/" + page.PageID.ToString();
                page.ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + page.PageID.ToString();
                page.FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + page.PageID.ToString();
                page.OcrUrl = "https://www.biodiversitylibrary.org/pagetext//" + page.PageID.ToString();
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
                    string[] pageNumbers = pageDetail.PageNumbers.Split('|');
                    foreach (string pageNumber in pageNumbers)
                    {
                        string pageNumberTrimmed = pageNumber.Trim();
                        if (pageNumberTrimmed != string.Empty)
                        {
                            PageNumber number = new PageNumber();
                            int pos = pageNumberTrimmed.IndexOf('%');
                            if (pos > 0)
                            {
                                number.Prefix = pageNumberTrimmed.Substring(0, pos).Trim();
                                number.Number = pageNumberTrimmed.Substring(pos+1).Trim();
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

        public CustomGenericList<Subject> GetSubjectMetadata(string keyword, string includePubs)
        {
            CustomGenericList<Subject> subjects = null;

            // Validate the parameters

            // "t" or "true" are acceptable values for the "includePubs" argument; anything else
            // is considered a value of "false"
            includePubs = (includePubs ?? "");
            bool pubs = (includePubs.ToLower() == "t" || includePubs.ToLower() == "true");

            // Retrieve the basic part metadata
            Api3DAL dal = new Api3DAL();
            subjects = dal.KeywordSelectByKeyword(null, null, keyword);

            // Add the extended metadata
            foreach (Subject subject in subjects)
            {
                if (pubs) subject.Publications = this.GetSubjectPublications(keyword);
            }

            return subjects;
        }

        private CustomGenericList<Publication> GetSubjectPublications(string subject)
        {
            Api3DAL dal = new Api3DAL();
            CustomGenericList<Title> titles = dal.TitleSelectByKeyword(null, null, subject);
            CustomGenericList<Part> parts = dal.SegmentSelectByKeyword(null, null, subject);

            foreach (Title title in titles) title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
            foreach (Part part in parts) part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);

            CustomGenericList<Publication> pubs = BuildPublicationList(titles);
            CustomGenericList<Publication> partPubs = BuildPublicationList(parts);
            foreach (Publication pub in partPubs) pubs.Add(pub);

            return pubs;
        }

        #endregion Subject methods

        #region Author methods

        public CustomGenericList<Author> GetAuthorMetadata(string id, string idType, string includePubs)
        {
            CustomGenericList<Author> authors = null;

            // Validate the parameters
            if (string.IsNullOrWhiteSpace(idType)) idType = "bhl";

            int authorID = 0;
            if (idType.ToLower() == "bhl")
            {
                if (!Int32.TryParse(id, out authorID))
                {
                    throw new InvalidApiParamException("id (" + id + ") of type 'bhl' must be a valid integer value.");
                }
            }

            // "t" or "true" are acceptable values for the "includePubs" argument; anything else
            // is considered a value of "false"
            includePubs = (includePubs?? "");
            bool pubs = (includePubs.ToLower() == "t" || includePubs.ToLower() == "true");

            // Retrieve the basic part metadata
            Api3DAL dal = new Api3DAL();
            switch (idType.ToLower())
            {
                case "bhl":
                    authors = dal.AuthorSelectByAuthorID(null, null, authorID);
                    break;
                case "biostor":
                case "viaf":
                    authors = dal.AuthorSelectByIdentifier(null, null, idType, id);
                    break;
                default:
                    throw new InvalidApiParamException("idType  must be one of the following values: bhl, biostor, viaf");
            }

            // Add the extended metadata
            foreach (Author author in authors)
            {
                author.CreatorUrl = "https://www.biodiversitylibrary.org/creator/" + author.AuthorID.ToString();
                author.Identifiers = dal.AuthorIdentifierSelectByAuthorID(null, null, Convert.ToInt32(author.AuthorID));
                if (author.Identifiers.Count == 0) author.Identifiers = null;
                if (pubs) author.Publications = this.GetAuthorPublications(author.AuthorID.ToString());
            }

            return authors;
        }

        private CustomGenericList<Publication> GetAuthorPublications(string creatorID)
        {
            // Validate the parameters
            int creatorIDint;
            if (!Int32.TryParse(creatorID, out creatorIDint))
            {
                throw new InvalidApiParamException("creatorID (" + creatorID + ") must be a valid integer value.");
            }

            Api3DAL dal = new Api3DAL();
            CustomGenericList<Title> titles = dal.TitleSelectByAuthor(null, null, creatorIDint);
            CustomGenericList<Part> parts = dal.SegmentSelectByAuthor(null, null, creatorIDint);

            foreach (Title title in titles) title.Authors = dal.AuthorSelectByTitleID(null, null, title.TitleID);
            foreach (Part part in parts) part.Authors = dal.AuthorSelectBySegmentID(null, null, part.PartID);

            CustomGenericList<Publication> pubs = BuildPublicationList(titles);
            CustomGenericList<Publication> partPubs = BuildPublicationList(parts);
            foreach (Publication pub in partPubs) pubs.Add(pub);

            return pubs;
        }

        #endregion Author methods

        #region Name Services

        public CustomGenericList<Name> GetNameMetadata(string nameConfirmed, string idType, string id)
        {
            Name name = null;

            if (!string.IsNullOrWhiteSpace(nameConfirmed))
            {
                name = this.GetNameDetail(nameConfirmed);
            }
            else
            {
                name = this.GetNameDetailByIdentifier(idType, id);
            }

            return new CustomGenericList<Name> { name };
        }

        private Name GetNameDetail(string nameConfirmed)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(nameConfirmed)) throw new InvalidApiParamException("Please supply a Name.");

            CustomGenericList<PageDetail> pageDetails = null;
            pageDetails = new Api3DAL().PageSelectByNameConfirmed(null, null, nameConfirmed);

            return GetNameDetailFromPageDetails(pageDetails);
        }

        private Name GetNameDetailByIdentifier(string identifierType, string identifierValue)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(identifierType) || string.IsNullOrWhiteSpace(identifierValue)) throw new InvalidApiParamException("Please supply an identifier Type and Value.");

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

            if (string.IsNullOrWhiteSpace(identifierName)) throw new InvalidApiParamException("Please supply one of the following identifier Types: namebank, eol, gni, ion, col, gbif, itis, ipni, worms.");

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
                name.Identifiers = new Api3DAL().NameIdentifierSelectByNameResolvedID(null, null, pageDetails[0].NameResolvedID);
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
                        page.TextSource = pageDetail.TextSource;
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
                throw new InvalidApiParamException("startRow (" + startRow + ") must be a valid integer value.");
            }
            else
            {
                startRowValid = (int)Math.Floor(startRowDouble);
            }
            if (!Double.TryParse(batchSize, out batchSizeDouble))
            {
                throw new InvalidApiParamException("batchSize (" + batchSize + ") must be a valid integer value.");
            }
            else
            {
                batchSizeValid = (int)Math.Floor(batchSizeDouble);
            }

            if (batchSizeValid > 1000)
            {
                throw new InvalidApiParamException("batchSize (" + batchSize + ") must be between 1 and 1000.");
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

        #region Institution methods

        public CustomGenericList<Institution> GetInstitutions()
        {
            Api3DAL dal = new Api3DAL();
            CustomGenericList<Institution> institutions = dal.InstitutionSelectAll(null, null);
            return institutions;
        }

        #endregion Institution methods

        #region Search methods

        public CustomGenericList<Publication> SearchPublication(string searchTerm, string searchType, 
            string page, bool sqlFullText)
        {
            // Validate the parameters
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new InvalidApiParamException("Please supply a searchterm for which to search.");
            }

            if (string.IsNullOrWhiteSpace(searchType)) searchType = "F";  // Default to "F" (full-text search)
            List<string> types = new List<string> { "F", "C" };
            if (!types.Contains(searchType.ToUpper()))
            {
                throw new InvalidApiParamException("searchtype (" + searchType + ") must be one of the following values: F, C");
            }

            int pageInt = 1;
            if (page != string.Empty)
            {
                if (!Int32.TryParse(page, out pageInt))
                {
                    throw new InvalidApiParamException("page (" + page + ") must be a valid integer value.");
                }

                /*
                 * Page must be between 1 and 50.  
                 * Total results must be less than 10000.  With a page size of 200 results, 50 pages 
                 * is the maximum.  If page greater than 50 is requested, ElasticSearch returns the 
                 * following error:  
                 *  Result window is too large, from + size must be less than or equal to: [10000] 
                 *  but was [XXXXX]. See the scroll api for a more efficient way to request large 
                 *  data sets. This limit can be set by changing the [index.max_result_window] index 
                 *  level setting.
                 */
                if (pageInt < 1)
                {
                    throw new InvalidApiParamException("page (" + page + ") must be greater than zero.");
                }

                if (pageInt > 50)
                {
                    throw new InvalidApiParamException("page (" + page + ") must be less than or equal to 50.");
                }
            }

            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();
            if (_useElasticSearch)
            {
                pubs = SearchPublicationGlobal(searchTerm, searchType, pageInt);
            }
            else
            {
                pubs = SearchPublicationGlobalSQL(searchTerm, sqlFullText);
            }

            return pubs;
        }

        public CustomGenericList<Publication> SearchPublication(string title, string titleOp, 
            string authorName, string year, string subject, string languageCode, string collectionID, 
            string text, string textOp, string page, bool sqlFullText)
        {
            // Validate the parameters
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(authorName) && 
                string.IsNullOrWhiteSpace(collectionID))
            {
                throw new InvalidApiParamException("Please supply a title, author last name, or collection ID for which to search.");
            }

            if (string.IsNullOrWhiteSpace(titleOp)) titleOp = "All";  // Default to "All" (an AND search)
            switch (titleOp.ToLower())
            {
                case "all":
                    titleOp = "A";
                    break;
                case "phrase":
                    titleOp = "P";
                    break;
                default:
                    throw new InvalidApiParamException("titleop (" + titleOp + ") must be one of the following values: All, Phrase");
            }

            if (string.IsNullOrWhiteSpace(textOp)) textOp = "All";  // Default to "All" (an AND search)
            switch (textOp.ToLower())
            {
                case "all":
                    textOp = "A";
                    break;
                case "phrase":
                    textOp = "P";
                    break;
                default:
                    throw new InvalidApiParamException("textop (" + textOp + ") must be one of the following values: All, Phrase");
            }

            int yearInt = 0;
            if (year != string.Empty)
            {
                if (!Int32.TryParse(year, out yearInt))
                {
                    throw new InvalidApiParamException("year (" + year + ") must be a valid integer value.");
                }
            }

            int collectionIDint = 0;
            if (collectionID != string.Empty)
            {
                if (!Int32.TryParse(collectionID, out collectionIDint))
                {
                    throw new InvalidApiParamException("collection (" + collectionID + ") must be a valid integer value.");
                }
            }

            int pageInt = 1;
            if (page != string.Empty)
            {
                if (!Int32.TryParse(page, out pageInt))
                {
                    throw new InvalidApiParamException("page (" + page + ") must be a valid integer value.");
                }

                /*
                 * Page must be between 1 and 50.  
                 * Total results must be less than 10000.  With a page size of 200 results, 50 pages 
                 * is the maximum.  If page greater than 50 is requested, ElasticSearch returns the 
                 * following error:  
                 *  Result window is too large, from + size must be less than or equal to: [10000] 
                 *  but was [XXXXX]. See the scroll api for a more efficient way to request large 
                 *  data sets. This limit can be set by changing the [index.max_result_window] index 
                 *  level setting.
                 */
                if (pageInt < 1)
                {
                    throw new InvalidApiParamException("page (" + page + ") must be greater than zero.");
                }

                if (pageInt > 50)
                {
                    throw new InvalidApiParamException("page (" + page + ") must be less than or equal to 50.");
                }
            }

            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();
            if (_useElasticSearch)
            {
                pubs = SearchPublicationAdvanced(title, titleOp, authorName, year, subject, languageCode,
                    collectionID, text, textOp, pageInt);
            }
            else
            {
                pubs = SearchPublicationAdvancedSQL(title, authorName, yearInt, subject, 
                    languageCode, collectionIDint, sqlFullText);
            }

            return pubs;
        }

        /// <summary>
        /// Use the search engine to perform an advanced search
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titleOp"></param>
        /// <param name="authorName"></param>
        /// <param name="volume"></param>
        /// <param name="year"></param>
        /// <param name="subject"></param>
        /// <param name="languageCode"></param>
        /// <param name="collectionID"></param>
        /// <param name="text"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> SearchPublicationAdvanced(string title, string titleOp,
            string authorName, string year, string subject, string languageCode, string collectionID, 
            string text, string textOp, int page)
        {
            // Build the language and collection parameters
            Tuple<string, string> languageParam = null;
            Tuple<string, string> collectionParam = null;

            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                languageParam = new Tuple<string, string>(languageCode,
                    new Api3DAL().GetLanguageName(languageCode));
            }
            if (!string.IsNullOrWhiteSpace(collectionID))
            {
                collectionParam = new Tuple<string, string>(collectionID,
                    new Api3DAL().GetCollectionName(collectionID));
            }

            // Submit the request to ElasticSearch
            ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
            search.StartPage = page;
            search.NumResults = 200;
            if (!string.IsNullOrWhiteSpace(text)) search.Highlight = true;
            search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PublicationResultDefaultSort"]);

            ISearchResult result = search.SearchCatalog(
                new SearchStringParam(title, 
                    (titleOp == "A" ? SearchStringParamOperator.And : SearchStringParamOperator.Phrase)),
                new SearchStringParam(authorName, SearchStringParamOperator.And), 
                string.Empty, year,
                new SearchStringParam(subject, SearchStringParamOperator.And),
                languageParam, collectionParam,
                new SearchStringParam(text, 
                    (textOp == "A" ? SearchStringParamOperator.And : SearchStringParamOperator.Phrase)));

            // Build the list of results
            CustomGenericList<Publication> pubs = BuildPublicationList(result);

            return pubs;
        }

        /// <summary>
        /// Use the search engine to search metadata and text
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> SearchPublicationGlobal(string searchTerm, string searchType, int page)
        {
            // Submit the request to ElasticSearch
            ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
            search.StartPage = page;
            search.NumResults = 200;
            search.Highlight = true;
            search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PublicationResultDefaultSort"]);

            ISearchResult result = null;
            if (searchType.ToUpper() == "F")
            {
                // Full-text search
                result = search.SearchItem(searchTerm);
            }
            else
            {
                // Catalog search
                result = search.SearchCatalog(searchTerm);
            }

            // Build the list of results
            CustomGenericList<Publication> pubs = BuildPublicationList(result);

            return pubs;
        }

        /// <summary>
        ///  Build a list of publications from the results of a search server query
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> BuildPublicationList(ISearchResult result)
        {
            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();

            foreach (ItemHit hit in result.Items)
            {
                // Populate Publication objects
                Publication pub = new Publication();

                if (hit.TitleId != 0)
                {
                    pub.BHLType = BHLType.Item;
                    pub.TitleID = hit.TitleId.ToString();
                    pub.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + hit.TitleId;
                    pub.ItemID = (hit.ItemId == 0 ? null : hit.ItemId.ToString());
                    pub.ItemUrl = pub.ItemID == null ? null : "https://www.biodiversitylibrary.org/item/" + pub.ItemID;
                    pub.Genre = (string.IsNullOrWhiteSpace(hit.Genre) ? null : hit.Genre);
                    pub.MaterialType = (string.IsNullOrWhiteSpace(hit.MaterialType) ? null : hit.MaterialType);
                    //if (hit.Contributors.Count == 1) pub.HoldingInstitution = hit.Contributors[0];
                    pub.PublisherPlace = (string.IsNullOrWhiteSpace(hit.PublicationPlace) ? null : hit.PublicationPlace);
                    pub.PublisherName = (string.IsNullOrWhiteSpace(hit.Publisher) ? null : hit.Publisher);
                    if (hit.Dates.Count == 1) pub.PublicationDate = hit.Dates[0];
                    if (hit.Dates.Count > 1) pub.PublicationDate = hit.Dates[0] + "-" + hit.Dates[hit.Dates.Count - 1];
                }
                else
                {
                    pub.BHLType = BHLType.Part;
                    pub.PartID = hit.SegmentId.ToString();
                    //pub.StartPageID = (hit.StartPageId == 0 ? null : hit.StartPageId.ToString());
                    pub.PartUrl = "https://www.biodiversitylibrary.org/part/" + hit.SegmentId;
                    pub.ContainerTitle = (string.IsNullOrWhiteSpace(hit.Container) ? null : hit.Container);
                    pub.Genre = (string.IsNullOrWhiteSpace(hit.Genre) ? null : hit.Genre);
                    pub.PageRange = (hit.PageRange == "--" || string.IsNullOrWhiteSpace(hit.PageRange) ? null : hit.PageRange);
                    pub.Series = (string.IsNullOrWhiteSpace(hit.Series) ? null : hit.Series);
                    pub.Issue = (string.IsNullOrWhiteSpace(hit.Issue) ? null : hit.Issue);
                    if (hit.Dates.Count == 1) pub.Date = hit.Dates[0];
                    if (hit.Dates.Count > 1) pub.Date = hit.Dates[0] + "-" + hit.Dates[hit.Dates.Count - 1];

                    /*
                    foreach (string contributor in hit.Contributors)
                    {
                        if (pub.Contributors == null) pub.Contributors = new CustomGenericList<Contributor>();
                        pub.Contributors.Add(new Contributor { ContributorName = contributor });
                    }
                    */
            }

            pub.Title = hit.Title;
                pub.Volume = (string.IsNullOrWhiteSpace(hit.Volume) ? null : hit.Volume);
                //pub.Language = (string.IsNullOrWhiteSpace(hit.Language) ? null : hit.Language);
                pub.ExternalUrl = (string.IsNullOrWhiteSpace(hit.Url) ? null : hit.Url);
                //pub.Doi = (string.IsNullOrWhiteSpace(hit.Doi) ? null : hit.Doi);

                /*
                foreach (string cName in hit.Collections)
                {
                    if (pub.Collections == null) pub.Collections = new CustomGenericList<Collection>();
                    pub.Collections.Add(new Collection { CollectionName = cName });
                }
                */
                foreach (string aName in hit.Authors)
                {
                    if (pub.Authors == null) pub.Authors = new CustomGenericList<Author>();
                    pub.Authors.Add(new Author { Name = aName });
                }
                /*
                if (hit.Oclc.Count > 0)
                {
                    if (pub.Identifiers == null) pub.Identifiers = new CustomGenericList<Identifier>();
                    foreach (string id in hit.Oclc) pub.Identifiers.Add(new Identifier { IdentifierName = "OCLC", IdentifierValue = id });
                }
                if (hit.Isbn.Count > 0)
                {
                    if (pub.Identifiers == null) pub.Identifiers = new CustomGenericList<Identifier>();
                    foreach (string id in hit.Isbn) pub.Identifiers.Add(new Identifier { IdentifierName = "ISBN", IdentifierValue = id });
                }
                if (hit.Issn.Count > 0)
                {
                    if (pub.Identifiers == null) pub.Identifiers = new CustomGenericList<Identifier>();
                    foreach (string id in hit.Issn) pub.Identifiers.Add(new Identifier { IdentifierName = "ISSN", IdentifierValue = id });
                }
                */

                if (hit.Highlights.Count == 0)
                {
                    pub.FoundIn = FoundIn.Metadata;
                }
                else
                {
                    bool foundInMetadata = false;
                    bool foundInText = false;

                    foreach(Tuple<string,string> h in hit.Highlights)
                    {
                        if (h.Item1 == "text") foundInText = true;
                        else foundInMetadata = true;
                    }

                    if (foundInMetadata && foundInText) pub.FoundIn = FoundIn.Both;
                    if (foundInMetadata && !foundInText) pub.FoundIn = FoundIn.Metadata;
                    if (!foundInMetadata && foundInText) pub.FoundIn = FoundIn.Text;
                }

                pubs.Add(pub);
            }

            return pubs;
        }

        /// <summary>
        ///  Build a list of publications from a list of Titles returned by a SQL query
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> BuildPublicationList(CustomGenericList<Title> titles)
        {
            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();

            foreach (Title title in titles)
            {
                // Populate Publication objects
                Publication pub = new Publication();

                pub.BHLType = BHLType.Title;
                pub.TitleID = title.TitleID.ToString();
                pub.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                pub.Title = (title.FullTitle + " " + (title.PartNumber ?? string.Empty) + " " + (title.PartName ?? string.Empty)).Trim();
                pub.Genre = (string.IsNullOrWhiteSpace(title.Genre) ? null : title.Genre);
                pub.MaterialType = (string.IsNullOrWhiteSpace(title.MaterialType) ? null : title.MaterialType);
                pub.PublisherPlace = (string.IsNullOrWhiteSpace(title.PublisherPlace) ? null : title.PublisherPlace);
                pub.PublisherName = (string.IsNullOrWhiteSpace(title.PublisherName) ? null : title.PublisherName);
                pub.PublicationDate = (string.IsNullOrWhiteSpace(title.PublicationDate) ? null : title.PublicationDate);
                //pub.Doi = (string.IsNullOrWhiteSpace(title.Doi) ? null : title.Doi);

                /*
                if (title.Collections != null)
                {
                    foreach (Collection collection in title.Collections)
                    {
                        if (pub.Collections == null) pub.Collections = new CustomGenericList<Collection>();
                        pub.Collections.Add(new Collection
                        {
                            CollectionID = collection.CollectionID,
                            CollectionName = collection.CollectionName
                        });
                    }
                }
                */
                if (title.Authors != null)
                {
                    foreach (Author author in title.Authors)
                    {
                        if (pub.Authors == null) pub.Authors = new CustomGenericList<Author>();
                        pub.Authors.Add(new Author
                        {
                            //AuthorID = author.AuthorID,
                            Name = author.Name//,
                            //FullerForm = author.FullerForm,
                            //Location = author.Location,
                            //Role = author.Role,
                            //Title = author.Title,
                            //Unit = author.Unit,
                            //Numeration = author.Numeration,
                            //Dates = author.Dates
                        });
                    }
                }
                /*
                if (title.Identifiers != null)
                {
                    foreach (Identifier id in title.Identifiers)
                    {
                        if (pub.Identifiers == null) pub.Identifiers = new CustomGenericList<Identifier>();
                        pub.Identifiers.Add(new Identifier
                        {
                            IdentifierName = id.IdentifierName,
                            IdentifierValue = id.IdentifierValue
                        });
                    }
                }
                */
                pub.FoundIn = FoundIn.Metadata;

                pubs.Add(pub);
            }

            return pubs;
        }

        /// <summary>
        ///  Build a list of publications from a list of Parts returned by a SQL query
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> BuildPublicationList(CustomGenericList<Part> parts)
        {
            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();

            foreach (Part part in parts)
            {
                // Populate Publication objects
                Publication pub = new Publication();

                pub.BHLType = BHLType.Part;
                pub.PartID = part.PartID.ToString();
                //pub.StartPageID = part.StartPageID;
                pub.PartUrl = "https://www.biodiversitylibrary.org/part/" + part.PartID;
                pub.Title = part.Title;
                pub.ContainerTitle = (string.IsNullOrWhiteSpace(part.ContainerTitle) ? null : part.ContainerTitle);
                //pub.PublisherPlace = (string.IsNullOrWhiteSpace(part.PublisherPlace) ? null : part.PublisherPlace);
                //pub.PublisherName = (string.IsNullOrWhiteSpace(part.PublisherName) ? null : part.PublisherName);
                pub.Date = (string.IsNullOrWhiteSpace(part.Date) ? null : part.Date);
                pub.Genre = (string.IsNullOrWhiteSpace(part.Genre) ? null : part.Genre);
                pub.Volume = (string.IsNullOrWhiteSpace(part.Volume) ? null : part.Volume);
                pub.Series = (string.IsNullOrWhiteSpace(part.Series) ? null : part.Series);
                pub.Issue = (string.IsNullOrWhiteSpace(part.Issue) ? null : part.Issue);
                //pub.Language = (string.IsNullOrWhiteSpace(part.Language) ? null : part.Language);
                pub.ExternalUrl = (string.IsNullOrWhiteSpace(part.ExternalUrl) ? null : part.ExternalUrl);
                //pub.Rights = (string.IsNullOrWhiteSpace(part.RightsStatement) ? null : part.RightsStatement);
                //pub.RightsStatus = (string.IsNullOrWhiteSpace(part.RightsStatus) ? null : part.RightsStatus);
                pub.PageRange = (part.PageRange == "--" || string.IsNullOrWhiteSpace(part.PageRange) ? null : part.PageRange);
                //pub.StartPageNumber = (string.IsNullOrWhiteSpace(part.StartPageNumber) ? null : part.StartPageNumber);
                //pub.EndPageNumber = (string.IsNullOrWhiteSpace(part.EndPageNumber) ? null : part.EndPageNumber);
                //pub.Doi = (string.IsNullOrWhiteSpace(part.Doi) ? null : part.Doi);

                /*
                if (part.Contributors != null)
                {
                    foreach (string contributor in part.Contributors)
                    {
                        if (pub.Contributors == null) pub.Contributors = new CustomGenericList<Contributor>();
                        pub.Contributors.Add(new Contributor { ContributorName = contributor });
                    }
                }
                */
                if (part.Authors != null)
                {
                    foreach (Author author in part.Authors)
                    {
                        if (pub.Authors == null) pub.Authors = new CustomGenericList<Author>();
                        pub.Authors.Add(new Author
                        {
                            //AuthorID = author.AuthorID,
                            Name = author.Name//,
                            //FullerForm = author.FullerForm,
                            //Location = author.Location,
                            //Role = author.Role,
                            //Title = author.Title,
                            //Unit = author.Unit,
                            //Numeration = author.Numeration,
                            //Dates = author.Dates
                        });
                    }
                }
                /*
                if (part.Identifiers != null)
                {
                    foreach (Identifier id in part.Identifiers)
                    {
                        if (pub.Identifiers == null) pub.Identifiers = new CustomGenericList<Identifier>();
                        pub.Identifiers.Add(new Identifier
                        {
                            IdentifierName = id.IdentifierName,
                            IdentifierValue = id.IdentifierValue
                        });
                    }
                }
                */
                pub.FoundIn = FoundIn.Metadata;

                pubs.Add(pub);
            }

            return pubs;
        }

        /// <summary>
        /// Use SQL to search metadata
        /// </summary>
        /// <param name="title"></param>
        /// <param name="authorName"></param>
        /// <param name="volume"></param>
        /// <param name="year"></param>
        /// <param name="subject"></param>
        /// <param name="languageCode"></param>
        /// <param name="collectionID"></param>
        /// <param name="sqlFullText"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> SearchPublicationAdvancedSQL(string title, string authorName, 
            int year, string subject, string languageCode, int collectionID, bool sqlFullText)
        {
            CustomGenericList<Publication> pubs = new CustomGenericList<Publication>();

            // Perform a SQL search for books and articles
            CustomGenericList<Title> titles = this.SearchBook(title, authorName, "", "",
                (int?)(year == 0 ? (int?)null : year), subject,
                languageCode, (int?)(collectionID == 0 ? (int?)null : collectionID),
                200, sqlFullText);

            foreach (Title t in titles)
            {
                foreach (Item i in t.Items)
                {
                    Publication pub = new Publication();
                    pub.BHLType = BHLType.Item;
                    pub.TitleID = t.TitleID.ToString();
                    pub.ItemID = i.ItemID.ToString();
                    pub.TitleUrl = "https://www.biodiversitylibrary.org/bibliography/" + t.TitleID.ToString();
                    pub.ItemUrl = "https://www.biodiversitylibrary.org/item/" + i.ItemID.ToString();
                    pub.Title = (t.FullTitle + " " + (t.PartNumber ?? string.Empty) + " " + (t.PartName ?? string.Empty)).Trim();
                    pub.Genre = t.Genre;
                    pub.MaterialType = t.MaterialType;
                    pub.PublisherPlace = t.PublisherPlace;
                    pub.PublisherName = t.PublisherName;
                    pub.PublicationDate = t.PublicationDate;
                    pub.HoldingInstitution = i.HoldingInstitution;
                    pub.ScanningInstitution = i.ScanningInstitution;
                    pub.RightsHolder = i.RightsHolder;
                    pub.Volume = i.Volume;
                    pub.Doi = t.Doi;
                    pub.CopySpecificInformation = i.CopySpecificInformation;
                    pub.Rights = i.Rights;
                    pub.RightsStatus = i.CopyrightStatus;
                    pub.LicenseUrl = i.LicenseUrl;
                    pub.DueDiligence = i.DueDiligence;
                    if (t.Authors.Count > 0) pub.Authors = t.Authors;
                    if (i.Collections != null) pub.Collections = i.Collections;
                    pubs.Add(pub);
                }
            }

            CustomGenericList<Part> parts = this.SearchSegment(title, "", authorName, year.ToString(), "", "", "", 200, "Title", sqlFullText);

            foreach (Part p in parts)
            {
                Publication pub = new Publication();

                pub.BHLType = BHLType.Part;
                pub.PartUrl = "https://www.biodiversitylibrary.org/part/" + p.PartID;
                pub.PartID = p.PartID.ToString();
                pub.ItemID = p.ItemID;
                pub.StartPageID = p.StartPageID;
                pub.Title = p.Title;
                pub.ContainerTitle = p.ContainerTitle;
                pub.TranslatedTitle = p.TranslatedTitle;
                pub.Genre = p.Genre;
                pub.Volume = p.Volume;
                pub.Series = p.Series;
                pub.Issue = p.Issue;
                pub.PublicationDetails = p.PublicationDetails;
                pub.PublisherName = p.PublisherName;
                pub.PublisherPlace = p.PublisherPlace;
                pub.Date = p.Date;
                pub.Notes = p.Notes;
                pub.Language = p.Language;
                pub.Doi = p.Doi;
                pub.PageRange = (p.PageRange == "--" ? null : p.PageRange);
                pub.StartPageNumber = p.StartPageNumber;
                pub.EndPageNumber = p.EndPageNumber;
                pub.ExternalUrl = p.ExternalUrl;
                pub.Rights = p.RightsStatement;
                pub.RightsStatus = p.RightsStatus;
                pub.LicenseUrl = p.LicenseUrl;
                pub.LicenseName = p.LicenseName;
                if (p.Authors.Count > 0) pub.Authors = p.Authors;
                if (p.Contributors.Count > 0) pub.Contributors = p.Contributors;
                if (p.Identifiers.Count > 0) pub.Identifiers = p.Identifiers;

                pubs.Add(pub);
            }

            return pubs;
        }

        /// <summary>
        /// Use SQL to search metadata globally
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sqlFullText"></param>
        /// <returns></returns>
        private CustomGenericList<Publication> SearchPublicationGlobalSQL(string searchTerm, bool sqlFullText)
        {
            // There is no non-ElasticSearch implementation
            throw new NotImplementedException();
        }

        private CustomGenericList<Title> SearchBook(string title, string authorLastName, string volume, string edition,
            int? year, string subject, string languageCode, int? collectionID, int returnCount, bool sqlFullText)
        {
            Api3DAL dal = new Api3DAL();

            // Get the list of books
            CustomGenericList<SearchBookResult> books = null;
            if (sqlFullText)
            {
                books = dal.SearchBookFullText(null, null, title, authorLastName, volume, edition,
                    year, subject, languageCode, collectionID, returnCount);
            }
            else
            {
                books = dal.SearchBook(null, null, title, authorLastName, volume, edition,
                    year, subject, languageCode, collectionID, returnCount);
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

        private CustomGenericList<Part> SearchSegment(string title, string containerTitle, string author, string date, 
            string volume, string series, string issue, int returnCount, string sortBy, bool sqlFullText)
        {
            Api3DAL dal = new Api3DAL();

            // Get the list of segments
            CustomGenericList<Part> parts = null;
            if (sqlFullText)
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

        public CustomGenericList<Subject> SubjectSearch(string subject, bool sqlFullText)
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
                foreach (KeywordHit hit in result.Keywords)
                {
                    subjects.Add(new Subject { SubjectText = hit.Keyword });
                }
            }
            else
            {
                Api3DAL dal = new Api3DAL();
                if (sqlFullText)
                    subjects = dal.SearchTitleKeyword(null, null, subject);
                else
                    subjects = dal.TitleKeywordSelectLikeTag(null, null, subject);
            }

            return subjects;
        }

        public CustomGenericList<Author> AuthorSearch(string name, bool sqlFullText)
        {
            CustomGenericList<Author> creators = new CustomGenericList<Author>();

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
                if (sqlFullText)
                    creators = new Api3DAL().SearchAuthor(null, null, name);
                else
                    creators = new Api3DAL().AuthorSelectNameStartsWith(null, null, name);
            }

            foreach (Author creator in creators)
            {
                creator.CreatorUrl = "https://www.biodiversitylibrary.org/creator/" + creator.AuthorID.ToString();
            }

            return creators;
        }

        public CustomGenericList<Name> NameSearch(string name)
        {
            if (name == String.Empty)
            {
                throw new InvalidApiParamException("Please supply a name for which to search.");
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
                throw new InvalidApiParamException("itemID (" + itemID + ") must be a valid integer value.");
            }
            if (text == String.Empty)
            {
                throw new InvalidApiParamException("Please supply text for which to search.");
            }

            CustomGenericList<Page> pages = new CustomGenericList<Page>();
            DataTable pageIDs = new DataTable();
            pageIDs.Columns.Add(new DataColumn("ID", typeof(int)));

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
                    Page page = new Page
                    {
                        PageID = Convert.ToInt32(hit.Id),
                        ItemID = itemIDint,
                        PageUrl = "https://www.biodiversitylibrary.org/pagetext/" + hit.Id,
                        ThumbnailUrl = "https://www.biodiversitylibrary.org/pagethumb/" + hit.Id,
                        FullSizeImageUrl = "https://www.biodiversitylibrary.org/pageimage/" + hit.Id,
                        OcrUrl = "https://www.biodiversitylibrary.org/pagetext/" + hit.Id,
                        OcrText = hit.Text
                    };

                    pageIDs.Rows.Add(page.PageID);

                    if (hit.PageTypes.Count > 0) page.PageTypes = new CustomGenericList<PageType>();
                    foreach (string pageType in hit.PageTypes)
                    {
                        page.PageTypes.Add(new PageType { PageTypeName = pageType });
                    }

                    if (hit.pageIndicators.Count > 0) page.PageNumbers = new CustomGenericList<PageNumber>();
                    foreach (string pageIndicator in hit.pageIndicators)
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

            if (pages.Count > 0)
            {
                // Get additional page details from the database and add them to the results
                Api3DAL dal = new Api3DAL();
                CustomGenericList<Page> pageDetails = dal.PageSelectByPageIDList(null, null, pageIDs);

                foreach(Page pg in pages)
                {
                    foreach(Page pd in pageDetails)
                    {
                        if (pg.PageID == pd.PageID)
                        {
                            pg.TextSource = pd.TextSource;
                            pageDetails.Remove(pd);
                            break;
                        }
                    }
                }
            }

            return pages;
        }

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
            GetAuthorMetadata = 441,
            GetSubjectPublications = 450,
            GetSubjectMetadata = 451,
            GetNameDetail = 460,
            GetNameDetailByIdentifier = 461,
            GetNameMetadata = 462,
            AuthorSearch = 470,
            SubjectSearch = 471,
            NameSearch = 472,
            PublicationSearchAdvanced = 473,
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
    }
}
