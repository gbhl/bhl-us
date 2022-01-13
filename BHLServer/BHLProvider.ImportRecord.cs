using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ImportRecordStatus ImportRecordStatusSelectAuto(int importRecordStatusID)
        {
            return new ImportRecordStatusDAL().ImportRecordStatusSelectAuto(null, null, importRecordStatusID);
        }

        public List<ImportRecordStatus> ImportRecordStatusSelectAll()
        {
            return new ImportRecordStatusDAL().SelectAll(null, null);
        }

        public List<ImportRecord> ImportRecordSelectByImportFileID(int importFileID, int numRows, int startRow,
            string sortColumn, string sortDirection, int extended = 0)
        {
            return new ImportRecordDAL().ImportRecordSelectByImportFileID(null, null, importFileID, numRows, startRow,
                sortColumn, sortDirection, extended);
        }

        public List<ImportRecordReview> ImportRecordSelectForReviewByImportFileID(int importFileID, int numRows, int startRow,
            string sortColumn, string sortDirection, int extended = 0)
        {
            return new ImportRecordDAL().ImportRecordSelectForReviewByImportFileID(null, null, importFileID, numRows, startRow,
                sortColumn, sortDirection, extended);
        }

        public List<ImportRecordReview> ImportRecordSelectForReviewByImportRecordID(int importRecordID)
        {
            return new ImportRecordDAL().ImportRecordSelectForReviewByImportRecordID(null, null, importRecordID);
        }

        public void ImportRecordSave(ImportRecord citation, int userID)
        {
            // Perform the following actions if the record is new
            if (citation.ImportRecordID == default(int))
            {
                // Get production author IDs
                foreach(ImportRecordCreator author in citation.Authors)
                {
                    List<Author> authors = AuthorResolve(author.FullName, author.LastName,
                        author.FirstName, author.StartYear, author.EndYear, author.ImportedAuthorID);
                    if (authors.Count == 1) author.ProductionAuthorID = authors[0].AuthorID;
                }

                // Validate the imported record
                if (!IsImportRecordValid(citation))
                {
                    citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Invalid;
                }

                if (citation.ImportRecordStatusID == default(int))
                {
                    // Check for completeness of the imported record.
                    if (!IsImportRecordComplete(citation) || citation.Warnings.Count > 0)
                    {
                        citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Warning;
                    }
                }

                if (citation.ImportRecordStatusID == default(int))
                {
                    if (citation.SegmentID == null)
                    {
                        // Resolve with production segments.  If duplicates found, set status to Duplicate.
                        List<Segment> segments = SegmentResolve(citation.DOI, citation.StartPageID ?? 0);
                        if (segments.Count > 0)
                        {
                            citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Duplicate;
                        }
                    }
                }
            }

            // If no status set, set it to OK
            if (citation.ImportRecordStatusID == default(int))
            {
                citation.ImportRecordStatusID = (int)BHL.DataObjects.Enum.ImportRecordStatus.OK;
            }

            new ImportRecordDAL().ImportRecordSave(null, null, citation, userID);
        }

        public ImportRecord ImportRecordUpdateRecordStatus(int importRecordID, int importRecordStatusID, int userID)
		{
            ImportRecordDAL dal = new ImportRecordDAL();
			ImportRecord savedImportRecord = dal.ImportRecordSelectAuto(null, null, importRecordID);
			if ( savedImportRecord != null )
			{
                // If changing 'Warning' or 'Rejected' status to 'OK', then resolve this record 
                // against production data to determine if it is a duplicate of an existing segment.
                if (importRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.OK &&
                    (savedImportRecord.ImportRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.Warning ||
                    savedImportRecord.ImportRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.Rejected) &&
                    savedImportRecord.SegmentID == null)
                {
                    // Resolve with production segments.  If duplicates found, set status to Duplicate.
                    List<Segment> segments = SegmentResolve(savedImportRecord.DOI,
                        savedImportRecord.StartPageID ?? 0);
                    if (segments.Count > 0)
                    {
                        savedImportRecord.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Duplicate;
                    }
                }

                savedImportRecord.ImportRecordStatusID = importRecordStatusID;
                savedImportRecord.LastModifiedDate = DateTime.Now;
				savedImportRecord.LastModifiedUserID = userID;
				savedImportRecord = dal.ImportRecordUpdateAuto(null, null, savedImportRecord);
			}
			else
			{
				throw new Exception( "Could not find existing ImportRecord." );
			}
			return savedImportRecord;
		}

        public ImportRecordCreator ImportRecordCreatorUpdateAuthorID(int importRecordCreatorID, int? AuthorID, int userID)
        {
            ImportRecordCreatorDAL dal = new ImportRecordCreatorDAL();
            ImportRecordCreator savedImportRecordCreator = dal.ImportRecordCreatorSelectAuto(null, null, importRecordCreatorID);
            if (savedImportRecordCreator != null)
            {
                savedImportRecordCreator.ProductionAuthorID = AuthorID;
                savedImportRecordCreator.LastModifiedDate = DateTime.Now;
                savedImportRecordCreator.LastModifiedUserID = userID;
                savedImportRecordCreator = dal.ImportRecordCreatorUpdateAuto(null, null, savedImportRecordCreator);
            }
            else
            {
                throw new Exception("Could not find existing ImportRecordCreator.");
            }
            return savedImportRecordCreator;
        }

        /// <summary>
        /// Ensure that the imported record has valid Title, Item, Segment, and Page IDs (if they were provided).
        /// Also look for invalid author names (ex. "et al"), invalid author IDs, and mal-formed DOIs.
        /// 
        /// The "final" fields for all valid identifiers should be populated (ex. ItemID, TitleID, SegmentID, list 
        /// of ImportRecordPage records).
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        private bool IsImportRecordValid (ImportRecord citation)
        {
            bool isValid = true;

            // Check data type of item identifier
            if (string.IsNullOrWhiteSpace(citation.ItemIDString))
            {
                citation.ItemID = null;
            }
            else
            {
                if (Int32.TryParse(citation.ItemIDString, out int itemID))
                {
                    citation.ItemID = itemID;
                }
                else
                {
                    citation.ItemID = null;
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Item ID {0}", citation.ItemIDString)));
                    isValid = false;
                }
            }

            // Check data type of segment identifier
            if (string.IsNullOrWhiteSpace(citation.SegmentIDString))
            {
                citation.SegmentID = null;
                citation.ImportSegmentID = null;
            }
            else
            {
                if (Int32.TryParse(citation.SegmentIDString, out int segmentID))
                {
                    citation.SegmentID = segmentID;
                    citation.ImportSegmentID = segmentID;
                }
                else
                {
                    citation.SegmentID = null;
                    citation.ImportSegmentID = null;
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Segment ID {0}", citation.SegmentIDString)));
                    isValid = false;
                }
            }

            // Make sure Segment ID is valid BHL identifier
            if (citation.SegmentID != null)
            {
                if (SegmentSelectAuto((int)citation.SegmentID) == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Segment {0} not found", citation.SegmentIDString)));
                    isValid = false;
                }
            }

            // Given a segment ID, find the related Item ID.
            if (citation.ItemID == null && citation.SegmentID != null)
            {
                Segment segment = SegmentSelectForSegmentID((int)citation.SegmentID);
                if (segment != null)
                {
                    citation.ItemID = segment.BookID;
                    citation.ItemIDString = segment.BookID.ToString();
                }
            }

            // Make sure Item ID is valid BHL identifier.  If the segment is new, the book must be a non-virtual item. 
            bool isVirtual = false;
            if (citation.ItemID != null)
            {
                Book book = BookSelectAuto((int)citation.ItemID);
                if (book == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Item {0} not found", citation.ItemIDString)));
                    isValid = false;
                }
                else
                {
                    if (book.IsVirtual == 1)
                    {
                        isVirtual = true;

                        // Specified Item ID is for a virtual item
                        if (citation.SegmentID == null)
                        {
                            // New segments cannot be added to virtual items
                            citation.Errors.Add(GetNewImportRecordError(string.Format("'Import Segments' cannot be used to add Segments to Virtual Item {0}", citation.ItemIDString)));
                            isValid = false;
                        }
                        else if (citation.SegmentID != null && 
                            (!string.IsNullOrWhiteSpace(citation.StartPageIDString) || !string.IsNullOrWhiteSpace(citation.EndPageIDString)) || citation.PageIDs.Count > 0)
                        {
                            // Metadata for existing segments on virtual items can be updated, but not pages
                            citation.Warnings.Add(GetNewImportRecordWarning(string.Format("Page information will not be updated, because the segment is associated with a Virtual Item ({0})", citation.ItemIDString)));
                            citation.StartPageIDString = string.Empty;
                            citation.StartPageID = null;
                            citation.StartPage = string.Empty;
                            citation.EndPageIDString = string.Empty;
                            citation.EndPageID = null;
                            citation.EndPage = string.Empty;
                            citation.PageIDs.Clear();
                        }
                    }
                }
            }

            // Verify the start and end page identifiers
            int startPageID = -1;
            int endPageID = -1;
            if (!string.IsNullOrWhiteSpace(citation.StartPageIDString) && !isVirtual)
            {
                if (!Int32.TryParse(citation.StartPageIDString, out startPageID))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Start Page ID {0}", citation.StartPageID)));
                    isValid = false;
                }
                else
                {
                    citation.StartPageID = startPageID;
                    PageSummaryView page = PageSummarySelectByPageId(startPageID);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Start Page {0} not found", citation.StartPageID)));
                        isValid = false;
                    }
                    else
                    {
                        if (citation.ItemID == null) 
                        { 
                            citation.ItemID = page.BookID; 
                            citation.ItemIDString = page.BookID.ToString(); 
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(citation.EndPageIDString) && !isVirtual)
            {
                if (!Int32.TryParse(citation.EndPageIDString, out endPageID))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid End Page ID {0}", citation.EndPageID)));
                    isValid = false;
                }
                else
                {
                    citation.EndPageID = endPageID;
                    PageSummaryView page = PageSummarySelectByPageId(endPageID);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("End Page {0} not found", citation.EndPageID)));
                        isValid = false;
                    }
                    else
                    {
                        if (citation.ItemID == null) 
                        { 
                            citation.ItemID = page.BookID; 
                            citation.ItemIDString = page.BookID.ToString(); 
                        }
                    }
                }
            }

            // Given metadata and StartPage/EndPage (no IDs), try to find a single matching Item ID.  
            // If no item is found, this citation is still valid, but incomplete.  Status will be set to "Warning".
            if (citation.ItemID == null && startPageID == -1 && endPageID == -1)
            {
                List<Item> items = ItemResolve(citation.JournalTitle, citation.ISSN,
                    citation.ISBN, citation.OCLC, citation.Volume, citation.Issue, citation.Year);
                if (items.Count == 1) citation.ItemID = items[0].ItemID;
            }

            // Given an ItemID and StartPage/EndPage, try to find unique IDs for the Start and End page.
            // If the IDs are not found, this citation is valid, but incomplete.  Status will be set to "Warning".
            if (citation.ItemID != null && startPageID == -1 && !string.IsNullOrWhiteSpace(citation.StartPage) && !isVirtual)
            {
                List<Page> startPageIDs = PageSelectByItemAndPageNumber(
                    (int)citation.ItemID, citation.Volume, citation.Issue, citation.StartPage);
                if (startPageIDs.Count == 1)
                {
                    startPageID = startPageIDs[0].PageID;
                    citation.StartPageID = startPageID;
                }
            }

            if (citation.ItemID != null && endPageID == -1 && !string.IsNullOrWhiteSpace(citation.EndPage) && !isVirtual)
            {
                List<Page> endPageIDs = PageSelectByItemAndPageNumber(
                    (int)citation.ItemID, citation.Volume, citation.Issue, citation.EndPage);
                if (endPageIDs.Count == 1)
                {
                    endPageID = endPageIDs[0].PageID;
                    citation.EndPageID = endPageID;
                }
            }

            // Given a SegmentID and StartPage/EndPage, try to find unique IDs for the Start and End page.
            // If the IDs are not found, this citation is valid, but incomplete.  Status will be set to "Warning".
            if (citation.SegmentID != null && startPageID == -1 && !string.IsNullOrWhiteSpace(citation.StartPage))
            {
                List<Page> startPageIDs = PageSelectBySegmentAndPageNumber((int)citation.SegmentID, citation.StartPage);
                if (startPageIDs.Count == 1)
                {
                    startPageID = startPageIDs[0].PageID;
                    citation.StartPageID = startPageID;
                }
            }

            if (citation.SegmentID != null && endPageID == -1 && !string.IsNullOrWhiteSpace(citation.EndPage))
            {
                List<Page> endPageIDs = PageSelectBySegmentAndPageNumber((int)citation.SegmentID, citation.EndPage);
                if (endPageIDs.Count == 1)
                {
                    endPageID = endPageIDs[0].PageID;
                    citation.EndPageID = endPageID;
                }
            }

            if (isValid && startPageID != -1 && endPageID != -1)
            {
                // Make sure the start page, end page, segment, and item identifiers all belong to the same item.
                List<Page> pageRange = PageSelectRangeForPagesAndItem(startPageID, endPageID, citation.ItemID);
                if (pageRange.Count == 0)
                {
                    citation.Errors.Add(
                        GetNewImportRecordError(
                            string.Format("Item {0}, Start Page {1}, and End Page {2} are mismatched", 
                                citation.ItemIDString, citation.StartPageID.ToString(), citation.EndPageID.ToString())
                            )
                        );
                    isValid = false;
                }
                else
                {
                    // Add ImportRecordPage records for the range of page ids
                    foreach(Page page in pageRange)
                    {
                        ImportRecordPage irPage = GetNewImportRecordPage(page.PageID);
                        irPage.SequenceOrder = (short)(citation.Pages.Count + 1);
                        citation.Pages.Add(irPage);
                    }
                }
            }

            // Verify any additional page IDs
            foreach(string pageID in citation.PageIDs)
            {
                if (pageID.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null)
                {
                    // Nothing to do, since NULL specified as the "Additional pages" value for an existing segment.  Existing additional page
                    // records will be removed as part of the update process.
                }
                else if (Int32.TryParse(pageID, out int pid))   // Check data type of page id
                {
                    // Make sure page id is valid BHL identifier

                    PageSummaryView page = PageSummarySelectByPageId(pid);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Page {0} not found", pid.ToString())));
                        isValid = false;
                    }
                    else
                    {
                        if (page.BookID != citation.ItemID && (!string.IsNullOrWhiteSpace(citation.ItemIDString) || !string.IsNullOrWhiteSpace(citation.SegmentIDString)))
                        {
                            citation.Errors.Add(GetNewImportRecordError(string.Format("Page {0} not part of Item {1}", pid.ToString(), citation.ItemID)));
                            isValid = false;
                        }
                        else
                        {
                            ImportRecordPage irPage = GetNewImportRecordPage(pid);
                            irPage.SequenceOrder = (short)(citation.Pages.Count + 1);
                            citation.Pages.Add(irPage);
                        }
                    }
                }
                else
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Page ID {0}", pageID)));
                    isValid = false;
                }
            }

            // Make sure that at least one non-"NULL" contributor has been specified
            //var nonNullContribs = citation.Contributors
            //    .Where(c => c.InstitutionCode.ToUpper().Trim() != "NULL")
            //    .ToList();
            if (citation.Contributors.Count == 0 && citation.ImportSegmentID == null)
            {
                citation.Errors.Add(GetNewImportRecordError("At least one contributor must be specified"));
                isValid = false;
            }

            // If more than two contributors were specified, remove everything after the first two and alert the user.
            // This does NOT produce an invalid citation.
            if (citation.Contributors.Count > 2)
            {
                citation.Warnings.Add(GetNewImportRecordWarning("Only the first two contributors will be imported"));
                citation.Contributors.RemoveRange(2, citation.Contributors.Count - 2);
            }

            // Make sure the contributor names/codes are valid
            for (int x = 0; x < citation.Contributors.Count; x++)
            {
                ImportRecordContributor contributor = citation.Contributors[x];

                string institutionCode = IsContributorValid(contributor.InstitutionCode);
                if (institutionCode == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Contributor {0} is invalid", contributor.InstitutionCode)));
                    isValid = false;
                }
                else
                {
                    citation.Contributors[x].InstitutionCode = institutionCode;
                }
            }

            // Make sure author names and IDs are valid
            foreach (ImportRecordCreator author in citation.Authors)
            {
                if (author.FullName.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null)
                {
                    // Nothing to do, since NULL specified as the "Author Names" value for an existing segment is a valid indication to delete all authors from the segment.
                }
                else
                {
                    if (author.FullName.ToLower().Contains("et al") ||
                        author.LastName.ToLower().Contains("et al") ||
                        author.FirstName.ToLower().Contains("et al"))
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Author {0}", author.FullName)));
                        isValid = false;
                    }

                    if (author.ImportedAuthorID != null &&
                        author.ProductionAuthorID == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Author ID {0} is invalid", author.ImportedAuthorID.ToString())));
                        isValid = false;
                    }

                    if (author.ImportedAuthorID != null &&
                        author.ProductionAuthorID != null &&
                        author.ImportedAuthorID != author.ProductionAuthorID)
                    {
                        citation.Warnings.Add(GetNewImportRecordWarning(string.Format(
                            "Author ID {0} substituted for Author ID {1}", author.ProductionAuthorID.ToString(), author.ImportedAuthorID.ToString())));
                    }
                }
            }

            // Make sure the ISSN is syntactically correct.  Ignore the values "NULL" if it is part of a segment update.
            if (!string.IsNullOrWhiteSpace(citation.ISSN) &&
                !(citation.ISSN.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                Identifier identifierDefinition = IdentifierSelectAll().Where(i => i.IdentifierName == "ISSN").Single();
                IdentifierValidationResult idValidateResult = ValidateIdentifier(citation.ISSN, citation.ImportSegmentID == null, identifierDefinition);
                if (!idValidateResult.IsValid)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("{0} is an invalid format.  {1}", citation.ISSN, identifierDefinition.PatternDescription)));
                    isValid = false;
                }
            }

            // Make sure that DOIs are syntactically correct.  Ignore the value "NULL" if it is part of a segment update.
            if (!string.IsNullOrWhiteSpace(citation.DOI) &&
                !(citation.DOI.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                // Strip off doi.org prefixes
                citation.DOI = citation.DOI.Replace("http://dx.doi.org/", "").Replace("https://dx.doi.org/", "")
                    .Replace("dx.doi.org/", "").Replace("http://doi.org/", "").Replace("https://doi.org/", "")
                    .Replace("doi.org/", "");

                // Check for an existing DOI value
                string existingDOI = string.Empty;
                if (citation.ImportSegmentID != null)
                {
                    List<ItemIdentifier> dois = ItemIdentifierSelectByNameAndID("DOI", (int)citation.ImportSegmentID);
                    if (dois.Count > 0) existingDOI = dois[0].IdentifierValue;
                }

                // Make sure the cleaned DOI value is valid
                Identifier identifierDefinition = IdentifierSelectAll().Where(i => i.IdentifierName == "DOI").Single();
                IdentifierValidationResult idValidateResult = ValidateIdentifier(citation.DOI, citation.DOI != existingDOI, identifierDefinition);
                if (!idValidateResult.IsValid)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("{0} is an invalid DOI format.  The valid format is prefix/suffix, where prefix is 10.NNNN.", citation.DOI)));
                    isValid = false;
                }
                else
                {
                    string bhlPrefix = "10.5962";

                    // If a BHL-assigned DOI (prefix 10.5962) is being added or replaced, issue a warning indicating that the change will be ignored.
                    // This does NOT produce an invalid citation.
                    if (citation.DOI != existingDOI)
                    {
                        if (existingDOI.StartsWith(bhlPrefix))
                        {
                            citation.Warnings.Add(GetNewImportRecordWarning(string.Format("Existing BHL-assigned DOI {0} can not be modified via a batch update", existingDOI)));
                        }
                        else if (citation.DOI.StartsWith(bhlPrefix))
                        {
                            citation.Warnings.Add(GetNewImportRecordWarning(string.Format("BHL-assigned DOI {0} cannot be added via a batch update", citation.DOI)));
                        }
                    }
                }
            }

            // Make sure a Title is supplied during a segment addition, and is not being removed during a segment update
            if ((string.IsNullOrWhiteSpace(citation.Title) && citation.ImportSegmentID == null) ||
                (citation.Title.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                citation.Errors.Add(GetNewImportRecordError(string.Format("Title cannot be blank")));
                isValid = false;
            }

            // Make sure that the Volume value is formatted correctly. Ignore the value "NULL" if it is part of a segment update.
            if (!string.IsNullOrWhiteSpace(citation.Volume) &&
                !(citation.Volume.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                if (!DataCleaner.ValidateSegmentVolumeIssue(citation.Volume))
                {
                    citation.Errors.Add(GetNewImportRecordError(
                        string.Format("Volume {0} must be NN, NN-NN, or NN/NN", citation.Volume)));
                    isValid = false;
                }
            }

            // Make sure that the Issue value is formatted correctly. Ignore the value "NULL" if it is part of a segment update.
            if (!string.IsNullOrWhiteSpace(citation.Issue) &&
                !(citation.Issue.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                if (!DataCleaner.ValidateSegmentVolumeIssue(citation.Issue))
                {
                    citation.Errors.Add(GetNewImportRecordError(
                        string.Format("Issue {0} must be NN, NN-NN, or NN/NN", citation.Issue)));
                    isValid = false;
                }
            }

            // Make sure that the date value is formatted correctly.  Ignore the value "NULL" if it is part of a segment update.
            // Only YYYY, YYYY-MM, YYYY-MM-DD, and YYYY-YYYY are accepted.
            if (!string.IsNullOrWhiteSpace(citation.Year) &&
                !(citation.Year.ToUpper().Trim() == "NULL" && citation.ImportSegmentID != null))
            {
                Regex regexYYYY = new Regex("^[0-9]{4}$");
                Regex regexYYYYMM = new Regex("^[0-9]{4}-[0-9]{2}$");
                Regex regexYYYYMMDD = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2}$");
                Regex regexYYYYYYYY = new Regex("^[0-9]{4}-[0-9]{4}$");
                if (!(regexYYYY.Match(citation.Year).Success ||
                    regexYYYYMM.Match(citation.Year).Success ||
                    regexYYYYMMDD.Match(citation.Year).Success ||
                    regexYYYYYYYY.Match(citation.Year).Success))
                {
                    citation.Errors.Add(GetNewImportRecordError(
                        string.Format("Date {0} must be YYYY, YYYY-MM, YYYY-MM-DD, or YYYY-YYYY.", citation.Year)));
                    isValid = false;
                }

            }

            // Check data type of title identifier
            if (string.IsNullOrWhiteSpace(citation.TitleIDString))
            {
                citation.TitleID = null;
            }
            else
            {
                if (Int32.TryParse(citation.TitleIDString, out int titleID))
                {
                    citation.TitleID = titleID;
                }
                else
                {
                    citation.TitleID = null;
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Title ID {0}", citation.TitleIDString)));
                    isValid = false;
                }
            }

            // Make sure Title ID is valid BHL identifier
            if (citation.TitleID != null)
            {
                if (TitleSelectAuto((int)citation.TitleID) == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Title {0} not found", citation.TitleIDString)));
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Returns the InstitutionCode if the contributor code/name is valid
        /// </summary>
        /// <param name="contributorValue"></param>
        /// <returns></returns>
        private string IsContributorValid(string contributorValue)
        {
            string contributorCode = null;
            if (contributorValue == string.Empty)
            {
                contributorCode = contributorValue;
            }
            else
            {
                Institution institution = this.InstitutionSelectAuto(contributorValue);    // select by code
                if (institution == null) institution = this.InstitutionSelectByName(contributorValue);  //select by name
                if (institution != null) contributorCode = institution.InstitutionCode;
            }
            return contributorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        private bool IsImportRecordComplete(ImportRecord citation)
        {
            bool isComplete = true;

            if (string.IsNullOrWhiteSpace(citation.ItemID.ToString()))
            {
                citation.Warnings.Add(GetNewImportRecordWarning(string.Format("No item can be identified from the supplied metadata.")));
                isComplete = false;
            }

            // If pages were supplied or no segment ID was included in the import file, but the Page.Count is still 0, then the segment is incomplete
            if (citation.Pages.Count == 0 && 
                (citation.ImportSegmentID == null ||
                ((citation.PageIDs.Count > 0 && citation.PageIDs[0].ToUpper().Trim() != "NULL") || !string.IsNullOrWhiteSpace(citation.StartPage) || 
                 !string.IsNullOrWhiteSpace(citation.EndPage) || !string.IsNullOrWhiteSpace(citation.StartPageIDString) || !string.IsNullOrWhiteSpace(citation.EndPageIDString))
                )
               )
            {
                citation.Warnings.Add(GetNewImportRecordWarning(string.Format("No pages supplied, or pages could not be identified.")));
                isComplete = false;
            }

            return isComplete;
        }

        private ImportRecordErrorLog GetNewImportRecordError(string message)
        {
            return GetNewImportRecordErrorLog(message, "Error");
        }

        private ImportRecordErrorLog GetNewImportRecordWarning(string message)
        {
            return GetNewImportRecordErrorLog(message, "Warning");
        }

        private ImportRecordErrorLog GetNewImportRecordErrorLog(string message, string severity)
        {
            ImportRecordErrorLog importRecordErrorLog = new ImportRecordErrorLog();
            importRecordErrorLog.ErrorDate = DateTime.Now;
            importRecordErrorLog.ErrorMessage = message;
            importRecordErrorLog.Severity = severity;
            return importRecordErrorLog;
        }

        private ImportRecordPage GetNewImportRecordPage(int pageID)
        {
            ImportRecordPage importRecordPage = new ImportRecordPage();
            importRecordPage.PageID = pageID;
            return importRecordPage;
        }
    }
}
