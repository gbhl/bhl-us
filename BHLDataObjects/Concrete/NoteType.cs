
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class NoteType : __NoteType
	{
        #region Properties

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
    }
}
