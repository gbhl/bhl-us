
// Generated 1/5/2021 3:25:27 PM
// Do not modify the contents of this code file.
// This abstract class __InstitutionRole is based upon dbo.InstitutionRole.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class InstitutionRole : __InstitutionRole
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
	public abstract class __InstitutionRole : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __InstitutionRole()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="institutionRoleID"></param>
		/// <param name="institutionRoleName"></param>
		/// <param name="institutionRoleLabel"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __InstitutionRole(int institutionRoleID, 
			string institutionRoleName, 
			string institutionRoleLabel, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_InstitutionRoleID = institutionRoleID;
			InstitutionRoleName = institutionRoleName;
			InstitutionRoleLabel = institutionRoleLabel;
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
		~__InstitutionRole()
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
					case "InstitutionRoleID" :
					{
						_InstitutionRoleID = (int)column.Value;
						break;
					}
					case "InstitutionRoleName" :
					{
						_InstitutionRoleName = (string)column.Value;
						break;
					}
					case "InstitutionRoleLabel" :
					{
						_InstitutionRoleLabel = (string)column.Value;
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
		
		#region InstitutionRoleID
		
		private int _InstitutionRoleID = default(int);
		
		/// <summary>
		/// Column: InstitutionRoleID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("InstitutionRoleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int InstitutionRoleID
		{
			get
			{
				return _InstitutionRoleID;
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
		
		#endregion InstitutionRoleID
		
		#region InstitutionRoleName
		
		private string _InstitutionRoleName = string.Empty;
		
		/// <summary>
		/// Column: InstitutionRoleName;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("InstitutionRoleName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=100)]
		public string InstitutionRoleName
		{
			get
			{
				return _InstitutionRoleName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_InstitutionRoleName != value)
				{
					_InstitutionRoleName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionRoleName
		
		#region InstitutionRoleLabel
		
		private string _InstitutionRoleLabel = string.Empty;
		
		/// <summary>
		/// Column: InstitutionRoleLabel;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("InstitutionRoleLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string InstitutionRoleLabel
		{
			get
			{
				return _InstitutionRoleLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_InstitutionRoleLabel != value)
				{
					_InstitutionRoleLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionRoleLabel
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__InstitutionRole"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__InstitutionRole"/>, 
		/// returns an instance of <see cref="__InstitutionRole"/>; otherwise returns null.</returns>
		public static new __InstitutionRole FromArray(byte[] byteArray)
		{
			__InstitutionRole o = null;
			
			try
			{
				o = (__InstitutionRole) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__InstitutionRole"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__InstitutionRole"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __InstitutionRole)
			{
				__InstitutionRole o = (__InstitutionRole) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.InstitutionRoleID == InstitutionRoleID &&
					GetComparisonString(o.InstitutionRoleName) == GetComparisonString(InstitutionRoleName) &&
					GetComparisonString(o.InstitutionRoleLabel) == GetComparisonString(InstitutionRoleLabel) &&
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
				throw new ArgumentException("Argument is not of type __InstitutionRole");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__InstitutionRole"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionRole"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__InstitutionRole a, __InstitutionRole b)
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
		/// <param name="a">The first <see cref="__InstitutionRole"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionRole"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__InstitutionRole a, __InstitutionRole b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__InstitutionRole"/> object to compare with the current <see cref="__InstitutionRole"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __InstitutionRole))
			{
				return false;
			}
			
			return this == (__InstitutionRole) obj;
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
		/// list.Sort(SortOrder.Ascending, __InstitutionRole.SortColumn.InstitutionRoleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string InstitutionRoleID = "InstitutionRoleID";	
			public const string InstitutionRoleName = "InstitutionRoleName";	
			public const string InstitutionRoleLabel = "InstitutionRoleLabel";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

