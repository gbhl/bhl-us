
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class BSSegmentDAL
    {
        public List<BSSegment> BSSegmentSelectByItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentSelectByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
                {
                    List<BSSegment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<BSSegment> BSSegmentSelectHarvestedByItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentSelectHarvestedByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
                {
                    List<BSSegment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void BSSegmentResolveAuthors(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentResolveAuthors", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void BSSegmentPublishToProduction(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID, int segmentID, out int statusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentPublishToProduction", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateOutputParameter("@SegmentStatusID", SqlDbType.Int, null, false)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                statusID = (int)command.Parameters["@SegmentStatusID"].Value;
            }
        }

        public BSSegment Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, BSSegment segment, List<BSSegmentAuthor> authors)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;
            CustomDataAccessStatus<BSSegment> updatedSegment;

            if (connection == null) connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"));
            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                this.SetSegmentDefaults(segment);
                updatedSegment = new BSSegmentDAL().BSSegmentManageAuto(connection, transaction, segment);

                BSSegmentPageDAL pageDAL = new BSSegmentPageDAL();
                foreach (BSSegmentPage page in segment.BSSegmentPages)
                {
                    page.SegmentID = updatedSegment.ReturnObject.SegmentID;
                    pageDAL.BSSegmentPageInsertAuto(connection, transaction, page);
                }

                BSSegmentAuthorDAL authorDAL = new BSSegmentAuthorDAL();
                int sequence = 1;
                foreach (BSSegmentAuthor author in authors)
                {
                    author.SegmentID = updatedSegment.ReturnObject.SegmentID;
                    author.SequenceOrder = sequence;
                    this.SetSegmentAuthorDefaults(author);
                    authorDAL.BSSegmentAuthorInsertAuto(connection, transaction, author);
                    sequence++;
                }

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);
                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }

            return updatedSegment.ReturnObject;
        }

        /// <summary>
        /// Set the defaults for any required fields that are null.
        /// </summary>
        /// <param name="segment"></param>
        private void SetSegmentDefaults(BSSegment segment)
        {
            DateTime date = DateTime.Now;

            segment.Title = segment.Title ?? string.Empty;
            segment.BioStorReferenceID = segment.BioStorReferenceID ?? string.Empty;
            segment.SequenceOrder = (segment.SequenceOrder == short.MinValue ? (short)0 : segment.SequenceOrder);
            segment.Genre = segment.Genre ?? string.Empty;
            segment.ContainerTitle = segment.ContainerTitle ?? string.Empty;
            segment.Volume = segment.Volume ?? string.Empty;
            segment.Series = segment.Series ?? string.Empty;
            segment.Issue = segment.Issue ?? string.Empty;
            segment.Year = segment.Year ?? string.Empty;
            segment.Date = segment.Date ?? string.Empty;
            segment.ISSN = segment.ISSN ?? string.Empty;
            segment.DOI = segment.DOI ?? string.Empty;
            segment.StartPageNumber = segment.StartPageNumber ?? string.Empty;
            segment.EndPageNumber = segment.EndPageNumber ?? string.Empty;
            segment.CreationDate = (segment.CreationDate == DateTime.MinValue ? date : segment.CreationDate);
            segment.LastModifiedDate = (segment.LastModifiedDate == DateTime.MinValue ? date : segment.LastModifiedDate);
            segment.ContributorName = segment.ContributorName ?? string.Empty;

            foreach (BSSegmentPage page in segment.BSSegmentPages)
            {
                page.CreationDate = (page.CreationDate == DateTime.MinValue ? date : page.CreationDate);
            }
        }

        private void SetSegmentAuthorDefaults(BSSegmentAuthor author)
        {
            DateTime date = DateTime.Now;

            author.BioStorID = author.BioStorID ?? string.Empty;
            author.LastName = author.LastName ?? string.Empty;
            author.FirstName = author.FirstName ?? string.Empty;
            author.SequenceOrder = (author.SequenceOrder == int.MinValue ? 1 : author.SequenceOrder);
            author.VIAFIdentifier = author.VIAFIdentifier ?? string.Empty;
            author.CreationDate = (author.CreationDate == DateTime.MinValue ? date : author.CreationDate);
            author.LastModifiedDate = (author.LastModifiedDate == DateTime.MinValue ? date : author.LastModifiedDate);
        }
    }
}

