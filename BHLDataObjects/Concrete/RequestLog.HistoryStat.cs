using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DataObjects
{
    public class HistoryStat : ISetValues
    {
        private int _month;
        private int _day;
        private int _year;
        private int _numRequests;

        public int Month
        {
            get { return this._month; }
            set { this._month = value; }
        }

        public int Day
        {
            get { return this._day; }
            set { this._day = value; }
        }

        public int Year
        {
            get { return this._year; }
            set { this._year = value; }
        }

        public int NumRequests
        {
            get { return this._numRequests; }
            set { this._numRequests = value; }
        }


        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {

            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Month":
                        _month = (int)column.Value;
                        break;
                    case "Day":
                        _day = (int)column.Value;
                        break;
                    case "Year":
                        _year = (int)column.Value;
                        break;
                    case "NumRequests":
                        _numRequests = (int)column.Value;
                        break;
                }
            }
        }

        #endregion
    }
}
