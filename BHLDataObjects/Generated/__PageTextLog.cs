
// Generated 9/14/2018 6:43:09 PM
// Do not modify the contents of this code file.
// This abstract class __PageTextLog is based upon dbo.PageTextLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PageTextLog : __PageTextLog
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
	public abstract class __PageTextLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageTextLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageTextLogID"></param>
		/// <param name="pageID"></param>
		/// <param name="textSource"></param>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="creationDate"></param>
		/// <param name="creationUserID"></param>
		public __PageTextLog(int pageTextLogID, 
			int pageID, 
			string textSource, 
			int? textImportBatchFileID, 
			DateTime creationDate, 
			int creationUserID) : this()
		{
			_PageTextLogID = pageTextLogID;
			PageID = pageID;
			TextSource = textSource;
			TextImportBatchFileID = textImportBatchFileID;
			CreationDate = creationDate;
			CreationUserID = creationUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PageTextLog()
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
					case "PageTextLogID" :
					{
						_PageTextLogID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "TextSource" :
					{
						_TextSource = (string)column.Value;
						break;
					}
					case "TextImportBatchFileID" :
					{
						_TextImportBatchFileID = (int?)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PageTextLogID
		
		private int _PageTextLogID = default(int);
		
		/// <summary>
		/// Column: PageTextLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageTextLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageTextLogID
		{
			get
			{
				return _PageTextLogID;
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
		
		#endregion PageTextLogID
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int PageID
		{
			get
			{
				return _PageID;
			}
			set
			{
				if (_PageID != value)
				{
					_PageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageID
		
		#region TextSource
		
		private string _TextSource = string.Empty;
		
		/// <summary>
		/// Column: TextSource;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("TextSource", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string TextSource
		{
			get
			{
				return _TextSource;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_TextSource != value)
				{
					_TextSource = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TextSource
		
		#region TextImportBatchFileID
		
		private int? _TextImportBatchFileID = null;
		
		/// <summary>
		/// Column: TextImportBatchFileID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TextImportBatchFileID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? TextImportBatchFileID
		{
			get
			{
				return _TextImportBatchFileID;
			}
			set
			{
				if (_TextImportBatchFileID != value)
				{
					_TextImportBatchFileID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TextImportBatchFileID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PageTextLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageTextLog"/>, 
		/// returns an instance of <see cref="__PageTextLog"/>; otherwise returns null.</returns>
		public static new __PageTextLog FromArray(byte[] byteArray)
		{
			__PageTextLog o = null;
			
			try
			{
				o = (__PageTextLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageTextLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageTextLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageTextLog)
			{
				__PageTextLog o = (__PageTextLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageTextLogID == PageTextLogID &&
					o.PageID == PageID &&
					GetComparisonString(o.TextSource) == GetComparisonString(TextSource) &&
					o.TextImportBatchFileID == TextImportBatchFileID &&
					o.CreationDate == CreationDate &&
					o.CreationUserID == CreationUserID 
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
				throw new ArgumentException("Argument is not of type __PageTextLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageTextLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageTextLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageTextLog a, __PageTextLog b)
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
		/// <param name="a">The first <see cref="__PageTextLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageTextLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageTextLog a, __PageTextLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageTextLog"/> object to compare with the current <see cref="__PageTextLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageTextLog))
			{
				return false;
			}
			
			return this == (__PageTextLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageTextLog.SortColumn.PageTextLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageTextLogID = "PageTextLogID";	
			public const string PageID = "PageID";	
			public const string TextSource = "TextSource";	
			public const string TextImportBatchFileID = "TextImportBatchFileID";	
			public const string CreationDate = "CreationDate";	
			public const string CreationUserID = "CreationUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

