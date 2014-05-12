using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.IO;

namespace MOBOT.BHL.BHLAuditImport
{
    class DataAbstractLayer
    {
        private Database _myDatabase;

        public Database myDatabase
        {
            get { return _myDatabase; }
            set { _myDatabase = value; }
        }

        public DataAbstractLayer(string DatabaseName)
        {
            this.myDatabase = DatabaseFactory.CreateDatabase(DatabaseName);
        }

        public bool RunObjectDataCommmand(string sqlCommandString, string LogFileName)
        {
            bool retBool = false;
            Database db = this.myDatabase;
            DbCommand cmdSelect = db.GetSqlStringCommand(sqlCommandString);
            try
            {
                db.ExecuteNonQuery(cmdSelect);
                retBool = true;
            }
            catch (Exception ex)
            {
                StreamWriter writerVer = new StreamWriter(LogFileName, true);
                writerVer.WriteLine(String.Concat(DateTime.Now.ToShortTimeString(), @"DBError:", ex.Message));
                writerVer.WriteLine(sqlCommandString);
                writerVer.Flush();
                writerVer.Close();
                retBool = false;
            }

            return retBool;
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
                    strWhereClause = String.Format(" Where {0} = {1}", tmpPair.Key.ToString(), tmpPair.Value.ToString());
                }
                else
                {
                    strWhereClause = String.Format(" AND {0} = {1}", tmpPair.Key.ToString(), tmpPair.Value.ToString());
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

        public static bool RunDataCommmand(string sqlCommandString, string LogFileName)
        {
            bool retBool = false;
            Database db = DatabaseFactory.CreateDatabase("BHL");
            DbCommand cmdSelect = db.GetSqlStringCommand(sqlCommandString);
            try
            {
                db.ExecuteNonQuery(cmdSelect);
                retBool = true;
            }
            catch (Exception ex)
            {
                StreamWriter writerVer = new StreamWriter(LogFileName, true);
                writerVer.WriteLine(String.Concat(DateTime.Now.ToShortTimeString(), @"DBError:", ex.Message));
                writerVer.WriteLine(sqlCommandString);
                writerVer.Flush();
                writerVer.Close();
                retBool = false;
            }

            return retBool;
        }
    }
}
