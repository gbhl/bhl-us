using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class SegmentDAL
	{
        /// <summary>
        /// Select the specified segment and all of its related objects.
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

            Segment segment = SegmentSelectForSegmentID(connection, transaction, segmentId);

            if (segment != null)
            {
                if (segment.ItemID != null)
                {
                    Item item = new ItemDAL().ItemSelectAuto(connection, transaction, (int)segment.ItemID);
                    segment.ItemVolume = item.Volume;

                    Title title = new TitleDAL().TitleSelectAuto(connection, transaction, item.PrimaryTitleID);
                    segment.TitleShortTitle = title.ShortTitle;
                }

                segment.AuthorList = new SegmentAuthorDAL().SegmentAuthorSelectBySegmentID(connection, transaction, segmentId);
                if (segment.AuthorList != null && segment.AuthorList.Count > 0)
                {
                    AuthorDAL authorDAL = new AuthorDAL();
                    foreach (SegmentAuthor segmentAuthor in segment.AuthorList)
                    {
                        segmentAuthor.Author = authorDAL.AuthorSelectAuto(connection, transaction, segmentAuthor.AuthorID);
                    }
                }

                segment.IdentifierList = new SegmentIdentifierDAL().SegmentIdentifierSelectBySegmentID(connection, transaction, segmentId);
                segment.KeywordList = new SegmentKeywordDAL().SegmentKeywordSelectBySegmentID(connection, transaction, segmentId);
                segment.PageList = new SegmentPageDAL().SegmentPageSelectBySegmentID(connection, transaction, segmentId);
                segment.NameList = new NameSegmentDAL().NameSegmentSelectBySegmentID(connection, transaction, segmentId);
                segment.RelatedSegmentList = SegmentSelectRelated(connection, transaction, segmentId);
            }

            return segment;
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
        /// Select segment data for EndNote citations.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleEndNote> SegmentSelectAllEndNoteCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectAllEndNoteCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleEndNote> helper = new CustomSqlHelper<TitleEndNote>())
                {
                    CustomGenericList<TitleEndNote> list = helper.ExecuteReader(command);
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
        /// Select data for EndNote reference for the specified Segment.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Segment identifier for which to get EndNote data</param>
        /// <returns>List of type TitleEndNote.</returns>
        public CustomGenericList<TitleEndNote> SegmentSelectEndNoteForSegmentID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int segmentId,
                        short includeNoContent)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectEndNoteForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, false, includeNoContent)))
            {
                using (CustomSqlHelper<TitleEndNote> helper = new CustomSqlHelper<TitleEndNote>())
                {
                    CustomGenericList<TitleEndNote> list = helper.ExecuteReader(command);
                    return (list);
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

                if (segment.RelatedSegmentList.Count > 0)
                {
                    SegmentClusterSegmentDAL clusterDAL = new SegmentClusterSegmentDAL();
                    foreach (Segment relatedSegment in segment.RelatedSegmentList)
                    {
                        if (relatedSegment.IsDeleted)
                        {
                            clusterDAL.SegmentClusterSegmentDeleteAuto(connection, transaction, relatedSegment.SegmentID);
                        }
                        else if (relatedSegment.IsNew || relatedSegment.IsDirty)
                        {
                            SegmentClusterSegment segmentCluster = 
                                clusterDAL.SegmentClusterSegmentUpdate(connection, transaction, relatedSegment.SegmentID, 
                                segment.SegmentClusterId, relatedSegment.IsPrimary, userId);

                            // Grab from just-affected record, in case the cluster ID for the segments has changed
                            segment.SegmentClusterId = segmentCluster.SegmentClusterID;
                        }
                    }

                    // Insert/Update the SegmentClusterSegment record for the current segment
                    clusterDAL.SegmentClusterSegmentUpdate(connection, transaction, segment.SegmentID, 
                        segment.SegmentClusterId, segment.IsPrimary, userId);
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

    }
}
