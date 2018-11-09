using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Institution : __Institution
    {
        #region Constructors

        /// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		public Institution( string institutionCode, string institutionName, string note, string institutionUrl, bool bHLMemberLibrary,
            DateTime? creationDate, DateTime? lastModifiedDate, int? creationUserID, int? lastModifiedUserID)
			: base( institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary,
                  creationDate, lastModifiedDate, creationUserID, lastModifiedUserID)
		{
		}

		public Institution()
		{
        }

        #endregion Constructors

        #region Properties

        private int _titleDOIs = 0;
        public int TitleDOIs
        {
            get { return _titleDOIs; }
            set { _titleDOIs = value; }
        }

        private int _segmentDOIs = 0;
        public int SegmentDOIs
        {
            get { return _segmentDOIs; }
            set { _segmentDOIs = value; }
        }

        private int _totalDOIs = 0;
        public int TotalDOIs
        {
            get { return _totalDOIs; }
            set { _totalDOIs = value; }
        }

        private int? _entityInstitutionID = null;
        public int? EntityInstitutionID
        {
            get { return _entityInstitutionID; }
            set { _entityInstitutionID = value; }
        }

        private string _institutionRoleName = string.Empty;
        public string InstitutionRoleName
        {
            get { return _institutionRoleName; }
            set { _institutionRoleName = value; }
        }

        private string _institutionRoleLabel = string.Empty;
        public string InstitutionRoleLabel
        {
            get { return _institutionRoleLabel; }
            set { _institutionRoleLabel = value; }
        }

        private string _url = string.Empty;
        /// <summary>
        /// Typically, this is a repository URL associated with a Title-Institution relationship
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        #endregion Properties

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleDOIs":
                        {
                            _titleDOIs = (int)column.Value;
                            break;
                        }
                    case "SegmentDOIs":
                        {
                            _segmentDOIs = (int)column.Value;
                            break;
                        }
                    case "TotalDOIs":
                        {
                            _totalDOIs = (int)column.Value;
                            break;
                        }
                    case "ItemInstitutionID":
                    case "SegmentInstitutionID":
                    case "TitleInstitutionID":
                        {
                            _entityInstitutionID = (int?)column.Value;
                            break;
                        }
                    case "InstitutionRoleName":
                        {
                            _institutionRoleName = (string)column.Value;
                            break;
                        }
                    case "InstitutionRoleLabel":
                        {
                            _institutionRoleLabel = (string)column.Value;
                            break;
                        }
                    case "Url":
                        {
                            _url = (string)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
