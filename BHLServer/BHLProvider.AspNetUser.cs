﻿using CustomDataAccess;
using MOBOT.BHL.DAL;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<List<Tuple<string, object>>> AspNetUserSelectAll()
        {
            List<CustomDataRow> rows = new AspNetUserDAL().AspNetUserSelectAll(null, null);

            // Convert custom DAL object to generic values
            List<List<Tuple<string, object>>> returnRows = new List<List<Tuple<string, object>>>();
            foreach (CustomDataRow row in rows)
            {
                List<Tuple<string, object>> returnRow = new List<Tuple<string, object>>();
                foreach (CustomDataColumn column in row)
                {
                    object columnValue = (column.Name.EndsWith("Utc") && column.Value != null) ? ((DateTime)column.Value).ToLocalTime() : column.Value;
                    Tuple<string, object> returnCell = new Tuple<string, object>(column.Name, columnValue);

                    returnRow.Add(returnCell);
                }
                returnRows.Add(returnRow);
            }

            return (returnRows);
        }
    }
}