using System;
using System.Xml.Serialization;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Name : DataObjectBase, ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Name()
		{
		}

		public Name(string nameBankID, 
			string nameConfirmed) : this()
		{
            _NameBankID = nameBankID;
            _NameConfirmed = nameConfirmed;
		}
		
		#endregion Constructors
		
		#region Set Values
		
		/// <summary>
		/// Set the property values of this instance from the specified <see cref="CustomDataRow"/>.
		/// </summary>
		public virtual void SetValues(CustomDataRow row)
		{
			foreach (CustomDataColumn column in row)
			{
				switch (column.Name)
				{
                    case "NameBankID":
					{
                        _NameBankID = Utility.EmptyIfNull(column.Value);
						break;
					}
                    case "EOLID":
                    {
                        _eolID = Utility.EmptyIfNull(column.Value);
                        break;
                    }
                    case "NameString":
                    {
                        _NameFound = Utility.EmptyIfNull(column.Value);
                        break;
                    }
                    case "ResolvedNameString":
					{
                        _NameConfirmed = Utility.EmptyIfNull(column.Value);
						break;
					}
                }
			}
		}
		
		#endregion Set Values
		
		#region Properties		

        private string  _NameBankID = string.Empty;
        public string NameBankID
        {
            get { return _NameBankID; }
            set { _NameBankID = value; }
        }

        private string _eolID = string.Empty;
        public string EOLID
        {
            get { return _eolID; }
            set { _eolID = value; }
        }

        private string _NameFound = null;
        public string NameFound
        {
            get { return _NameFound; }
            set 
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameFound = value; 
            }
        }


		private string _NameConfirmed= null;
        public string NameConfirmed
		{
			get { return _NameConfirmed; }
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
                _NameConfirmed = value;
			}
		}

        CustomGenericList<Title> _Titles;
        public CustomGenericList<Title> Titles
        {
            get { return _Titles; }
            set { _Titles = value; }
        }

		#endregion Properties
    }
}
