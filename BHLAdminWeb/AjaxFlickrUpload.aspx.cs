using FlickrUtility;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AjaxFlickrUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["type"];

            if (type == "validate")
            {
                    string oAuthAccessToken = "";
                    string oAuthAccessTokenSecret = "";

                    try
                    {
                        FlickrTools.OAuthAccessToken(Request["oauthtoken"], Request["oauthtokensecret"], Request["oauthverifier"], 
                            out oAuthAccessToken, out oAuthAccessTokenSecret);

                        StringBuilder sb = new StringBuilder("");
                        sb.Append("{\"oAuthAccessToken\":\"");
                        sb.Append(oAuthAccessToken);
                        sb.Append("\", \"oAuthAccessTokenSecret\":\"");
                        sb.Append(oAuthAccessTokenSecret);
                        sb.Append("\", \"errorMsg\":\"");
                        sb.Append("");
                        sb.Append("\"}");
                        Response.Write(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        StringBuilder sb = new StringBuilder("");
                        sb.Append("{\"errorMsg\":\"");
                        sb.Append(ex.Message);
                        sb.Append("\"}");
                        Response.Write(sb.ToString());
                    }
            }
            else if (type == "upload")
            {
                try
                {
                    HttpCookie c = new HttpCookie("oAuthAccessToken");
                    c.Value = Request["oAuthAccessToken"];
                    Response.Cookies.Add(c);
                    Request.Cookies.Add(c);
                    c = new HttpCookie("oAuthAccessTokenSecret");
                    c.Value = Request["oAuthAccessTokenSecret"];
                    Response.Cookies.Add(c);
                    Request.Cookies.Add(c);

                    BHLProvider provider = new BHLProvider();

                    int pageId = int.Parse(Request["pageid"]);
                    int titleId = int.Parse(Request["titleid"]);

                    double rotate = 0;
                    double.TryParse(Request["rotate"], out rotate);

                    Page page = provider.PageMetadataSelectByPageID(pageId);
                    Item item = provider.ItemSelectByBarcodeOrItemID(null, page.BarCode);
                    Title title = provider.TitleSelect(titleId);
                    string pageUrl = "https://biodiversitylibrary.org/page/" + pageId;
                    string itemUrl = "https://biodiversitylibrary.org/item/" + item.ItemID;
                    string description = title.ShortTitle + "\n" + title.PublicationDetails + "\n" + pageUrl;

                    List<TitleKeyword> titleKeywords = provider.TitleKeywordSelectByTitleID(titleId);
                    List<Institution> institutions = provider.InstitutionSelectByItemID(item.ItemID);
                    List<string> titleKeywordsList = new List<string>();
                    foreach (TitleKeyword tk in titleKeywords)
                    {
                        titleKeywordsList.Add(tk.Keyword);
                    }
                    foreach (Institution institution in institutions)
                    {
                        if (institution.InstitutionRoleName == InstitutionRole.HoldingInstitution) titleKeywordsList.Add(institution.InstitutionName);
                    }
                    string[] subjects = titleKeywordsList.ToArray();

                    List<Author> authors = provider.AuthorSelectByTitleId(titleId);
                    string authorName = "";
                    if (authors != null && authors.Count > 0)
                        authorName = authors[0].FullName;

                    string fileName = "n" + (page.SequenceOrder - 1) + "_w1150";
                    string iaUrl = item.DownloadUrl + "/page/" + fileName + ".jpg";
                    string flickrImageUrl = FlickrTools.UploadImageToFlickr(iaUrl, authorName, description, subjects, itemUrl, 
                        Request["oAuthAccessToken"], Request["oAuthAccessTokenSecret"], fileName, 
                        "bhl:page=" + pageId + ",dc:identifier=" + pageUrl, rotate);

                    SaveFlickrPageRecord(pageId, flickrImageUrl);
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    if (ConfigurationManager.AppSettings["LogExceptions"] == "true")
                    {
                        ExceptionUtility.LogException(ex, "AjaxFlickrUpload.Page_Load");
                    }

                    StringBuilder sb = new StringBuilder("");
                    sb.Append("{\"errorMsg\":\"");
                    sb.Append(errorMsg);
                    sb.Append("\"}");
                    Response.Write(sb.ToString());
                }
            }
        }

        private void SaveFlickrPageRecord(int pageId, string flickrImageUrl)
        {
            BHLProvider provider = new BHLProvider();
            PageFlickr pf = provider.PageFlickrSelectByPage(pageId);
            if (pf == null)
                pf = new PageFlickr();
            pf.PageID = pageId;
            pf.FlickrURL = flickrImageUrl;

            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
            provider.PageFlickrSave(pf, userId);
        }
    }
}