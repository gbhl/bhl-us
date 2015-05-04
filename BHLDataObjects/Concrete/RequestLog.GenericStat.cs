using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DataObjects
{
	public class GenericStat : ISetValues
	{
		private string _stringColumn01;
        private string _stringColumn02;
		private int _intColumn01;
		private int? _intColumn02;

		public string StringColumn01
		{
			get { return this._stringColumn01; }
			set { this._stringColumn01 = value; }
		}

        public string StringColumn02
        {
            get { return this._stringColumn02; }
            set { this._stringColumn02 = value; }
        }

		public int IntColumn01
		{
			get { return this._intColumn01; }
			set { this._intColumn01 = value; }
		}

		#region ISetValues Members

		public void SetValues( CustomDataRow row )
		{

			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
					case "StringColumn01":
						_stringColumn01 = (string)column.Value;
						break;
                    case "StringColumn02":
                        _stringColumn02 = (string)column.Value;
                        break;
					case "IntColumn01":
						_intColumn01 = (int)column.Value;
						break;
					case "IntColumn02":
                        if(column.Value != null)
						    _intColumn02 = (int)column.Value;
						break;
				}
			}
		}

		#endregion

		public int? IntColumn02
		{
			get { return this._intColumn02; }
			set { this._intColumn02 = value; }
		}
	}
}
