
// Generated 1/5/2021 3:25:44 PM
// Do not modify the contents of this code file.
// This abstract class __ItemNameFileLog is based upon dbo.ItemNameFileLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ItemNameFileLog : __ItemNameFileLog
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
	public abstract class __ItemNameFileLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ItemNameFileLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="logID"></param>
		/// <param name="itemID"></param>
		/// <param name="doCreate"></param>
		/// <param name="doUpload"></param>
		/// <param name="lastCreateDate"></param>
		/// <param name="lastUploadDate"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __ItemNameFileLog(int logID, 
			int itemID, 
			bool doCreate, 
			bool doUpload, 
			DateTime? lastCreateDate, 
			DateTime? lastUploadDate, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_LogID = logID;
			ItemID = itemID;
			DoCreate = doCreate;
			DoUpload = doUpload;
			LastCreateDate = lastCreateDate;
			LastUploadDate = lastUploadDate;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ItemNameFileLog()
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
					case "LogID" :
					{
						_LogID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "DoCreate" :
					{
						_DoCreate = (bool)column.Value;
						break;
					}
					case "DoUpload" :
					{
						_DoUpload = (bool)column.Value;
						break;
					}
					case "LastCreateDate" :
					{
						_LastCreateDate = (DateTime?)column.Value;
						break;
					}
					case "LastUploadDate" :
					{
						_LastUploadDate = (DateTime?)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region LogID
		
		private int _LogID = default(int);
		
		/// <summary>
		/// Column: LogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("LogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int LogID
		{
			get
			{
				return _LogID;
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
		
		#endregion LogID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				if (_ItemID != value)
				{
					_ItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemID
		
		#region DoCreate
		
		private bool _DoCreate = false;
		
		/// <summary>
		/// Column: DoCreate;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("DoCreate", DbTargetType=SqlDbType.Bit, Ordinal=3)]
		public bool DoCreate
		{
			get
			{
				return _DoCreate;
			}
			set
			{
				if (_DoCreate != value)
				{
					_DoCreate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DoCreate
		
		#region DoUpload
		
		private bool _DoUpload = false;
		
		/// <summary>
		/// Column: DoUpload;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("DoUpload", DbTargetType=SqlDbType.Bit, Ordinal=4)]
		public bool DoUpload
		{
			get
			{
				return _DoUpload;
			}
			set
			{
				if (_DoUpload != value)
				{
					_DoUpload = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DoUpload
		
		#region LastCreateDate
		
		private DateTime? _LastCreateDate = null;
		
		/// <summary>
		/// Column: LastCreateDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastCreateDate", DbTargetType=SqlDbType.DateTime, Ordinal=5, IsNullable=true)]
		public DateTime? LastCreateDate
		{
			get
			{
				return _LastCreateDate;
			}
			set
			{
				if (_LastCreateDate != value)
				{
					_LastCreateDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastCreateDate
		
		#region LastUploadDate
		
		private DateTime? _LastUploadDate = null;
		
		/// <summary>
		/// Column: LastUploadDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastUploadDate", DbTargetType=SqlDbType.DateTime, Ordinal=6, IsNullable=true)]
		public DateTime? LastUploadDate
		{
			get
			{
				return _LastUploadDate;
			}
			set
			{
				if (_LastUploadDate != value)
				{
					_LastUploadDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastUploadDate
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ItemNameFileLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ItemNameFileLog"/>, 
		/// returns an instance of <see cref="__ItemNameFileLog"/>; otherwise returns null.</returns>
		public static new __ItemNameFileLog FromArray(byte[] byteArray)
		{
			__ItemNameFileLog o = null;
			
			try
			{
				o = (__ItemNameFileLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ItemNameFileLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ItemNameFileLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ItemNameFileLog)
			{
				__ItemNameFileLog o = (__ItemNameFileLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.LogID == LogID &&
					o.ItemID == ItemID &&
					o.DoCreate == DoCreate &&
					o.DoUpload == DoUpload &&
					o.LastCreateDate == LastCreateDate &&
					o.LastUploadDate == LastUploadDate &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate 
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
				throw new ArgumentException("Argument is not of type __ItemNameFileLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ItemNameFileLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemNameFileLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ItemNameFileLog a, __ItemNameFileLog b)
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
		/// <param name="a">The first <see cref="__ItemNameFileLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemNameFileLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ItemNameFileLog a, __ItemNameFileLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ItemNameFileLog"/> object to compare with the current <see cref="__ItemNameFileLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ItemNameFileLog))
			{
				return false;
			}
			
			return this == (__ItemNameFileLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __ItemNameFileLog.SortColumn.LogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string LogID = "LogID";	
			public const string ItemID = "ItemID";	
			public const string DoCreate = "DoCreate";	
			public const string DoUpload = "DoUpload";	
			public const string LastCreateDate = "LastCreateDate";	
			public const string LastUploadDate = "LastUploadDate";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

