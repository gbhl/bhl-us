using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public partial class NameList : BrowsePage
    {
        public string NameParam { get; set; }
        public string EOLID { get; set; }
        public string TitleLink { get; set; }
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string name = string.Empty;
            // Read the parameters passed to the page
            if (RouteData.Values["name"] != null)
            {
                NameParam = (string)RouteData.Values["name"];
                name = NameParam.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            }

            main.Page.Title = string.Format("Bibliography for \"{0}\"- Biodiversity Heritage Library", name);

            // Get the identifiers for this name            
            CustomGenericList<NameIdentifier> nameIdentifiers = bhlProvider.NameIdentifierSelectForResolvedName(name);
      
            string nameBankID = string.Empty;
            foreach (NameIdentifier nameIdentifier in nameIdentifiers)
            {
                if (nameIdentifier.IdentifierName == "EOL") EOLID = nameIdentifier.IdentifierValue;
                if (nameIdentifier.IdentifierName == "NameBank") nameBankID = nameIdentifier.IdentifierValue;
            }

            litEOLLink.Text = (!string.IsNullOrEmpty(EOLID)) ? string.Format("<a class=\"button\" target=\"_blank\" href=\"http://www.eol.org/pages/{0}\">View in <img src='/images/eol_15px.png' style='position:relative;top:2px'></a>", EOLID) : string.Empty;
            TitleLink = name;
        }
    
    }
}