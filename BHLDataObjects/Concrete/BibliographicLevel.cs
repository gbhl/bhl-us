
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class BibliographicLevel : __BibliographicLevel
	{
        public string ExpandedLabel
        {
            get
            {
                return (
                    string.IsNullOrWhiteSpace(BibliographicLevelName + BibliographicLevelLabel)
                    ? ""
                    : string.Format("{0} ({1})", BibliographicLevelName, BibliographicLevelLabel)
                );
            }
        }
    }
}
