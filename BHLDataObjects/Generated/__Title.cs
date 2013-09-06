
// Generated 8/3/2010 11:16:34 AM
// Do not modify the contents of this code file.
// This abstract class __Title is based upon Title.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Title : __Title
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
	public abstract class __Title : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Title()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="tropicosTitleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="note"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		public __Title(int titleID, 
			string mARCBibID, 
			string mARCLeader, 
			int? bibliographicLevelID, 
			int? tropicosTitleID, 
			int? redirectTitleID, 
			string fullTitle, 
			string shortTitle, 
			string uniformTitle, 
			string sortTitle, 
			string callNumber, 
			string publicationDetails, 
			short? startYear, 
			short? endYear, 
			string datafield_260_a, 
			string datafield_260_b, 
			string datafield_260_c, 
			string institutionCode, 
			string languageCode, 
			string titleDescription, 
			string tL2Author, 
			bool publishReady, 
			bool rareBooks, 
			string note, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID, 
			string originalCatalogingSource, 
			string editionStatement, 
			string currentPublicationFrequency, 
			string partNumber, 
			string partName) : this()
		{
			_TitleID = titleID;
			MARCBibID = mARCBibID;
			MARCLeader = mARCLeader;
			BibliographicLevelID = bibliographicLevelID;
			TropicosTitleID = tropicosTitleID;
			RedirectTitleID = redirectTitleID;
			FullTitle = fullTitle;
			ShortTitle = shortTitle;
			UniformTitle = uniformTitle;
			SortTitle = sortTitle;
			CallNumber = callNumber;
			PublicationDetails = publicationDetails;
			StartYear = startYear;
			EndYear = endYear;
			Datafield_260_a = datafield_260_a;
			Datafield_260_b = datafield_260_b;
			Datafield_260_c = datafield_260_c;
			InstitutionCode = institutionCode;
			LanguageCode = languageCode;
			TitleDescription = titleDescription;
			TL2Author = tL2Author;
			PublishReady = publishReady;
			RareBooks = rareBooks;
			Note = note;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
			OriginalCatalogingSource = originalCatalogingSource;
			EditionStatement = editionStatement;
			CurrentPublicationFrequency = currentPublicationFrequency;
			PartNumber = partNumber;
			PartName = partName;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Title()
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
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "MARCBibID" :
					{
						_MARCBibID = (string)column.Value;
						break;
					}
					case "MARCLeader" :
					{
						_MARCLeader = (string)column.Value;
						break;
					}
					case "BibliographicLevelID" :
					{
						_BibliographicLevelID = (int?)column.Value;
						break;
					}
					case "TropicosTitleID" :
					{
						_TropicosTitleID = (int?)column.Value;
						break;
					}
					case "RedirectTitleID" :
					{
						_RedirectTitleID = (int?)column.Value;
						break;
					}
					case "FullTitle" :
					{
						_FullTitle = (string)column.Value;
						break;
					}
					case "ShortTitle" :
					{
						_ShortTitle = (string)column.Value;
						break;
					}
					case "UniformTitle" :
					{
						_UniformTitle = (string)column.Value;
						break;
					}
					case "SortTitle" :
					{
						_SortTitle = (string)column.Value;
						break;
					}
					case "CallNumber" :
					{
						_CallNumber = (string)column.Value;
						break;
					}
					case "PublicationDetails" :
					{
						_PublicationDetails = (string)column.Value;
						break;
					}
					case "StartYear" :
					{
						_StartYear = (short?)column.Value;
						break;
					}
					case "EndYear" :
					{
						_EndYear = (short?)column.Value;
						break;
					}
					case "Datafield_260_a" :
					{
						_Datafield_260_a = (string)column.Value;
						break;
					}
					case "Datafield_260_b" :
					{
						_Datafield_260_b = (string)column.Value;
						break;
					}
					case "Datafield_260_c" :
					{
						_Datafield_260_c = (string)column.Value;
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
					case "TitleDescription" :
					{
						_TitleDescription = (string)column.Value;
						break;
					}
					case "TL2Author" :
					{
						_TL2Author = (string)column.Value;
						break;
					}
					case "PublishReady" :
					{
						_PublishReady = (bool)column.Value;
						break;
					}
					case "RareBooks" :
					{
						_RareBooks = (bool)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime?)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime?)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
					case "OriginalCatalogingSource" :
					{
						_OriginalCatalogingSource = (string)column.Value;
						break;
					}
					case "EditionStatement" :
					{
						_EditionStatement = (string)column.Value;
						break;
					}
					case "CurrentPublicationFrequency" :
					{
						_CurrentPublicationFrequency = (string)column.Value;
						break;
					}
					case "PartNumber" :
					{
						_PartNumber = (string)column.Value;
						break;
					}
					case "PartName" :
					{
						_PartName = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region TitleID
		
		private int _TitleID = default(int);
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int TitleID
		{
			get
			{
				return _TitleID;
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
		
		#endregion TitleID
		
		#region MARCBibID
		
		private string _MARCBibID = string.Empty;
		
		/// <summary>
		/// Column: MARCBibID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("MARCBibID", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string MARCBibID
		{
			get
			{
				return _MARCBibID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MARCBibID != value)
				{
					_MARCBibID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCBibID
		
		#region MARCLeader
		
		private string _MARCLeader = null;
		
		/// <summary>
		/// Column: MARCLeader;
		/// DBMS data type: nvarchar(24); Nullable;
		/// </summary>
		[ColumnDefinition("MARCLeader", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=24, IsNullable=true)]
		public string MARCLeader
		{
			get
			{
				return _MARCLeader;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 24);
				if (_MARCLeader != value)
				{
					_MARCLeader = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCLeader
		
		#region BibliographicLevelID
		
		private int? _BibliographicLevelID = null;
		
		/// <summary>
		/// Column: BibliographicLevelID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("BibliographicLevelID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? BibliographicLevelID
		{
			get
			{
				return _BibliographicLevelID;
			}
			set
			{
				if (_BibliographicLevelID != value)
				{
					_BibliographicLevelID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BibliographicLevelID
		
		#region TropicosTitleID
		
		private int? _TropicosTitleID = null;
		
		/// <summary>
		/// Column: TropicosTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TropicosTitleID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? TropicosTitleID
		{
			get
			{
				return _TropicosTitleID;
			}
			set
			{
				if (_TropicosTitleID != value)
				{
					_TropicosTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TropicosTitleID
		
		#region RedirectTitleID
		
		private int? _RedirectTitleID = null;
		
		/// <summary>
		/// Column: RedirectTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectTitleID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? RedirectTitleID
		{
			get
			{
				return _RedirectTitleID;
			}
			set
			{
				if (_RedirectTitleID != value)
				{
					_RedirectTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectTitleID
		
		#region FullTitle
		
		private string _FullTitle = string.Empty;
		
		/// <summary>
		/// Column: FullTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("FullTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=2000)]
		public string FullTitle
		{
			get
			{
				return _FullTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_FullTitle != value)
				{
					_FullTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FullTitle
		
		#region ShortTitle
		
		private string _ShortTitle = null;
		
		/// <summary>
		/// Column: ShortTitle;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("ShortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=255, IsNullable=true)]
		public string ShortTitle
		{
			get
			{
				return _ShortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_ShortTitle != value)
				{
					_ShortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ShortTitle
		
		#region UniformTitle
		
		private string _UniformTitle = null;
		
		/// <summary>
		/// Column: UniformTitle;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("UniformTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=255, IsNullable=true)]
		public string UniformTitle
		{
			get
			{
				return _UniformTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_UniformTitle != value)
				{
					_UniformTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion UniformTitle
		
		#region SortTitle
		
		private string _SortTitle = null;
		
		/// <summary>
		/// Column: SortTitle;
		/// DBMS data type: nvarchar(60); Nullable;
		/// </summary>
		[ColumnDefinition("SortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=60, IsNullable=true)]
		public string SortTitle
		{
			get
			{
				return _SortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 60);
				if (_SortTitle != value)
				{
					_SortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SortTitle
		
		#region CallNumber
		
		private string _CallNumber = null;
		
		/// <summary>
		/// Column: CallNumber;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=100, IsNullable=true)]
		public string CallNumber
		{
			get
			{
				return _CallNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_CallNumber != value)
				{
					_CallNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CallNumber
		
		#region PublicationDetails
		
		private string _PublicationDetails = null;
		
		/// <summary>
		/// Column: PublicationDetails;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PublicationDetails", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=255, IsNullable=true)]
		public string PublicationDetails
		{
			get
			{
				return _PublicationDetails;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PublicationDetails != value)
				{
					_PublicationDetails = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationDetails
		
		#region StartYear
		
		private short? _StartYear = null;
		
		/// <summary>
		/// Column: StartYear;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("StartYear", DbTargetType=SqlDbType.SmallInt, Ordinal=13, NumericPrecision=5, IsNullable=true)]
		public short? StartYear
		{
			get
			{
				return _StartYear;
			}
			set
			{
				if (_StartYear != value)
				{
					_StartYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartYear
		
		#region EndYear
		
		private short? _EndYear = null;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.SmallInt, Ordinal=14, NumericPrecision=5, IsNullable=true)]
		public short? EndYear
		{
			get
			{
				return _EndYear;
			}
			set
			{
				if (_EndYear != value)
				{
					_EndYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndYear
		
		#region Datafield_260_a
		
		private string _Datafield_260_a = null;
		
		/// <summary>
		/// Column: Datafield_260_a;
		/// DBMS data type: nvarchar(150); Nullable;
		/// </summary>
		[ColumnDefinition("Datafield_260_a", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=150, IsNullable=true)]
		public string Datafield_260_a
		{
			get
			{
				return _Datafield_260_a;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_Datafield_260_a != value)
				{
					_Datafield_260_a = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Datafield_260_a
		
		#region Datafield_260_b
		
		private string _Datafield_260_b = null;
		
		/// <summary>
		/// Column: Datafield_260_b;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Datafield_260_b", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=255, IsNullable=true)]
		public string Datafield_260_b
		{
			get
			{
				return _Datafield_260_b;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Datafield_260_b != value)
				{
					_Datafield_260_b = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Datafield_260_b
		
		#region Datafield_260_c
		
		private string _Datafield_260_c = null;
		
		/// <summary>
		/// Column: Datafield_260_c;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("Datafield_260_c", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=100, IsNullable=true)]
		public string Datafield_260_c
		{
			get
			{
				return _Datafield_260_c;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Datafield_260_c != value)
				{
					_Datafield_260_c = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Datafield_260_c
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
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
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region TitleDescription
		
		private string _TitleDescription = null;
		
		/// <summary>
		/// Column: TitleDescription;
		/// DBMS data type: ntext; Nullable;
		/// </summary>
		[ColumnDefinition("TitleDescription", DbTargetType=SqlDbType.NText, Ordinal=20, CharacterMaxLength=1073741823, IsNullable=true)]
		public string TitleDescription
		{
			get
			{
				return _TitleDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_TitleDescription != value)
				{
					_TitleDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleDescription
		
		#region TL2Author
		
		private string _TL2Author = null;
		
		/// <summary>
		/// Column: TL2Author;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("TL2Author", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=100, IsNullable=true)]
		public string TL2Author
		{
			get
			{
				return _TL2Author;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_TL2Author != value)
				{
					_TL2Author = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TL2Author
		
		#region PublishReady
		
		private bool _PublishReady = false;
		
		/// <summary>
		/// Column: PublishReady;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("PublishReady", DbTargetType=SqlDbType.Bit, Ordinal=22)]
		public bool PublishReady
		{
			get
			{
				return _PublishReady;
			}
			set
			{
				if (_PublishReady != value)
				{
					_PublishReady = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublishReady
		
		#region RareBooks
		
		private bool _RareBooks = false;
		
		/// <summary>
		/// Column: RareBooks;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("RareBooks", DbTargetType=SqlDbType.Bit, Ordinal=23)]
		public bool RareBooks
		{
			get
			{
				return _RareBooks;
			}
			set
			{
				if (_RareBooks != value)
				{
					_RareBooks = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RareBooks
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=255, IsNullable=true)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=25, IsNullable=true)]
		public DateTime? CreationDate
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
		
		private DateTime? _LastModifiedDate = null;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=26, IsNullable=true)]
		public DateTime? LastModifiedDate
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
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=27, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=28, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		
		#region OriginalCatalogingSource
		
		private string _OriginalCatalogingSource = null;
		
		/// <summary>
		/// Column: OriginalCatalogingSource;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("OriginalCatalogingSource", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=100, IsNullable=true)]
		public string OriginalCatalogingSource
		{
			get
			{
				return _OriginalCatalogingSource;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_OriginalCatalogingSource != value)
				{
					_OriginalCatalogingSource = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OriginalCatalogingSource
		
		#region EditionStatement
		
		private string _EditionStatement = null;
		
		/// <summary>
		/// Column: EditionStatement;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("EditionStatement", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=450, IsNullable=true)]
		public string EditionStatement
		{
			get
			{
				return _EditionStatement;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_EditionStatement != value)
				{
					_EditionStatement = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EditionStatement
		
		#region CurrentPublicationFrequency
		
		private string _CurrentPublicationFrequency = null;
		
		/// <summary>
		/// Column: CurrentPublicationFrequency;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("CurrentPublicationFrequency", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=100, IsNullable=true)]
		public string CurrentPublicationFrequency
		{
			get
			{
				return _CurrentPublicationFrequency;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_CurrentPublicationFrequency != value)
				{
					_CurrentPublicationFrequency = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CurrentPublicationFrequency
		
		#region PartNumber
		
		private string _PartNumber = null;
		
		/// <summary>
		/// Column: PartNumber;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=32, CharacterMaxLength=255, IsNullable=true)]
		public string PartNumber
		{
			get
			{
				return _PartNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartNumber != value)
				{
					_PartNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartNumber
		
		#region PartName
		
		private string _PartName = null;
		
		/// <summary>
		/// Column: PartName;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PartName", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=255, IsNullable=true)]
		public string PartName
		{
			get
			{
				return _PartName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartName != value)
				{
					_PartName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartName
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Title"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Title"/>, 
		/// returns an instance of <see cref="__Title"/>; otherwise returns null.</returns>
		public static new __Title FromArray(byte[] byteArray)
		{
			__Title o = null;
			
			try
			{
				o = (__Title) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Title"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Title"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Title)
			{
				__Title o = (__Title) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleID == TitleID &&
					GetComparisonString(o.MARCBibID) == GetComparisonString(MARCBibID) &&
					GetComparisonString(o.MARCLeader) == GetComparisonString(MARCLeader) &&
					o.BibliographicLevelID == BibliographicLevelID &&
					o.TropicosTitleID == TropicosTitleID &&
					o.RedirectTitleID == RedirectTitleID &&
					GetComparisonString(o.FullTitle) == GetComparisonString(FullTitle) &&
					GetComparisonString(o.ShortTitle) == GetComparisonString(ShortTitle) &&
					GetComparisonString(o.UniformTitle) == GetComparisonString(UniformTitle) &&
					GetComparisonString(o.SortTitle) == GetComparisonString(SortTitle) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					GetComparisonString(o.PublicationDetails) == GetComparisonString(PublicationDetails) &&
					o.StartYear == StartYear &&
					o.EndYear == EndYear &&
					GetComparisonString(o.Datafield_260_a) == GetComparisonString(Datafield_260_a) &&
					GetComparisonString(o.Datafield_260_b) == GetComparisonString(Datafield_260_b) &&
					GetComparisonString(o.Datafield_260_c) == GetComparisonString(Datafield_260_c) &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.TitleDescription) == GetComparisonString(TitleDescription) &&
					GetComparisonString(o.TL2Author) == GetComparisonString(TL2Author) &&
					o.PublishReady == PublishReady &&
					o.RareBooks == RareBooks &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					GetComparisonString(o.OriginalCatalogingSource) == GetComparisonString(OriginalCatalogingSource) &&
					GetComparisonString(o.EditionStatement) == GetComparisonString(EditionStatement) &&
					GetComparisonString(o.CurrentPublicationFrequency) == GetComparisonString(CurrentPublicationFrequency) &&
					GetComparisonString(o.PartNumber) == GetComparisonString(PartNumber) &&
					GetComparisonString(o.PartName) == GetComparisonString(PartName) 
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
				throw new ArgumentException("Argument is not of type __Title");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Title"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Title a, __Title b)
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
		/// <param name="a">The first <see cref="__Title"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Title a, __Title b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Title"/> object to compare with the current <see cref="__Title"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Title))
			{
				return false;
			}
			
			return this == (__Title) obj;
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
		/// list.Sort(SortOrder.Ascending, __Title.SortColumn.TitleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleID = "TitleID";	
			public const string MARCBibID = "MARCBibID";	
			public const string MARCLeader = "MARCLeader";	
			public const string BibliographicLevelID = "BibliographicLevelID";	
			public const string TropicosTitleID = "TropicosTitleID";	
			public const string RedirectTitleID = "RedirectTitleID";	
			public const string FullTitle = "FullTitle";	
			public const string ShortTitle = "ShortTitle";	
			public const string UniformTitle = "UniformTitle";	
			public const string SortTitle = "SortTitle";	
			public const string CallNumber = "CallNumber";	
			public const string PublicationDetails = "PublicationDetails";	
			public const string StartYear = "StartYear";	
			public const string EndYear = "EndYear";	
			public const string Datafield_260_a = "Datafield_260_a";	
			public const string Datafield_260_b = "Datafield_260_b";	
			public const string Datafield_260_c = "Datafield_260_c";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string LanguageCode = "LanguageCode";	
			public const string TitleDescription = "TitleDescription";	
			public const string TL2Author = "TL2Author";	
			public const string PublishReady = "PublishReady";	
			public const string RareBooks = "RareBooks";	
			public const string Note = "Note";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string OriginalCatalogingSource = "OriginalCatalogingSource";	
			public const string EditionStatement = "EditionStatement";	
			public const string CurrentPublicationFrequency = "CurrentPublicationFrequency";	
			public const string PartNumber = "PartNumber";	
			public const string PartName = "PartName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
