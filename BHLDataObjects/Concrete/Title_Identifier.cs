using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Title_Identifier : __Title_Identifier
	{
		private string _IdentifierName;

		public string IdentifierName
		{
			get { return this._IdentifierName; }
			set { this._IdentifierName = value; }
		}

		private string _identifierLabel;
		public string IdentifierLabel
		{
			get { return _identifierLabel; }
			set { _identifierLabel = value; }
		}

		private string _prefix;
		public string Prefix
		{
			get { return _prefix; }
			set { _prefix = value; }
		}

		public string IdentifierValueDisplay
		{
			get { return string.Format("{0}{1}", Prefix, IdentifierValue); }
		}

		#region Constructors

		public Title_Identifier()
		{
		}

		public Title_Identifier( int title_IdentifierID, int titleID, int identifierID, 
            string identifierValue, DateTime? creationDate, DateTime? lastModifiedDate )
			:
		base( title_IdentifierID, titleID, identifierID, identifierValue, creationDate, lastModifiedDate, null, null )
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
                    case "IdentifierLabel":
                        {
                            _identifierLabel = (string)column.Value;
                            break;
                        }
					case "Prefix":
                        {
							_prefix = (string)column.Value;
							break;
                        }
                }
			}

			base.SetValues( row );
		}
    }
}
