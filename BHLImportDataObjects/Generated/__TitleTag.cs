
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This abstract class __TitleTag is based upon TitleTag.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class TitleTag : __TitleTag
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
	public abstract class __TitleTag : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleTag()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleTagID"></param>
		/// <param name="tagText"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __TitleTag(int titleTagID, 
			string tagText, 
			string importKey, 
			int importStatusID, 
			int? importSourceID, 
			string marcDataFieldTag, 
			string marcSubFieldCode, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_TitleTagID = titleTagID;
			TagText = tagText;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			MarcDataFieldTag = marcDataFieldTag;
			MarcSubFieldCode = marcSubFieldCode;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleTag()
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
					case "TitleTagID" :
					{
						_TitleTagID = (int)column.Value;
						break;
					}
					case "TagText" :
					{
						_TagText = (string)column.Value;
						break;
					}
					case "ImportKey" :
					{
						_ImportKey = (string)column.Value;
						break;
					}
					case "ImportStatusID" :
					{
						_ImportStatusID = (int)column.Value;
						break;
					}
					case "ImportSourceID" :
					{
						_ImportSourceID = (int?)column.Value;
						break;
					}
					case "MarcDataFieldTag" :
					{
						_MarcDataFieldTag = (string)column.Value;
						break;
					}
					case "MarcSubFieldCode" :
					{
						_MarcSubFieldCode = (string)column.Value;
						break;
					}
					case "ExternalCreationDate" :
					{
						_ExternalCreationDate = (DateTime?)column.Value;
						break;
					}
					case "ExternalLastModifiedDate" :
					{
						_ExternalLastModifiedDate = (DateTime?)column.Value;
						break;
					}
					case "ProductionDate" :
					{
						_ProductionDate = (DateTime?)column.Value;
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
		
		#region TitleTagID
		
		private int _TitleTagID = default(int);
		
		/// <summary>
		/// Column: TitleTagID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleTagID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleTagID
		{
			get
			{
				return _TitleTagID;
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
		
		#endregion TitleTagID
		
		#region TagText
		
		private string _TagText = string.Empty;
		
		/// <summary>
		/// Column: TagText;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("TagText", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string TagText
		{
			get
			{
				return _TagText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_TagText != value)
				{
					_TagText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TagText
		
		#region ImportKey
		
		private string _ImportKey = string.Empty;
		
		/// <summary>
		/// Column: ImportKey;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ImportKey", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string ImportKey
		{
			get
			{
				return _ImportKey;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ImportKey != value)
				{
					_ImportKey = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportKey
		
		#region ImportStatusID
		
		private int _ImportStatusID = default(int);
		
		/// <summary>
		/// Column: ImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportStatusID
		{
			get
			{
				return _ImportStatusID;
			}
			set
			{
				if (_ImportStatusID != value)
				{
					_ImportStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportStatusID
		
		#region ImportSourceID
		
		private int? _ImportSourceID = null;
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ImportSourceID
		{
			get
			{
				return _ImportSourceID;
			}
			set
			{
				if (_ImportSourceID != value)
				{
					_ImportSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportSourceID
		
		#region MarcDataFieldTag
		
		private string _MarcDataFieldTag = null;
		
		/// <summary>
		/// Column: MarcDataFieldTag;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("MarcDataFieldTag", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50, IsNullable=true)]
		public string MarcDataFieldTag
		{
			get
			{
				return _MarcDataFieldTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MarcDataFieldTag != value)
				{
					_MarcDataFieldTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcDataFieldTag
		
		#region MarcSubFieldCode
		
		private string _MarcSubFieldCode = null;
		
		/// <summary>
		/// Column: MarcSubFieldCode;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("MarcSubFieldCode", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50, IsNullable=true)]
		public string MarcSubFieldCode
		{
			get
			{
				return _MarcSubFieldCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MarcSubFieldCode != value)
				{
					_MarcSubFieldCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcSubFieldCode
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=8, IsNullable=true)]
		public DateTime? ExternalCreationDate
		{
			get
			{
				return _ExternalCreationDate;
			}
			set
			{
				if (_ExternalCreationDate != value)
				{
					_ExternalCreationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalCreationDate
		
		#region ExternalLastModifiedDate
		
		private DateTime? _ExternalLastModifiedDate = null;
		
		/// <summary>
		/// Column: ExternalLastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=9, IsNullable=true)]
		public DateTime? ExternalLastModifiedDate
		{
			get
			{
				return _ExternalLastModifiedDate;
			}
			set
			{
				if (_ExternalLastModifiedDate != value)
				{
					_ExternalLastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalLastModifiedDate
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=10, IsNullable=true)]
		public DateTime? ProductionDate
		{
			get
			{
				return _ProductionDate;
			}
			set
			{
				if (_ProductionDate != value)
				{
					_ProductionDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionDate
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=11)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__TitleTag"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleTag"/>, 
		/// returns an instance of <see cref="__TitleTag"/>; otherwise returns null.</returns>
		public static new __TitleTag FromArray(byte[] byteArray)
		{
			__TitleTag o = null;
			
			try
			{
				o = (__TitleTag) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleTag"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleTag"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleTag)
			{
				__TitleTag o = (__TitleTag) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleTagID == TitleTagID &&
					GetComparisonString(o.TagText) == GetComparisonString(TagText) &&
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.MarcDataFieldTag) == GetComparisonString(MarcDataFieldTag) &&
					GetComparisonString(o.MarcSubFieldCode) == GetComparisonString(MarcSubFieldCode) &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
					o.ProductionDate == ProductionDate &&
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
				throw new ArgumentException("Argument is not of type __TitleTag");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleTag"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleTag"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleTag a, __TitleTag b)
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
		/// <param name="a">The first <see cref="__TitleTag"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleTag"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleTag a, __TitleTag b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleTag"/> object to compare with the current <see cref="__TitleTag"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleTag))
			{
				return false;
			}
			
			return this == (__TitleTag) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleTag.SortColumn.TitleTagID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleTagID = "TitleTagID";	
			public const string TagText = "TagText";	
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string MarcDataFieldTag = "MarcDataFieldTag";	
			public const string MarcSubFieldCode = "MarcSubFieldCode";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
