using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Web2
{
    public partial class CreatorList : BrowsePage
    {

        protected string Start { get; set; }
        protected List<Author> BhlAuthorList { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Start = (string)RouteData.Values["start"];

            if (string.IsNullOrWhiteSpace(Start))
            {
                Start = string.Empty;
                main.Page.Title = "Browsing all Authors - Biodiversity Heritage Library";
            }
            else
            {
                // Should prob be a route constraint.
                if (Start.Length != 1 || !Regex.IsMatch(Start, @"[A-Za-z]"))
                {
                    Response.Redirect("~/browse/authors/a");
                }

                main.Page.Title = string.Format("Browsing Authors begining with \"{0}\"- Biodiversity Heritage Library", Start);
            }

            String cacheKey = "AuthorBrowse" + Start;

            if (Cache[cacheKey] != null)
            {
                // Use cached version
                BhlAuthorList = (List<Author>)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                BhlAuthorList = bhlProvider.AuthorSelectByNameLike(Start, 1000000);

                Cache.Add(cacheKey,
                    BhlAuthorList,
                    null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal,
                    null);
            }
        }

        public string SetClass(string page)
        {
            return Start.Equals(page, StringComparison.OrdinalIgnoreCase) ? "active" : string.Empty;
        }
    }
}