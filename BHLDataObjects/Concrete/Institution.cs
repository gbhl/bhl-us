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

        private int _titleMinted = 0;
        public int TitleMinted { get => _titleMinted; set => _titleMinted = value; }
        private int _titleAcquired = 0;
        public int TitleAcquired { get => _titleAcquired; set => _titleAcquired = value; }
        private int _titleNonBHL = 0;
        public int TitleNonBHL { get => _titleNonBHL; set => _titleNonBHL = value; }

        private int _titleTotalDOIs = 0;
        public int TitleTotalDOIs
        {
            get { return _titleTotalDOIs; }
            set { _titleTotalDOIs = value; }
        }

        private int _segmentMinted = 0;
        public int SegmentMinted { get => _segmentMinted; set => _segmentMinted = value; }
        private int _segmentAcquired = 0;
        public int SegmentAcquired { get => _segmentAcquired; set => _segmentAcquired = value; }
        private int _segmentNonBHL = 0;
        public int SegmentNonBHL { get => _segmentNonBHL; set => _segmentNonBHL = value; }

        private int _segmentTotalDOIs = 0;
        public int SegmentTotalDOIs
        {
            get { return _segmentTotalDOIs; }
            set { _segmentTotalDOIs = value; }
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

        public string _institutionGroups = string.Empty;
        public string InstitutionGroups
        {
            get { return _institutionGroups; }
            set { _institutionGroups = value; }
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
                    case "TitleMinted":
                        {
                            _titleMinted = (int)column.Value;
                            break; 
                        }
                    case "TitleAcquired":
                        {
                            _titleAcquired = (int)column.Value;
                            break;
                        }
                    case "TitleNonBHL":
                        {
                            _titleNonBHL = (int)column.Value;
                            break;
                        }
                    case "TitleTotalDOIs":
                        {
                            _titleTotalDOIs = (int)column.Value;
                            break;
                        }
                    case "SegmentMinted":
                        {
                            _segmentMinted = (int)column.Value;
                            break;
                        }
                    case "SegmentAcquired":
                        {
                            _segmentAcquired = (int)column.Value;
                            break;
                        }
                    case "SegmentNonBHL":
                        {
                            _segmentNonBHL = (int)column.Value;
                            break;
                        }
                    case "SegmentTotalDOIs":
                        {
                            _segmentTotalDOIs = (int)column.Value;
                            break;
                        }
                    case "TotalDOIs":
                        {
                            _totalDOIs = (int)column.Value;
                            break;
                        }
                    case "ItemInstitutionID":
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
                    case "InstitutionGroups":
                        {
                            _institutionGroups = (string)column.Value;
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
