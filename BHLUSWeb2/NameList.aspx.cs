﻿using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

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

            // Get the identifiers for this name
            List<NameIdentifier> nameIdentifiers = bhlProvider.NameIdentifierSelectForResolvedName(searchName);
            foreach (NameIdentifier nameIdentifier in nameIdentifiers)
            {
                if (nameIdentifier.IdentifierName == "EOL") { EOLID = nameIdentifier.IdentifierValue; break; }
            }

            main.Page.Title = string.Format("Bibliography for \"{0}\"- Biodiversity Heritage Library", searchName);
            litEOLLink.Text = (!string.IsNullOrEmpty(EOLID)) ? string.Format("<a class=\"button\" rel=\"noopener noreferrer\" target=\"_blank\" href=\"http://www.eol.org/pages/{0}\">View in <img src='/images/eol_15px.png' style='position:relative;top:2px'></a>", EOLID) : string.Empty;
            TitleLink = searchName;
        }
    
    }
}