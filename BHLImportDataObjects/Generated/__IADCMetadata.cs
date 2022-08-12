
// Generated 1/5/2021 2:13:40 PM
// Do not modify the contents of this code file.
// This abstract class __IADCMetadata is based upon dbo.IADCMetadata.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IADCMetadata : __IADCMetadata
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public abstract class __IADCMetadata : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IADCMetadata()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="dCMetadataID"></param>
		/// <param name="itemID"></param>
		/// <param name="dCElementName"></param>
		/// <param name="dCElementValue"></param>
		/// <param name="source"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IADCMetadata(int dCMetadataID, 
			int itemID, 
			string dCElementName, 
			string dCElementValue, 
			string source, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_DCMetadataID = dCMetadataID;
			ItemID = itemID;
			DCElementName = dCElementName;
			DCElementValue = dCElementValue;
			Source = source;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IADCMetadata()
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
					case "DCMetadataID" :
					{
						_DCMetadataID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "DCElementName" :
					{
						_DCElementName = (string)column.Value;
						break;
					}
					case "DCElementValue" :
					{
						_DCElementValue = (string)column.Value;
						break;
					}
					case "Source" :
					{
						_Source = (string)column.Value;
						break;
					}
					case "CreatedDate" :
					{
						_CreatedDate = (DateTime)column.Value;
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
		
		#region DCMetadataID
		
		private int _DCMetadataID = default(int);
		
		/// <summary>
		/// Column: DCMetadataID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("DCMetadataID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int DCMetadataID
		{
			get
			{
				return _DCMetadataID;
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
		
		#endregion DCMetadataID
		
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
		
		#region DCElementName
		
		private string _DCElementName = string.Empty;
		
		/// <summary>
		/// Column: DCElementName;
		/// DBMS data type: nvarchar(15);
		/// </summary>
		[ColumnDefinition("DCElementName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=15)]
		public string DCElementName
		{
			get
			{
				return _DCElementName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 15);
				if (_DCElementName != value)
				{
					_DCElementName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DCElementName
		
		#region DCElementValue
		
		private string _DCElementValue = string.Empty;
		
		/// <summary>
		/// Column: DCElementValue;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("DCElementValue", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=500)]
		public string DCElementValue
		{
			get
			{
				return _DCElementValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_DCElementValue != value)
				{
					_DCElementValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DCElementValue
		
		#region Source
		
		private string _Source = string.Empty;
		
		/// <summary>
		/// Column: Source;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Source", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Source != value)
				{
					_Source = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Source
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
		public DateTime CreatedDate
		{
			get
			{
				return _CreatedDate;
			}
			set
			{
				if (_CreatedDate != value)
				{
					_CreatedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatedDate
		
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__IADCMetadata"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IADCMetadata"/>, 
		/// returns an instance of <see cref="__IADCMetadata"/>; otherwise returns null.</returns>
		public static new __IADCMetadata FromArray(byte[] byteArray)
		{
			__IADCMetadata o = null;
			
			try
			{
				o = (__IADCMetadata) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IADCMetadata"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IADCMetadata"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IADCMetadata)
			{
				__IADCMetadata o = (__IADCMetadata) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DCMetadataID == DCMetadataID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.DCElementName) == GetComparisonString(DCElementName) &&
					GetComparisonString(o.DCElementValue) == GetComparisonString(DCElementValue) &&
					GetComparisonString(o.Source) == GetComparisonString(Source) &&
					o.CreatedDate == CreatedDate &&
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
				throw new ArgumentException("Argument is not of type __IADCMetadata");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IADCMetadata"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IADCMetadata"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IADCMetadata a, __IADCMetadata b)
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
		/// <param name="a">The first <see cref="__IADCMetadata"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IADCMetadata"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IADCMetadata a, __IADCMetadata b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IADCMetadata"/> object to compare with the current <see cref="__IADCMetadata"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IADCMetadata))
			{
				return false;
			}
			
			return this == (__IADCMetadata) obj;
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
		/// list.Sort(SortOrder.Ascending, __IADCMetadata.SortColumn.DCMetadataID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DCMetadataID = "DCMetadataID";	
			public const string ItemID = "ItemID";	
			public const string DCElementName = "DCElementName";	
			public const string DCElementValue = "DCElementValue";	
			public const string Source = "Source";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

