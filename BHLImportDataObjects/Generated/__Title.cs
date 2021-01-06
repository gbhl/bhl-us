
// Generated 1/5/2021 2:18:18 PM
// Do not modify the contents of this code file.
// This abstract class __Title is based upon dbo.Title.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
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

namespace MOBOT.BHLImport.DataObjects
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
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
		/// <param name="note"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="rareBooks"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="productionTitleID"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		public __Title(int titleID, 
			string importKey, 
			int importStatusID, 
			int? importSourceID, 
			string mARCBibID, 
			string mARCLeader, 
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
			bool? publishReady, 
			string note, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			int? externalCreationUser, 
			int? externalLastModifiedUser, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate, 
			bool? rareBooks, 
			string originalCatalogingSource, 
			string editionStatement, 
			string currentPublicationFrequency, 
			int? productionTitleID, 
			string partNumber, 
			string partName) : this()
		{
			_TitleID = titleID;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			MARCBibID = mARCBibID;
			MARCLeader = mARCLeader;
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
			Note = note;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ExternalCreationUser = externalCreationUser;
			ExternalLastModifiedUser = externalLastModifiedUser;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
			RareBooks = rareBooks;
			OriginalCatalogingSource = originalCatalogingSource;
			EditionStatement = editionStatement;
			CurrentPublicationFrequency = currentPublicationFrequency;
			ProductionTitleID = productionTitleID;
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
						_PublishReady = (bool?)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
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
					case "ExternalCreationUser" :
					{
						_ExternalCreationUser = (int?)column.Value;
						break;
					}
					case "ExternalLastModifiedUser" :
					{
						_ExternalLastModifiedUser = (int?)column.Value;
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
					case "RareBooks" :
					{
						_RareBooks = (bool?)column.Value;
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
					case "ProductionTitleID" :
					{
						_ProductionTitleID = (int?)column.Value;
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
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
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
		
		private int? _ImportSourceID = null;
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region MARCBibID
		
		private string _MARCBibID = string.Empty;
		
		/// <summary>
		/// Column: MARCBibID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("MARCBibID", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
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
		[ColumnDefinition("MARCLeader", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=24, IsNullable=true)]
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
		
		#region FullTitle
		
		private string _FullTitle = null;
		
		/// <summary>
		/// Column: FullTitle;
		/// DBMS data type: ntext; Nullable;
		/// </summary>
		[ColumnDefinition("FullTitle", DbTargetType=SqlDbType.NText, Ordinal=7, CharacterMaxLength=1073741823, IsNullable=true)]
		public string FullTitle
		{
			get
			{
				return _FullTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
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
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=10, IsNullable=true)]
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
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=10, IsNullable=true)]
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
		
		private bool? _PublishReady = null;
		
		/// <summary>
		/// Column: PublishReady;
		/// DBMS data type: bit; Nullable;
		/// </summary>
		[ColumnDefinition("PublishReady", DbTargetType=SqlDbType.Bit, Ordinal=22, IsNullable=true)]
		public bool? PublishReady
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
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=255, IsNullable=true)]
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
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=24, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=25, IsNullable=true)]
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
		
		#region ExternalCreationUser
		
		private int? _ExternalCreationUser = null;
		
		/// <summary>
		/// Column: ExternalCreationUser;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationUser", DbTargetType=SqlDbType.Int, Ordinal=26, NumericPrecision=10, IsNullable=true)]
		public int? ExternalCreationUser
		{
			get
			{
				return _ExternalCreationUser;
			}
			set
			{
				if (_ExternalCreationUser != value)
				{
					_ExternalCreationUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalCreationUser
		
		#region ExternalLastModifiedUser
		
		private int? _ExternalLastModifiedUser = null;
		
		/// <summary>
		/// Column: ExternalLastModifiedUser;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalLastModifiedUser", DbTargetType=SqlDbType.Int, Ordinal=27, NumericPrecision=10, IsNullable=true)]
		public int? ExternalLastModifiedUser
		{
			get
			{
				return _ExternalLastModifiedUser;
			}
			set
			{
				if (_ExternalLastModifiedUser != value)
				{
					_ExternalLastModifiedUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalLastModifiedUser
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=28, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=29)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=30)]
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
		
		#region RareBooks
		
		private bool? _RareBooks = null;
		
		/// <summary>
		/// Column: RareBooks;
		/// DBMS data type: bit; Nullable;
		/// </summary>
		[ColumnDefinition("RareBooks", DbTargetType=SqlDbType.Bit, Ordinal=31, IsNullable=true)]
		public bool? RareBooks
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
		
		#region OriginalCatalogingSource
		
		private string _OriginalCatalogingSource = null;
		
		/// <summary>
		/// Column: OriginalCatalogingSource;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("OriginalCatalogingSource", DbTargetType=SqlDbType.NVarChar, Ordinal=32, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("EditionStatement", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=450, IsNullable=true)]
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
		[ColumnDefinition("CurrentPublicationFrequency", DbTargetType=SqlDbType.NVarChar, Ordinal=34, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region ProductionTitleID
		
		private int? _ProductionTitleID = null;
		
		/// <summary>
		/// Column: ProductionTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionTitleID", DbTargetType=SqlDbType.Int, Ordinal=35, NumericPrecision=10, IsNullable=true)]
		public int? ProductionTitleID
		{
			get
			{
				return _ProductionTitleID;
			}
			set
			{
				if (_ProductionTitleID != value)
				{
					_ProductionTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionTitleID
		
		#region PartNumber
		
		private string _PartNumber = null;
		
		/// <summary>
		/// Column: PartNumber;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=36, CharacterMaxLength=255, IsNullable=true)]
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
		[ColumnDefinition("PartName", DbTargetType=SqlDbType.NVarChar, Ordinal=37, CharacterMaxLength=255, IsNullable=true)]
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
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.MARCBibID) == GetComparisonString(MARCBibID) &&
					GetComparisonString(o.MARCLeader) == GetComparisonString(MARCLeader) &&
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
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
					o.ExternalCreationUser == ExternalCreationUser &&
					o.ExternalLastModifiedUser == ExternalLastModifiedUser &&
					o.ProductionDate == ProductionDate &&
					o.CreatedDate == CreatedDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.RareBooks == RareBooks &&
					GetComparisonString(o.OriginalCatalogingSource) == GetComparisonString(OriginalCatalogingSource) &&
					GetComparisonString(o.EditionStatement) == GetComparisonString(EditionStatement) &&
					GetComparisonString(o.CurrentPublicationFrequency) == GetComparisonString(CurrentPublicationFrequency) &&
					o.ProductionTitleID == ProductionTitleID &&
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
		/// list.Sort(SortOrder.Ascending, __Title.SortColumn.TitleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleID = "TitleID";	
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string MARCBibID = "MARCBibID";	
			public const string MARCLeader = "MARCLeader";	
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
			public const string Note = "Note";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ExternalCreationUser = "ExternalCreationUser";	
			public const string ExternalLastModifiedUser = "ExternalLastModifiedUser";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string RareBooks = "RareBooks";	
			public const string OriginalCatalogingSource = "OriginalCatalogingSource";	
			public const string EditionStatement = "EditionStatement";	
			public const string CurrentPublicationFrequency = "CurrentPublicationFrequency";	
			public const string ProductionTitleID = "ProductionTitleID";	
			public const string PartNumber = "PartNumber";	
			public const string PartName = "PartName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

