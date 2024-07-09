
// Generated 7/9/2024 11:03:14 AM
// Do not modify the contents of this code file.
// This abstract class __SegmentExternalResource is based upon dbo.SegmentExternalResource.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class SegmentExternalResource : __SegmentExternalResource
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
	public abstract class __SegmentExternalResource : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __SegmentExternalResource()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentExternalResourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="externalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __SegmentExternalResource(int segmentExternalResourceID, 
			int segmentID, 
			int externalResourceTypeID, 
			string urlText, 
			string url, 
			short sequenceOrder, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_SegmentExternalResourceID = segmentExternalResourceID;
			SegmentID = segmentID;
			ExternalResourceTypeID = externalResourceTypeID;
			UrlText = urlText;
			Url = url;
			SequenceOrder = sequenceOrder;
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
		~__SegmentExternalResource()
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
					case "SegmentExternalResourceID" :
					{
						_SegmentExternalResourceID = (int)column.Value;
						break;
					}
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
						break;
					}
					case "ExternalResourceTypeID" :
					{
						_ExternalResourceTypeID = (int)column.Value;
						break;
					}
					case "UrlText" :
					{
						_UrlText = (string)column.Value;
						break;
					}
					case "Url" :
					{
						_Url = (string)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (short)column.Value;
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
		
		#region SegmentExternalResourceID
		
		private int _SegmentExternalResourceID = default(int);
		
		/// <summary>
		/// Column: SegmentExternalResourceID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentExternalResourceID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int SegmentExternalResourceID
		{
			get
			{
				return _SegmentExternalResourceID;
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
		
		#endregion SegmentExternalResourceID
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
			}
			set
			{
				if (_SegmentID != value)
				{
					_SegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentID
		
		#region ExternalResourceTypeID
		
		private int _ExternalResourceTypeID = default(int);
		
		/// <summary>
		/// Column: ExternalResourceTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ExternalResourceTypeID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int ExternalResourceTypeID
		{
			get
			{
				return _ExternalResourceTypeID;
			}
			set
			{
				if (_ExternalResourceTypeID != value)
				{
					_ExternalResourceTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalResourceTypeID
		
		#region UrlText
		
		private string _UrlText = string.Empty;
		
		/// <summary>
		/// Column: UrlText;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("UrlText", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=100)]
		public string UrlText
		{
			get
			{
				return _UrlText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_UrlText != value)
				{
					_UrlText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion UrlText
		
		#region Url
		
		private string _Url = string.Empty;
		
		/// <summary>
		/// Column: Url;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Url", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=200)]
		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Url != value)
				{
					_Url = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Url
		
		#region SequenceOrder
		
		private short _SequenceOrder = default(short);
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.SmallInt, Ordinal=6, NumericPrecision=5)]
		public short SequenceOrder
		{
			get
			{
				return _SequenceOrder;
			}
			set
			{
				if (_SequenceOrder != value)
				{
					_SequenceOrder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceOrder
		
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__SegmentExternalResource"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__SegmentExternalResource"/>, 
		/// returns an instance of <see cref="__SegmentExternalResource"/>; otherwise returns null.</returns>
		public static new __SegmentExternalResource FromArray(byte[] byteArray)
		{
			__SegmentExternalResource o = null;
			
			try
			{
				o = (__SegmentExternalResource) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__SegmentExternalResource"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__SegmentExternalResource"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __SegmentExternalResource)
			{
				__SegmentExternalResource o = (__SegmentExternalResource) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentExternalResourceID == SegmentExternalResourceID &&
					o.SegmentID == SegmentID &&
					o.ExternalResourceTypeID == ExternalResourceTypeID &&
					GetComparisonString(o.UrlText) == GetComparisonString(UrlText) &&
					GetComparisonString(o.Url) == GetComparisonString(Url) &&
					o.SequenceOrder == SequenceOrder &&
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
				throw new ArgumentException("Argument is not of type __SegmentExternalResource");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__SegmentExternalResource"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentExternalResource"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__SegmentExternalResource a, __SegmentExternalResource b)
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
		/// <param name="a">The first <see cref="__SegmentExternalResource"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentExternalResource"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__SegmentExternalResource a, __SegmentExternalResource b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__SegmentExternalResource"/> object to compare with the current <see cref="__SegmentExternalResource"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __SegmentExternalResource))
			{
				return false;
			}
			
			return this == (__SegmentExternalResource) obj;
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
		/// list.Sort(SortOrder.Ascending, __SegmentExternalResource.SortColumn.SegmentExternalResourceID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentExternalResourceID = "SegmentExternalResourceID";	
			public const string SegmentID = "SegmentID";	
			public const string ExternalResourceTypeID = "ExternalResourceTypeID";	
			public const string UrlText = "UrlText";	
			public const string Url = "Url";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

