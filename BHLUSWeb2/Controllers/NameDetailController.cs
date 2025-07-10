using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class NameDetailController : Controller
    {
        // GET: NameDetail
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(string name)
        {
            NameDetailModel model = new NameDetailModel();

            // Read the parameters passed to the page
            if (name != null)
            {
                model.NameClean = name;
                model.NameParam = Server.UrlEncode(model.NameClean);
                model.NameClean= Server.HtmlEncode(model.NameClean).Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            }

            if (!string.IsNullOrWhiteSpace(model.NameClean))
            {
                // Call the Global Names service to get the details about the name
                List<GNVerifierResponse> nameDetails = null;
                string errorMessage = string.Empty;
                try
                {
                    nameDetails = GetNameDetail(model.NameClean);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "NameDetailController.Index");
                    errorMessage = "No response received from the <a target=\"_blank\"  rel=\"noopener noreferrer\" href=\"http://resolver.globalnames.org\">Global Names Index</a>.  Please try again later.";
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
                    List<GNVerifierResponse> displayNames = new List<GNVerifierResponse>();
                    foreach (var dataSourceTitle in distinctSources)
                    {
                        var sourceNames = from n in nameDetails
                                          where n.DataSourceTitle == dataSourceTitle.DataSourceTitle
                                          orderby n.MatchType, n.Score
                                          select n;

                        foreach (GNVerifierResponse nameDetail in sourceNames)
                        {
                            displayNames.Add(nameDetail);
                        }
                    }

                    // Build the HTML to display the name information
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<ol>");
                    string currentSourceName = string.Empty;
                    foreach (GNVerifierResponse nameDetail in displayNames)
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
                        if (hasUrl) sb.Append("<a target=\"_blank\" rel=\"noopener noreferrer\" href=\"" + nameDetail.Url + "\">");
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

                            string classRank = nameDetail.ClassificationPathRanks;
                            if (classRank.StartsWith("|")) classRank = classRank.Substring(1);

                            // Add the formatted classification path
                            System.Text.StringBuilder sbClass = new System.Text.StringBuilder();
                            string[] classes = classPath.Split('|');
                            string[] ranks = classRank.Split('|');
                            for (int x = classes.Length - 1; x >= 0; x--)
                            {
                                string className = classes[x];

                                // Include the rank, if it was supplied
                                if (ranks.Length == classes.Length)
                                {
                                    if (!string.IsNullOrWhiteSpace(ranks[x])) className += " (" + ranks[x] + ")";
                                }

                                sbClass.Insert(0, "<ul class=\"classificationList\"><li" + (x == 0 ? " style=\"padding-left:0px;\">" : ">") + className);
                                sbClass.Append("</li></ul>");
                            }
                            sb.Append(sbClass.ToString());

                            sb.Append("</div>");
                            sb.Append("</div>");
                        }

                        sb.Append("</li>");
                    }
                    sb.Append("</ol>");
                    model.NameDetails = sb.ToString();
                }
                else
                {
                    model.NameDetails = errorMessage;
                }
            }
            else
            {
                model.NameDetails = "No name supplied.";
            }

            return View(model);
        }

        /// <summary>
        /// Get the details for the specified name.  If not already available in the cache, then
        /// get the information from the Global Names service.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<GNVerifierResponse> GetNameDetail(string name)
        {
            List<GNVerifierResponse> nameDetails = new BHLProvider().GetNameDetailFromGNVerifier(name);

            return nameDetails;
        }
    }
}