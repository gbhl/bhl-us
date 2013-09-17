using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class NameDetail : BasePage
    {
        public string NameParam = string.Empty;
        public string NameClean = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["name"] != null)
            {
                NameParam = (string)RouteData.Values["name"];
                NameClean = NameParam.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            }
            main.Page.Title = string.Format("{0} - Biodiversity Heritage Library", NameClean);

            if (!string.IsNullOrWhiteSpace(NameClean))
            {
                // Call the Global Names service to get the details about the name
                List<GNResolverResponse> nameDetails = null;
                string errorMessage = string.Empty;
                try
                {
                    nameDetails = GetNameDetail(NameClean);
                }
                catch
                {
                    errorMessage = "No response received from the <a target=\"_blank\" href=\"http://resolver.globalnames.org\">Global Names Index</a>.  Please try again later.";
                }

                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    // Format the Global Names response

                    // Get the distinct data sources in the response, along with the 'best' match type for each
                    // source.  Match type is a value from 1 to 6, with 1 being the best (most definite) match,
                    // and 6 being the worst (most questionable) match.  Order the list by match type and by
                    // data source name.
                    var distinctSources = from n in nameDetails
                                          group n by n.DataSourceTitle into g
                                          let MatchType = g.Min(n => n.MatchType)
                                          orderby MatchType, g.Key
                                          select new { DataSourceTitle = g.Key, MatchType };

                    // For each data source, accumulate every result for that source and add it to the final 
                    // ordered result set.
                    List<GNResolverResponse> displayNames = new List<GNResolverResponse>();
                    foreach (var dataSourceTitle in distinctSources)
                    {
                        var sourceNames = from n in nameDetails
                                          where n.DataSourceTitle == dataSourceTitle.DataSourceTitle
                                          orderby n.MatchType, n.Score
                                          select n;

                        foreach (GNResolverResponse nameDetail in sourceNames)
                        {
                            displayNames.Add(nameDetail);
                        }
                    }

                    // Build the HTML to display the name information
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<ol>");
                    string currentSourceName = string.Empty;
                    foreach (GNResolverResponse nameDetail in displayNames)
                    {
                        sb.Append("<li class=\"titlelisting\">");

                        if (currentSourceName != nameDetail.DataSourceTitle)
                        {
                            sb.Append("<div class=\"titledetails\">");
                            sb.Append("<h3>");
                            sb.Append(nameDetail.DataSourceTitle);
                            sb.Append("</h3>");
                            sb.Append("</div>");
                            sb.Append("</li>");
                            sb.Append("<li class=\"titlelisting\">");
                            currentSourceName = nameDetail.DataSourceTitle;
                        }

                        sb.Append("<div class=\"titledetails\" style=\"overflow:auto;\">");
                        sb.Append("<div style=\"float:left; margin:0; width:25%;\">Name: </div>");
                        sb.Append("<div style=\"float:left; margin:0;\">");
                        bool hasUrl = !string.IsNullOrWhiteSpace(nameDetail.Url);
                        if (hasUrl) sb.Append("<a target=\"_blank\" href=\"" + nameDetail.Url + "\">");
                        sb.Append(nameDetail.NameString);
                        if (hasUrl) sb.Append("</a>");
                        sb.Append("</div>");
                        sb.Append("</div>");

                        if (!string.IsNullOrWhiteSpace(nameDetail.LocalID))
                        {
                            sb.Append("<div class=\"titledetails\" style=\"overflow:auto;\">");
                            sb.Append("<div style=\"float:left; margin:0; width:25%;\">Local ID: </div>");
                            sb.Append("<div style=\"float:left; margin:0;\">" + nameDetail.LocalID + "</div>");
                            sb.Append("</div>");
                        }

                        // Example for formatting classification path: http://eol.org/pages/8914335/overview
                        if (!string.IsNullOrWhiteSpace(nameDetail.ClassificationPath))
                        {
                            string classPath = nameDetail.ClassificationPath;
                            if (classPath.StartsWith("|")) classPath = classPath.Substring(1);
                            sb.Append("<div class=\"titledetails\" style=\"overflow:auto;\">");
                            sb.Append("<div style=\"float:left; margin:0; width:25%;\">Classification: </div>");
                            sb.Append("<div style=\"float:left; margin:0;\">");

                            // Add the formatted classification path
                            System.Text.StringBuilder sbClass = new System.Text.StringBuilder();
                            string[] classes = classPath.Split('|');
                            for (int x = classes.Length - 1; x >= 0; x--)
                            {
                                sbClass.Insert(0, "<ul class=\"classificationList\"><li" + (x==0 ? " style=\"padding-left:0px;\">" : ">") + classes[x]);
                                sbClass.Append("</li></ul>");
                            }
                            sb.Append(sbClass.ToString());

                            sb.Append("</div>");
                            sb.Append("</div>");
                        }

                        sb.Append("</li>");
                    }
                    sb.Append("</ol>");
                    litDetails.Text = sb.ToString();
                }
                else
                {
                    litDetails.Text = errorMessage;
                }
            }
            else
            {
                litDetails.Text = "No name supplied.";
            }
        }

        /// <summary>
        /// Get the details for the specified name.  If not already available in the cache, then
        /// get the information from the Global Names service.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<GNResolverResponse> GetNameDetail(string name)
        {

            List<GNResolverResponse> nameDetails = null;

            string cacheKey = "NameDetail" + name;

            if (Cache[cacheKey] != null)
            {
                // Use cached version
                nameDetails = (List<GNResolverResponse>)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                nameDetails = bhlProvider.GetNameDetailFromGNResolver(name);

                Cache.Add(cacheKey,
                    nameDetails,
                    null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["NameDetailCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal,
                    null);
            }

            return nameDetails;
        }
    }
}