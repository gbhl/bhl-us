
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAFileDAL
	{
        /// <summary>
        /// Select the file with the specified RemoteFileName.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="remoteFileName">File name for which to look</param>
        /// <returns>Object of type File.</returns>
        public IAFile IAFileSelectByRemoteFileName(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string remoteFileName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectByRemoteFileName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("RemoteFileName", SqlDbType.NVarChar, 100, false, remoteFileName)))
            {
                using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
                {
                    CustomGenericList<IAFile> list = helper.ExecuteReader(command);

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

        /// <summary>
        /// Select the file for the specified item with the specified RemoteFileName.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Item for which to look</param>
        /// <param name="remoteFileName">File name for which to look</param>
        /// <returns>Object of type File.</returns>
        public IAFile IAFileSelectByItemAndRemoteFileName(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string remoteFileName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectByItemAndRemoteFileName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("RemoteFileName", SqlDbType.NVarChar, 100, false, remoteFileName)))
            {
                using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
                {
                    CustomGenericList<IAFile> list = helper.ExecuteReader(command);

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

        /// <summary>
        /// Select the files to be downloaded for the specified ItemID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="remoteFileName">File name for which to look</param>
        /// <returns>List of objects of type File.</returns>
        public CustomGenericList<IAFile> IAFileSelectForDownload(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectForDownload", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
                {
                    CustomGenericList<IAFile> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select the files associated with the specified ItemID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Item identifier for which to retreive data</param>
        /// <returns>List of objects of type File.</returns>
        public CustomGenericList<IAFile> IAFileSelectByItem(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
                {
                    CustomGenericList<IAFile> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select the files associated with the specified ItemID and Format.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Item identifier for which to retrieve data</param>
        /// <param name="format">Format for which to retrieve data</param>
        /// <returns>List of objects of type File.</returns>
        public IAFile IAFileSelectByItemAndFormat(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string format)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectByItemAndFormat", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("Format", SqlDbType.NVarChar, 50, false, format)))
            {
                using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
                {
                    CustomGenericList<IAFile> list = helper.ExecuteReader(command);

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
    }
}
