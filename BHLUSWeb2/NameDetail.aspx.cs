using System;
using System.Collections.Generic;
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

            // Example service calls
            // http://resolver.globalnames.org/name_resolvers.xml?names=Poa+annua+ssp.+exilis+(Tomm.+ex+Freyn)+Asch.+%26+Graebn.
            // http://resolver.globalnames.org/name_resolvers.xml?names=Poa+annua

            if (!string.IsNullOrWhiteSpace(NameClean))
            {
                List<GNResolverResponse> nameDetails = bhlProvider.GetNameDetailFromGNResolver(NameClean);
                List<GNResolverResponse> displayNames = new List<GNResolverResponse>();

                var distinctSource = from n in nameDetails 
                                      group n by n.DataSourceTitle into g
                                      let MatchType = g.Min(n => n.MatchType)
                                      orderby MatchType, g.Key
                                      select new {DataSourceTitle = g.Key, MatchType};

                foreach (var dataSourceTitle in distinctSource)
                {
                    var sourceNames = from n in nameDetails where n.DataSourceTitle == dataSourceTitle.DataSourceTitle orderby n.MatchType, n.Score select n;

                    foreach (GNResolverResponse nameDetail in sourceNames)
                    {
                        displayNames.Add(nameDetail);
                    }
                }

                //rptNameDetails.DataSource = displayNames;
                //rptNameDetails.DataBind();

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

                    sb.Append("<div class=\"titledetails\">");
                    sb.Append("<span>Name: </span>");
                    sb.Append("<span style=\"position:relative; left:150px\">");
                    bool hasUrl = !string.IsNullOrWhiteSpace(nameDetail.Url);
                    if (hasUrl) sb.Append("<a target=\"_blank\" href=\"" + nameDetail.Url + "\">");
                    sb.Append(nameDetail.NameString);
                    if (hasUrl) sb.Append("</a>");
                    sb.Append("</span>");
                    sb.Append("</div>");

                    if (!string.IsNullOrWhiteSpace(nameDetail.LocalID))
                    {
                        sb.Append("<div class=\"titledetails\">");
                        sb.Append("<span>Local ID: </span><span style=\"position:relative;left:150px\">" + nameDetail.LocalID + "</span>");
                        sb.Append("</div>");
                    }

                    // Example for formatting classification path: http://eol.org/pages/8914335/overview
                    if (!string.IsNullOrWhiteSpace(nameDetail.ClassificationPath))
                    {
                        string classPath = nameDetail.ClassificationPath;
                        if (classPath.StartsWith("|")) classPath = classPath.Substring(1);
                        sb.Append("<div class=\"titledetails\">");
                        sb.Append("<span>Classification: </span><span style=\"position:relative;left:150px\">" + classPath.Replace("|", "<br/>") + "</span>");
                        sb.Append("</div>");
                    }

                    sb.Append("</li>");
                }
                sb.Append("</ol>");
                litDetails.Text = sb.ToString();

            }
        }
    }
}