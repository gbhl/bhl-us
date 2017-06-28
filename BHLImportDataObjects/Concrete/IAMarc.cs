
#region Using

using CustomDataAccess;
using System;
using System.Data;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class IAMarc : __IAMarc
	{
        /// <summary>
        /// Override the base Leader property to avoid having the leading spaces 
        /// trimmed off of the value when the property is set.
        /// </summary>
        private string _Leader = string.Empty;

        [ColumnDefinition("Leader", DbTargetType = SqlDbType.VarChar, Ordinal = 3, CharacterMaxLength = 200)]
        public new string Leader
        {
            get
            {
                return _Leader;
            }
            set
            {
                if (value != null) value = value.Substring(0, (value.Length > 200 ? 200 : value.Length));
                if (_Leader != value)
                {
                    _Leader = value;
                    _IsDirty = true;
                }
            }
        }
    }
}
