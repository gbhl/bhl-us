using MOBOT.OpenUrl.Utilities;
using MvcThrottle;
using System;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class OpenUrlController : Controller
    {
        [EnableThrottling]
        // GET: OpenUrl
        public ActionResult OpenUrlResolver()
        {
            String response = String.Empty;
            bool redirect = false;

            IOpenUrlQuery ouQuery = OpenUrlQueryFactory.CreateOpenUrlQuery(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            MOBOT.BHL.OpenUrlProvider.BHLOpenUrlProvider openurl = new MOBOT.BHL.OpenUrlProvider.BHLOpenUrlProvider();
            openurl.UrlFormat = AppConfig.PagePageUrl;
            openurl.ItemUrlFormat = AppConfig.ItemPageUrl;
            openurl.TitleUrlFormat = AppConfig.BibPageUrl;
            openurl.PartUrlFormat = AppConfig.PartPageUrl;
            openurl.IpAddress = Request.UserHostAddress;
            openurl.UseFullTextSearch = AppConfig.EnableFullTextSearch;
            IOpenUrlResponse ouResponse = openurl.FindCitation(ouQuery);

            // Format the response as requested
            string format = Request.QueryString["format"];
            string callback = Request.QueryString["callback"];
            string contentType = string.Empty;
            if (format == null) format = "redirect";
            switch (format)
            {
                case "xml":
                    response = ouResponse.ToXml();
                    contentType = "text/xml";
                    break;
                case "json":
                    response = ouResponse.ToJson();

                    // Include any specified callback function in JSON responses
                    if ((callback != null) && (callback != String.Empty)) response = callback + "(" + response + ");";

                    contentType = "text/json";
                    break;
                case "html":
                    response = this.FormatAsHTML(ouResponse);
                    contentType = "text/html";
                    break;
                case "redirect":
                    redirect = true;

                    if (ouResponse.Status == ResponseStatus.Error || ouResponse.Status == ResponseStatus.Undefined)
                    {
                        response = "/openurlhelp.aspx";
                        break;
                    }
                    else
                    {
                        switch (ouResponse.citations.Count)
                        {
                            case 0:
                                response = "/openurlnone.aspx";
                                break;
                            case 1:
                                response = (ouResponse.citations[0].Url != String.Empty ?
                                                ouResponse.citations[0].Url :
                                                (ouResponse.citations[0].PartUrl != String.Empty ?
                                                    ouResponse.citations[0].PartUrl :
                                                    (ouResponse.citations[0].ItemUrl != String.Empty ?
                                                        ouResponse.citations[0].ItemUrl :
                                                        ouResponse.citations[0].TitleUrl)));
                                break;
                            default:
                                string matches = string.Empty;
                                string lastTitleUrl = string.Empty;
                                int numCitations = 0;
                                // Build a list of the identifiers
                                foreach (OpenUrlResponseCitation citation in ouResponse.citations)
                                {
                                    if (citation.PartUrl.Length > 0)
                                    {
                                        matches += (matches.Length > 0) ? "|" : "";
                                        matches += "s" + citation.PartUrl.Substring(citation.PartUrl.LastIndexOf("/") + 1);
                                        numCitations++;
                                    }
                                    // Only show page/item detail if no more than 50 citations
                                    else if (citation.Url.Length > 0 && ouResponse.citations.Count < 50)
                                    {
                                        matches += (matches.Length > 0) ? "|" : "";
                                        matches += "p" + citation.Url.Substring(citation.Url.LastIndexOf("/") + 1);
                                        numCitations++;
                                    }
                                    else if (citation.ItemUrl.Length > 0 && ouResponse.citations.Count < 50)
                                    {
                                        matches += (matches.Length > 0) ? "|" : "";
                                        matches += "i" + citation.ItemUrl.Substring(citation.ItemUrl.LastIndexOf("/") + 1);
                                        numCitations++;
                                    }
                                    // If more than 50 citations, show only title-level information
                                    else
                                    {
                                        if (citation.TitleUrl != lastTitleUrl)
                                        {
                                            matches += (matches.Length > 0) ? "|" : "";
                                            matches += "t" + citation.TitleUrl.Substring(citation.TitleUrl.LastIndexOf("/") + 1);
                                            lastTitleUrl = citation.TitleUrl;
                                            numCitations++;
                                        }
                                    }
                                }
                                if (numCitations == 1 && lastTitleUrl != string.Empty)
                                {
                                    response = lastTitleUrl;
                                }
                                else
                                {
                                    response = "/openurlmultiple.aspx?id=" + matches;
                                }
                                break;
                        }
                    }
                    break;
            }

            // Return the response (redirecting if necessary)
            if (redirect)
            {
                return Redirect(response);
            }
            else
            {
                return Content(response, contentType);
            }
        }

        private string FormatAsHTML(IOpenUrlResponse ouResponse)
        {
            System.Text.StringBuilder html = new System.Text.StringBuilder();

            html.Append("<div class='openurlresponse'>");

            if (ouResponse.Status == ResponseStatus.Success)
            {
                foreach (OpenUrlResponseCitation citation in ouResponse.citations)
                {
                    html.Append("<div class='oucitation'>");
                    if (citation.Genre != string.Empty) html.Append("<div class='ougenre'>" + citation.Genre + "</div>");
                    if (citation.Title != string.Empty) html.Append("<div class='outitle'>" + citation.Title + "</div>");
                    if (citation.STitle != string.Empty) html.Append("<div class='oustitle'>" + citation.STitle + "</div>");
                    if (citation.ATitle != string.Empty) html.Append("<div class='ouatitle'>" + citation.ATitle + "</div>");
                    if (citation.Authors.Count > 0)
                    {
                        html.Append("<div class='ouauthors'>");
                        foreach (string author in citation.Authors)
                        {
                            html.Append("<div class='ouauthor'>" + author + "</div>");
                        }
                        html.Append("</div>");
                    }
                    if (citation.PublisherPlace != string.Empty) html.Append("<div class='ouplace'>" + citation.PublisherPlace + "</div>");
                    if (citation.PublisherName != string.Empty) html.Append("<div class='oupub'>" + citation.PublisherName + "</div>");
                    if (citation.Date != string.Empty) html.Append("<div class='oudate'>" + citation.Date + "</div>");
                    if (citation.Volume != string.Empty) html.Append("<div class='ouvolume'>" + citation.Volume + "</div>");
                    if (citation.Edition != string.Empty) html.Append("<div class='ouedition'>" + citation.Edition + "</div>");
                    if (citation.PublicationFrequency != string.Empty) html.Append("<div class='oupubfreq'>" + citation.PublicationFrequency + "</div>");
                    if (citation.Language != string.Empty) html.Append("<div class='oulanguage'>" + citation.Language + "</div>");
                    if (citation.SPage != string.Empty) html.Append("<div class='ouspage'>" + citation.SPage + "</div>");
                    if (citation.EPage != string.Empty) html.Append("<div class='ouepage'>" + citation.EPage + "</div>");
                    if (citation.Pages != string.Empty) html.Append("<div class='oupages'>" + citation.Pages + "</div>");
                    if (citation.Subjects.Count > 0)
                    {
                        html.Append("<div class='ousubjects'>");
                        foreach (string subject in citation.Subjects)
                        {
                            html.Append("<div class='ousubject'>" + subject + "</div>");
                        }
                        html.Append("</div>");
                    }
                    if (citation.Issn != string.Empty) html.Append("<div class='ouissn'>ISSN:" + citation.Issn + "</div>");
                    if (citation.Isbn != string.Empty) html.Append("<div class='ouisbn'>ISBN:" + citation.Isbn + "</div>");
                    if (citation.Oclc != string.Empty) html.Append("<div class='ouoclc'>OCLC:" + citation.Oclc + "</div>");
                    if (citation.Lccn != string.Empty) html.Append("<div class='oulccn'>LCCN:" + citation.Lccn + "</div>");
                    if (citation.Url != string.Empty) html.Append("<div class='ouurl'><a href='" + citation.Url + "'>" + citation.Url + "</a></div>");
                    html.Append("</div>");
                }
            }
            else
            {
                // Return error message
                html.Append("<div class='ouerror'>" + ouResponse.Message + "</div>");
            }

            html.Append("</div>");

            return html.ToString();
        }
    }
}