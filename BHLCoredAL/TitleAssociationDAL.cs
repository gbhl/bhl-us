#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleAssociationDAL
	{
        /// <summary>
        /// Select all Items for a particular Title.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Title.</returns>
        public List<TitleAssociation> TitleAssociationSelectByTitleID(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int titleID,
                bool? active)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
                    CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active)))
            {
                using (CustomSqlHelper<TitleAssociation> helper = new CustomSqlHelper<TitleAssociation>())
                {
                    List<TitleAssociation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public TitleAssociation TitleAssociationSelectExtended(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int titleAssociationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            TitleAssociation titleAssociation = this.TitleAssociationSelectAuto(connection, transaction, titleAssociationID);

            if (titleAssociation != null)
            {
                titleAssociation.TitleAssociationIdentifiers = new TitleAssociation_TitleIdentifierDAL().TitleAssociation_TitleIdentifierSelectByTitleAssociationID(connection, transaction, titleAssociation.TitleAssociationID);
            }

            return titleAssociation;
        }

        public List<TitleAssociation> TitleAssociationSelectExtendedForTitle(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            List<TitleAssociation> titleAssociations = this.TitleAssociationSelectByTitleID(connection, transaction, titleID, null);

            foreach (TitleAssociation titleAssociation in titleAssociations)
            {
                titleAssociation.TitleAssociationIdentifiers = new TitleAssociation_TitleIdentifierDAL().TitleAssociation_TitleIdentifierSelectByTitleAssociationID(connection, transaction, titleAssociation.TitleAssociationID);
            }

            return titleAssociations;
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            TitleAssociation titleAssociation, int userId)
        {
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

                CustomDataAccessStatus<TitleAssociation> updatedTitleAssociation = 
                    new TitleAssociationDAL().TitleAssociationManageAuto(connection, transaction, titleAssociation, userId);

                if (titleAssociation.TitleAssociationIdentifiers.Count > 0)
                {
                    TitleAssociation_TitleIdentifierDAL titleAssociationTitleIdentifierDAL = new TitleAssociation_TitleIdentifierDAL();
                    foreach (TitleAssociation_TitleIdentifier titleAssociationTitleIdentifier in titleAssociation.TitleAssociationIdentifiers)
                    {
                        if (titleAssociationTitleIdentifier.TitleAssociationID == 0) titleAssociationTitleIdentifier.TitleAssociationID = updatedTitleAssociation.ReturnObject.TitleAssociationID;
                        titleAssociationTitleIdentifierDAL.TitleAssociation_TitleIdentifierManageAuto(connection, transaction, titleAssociationTitleIdentifier, userId);
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
        }

        /// <summary>
        /// Returns a list of associations that have suspected character encoding problems.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Institution for which to return associations</param>
        /// <param name="maxAge">Age in days of associations to consider (i.e. associations new in the last 30 days)</param>
        /// <returns></returns>
        public List<TitleAssociationSuspectCharacter> TitleAssociationSelectWithSuspectCharacters(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode,
                int maxAge)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationSelectWithSuspectCharacters", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("MaxAge", SqlDbType.Int, null, false, maxAge)))
            {
                using (CustomSqlHelper<TitleAssociationSuspectCharacter> helper = new CustomSqlHelper<TitleAssociationSuspectCharacter>())
                {
                    List<TitleAssociationSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
