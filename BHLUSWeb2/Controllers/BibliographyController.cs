using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System.Collections.Generic;
using System;
using System.Web.Mvc;
using MvcThrottle;
using MOBOT.BHL.API.BHLApiDataObjects2;
using System.Drawing;

namespace MOBOT.BHL.Web2.Controllers
{
    public class BibliographyController : Controller
    {
        [EnableThrottling]
        [HttpGet]
        // GET: Bibliography
        public ActionResult Index(int titleid)
        {
            BHLProvider bhlProvider = new BHLProvider();
            BibliographyModel model = new BibliographyModel();
            model.Title = bhlProvider.TitleSelect(titleid);
            if (model.Title == null)
            {
                return Redirect("~/titlenotfound");
            }
            else
            {
                // Check to make sure this title hasn't been replaced.  If it has, redirect
                // to the appropriate titleid.
                if (model.Title.RedirectTitleID != null)
                {
                    return Redirect("~/bibliography/" + model.Title.RedirectTitleID);
                }

                // Make sure the title is published.
                if (!model.Title.PublishReady)
                {
                    return Redirect("~/titleunavailable");
                }
            }

            List<DataObjects.Book> Books = bhlProvider.BookSelectByTitleId(titleid);
            if (Books == null)
            {
                Response.Redirect("~/titleunavailable");
            }
            if (Books.Count == 0)
            {
                Response.Redirect("~/titleunavailable");
            }
            else
            {
                model.Barcode = Books[0].BarCode;
            }

            // Assign Authors
            model.Authors = new List<Author>();
            model.AuthorsDetail = new List<Author>();
            model.AdditionalAuthors = new List<Author>();
            model.AdditionalAuthorsDetail = new List<Author>();
            List<Author> authorList = bhlProvider.AuthorSelectByTitleId(titleid);
            foreach (Author author in authorList)
            {
                if (author.AuthorRoleID >= 1 && author.AuthorRoleID <= 3)
                {
                    model.AuthorsDetail.Add(author);
                    if (!ListContainsAuthor(model.Authors, author.AuthorID, author.Relationship)) model.Authors.Add(author);
                }
                else
                {
                    model.AdditionalAuthorsDetail.Add(author);
                    if (!ListContainsAuthor(model.Authors, author.AuthorID, author.Relationship) &&
                        !ListContainsAuthor(model.AdditionalAuthors, author.AuthorID, author.Relationship)) model.AdditionalAuthors.Add(author);
                }
            }

            model.TitleKeywords = bhlProvider.TitleKeywordSelectKeywordByTitle(titleid);
            model.TitleIdentifiers = bhlProvider.Title_IdentifierSelectForDisplayByTitleID(titleid);
            model.TitleVariants = bhlProvider.TitleVariantSelectByTitleID(titleid);
            model.TitleAssociations = bhlProvider.TitleAssociationSelectByTitleId(titleid, true);
            model.TitleNotes = bhlProvider.TitleNoteSelectByTitleID(titleid);
            model.TitleExternalResources = bhlProvider.TitleExternalResourceSelectByTitleID(titleid);
            model.Collections = bhlProvider.CollectionSelectAllForTitle(titleid);
            //Institutions = bhlProvider.InstitutionSelectByTitleID(titleId);
            if (!string.IsNullOrEmpty(model.Title.LanguageCode)) model.LanguageName = bhlProvider.LanguageSelectAuto(model.Title.LanguageCode).LanguageName;

            BibliographicLevel bibliographicLevel = bhlProvider.BibliographicLevelSelect(model.Title.BibliographicLevelID ?? 0);
            model.Genre = (bibliographicLevel == null) ? string.Empty : bibliographicLevel.BibliographicLevelLabel;

            DataObjects.MaterialType materialType = bhlProvider.MaterialTypeSelect(model.Title.MaterialTypeID ?? 0);
            model.Material = (materialType == null) ? string.Empty : materialType.MaterialTypeLabel;

            model.BibliographyItems = new List<BibliographyItem>();
            foreach (DataObjects.Book book in Books)
            {
                // Populate empty volume descriptions with default text
                if (string.IsNullOrWhiteSpace(book.Volume)) book.Volume = "Volume details";

                string externalUrl = (book.FirstPageID == null) ? "" : string.Format("/pagethumb/{0},100,100", book.FirstPageID.ToString());
                BibliographyItem bibliographyItem = new BibliographyItem(book, externalUrl)
                {
                    institutions = bhlProvider.InstitutionSelectByItemID((int)book.ItemID)
                };

                model.BibliographyItems.Add(bibliographyItem);
            }

            // Look for an OCLC identifier (use the first one... might need to account for multiple at some point)
            bool oclcFound = false;
            model.LocalLibraryUrl = @"https://worldcat.org/";
            foreach (Title_Identifier titleIdentifier in model.TitleIdentifiers)
            {
                if (string.Equals(titleIdentifier.IdentifierName, "OCLC", StringComparison.OrdinalIgnoreCase))
                {
                    model.LocalLibraryUrl += "title/";
                    if (titleIdentifier.IdentifierValue.ToLower().StartsWith("ocm"))
                    {
                        //strip the "ocm" from the beginning of the value.
                        model.LocalLibraryUrl += titleIdentifier.IdentifierValue.Substring(3);
                    }
                    else
                    {
                        model.LocalLibraryUrl += titleIdentifier.IdentifierValue;
                    }
                    oclcFound = true;
                    break;
                }
            }
            if (!oclcFound)
            {
                string truncatedTitle = model.Title.FullTitle.Length > 220 ? model.Title.FullTitle.Substring(0, 220).Trim() : model.Title.FullTitle;
                model.LocalLibraryUrl += "search?q=" + truncatedTitle.Replace(" ", "+") + "&qt=owc_search";
            }

            // Look for any DDC call numbers for this title
            model.DDC = string.Empty;
            for (int x = model.TitleIdentifiers.Count - 1; x >= 0; x--)
            {
                Title_Identifier titleIdentifier = model.TitleIdentifiers[x];
                if (String.Compare(titleIdentifier.IdentifierName, "DDC", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    if (model.DDC.Length > 0) model.DDC += ", ";
                    model.DDC += titleIdentifier.IdentifierValue;

                    // Don't show this in the identifier list
                    model.TitleIdentifiers.Remove(titleIdentifier);
                }
            }

            // Set the data for the COinS output
            model.COinS.TitleID = titleid;
            model.COinS.TitleIdentifiers = model.TitleIdentifiers;
            model.COinS.TitleKeywords = model.TitleKeywords;
            model.COinS.TitleAuthors = authorList;
            model.COinS.MarcLeader = model.Title.MARCLeader;
            model.COinS.Title = model.Title.FullTitle;
            model.COinS.Publisher = model.Title.Datafield_260_b;
            model.COinS.PublisherPlace = model.Title.Datafield_260_a;
            model.COinS.Edition = model.Title.EditionStatement;
            model.COinS.Language = model.Title.LanguageCode;
            model.COinS.Date = model.Title.StartYear.ToString();

            ViewBag.COinS = "<span class=\"Z3988\" title=\"" + model.COinS.GetCOinS() + "\"></span>";

            return View(model);
        }

        private bool ListContainsAuthor(IList<Author> list, int authorID, string relationship)
        {
            bool containsAuthor = false;

            foreach (Author author in list)
            {
                if (author.AuthorID == authorID && author.Relationship == relationship)
                {
                    containsAuthor = true;
                    break;
                }
            }

            return containsAuthor;
        }
    }
}