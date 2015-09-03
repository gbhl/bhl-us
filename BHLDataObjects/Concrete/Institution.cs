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
		public Institution( string institutionCode, string institutionName, string note, string institutionUrl, bool bHLMemberLibrary )
			: base( institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary )
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
                }
            }

            base.SetValues(row);
        }
    }
}
