
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleNote : __TitleNote
	{
        #region Properties

        private string _noteTypeName;
        public string NoteTypeName
        {
            get { return _noteTypeName; }
            set { _noteTypeName = value; }
        }

        private string _noteTypeDisplay;
        public string NoteTypeDisplay
        {
            get { return _noteTypeDisplay; }
            set { _noteTypeDisplay = value; }
        }

        private string _marcDataFieldTag;
        public string MarcDataFieldTag
        {
            get { return _marcDataFieldTag; }
            set { _marcDataFieldTag = value; }
        }

        private string _marcIndicator1;
        public string MarcIndicator1
        {
            get { return _marcIndicator1; }
            set { _marcIndicator1 = value; }
        }

        public string NoteTypeNameExtended
        {
            get
            {
                string nameExtended = string.Empty;
                string marcPrefix = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.MarcDataFieldTag + this.MarcIndicator1))
                {
                    marcPrefix = "MARC " + this.MarcDataFieldTag.Trim();
                    if (!string.IsNullOrWhiteSpace(this.MarcIndicator1)) marcPrefix += " " + this.MarcIndicator1.Trim();
                    marcPrefix += ": ";
                }

                nameExtended = marcPrefix + this.NoteTypeName;

                if (!string.IsNullOrWhiteSpace(this.NoteTypeDisplay))
                {
                    nameExtended += " (Display as: " + this.NoteTypeDisplay.Trim() + ")";
                }

                return nameExtended.Trim();
            }
        }
        
        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "NoteTypeName":
                        {
                            this._noteTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "NoteTypeDisplay":
                        {
                            this._noteTypeDisplay = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "MarcDataFieldTag":
                        {
                            this._marcDataFieldTag = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "MarcIndicator1":
                        {
                            this._marcIndicator1 = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}
