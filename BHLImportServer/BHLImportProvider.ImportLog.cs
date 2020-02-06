using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<List<Tuple<string, object>>> ImportLogSelectRecent(int numLogs)
        {
            List<CustomDataRow> rows = new ImportLogDAL().ImportLogSelectRecent(null, null, numLogs);

            // Convert custom DAL object to generic values
            List<List<Tuple<string, object>>> returnRows = new List<List<Tuple<string, object>>>();
            foreach (CustomDataRow row in rows)
            {
                List<Tuple<string, object>> returnRow = new List<Tuple<string, object>>();
                foreach(CustomDataColumn column in row)
                {
                    Tuple<string, object> returnCell = new Tuple<string, object>(column.Name, column.Value);
                    returnRow.Add(returnCell);
                }
                returnRows.Add(returnRow);
            }

            return (returnRows);
        }
    }
}
