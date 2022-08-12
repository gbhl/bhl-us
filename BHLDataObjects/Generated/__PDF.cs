
// Generated 1/5/2021 3:26:45 PM
// Do not modify the contents of this code file.
// This abstract class __PDF is based upon dbo.PDF.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PDF : __PDF
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
	public abstract class __PDF : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PDF()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pdfID"></param>
		/// <param name="itemID"></param>
		/// <param name="fileLocation"></param>
		/// <param name="emailAddress"></param>
		/// <param name="shareWithEmailAddresses"></param>
		/// <param name="articleTitle"></param>
		/// <param name="articleCreators"></param>
		/// <param name="articleTags"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="imagesOnly"></param>
		/// <param name="fileUrl"></param>
		/// <param name="fileGenerationDate"></param>
		/// <param name="fileDeletionDate"></param>
		/// <param name="pdfStatusID"></param>
		/// <param name="numberImagesMissing"></param>
		/// <param name="numberOcrMissing"></param>
		/// <param name="comment"></param>
		public __PDF(int pdfID, 
			int itemID, 
			string fileLocation, 
			string emailAddress, 
			string shareWithEmailAddresses, 
			string articleTitle, 
			string articleCreators, 
			string articleTags, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			bool imagesOnly, 
			string fileUrl, 
			DateTime? fileGenerationDate, 
			DateTime? fileDeletionDate, 
			int pdfStatusID, 
			int numberImagesMissing, 
			int numberOcrMissing, 
			string comment) : this()
		{
			_PdfID = pdfID;
			ItemID = itemID;
			FileLocation = fileLocation;
			EmailAddress = emailAddress;
			ShareWithEmailAddresses = shareWithEmailAddresses;
			ArticleTitle = articleTitle;
			ArticleCreators = articleCreators;
			ArticleTags = articleTags;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			ImagesOnly = imagesOnly;
			FileUrl = fileUrl;
			FileGenerationDate = fileGenerationDate;
			FileDeletionDate = fileDeletionDate;
			PdfStatusID = pdfStatusID;
			NumberImagesMissing = numberImagesMissing;
			NumberOcrMissing = numberOcrMissing;
			Comment = comment;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PDF()
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
					case "PdfID" :
					{
						_PdfID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "FileLocation" :
					{
						_FileLocation = (string)column.Value;
						break;
					}
					case "EmailAddress" :
					{
						_EmailAddress = (string)column.Value;
						break;
					}
					case "ShareWithEmailAddresses" :
					{
						_ShareWithEmailAddresses = (string)column.Value;
						break;
					}
					case "ArticleTitle" :
					{
						_ArticleTitle = (string)column.Value;
						break;
					}
					case "ArticleCreators" :
					{
						_ArticleCreators = (string)column.Value;
						break;
					}
					case "ArticleTags" :
					{
						_ArticleTags = (string)column.Value;
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
					case "ImagesOnly" :
					{
						_ImagesOnly = (bool)column.Value;
						break;
					}
					case "FileUrl" :
					{
						_FileUrl = (string)column.Value;
						break;
					}
					case "FileGenerationDate" :
					{
						_FileGenerationDate = (DateTime?)column.Value;
						break;
					}
					case "FileDeletionDate" :
					{
						_FileDeletionDate = (DateTime?)column.Value;
						break;
					}
					case "PdfStatusID" :
					{
						_PdfStatusID = (int)column.Value;
						break;
					}
					case "NumberImagesMissing" :
					{
						_NumberImagesMissing = (int)column.Value;
						break;
					}
					case "NumberOcrMissing" :
					{
						_NumberOcrMissing = (int)column.Value;
						break;
					}
					case "Comment" :
					{
						_Comment = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PdfID
		
		private int _PdfID = default(int);
		
		/// <summary>
		/// Column: PdfID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PdfID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int PdfID
		{
			get
			{
				return _PdfID;
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
		
		#endregion PdfID
		
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
		
		#region FileLocation
		
		private string _FileLocation = string.Empty;
		
		/// <summary>
		/// Column: FileLocation;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("FileLocation", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=200)]
		public string FileLocation
		{
			get
			{
				return _FileLocation;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_FileLocation != value)
				{
					_FileLocation = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileLocation
		
		#region EmailAddress
		
		private string _EmailAddress = string.Empty;
		
		/// <summary>
		/// Column: EmailAddress;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("EmailAddress", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=200)]
		public string EmailAddress
		{
			get
			{
				return _EmailAddress;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_EmailAddress != value)
				{
					_EmailAddress = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EmailAddress
		
		#region ShareWithEmailAddresses
		
		private string _ShareWithEmailAddresses = string.Empty;
		
		/// <summary>
		/// Column: ShareWithEmailAddresses;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ShareWithEmailAddresses", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string ShareWithEmailAddresses
		{
			get
			{
				return _ShareWithEmailAddresses;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ShareWithEmailAddresses != value)
				{
					_ShareWithEmailAddresses = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ShareWithEmailAddresses
		
		#region ArticleTitle
		
		private string _ArticleTitle = string.Empty;
		
		/// <summary>
		/// Column: ArticleTitle;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ArticleTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=1073741823)]
		public string ArticleTitle
		{
			get
			{
				return _ArticleTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ArticleTitle != value)
				{
					_ArticleTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ArticleTitle
		
		#region ArticleCreators
		
		private string _ArticleCreators = string.Empty;
		
		/// <summary>
		/// Column: ArticleCreators;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ArticleCreators", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=1073741823)]
		public string ArticleCreators
		{
			get
			{
				return _ArticleCreators;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ArticleCreators != value)
				{
					_ArticleCreators = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ArticleCreators
		
		#region ArticleTags
		
		private string _ArticleTags = string.Empty;
		
		/// <summary>
		/// Column: ArticleTags;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ArticleTags", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1073741823)]
		public string ArticleTags
		{
			get
			{
				return _ArticleTags;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ArticleTags != value)
				{
					_ArticleTags = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ArticleTags
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		
		#region ImagesOnly
		
		private bool _ImagesOnly = false;
		
		/// <summary>
		/// Column: ImagesOnly;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("ImagesOnly", DbTargetType=SqlDbType.Bit, Ordinal=11)]
		public bool ImagesOnly
		{
			get
			{
				return _ImagesOnly;
			}
			set
			{
				if (_ImagesOnly != value)
				{
					_ImagesOnly = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImagesOnly
		
		#region FileUrl
		
		private string _FileUrl = string.Empty;
		
		/// <summary>
		/// Column: FileUrl;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("FileUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=200)]
		public string FileUrl
		{
			get
			{
				return _FileUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_FileUrl != value)
				{
					_FileUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileUrl
		
		#region FileGenerationDate
		
		private DateTime? _FileGenerationDate = null;
		
		/// <summary>
		/// Column: FileGenerationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("FileGenerationDate", DbTargetType=SqlDbType.DateTime, Ordinal=13, IsNullable=true)]
		public DateTime? FileGenerationDate
		{
			get
			{
				return _FileGenerationDate;
			}
			set
			{
				if (_FileGenerationDate != value)
				{
					_FileGenerationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileGenerationDate
		
		#region FileDeletionDate
		
		private DateTime? _FileDeletionDate = null;
		
		/// <summary>
		/// Column: FileDeletionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("FileDeletionDate", DbTargetType=SqlDbType.DateTime, Ordinal=14, IsNullable=true)]
		public DateTime? FileDeletionDate
		{
			get
			{
				return _FileDeletionDate;
			}
			set
			{
				if (_FileDeletionDate != value)
				{
					_FileDeletionDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileDeletionDate
		
		#region PdfStatusID
		
		private int _PdfStatusID = default(int);
		
		/// <summary>
		/// Column: PdfStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PdfStatusID", DbTargetType=SqlDbType.Int, Ordinal=15, NumericPrecision=10, IsInForeignKey=true)]
		public int PdfStatusID
		{
			get
			{
				return _PdfStatusID;
			}
			set
			{
				if (_PdfStatusID != value)
				{
					_PdfStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PdfStatusID
		
		#region NumberImagesMissing
		
		private int _NumberImagesMissing = default(int);
		
		/// <summary>
		/// Column: NumberImagesMissing;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("NumberImagesMissing", DbTargetType=SqlDbType.Int, Ordinal=16, NumericPrecision=10)]
		public int NumberImagesMissing
		{
			get
			{
				return _NumberImagesMissing;
			}
			set
			{
				if (_NumberImagesMissing != value)
				{
					_NumberImagesMissing = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NumberImagesMissing
		
		#region NumberOcrMissing
		
		private int _NumberOcrMissing = default(int);
		
		/// <summary>
		/// Column: NumberOcrMissing;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("NumberOcrMissing", DbTargetType=SqlDbType.Int, Ordinal=17, NumericPrecision=10)]
		public int NumberOcrMissing
		{
			get
			{
				return _NumberOcrMissing;
			}
			set
			{
				if (_NumberOcrMissing != value)
				{
					_NumberOcrMissing = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NumberOcrMissing
		
		#region Comment
		
		private string _Comment = string.Empty;
		
		/// <summary>
		/// Column: Comment;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Comment", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=1073741823)]
		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Comment != value)
				{
					_Comment = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Comment
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PDF"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PDF"/>, 
		/// returns an instance of <see cref="__PDF"/>; otherwise returns null.</returns>
		public static new __PDF FromArray(byte[] byteArray)
		{
			__PDF o = null;
			
			try
			{
				o = (__PDF) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PDF"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PDF"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PDF)
			{
				__PDF o = (__PDF) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PdfID == PdfID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.FileLocation) == GetComparisonString(FileLocation) &&
					GetComparisonString(o.EmailAddress) == GetComparisonString(EmailAddress) &&
					GetComparisonString(o.ShareWithEmailAddresses) == GetComparisonString(ShareWithEmailAddresses) &&
					GetComparisonString(o.ArticleTitle) == GetComparisonString(ArticleTitle) &&
					GetComparisonString(o.ArticleCreators) == GetComparisonString(ArticleCreators) &&
					GetComparisonString(o.ArticleTags) == GetComparisonString(ArticleTags) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.ImagesOnly == ImagesOnly &&
					GetComparisonString(o.FileUrl) == GetComparisonString(FileUrl) &&
					o.FileGenerationDate == FileGenerationDate &&
					o.FileDeletionDate == FileDeletionDate &&
					o.PdfStatusID == PdfStatusID &&
					o.NumberImagesMissing == NumberImagesMissing &&
					o.NumberOcrMissing == NumberOcrMissing &&
					GetComparisonString(o.Comment) == GetComparisonString(Comment) 
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
				throw new ArgumentException("Argument is not of type __PDF");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PDF"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDF"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PDF a, __PDF b)
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
		/// <param name="a">The first <see cref="__PDF"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDF"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PDF a, __PDF b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PDF"/> object to compare with the current <see cref="__PDF"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PDF))
			{
				return false;
			}
			
			return this == (__PDF) obj;
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
		/// list.Sort(SortOrder.Ascending, __PDF.SortColumn.PdfID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PdfID = "PdfID";	
			public const string ItemID = "ItemID";	
			public const string FileLocation = "FileLocation";	
			public const string EmailAddress = "EmailAddress";	
			public const string ShareWithEmailAddresses = "ShareWithEmailAddresses";	
			public const string ArticleTitle = "ArticleTitle";	
			public const string ArticleCreators = "ArticleCreators";	
			public const string ArticleTags = "ArticleTags";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string ImagesOnly = "ImagesOnly";	
			public const string FileUrl = "FileUrl";	
			public const string FileGenerationDate = "FileGenerationDate";	
			public const string FileDeletionDate = "FileDeletionDate";	
			public const string PdfStatusID = "PdfStatusID";	
			public const string NumberImagesMissing = "NumberImagesMissing";	
			public const string NumberOcrMissing = "NumberOcrMissing";	
			public const string Comment = "Comment";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

