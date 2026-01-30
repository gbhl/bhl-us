using System;
using System.Web.UI;
using MOBOT.BHL.DataObjects;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class Recent : BasePage
    {
        public string institutionCode = string.Empty;
        public string languageCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int top = 100;
            string paramTop = (string)RouteData.Values["top"]; 
            if (paramTop != null) Int32.TryParse(paramTop, out top);
            top = (top < 1 || top > 1000) ? 100 : top;

            string institutionName = ((string)RouteData.Values["inst"] ?? string.Empty).ToString();
            if (institutionName != String.Empty)
            {
                institutionCode = institutionName.ToUpper();
                Institution institution = bhlProvider.InstitutionSelectAuto(institutionCode);
                if (institution != null) institutionName = institution.InstitutionName.Replace("(archive.org)", "").Trim();
            }

            string languageName = ((string)RouteData.Values["lang"] ?? string.Empty).ToString();
            if (this.Request.QueryString["lang"] != null)
            {
                languageCode = languageName.ToUpper();
                Language language = bhlProvider.LanguageSelectAuto(languageCode);
                if (language != null) languageName = language.LanguageName;
            }

            rptRecent.DataSource = bhlProvider.BookSelectRecent(top, languageCode, institutionCode);
            rptRecent.DataBind();

            string recentLink = string.Format("http://{0}/RecentRss/{1}", Request.ServerVariables["HTTP_HOST"], top.ToString());
            if ((languageCode + institutionCode) != string.Empty)
            {
                if (languageCode == string.Empty) languageCode = "ALL";
                if (institutionCode == string.Empty) institutionCode = "ALL";
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

            Page.Title = string.Format(AppConfig.PageTitle, "Recent Additions");
        }
    }
}