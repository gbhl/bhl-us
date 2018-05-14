using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
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

        private string _noteSequence = null;
        public string NoteSequence
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
                            _noteSequence = (column.Value == null ? null : column.Value.ToString());
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
