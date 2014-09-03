using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class Recent : BasePage
    {
        public String institutionCode = String.Empty;
        public String languageCode = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int top = 100;
            String paramTop = (string)RouteData.Values["top"]; 
            if (paramTop != null)
                Int32.TryParse(paramTop, out top);
            top = (top < 1 || top > 500) ? 100 : top;

            String institutionName = ((string)RouteData.Values["inst"] ?? String.Empty).ToString();
            if (institutionName != String.Empty)
            {
                institutionCode = institutionName.ToUpper();
                Institution institution = bhlProvider.InstitutionSelectAuto(institutionCode);
                if (institution != null) institutionName = institution.InstitutionName.Replace("(archive.org)", "").Trim();
            }

            String languageName = ((string)RouteData.Values["lang"] ?? String.Empty).ToString();
            if (this.Request.QueryString["lang"] != null)
            {
                languageCode = languageName.ToUpper();
                Language language = bhlProvider.LanguageSelectAuto(languageCode);
                if (language != null) languageName = language.LanguageName;
            }

            rptRecent.DataSource = bhlProvider.ItemSelectRecent(top, languageCode, institutionCode);
            rptRecent.DataBind();

            String recentLink = "http://" + Request.ServerVariables["HTTP_HOST"] + "/RecentRss/" + top.ToString();
            if ((languageCode + institutionCode) != String.Empty)
            {
                if (languageCode == String.Empty) languageCode = "ALL";
                if (institutionCode == String.Empty) institutionCode = "ALL";
                recentLink += "/" + languageCode + "/" + institutionCode;
                lnkRecent25.HRef += "/" + languageCode + "/" + institutionCode;
                lnkRecent50.HRef += "/" + languageCode + "/" + institutionCode;
                lnkRecent100.HRef += "/" + languageCode + "/" + institutionCode;
                lnkRecent250.HRef += "/" + languageCode + "/" + institutionCode;
                lnkRecent500.HRef += "/" + languageCode + "/" + institutionCode;
            }

            if (!string.IsNullOrEmpty(languageName)) lblLanguage.Text = " &nbsp; Published In: " + languageName;
            if (!string.IsNullOrEmpty(institutionName)) lblContributor.Text = " &nbsp; For: " + institutionName;

            lblNumberDisplayed.Text = " (Last " + top.ToString() + ")";
            rssFeedLink.HRef = recentLink;
            rssFeedLink.InnerHtml = recentLink;
            rssFeedImageLink.HRef = recentLink;

            Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Recent Additions");
            //((SiteMaster)Page.Master).SetTweetMessage(String.Format(ConfigurationManager.AppSettings["TweetMessage"], "Recent Additions"));

        }
    }
}