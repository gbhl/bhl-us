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

        private int _numberOfDOIs = 0;
        public int NumberOfDOIs
        {
            get { return _numberOfDOIs; }
            set { _numberOfDOIs = value; }
        }

        #endregion Properties

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "NumberDOIs":
                        {
                            _numberOfDOIs = (int)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
