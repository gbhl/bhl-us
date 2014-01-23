﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class Bibliography : BasePage
    {
        public string Barcode { get; set; }
        public string Genre { get; set; }
        public string DOI { get; set; }
        public string DDC { get; set; }
        public string LanguageName { get; set; }
        public string LocalLibraryUrl { get; set; }
        protected int TitleId { get; set; }

        protected Title BhlTitle { get; set; }
        protected IList<TitleVariant> TitleVariants { get; set; }
        protected IList<TitleAssociation> TitleAssociations { get; set; }
        protected CustomGenericList<TitleKeyword> TitleKeywords { get; set; }
        protected IList<Title_Identifier> TitleIdentifiers { get; set; }
        protected IList<Author> Authors { get; set; }
        protected IList<Author> AdditionalAuthors { get; set; }
        protected IList<BibliographyItem> BibliographyItems { get; set; }
        protected IList<Collection> Collections { get; set; }

        public class BibliographyItem
        {
            public Item Item { get; set; }
            public string ThumbUrl { get; set; }

            public BibliographyItem(Item item, string thumbUrl)
            {
                Item = item;
                ThumbUrl = thumbUrl;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSummaryView pageSummary;
            DOI = string.Empty;

            // Parse TitleID
            int titleId;
            if (!int.TryParse((string)RouteData.Values["titleid"], out titleId))
            {
                Response.Redirect("~/titlenotfound");
            }
            else
            {
                TitleId = titleId;
            }

            // Find Title
            BhlTitle = bhlProvider.TitleSelect(titleId);
            if (BhlTitle == null)
            {
                Response.Redirect("~/titlenotfound");
            }
            else
            {
                // Check to make sure this title hasn't been replaced.  If it has, redirect
                // to the appropriate titleid.
                if (BhlTitle.RedirectTitleID != null)
                {
                    Response.Redirect("~/bibliography/" + BhlTitle.RedirectTitleID);
                }

                // Make sure the title is published.
                if (!BhlTitle.PublishReady)
                {
                    Response.Redirect("~/titlenotfound");
                }
            }

            CustomGenericList<Item> Items = bhlProvider.ItemSelectByTitleId(titleId);
            if (Items == null)
            {
                Response.Redirect("~/titlenotfound");
            }
            else
            {
                Barcode = Items[0].BarCode;
            }

            // Set the title for the COinS
            COinS.TitleID = titleId;

            // Set up the Mendeley share control
            mendeley.TitleID = titleId;

            // Assign Authors
            Authors = new List<Author>();
            AdditionalAuthors = new List<Author>();
            foreach (Author author in bhlProvider.AuthorSelectByTitleId(titleId))
            {
                if (author.AuthorRoleID >= 1 && author.AuthorRoleID <= 3)
                {
                    Authors.Add(author);
                }
                else
                {
                    AdditionalAuthors.Add(author);
                }
            }

            //CustomGenericList<Item> Items = bhlProvider.ItemSelectByTitleId(titleId);
            TitleKeywords = bhlProvider.TitleKeywordSelectKeywordByTitle(titleId);
            TitleIdentifiers = bhlProvider.Title_IdentifierSelectForDisplayByTitleID(titleId).ToList();
            TitleVariants = bhlProvider.TitleVariantSelectByTitleID(titleId).ToList();
            TitleAssociations = bhlProvider.TitleAssociationSelectByTitleId(titleId, true).ToList();
            Collections = bhlProvider.CollectionSelectAllForTitle(titleId).ToList();
            if (!string.IsNullOrEmpty(BhlTitle.LanguageCode)) LanguageName = bhlProvider.LanguageSelectAuto(BhlTitle.LanguageCode).LanguageName;

            BibliographicLevel bibliographicLevel = bhlProvider.BibliographicLevelSelect(BhlTitle.BibliographicLevelID ?? 0);
            if (bibliographicLevel == null)
            {
                Genre = string.Empty;
            }
            else
            {
                Genre = (bibliographicLevel.BibliographicLevelName.ToLower().Contains("serial") ? "Journal" : "Book");
            }

            CustomGenericList<DOI> dois = bhlProvider.DOISelectValidForTitle(titleId);
            if (dois.Count > 0) DOI = ConfigurationManager.AppSettings["DOIResolverURL"] + dois[0].DOIName;

            main.Page.Title = string.Format("Details - {0} - Biodiversity Heritage Library", BhlTitle.FullTitle);

            BibliographyItems = new List<BibliographyItem>();
            foreach (Item item in Items)
            {
                // Populate empty volume descriptions with default text
                if (string.IsNullOrWhiteSpace(item.Volume))
                {
                    item.Volume = "(no volume description)";
                }

                var page = bhlProvider.PageSelectFirstPageForItem(item.ItemID);
                string externalUrl = (page == null) ? "" : string.Format("/pagethumb/{0},100,100", page.PageID.ToString());
                BibliographyItems.Add(new BibliographyItem(item, externalUrl));
            }

            // Look for an OCLC identifier (use the first one... might need to account for multiple at some point)
            bool oclcFound = false;
            LocalLibraryUrl = @"http://worldcatlibraries.org/";
            foreach (Title_Identifier titleIdentifier in TitleIdentifiers)
            {
                if (string.Equals(titleIdentifier.IdentifierName, "OCLC", StringComparison.OrdinalIgnoreCase))
                {
                    LocalLibraryUrl += "wcpa/oclc/";
                    if (titleIdentifier.IdentifierValue.ToLower().StartsWith("ocm"))
                    {
                        //strip the "ocm" from the beginning of the value.
                        LocalLibraryUrl += titleIdentifier.IdentifierValue.Substring(3);
                    }
                    else
                    {
                        LocalLibraryUrl += titleIdentifier.IdentifierValue;
                    }
                    oclcFound = true;
                    break;
                }
            }
            if (!oclcFound)
            {
                string truncatedTitle = BhlTitle.FullTitle.Length > 220 ? BhlTitle.FullTitle.Substring(0, 220).Trim() : BhlTitle.FullTitle;
                LocalLibraryUrl += "search?q=" + truncatedTitle.Replace(" ", "+") + "&qt=owc_search";
            }

            // Look for any DDC call numbers for this title
            DDC = string.Empty;
            for (int x = TitleIdentifiers.Count - 1; x >= 0; x--)
            {
                Title_Identifier titleIdentifier = TitleIdentifiers[x];
                if (String.Compare(titleIdentifier.IdentifierName, "DDC", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    if (DDC.Length > 0) DDC += ", ";
                    DDC += titleIdentifier.IdentifierValue;

                    // Don't show this in the identifier list
                    TitleIdentifiers.Remove(titleIdentifier);
                }
            }

            // Get the MODS for this title
            //hypMODS.NavigateUrl += title.TitleID.ToString();
            OAI2.OAIRecord record = new OAI2.OAIRecord("oai:" + ConfigurationManager.AppSettings["OAIIdentifierNamespace"] + ":title/" + TitleId);
            OAIMODS.Convert mods = new OAIMODS.Convert(record);
            litMods.Text = Server.HtmlEncode(mods.ToString()).Replace("\n", "<br />");

            // Get the BibTex citations for this title
            try
            {
                litBibTeX.Text = bhlProvider.TitleBibTeXGetCitationStringForTitleID(BhlTitle.TitleID).Replace("\n", "<br />");
            }
            catch
            {
                litBibTeX.Text = string.Empty;
            }

            // Get the EndNote citation for this title
            try
            {
                litEndNote.Text = bhlProvider.TitleEndNoteGetCitationStringForTitleID(BhlTitle.TitleID, ConfigurationManager.AppSettings["ItemPageUrl"]).Replace("\n", "<br />");
            }
            catch
            {
                litEndNote.Text = string.Empty;
            }
        }
    }
}