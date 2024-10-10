using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class CreatorTitle : ISetValues
	{
        private int _titleID;
        private string _fullTitle;
        private string _roleDescription;
        private string _relationship;
        private string _titleOfWork;
        private short? _StartYear = null;
        private short? _EndYear = null;

        public int TitleID
		{
			get { return this._titleID; }
			set { this._titleID = value; }
		}

		public string FullTitle
		{
			get { return this._fullTitle; }
			set { this._fullTitle = value; }
		}

		public string RoleDescription
		{
			get { return this._roleDescription; }
			set { this._roleDescription = value; }
		}

        public string Relationship
        {
            get { return this._relationship; }
            set { this._relationship = value; }
        }

        public string TitleOfWork
        {
            get { return this._titleOfWork; }
            set { this._titleOfWork = value; }
        }

        public short? StartYear
        {
            get { return _StartYear; }
            set { this._StartYear = value; }
        }
        public short? EndYear
        {
            get { return _EndYear; }
            set { this._EndYear = value; }
        }

        public string PublicationDates
        {
            get
            {
                string dates = 
                    (StartYear == null ? string.Empty : StartYear.ToString()) + 
                    (EndYear == null ? string.Empty : "-" + EndYear.ToString());
                return dates;
            }
        }

        #region ISetValues Members

        public void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
					case "TitleID":
						{
							TitleID = (int)column.Value;
							break;
						}
					case "FullTitle":
						{
							FullTitle = (string)column.Value;
							break;
						}
					case "RoleDescription":
						{
							RoleDescription = (string)column.Value;
							break;
						}
                    case "Relationship":
                        {
                            Relationship = (string)column.Value;
                            break;
                        }
                    case "TitleOfWork":
                        {
                            TitleOfWork = (string)column.Value;
                            break;
                        }
                    case "StartYear":
                        {
                            _StartYear = (short?)column.Value;
                            break;
                        }
                    case "EndYear":
                        {
                            _EndYear = (short?)column.Value;
                            break;
                        }
                }
            }
		}

		#endregion
	}
}
