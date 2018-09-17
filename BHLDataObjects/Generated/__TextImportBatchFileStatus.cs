
// Generated 9/17/2018 2:48:01 PM
// Do not modify the contents of this code file.
// This abstract class __TextImportBatchFileStatus is based upon txtimport.TextImportBatchFileStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TextImportBatchFileStatus : __TextImportBatchFileStatus
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
	public abstract class __TextImportBatchFileStatus : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TextImportBatchFileStatus()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TextImportBatchFileStatus(int textImportBatchFileStatusID, 
			string statusName, 
			string statusDescription, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			TextImportBatchFileStatusID = textImportBatchFileStatusID;
			StatusName = statusName;
			StatusDescription = statusDescription;
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
		~__TextImportBatchFileStatus()
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
					case "TextImportBatchFileStatusID" :
					{
						_TextImportBatchFileStatusID = (int)column.Value;
						break;
					}
					case "StatusName" :
					{
						_StatusName = (string)column.Value;
						break;
					}
					case "StatusDescription" :
					{
						_StatusDescription = (string)column.Value;
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
		
		#region TextImportBatchFileStatusID
		
		private int _TextImportBatchFileStatusID = default(int);
		
		/// <summary>
		/// Column: TextImportBatchFileStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TextImportBatchFileStatusID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int TextImportBatchFileStatusID
		{
			get
			{
				return _TextImportBatchFileStatusID;
			}
			set
			{
				if (_TextImportBatchFileStatusID != value)
				{
					_TextImportBatchFileStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TextImportBatchFileStatusID
		
		#region StatusName
		
		private string _StatusName = string.Empty;
		
		/// <summary>
		/// Column: StatusName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("StatusName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string StatusName
		{
			get
			{
				return _StatusName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_StatusName != value)
				{
					_StatusName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatusName
		
		#region StatusDescription
		
		private string _StatusDescription = string.Empty;
		
		/// <summary>
		/// Column: StatusDescription;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("StatusDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=500)]
		public string StatusDescription
		{
			get
			{
				return _StatusDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_StatusDescription != value)
				{
					_StatusDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatusDescription
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__TextImportBatchFileStatus"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TextImportBatchFileStatus"/>, 
		/// returns an instance of <see cref="__TextImportBatchFileStatus"/>; otherwise returns null.</returns>
		public static new __TextImportBatchFileStatus FromArray(byte[] byteArray)
		{
			__TextImportBatchFileStatus o = null;
			
			try
			{
				o = (__TextImportBatchFileStatus) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TextImportBatchFileStatus"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TextImportBatchFileStatus"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TextImportBatchFileStatus)
			{
				__TextImportBatchFileStatus o = (__TextImportBatchFileStatus) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TextImportBatchFileStatusID == TextImportBatchFileStatusID &&
					GetComparisonString(o.StatusName) == GetComparisonString(StatusName) &&
					GetComparisonString(o.StatusDescription) == GetComparisonString(StatusDescription) &&
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
				throw new ArgumentException("Argument is not of type __TextImportBatchFileStatus");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TextImportBatchFileStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TextImportBatchFileStatus"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TextImportBatchFileStatus a, __TextImportBatchFileStatus b)
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
		/// <param name="a">The first <see cref="__TextImportBatchFileStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TextImportBatchFileStatus"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TextImportBatchFileStatus a, __TextImportBatchFileStatus b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TextImportBatchFileStatus"/> object to compare with the current <see cref="__TextImportBatchFileStatus"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TextImportBatchFileStatus))
			{
				return false;
			}
			
			return this == (__TextImportBatchFileStatus) obj;
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
		/// list.Sort(SortOrder.Ascending, __TextImportBatchFileStatus.SortColumn.TextImportBatchFileStatusID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TextImportBatchFileStatusID = "TextImportBatchFileStatusID";	
			public const string StatusName = "StatusName";	
			public const string StatusDescription = "StatusDescription";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

