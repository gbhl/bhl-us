
// Generated 1/5/2021 3:25:22 PM
// Do not modify the contents of this code file.
// This abstract class __InstitutionGroup is based upon dbo.InstitutionGroup.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class InstitutionGroup : __InstitutionGroup
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
	public abstract class __InstitutionGroup : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __InstitutionGroup()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionGroupName"></param>
		/// <param name="institutionGroupDescription"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __InstitutionGroup(int institutionGroupID, 
			string institutionGroupName, 
			string institutionGroupDescription, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_InstitutionGroupID = institutionGroupID;
			InstitutionGroupName = institutionGroupName;
			InstitutionGroupDescription = institutionGroupDescription;
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
		~__InstitutionGroup()
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
					case "InstitutionGroupID" :
					{
						_InstitutionGroupID = (int)column.Value;
						break;
					}
					case "InstitutionGroupName" :
					{
						_InstitutionGroupName = (string)column.Value;
						break;
					}
					case "InstitutionGroupDescription" :
					{
						_InstitutionGroupDescription = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime?)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime?)column.Value;
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
		
		#region InstitutionGroupID
		
		private int _InstitutionGroupID = default(int);
		
		/// <summary>
		/// Column: InstitutionGroupID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("InstitutionGroupID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int InstitutionGroupID
		{
			get
			{
				return _InstitutionGroupID;
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
		
		#endregion InstitutionGroupID
		
		#region InstitutionGroupName
		
		private string _InstitutionGroupName = string.Empty;
		
		/// <summary>
		/// Column: InstitutionGroupName;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("InstitutionGroupName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=300)]
		public string InstitutionGroupName
		{
			get
			{
				return _InstitutionGroupName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_InstitutionGroupName != value)
				{
					_InstitutionGroupName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionGroupName
		
		#region InstitutionGroupDescription
		
		private string _InstitutionGroupDescription = string.Empty;
		
		/// <summary>
		/// Column: InstitutionGroupDescription;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("InstitutionGroupDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1073741823)]
		public string InstitutionGroupDescription
		{
			get
			{
				return _InstitutionGroupDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_InstitutionGroupDescription != value)
				{
					_InstitutionGroupDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionGroupDescription
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4, IsNullable=true)]
		public DateTime? CreationDate
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
		
		private DateTime? _LastModifiedDate = null;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5, IsNullable=true)]
		public DateTime? LastModifiedDate
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
		/// Deserializes the byte array and returns an instance of <see cref="__InstitutionGroup"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__InstitutionGroup"/>, 
		/// returns an instance of <see cref="__InstitutionGroup"/>; otherwise returns null.</returns>
		public static new __InstitutionGroup FromArray(byte[] byteArray)
		{
			__InstitutionGroup o = null;
			
			try
			{
				o = (__InstitutionGroup) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__InstitutionGroup"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__InstitutionGroup"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __InstitutionGroup)
			{
				__InstitutionGroup o = (__InstitutionGroup) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.InstitutionGroupID == InstitutionGroupID &&
					GetComparisonString(o.InstitutionGroupName) == GetComparisonString(InstitutionGroupName) &&
					GetComparisonString(o.InstitutionGroupDescription) == GetComparisonString(InstitutionGroupDescription) &&
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
				throw new ArgumentException("Argument is not of type __InstitutionGroup");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__InstitutionGroup"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionGroup"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__InstitutionGroup a, __InstitutionGroup b)
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
		/// <param name="a">The first <see cref="__InstitutionGroup"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionGroup"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__InstitutionGroup a, __InstitutionGroup b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__InstitutionGroup"/> object to compare with the current <see cref="__InstitutionGroup"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __InstitutionGroup))
			{
				return false;
			}
			
			return this == (__InstitutionGroup) obj;
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
		/// list.Sort(SortOrder.Ascending, __InstitutionGroup.SortColumn.InstitutionGroupID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string InstitutionGroupID = "InstitutionGroupID";	
			public const string InstitutionGroupName = "InstitutionGroupName";	
			public const string InstitutionGroupDescription = "InstitutionGroupDescription";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

