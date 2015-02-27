using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class TitleNote : DataObjectBase, ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TitleNote()
		{
		}

		#endregion Constructors

        #region Properties

        private string _noteText = string.Empty;
        public string NoteText
        {
            get { return _noteText; }
            set { _noteText = value; }
        }

        private short? _noteSequence = null;
        public short? NoteSequence
        {
            get { return _noteSequence; }
            set { _noteSequence = value; }
        }

        private string _noteTypeName;
        public string NoteTypeName
        {
            get { return _noteTypeName; }
            set { _noteTypeName = value; }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "NoteText":
                        {
                            _noteText = (string)column.Value;
                            break;
                        }
                    case "NoteSequence":
                        {
                            _noteSequence = (short?)column.Value;
                            break;
                        }
                    case "NoteTypeName":
                        {
                            this._noteTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
