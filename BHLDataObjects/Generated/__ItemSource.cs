
// Generated 1/5/2021 3:25:50 PM
// Do not modify the contents of this code file.
// This abstract class __ItemSource is based upon dbo.ItemSource.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ItemSource : __ItemSource
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
	public abstract class __ItemSource : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ItemSource()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemSourceID"></param>
		/// <param name="sourceName"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="imageServerUrlFormat"></param>
		public __ItemSource(int itemSourceID, 
			string sourceName, 
			string downloadUrl, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			string imageServerUrlFormat) : this()
		{
			_ItemSourceID = itemSourceID;
			SourceName = sourceName;
			DownloadUrl = downloadUrl;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			ImageServerUrlFormat = imageServerUrlFormat;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ItemSource()
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
					case "ItemSourceID" :
					{
						_ItemSourceID = (int)column.Value;
						break;
					}
					case "SourceName" :
					{
						_SourceName = (string)column.Value;
						break;
					}
					case "DownloadUrl" :
					{
						_DownloadUrl = (string)column.Value;
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
					case "ImageServerUrlFormat" :
					{
						_ImageServerUrlFormat = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ItemSourceID
		
		private int _ItemSourceID = default(int);
		
		/// <summary>
		/// Column: ItemSourceID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ItemSourceID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ItemSourceID
		{
			get
			{
				return _ItemSourceID;
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
		
		#endregion ItemSourceID
		
		#region SourceName
		
		private string _SourceName = string.Empty;
		
		/// <summary>
		/// Column: SourceName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("SourceName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string SourceName
		{
			get
			{
				return _SourceName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SourceName != value)
				{
					_SourceName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SourceName
		
		#region DownloadUrl
		
		private string _DownloadUrl = string.Empty;
		
		/// <summary>
		/// Column: DownloadUrl;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("DownloadUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string DownloadUrl
		{
			get
			{
				return _DownloadUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_DownloadUrl != value)
				{
					_DownloadUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DownloadUrl
		
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
		
		#region ImageServerUrlFormat
		
		private string _ImageServerUrlFormat = string.Empty;
		
		/// <summary>
		/// Column: ImageServerUrlFormat;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("ImageServerUrlFormat", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
		public string ImageServerUrlFormat
		{
			get
			{
				return _ImageServerUrlFormat;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_ImageServerUrlFormat != value)
				{
					_ImageServerUrlFormat = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImageServerUrlFormat
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ItemSource"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ItemSource"/>, 
		/// returns an instance of <see cref="__ItemSource"/>; otherwise returns null.</returns>
		public static new __ItemSource FromArray(byte[] byteArray)
		{
			__ItemSource o = null;
			
			try
			{
				o = (__ItemSource) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ItemSource"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ItemSource"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ItemSource)
			{
				__ItemSource o = (__ItemSource) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemSourceID == ItemSourceID &&
					GetComparisonString(o.SourceName) == GetComparisonString(SourceName) &&
					GetComparisonString(o.DownloadUrl) == GetComparisonString(DownloadUrl) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.ImageServerUrlFormat) == GetComparisonString(ImageServerUrlFormat) 
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
				throw new ArgumentException("Argument is not of type __ItemSource");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ItemSource"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemSource"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ItemSource a, __ItemSource b)
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
		/// <param name="a">The first <see cref="__ItemSource"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemSource"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ItemSource a, __ItemSource b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ItemSource"/> object to compare with the current <see cref="__ItemSource"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ItemSource))
			{
				return false;
			}
			
			return this == (__ItemSource) obj;
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
		/// list.Sort(SortOrder.Ascending, __ItemSource.SortColumn.ItemSourceID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemSourceID = "ItemSourceID";	
			public const string SourceName = "SourceName";	
			public const string DownloadUrl = "DownloadUrl";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string ImageServerUrlFormat = "ImageServerUrlFormat";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

