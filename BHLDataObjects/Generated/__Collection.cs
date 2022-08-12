
// Generated 1/5/2021 3:25:07 PM
// Do not modify the contents of this code file.
// This abstract class __Collection is based upon dbo.Collection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Collection : __Collection
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
	public abstract class __Collection : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Collection()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <param name="collectionDescription"></param>
		/// <param name="collectionURL"></param>
		/// <param name="htmlContent"></param>
		/// <param name="canContainTitles"></param>
		/// <param name="canContainItems"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="active"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="collectionTarget"></param>
		/// <param name="iTunesImageURL"></param>
		/// <param name="iTunesURL"></param>
		/// <param name="imageURL"></param>
		/// <param name="featured"></param>
		public __Collection(int collectionID, 
			string collectionName, 
			string collectionDescription, 
			string collectionURL, 
			string htmlContent, 
			short canContainTitles, 
			short canContainItems, 
			string institutionCode, 
			string languageCode, 
			short active, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			string collectionTarget, 
			string iTunesImageURL, 
			string iTunesURL, 
			string imageURL, 
			short featured) : this()
		{
			_CollectionID = collectionID;
			CollectionName = collectionName;
			CollectionDescription = collectionDescription;
			CollectionURL = collectionURL;
			HtmlContent = htmlContent;
			CanContainTitles = canContainTitles;
			CanContainItems = canContainItems;
			InstitutionCode = institutionCode;
			LanguageCode = languageCode;
			Active = active;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CollectionTarget = collectionTarget;
			ITunesImageURL = iTunesImageURL;
			ITunesURL = iTunesURL;
			ImageURL = imageURL;
			Featured = featured;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Collection()
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
					case "CollectionID" :
					{
						_CollectionID = (int)column.Value;
						break;
					}
					case "CollectionName" :
					{
						_CollectionName = (string)column.Value;
						break;
					}
					case "CollectionDescription" :
					{
						_CollectionDescription = (string)column.Value;
						break;
					}
					case "CollectionURL" :
					{
						_CollectionURL = (string)column.Value;
						break;
					}
					case "HtmlContent" :
					{
						_HtmlContent = (string)column.Value;
						break;
					}
					case "CanContainTitles" :
					{
						_CanContainTitles = (short)column.Value;
						break;
					}
					case "CanContainItems" :
					{
						_CanContainItems = (short)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "Active" :
					{
						_Active = (short)column.Value;
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
					case "CollectionTarget" :
					{
						_CollectionTarget = (string)column.Value;
						break;
					}
					case "ITunesImageURL" :
					{
						_ITunesImageURL = (string)column.Value;
						break;
					}
					case "ITunesURL" :
					{
						_ITunesURL = (string)column.Value;
						break;
					}
					case "ImageURL" :
					{
						_ImageURL = (string)column.Value;
						break;
					}
					case "Featured" :
					{
						_Featured = (short)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region CollectionID
		
		private int _CollectionID = default(int);
		
		/// <summary>
		/// Column: CollectionID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("CollectionID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int CollectionID
		{
			get
			{
				return _CollectionID;
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
		
		#endregion CollectionID
		
		#region CollectionName
		
		private string _CollectionName = string.Empty;
		
		/// <summary>
		/// Column: CollectionName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CollectionName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string CollectionName
		{
			get
			{
				return _CollectionName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CollectionName != value)
				{
					_CollectionName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionName
		
		#region CollectionDescription
		
		private string _CollectionDescription = string.Empty;
		
		/// <summary>
		/// Column: CollectionDescription;
		/// DBMS data type: nvarchar(4000);
		/// </summary>
		[ColumnDefinition("CollectionDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=4000)]
		public string CollectionDescription
		{
			get
			{
				return _CollectionDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_CollectionDescription != value)
				{
					_CollectionDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionDescription
		
		#region CollectionURL
		
		private string _CollectionURL = string.Empty;
		
		/// <summary>
		/// Column: CollectionURL;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CollectionURL", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string CollectionURL
		{
			get
			{
				return _CollectionURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CollectionURL != value)
				{
					_CollectionURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionURL
		
		#region HtmlContent
		
		private string _HtmlContent = string.Empty;
		
		/// <summary>
		/// Column: HtmlContent;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("HtmlContent", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string HtmlContent
		{
			get
			{
				return _HtmlContent;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_HtmlContent != value)
				{
					_HtmlContent = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HtmlContent
		
		#region CanContainTitles
		
		private short _CanContainTitles = default(short);
		
		/// <summary>
		/// Column: CanContainTitles;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("CanContainTitles", DbTargetType=SqlDbType.SmallInt, Ordinal=6, NumericPrecision=5)]
		public short CanContainTitles
		{
			get
			{
				return _CanContainTitles;
			}
			set
			{
				if (_CanContainTitles != value)
				{
					_CanContainTitles = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CanContainTitles
		
		#region CanContainItems
		
		private short _CanContainItems = default(short);
		
		/// <summary>
		/// Column: CanContainItems;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("CanContainItems", DbTargetType=SqlDbType.SmallInt, Ordinal=7, NumericPrecision=5)]
		public short CanContainItems
		{
			get
			{
				return _CanContainItems;
			}
			set
			{
				if (_CanContainItems != value)
				{
					_CanContainItems = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CanContainItems
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region LanguageCode
		
		private string _LanguageCode = null;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
		public string LanguageCode
		{
			get
			{
				return _LanguageCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_LanguageCode != value)
				{
					_LanguageCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LanguageCode
		
		#region Active
		
		private short _Active = default(short);
		
		/// <summary>
		/// Column: Active;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("Active", DbTargetType=SqlDbType.SmallInt, Ordinal=10, NumericPrecision=5)]
		public short Active
		{
			get
			{
				return _Active;
			}
			set
			{
				if (_Active != value)
				{
					_Active = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Active
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=11)]
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
		
		#region CollectionTarget
		
		private string _CollectionTarget = string.Empty;
		
		/// <summary>
		/// Column: CollectionTarget;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("CollectionTarget", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=30)]
		public string CollectionTarget
		{
			get
			{
				return _CollectionTarget;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_CollectionTarget != value)
				{
					_CollectionTarget = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionTarget
		
		#region ITunesImageURL
		
		private string _ITunesImageURL = string.Empty;
		
		/// <summary>
		/// Column: ITunesImageURL;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("ITunesImageURL", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=100)]
		public string ITunesImageURL
		{
			get
			{
				return _ITunesImageURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ITunesImageURL != value)
				{
					_ITunesImageURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ITunesImageURL
		
		#region ITunesURL
		
		private string _ITunesURL = string.Empty;
		
		/// <summary>
		/// Column: ITunesURL;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("ITunesURL", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=100)]
		public string ITunesURL
		{
			get
			{
				return _ITunesURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ITunesURL != value)
				{
					_ITunesURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ITunesURL
		
		#region ImageURL
		
		private string _ImageURL = string.Empty;
		
		/// <summary>
		/// Column: ImageURL;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("ImageURL", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=100)]
		public string ImageURL
		{
			get
			{
				return _ImageURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ImageURL != value)
				{
					_ImageURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImageURL
		
		#region Featured
		
		private short _Featured = default(short);
		
		/// <summary>
		/// Column: Featured;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("Featured", DbTargetType=SqlDbType.SmallInt, Ordinal=17, NumericPrecision=5)]
		public short Featured
		{
			get
			{
				return _Featured;
			}
			set
			{
				if (_Featured != value)
				{
					_Featured = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Featured
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Collection"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Collection"/>, 
		/// returns an instance of <see cref="__Collection"/>; otherwise returns null.</returns>
		public static new __Collection FromArray(byte[] byteArray)
		{
			__Collection o = null;
			
			try
			{
				o = (__Collection) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Collection"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Collection"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Collection)
			{
				__Collection o = (__Collection) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.CollectionID == CollectionID &&
					GetComparisonString(o.CollectionName) == GetComparisonString(CollectionName) &&
					GetComparisonString(o.CollectionDescription) == GetComparisonString(CollectionDescription) &&
					GetComparisonString(o.CollectionURL) == GetComparisonString(CollectionURL) &&
					GetComparisonString(o.HtmlContent) == GetComparisonString(HtmlContent) &&
					o.CanContainTitles == CanContainTitles &&
					o.CanContainItems == CanContainItems &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					o.Active == Active &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.CollectionTarget) == GetComparisonString(CollectionTarget) &&
					GetComparisonString(o.ITunesImageURL) == GetComparisonString(ITunesImageURL) &&
					GetComparisonString(o.ITunesURL) == GetComparisonString(ITunesURL) &&
					GetComparisonString(o.ImageURL) == GetComparisonString(ImageURL) &&
					o.Featured == Featured 
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
				throw new ArgumentException("Argument is not of type __Collection");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Collection"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Collection"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Collection a, __Collection b)
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
		/// <param name="a">The first <see cref="__Collection"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Collection"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Collection a, __Collection b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Collection"/> object to compare with the current <see cref="__Collection"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Collection))
			{
				return false;
			}
			
			return this == (__Collection) obj;
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
		/// list.Sort(SortOrder.Ascending, __Collection.SortColumn.CollectionID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string CollectionID = "CollectionID";	
			public const string CollectionName = "CollectionName";	
			public const string CollectionDescription = "CollectionDescription";	
			public const string CollectionURL = "CollectionURL";	
			public const string HtmlContent = "HtmlContent";	
			public const string CanContainTitles = "CanContainTitles";	
			public const string CanContainItems = "CanContainItems";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string LanguageCode = "LanguageCode";	
			public const string Active = "Active";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CollectionTarget = "CollectionTarget";	
			public const string ITunesImageURL = "ITunesImageURL";	
			public const string ITunesURL = "ITunesURL";	
			public const string ImageURL = "ImageURL";	
			public const string Featured = "Featured";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

