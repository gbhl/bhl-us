
// Generated 11/6/2018 1:46:48 PM
// Do not modify the contents of this code file.
// This abstract class __TitleInstitution is based upon dbo.TitleInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleInstitution : __TitleInstitution
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public abstract class __TitleInstitution : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleInstitution()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleInstitutionID"></param>
		/// <param name="titleID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="url"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TitleInstitution(int titleInstitutionID, 
			int titleID, 
			string institutionCode, 
			int institutionRoleID, 
			string url, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_TitleInstitutionID = titleInstitutionID;
			TitleID = titleID;
			InstitutionCode = institutionCode;
			InstitutionRoleID = institutionRoleID;
			Url = url;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleInstitution()
		{
		}
		
		#endregion Destructor
		
		#region Set Values
		
		/// <summary>
		/// Set the property values of this instance from the specified <see cref="CustomDataRow"/>.
		/// </summary>
		public virtual void SetValues(CustomDataRow row)
		{
			foreach (CustomDataColumn column in row)
			{
				switch (column.Name)
				{
					case "TitleInstitutionID" :
					{
						_TitleInstitutionID = (int)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "InstitutionRoleID" :
					{
						_InstitutionRoleID = (int)column.Value;
						break;
					}
					case "Url" :
					{
						_Url = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region TitleInstitutionID
		
		private int _TitleInstitutionID = default(int);
		
		/// <summary>
		/// Column: TitleInstitutionID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleInstitutionID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleInstitutionID
		{
			get
			{
				return _TitleInstitutionID;
			}
			set
			{
				// NOTE: This dummy setter provides a work-around for the following: Read-only properties cannot be exposed by XML Web Services
				// see http://support.microsoft.com/kb/313584
				// Cause: When an object is passed i.e. marshalled to or from a Web service, it must be serialized into an XML stream and then deserialized back into an object.
				// The XML Serializer cannot deserialize the XML back into an object because it cannot load the read-only properties. 
				// Thus the read-only properties are not exposed through the Web Services Description Language (WSDL). 
				// Because the Web service proxy is generated from the WSDL, the proxy also excludes any read-only properties.
			}
		}
		
		#endregion TitleInstitutionID
		
		#region TitleID
		
		private int _TitleID = default(int);
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region InstitutionCode
		
		private string _InstitutionCode = string.Empty;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=10, IsInForeignKey=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region InstitutionRoleID
		
		private int _InstitutionRoleID = default(int);
		
		/// <summary>
		/// Column: InstitutionRoleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("InstitutionRoleID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int InstitutionRoleID
		{
			get
			{
				return _InstitutionRoleID;
			}
			set
			{
				if (_InstitutionRoleID != value)
				{
					_InstitutionRoleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionRoleID
		
		#region Url
		
		private string _Url = string.Empty;
		
		/// <summary>
		/// Column: Url;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Url", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=500)]
		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Url != value)
				{
					_Url = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Url
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
		public DateTime CreationDate
		{
			get
			{
				return _CreationDate;
			}
			set
			{
				if (_CreationDate != value)
				{
					_CreationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationDate
		
		#region LastModifiedDate
		
		private DateTime _LastModifiedDate;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
		public DateTime LastModifiedDate
		{
			get
			{
				return _LastModifiedDate;
			}
			set
			{
				if (_LastModifiedDate != value)
				{
					_LastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedDate
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int CreationUserID
		{
			get
			{
				return _CreationUserID;
			}
			set
			{
				if (_CreationUserID != value)
				{
					_CreationUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationUserID
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int LastModifiedUserID
		{
			get
			{
				return _LastModifiedUserID;
			}
			set
			{
				if (_LastModifiedUserID != value)
				{
					_LastModifiedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedUserID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleInstitution"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleInstitution"/>, 
		/// returns an instance of <see cref="__TitleInstitution"/>; otherwise returns null.</returns>
		public static new __TitleInstitution FromArray(byte[] byteArray)
		{
			__TitleInstitution o = null;
			
			try
			{
				o = (__TitleInstitution) CustomObjectBase.FromArray(byteArray);
			}
			catch (Exception e)
			{
				throw e;
			}

			return o;
		}
		
		#endregion From Array serialization

		#region CompareTo
		
		/// <summary>
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleInstitution"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleInstitution"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleInstitution)
			{
				__TitleInstitution o = (__TitleInstitution) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleInstitutionID == TitleInstitutionID &&
					o.TitleID == TitleID &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					o.InstitutionRoleID == InstitutionRoleID &&
					GetComparisonString(o.Url) == GetComparisonString(Url) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID 
				)
				{
					o = null;
					return 0; // true
				}
				else
				{
					o = null;
					return -1; // false
				}
			}
			else
			{
				throw new ArgumentException("Argument is not of type __TitleInstitution");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleInstitution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleInstitution"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleInstitution a, __TitleInstitution b)
		{
			if (((Object) a) == null || ((Object) b) == null)
			{
				if (((Object) a) == null && ((Object) b) == null)
				{
					return true;
				}
			}
			else
			{
				int r = a.CompareTo(b);
				
				if (r == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Inequality operator (!=) returns false if its operands are equal, true otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleInstitution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleInstitution"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleInstitution a, __TitleInstitution b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleInstitution"/> object to compare with the current <see cref="__TitleInstitution"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleInstitution))
			{
				return false;
			}
			
			return this == (__TitleInstitution) obj;
		}
	
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance as an integer.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		#endregion Operators
		
		#region SortColumn
		
		/// <summary>
		/// Use when defining sort columns for a collection sort request.
		/// For example where list is a instance of <see cref="CustomGenericList">, 
		/// list.Sort(SortOrder.Ascending, __TitleInstitution.SortColumn.TitleInstitutionID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleInstitutionID = "TitleInstitutionID";	
			public const string TitleID = "TitleID";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string InstitutionRoleID = "InstitutionRoleID";	
			public const string Url = "Url";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

