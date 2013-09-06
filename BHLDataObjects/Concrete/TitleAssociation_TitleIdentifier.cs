
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleAssociation_TitleIdentifier : __TitleAssociation_TitleIdentifier
	{
		private string _IdentifierName;

		public string IdentifierName
		{
			get { return this._IdentifierName; }
			set { this._IdentifierName = value; }
		}


		#region Constructors

		public TitleAssociation_TitleIdentifier()
		{
		}

		public TitleAssociation_TitleIdentifier( int titleAssociation_TitleIdentifierID, 
            int titleAssociationID, int titleIdentifierID, string identifierValue, 
            DateTime? creationDate, DateTime? lastModifiedDate )
			:
            base(titleAssociation_TitleIdentifierID, titleAssociationID, titleIdentifierID, 
            identifierValue, creationDate, lastModifiedDate, null, null )
		{
		}

		#endregion Constructors

		public override void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
					case "IdentifierName":
						{
							_IdentifierName = (string)column.Value;
							break;
						}
				}
			}

			base.SetValues( row );
		}
	}
}
