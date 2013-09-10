using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Sql;
using System.Data.Common;

namespace MOBOT.BHL.BHLAuditExport
{
    class DataAbstractLayer
    {
        public static IDataReader RecentAuditTable(DateTime lastAuditDate)
        {
            //DataTable retDataTable = null;
            Database db = DatabaseFactory.CreateDatabase("BHL");
            string strCommand = null;
            strCommand = "audit.AuditBasicSelectFromDateToNow";
            DbCommand cmdCust = db.GetStoredProcCommand(strCommand);
            cmdCust.CommandTimeout = 600;
            db.AddInParameter(cmdCust, "AuditDate", DbType.Date, lastAuditDate);
            //retDataTable = db.ExecuteDataSet(cmdCust).Tables[0];

            IDataReader reader = db.ExecuteReader(cmdCust);

            return reader;
        }

        public static DataRow GetDataForRow(string tableName, Dictionary<string, string> EntityIDs)
        {
            DataRow retDataRow = null;
            Database db = DatabaseFactory.CreateDatabase("BHL");
            string strCommand = null;
            strCommand = String.Format("Select * from {0} ", tableName);
            string strWhereClause = null;
            foreach (KeyValuePair<string, string> tmpPair in EntityIDs)
            {
                if (strWhereClause == null)
                {
                    strWhereClause = String.Format(" Where {0} = '{1}'", tmpPair.Key.ToString(), tmpPair.Value.ToString().Replace("'", "''"));
                }
                else
                {
                    strWhereClause += String.Format(" AND {0} = '{1}'", tmpPair.Key.ToString(), tmpPair.Value.ToString().Replace("'", "''"));
                }
            }

            DbCommand cmdSelect = db.GetSqlStringCommand(strCommand + strWhereClause);
            DataTable tmpDataTable = db.ExecuteDataSet(cmdSelect).Tables[0];
            if (tmpDataTable.Rows.Count > 0)
            {
                retDataRow = tmpDataTable.Rows[0];
            }

            return retDataRow;
        }

        public static DataTable GetPrimaryKeys()
        {
            DataTable retDataTable = null;
            Database db = DatabaseFactory.CreateDatabase("BHL");
            string strCommand = "audit.AuditBasicSelectPrimaryKeyNames";
            DbCommand cmdCust = db.GetStoredProcCommand(strCommand);
            retDataTable = db.ExecuteDataSet(cmdCust).Tables[0];
            return retDataTable;
        }
    }
}
