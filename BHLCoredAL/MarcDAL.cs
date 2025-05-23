using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class MarcDAL
	{
        public List<Marc> MarcSelectPendingImport(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int batchID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectPendingImport", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, batchID)))
            {
                using (CustomSqlHelper<Marc> helper = new CustomSqlHelper<Marc>())
                {
                    List<Marc> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<vwMarcDataField> MarcSelectFullDetailsForMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectFullDetailsForMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
            {
                using (CustomSqlHelper<vwMarcDataField> helper = new CustomSqlHelper<vwMarcDataField>())
                {
                    List<vwMarcDataField> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<Marc> MarcSelectForImportByBatchID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int batchID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectForImportByBatchID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, batchID)))
            {
                using (CustomSqlHelper<Marc> helper = new CustomSqlHelper<Marc>())
                {
                    List<Marc> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public Title MarcSelectTitleDetailsByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectTitleDetailsByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    List<Title> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<TitleKeyword> MarcSelectTitleKeywordsByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectTitleKeywordsByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
                {
                    List<TitleKeyword> list = helper.ExecuteReader(command);
                        return (list);
                }
            }
        }

        public List<TitleNote> MarcSelectTitleNotesByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectTitleNotesByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
                {
                    List<TitleNote> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<TitleLanguage> MarcSelectTitleLanguagesByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectTitleLanguagesByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleLanguage> helper = new CustomSqlHelper<TitleLanguage>())
                {
                    List<TitleLanguage> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<TitleAuthor> MarcSelectAuthorsByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectAuthorsByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleAuthor> helper = new CustomSqlHelper<TitleAuthor>())
                {
                    List<TitleAuthor> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<Title_Identifier> MarcSelectTitleIdentifiersByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectTitleIdentifiersByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
                {
                    List<Title_Identifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<TitleAssociation> MarcSelectAssociationsByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectAssociationsByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleAssociation> helper = new CustomSqlHelper<TitleAssociation>())
                {
                    List<TitleAssociation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<TitleAssociation_TitleIdentifier> MarcSelectAssociationIdsByMarcDataFieldID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcDataFieldId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectAssociationIdsByMarcDataFieldID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldId)))
            {
                using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
                {
                    List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<TitleVariant> MarcSelectVariantsByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectVariantsByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcId)))
            {
                using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
                {
                    List<TitleVariant> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }


        public bool MarcResolveTitles(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int batchId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcResolveTitles", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, batchId)))
            {
                bool result = Convert.ToBoolean(CustomSqlHelper.ExecuteScalar(command));
                return result;
            }
        }

        public void MarcUpdateStatusImported(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcUpdateStatusImported", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void MarcUpdateStatusError(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcUpdateStatusError", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
