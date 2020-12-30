using MOBOT.BHL.DataObjects;
using System;

namespace MOBOT.BHL.Web2
{
    public partial class NameList : BrowsePage
    {
        public string NameParam { get; set; }
        public string TitleLink { get; set; }
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string searchName = string.Empty;

            // Read the parameters passed to the page
            if (RouteData.Values["name"] != null)
            {
                searchName = (string)RouteData.Values["name"];
                NameParam = Server.UrlEncode(searchName);
                searchName = Server.HtmlEncode(searchName).Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            }

            NameResolved nameResolved = bhlProvider.NameResolvedSelectByResolvedName(searchName);
            if (nameResolved != null)
            {
                searchName = nameResolved.CanonicalNameString;
                NameParam = Server.UrlEncode(nameResolved.CanonicalNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~'));
            }

            main.Page.Title = string.Format("Bibliography for \"{0}\"- Biodiversity Heritage Library", searchName);
            TitleLink = searchName;
        }
    
    }
}