using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<NameResolved> NameResolvedSelectByPageID(int pageID)
        {
            return new NameResolvedDAL().NameResolvedSelectByPageID(null, null, pageID);
        }

        public List<NameResolved> NameResolvedSelectByNameLike(string name, int returnCount)
        {
            return new NameResolvedDAL().NameResolvedSelectByNameLike(null, null, name, returnCount);
        }

        public NameResolved NameResolvedSelectByResolvedName(string name)
        {
            return new NameResolvedDAL().NameResolvedSelectByResolvedName(null, null, name);
        }

        public NameSearchResult NameResolvedSearchForPages(string name, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            List<CustomDataRow> data = new NameResolvedDAL().NameResolvedSearchForPages(null, null,
                name, numberOfRows, pageNumber, sortColumn, sortDirection);

            NameSearchResult result = new NameSearchResult();
            result.QueryDate = DateTime.Now.ToString("dd MMM yyyy h:mmtt");

            //loop through the results and create a list of NameSearchPage objects
            foreach (CustomDataRow row in data)
            {
                NameSearchPage page = new NameSearchPage();
                page.TitleID = (int)row["TitleID"].Value;
                page.PageID = (int)row["PageID"].Value;
                page.ItemID = (int)row["ItemID"].Value;
                page.BibliographicLevelLabel = (string)row["BibliographicLevelLabel"].Value;
                page.FullTitle = (string)row["FullTitle"].Value + " " + (string)row["PartNumber"].Value + " " + (string)row["PartName"].Value;
                page.ShortTitle = (string)row["ShortTitle"].Value + " " + (string)row["PartNumber"].Value + " " + (string)row["PartName"].Value;
                page.Authors = (string)row["Authors"].Value;
                page.Volume = (string)row["Volume"].Value;
                page.Date = (string)row["Date"].Value;
                page.IndicatedPages = (string)row["IndicatedPages"].Value;
                page.TotalPages = (int)row["TotalPages"].Value;
                result.Pages.Add(page);
            }
            return result;
        }

        public NameSearchResult NameResolvedSearchForPagesDownload(string name)
        {
            List<CustomDataRow> data = new NameResolvedDAL().NameResolvedSearchForPagesDownload(
                null, null, name);

            NameSearchResult result = new NameSearchResult();
            result.QueryDate = DateTime.Now.ToString("dd MMM yyyy h:mmtt");

            //loop through the results and create a list of NameSearchPage objects
            foreach (CustomDataRow row in data)
            {
                NameSearchPage page = new NameSearchPage();
                page.TitleID = (int)row["TitleID"].Value;
                page.PageID = (int)row["PageID"].Value;
                page.ItemID = (int)row["ItemID"].Value;
                page.BibliographicLevelLabel = (string)row["BibliographicLevelLabel"].Value;
                page.FullTitle = (string)row["FullTitle"].Value + " " + (string)row["PartNumber"].Value + " " + (string)row["PartName"].Value;
                page.ShortTitle = (string)row["ShortTitle"].Value + " " + (string)row["PartNumber"].Value + " " + (string)row["PartName"].Value;
                page.PublisherPlace = (string)row["PublisherPlace"].Value;
                page.Publisher = (string)row["Publisher"].Value;
                page.Authors = (string)row["Authors"].Value;
                page.Volume = (string)row["Volume"].Value;
                page.Date = (string)row["Date"].Value;
                page.IndicatedPages = (string)row["IndicatedPages"].Value;
                page.CallNumber = (string)row["CallNumber"].Value;
                page.LanguageName = (string)row["LanguageName"].Value;
                result.Pages.Add(page);
            }
            return result;
        }
    }
}
