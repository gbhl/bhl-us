using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }
    }
}