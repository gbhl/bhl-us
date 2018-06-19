﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleImportResults : System.Web.UI.Page
    {
        private List<Title> _inserted = new List<Title>();
        private List<Title> _updated = new List<Title>();
        private List<String> _error = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Save the batch id
                String batchID = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();

                // Get the list of titles in this batch
                int batchInt;
                if (Int32.TryParse(batchID, out batchInt))
                {
                    // Get the id of the current user
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    this.ImportMarcBatch(batchInt, userId);

                    litInsertedCount.Text = _inserted.Count.ToString();
                    litUpdatedCount.Text = _updated.Count.ToString();
                    litErrorCount.Text = _error.Count.ToString();

                    if (_inserted.Count > 0)
                    {
                        litInserted.Visible = true;
                        dlInserted.DataSource = _inserted;
                        dlInserted.DataBind();
                    }

                    if (_updated.Count > 0)
                    {
                        litUpdated.Visible = true;
                        dlUpdated.DataSource = _updated;
                        dlUpdated.DataBind();
                    }

                    if (_error.Count > 0)
                    {
                        litErrHeader.Visible = true;
                        dlErrors.DataSource = _error;
                        dlErrors.DataBind();
                    }
                }
                else
                {
                    errorControl.AddErrorText("Invalid batch identifier.");
                    errorControl.Visible = true;
                }
            }
        }

        private void ImportMarcBatch(int batchID, int userID)
        {
            BHLProvider provider = new BHLProvider();

            CustomGenericList<Marc> marcs = provider.MarcSelectForImportByBatchID(batchID);

            foreach (Marc marc in marcs)
            {
                Title title = null;

                try
                {
                    title = null;
                    String marcBibId = marc.Leader.Trim().Replace(' ', 'x');

                    if (marc.BhlTitleId == 0)
                    {
                        // Build new title class, populate, and save
                        title = provider.MarcSelectTitleDetailsByMarcID(marc.MarcID);
                        title.IsNew = true;

                        // Grab all of the supporting data (subjects, languages, authors, identifiers, associations)
                        CustomGenericList<TitleKeyword> titleKeywords = provider.MarcSelectTitleKeywordsByMarcID(marc.MarcID);
                        foreach (TitleKeyword titleKeyword in titleKeywords)
                        {
                            titleKeyword.IsNew = true;
                            title.TitleKeywords.Add(titleKeyword);
                        }
                        CustomGenericList<TitleLanguage> titleLanguages = provider.MarcSelectTitleLanguagesByMarcID(marc.MarcID);
                        foreach (TitleLanguage titleLanguage in titleLanguages)
                        {
                            titleLanguage.IsNew = true;
                            title.TitleLanguages.Add(titleLanguage);
                        }
                        CustomGenericList<TitleNote> titleNotes = provider.MarcSelectTitleNotesByMarcID(marc.MarcID);
                        foreach(TitleNote titleNote in titleNotes)
                        {
                            titleNote.IsNew = true;
                            title.TitleNotes.Add(titleNote);
                        }
                        CustomGenericList<TitleAuthor> titleAuthors = provider.MarcSelectAuthorsByMarcID(marc.MarcID);
                        foreach (TitleAuthor titleAuthor in titleAuthors)
                        {
                            titleAuthor.IsNew = true;
                            title.TitleAuthors.Add(titleAuthor);
                        }
                        CustomGenericList<Title_Identifier> titleIdentifiers = provider.MarcSelectTitleIdentifiersByMarcID(marc.MarcID);
                        foreach (Title_Identifier titleIdentifier in titleIdentifiers)
                        {
                            titleIdentifier.IsNew = true;
                            title.TitleIdentifiers.Add(titleIdentifier);
                        }
                        CustomGenericList<TitleAssociation> titleAssociations = provider.MarcSelectAssociationsByMarcID(marc.MarcID);
                        foreach (TitleAssociation titleAssociation in titleAssociations)
                        {
                            titleAssociation.IsNew = true;

                            // Get title association title identifiers
                            titleAssociation.TitleAssociationIdentifiers =
                                provider.MarcSelectAssociationIdsByMarcDataFieldID(titleAssociation.MarcDataFieldID);
                            title.TitleAssociations.Add(titleAssociation);
                        }

                        CustomGenericList<TitleVariant> titleVariants = provider.MarcSelectVariantsByMarcID(marc.MarcID);
                        foreach(TitleVariant titleVariant in titleVariants)
                        {
                            titleVariant.IsNew = true;
                            title.TitleVariants.Add(titleVariant);
                        }

                        // Save the new title
                        title = provider.TitleSave(title, userID);

                        // Copy MARC XML file to the appropriate server
                        // - Create new folder in current vault (using MarcBibID as name)
                        // - Copy MARC.XML file to new folder (using MarcBibID as name)
                        try
                        {
                            SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                            service.MarcCreateFile(title.MARCBibID, System.IO.File.ReadAllText(marc.MarcFileLocation));
                        }
                        catch
                        {
                            // Do nothing... file copy is not critical
                        }

                        _inserted.Add(title);
                    }
                    else
                    {
                        // Load existing title and new title information
                        title = provider.TitleSelectExtended(marc.BhlTitleId);
                        Title marcTitle = provider.MarcSelectTitleDetailsByMarcID(marc.MarcID);

                        // Update title values
                        title.MARCLeader = marcTitle.MARCLeader;
                        title.BibliographicLevelID = marcTitle.BibliographicLevelID;
                        title.MaterialTypeID = marcTitle.MaterialTypeID;
                        title.FullTitle = marcTitle.FullTitle;
                        title.ShortTitle = marcTitle.ShortTitle;
                        title.UniformTitle = marcTitle.UniformTitle;
                        title.SortTitle = marcTitle.SortTitle;
                        title.CallNumber = marcTitle.CallNumber;
                        title.PublicationDetails = marcTitle.PublicationDetails;
                        title.StartYear = marcTitle.StartYear;
                        title.EndYear = marcTitle.EndYear;
                        title.Datafield_260_a = marcTitle.Datafield_260_a;
                        title.Datafield_260_b = marcTitle.Datafield_260_b;
                        title.Datafield_260_c = marcTitle.Datafield_260_c;
                        title.LanguageCode = marcTitle.LanguageCode;
                        title.OriginalCatalogingSource = marcTitle.OriginalCatalogingSource;
                        title.EditionStatement = marcTitle.EditionStatement;
                        title.CurrentPublicationFrequency = marcTitle.CurrentPublicationFrequency;
                        title.PartName = marcTitle.PartName;
                        title.PartNumber = marcTitle.PartNumber;

                        // Replace all subjects associated with this title
                        foreach (TitleKeyword titleKeyword in title.TitleKeywords)
                        {
                            titleKeyword.IsDeleted = true;
                        }

                        CustomGenericList<TitleKeyword> titleKeywords = provider.MarcSelectTitleKeywordsByMarcID(marc.MarcID);
                        foreach (TitleKeyword titleKeyword in titleKeywords)
                        {
                            titleKeyword.IsNew = true;
                            titleKeyword.TitleID = title.TitleID;
                            title.TitleKeywords.Add(titleKeyword);
                        }

                        // Replace all languages associated with this title
                        foreach (TitleLanguage titlelanguage in title.TitleLanguages)
                        {
                            titlelanguage.IsDeleted = true;
                        }

                        CustomGenericList<TitleLanguage> titleLanguages = provider.MarcSelectTitleLanguagesByMarcID(marc.MarcID);
                        foreach (TitleLanguage titleLanguage in titleLanguages)
                        {
                            titleLanguage.IsNew = true;
                            titleLanguage.TitleID = title.TitleID;
                            title.TitleLanguages.Add(titleLanguage);
                        }

                        // Replace all notes associated with this title
                        foreach(TitleNote titleNote in title.TitleNotes)
                        {
                            titleNote.IsDeleted = true;
                        }

                        CustomGenericList<TitleNote> titleNotes = provider.MarcSelectTitleNotesByMarcID(marc.MarcID);
                        foreach (TitleNote titleNote in titleNotes)
                        {
                            titleNote.IsNew = true;
                            titleNote.TitleID = title.TitleID;
                            title.TitleNotes.Add(titleNote);
                        }

                        // Replace all title authors associated with this title
                        foreach (TitleAuthor titleAuthor in title.TitleAuthors)
                        {
                            titleAuthor.IsDeleted = true;
                        }

                        CustomGenericList<TitleAuthor> titleAuthors = provider.MarcSelectAuthorsByMarcID(marc.MarcID);
                        foreach (TitleAuthor titleAuthor in titleAuthors)
                        {
                            titleAuthor.IsNew = true;
                            titleAuthor.TitleID = title.TitleID;
                            title.TitleAuthors.Add(titleAuthor);
                        }

                        // Replace all identifiers associated with this title
                        foreach (Title_Identifier title_identifier in title.TitleIdentifiers)
                        {
                            title_identifier.IsDeleted = true;
                        }

                        CustomGenericList<Title_Identifier> titleIdentifiers = provider.MarcSelectTitleIdentifiersByMarcID(marc.MarcID);
                        foreach (Title_Identifier titleIdentifier in titleIdentifiers)
                        {
                            titleIdentifier.IsNew = true;
                            titleIdentifier.TitleID = title.TitleID;
                            title.TitleIdentifiers.Add(titleIdentifier);
                        }

                        // Replace all associations associated with this title
                        foreach (TitleAssociation titleAssociation in title.TitleAssociations)
                        {
                            titleAssociation.IsDeleted = true;
                        }

                        CustomGenericList<TitleAssociation> titleAssociations = provider.MarcSelectAssociationsByMarcID(marc.MarcID);
                        foreach (TitleAssociation titleAssociation in titleAssociations)
                        {
                            titleAssociation.IsNew = true;
                            titleAssociation.TitleID = title.TitleID;
                            titleAssociation.Active = true;

                            // Get title association title identifiers
                            titleAssociation.TitleAssociationIdentifiers = 
                                provider.MarcSelectAssociationIdsByMarcDataFieldID(titleAssociation.MarcDataFieldID);
                            title.TitleAssociations.Add(titleAssociation);
                        }

                        // Replace all variants associated with this title
                        foreach(TitleVariant titleVariant in title.TitleVariants)
                        {
                            titleVariant.IsDeleted = true;
                        }

                        CustomGenericList<TitleVariant> titleVariants = provider.MarcSelectVariantsByMarcID(marc.MarcID);
                        foreach(TitleVariant titleVariant in titleVariants)
                        {
                            titleVariant.IsNew = true;
                            titleVariant.TitleID = title.TitleID;
                            title.TitleVariants.Add(titleVariant);
                        }

                        // Update the title
                        title = provider.TitleSave(title, userID);

                        try
                        {
                            // Copy MARC XML file to the appropriate location(s)
                            CustomGenericList<PageSummaryView> folders = provider.PageSummarySelectFoldersForTitleID(title.TitleID);
                            CustomGenericList<PageSummaryView> barcodes = provider.PageSummarySelectBarcodeForTitleID(title.TitleID);

                            // For each folder containing items for this title
                            foreach (PageSummaryView folder in folders)
                            {
                                // Get the files in the folder
                                MOBOT.FileAccess.IFileAccessProvider fileAccess =
                                    provider.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
                                String destinationFolder = folder.OCRFolderShare + "\\" + folder.FileRootFolder;
                                String[] marcXmlFiles = fileAccess.GetFiles(destinationFolder);

                                // Check each file
                                foreach (String marcXmlFile in marcXmlFiles)
                                {
                                    if (marcXmlFile.EndsWith("_marc.xml"))
                                    {
                                        // See if any file is related to this title
                                        foreach (PageSummaryView barcode in barcodes)
                                        {
                                            if (marcXmlFile.EndsWith(barcode.BarCode + "_marc.xml"))
                                            {
                                                // Found a match, so replace the file
                                                fileAccess.MoveFile(marcXmlFile, marcXmlFile + "." + System.DateTime.Now.Ticks.ToString());
                                                fileAccess.CopyFile(marc.MarcFileLocation, marcXmlFile, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // Do nothing... file operations not critical
                        }

                        _updated.Add(title);
                    }

                    // Update the record to indicate that the import has completed
                    provider.MarcUpdateStatusImported(marc.MarcID);
                }
                catch (Exception ex)
                {
                    // Update the record to indicate the failed import
                    provider.MarcUpdateStatusError(marc.MarcID);

                    if (title != null)
                    {
                        _error.Add("Error loading '" + title.ShortTitle + "': " + ex.Message);
                    }
                    else
                    {
                        _error.Add("Error loading MARC ID " + marc.MarcID.ToString() + ": " + ex.Message);
                    }
                }
            }
        }
    }
}
