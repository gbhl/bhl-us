
// Generated 1/5/2021 3:24:58 PM
// Do not modify the contents of this code file.
// This abstract class __AuthorRole is based upon dbo.AuthorRole.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AuthorRole : __AuthorRole
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
	public abstract class __AuthorRole : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AuthorRole()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="authorRoleID"></param>
		/// <param name="roleDescription"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __AuthorRole(int authorRoleID, 
			string roleDescription, 
			string mARCDataFieldTag, 
			DateTime creationDate, 
			DateTime lastModifedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			AuthorRoleID = authorRoleID;
			RoleDescription = roleDescription;
			MARCDataFieldTag = mARCDataFieldTag;
			CreationDate = creationDate;
			LastModifedDate = lastModifedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AuthorRole()
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
					case "AuthorRoleID" :
					{
						_AuthorRoleID = (int)column.Value;
						break;
					}
					case "RoleDescription" :
					{
						_RoleDescription = (string)column.Value;
						break;
					}
					case "MARCDataFieldTag" :
					{
						_MARCDataFieldTag = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "LastModifedDate" :
					{
						_LastModifedDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region AuthorRoleID
		
		private int _AuthorRoleID = default(int);
		
		/// <summary>
		/// Column: AuthorRoleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AuthorRoleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AuthorRoleID
		{
			get
			{
				return _AuthorRoleID;
			}
			set
			{
				if (_AuthorRoleID != value)
				{
					_AuthorRoleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorRoleID
		
		#region RoleDescription
		
		private string _RoleDescription = string.Empty;
		
		/// <summary>
		/// Column: RoleDescription;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("RoleDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=255)]
		public string RoleDescription
		{
			get
			{
				return _RoleDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_RoleDescription != value)
				{
					_RoleDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RoleDescription
		
		#region MARCDataFieldTag
		
		private string _MARCDataFieldTag = string.Empty;
		
		/// <summary>
		/// Column: MARCDataFieldTag;
		/// DBMS data type: nvarchar(3);
		/// </summary>
		[ColumnDefinition("MARCDataFieldTag", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=3)]
		public string MARCDataFieldTag
		{
			get
			{
				return _MARCDataFieldTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 3);
				if (_MARCDataFieldTag != value)
				{
					_MARCDataFieldTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCDataFieldTag
		
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
		
		#region LastModifedDate
		
		private DateTime _LastModifedDate;
		
		/// <summary>
		/// Column: LastModifedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastModifedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
		public DateTime LastModifedDate
		{
			get
			{
				return _LastModifedDate;
			}
			set
			{
				if (_LastModifedDate != value)
				{
					_LastModifedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifedDate
		
		#region CreationUserID
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__AuthorRole"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AuthorRole"/>, 
		/// returns an instance of <see cref="__AuthorRole"/>; otherwise returns null.</returns>
		public static new __AuthorRole FromArray(byte[] byteArray)
		{
			__AuthorRole o = null;
			
			try
			{
				o = (__AuthorRole) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AuthorRole"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AuthorRole"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AuthorRole)
			{
				__AuthorRole o = (__AuthorRole) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AuthorRoleID == AuthorRoleID &&
					GetComparisonString(o.RoleDescription) == GetComparisonString(RoleDescription) &&
					GetComparisonString(o.MARCDataFieldTag) == GetComparisonString(MARCDataFieldTag) &&
					o.CreationDate == CreationDate &&
					o.LastModifedDate == LastModifedDate &&
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
				throw new ArgumentException("Argument is not of type __AuthorRole");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AuthorRole"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AuthorRole"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AuthorRole a, __AuthorRole b)
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
		/// <param name="a">The first <see cref="__AuthorRole"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AuthorRole"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AuthorRole a, __AuthorRole b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AuthorRole"/> object to compare with the current <see cref="__AuthorRole"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AuthorRole))
			{
				return false;
			}
			
			return this == (__AuthorRole) obj;
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
		/// list.Sort(SortOrder.Ascending, __AuthorRole.SortColumn.AuthorRoleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AuthorRoleID = "AuthorRoleID";	
			public const string RoleDescription = "RoleDescription";	
			public const string MARCDataFieldTag = "MARCDataFieldTag";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifedDate = "LastModifedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

