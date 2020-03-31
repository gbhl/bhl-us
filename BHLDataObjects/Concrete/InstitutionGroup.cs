
#region Using

using CustomDataAccess;
using System;
using System.Collections.Generic;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class InstitutionGroup : __InstitutionGroup
	{
		public int NumberOfInstitutions { get; set; }
		public List<InstitutionGroupInstitution> Institutions { get; set; }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "NumberOfInstitutions")
                {
                    NumberOfInstitutions = Utility.ZeroIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
    }
}

