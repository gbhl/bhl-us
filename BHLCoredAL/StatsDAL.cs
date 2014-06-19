#region Using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class StatsDAL
	{
        public Stats StatsSelect(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, false, false, false, false, false, false);
        }

        public Stats StatsSelectExpanded(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, false, false, false, false, false);
        }

        public Stats StatsSelectNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, true, false, false, false, false);
        }

        public Stats StatsSelectUniqueNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, false, true, false, false, false);
        }

        public Stats StatsSelectVerifiedNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, false, false, true, false, false);
        }

        public Stats StatsSelectEOLNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, false, false, false, true, false);
        }

        public Stats StatsSelectEOLPages(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            return this.StatsSelect(sqlConnection, sqlTransaction, true, false, false, false, false, true);
        }

		private Stats StatsSelect(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
                bool expanded,
                bool names,
                bool uniqueNames,
                bool verifiedNames,
                bool eolNames,
                bool eolPages)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "StatsSelect", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Expanded", SqlDbType.Bit, null, false, expanded),
                CustomSqlHelper.CreateInputParameter("Names", SqlDbType.Bit, null, false, names),
                CustomSqlHelper.CreateInputParameter("UniqueNames", SqlDbType.Bit, null, false, uniqueNames),
                CustomSqlHelper.CreateInputParameter("VerifiedNames", SqlDbType.Bit, null, false, verifiedNames),
                CustomSqlHelper.CreateInputParameter("EOLNames", SqlDbType.Bit, null, false, eolNames),
                CustomSqlHelper.CreateInputParameter("EOLPages", SqlDbType.Bit, null, false, eolPages)))
			{
                command.CommandTimeout = 300;   // Set timeout to 5 minutes (name stats can take a while to generate)
				CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows( command );
				CustomDataRow row = list[ 0 ];
				Stats stats = new Stats();
				stats.TitleCount = (int)row[ "TitleCount" ].Value;
				stats.VolumeCount = (int)row[ "VolumeCount" ].Value;
				stats.PageCount = (int)row[ "PageCount" ].Value;
				stats.PageTotal = (int)row[ "PageTotal" ].Value;
				stats.TitleTotal = (int)row[ "TitleTotal" ].Value;
				stats.VolumeTotal = (int)row[ "VolumeTotal" ].Value;
                stats.SegmentCount = (int)row["SegmentCount"].Value;
                stats.SegmentTotal = (int)row["SegmentTotal"].Value;
                stats.ItemSegmentCount = (int)row["ItemSegmentCount"].Value;
                stats.ItemSegmentTotal = (int)row["ItemSegmentTotal"].Value;
                stats.NameCount = (int)row["NameCount"].Value;
                stats.NameTotal = (int)row["NameTotal"].Value;
				stats.UniqueNameCount = (int)row[ "UniqueNameCount" ].Value;
				stats.UniqueNameTotal = (int)row[ "UniqueNameTotal" ].Value;
                stats.VerifiedNameCount = (int)row["VerifiedNameCount"].Value;
                stats.VerifiedNameTotal = (int)row["VerifiedNameTotal"].Value;
                stats.EolNameCount = (int)row["EOLNameCount"].Value;
                stats.EolNameTotal = (int)row["EOLNameTotal"].Value;
                stats.EolPageCount = (int)row["EOLPageCount"].Value;
                stats.EolPageTotal = (int)row["EOLPageTotal"].Value;

				return stats;
			}
		}

        public Stats StatsSelectForCollection(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsSelectForCollection", connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                command.CommandTimeout = 60;   // Set timeout to 1 minutes (stats can take a while to generate)
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                CustomDataRow row = list[0];

                Stats stats = new Stats();
                stats.TitleCount = (int)row["TitleCount"].Value;
                stats.VolumeCount = (int)row["VolumeCount"].Value;
                stats.PageCount = (int)row["PageCount"].Value;

                return stats;
            }
        }
    
        public CustomGenericList<EntityCount> EntityCountSelectLatest(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            CustomGenericList<EntityCount> counts = new CustomGenericList<EntityCount>();

            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.EntityCountSelectLatest", connection, transaction))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                foreach(CustomDataRow row in list)
                {
                    EntityCount count = new EntityCount();
                    count.EntityCountTypeID = (EntityCount.EntityType)row["EntityCountTypeID"].Value;
                    count.FullName = row["FullName"].Value.ToString();
                    count.DisplayName = row["DisplayName"].Value.ToString();
                    count.CountValue = (int)row["CountValue"].Value;
                    counts.Add(count);
                }
            }

            return counts;
        }
    }
}
