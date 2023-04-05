using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHL.DOIDeposit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;

namespace MOBOT.BHL.BHLDOIService
{
    public class DOIProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> submittedDOIAdds = new List<string>();
        private List<string> submittedDOIUpdates = new List<string>();
        private List<string> approvedDOIAdds = new List<string>();
        private List<string> approvedDOIUpdates = new List<string>();
        private List<string> warningDOIs = new List<string>();
        private List<string> rejectedDOIs = new List<string>();
        private List<string> unverifiedDOIs = new List<string>();
        private List<string> foundDOIs = new List<string>();
        private List<string> unresolvedEntities = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            this.LogMessage("BHLDOIService Processing Starting");

            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Generate and submit DOIs for any titles without DOIs
            if (configParms.SubmitTitles) this.ProcessEntitiesWithoutDOIs(configParms.DoiEntityTypeTitle, 
                new TitlesClient(configParms.BHLWSRestEndpoint), TitleSelectWithoutSubmittedDOI);

            // Generate and submit DOIs for any segments without DOIs
            if (configParms.SubmitSegments) this.ProcessEntitiesWithoutDOIs(configParms.DoiEntityTypeSegment, 
                new SegmentsClient(configParms.BHLWSRestEndpoint), SegmentSelectWithoutSubmittedDOI);

            // Verify all previously submitted DOIs
            if (configParms.ValidateSubmissions) this.VerifySubmittedDOIs();

            // Report the results of pdf generation
            this.ProcessResults();

            this.LogMessage("BHLDOIService Processing Complete");
        }

        #region Process Entities Without DOIs

        private void ProcessEntitiesWithoutDOIs(int entityTypeId, 
            global::BHL.WebServiceREST.v1.Client.IRestClient entitySelectClient, 
            EntitySelectWithoutSubmittedDOI entitySelect)
        {
            this.LogMessage(string.Format("Processing {0}s", GetEntityTypeName(entityTypeId)));

            DoiClient restClient = null;

            try
            {
                restClient = new DoiClient(configParms.BHLWSRestEndpoint);

                // Get IDs of entities without a DOI
                DOI[] doisToSubmit = entitySelect(entitySelectClient, configParms.NumberToSubmit);
                this.LogMessage(string.Format("Found {0} {1}s to process", doisToSubmit.Count().ToString(), GetEntityTypeName(entityTypeId)));
                foreach (DOI doi in doisToSubmit)
                {
                    DOIDepositData depositData = this.GetDepositData(entityTypeId, (int)doi.EntityID, doi.DoiName);

                    ExistingDOICheckResult result = ExistingDOICheck(depositData, (int)doi.DoiStatusID);

                    switch (result.ResultValue)
                    {
                        case DOICheckResult.NotFound:
                        case DOICheckResult.Update:

                            // Add the DOI name, if one has not already been assigned
                            if (string.IsNullOrWhiteSpace(doi.DoiName))
                            {
                                string doiName = this.GenerateDOIName(configParms.DoiPrefix, entityTypeId, (int)doi.EntityID);
                                restClient.UpdateDoiName((int)doi.Doiid,
                                    new DoiModel
                                    {
                                        Doistatusid = doi.DoiStatusID,
                                        Doiname = doiName,
                                        Userid = null
                                    });
                                doi.DoiName = doiName;
                            }

                            // Generate a batch identifier for this DOI
                            string doiBatchID = this.GenerateDOIBatchID((int)doi.Doiid);
                            restClient.UpdateDoiBatchID((int)doi.Doiid,
                                new DoiModel
                                {
                                    Doistatusid = doi.DoiStatusID,
                                    Doibatchid = doiBatchID,
                                    Userid = null
                                });
                            doi.DoiBatchID = doiBatchID;

                            // Create a CrossRef deposit record for this DOI
                            depositData.BatchID = doi.DoiBatchID;
                            depositData.DoiName = doi.DoiName;
                            string depositTemplate = this.GetDepositTemplate(depositData);
                            string doiDeposit = this.GenerateDOIDepositRecord(depositData, depositTemplate);
                            File.WriteAllText(configParms.DepositFolder + string.Format(configParms.DepositFileFormat, doiBatchID), doiDeposit);

                            try
                            {
                                // Submit the new DOI to CrossRef and update the DOI status to "Submitted"
                                this.SubmitDOI(doiDeposit, string.Format(configParms.DepositFileFormat, doiBatchID));
                                restClient.UpdateDoiStatus((int)doi.Doiid,
                                    new DoiModel
                                    {
                                        Doistatusid = configParms.DoiStatusSubmitted,
                                        Message = String.Empty,
                                        Isvalid = null,
                                        Userid = null
                                    });
                                if (depositData.IsUpdate)
                                    submittedDOIUpdates.Add(doi.DoiName);
                                else
                                    submittedDOIAdds.Add(doi.DoiName);
                            }
                            catch (Exception ex)
                            {
                                // Set DOI error status and record the error
                                log.Error("Exception submitting DOI " + doi.DoiName, ex);
                                errorMessages.Add("Exception submitting DOI " + doi.DoiName + ": " + ex.Message);
                                restClient.UpdateDoiStatus((int)doi.Doiid,
                                    new DoiModel
                                    {
                                        Doistatusid = configParms.DoiStatusError,
                                        Message = "ERROR (SUBMIT): " + ex.Message,
                                        Isvalid = null,
                                        Userid = null
                                    });
                            }

                            this.LogMessage("DOI " + doi.DoiName + " processed");

                            break;
                        case DOICheckResult.Found:
                            // Add the external DOI to the database
                            restClient.AddDoi(new DoiModel
                            {
                                Entitytypeid = entityTypeId,
                                Entityid = doi.EntityID,
                                Doistatusid = configParms.DoiStatusExternal,
                                Doiname = result.DoiList.First(),
                                Isvalid = 1,
                                Doibatchid = string.Empty,
                                Message = string.Empty,
                                Userid = 1,
                                Excludebhldoi = 1
                            });
                            foundDOIs.Add(result.DoiList.First());

                            this.LogMessage("DOI " + result.DoiList.First() + " added");

                            break;
                        case DOICheckResult.Unknown:
                            // Log the unresolvable entities.  The appropriate external DOI will need to be
                            // manually assigned.
                            // Once we determine how commonly these occur, a more robust logging mechanism
                            // may be needed.
                            unresolvedEntities.Add(string.Format("{0}\t{1}", entityTypeId.ToString(), doi.EntityID.ToString()));

                            string unresolvableMessage = string.Format("Entity {0}, Type {1} cound not be resolved", doi.EntityID.ToString(), doi.DoiEntityTypeID.ToString());
                            this.LogMessage(unresolvableMessage);

                            foreach (string doiName in result.DoiList)
                            {
                                string possibleDOIMessage = string.Format("\r\nEntity {0}, Type {1} - possible DOI: {2}", doi.EntityID.ToString(), doi.DoiEntityTypeID.ToString(), doiName);
                                unresolvableMessage += possibleDOIMessage;
                                this.LogMessage(possibleDOIMessage);
                            }

                            // Set queue item status to Error, since it cannot be resolved
                            restClient.UpdateDoiStatus((int)doi.Doiid,
                                new DoiModel
                                {
                                    Doistatusid = configParms.DoiStatusError,
                                    Message = "UNRESOLVABLE: " + unresolvableMessage,
                                    Isvalid = null,
                                    Userid = null
                                });

                            break;
                        case DOICheckResult.Error:
                            // Report any error messages returned from CrossRef.  Otherwise, do nothing.
                            // Will get picked up and re-tried the next time this process is run.
                            if (!string.IsNullOrWhiteSpace(result.Message))
                            {
                                string logMessage = string.Format("Error processing {0} {1}: {2}",
                                    this.GetEntityTypeName(entityTypeId), doi.EntityID, result.Message);
                                log.Error(logMessage);
                                errorMessages.Add(logMessage);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Exception processing {0}s", GetEntityTypeName(entityTypeId)), ex);
                errorMessages.Add(string.Format("Exception processing {0}s: {1}", GetEntityTypeName(entityTypeId), ex.Message));
            }

            this.LogMessage(string.Format("Done processing {0}s", GetEntityTypeName(entityTypeId)));
        }

        /// <summary>
        /// Delegates for the methods that select entities to be assigned DOIs
        /// </summary>
        /// <param name="client"></param>
        /// <param name="numToSubmit"></param>
        /// <returns></returns>
        public delegate DOI[] EntitySelectWithoutSubmittedDOI(global::BHL.WebServiceREST.v1.Client.IRestClient client, int numToSubmit);

        public DOI[] TitleSelectWithoutSubmittedDOI(global::BHL.WebServiceREST.v1.Client.IRestClient client, int numToSubmit)
        {
            return ((TitlesClient)client).GetTitleWithoutDois(numToSubmit).ToArray<DOI>();
        }

        public DOI[] SegmentSelectWithoutSubmittedDOI(global::BHL.WebServiceREST.v1.Client.IRestClient client, int numToSubmit)
        {
            return ((SegmentsClient)client).GetSegmentWithoutDois(numToSubmit).ToArray<DOI>();
        }

        /// <summary>
        /// Query CrossRef for an existing DOI for the specified entity being considered for deposit.
        /// Both monograph and article queries must be submitted, due to how CrossRef maintains their
        /// data (some articles may be stored as "reports", which are treated as "monographs" by their
        /// APIs).
        /// </summary>
        /// <param name="depositData"></param>
        /// <param name="doiStatusId"></param>
        /// <returns></returns>
        private ExistingDOICheckResult ExistingDOICheck(DOIDepositData depositData, int doiStatusId)
        {
            ExistingDOICheckResult result = new ExistingDOICheckResult();

            if (depositData.IsUpdate)
            {
                // Existing DOI is being updated, so no need to submit a query to Crossref
                result.ResultValue = DOICheckResult.Update;
            }
            else
            {
                // Submit queries to CrossRef to see if a DOI exists.
                try
                {
                    // Generate a batch identifier
                    depositData.BatchID = this.GenerateDOIBatchID(depositData.EntityID);

                    // Prepare the first (monograph/journal) query
                    string queryTemplate = this.GetQueryTemplate(depositData);
                    string doiQuery;
                    if (depositData.PublicationType == DOIDepositData.PublicationTypeValue.Journal)
                        doiQuery = this.GenerateDOIQuery(DOIDepositData.PublicationTypeValue.Journal, depositData, queryTemplate);
                    else
                        doiQuery = this.GenerateDOIQuery(DOIDepositData.PublicationTypeValue.Monograph, depositData, queryTemplate);

                    // Send the query
                    string queryResponse = this.SubmitQuery(doiQuery);

                    // Process the response
                    result = this.ProcessQueryResult(queryResponse, result);

                    // Only submit an Article query if no error has occurred, the data
                    // required for an article query is available, and the original
                    // query was not looking for a journal
                    if (result.ResultValue != DOICheckResult.Error && 
                        depositData.Issn.Count > 0 &&
                        depositData.PublicationType != DOIDepositData.PublicationTypeValue.Journal)
                    {
                        // Prepare, send, and process the second query
                        queryTemplate = this.GetQueryTemplate(depositData);
                        doiQuery = this.GenerateDOIQuery(DOIDepositData.PublicationTypeValue.Article, depositData, queryTemplate);
                        queryResponse = this.SubmitQuery(doiQuery);
                        result = this.ProcessQueryResult(queryResponse, result);
                    }
                }
                catch(Exception ex)
                {
                    // Set DOI error status and record the error
                    log.Error("Exception resolving Entity " + depositData.EntityID.ToString() + ", Type " + depositData.EntityTypeID.ToString(), ex);
                    errorMessages.Add("Exception resolving Entity " + depositData.EntityID.ToString() + ", Type " + depositData.EntityTypeID.ToString() + ": " + ex.Message);
                    result.ResultValue = DOICheckResult.Error;
                }
            }

            return result;
        }

        /// <summary>
        /// Class containing values set and returned by the ExistingDOICheck function
        /// </summary>
        private class ExistingDOICheckResult
        {
            public DOICheckResult ResultValue = DOICheckResult.Unknown;
            public List<string> DoiList = new List<string>();
            public string Message = string.Empty;
        }

        /// <summary>
        /// Enumeration of possible results of the ExistingDOICheck function.
        /// Found = one matching DOI was found
        /// NotFound = no matching DOIs were found
        /// Unknown = more than one match was found; not sure if any or none are "real" matches
        /// Error = an error occured while checking for existing DOIs
        /// </summary>
        private enum DOICheckResult
        {
            Found,
            NotFound,
            Update,
            Unknown,
            Error
        }

        /// <summary>
        /// Generate a DOI
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private string GenerateDOIName(string prefix, int entityTypeId, int entityID)
        {
            string doiName = string.Empty;
            string entityTypeAbbreviation = GetEntityTypeAbbreviation(entityTypeId);
            doiName = string.Format(configParms.DoiFormat, prefix, entityTypeAbbreviation, entityID.ToString());
            return doiName;
        }

        /// <summary>
        /// Get the name of the entity type represented by the type identifier
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private string GetEntityTypeName(int entityTypeId)
        {
            string entityTypeName = string.Empty;

            if (entityTypeId == configParms.DoiEntityTypeTitle) entityTypeName = "title";
            if (entityTypeId == configParms.DoiEntityTypeItem) entityTypeName = "item";
            if (entityTypeId == configParms.DoiEntityTypePage) entityTypeName = "page";
            if (entityTypeId == configParms.DoiEntityTypeSegment) entityTypeName = "part";

            return entityTypeName;
        }

        /// <summary>
        /// Get the abbreviation for the entity type represented by the type identifier.  Abbreviation is used in the DOI name.
        /// </summary>
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        private string GetEntityTypeAbbreviation(int entityTypeId)
        {
            string entityTypeAbbreviation = string.Empty;

            if (entityTypeId == configParms.DoiEntityTypeTitle) entityTypeAbbreviation = "t";
            if (entityTypeId == configParms.DoiEntityTypeItem) entityTypeAbbreviation = "i";
            if (entityTypeId == configParms.DoiEntityTypePage) entityTypeAbbreviation = "pg";
            if (entityTypeId == configParms.DoiEntityTypeSegment) entityTypeAbbreviation = "p";

            return entityTypeAbbreviation;
        }

        /// <summary>
        /// Generate a batch ID to include in the deposit record sent to CrossRef for the specified DOI
        /// </summary>
        /// <param name="doiID"></param>
        /// <returns></returns>
        private string GenerateDOIBatchID(int doiID)
        {
            return String.Format("{0}.bhl.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), doiID.ToString());
        }

        /// <summary>
        /// Populate a DOIDepositData object with data about the specified entity.
        /// </summary>
        /// <param name="doiEntityType"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        private DOIDepositData GetDepositData(int doiEntityType, int entityID, string doiName)
        {
            DOIDepositData data = new DOIDepositData();

            if (doiEntityType == configParms.DoiEntityTypeTitle)
            {
                data = GetBookDepositData(entityID);
            }
            else if (doiEntityType == configParms.DoiEntityTypeSegment)
            {
                data = GetSegmentDepositData(entityID);
            }
            else
            {
                // Only titles and articles have been implemented
                throw new NotImplementedException();
            }

            data.IsUpdate = !string.IsNullOrWhiteSpace(doiName);
            data.EntityID = entityID;
            data.EntityTypeID = doiEntityType;
            data.DepositorEmail = configParms.CrossrefDepositorEmail;
            data.DepositorName = configParms.CrossrefDepositorName;
            data.Registrant = configParms.CrossrefRegistrantName;

            return data;
        }

        /// <summary>
        /// Accumulate the data for a book/journal deposit
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        private DOIDepositData GetBookDepositData(int entityID)
        {
            TitlesClient restClient = new TitlesClient(configParms.BHLWSRestEndpoint);
            Title title = restClient.GetTitleDetails(entityID);

            DOIDepositData data = new DOIDepositData();
            data.Title = title.FullTitle;
            data.PublisherName = title.Datafield_260_b;
            data.PublisherPlace = title.Datafield_260_a;
            data.PublicationDate = (title.StartYear == null ? "" : title.StartYear.ToString());
            // data.Language = title.LanguageCode;      // Need to translate to ISO 639 language codes
            // data.Edition = title.EditionStatement;   // Should only contain a number; our edition data is too messy
            data.DoiResource = string.Format(configParms.BhlTitleUrlFormat, entityID.ToString());

            foreach (TitleVariant titleVariant in title.TitleVariants)
            {
                if (titleVariant.TitleVariantTypeID == configParms.TitleVariantAbbreviated) data.AbbreviatedTitle = titleVariant.Title;
            }

            foreach (Title_Identifier titleIdentifier in title.TitleIdentifiers)
            {
                if (string.Compare(titleIdentifier.IdentifierName, "ISBN", true, CultureInfo.CurrentCulture) == 0) data.Isbn = titleIdentifier.IdentifierValue;
                if (string.Compare(titleIdentifier.IdentifierName, "ISSN", true, CultureInfo.CurrentCulture) == 0) data.Issn.Add(("print", titleIdentifier.IdentifierValue));
                if (string.Compare(titleIdentifier.IdentifierName, "eISSN", true, CultureInfo.CurrentCulture) == 0) data.Issn.Add(("electronic", titleIdentifier.IdentifierValue));
                if (string.Compare(titleIdentifier.IdentifierName, "CODEN", true, CultureInfo.CurrentCulture) == 0) data.Coden = titleIdentifier.IdentifierValue;
                if (string.Compare(titleIdentifier.IdentifierName, "Abbreviation", true, CultureInfo.CurrentCulture) == 0 && 
                    data.AbbreviatedTitle == string.Empty) data.AbbreviatedTitle = titleIdentifier.IdentifierValue;
            }

            foreach (TitleAuthor titleAuthor in title.TitleAuthors)
            {
                DOIDepositData.Contributor contributor = new DOIDepositData.Contributor();

                if (titleAuthor.AuthorRoleID == configParms.AuthorRole100 ||
                    titleAuthor.AuthorRoleID == configParms.AuthorRole700)
                {
                    contributor.PersonName = titleAuthor.FullName.TrimEnd(',');
                    contributor.Suffix = titleAuthor.Numeration;
                    contributor.Sequence = (titleAuthor.AuthorRoleID == configParms.AuthorRole100 ?
                                            DOIDepositData.PersonNameSequence.First :
                                            DOIDepositData.PersonNameSequence.Additional);
                }
                else
                {
                    contributor.OrganizationName = titleAuthor.FullName.TrimEnd(',');
                    contributor.Sequence = (titleAuthor.AuthorRoleID == configParms.AuthorRole110 ||
                                            titleAuthor.AuthorRoleID == configParms.AuthorRole111 ?
                                            DOIDepositData.PersonNameSequence.First :
                                            DOIDepositData.PersonNameSequence.Additional);
                }

                foreach (AuthorIdentifier authorIdentifier in titleAuthor.Author.AuthorIdentifiers)
                {
                    if (authorIdentifier.IdentifierName == "ORCID") { contributor.ORCID = authorIdentifier.IdentifierValue; break; }
                }

                contributor.Role = DOIDepositData.ContributorRole.Author;

                data.Contributors.Add(contributor);
            }

            // Set the PublicationType
            if (title.BibliographicLevelID == configParms.BibLevelMonographComponent ||
                title.BibliographicLevelID == configParms.BibLevelMonograph)
            {
                SeriesMetadata seriesMetadata = new SeriesMetadata();
                if (configParms.CheckForMonoSeries) seriesMetadata = ValidateMonographicSeries(title);
                if (seriesMetadata.IsMonographicSeries) // always false if check for mono series is not performed
                {
                    data.PublicationType = DOIDepositData.PublicationTypeValue.MonographicSeries;
                    data.SeriesTitle = seriesMetadata.Title;
                    data.SeriesISSN = seriesMetadata.ISSN;
                    data.SeriesVolume = seriesMetadata.Volume;
                }
                else
                {
                    data.PublicationType = DOIDepositData.PublicationTypeValue.Monograph;
                }
            }
            else if (title.BibliographicLevelID == configParms.BibLevelSerial ||
                title.BibliographicLevelID == configParms.BibLevelSerialComponent)
            {
                data.PublicationType = DOIDepositData.PublicationTypeValue.Journal;
            }
            else
            {
                data.PublicationType = DOIDepositData.PublicationTypeValue.EditedBook;
            }

            return data;
        }

        /// <summary>
        /// Checks to see if the specified Title has a single TitleAssociation that meets all of the requirements of a monographic series
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private SeriesMetadata ValidateMonographicSeries(Title title)
        {
            SeriesMetadata seriesMetadata = new SeriesMetadata();
            int seriesCount = 0;
            TitlesClient restClient = new TitlesClient(configParms.BHLWSRestEndpoint);

            foreach (TitleAssociation ta in title.TitleAssociations)
            {
                // Only consider associated titles with MARC tags 440, 490, and 830
                if ((ta.MarcTag == "440" || ta.MarcTag == "490" || ta.MarcTag == "830") && (ta.Active ?? true))
                {
                    // Make sure the asssociated title is linked to another BHL title
                    if (ta.AssociatedTitleID != null)
                    {
                        Title associatedTitle = restClient.GetTitleDetails((int)ta.AssociatedTitleID);

                        // The associated title must be a serial
                        if (associatedTitle.BibliographicLevelID == configParms.BibLevelSerial ||
                            associatedTitle.BibliographicLevelID == configParms.BibLevelSerialComponent)
                        {
                            bool firstIssnForAssociation = true;
                            foreach(Title_Identifier ti in associatedTitle.TitleIdentifiers)
                            {
                                // The associated title must have an ISSN
                                if (string.Compare(ti.IdentifierName, "ISSN", CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0 ||
                                    string.Compare(ti.IdentifierName, "eISSN", CultureInfo.CurrentCulture,  CompareOptions.IgnoreCase) == 0)
                                {
                                    // An associated series was found with all necessary metadata

                                    // Make sure the series is only counted once, even if more than one ISSN exists
                                    if (firstIssnForAssociation) seriesCount++; 
                                    firstIssnForAssociation = false;

                                    // Save the important details about this series
                                    // If a volume exists, use it.  Else, use number if not also a part.  Finally, use part if no number.
                                    Utility.VolumeData vol = Utility.DataCleaner.ParseVolumeString(ta.Volume);
                                    if (!string.IsNullOrWhiteSpace(vol.StartVolume)) seriesMetadata.Volume = vol.StartVolume;
                                    else if (!string.IsNullOrWhiteSpace(vol.StartNumber) && string.IsNullOrWhiteSpace(vol.StartPart)) seriesMetadata.Volume = vol.StartNumber;
                                    else if (string.IsNullOrWhiteSpace(vol.StartNumber) && !string.IsNullOrWhiteSpace(vol.StartPart)) seriesMetadata.Volume = vol.StartPart;
                                    seriesMetadata.Title = associatedTitle.FullTitle;
                                    seriesMetadata.ISSN = ti.IdentifierValue;

                                    if (string.Compare(ti.IdentifierName, "ISSN", CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0) break;
                                }
                            }
                        }
                    }
                }
            }

            // If exactly one series was identified, then the title is part of a Monographic Series
            seriesMetadata.IsMonographicSeries = (seriesCount == 1);

            return seriesMetadata;
        }

        /// <summary>
        /// Contains the details of the series associated with a Monographic Series title.
        /// </summary>
        private class SeriesMetadata
        {
            public bool IsMonographicSeries { get; set; } = false;
            public string Volume { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string ISSN { get; set; } = string.Empty;
        }

        /// <summary>
        /// Accumulate the data for a segment/article deposit
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        private DOIDepositData GetSegmentDepositData(int entityID)
        {
            DOIDepositData data = new DOIDepositData();
            SegmentsClient segmentRestClient = null;
            TitlesClient titlesRestClient = null;

            segmentRestClient = new SegmentsClient(configParms.BHLWSRestEndpoint);
            titlesRestClient = new TitlesClient(configParms.BHLWSRestEndpoint);

            Segment segment = segmentRestClient.GetSegmentDetails(entityID);

            data.DoiResource = string.Format(configParms.BhlPartUrlFormat, entityID.ToString());
            data.PublicationType = DOIDepositData.PublicationTypeValue.Article;
            data.ArticleTitle = segment.Title;
            data.ArticlePublicationDate = segment.Date;
            data.PublisherName = segment.PublisherName;
            data.PublisherPlace = segment.PublisherPlace;
            data.PublicationDate = segment.Date;  // this.GetPublicationDate(segment);
            data.FirstPage = segment.StartPageNumber;
            data.LastPage = segment.EndPageNumber;

            data.Title = segment.TitleFullTitle;
            data.Volume = string.IsNullOrWhiteSpace(segment.Volume) ? segment.ItemVolume : segment.Volume;
            data.Issue = segment.Issue;

            Title_Identifier[] titleIdentifierList = titlesRestClient.GetTitleIdentifiers(segment.TitleId ?? 0).ToArray<Title_Identifier>();
            foreach (Title_Identifier titleIdentifier in titleIdentifierList)
            {
                if (string.Compare(titleIdentifier.IdentifierName, "ISSN", true, CultureInfo.CurrentCulture) == 0) data.Issn.Add(("print", titleIdentifier.IdentifierValue));
                if (string.Compare(titleIdentifier.IdentifierName, "eISSN", true, CultureInfo.CurrentCulture) == 0) data.Issn.Add(("electronic", titleIdentifier.IdentifierValue));
            }

            // If no ISSNs, use the DOI of the associated title
            if (data.Issn.Count == 0)
            {
                Title_Identifier[] titleDOIs = titlesRestClient.GetTitleDois(segment.TitleId ?? 0).ToArray<Title_Identifier>();
                foreach(Title_Identifier doi in titleDOIs)
                {
                    data.TitleDOIName = doi.IdentifierValue;
                    data.TitleDOIResource = string.Format(configParms.BhlTitleUrlFormat, segment.TitleId);
                    break;
                }
            }

            bool first = true;
            foreach (ItemAuthor itemAuthor in segment.AuthorList)
            {
                DOIDepositData.Contributor contributor = new DOIDepositData.Contributor();

                if (itemAuthor.Author.AuthorTypeID == configParms.AuthorTypePerson)
                {
                    contributor.PersonName = itemAuthor.FullName.TrimEnd(',');
                    contributor.Suffix = itemAuthor.Numeration;
                }
                else
                {
                    contributor.OrganizationName = itemAuthor.FullName.TrimEnd(',');
                }

                foreach(AuthorIdentifier authorIdentifier in itemAuthor.Author.AuthorIdentifiers)
                {
                    if (authorIdentifier.IdentifierName == "ORCID") { contributor.ORCID = authorIdentifier.IdentifierValue; break; }
                }

                contributor.Role = DOIDepositData.ContributorRole.Author;
                contributor.Sequence = (first ?
                                        DOIDepositData.PersonNameSequence.First :
                                        DOIDepositData.PersonNameSequence.Additional);

                first = false;

                data.Contributors.Add(contributor);
            }                

            return data;
        }

        /// <summary>
        /// Return the four-digit year publication date (Item.Year or Title.StartDate) for the segment.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private string GetPublicationDate(Segment segment)
        {
            string publicationDate = segment.TitlePublicationDate;

            // Get item year, without brackets
            string itemYear = segment.ItemYear.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "");

            // Make sure the item year value has at least four characters
            if (itemYear.Length >= 4)
            { 
                double year = 0;
                itemYear = itemYear.Substring(0, 4);

                // Make sure the value is numeric
                if (Double.TryParse(itemYear, out year))
                {
                    // Make sure the value is between 1400 and the current year
                    if (year >= 1400 && year <= DateTime.Now.Year + 1) publicationDate = itemYear;
                }
            }

            return publicationDate;
        }

        /// <summary>
        /// Examine the deposit data and return the appropriate template
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetDepositTemplate(DOIDepositData data)
        {
            string depositTemplate = string.Empty;
            if (data.PublicationType == DOIDepositData.PublicationTypeValue.Monograph ||
                data.PublicationType == DOIDepositData.PublicationTypeValue.MonographicSeries)
                depositTemplate = File.ReadAllText(configParms.MonographDepositTemplateFile);
            else if (data.PublicationType == DOIDepositData.PublicationTypeValue.Article)
                depositTemplate = File.ReadAllText(configParms.ArticleDepositTemplateFile);
            else
                depositTemplate = File.ReadAllText(configParms.JournalDepositTemplateFile);

            return depositTemplate;
        }

        /// <summary>
        /// Examine the deposit data and return the approriate query template
        /// </summary>
        /// <returns></returns>
        private string GetQueryTemplate(DOIDepositData data)
        {
            string queryTemplate = string.Empty;
            queryTemplate = File.ReadAllText(configParms.QueryTemplateFile);
            return queryTemplate;
        }

        /// <summary>
        /// Generate a deposit record from the specified deposit data and template
        /// </summary>
        /// <param name="data"></param>
        /// <param name="depositTemplate"></param>
        /// <returns></returns>
        private string GenerateDOIDepositRecord(DOIDepositData data, string depositTemplate)
        {
            DOIDepositFactory depositFactory = new DOIDepositFactory(data);
            DOIDeposit.DOIDeposit deposit;

            if (data.PublicationType == DOIDepositData.PublicationTypeValue.Monograph ||
                data.PublicationType == DOIDepositData.PublicationTypeValue.MonographicSeries)
                deposit = depositFactory.GetDOIDeposit(DOIDepositFactory.DOIDepositType.Monograph);
            else if (data.PublicationType == DOIDepositData.PublicationTypeValue.Article)
                deposit = depositFactory.GetDOIDeposit(DOIDepositFactory.DOIDepositType.Article);
            else
                deposit = depositFactory.GetDOIDeposit(DOIDepositFactory.DOIDepositType.Journal);

            return deposit.ToString(depositTemplate);
        }

        /// <summary>
        /// Generate a DOI query of the specified type from the specified deposit data and template
        /// </summary>
        /// <param name="publicationType"></param>
        /// <param name="data"></param>
        /// <param name="queryTemplate"></param>
        /// <returns></returns>
        private string GenerateDOIQuery(DOIDepositData.PublicationTypeValue queryType, DOIDepositData data, 
            string queryTemplate)
        {
            DOIXmlQueryFactory xmlQueryFactory = new DOIXmlQueryFactory(data);
            DOIDeposit.DOIQuery query;

            if (queryType == DOIDepositData.PublicationTypeValue.Monograph ||
                queryType == DOIDepositData.PublicationTypeValue.MonographicSeries)
                query = xmlQueryFactory.GetDOIQuery(DOIXmlQueryFactory.DOIQueryType.Monograph);
            else if (queryType == DOIDepositData.PublicationTypeValue.Journal)
                query = xmlQueryFactory.GetDOIQuery(DOIXmlQueryFactory.DOIQueryType.Journal);
            else
                query = xmlQueryFactory.GetDOIQuery(DOIXmlQueryFactory.DOIQueryType.Article);

            return query.ToString(queryTemplate);
        }

        /// <summary>
        /// Use the RESTSharp library to submit the CrossRef deposit file
        /// </summary>
        /// <remarks>
        /// See http://dkdevelopment.net/2010/05/25/dropbox-api-restsharp-and-c-part-2-the-revenge/
        /// for an example of using RestSharp to perform a file upload.
        /// </remarks>
        /// <param name="deposit"></param>
        private void SubmitDOI(string deposit, string filename)
        {
            // Set up the REST client
            // NOTE:  The Url Base and Url Query are separated here because RestSharp puts a
            // trailing slash at the end of the base URL of POST operations.  Therefore, if we 
            // included the Url Query in the Url used to set up the RestClient object, the slash
            // would be placed at the end of the querystring.  By splitting the Base and Query, 
            // we "trick" RestSharp into putting the slash between the Base and Query (which is
            // where a slash should appear).
            RestSharp.RestClient restClient = new RestSharp.RestClient(configParms.CrossrefDepositUrlBase);
            RestRequest request = new RestRequest(
                String.Format(configParms.CrossrefDepositUrlQueryFormat,
                configParms.CrossrefLogin, configParms.CrossrefPassword, configParms.CrossrefDepositArea), 
                Method.Post);

            // Convert the deposit into a byte array and add it to the request
            byte[] bytes = new UTF8Encoding().GetBytes(deposit);
            request.AddFile("fname", bytes, filename, "text/xml");

            // Perform the POST operation
            //RestResponse response = restClient.ExecuteAsync(request);
            RestResponse response = System.Threading.Tasks.Task.Run(async () => await restClient.ExecuteAsync(request)).GetAwaiter().GetResult();

            // Check the result of the POST operation.
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.ErrorMessage);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.StatusDescription);
            }
            if (response.Content.Contains("FAILURE"))
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.Content);
            }
        }

        /// <summary>
        /// Submit the query to CrossRef and return the response
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string SubmitQuery(string query)
        {
            string response = string.Empty;

            // Set up the REST client
            RestSharp.RestClient restClient = new RestSharp.RestClient(configParms.CrossrefXmlQueryUrlBase);
            RestRequest request = new RestRequest(
                String.Format(configParms.CrossrefXmlQueryFormat,
                configParms.CrossrefLogin, configParms.CrossrefPassword, query),
                Method.Get);

            // Submit the query
            //RestResponse restResponse = restClient.Execute(request);
            RestResponse restResponse = System.Threading.Tasks.Task.Run(async () => await restClient.ExecuteAsync(request)).GetAwaiter().GetResult();

            // Check the query result
            if (restResponse.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error querying for DOI: " + 
                    (restResponse.ErrorMessage == null 
                        ? restResponse.ErrorException == null ? "Unknown Error" : (restResponse.ErrorException.Message ?? "Unknown Error")
                        : restResponse.ErrorMessage));
            }
            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Error querying for DOI: {0}\n\r{1}", 
                    restResponse.StatusDescription, restResponse.Content));
            }
            if (restResponse.Content.Contains("FAILURE"))
            {
                throw new Exception("Error querying for DOI:" + restResponse.Content);
            }

            return restResponse.Content;
        }

        /// <summary>
        /// Check the status of the response and read any existing DOIs from the response
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private ExistingDOICheckResult ProcessQueryResult(string queryResult, ExistingDOICheckResult result)
        {
            /*
            Query results look something like the following:

            <?xml version="1.0" encoding="UTF-8"?>
            <crossref_result xmlns="http://www.crossref.org/qrschema/3.0" version="3.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.crossref.org/qrschema/3.0 http://www.crossref.org/schema/queryResultSchema/crossref_query_output3.0.xsd">
              <query_result>
                <head>
                  <email_address>mike.lichtenberg@mobot.org</email_address>
                  <doi_batch_id>20150626021404.bhl.0</doi_batch_id>
                </head>
                <body>
                  <query status="resolved" query_mode="metadata">
                    <doi type="journal_article">10.4039/Ent13220-11</doi>
                    <crm-item name="publisher-name" type="string">Cambridge University Press (CUP)</crm-item>
                    <crm-item name="prefix-name" type="string">Cambridge University Press (Entomological Society of Canada)</crm-item>
                    <crm-item name="member-id" type="number">56</crm-item>
                    <crm-item name="citation-id" type="number">41085485</crm-item>
                    <crm-item name="journal-id" type="number">81081</crm-item>
                    <crm-item name="deposit-timestamp" type="number">201104240144220739</crm-item>
                    <crm-item name="owner-prefix" type="string">10.4039</crm-item>
                    <crm-item name="last-update" type="date">2011-06-28 21:14:23.0</crm-item>
                    <crm-item name="citedby-count" type="number">5</crm-item>
                    <doi_record>
                      ...
                      [ DETAILED METADATA HERE ]
                      ...
                    </doi_record>
                  </query>
                </body>
              </query_result>
            </crossref_result>

            <?xml version="1.0" encoding="UTF-8"?>
            <crossref_result xmlns="http://www.crossref.org/qrschema/3.0" version="3.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.crossref.org/qrschema/3.0 http://www.crossref.org/schema/queryResultSchema/crossref_query_output3.0.xsd">
              <query_result>
                <head>
                  <email_address>mike.lichtenberg@mobot.org</email_address>
                  <doi_batch_id>20150626023143.bhl.0</doi_batch_id>
                </head>
                <body>
                  <query key="" status="unresolved" fl_count="0">
                    <article_title>Sur quelques Gobius mediterraneens (G. kneri, Stndr., G. elongatus Canestr., G. niger L.)</article_title>
                    <first_page>164</first_page>
                    <issn>0037962X</issn>
                    <journal_title>Bulletin de la Société zoologique de France</journal_title>
                    <volume>40</volume>
                    <year>1915</year>
                  </query>
                </body>
              </query_result>
            </crossref_result>
            */

            try
            {
                XDocument xmlResult = XDocument.Parse(queryResult);
                XNamespace ns = "http://www.crossref.org/qrschema/3.0";

                XElement firstQueryElement = xmlResult.
                    Root.
                    Element(ns + "query_result").
                    Element(ns + "body").
                    Element(ns + "query");

                // Read the status of the query response
                string status = firstQueryElement.Attribute("status").Value;
                switch (status)
                {
                    case "resolved":
                    case "unresolved":
                    case "multiresolved":
                        // ResultValue will be assigned after DOIs are collected from the query result
                        break;
                    case "malformed":
                        result.ResultValue = DOICheckResult.Error;
                        break;
                    default:
                        throw new Exception("Error response received in query result: " + status);
                }

                // Read the DOIs from the query response
                IEnumerable<XElement> queryElements = xmlResult.
                    Root.
                    Element(ns + "query_result").
                    Element(ns + "body").
                    Elements(ns + "query");

                foreach(XElement queryElement in queryElements)
                {
                    if (result.ResultValue == DOICheckResult.Error)
                    {
                        XElement msgElement = queryElement.Element(ns + "msg");
                        if (msgElement != null)
                        {
                            if (!string.IsNullOrWhiteSpace(result.Message)) result.Message += "\n";
                            result.Message = "Query result - " + msgElement.Value;
                            break;
                        }
                    }
                    else
                    {
                        XElement doiElement = queryElement.Element(ns + "doi");
                        if (doiElement != null)
                        {
                            string doiValue = doiElement.Value;
                            // Do not include any BHL-owned DOIs found by the query.  
                            // BHL always assigns new DOIs to unique digital entities.
                            if (!doiValue.StartsWith(configParms.DoiPrefix)) result.DoiList.Add(doiElement.Value);
                        }
                    }
                }

                // If no errors, set the ResultValue based on the number of non-BHL-owned DOIs
                if (result.ResultValue != DOICheckResult.Error)
                {
                    switch (result.DoiList.Count)
                    {
                        case 0: result.ResultValue = DOICheckResult.NotFound; break;
                        case 1: result.ResultValue = DOICheckResult.Found; break;
                        default: result.ResultValue = DOICheckResult.Unknown; break;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add("Error processing query result: " + ex.Message);
                LogMessage("Error processing query result: " + ex.Message);
                throw;
            }

            return result;
        }

        #endregion Process Entities Without DOIs

        #region Verify Submitted DOIs

        private void VerifySubmittedDOIs()
        {
            this.LogMessage("Verifying Submitted DOIs");

            DoiClient doiClient = new DoiClient(configParms.BHLWSRestEndpoint);

            try
            {
                // Check the CrossRef status of submitted DOIs that have not yet been verified
                DOI[] submittedDOIs = doiClient.GetSubmittedDois(configParms.MinMinutesSinceSubmit).ToArray<DOI>();
                this.LogMessage("Found " + submittedDOIs.Count().ToString() + " Submitted DOIs To Verify");
                foreach (DOI doi in submittedDOIs)
                {
                    XDocument submitLog = null;

                    // Get the submission log from CrossRef
                    bool gotSubmissionLog = false;
                    try
                    {
                        submitLog = this.GetSubmissionLog(doi.DoiBatchID, string.Format(configParms.DepositFileFormat, doi.DoiBatchID));

                        // Write the submission log to the file system
                        File.WriteAllText(
                            configParms.SubmitLogFolder + string.Format(configParms.SubmitLogFileFormat, doi.DoiBatchID),
                            submitLog.ToString());

                        gotSubmissionLog = true;
                    }
                    catch (Exception ex)
                    {
                        // Set DOI error status and record the error
                        log.Error("Exception getting submission log for batch " + doi.DoiBatchID, ex);
                        errorMessages.Add("Exception getting submission log for batch " + doi.DoiBatchID + ":" + ex.Message);
                        doiClient.UpdateDoiStatus((int)doi.Doiid,
                            new DoiModel
                            {
                                Doistatusid = configParms.DoiStatusError,
                                Message = "ERROR (SUBMIT LOG): " + ex.Message,
                                Isvalid = null,
                                Userid = null
                            });
                    }

                    // Parse the submission log to determine current status of DOI
                    if (gotSubmissionLog)
                    {
                        try
                        {
                            XAttribute batchStatusAttrib = submitLog.Root.Attribute("status");

                            // Make sure the batch processing is complete
                            if (batchStatusAttrib.Value == "completed")
                            {
                                XElement recordDiagnostic = null;
                                foreach(XElement rd in submitLog.Root.Elements("record_diagnostic"))
                                {
                                    recordDiagnostic = rd;
                                    if (rd.Element("doi") != null)
                                    {
                                        if (rd.Element("doi").Value == doi.DoiName) break;
                                    }
                                }

                                switch (recordDiagnostic.Attribute("status").Value)
                                {
                                    case  "Success":
                                        {
                                            // DOI accepted by CrossRef
                                            XElement diagnosticMessage = recordDiagnostic.Element("msg");
                                            doiClient.UpdateDoiStatus((int)doi.Doiid,
                                                new DoiModel
                                                {
                                                    Doistatusid = configParms.DoiStatusApproved,
                                                    Message = String.Empty,
                                                    Isvalid = 1,
                                                    Userid = null
                                                });
                                            this.LogMessage("DOI " + doi.DoiName + " Verified");
                                            if (string.Compare(diagnosticMessage.Value, "Successfully added", true) == 0)
                                            {
                                                doiClient.AddDoiIdentifier(
                                                    new DoiModel
                                                    {
                                                        Entitytypeid = doi.DoiEntityTypeID,
                                                        Entityid = doi.EntityID,
                                                        Doiname = doi.DoiName,
                                                        Userid = null
                                                    });
                                                approvedDOIAdds.Add(doi.DoiName);
                                            }
                                            else
                                            {
                                                approvedDOIUpdates.Add(doi.DoiName);
                                            }
                                            break;
                                        }
                                    case "Warning":
                                        {
                                            // Warning; DOI deposited, but has a metadata conflict with another DOI
                                            XElement diagnosticMessage = recordDiagnostic.Element("msg");
                                            doiClient.UpdateDoiStatus((int)doi.Doiid,
                                                new DoiModel
                                                {
                                                    Doistatusid = configParms.DoiStatusError,
                                                    Message = "WARNING (CROSSREF): " + diagnosticMessage.Value,
                                                    Isvalid = null,
                                                    Userid = null
                                                });
                                            this.LogMessage("DOI " + doi.DoiName+ " Verification WARNING: " + diagnosticMessage.Value);
                                            warningDOIs.Add(doi.DoiName);
                                            break;
                                        }
                                    case "Failure":
                                        {
                                            // Error reported by CrossRef; set status and record the error message
                                            XElement diagnosticMessage = recordDiagnostic.Element("msg");
                                            doiClient.UpdateDoiStatus((int)doi.Doiid,
                                                new DoiModel
                                                {
                                                    Doistatusid = configParms.DoiStatusError,
                                                    Message = "ERROR (CROSSREF): " + diagnosticMessage.Value,
                                                    Isvalid = null,
                                                    Userid = null
                                                });
                                            this.LogMessage("DOI " + doi.DoiName+ " Verification FAILURE: " + diagnosticMessage.Value);
                                            rejectedDOIs.Add(doi.DoiName);
                                            break;
                                        }
                                }
                            }
                            else if (batchStatusAttrib.Value == "unknown_submission")
                            {
                                // Something went wrong; set the DOI status and log the message from CrossRef
                                doiClient.UpdateDoiStatus((int)doi.Doiid,
                                    new DoiModel
                                    {
                                        Doistatusid = configParms.DoiStatusError,
                                        Message = "ERROR (CROSSREF): " + batchStatusAttrib.Value,
                                        Isvalid = null,
                                        Userid = null
                                    });
                                this.LogMessage("DOI " + doi.DoiName + " NOT Verified: " + batchStatusAttrib.Value);
                                unverifiedDOIs.Add(doi.DoiName);
                            }
                            else
                            {
                                // Keep the current local DOI Status, and record the current CrossRef status
                                doiClient.UpdateDoiStatus((int)doi.Doiid,
                                    new DoiModel
                                    {
                                        Doistatusid = doi.DoiStatusID,
                                        Message = batchStatusAttrib.Value,
                                        Isvalid = null,
                                        Userid = null
                                    });
                                this.LogMessage("DOI " + doi.DoiName + " NOT Verified: " + batchStatusAttrib.Value);
                                unverifiedDOIs.Add(doi.DoiName);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Set DOI error status and record the error
                            log.Error("Exception parsing submission log for batch " + doi.DoiBatchID, ex);
                            errorMessages.Add("Exception parsing submission log for batch " + doi.DoiBatchID+ ":" + ex.Message);
                            doiClient.UpdateDoiStatus((int)doi.Doiid,
                                new DoiModel
                                {
                                    Doistatusid = configParms.DoiStatusError,
                                    Message = "ERROR (CROSSREF): " + ex.Message,
                                    Isvalid = null,
                                    Userid = null
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception verifying submitted DOIs", ex);
                errorMessages.Add("Exception verifying submitted DOIs: " + ex.Message);
            }

            this.LogMessage("Done Verifying Submitted DOIs");
        }

        /// <summary>
        /// Load the submission log for the specified DOI Batch
        /// </summary>
        /// <param name="doiBatchID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private XDocument GetSubmissionLog(string doiBatchID, string filename)
        {
            //var xml = XDocument.Load(string.Format(configParms.CrossrefCheckSubmissionUrlFormat,
            //    configParms.CrossrefLogin, configParms.CrossrefPassword, doiBatchID, filename));
            var xml = XDocument.Load(string.Format(configParms.CrossrefCheckSubmissionUrlFormat,
                configParms.CrossrefLogin, configParms.CrossrefPassword, string.Empty, filename));

            return xml;
        }

        #endregion Verify Submitted DOIs

        #region Get and validate parameters

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;
            string[] keyValue;

            string[] args = System.Environment.GetCommandLineArgs();

            // If command line arguments have been specified, first turn off all functions of this service.
            // Parsing of the arguments will re-enable only the specified functions.
            if (args.Length > 1)
            {
                configParms.ValidateSubmissions = false;
                configParms.SubmitTitles = false;
                configParms.SubmitSegments = false;
            }

            for (int x = 0; x < args.Length; x++)
            {
                if (x > 0)  // first argument is the EXE name; skip it
                {
                    string arg = args[x];

                    switch (arg.ToUpper())
                    {
                        case "/VALIDATE":
                            {
                                configParms.ValidateSubmissions = true;
                                break;
                            }
                        default:
                            {
                                keyValue = arg.Split(':');

                                if (keyValue.Length != 2)
                                {
                                    returnValue = false;
                                }
                                else
                                {
                                    if (keyValue[0].ToUpper() == "/SUBMIT")
                                    {
                                        switch (keyValue[1].ToUpper())
                                        {
                                            case "ALL":
                                                configParms.SubmitTitles = true;
                                                configParms.SubmitSegments = true;
                                                break;
                                            case "TITLE":
                                                configParms.SubmitTitles = true;
                                                break;
                                            case "SEGMENT":
                                                configParms.SubmitSegments = true;
                                                break;
                                            default:
                                                returnValue = false;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = false;
                                    }
                                }

                                if (!returnValue) this.LogMessage("Invalid command line argument " + keyValue[0] + ".  Format is 'BHLDOIService.exe [/VALIDATE] [/SUBMIT:type]', where type is 'TITLE', 'SEGMENT', or 'ALL'.");
                                break;
                            }
                    }
                }

                if (!returnValue) break;
            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(configParms.DoiPrefix))
            {
                this.LogMessage("DOIPrefix not set correctly.  Check configuration file.");
                return false;
            }

            if (string.IsNullOrEmpty(configParms.DoiFormat))
            {
                this.LogMessage("DOIFormat not set correctly.  Check configuration file.");
                return false;
            }

            if (string.IsNullOrEmpty(configParms.CrossrefCheckSubmissionUrlFormat) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositArea) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositorEmail) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositorName) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositUrlBase) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositUrlQueryFormat) ||
                string.IsNullOrEmpty(configParms.CrossrefLogin) ||
                string.IsNullOrEmpty(configParms.CrossrefPassword) ||
                string.IsNullOrEmpty(configParms.CrossrefRegistrantName))
            {
                this.LogMessage("CrossRef information not set correctly.  Check all CrossRef settings in the configuration file.");
                return false;
            }

            if (configParms.NumberToSubmit <= 0)
            {
                this.LogMessage("NumberToSubmit must be greater than zero.  Check configuration file.");
                return false;
            }

            if (configParms.MinMinutesSinceSubmit <= 0)
            {
                this.LogMessage("MinimumMinutesSinceSubmit must be greater than zero.  Check configuration file.");
                return false;
            }

            return true;
        }

        #endregion Get and validate parameters

        #region Process results

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // Send email if any actions were taken
                if (approvedDOIAdds.Count > 0 || approvedDOIUpdates.Count > 0 || 
                    submittedDOIAdds.Count > 0 || submittedDOIUpdates.Count > 0 ||
                    unverifiedDOIs.Count > 0 || warningDOIs.Count > 0 || rejectedDOIs.Count > 0 || 
                    foundDOIs.Count > 0 || unresolvedEntities.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLDOIService: DOI processing on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLDOIService: DOI processing on " + thisComputer + " completed with errors.";
                    }

                    this.LogMessage("Sending Email....");
                    String message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                }
                else
                {
                    this.LogMessage("Nothing to do.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception sending email.", ex);
                return;
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private String GetCompletionEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLDOIService: DOI processing on " + thisComputer + " complete." + endOfLine);
            if (this.submittedDOIAdds.Count > 0)
            {
                sb.Append(endOfLine + this.submittedDOIAdds.Count.ToString() + " new DOIs were Submitted for additions" + endOfLine);
            }
            if (this.submittedDOIUpdates.Count > 0)
            {
                sb.Append(endOfLine + this.submittedDOIUpdates.Count.ToString() + " existing DOIs were Submitted for updates" + endOfLine);
            }
            if (this.approvedDOIAdds.Count > 0)
            {
                sb.Append(endOfLine + this.approvedDOIAdds.Count.ToString() + " DOI additions were Approved by CrossRef" + endOfLine);
            }
            if (this.approvedDOIUpdates.Count > 0)
            {
                sb.Append(endOfLine + this.approvedDOIUpdates.Count.ToString() + " DOI updates were Approved by CrossRef" + endOfLine);
            }
            if (this.unverifiedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.unverifiedDOIs.Count.ToString() + " DOIs are being Processed by CrossRef" + endOfLine);
            }
            if (this.warningDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.warningDOIs.Count.ToString() + " DOIs have a Metadata Conflict at CrossRef" + endOfLine);
            }
            if (this.rejectedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.rejectedDOIs.Count.ToString() + " DOIs were Rejected by CrossRef" + endOfLine);
            }
            if (this.foundDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.foundDOIs.Count.ToString() + " non-BHL DOIs were added to the database" + endOfLine);
            }
            if (this.unresolvedEntities.Count > 0)
            {
                sb.Append(endOfLine + this.unresolvedEntities.Count.ToString() + 
                    " unresolvable entities were found.  More than one matching citation was found for each of these entities." + endOfLine + 
                    "An external DOI needs to be added to each of these:" + endOfLine + endOfLine +
                    "Type\tID");

                foreach(string unresolvedEntity in this.unresolvedEntities)
                {
                    sb.Append(endOfLine + unresolvedEntity);
                }
                sb.Append(endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine + endOfLine);
                foreach (string message in errorMessages)
                {
                    sb.AppendLine(message);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(String subject, String message, String fromAddress,
            String toAddress, String ccAddresses)
        {
            EmailClient restClient = null;

            try
            {
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = subject;
                mailRequest.Body = message;
                mailRequest.From = fromAddress;

                List<string> recipients = new List<string>();
                foreach (string recipient in toAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                if (ccAddresses != String.Empty)
                {
                    List<string> ccs = new List<string>();
                    foreach (string cc in ccAddresses.Split(',')) ccs.Add(cc);
                    mailRequest.Cc = ccs;
                }

                restClient = new EmailClient(configParms.BHLWSRestEndpoint);
                restClient.SendEmail(mailRequest);

                /*
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(fromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(toAddress);
                if (ccAddresses != String.Empty) mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
                */
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        #endregion Process results

        #region Logging

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }

        #endregion Logging
    }
}
