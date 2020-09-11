using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class SegmentDAL
	{
        /// <summary>
        /// Select the specified segment and all of its related objects for editing.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="segmentId"></param>
        /// <returns></returns>
        public Segment SegmentSelectForEdit(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            // Get the segment information for editing
            Segment segment = SegmentSelectAuto(connection, transaction, segmentId);
            if (segment != null)
            {
                // NOTE: The selection of IsPrimary and PageRange values could be included with the previous database request
                // Get a couple extra segment details for editing
                Segment segmentExtra = SegmentSelectForSegmentID(connection, transaction, segmentId);
                segment.IsPrimary = segmentExtra.IsPrimary;
                segment.PageRange = segmentExtra.PageRange;
            }

            // Get the rest of the segment details
            SegmentSelectDetail(connection, transaction, segment);

            return segment;
        }

        /// <summary>
        /// Select the specified segment and all of its related objects for read-only access.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="segmentId">Identifier of the segment for which to search</param>
        /// <returns>An object of type Segment</returns>
        public Segment SegmentSelectExtended(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            // Get the read-only segment info for display
            Segment segment = SegmentSelectForSegmentID(connection, transaction, segmentId);

            // Get the rest of the segment details
            SegmentSelectDetail(connection, transaction, segment);

            return segment;
        }

        /// <summary>
        /// Select the supporting segment metadata (authors, keywords, identifier, etc)
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="segment"></param>
        private void SegmentSelectDetail(SqlConnection connection, SqlTransaction transaction, Segment segment)
        {
            if (segment != null)
            {
                if (segment.ItemID != null)
                {
                    Item item = new ItemDAL().ItemSelectAuto(connection, transaction, (int)segment.ItemID);
                    segment.ItemVolume = item.Volume;
                    segment.ItemYear = item.Year ?? string.Empty;

                    Title title = new TitleDAL().TitleSelectAuto(connection, transaction, item.PrimaryTitleID);
                    segment.TitleId = title.TitleID;
                    segment.TitleFullTitle = title.FullTitle;
                    segment.TitleShortTitle = title.ShortTitle;
                    segment.TitlePublicationPlace = title.Datafield_260_a;
                    segment.TitlePublisherName = title.Datafield_260_b;
                    segment.TitlePublicationDate = (title.StartYear == null ? "" : title.StartYear.ToString());
                }

                segment.AuthorList = new SegmentAuthorDAL().SegmentAuthorSelectBySegmentID(connection, transaction, segment.SegmentID);
                if (segment.AuthorList != null && segment.AuthorList.Count > 0)
                {
                    AuthorDAL authorDAL = new AuthorDAL();
                    foreach (SegmentAuthor segmentAuthor in segment.AuthorList)
                    {
                        segmentAuthor.Author = authorDAL.AuthorSelectAuto(connection, transaction, segmentAuthor.AuthorID);
                    }
                }

                CustomGenericList<DOI> dois = new DOIDAL().DOISelectValidForSegment(connection, transaction, segment.SegmentID);
                foreach (DOI doi in dois)
                {
                    // Grab the first DOI for the segment (by the very nature of DOIs, there should only be one)
                    segment.DOIName = doi.DOIName;
                    break;
                }

                segment.ContributorList = new InstitutionDAL().InstitutionSelectBySegmentIDAndRole(connection, transaction, segment.SegmentID, InstitutionRole.Contributor);
                segment.IdentifierList = new SegmentIdentifierDAL().SegmentIdentifierSelectBySegmentID(connection, transaction, segment.SegmentID, null);
                segment.KeywordList = new SegmentKeywordDAL().SegmentKeywordSelectBySegmentID(connection, transaction, segment.SegmentID);
                segment.PageList = new SegmentPageDAL().SegmentPageSelectBySegmentID(connection, transaction, segment.SegmentID);
                segment.NameList = new NameSegmentDAL().NameSegmentSelectBySegmentID(connection, transaction, segment.SegmentID);
                segment.RelatedSegmentList = SegmentSelectRelated(connection, transaction, segment.SegmentID);
            }
        }

        /// <summary>
        /// Select the segments associated with the specified author
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="authorId">Identifier of the author</param>
        /// <returns>A list of type Segment</returns>
        public CustomGenericList<Segment> SegmentSimpleSelectByAuthor(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSimpleSelectByAuthor", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorId", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select detailed information about the segments associated with the specified author
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="authorId">Identifier of the author</param>
        /// <returns>A list of type Segment</returns>
        public CustomGenericList<Segment> SegmentSelectForAuthorID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectForAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorId", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select detailed information about the segments published between the specified dates
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>A list of type Segment</returns>
        public CustomGenericList<Segment> SegmentSelectByDateRange(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string startDate, string endDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByDateRange", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.VarChar, 20, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.VarChar, 20, false, endDate)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select segments that start with the specified title string.  Can use regular expressions as argument
        /// (as long as the expression is recognized by the SQL Server "LIKE" operator.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public CustomGenericList<Segment> SegmentSelectByTitleLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string title)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByTitleLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select segments that do not start with the specified title string.  Can use regular expressions as argument
        /// (as long as the expression is recognized by the SQL Server "LIKE" operator.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public CustomGenericList<Segment> SegmentSelectByTitleNotLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string title)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByTitleNotLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select detailed information about the segments associated with the specified keyword
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="keyword">The keyword with which segments are associated</param>
        /// <returns>A list of type Segment</returns>
        public CustomGenericList<Segment> SegmentSelectForKeyword(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string keyword)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectForKeyword", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select the segments associated with the specified item
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="itemId">Identifier of the item.</param>
        /// <returns>A list of type segment</returns>
        public CustomGenericList<Segment> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemId, short showAll)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId),
                CustomSqlHelper.CreateInputParameter("ShowAll", SqlDbType.SmallInt, null, false, showAll)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select segment data for BibTex citations.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleBibTeX> SegmentSelectAllBibTeXCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectAllBibTeXCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
                {
                    CustomGenericList<TitleBibTeX> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for BibTeX reference for the specified Segment.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Segment identifier for which to get BibTex data</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleBibTeX> SegmentSelectBibTexForSegmentID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int segmentId,
                        short includeNoContent)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectBibTexForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, false, includeNoContent)))
            {
                using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
                {
                    CustomGenericList<TitleBibTeX> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select identifiers of all published segments.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Segment identifier for which to get BibTex data</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<Segment> SegmentSelectPublished(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectPublished", connection, transaction))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for RIS citations for all segments.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type RISCitation.</returns>
        public CustomGenericList<RISCitation> SegmentSelectAllRISCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectAllRISCitations", connection, transaction))
            {
                using (CustomSqlHelper<RISCitation> helper = new CustomSqlHelper<RISCitation>())
                {
                    CustomGenericList<RISCitation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for a RIS citations for the specified segment.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type RISCitation.</returns>
        public CustomGenericList<RISCitation>SegmentSelectRISCitationForSegmentID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectRISCitationForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<RISCitation> helper = new CustomSqlHelper<RISCitation>())
                {
                    CustomGenericList<RISCitation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select the segments related to the specified segment
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="segmentId">Identifier of the segment</param>
        /// <returns>A list of type Segment</returns>
        public CustomGenericList<Segment> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectRelated", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select the specified Segment.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Identifier of segment to retrieve/</param>
        /// <returns>Object of type Segment.</returns>
        public Segment SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return (list[0]);
                    else
                        return null;
                }
            }
        }

        /// <summary>
        /// Select the segments assigned the specified status
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="statusId">Segment status for which to search.</param>
        /// <returns>A list of type segment</returns>
        public CustomGenericList<Segment> SegmentSelectByStatusID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int statusId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByStatusID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, statusId)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select all of the segments included in recently modified clusters.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="numberOfClusters">The number of clusters to return.</param>
        /// <returns></returns>
        public CustomGenericList<Segment> SegmentSelectRecentlyClustered(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int numberOfClusters)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectRecentlyClustered", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumClusters", SqlDbType.Int, null, false, numberOfClusters)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        public CustomGenericList<Segment> SegmentSelectByInstitutionAndStartsWith(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        String institutionCode,
                        String startsWith)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByInstitutionAndStartsWith", connection, transaction,
                     CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                     CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.NVarChar, 1000, false, startsWith)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Segment> SegmentSelectByInstitutionAndStartsWithout(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        String institutionCode,
                        String startsWith)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectByInstitutionAndStartsWithout", connection, transaction,
                     CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                     CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.NVarChar, 1000, false, startsWith)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Find segment, given doi and/or page IDs.  Used during the ImportRecord process.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="doi"></param>
        /// <param name="startPageID"></param>
        /// <param name="endPageID"></param>
        /// <returns></returns>
        public CustomGenericList<Segment> SegmentResolve(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi, int? startPageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.SegmentResolve", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi),
                    CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, false, startPageID)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    CustomGenericList<Segment> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Save the specified segment and all supporting information (including authors, keywords, identifiers, and pages).
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="segment">The segment to be saved</param>
        /// <param name="userId">User identifier to associate with the segment (last modified / creator)</param>
        /// <returns>The identifier of the saved segment</returns>
        public int Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Segment segment, int userId)
        {
            int segmentID = segment.SegmentID;
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection =
                    CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                CustomDataAccessStatus<Segment> updatedSegment =
                    new SegmentDAL().SegmentManageAuto(connection, transaction, segment, userId);

                segmentID = updatedSegment.ReturnObject.SegmentID;

                DOIDAL doiDAL = new DOIDAL();
                CustomGenericList<DOI> doiList = doiDAL.DOISelectValidForSegment(connection, transaction, segmentID);

                DOI doi = null;
                if (doiList.Count == 0)
                {
                    if (!string.IsNullOrWhiteSpace(segment.DOIName))
                    {
                        // Insert
                        doi = new DOI();
                        doi.IsNew = true;
                        doi.EntityID = segmentID;
                        doi.DOIEntityTypeID = 40;   // Segment
                        doi.DOIName = segment.DOIName;
                        doi.DOIStatusID = 200;
                        doi.StatusDate = DateTime.Now;
                        doi.StatusMessage = "User-edited";
                        doi.IsValid = 1;
                        doi.CreationDate = DateTime.Now;
                        doi.LastModifiedDate = DateTime.Now;
                    }
                }
                else // DOI exists
                {
                    doi = doiList[0];
                    doi.IsNew = false;

                    if (!string.IsNullOrWhiteSpace(segment.DOIName))
                    {
                        // Update
                        if (string.Compare(doi.DOIName, segment.DOIName, true) != 0)
                        {
                            doi.DOIName = segment.DOIName;
                            if (!doi.DOIName.StartsWith("10.5962"))
                            {
                                doi.DOIStatusID = 200;
                                doi.StatusDate = DateTime.Now;
                            }
                            doi.StatusMessage = "User-edited";
                            doi.LastModifiedDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        // Delete
                        doi.IsDeleted = true;
                    }
                }
                if (doi != null) doiDAL.DOIManageAuto(connection, transaction, doi);

                if (segment.ContributorList.Count > 0)
                {
                    SegmentInstitutionDAL segmentInstitutionDAL = new SegmentInstitutionDAL();
                    foreach (Institution institution in segment.ContributorList)
                    {
                        if (institution.IsDeleted)
                        {
                            segmentInstitutionDAL.SegmentInstitutionDeleteAuto(connection, transaction, (int)institution.EntityInstitutionID);
                        }
                        if (institution.IsNew)
                        {
                            segmentInstitutionDAL.SegmentInstitutionInsert(connection, transaction, segmentID, 
                                institution.InstitutionCode, institution.InstitutionRoleName, userId);
                        }
                    }
                }

                if (segment.AuthorList.Count > 0)
                {
                    SegmentAuthorDAL segmentAuthorDAL = new SegmentAuthorDAL();
                    foreach (SegmentAuthor segmentAuthor in segment.AuthorList)
                    {
                        if (segmentAuthor.SegmentID == 0) segmentAuthor.SegmentID = updatedSegment.ReturnObject.SegmentID;
                        segmentAuthorDAL.SegmentAuthorManageAuto(connection, transaction, segmentAuthor, userId);
                    }
                }

                if (segment.KeywordList.Count > 0)
                {
                    SegmentKeywordDAL segmentKeywordDAL = new SegmentKeywordDAL();
                    KeywordDAL keywordDAL = new KeywordDAL();
                    foreach (SegmentKeyword segmentKeyword in segment.KeywordList)
                    {
                        // If we have a newly entered keyword, insert it and/or get its ID
                        if (segmentKeyword.KeywordID == 0)
                        {
                            Keyword keyword = keywordDAL.KeywordSelectByKeyword(connection, transaction, segmentKeyword.Keyword);
                            if (keyword == null)
                            {
                                // Keyword not found, so insert a new one
                                keyword = new Keyword();
                                keyword.Keyword = segmentKeyword.Keyword;
                                keyword.CreationUserID = userId;
                                keyword.CreationDate = DateTime.Now;
                                keyword.LastModifiedUserID = userId;
                                keyword.LastModifiedDate = DateTime.Now;
                                keyword.IsNew = true;
                                keyword = keywordDAL.KeywordInsertAuto(connection, transaction, keyword);
                            }
                            segmentKeyword.KeywordID = keyword.KeywordID;
                        }

                        // Insert/Update the TitleKeyword record
                        if (segmentKeyword.SegmentID == 0) segmentKeyword.SegmentID = updatedSegment.ReturnObject.SegmentID;
                        segmentKeywordDAL.SegmentKeywordManageAuto(connection, transaction, segmentKeyword, userId);
                    }
                }

                if (segment.IdentifierList.Count > 0)
                {
                    SegmentIdentifierDAL segmentIdentifierDAL = new SegmentIdentifierDAL();
                    foreach (SegmentIdentifier segmentIdentifier in segment.IdentifierList)
                    {
                        if (segmentIdentifier.SegmentID == 0) segmentIdentifier.SegmentID = updatedSegment.ReturnObject.SegmentID;
                        segmentIdentifierDAL.SegmentIdentifierManageAuto(connection, transaction, segmentIdentifier, userId);
                    }
                }

                if (segment.PageList.Count > 0)
                {
                    SegmentPageDAL segmentPageDAL = new SegmentPageDAL();
                    foreach (SegmentPage segmentPage in segment.PageList)
                    {
                        if (segmentPage.SegmentID == 0) segmentPage.SegmentID = updatedSegment.ReturnObject.SegmentID;
                        segmentPageDAL.SegmentPageManageAuto(connection, transaction, segmentPage, userId);
                    }
                }

                // See if any related segments have been modified (added/updated/deleted)
                bool isRelatedChanged = false;
                foreach (Segment relatedSegment in segment.RelatedSegmentList)
                {
                    if (relatedSegment.IsDirty ||
                        relatedSegment.IsNew ||
                        relatedSegment.IsDeleted)
                    {
                        isRelatedChanged = true;
                        break;
                    }
                }

                if (isRelatedChanged)
                {
                    SegmentClusterSegmentDAL clusterDAL = new SegmentClusterSegmentDAL();

                    // Delete the existing records
                    clusterDAL.SegmentClusterSegmentDeleteForSegment(connection, transaction, updatedSegment.ReturnObject.SegmentID);

                    // Add new records
                    foreach (SegmentClusterTypes type in Enum.GetValues(typeof(SegmentClusterTypes)))
                    {
                        var relatedSegments = from s in segment.RelatedSegmentList.Cast<Segment>()
                                              where s.SegmentClusterTypeId == (int?)type && !s.IsDeleted
                                              select s;

                        int? segmentClusterID = null;
                        foreach (Segment relatedSegment in relatedSegments)
                        {
                            if (relatedSegment.SegmentID == 0) relatedSegment.SegmentID = updatedSegment.ReturnObject.SegmentID;

                            // Each part-of relationship contains only two segments, so
                            // reset the cluster ID for each pair saved.  Other relationships
                            // may contain multiple segments, so reuse the same cluster ID.
                            if (type == SegmentClusterTypes.PartOf) segmentClusterID = null;

                            SegmentClusterSegment segmentCluster =
                                clusterDAL.SegmentClusterSegmentInsert(connection, transaction, relatedSegment.SegmentID,
                                segmentClusterID, relatedSegment.IsPrimary, relatedSegment.SegmentClusterTypeId, userId);

                            // Get the SegmentClusterID from the just-affected record
                            if (segmentClusterID == null)
                            {
                                segmentClusterID = segmentCluster.SegmentClusterID;

                                // Insert/Update the SegmentClusterSegment record for the current segment
                                clusterDAL.SegmentClusterSegmentInsert(connection, transaction, updatedSegment.ReturnObject.SegmentID,
                                    segmentClusterID, segment.IsPrimary, relatedSegment.SegmentClusterTypeId,
                                    userId);
                            }
                        }
                    }
                }

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch (Exception ex)
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);

                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }

            return segmentID;
        }

        public CustomGenericList<DOI> SegmentSelectWithoutSubmittedDOI(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int numberToReturn)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectWithoutSubmittedDOI",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumberToReturn", SqlDbType.Int, null, false, numberToReturn)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
