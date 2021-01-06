
// Generated 1/5/2021 3:36:27 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotatedTitle is based upon annotation.AnnotatedTitle.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotatedTitle : __AnnotatedTitle
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
	public abstract class __AnnotatedTitle : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotatedTitle()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedTitleID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="date"></param>
		/// <param name="location"></param>
		/// <param name="isBeagleEra"></param>
		/// <param name="inscription"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotatedTitle(int annotatedTitleID, 
			int annotationSourceID, 
			int? titleID, 
			string externalIdentifier, 
			string author, 
			string title, 
			string edition, 
			string volume, 
			string publicationDetails, 
			string date, 
			string location, 
			string isBeagleEra, 
			string inscription, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotatedTitleID = annotatedTitleID;
			AnnotationSourceID = annotationSourceID;
			TitleID = titleID;
			ExternalIdentifier = externalIdentifier;
			Author = author;
			Title = title;
			Edition = edition;
			Volume = volume;
			PublicationDetails = publicationDetails;
			Date = date;
			Location = location;
			IsBeagleEra = isBeagleEra;
			Inscription = inscription;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotatedTitle()
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
					case "AnnotatedTitleID" :
					{
						_AnnotatedTitleID = (int)column.Value;
						break;
					}
					case "AnnotationSourceID" :
					{
						_AnnotationSourceID = (int)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int?)column.Value;
						break;
					}
					case "ExternalIdentifier" :
					{
						_ExternalIdentifier = (string)column.Value;
						break;
					}
					case "Author" :
					{
						_Author = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "Edition" :
					{
						_Edition = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "PublicationDetails" :
					{
						_PublicationDetails = (string)column.Value;
						break;
					}
					case "Date" :
					{
						_Date = (string)column.Value;
						break;
					}
					case "Location" :
					{
						_Location = (string)column.Value;
						break;
					}
					case "IsBeagleEra" :
					{
						_IsBeagleEra = (string)column.Value;
						break;
					}
					case "Inscription" :
					{
						_Inscription = (string)column.Value;
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
		
		#region AnnotatedTitleID
		
		private int _AnnotatedTitleID = default(int);
		
		/// <summary>
		/// Column: AnnotatedTitleID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotatedTitleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotatedTitleID
		{
			get
			{
				return _AnnotatedTitleID;
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
		
		#endregion AnnotatedTitleID
		
		#region AnnotationSourceID
		
		private int _AnnotationSourceID = default(int);
		
		/// <summary>
		/// Column: AnnotationSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationSourceID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotationSourceID
		{
			get
			{
				return _AnnotationSourceID;
			}
			set
			{
				if (_AnnotationSourceID != value)
				{
					_AnnotationSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationSourceID
		
		#region TitleID
		
		private int? _TitleID = null;
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
		public int? TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region ExternalIdentifier
		
		private string _ExternalIdentifier = string.Empty;
		
		/// <summary>
		/// Column: ExternalIdentifier;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ExternalIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string ExternalIdentifier
		{
			get
			{
				return _ExternalIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ExternalIdentifier != value)
				{
					_ExternalIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalIdentifier
		
		#region Author
		
		private string _Author = string.Empty;
		
		/// <summary>
		/// Column: Author;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Author", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string Author
		{
			get
			{
				return _Author;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Author != value)
				{
					_Author = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Author
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region Edition
		
		private string _Edition = string.Empty;
		
		/// <summary>
		/// Column: Edition;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Edition", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50)]
		public string Edition
		{
			get
			{
				return _Edition;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Edition != value)
				{
					_Edition = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Edition
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region PublicationDetails
		
		private string _PublicationDetails = string.Empty;
		
		/// <summary>
		/// Column: PublicationDetails;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("PublicationDetails", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=100)]
		public string PublicationDetails
		{
			get
			{
				return _PublicationDetails;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_PublicationDetails != value)
				{
					_PublicationDetails = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationDetails
		
		#region Date
		
		private string _Date = string.Empty;
		
		/// <summary>
		/// Column: Date;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Date", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=20)]
		public string Date
		{
			get
			{
				return _Date;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Date != value)
				{
					_Date = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Date
		
		#region Location
		
		private string _Location = string.Empty;
		
		/// <summary>
		/// Column: Location;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Location", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=50)]
		public string Location
		{
			get
			{
				return _Location;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Location != value)
				{
					_Location = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Location
		
		#region IsBeagleEra
		
		private string _IsBeagleEra = string.Empty;
		
		/// <summary>
		/// Column: IsBeagleEra;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("IsBeagleEra", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=200)]
		public string IsBeagleEra
		{
			get
			{
				return _IsBeagleEra;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_IsBeagleEra != value)
				{
					_IsBeagleEra = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsBeagleEra
		
		#region Inscription
		
		private string _Inscription = string.Empty;
		
		/// <summary>
		/// Column: Inscription;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Inscription", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=200)]
		public string Inscription
		{
			get
			{
				return _Inscription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Inscription != value)
				{
					_Inscription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Inscription
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=14)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=15)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotatedTitle"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotatedTitle"/>, 
		/// returns an instance of <see cref="__AnnotatedTitle"/>; otherwise returns null.</returns>
		public static new __AnnotatedTitle FromArray(byte[] byteArray)
		{
			__AnnotatedTitle o = null;
			
			try
			{
				o = (__AnnotatedTitle) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotatedTitle"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotatedTitle"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotatedTitle)
			{
				__AnnotatedTitle o = (__AnnotatedTitle) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedTitleID == AnnotatedTitleID &&
					o.AnnotationSourceID == AnnotationSourceID &&
					o.TitleID == TitleID &&
					GetComparisonString(o.ExternalIdentifier) == GetComparisonString(ExternalIdentifier) &&
					GetComparisonString(o.Author) == GetComparisonString(Author) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.Edition) == GetComparisonString(Edition) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.PublicationDetails) == GetComparisonString(PublicationDetails) &&
					GetComparisonString(o.Date) == GetComparisonString(Date) &&
					GetComparisonString(o.Location) == GetComparisonString(Location) &&
					GetComparisonString(o.IsBeagleEra) == GetComparisonString(IsBeagleEra) &&
					GetComparisonString(o.Inscription) == GetComparisonString(Inscription) &&
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
				throw new ArgumentException("Argument is not of type __AnnotatedTitle");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotatedTitle"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedTitle"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotatedTitle a, __AnnotatedTitle b)
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
		/// <param name="a">The first <see cref="__AnnotatedTitle"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedTitle"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotatedTitle a, __AnnotatedTitle b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotatedTitle"/> object to compare with the current <see cref="__AnnotatedTitle"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotatedTitle))
			{
				return false;
			}
			
			return this == (__AnnotatedTitle) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotatedTitle.SortColumn.AnnotatedTitleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedTitleID = "AnnotatedTitleID";	
			public const string AnnotationSourceID = "AnnotationSourceID";	
			public const string TitleID = "TitleID";	
			public const string ExternalIdentifier = "ExternalIdentifier";	
			public const string Author = "Author";	
			public const string Title = "Title";	
			public const string Edition = "Edition";	
			public const string Volume = "Volume";	
			public const string PublicationDetails = "PublicationDetails";	
			public const string Date = "Date";	
			public const string Location = "Location";	
			public const string IsBeagleEra = "IsBeagleEra";	
			public const string Inscription = "Inscription";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

