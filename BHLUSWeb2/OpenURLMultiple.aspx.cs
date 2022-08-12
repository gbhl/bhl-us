using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class OpenURLMultiple : BasePage
    {
        protected IList<Page> PageList { get; set; }
        protected IList<PageSummaryView> ItemList { get; set; }
        protected IList<Title> TitleList { get; set; }
        protected IList<Segment> SegmentList { get; set; }
        protected IList<OpenUrlBookResult> BookList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Show the citations that were found
                int id;
                string idList = Request.QueryString["id"];

                SegmentList = new List<Segment>();
                BookList = new List<OpenUrlBookResult>();

                if (idList != null)
                {
                    string[] idStrings = idList.Split('|');
                    foreach (string idString in idStrings)
                    {
                        if (idString.Length > 1)
                        {
                            if (idString.Substring(0, 1) == "p")
                            {
                                if (Int32.TryParse(idString.Substring(1), out id))
                                {
                                    Page page = bhlProvider.PageMetadataSelectByPageID(id);
                                    OpenUrlBookResult book = new OpenUrlBookResult();
                                    book.Url = string.Format(ConfigurationManager.AppSettings["PagePageUrl"], page.PageID.ToString());
                                    book.Title = page.ShortTitle;
                                    book.Volume = page.Volume;
                                    book.Issue = page.Issue;
                                    book.Year = page.Year;
                                    book.Pages = page.IndicatedPages;
                                    BookList.Add(book);
                                }
                            }
                            else if (idString.Substring(0, 1) == "i")
                            {
                                if (Int32.TryParse(idString.Substring(1), out id))
                                {
                                    PageSummaryView psv = bhlProvider.PageSummarySelectByItemId(id, true);
                                    OpenUrlBookResult book = new OpenUrlBookResult();
                                    book.Url = string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], psv.BookID.ToString());
                                    book.Title = psv.ShortTitle;
                                    book.Volume = psv.Volume;
                                    BookList.Add(book);
                                }
                            }
                            else if (idString.Substring(0, 1) == "t")
                            {
                                if (Int32.TryParse(idString.Substring(1), out id))
                                {
                                    Title title = bhlProvider.TitleSelect(id);
                                    OpenUrlBookResult book = new OpenUrlBookResult();
                                    book.Url = string.Format(ConfigurationManager.AppSettings["BibPageUrl"], title.TitleID.ToString());
                                    book.Title = title.ShortTitle;
                                    BookList.Add(book);
                                }
                            }
                            else if (idString.Substring(0, 1) == "s")
                            {
                                if (Int32.TryParse(idString.Substring(1), out id))
                                {
                                    Segment segment = bhlProvider.SegmentSelectAuto(id);
                                    SegmentList.Add(segment);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected class OpenUrlBookResult
        {
            public string Url;
            public string Title;
            public string Volume;
            public string Issue;
            public string Year;
            public string Pages;
        }
    }
}
