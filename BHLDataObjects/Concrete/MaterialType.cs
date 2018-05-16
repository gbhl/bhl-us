
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class MaterialType : __MaterialType
	{
        public string ExpandedLabel
        {
            get {
                return (
                    string.IsNullOrWhiteSpace(MaterialTypeName + MaterialTypeLabel) 
                    ? "" 
                    : string.Format("{0} ({1})", MaterialTypeName, MaterialTypeLabel)
                );
            }
        }
    }
}

