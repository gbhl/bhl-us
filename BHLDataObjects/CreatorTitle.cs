using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	public class CreatorTitle : ISetValues
	{
		int _titleID;
		string _fullTitle;
		string _roleDescription;
        string _relationship;
        string _titleOfWork;

		public int TitleID
		{
			get { return this._titleID; }
			set { this._titleID = value; }
		}

		public string FullTitle
		{
			get { return this._fullTitle; }
			set { this._fullTitle = value; }
		}

		public string RoleDescription
		{
			get { return this._roleDescription; }
			set { this._roleDescription = value; }
		}

        public string Relationship
        {
            get { return this._relationship; }
            set { this._relationship = value; }
        }

        public string TitleOfWork
        {
            get { return this._titleOfWork; }
            set { this._titleOfWork = value; }
        }

		
		#region ISetValues Members

		public void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
					case "TitleID":
						{
							TitleID = (int)column.Value;
							break;
						}
					case "FullTitle":
						{
							FullTitle = (string)column.Value;
							break;
						}
					case "RoleDescription":
						{
							RoleDescription = (string)column.Value;
							break;
						}
                    case "Relationship":
                        {
                            Relationship = (string)column.Value;
                            break;
                        }
                    case "TitleOfWork":
                        {
                            TitleOfWork = (string)column.Value;
                            break;
                        }
				}
			}
		}

		#endregion
	}
}
