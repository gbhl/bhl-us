
// Generated 1/5/2021 3:26:52 PM
// Do not modify the contents of this code file.
// This abstract class __ScanningRequest is based upon dbo.ScanningRequest.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ScanningRequest : __ScanningRequest
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
	public abstract class __ScanningRequest : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ScanningRequest()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="scanningRequestID"></param>
		/// <param name="geminiIssueID"></param>
		/// <param name="title"></param>
		/// <param name="year"></param>
		/// <param name="type"></param>
		/// <param name="volume"></param>
		/// <param name="edition"></param>
		/// <param name="oCLC"></param>
		/// <param name="iSBN"></param>
		/// <param name="iSSN"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="language"></param>
		/// <param name="note"></param>
		/// <param name="creationDate"></param>
		public __ScanningRequest(int scanningRequestID, 
			int? geminiIssueID, 
			string title, 
			string year, 
			string type, 
			string volume, 
			string edition, 
			string oCLC, 
			string iSBN, 
			string iSSN, 
			string author, 
			string publisher, 
			string language, 
			string note, 
			DateTime creationDate) : this()
		{
			_ScanningRequestID = scanningRequestID;
			GeminiIssueID = geminiIssueID;
			Title = title;
			Year = year;
			Type = type;
			Volume = volume;
			Edition = edition;
			OCLC = oCLC;
			ISBN = iSBN;
			ISSN = iSSN;
			Author = author;
			Publisher = publisher;
			Language = language;
			Note = note;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ScanningRequest()
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
					case "ScanningRequestID" :
					{
						_ScanningRequestID = (int)column.Value;
						break;
					}
					case "GeminiIssueID" :
					{
						_GeminiIssueID = (int?)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "Type" :
					{
						_Type = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Edition" :
					{
						_Edition = (string)column.Value;
						break;
					}
					case "OCLC" :
					{
						_OCLC = (string)column.Value;
						break;
					}
					case "ISBN" :
					{
						_ISBN = (string)column.Value;
						break;
					}
					case "ISSN" :
					{
						_ISSN = (string)column.Value;
						break;
					}
					case "Author" :
					{
						_Author = (string)column.Value;
						break;
					}
					case "Publisher" :
					{
						_Publisher = (string)column.Value;
						break;
					}
					case "Language" :
					{
						_Language = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ScanningRequestID
		
		private int _ScanningRequestID = default(int);
		
		/// <summary>
		/// Column: ScanningRequestID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ScanningRequestID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ScanningRequestID
		{
			get
			{
				return _ScanningRequestID;
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
		
		#endregion ScanningRequestID
		
		#region GeminiIssueID
		
		private int? _GeminiIssueID = null;
		
		/// <summary>
		/// Column: GeminiIssueID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("GeminiIssueID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsNullable=true)]
		public int? GeminiIssueID
		{
			get
			{
				return _GeminiIssueID;
			}
			set
			{
				if (_GeminiIssueID != value)
				{
					_GeminiIssueID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GeminiIssueID
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=500)]
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
		
		#region Year
		
		private string _Year = string.Empty;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=20)]
		public string Year
		{
			get
			{
				return _Year;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Year != value)
				{
					_Year = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Year
		
		#region Type
		
		private string _Type = string.Empty;
		
		/// <summary>
		/// Column: Type;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Type", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=20)]
		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Type != value)
				{
					_Type = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Type
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Edition
		
		private string _Edition = string.Empty;
		
		/// <summary>
		/// Column: Edition;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Edition", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=100)]
		public string Edition
		{
			get
			{
				return _Edition;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Edition != value)
				{
					_Edition = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Edition
		
		#region OCLC
		
		private string _OCLC = string.Empty;
		
		/// <summary>
		/// Column: OCLC;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("OCLC", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=30)]
		public string OCLC
		{
			get
			{
				return _OCLC;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_OCLC != value)
				{
					_OCLC = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OCLC
		
		#region ISBN
		
		private string _ISBN = string.Empty;
		
		/// <summary>
		/// Column: ISBN;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("ISBN", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=30)]
		public string ISBN
		{
			get
			{
				return _ISBN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_ISBN != value)
				{
					_ISBN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ISBN
		
		#region ISSN
		
		private string _ISSN = string.Empty;
		
		/// <summary>
		/// Column: ISSN;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("ISSN", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=30)]
		public string ISSN
		{
			get
			{
				return _ISSN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_ISSN != value)
				{
					_ISSN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ISSN
		
		#region Author
		
		private string _Author = string.Empty;
		
		/// <summary>
		/// Column: Author;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Author", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=200)]
		public string Author
		{
			get
			{
				return _Author;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Author != value)
				{
					_Author = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Author
		
		#region Publisher
		
		private string _Publisher = string.Empty;
		
		/// <summary>
		/// Column: Publisher;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Publisher", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=200)]
		public string Publisher
		{
			get
			{
				return _Publisher;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Publisher != value)
				{
					_Publisher = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Publisher
		
		#region Language
		
		private string _Language = string.Empty;
		
		/// <summary>
		/// Column: Language;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Language", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=20)]
		public string Language
		{
			get
			{
				return _Language;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Language != value)
				{
					_Language = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Language
		
		#region Note
		
		private string _Note = string.Empty;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=1073741823)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=15)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ScanningRequest"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ScanningRequest"/>, 
		/// returns an instance of <see cref="__ScanningRequest"/>; otherwise returns null.</returns>
		public static new __ScanningRequest FromArray(byte[] byteArray)
		{
			__ScanningRequest o = null;
			
			try
			{
				o = (__ScanningRequest) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ScanningRequest"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ScanningRequest"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ScanningRequest)
			{
				__ScanningRequest o = (__ScanningRequest) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ScanningRequestID == ScanningRequestID &&
					o.GeminiIssueID == GeminiIssueID &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.Type) == GetComparisonString(Type) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Edition) == GetComparisonString(Edition) &&
					GetComparisonString(o.OCLC) == GetComparisonString(OCLC) &&
					GetComparisonString(o.ISBN) == GetComparisonString(ISBN) &&
					GetComparisonString(o.ISSN) == GetComparisonString(ISSN) &&
					GetComparisonString(o.Author) == GetComparisonString(Author) &&
					GetComparisonString(o.Publisher) == GetComparisonString(Publisher) &&
					GetComparisonString(o.Language) == GetComparisonString(Language) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.CreationDate == CreationDate 
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
				throw new ArgumentException("Argument is not of type __ScanningRequest");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ScanningRequest"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ScanningRequest"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ScanningRequest a, __ScanningRequest b)
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
		/// <param name="a">The first <see cref="__ScanningRequest"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ScanningRequest"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ScanningRequest a, __ScanningRequest b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ScanningRequest"/> object to compare with the current <see cref="__ScanningRequest"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ScanningRequest))
			{
				return false;
			}
			
			return this == (__ScanningRequest) obj;
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
		/// list.Sort(SortOrder.Ascending, __ScanningRequest.SortColumn.ScanningRequestID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ScanningRequestID = "ScanningRequestID";	
			public const string GeminiIssueID = "GeminiIssueID";	
			public const string Title = "Title";	
			public const string Year = "Year";	
			public const string Type = "Type";	
			public const string Volume = "Volume";	
			public const string Edition = "Edition";	
			public const string OCLC = "OCLC";	
			public const string ISBN = "ISBN";	
			public const string ISSN = "ISSN";	
			public const string Author = "Author";	
			public const string Publisher = "Publisher";	
			public const string Language = "Language";	
			public const string Note = "Note";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

