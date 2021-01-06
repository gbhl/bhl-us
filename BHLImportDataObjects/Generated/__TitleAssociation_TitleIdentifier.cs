
// Generated 1/5/2021 2:18:50 PM
// Do not modify the contents of this code file.
// This abstract class __TitleAssociation_TitleIdentifier is based upon dbo.TitleAssociation_TitleIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class TitleAssociation_TitleIdentifier : __TitleAssociation_TitleIdentifier
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
	public abstract class __TitleAssociation_TitleIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleAssociation_TitleIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		public __TitleAssociation_TitleIdentifier(int titleAssociation_TitleIdentifierID, 
			string importKey, 
			int importStatusID, 
			int importSourceID, 
			string mARCTag, 
			string mARCIndicator2, 
			string title, 
			string section, 
			string volume, 
			string identifierName, 
			string identifierValue, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate, 
			string heading, 
			string publication, 
			string relationship) : this()
		{
			_TitleAssociation_TitleIdentifierID = titleAssociation_TitleIdentifierID;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			MARCTag = mARCTag;
			MARCIndicator2 = mARCIndicator2;
			Title = title;
			Section = section;
			Volume = volume;
			IdentifierName = identifierName;
			IdentifierValue = identifierValue;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
			Heading = heading;
			Publication = publication;
			Relationship = relationship;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleAssociation_TitleIdentifier()
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
					case "TitleAssociation_TitleIdentifierID" :
					{
						_TitleAssociation_TitleIdentifierID = (int)column.Value;
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
						_ImportSourceID = (int)column.Value;
						break;
					}
					case "MARCTag" :
					{
						_MARCTag = (string)column.Value;
						break;
					}
					case "MARCIndicator2" :
					{
						_MARCIndicator2 = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "Section" :
					{
						_Section = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "IdentifierName" :
					{
						_IdentifierName = (string)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
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
					case "Heading" :
					{
						_Heading = (string)column.Value;
						break;
					}
					case "Publication" :
					{
						_Publication = (string)column.Value;
						break;
					}
					case "Relationship" :
					{
						_Relationship = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region TitleAssociation_TitleIdentifierID
		
		private int _TitleAssociation_TitleIdentifierID = default(int);
		
		/// <summary>
		/// Column: TitleAssociation_TitleIdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleAssociation_TitleIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleAssociation_TitleIdentifierID
		{
			get
			{
				return _TitleAssociation_TitleIdentifierID;
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
		
		#endregion TitleAssociation_TitleIdentifierID
		
		#region ImportKey
		
		private string _ImportKey = string.Empty;
		
		/// <summary>
		/// Column: ImportKey;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ImportKey", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
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
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
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
		
		private int _ImportSourceID = default(int);
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportSourceID
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
		
		#region MARCTag
		
		private string _MARCTag = string.Empty;
		
		/// <summary>
		/// Column: MARCTag;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("MARCTag", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=20)]
		public string MARCTag
		{
			get
			{
				return _MARCTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_MARCTag != value)
				{
					_MARCTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCTag
		
		#region MARCIndicator2
		
		private string _MARCIndicator2 = string.Empty;
		
		/// <summary>
		/// Column: MARCIndicator2;
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("MARCIndicator2", DbTargetType=SqlDbType.NChar, Ordinal=6, CharacterMaxLength=1)]
		public string MARCIndicator2
		{
			get
			{
				return _MARCIndicator2;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_MARCIndicator2 != value)
				{
					_MARCIndicator2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCIndicator2
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=500)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region Section
		
		private string _Section = string.Empty;
		
		/// <summary>
		/// Column: Section;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Section", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=500)]
		public string Section
		{
			get
			{
				return _Section;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Section != value)
				{
					_Section = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Section
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=500)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region IdentifierName
		
		private string _IdentifierName = string.Empty;
		
		/// <summary>
		/// Column: IdentifierName;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierName", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=40)]
		public string IdentifierName
		{
			get
			{
				return _IdentifierName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierName != value)
				{
					_IdentifierName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierName
		
		#region IdentifierValue
		
		private string _IdentifierValue = string.Empty;
		
		/// <summary>
		/// Column: IdentifierValue;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("IdentifierValue", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=125)]
		public string IdentifierValue
		{
			get
			{
				return _IdentifierValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_IdentifierValue != value)
				{
					_IdentifierValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierValue
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=12, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=13)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=14)]
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
		
		#region Heading
		
		private string _Heading = string.Empty;
		
		/// <summary>
		/// Column: Heading;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Heading", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=500)]
		public string Heading
		{
			get
			{
				return _Heading;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Heading != value)
				{
					_Heading = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Heading
		
		#region Publication
		
		private string _Publication = string.Empty;
		
		/// <summary>
		/// Column: Publication;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Publication", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=500)]
		public string Publication
		{
			get
			{
				return _Publication;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Publication != value)
				{
					_Publication = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Publication
		
		#region Relationship
		
		private string _Relationship = string.Empty;
		
		/// <summary>
		/// Column: Relationship;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Relationship", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=500)]
		public string Relationship
		{
			get
			{
				return _Relationship;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Relationship != value)
				{
					_Relationship = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Relationship
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleAssociation_TitleIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleAssociation_TitleIdentifier"/>, 
		/// returns an instance of <see cref="__TitleAssociation_TitleIdentifier"/>; otherwise returns null.</returns>
		public static new __TitleAssociation_TitleIdentifier FromArray(byte[] byteArray)
		{
			__TitleAssociation_TitleIdentifier o = null;
			
			try
			{
				o = (__TitleAssociation_TitleIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleAssociation_TitleIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleAssociation_TitleIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleAssociation_TitleIdentifier)
			{
				__TitleAssociation_TitleIdentifier o = (__TitleAssociation_TitleIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleAssociation_TitleIdentifierID == TitleAssociation_TitleIdentifierID &&
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.MARCTag) == GetComparisonString(MARCTag) &&
					GetComparisonString(o.MARCIndicator2) == GetComparisonString(MARCIndicator2) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.Section) == GetComparisonString(Section) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.IdentifierName) == GetComparisonString(IdentifierName) &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
					o.ProductionDate == ProductionDate &&
					o.CreatedDate == CreatedDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.Heading) == GetComparisonString(Heading) &&
					GetComparisonString(o.Publication) == GetComparisonString(Publication) &&
					GetComparisonString(o.Relationship) == GetComparisonString(Relationship) 
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
				throw new ArgumentException("Argument is not of type __TitleAssociation_TitleIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleAssociation_TitleIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAssociation_TitleIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleAssociation_TitleIdentifier a, __TitleAssociation_TitleIdentifier b)
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
		/// <param name="a">The first <see cref="__TitleAssociation_TitleIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAssociation_TitleIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleAssociation_TitleIdentifier a, __TitleAssociation_TitleIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleAssociation_TitleIdentifier"/> object to compare with the current <see cref="__TitleAssociation_TitleIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleAssociation_TitleIdentifier))
			{
				return false;
			}
			
			return this == (__TitleAssociation_TitleIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleAssociation_TitleIdentifier.SortColumn.TitleAssociation_TitleIdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleAssociation_TitleIdentifierID = "TitleAssociation_TitleIdentifierID";	
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string MARCTag = "MARCTag";	
			public const string MARCIndicator2 = "MARCIndicator2";	
			public const string Title = "Title";	
			public const string Section = "Section";	
			public const string Volume = "Volume";	
			public const string IdentifierName = "IdentifierName";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string Heading = "Heading";	
			public const string Publication = "Publication";	
			public const string Relationship = "Relationship";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

