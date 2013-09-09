
// Generated 2/15/2011 10:08:10 AM
// Do not modify the contents of this code file.
// This abstract class __ImportLog is based upon ImportLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class ImportLog : __ImportLog
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
	public abstract class __ImportLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importLogID"></param>
		/// <param name="importDate"></param>
		/// <param name="importSourceID"></param>
		/// <param name="barCode"></param>
		/// <param name="importResult"></param>
		/// <param name="titleInsert"></param>
		/// <param name="titleUpdate"></param>
		/// <param name="creatorInsert"></param>
		/// <param name="creatorUpdate"></param>
		/// <param name="titleCreatorInsert"></param>
		/// <param name="titleCreatorUpdate"></param>
		/// <param name="titleTagInsert"></param>
		/// <param name="titleTagUpdate"></param>
		/// <param name="titleTitleIdentifierInsert"></param>
		/// <param name="titleTitleIdentifierUpdate"></param>
		/// <param name="titleAssociationInsert"></param>
		/// <param name="titleAssociationTitleIdentifierInsert"></param>
		/// <param name="titleVariantInsert"></param>
		/// <param name="itemInsert"></param>
		/// <param name="itemUpdate"></param>
		/// <param name="titleItemInsert"></param>
		/// <param name="pageInsert"></param>
		/// <param name="pageUpdate"></param>
		/// <param name="indicatedPageInsert"></param>
		/// <param name="indicatedPageUpdate"></param>
		/// <param name="pagePageTypeInsert"></param>
		/// <param name="pagePageTypeUpdate"></param>
		/// <param name="pageNameInsert"></param>
		/// <param name="pageNameUpdate"></param>
		public __ImportLog(int importLogID, 
			DateTime importDate, 
			int? importSourceID, 
			string barCode, 
			string importResult, 
			int titleInsert, 
			int titleUpdate, 
			int creatorInsert, 
			int creatorUpdate, 
			int titleCreatorInsert, 
			int titleCreatorUpdate, 
			int titleTagInsert, 
			int titleTagUpdate, 
			int? titleTitleIdentifierInsert, 
			int? titleTitleIdentifierUpdate, 
			int? titleAssociationInsert, 
			int? titleAssociationTitleIdentifierInsert, 
			int? titleVariantInsert, 
			int itemInsert, 
			int itemUpdate, 
			int? titleItemInsert, 
			int pageInsert, 
			int pageUpdate, 
			int indicatedPageInsert, 
			int indicatedPageUpdate, 
			int pagePageTypeInsert, 
			int pagePageTypeUpdate, 
			int pageNameInsert, 
			int pageNameUpdate) : this()
		{
			_ImportLogID = importLogID;
			ImportDate = importDate;
			ImportSourceID = importSourceID;
			BarCode = barCode;
			ImportResult = importResult;
			TitleInsert = titleInsert;
			TitleUpdate = titleUpdate;
			CreatorInsert = creatorInsert;
			CreatorUpdate = creatorUpdate;
			TitleCreatorInsert = titleCreatorInsert;
			TitleCreatorUpdate = titleCreatorUpdate;
			TitleTagInsert = titleTagInsert;
			TitleTagUpdate = titleTagUpdate;
			TitleTitleIdentifierInsert = titleTitleIdentifierInsert;
			TitleTitleIdentifierUpdate = titleTitleIdentifierUpdate;
			TitleAssociationInsert = titleAssociationInsert;
			TitleAssociationTitleIdentifierInsert = titleAssociationTitleIdentifierInsert;
			TitleVariantInsert = titleVariantInsert;
			ItemInsert = itemInsert;
			ItemUpdate = itemUpdate;
			TitleItemInsert = titleItemInsert;
			PageInsert = pageInsert;
			PageUpdate = pageUpdate;
			IndicatedPageInsert = indicatedPageInsert;
			IndicatedPageUpdate = indicatedPageUpdate;
			PagePageTypeInsert = pagePageTypeInsert;
			PagePageTypeUpdate = pagePageTypeUpdate;
			PageNameInsert = pageNameInsert;
			PageNameUpdate = pageNameUpdate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ImportLog()
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
					case "ImportLogID" :
					{
						_ImportLogID = (int)column.Value;
						break;
					}
					case "ImportDate" :
					{
						_ImportDate = (DateTime)column.Value;
						break;
					}
					case "ImportSourceID" :
					{
						_ImportSourceID = (int?)column.Value;
						break;
					}
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "ImportResult" :
					{
						_ImportResult = (string)column.Value;
						break;
					}
					case "TitleInsert" :
					{
						_TitleInsert = (int)column.Value;
						break;
					}
					case "TitleUpdate" :
					{
						_TitleUpdate = (int)column.Value;
						break;
					}
					case "CreatorInsert" :
					{
						_CreatorInsert = (int)column.Value;
						break;
					}
					case "CreatorUpdate" :
					{
						_CreatorUpdate = (int)column.Value;
						break;
					}
					case "TitleCreatorInsert" :
					{
						_TitleCreatorInsert = (int)column.Value;
						break;
					}
					case "TitleCreatorUpdate" :
					{
						_TitleCreatorUpdate = (int)column.Value;
						break;
					}
					case "TitleTagInsert" :
					{
						_TitleTagInsert = (int)column.Value;
						break;
					}
					case "TitleTagUpdate" :
					{
						_TitleTagUpdate = (int)column.Value;
						break;
					}
					case "TitleTitleIdentifierInsert" :
					{
						_TitleTitleIdentifierInsert = (int?)column.Value;
						break;
					}
					case "TitleTitleIdentifierUpdate" :
					{
						_TitleTitleIdentifierUpdate = (int?)column.Value;
						break;
					}
					case "TitleAssociationInsert" :
					{
						_TitleAssociationInsert = (int?)column.Value;
						break;
					}
					case "TitleAssociationTitleIdentifierInsert" :
					{
						_TitleAssociationTitleIdentifierInsert = (int?)column.Value;
						break;
					}
					case "TitleVariantInsert" :
					{
						_TitleVariantInsert = (int?)column.Value;
						break;
					}
					case "ItemInsert" :
					{
						_ItemInsert = (int)column.Value;
						break;
					}
					case "ItemUpdate" :
					{
						_ItemUpdate = (int)column.Value;
						break;
					}
					case "TitleItemInsert" :
					{
						_TitleItemInsert = (int?)column.Value;
						break;
					}
					case "PageInsert" :
					{
						_PageInsert = (int)column.Value;
						break;
					}
					case "PageUpdate" :
					{
						_PageUpdate = (int)column.Value;
						break;
					}
					case "IndicatedPageInsert" :
					{
						_IndicatedPageInsert = (int)column.Value;
						break;
					}
					case "IndicatedPageUpdate" :
					{
						_IndicatedPageUpdate = (int)column.Value;
						break;
					}
					case "PagePageTypeInsert" :
					{
						_PagePageTypeInsert = (int)column.Value;
						break;
					}
					case "PagePageTypeUpdate" :
					{
						_PagePageTypeUpdate = (int)column.Value;
						break;
					}
					case "PageNameInsert" :
					{
						_PageNameInsert = (int)column.Value;
						break;
					}
					case "PageNameUpdate" :
					{
						_PageNameUpdate = (int)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region ImportLogID
		
		private int _ImportLogID = default(int);
		
		/// <summary>
		/// Column: ImportLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ImportLogID
		{
			get
			{
				return _ImportLogID;
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
		
		#endregion ImportLogID
		
		#region ImportDate
		
		private DateTime _ImportDate;
		
		/// <summary>
		/// Column: ImportDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("ImportDate", DbTargetType=SqlDbType.DateTime, Ordinal=2)]
		public DateTime ImportDate
		{
			get
			{
				return _ImportDate;
			}
			set
			{
				if (_ImportDate != value)
				{
					_ImportDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportDate
		
		#region ImportSourceID
		
		private int? _ImportSourceID = null;
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
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
		
		#region BarCode
		
		private string _BarCode = null;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(40); Nullable;
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=40, IsNullable=true)]
		public string BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_BarCode != value)
				{
					_BarCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BarCode
		
		#region ImportResult
		
		private string _ImportResult = string.Empty;
		
		/// <summary>
		/// Column: ImportResult;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("ImportResult", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=30)]
		public string ImportResult
		{
			get
			{
				return _ImportResult;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_ImportResult != value)
				{
					_ImportResult = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportResult
		
		#region TitleInsert
		
		private int _TitleInsert = default(int);
		
		/// <summary>
		/// Column: TitleInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleInsert", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10)]
		public int TitleInsert
		{
			get
			{
				return _TitleInsert;
			}
			set
			{
				if (_TitleInsert != value)
				{
					_TitleInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleInsert
		
		#region TitleUpdate
		
		private int _TitleUpdate = default(int);
		
		/// <summary>
		/// Column: TitleUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleUpdate", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
		public int TitleUpdate
		{
			get
			{
				return _TitleUpdate;
			}
			set
			{
				if (_TitleUpdate != value)
				{
					_TitleUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleUpdate
		
		#region CreatorInsert
		
		private int _CreatorInsert = default(int);
		
		/// <summary>
		/// Column: CreatorInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreatorInsert", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int CreatorInsert
		{
			get
			{
				return _CreatorInsert;
			}
			set
			{
				if (_CreatorInsert != value)
				{
					_CreatorInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorInsert
		
		#region CreatorUpdate
		
		private int _CreatorUpdate = default(int);
		
		/// <summary>
		/// Column: CreatorUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreatorUpdate", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int CreatorUpdate
		{
			get
			{
				return _CreatorUpdate;
			}
			set
			{
				if (_CreatorUpdate != value)
				{
					_CreatorUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorUpdate
		
		#region TitleCreatorInsert
		
		private int _TitleCreatorInsert = default(int);
		
		/// <summary>
		/// Column: TitleCreatorInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleCreatorInsert", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
		public int TitleCreatorInsert
		{
			get
			{
				return _TitleCreatorInsert;
			}
			set
			{
				if (_TitleCreatorInsert != value)
				{
					_TitleCreatorInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleCreatorInsert
		
		#region TitleCreatorUpdate
		
		private int _TitleCreatorUpdate = default(int);
		
		/// <summary>
		/// Column: TitleCreatorUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleCreatorUpdate", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10)]
		public int TitleCreatorUpdate
		{
			get
			{
				return _TitleCreatorUpdate;
			}
			set
			{
				if (_TitleCreatorUpdate != value)
				{
					_TitleCreatorUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleCreatorUpdate
		
		#region TitleTagInsert
		
		private int _TitleTagInsert = default(int);
		
		/// <summary>
		/// Column: TitleTagInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleTagInsert", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10)]
		public int TitleTagInsert
		{
			get
			{
				return _TitleTagInsert;
			}
			set
			{
				if (_TitleTagInsert != value)
				{
					_TitleTagInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleTagInsert
		
		#region TitleTagUpdate
		
		private int _TitleTagUpdate = default(int);
		
		/// <summary>
		/// Column: TitleTagUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleTagUpdate", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10)]
		public int TitleTagUpdate
		{
			get
			{
				return _TitleTagUpdate;
			}
			set
			{
				if (_TitleTagUpdate != value)
				{
					_TitleTagUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleTagUpdate
		
		#region TitleTitleIdentifierInsert
		
		private int? _TitleTitleIdentifierInsert = null;
		
		/// <summary>
		/// Column: TitleTitleIdentifierInsert;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleTitleIdentifierInsert", DbTargetType=SqlDbType.Int, Ordinal=14, NumericPrecision=10, IsNullable=true)]
		public int? TitleTitleIdentifierInsert
		{
			get
			{
				return _TitleTitleIdentifierInsert;
			}
			set
			{
				if (_TitleTitleIdentifierInsert != value)
				{
					_TitleTitleIdentifierInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleTitleIdentifierInsert
		
		#region TitleTitleIdentifierUpdate
		
		private int? _TitleTitleIdentifierUpdate = null;
		
		/// <summary>
		/// Column: TitleTitleIdentifierUpdate;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleTitleIdentifierUpdate", DbTargetType=SqlDbType.Int, Ordinal=15, NumericPrecision=10, IsNullable=true)]
		public int? TitleTitleIdentifierUpdate
		{
			get
			{
				return _TitleTitleIdentifierUpdate;
			}
			set
			{
				if (_TitleTitleIdentifierUpdate != value)
				{
					_TitleTitleIdentifierUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleTitleIdentifierUpdate
		
		#region TitleAssociationInsert
		
		private int? _TitleAssociationInsert = null;
		
		/// <summary>
		/// Column: TitleAssociationInsert;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleAssociationInsert", DbTargetType=SqlDbType.Int, Ordinal=16, NumericPrecision=10, IsNullable=true)]
		public int? TitleAssociationInsert
		{
			get
			{
				return _TitleAssociationInsert;
			}
			set
			{
				if (_TitleAssociationInsert != value)
				{
					_TitleAssociationInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleAssociationInsert
		
		#region TitleAssociationTitleIdentifierInsert
		
		private int? _TitleAssociationTitleIdentifierInsert = null;
		
		/// <summary>
		/// Column: TitleAssociationTitleIdentifierInsert;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleAssociationTitleIdentifierInsert", DbTargetType=SqlDbType.Int, Ordinal=17, NumericPrecision=10, IsNullable=true)]
		public int? TitleAssociationTitleIdentifierInsert
		{
			get
			{
				return _TitleAssociationTitleIdentifierInsert;
			}
			set
			{
				if (_TitleAssociationTitleIdentifierInsert != value)
				{
					_TitleAssociationTitleIdentifierInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleAssociationTitleIdentifierInsert
		
		#region TitleVariantInsert
		
		private int? _TitleVariantInsert = null;
		
		/// <summary>
		/// Column: TitleVariantInsert;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleVariantInsert", DbTargetType=SqlDbType.Int, Ordinal=18, NumericPrecision=10, IsNullable=true)]
		public int? TitleVariantInsert
		{
			get
			{
				return _TitleVariantInsert;
			}
			set
			{
				if (_TitleVariantInsert != value)
				{
					_TitleVariantInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleVariantInsert
		
		#region ItemInsert
		
		private int _ItemInsert = default(int);
		
		/// <summary>
		/// Column: ItemInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemInsert", DbTargetType=SqlDbType.Int, Ordinal=19, NumericPrecision=10)]
		public int ItemInsert
		{
			get
			{
				return _ItemInsert;
			}
			set
			{
				if (_ItemInsert != value)
				{
					_ItemInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemInsert
		
		#region ItemUpdate
		
		private int _ItemUpdate = default(int);
		
		/// <summary>
		/// Column: ItemUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemUpdate", DbTargetType=SqlDbType.Int, Ordinal=20, NumericPrecision=10)]
		public int ItemUpdate
		{
			get
			{
				return _ItemUpdate;
			}
			set
			{
				if (_ItemUpdate != value)
				{
					_ItemUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemUpdate
		
		#region TitleItemInsert
		
		private int? _TitleItemInsert = null;
		
		/// <summary>
		/// Column: TitleItemInsert;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleItemInsert", DbTargetType=SqlDbType.Int, Ordinal=21, NumericPrecision=10, IsNullable=true)]
		public int? TitleItemInsert
		{
			get
			{
				return _TitleItemInsert;
			}
			set
			{
				if (_TitleItemInsert != value)
				{
					_TitleItemInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleItemInsert
		
		#region PageInsert
		
		private int _PageInsert = default(int);
		
		/// <summary>
		/// Column: PageInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageInsert", DbTargetType=SqlDbType.Int, Ordinal=22, NumericPrecision=10)]
		public int PageInsert
		{
			get
			{
				return _PageInsert;
			}
			set
			{
				if (_PageInsert != value)
				{
					_PageInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageInsert
		
		#region PageUpdate
		
		private int _PageUpdate = default(int);
		
		/// <summary>
		/// Column: PageUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageUpdate", DbTargetType=SqlDbType.Int, Ordinal=23, NumericPrecision=10)]
		public int PageUpdate
		{
			get
			{
				return _PageUpdate;
			}
			set
			{
				if (_PageUpdate != value)
				{
					_PageUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageUpdate
		
		#region IndicatedPageInsert
		
		private int _IndicatedPageInsert = default(int);
		
		/// <summary>
		/// Column: IndicatedPageInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("IndicatedPageInsert", DbTargetType=SqlDbType.Int, Ordinal=24, NumericPrecision=10)]
		public int IndicatedPageInsert
		{
			get
			{
				return _IndicatedPageInsert;
			}
			set
			{
				if (_IndicatedPageInsert != value)
				{
					_IndicatedPageInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IndicatedPageInsert
		
		#region IndicatedPageUpdate
		
		private int _IndicatedPageUpdate = default(int);
		
		/// <summary>
		/// Column: IndicatedPageUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("IndicatedPageUpdate", DbTargetType=SqlDbType.Int, Ordinal=25, NumericPrecision=10)]
		public int IndicatedPageUpdate
		{
			get
			{
				return _IndicatedPageUpdate;
			}
			set
			{
				if (_IndicatedPageUpdate != value)
				{
					_IndicatedPageUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IndicatedPageUpdate
		
		#region PagePageTypeInsert
		
		private int _PagePageTypeInsert = default(int);
		
		/// <summary>
		/// Column: PagePageTypeInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PagePageTypeInsert", DbTargetType=SqlDbType.Int, Ordinal=26, NumericPrecision=10)]
		public int PagePageTypeInsert
		{
			get
			{
				return _PagePageTypeInsert;
			}
			set
			{
				if (_PagePageTypeInsert != value)
				{
					_PagePageTypeInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePageTypeInsert
		
		#region PagePageTypeUpdate
		
		private int _PagePageTypeUpdate = default(int);
		
		/// <summary>
		/// Column: PagePageTypeUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PagePageTypeUpdate", DbTargetType=SqlDbType.Int, Ordinal=27, NumericPrecision=10)]
		public int PagePageTypeUpdate
		{
			get
			{
				return _PagePageTypeUpdate;
			}
			set
			{
				if (_PagePageTypeUpdate != value)
				{
					_PagePageTypeUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePageTypeUpdate
		
		#region PageNameInsert
		
		private int _PageNameInsert = default(int);
		
		/// <summary>
		/// Column: PageNameInsert;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageNameInsert", DbTargetType=SqlDbType.Int, Ordinal=28, NumericPrecision=10)]
		public int PageNameInsert
		{
			get
			{
				return _PageNameInsert;
			}
			set
			{
				if (_PageNameInsert != value)
				{
					_PageNameInsert = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageNameInsert
		
		#region PageNameUpdate
		
		private int _PageNameUpdate = default(int);
		
		/// <summary>
		/// Column: PageNameUpdate;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageNameUpdate", DbTargetType=SqlDbType.Int, Ordinal=29, NumericPrecision=10)]
		public int PageNameUpdate
		{
			get
			{
				return _PageNameUpdate;
			}
			set
			{
				if (_PageNameUpdate != value)
				{
					_PageNameUpdate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageNameUpdate
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ImportLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportLog"/>, 
		/// returns an instance of <see cref="__ImportLog"/>; otherwise returns null.</returns>
		public static new __ImportLog FromArray(byte[] byteArray)
		{
			__ImportLog o = null;
			
			try
			{
				o = (__ImportLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportLog)
			{
				__ImportLog o = (__ImportLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportLogID == ImportLogID &&
					o.ImportDate == ImportDate &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.ImportResult) == GetComparisonString(ImportResult) &&
					o.TitleInsert == TitleInsert &&
					o.TitleUpdate == TitleUpdate &&
					o.CreatorInsert == CreatorInsert &&
					o.CreatorUpdate == CreatorUpdate &&
					o.TitleCreatorInsert == TitleCreatorInsert &&
					o.TitleCreatorUpdate == TitleCreatorUpdate &&
					o.TitleTagInsert == TitleTagInsert &&
					o.TitleTagUpdate == TitleTagUpdate &&
					o.TitleTitleIdentifierInsert == TitleTitleIdentifierInsert &&
					o.TitleTitleIdentifierUpdate == TitleTitleIdentifierUpdate &&
					o.TitleAssociationInsert == TitleAssociationInsert &&
					o.TitleAssociationTitleIdentifierInsert == TitleAssociationTitleIdentifierInsert &&
					o.TitleVariantInsert == TitleVariantInsert &&
					o.ItemInsert == ItemInsert &&
					o.ItemUpdate == ItemUpdate &&
					o.TitleItemInsert == TitleItemInsert &&
					o.PageInsert == PageInsert &&
					o.PageUpdate == PageUpdate &&
					o.IndicatedPageInsert == IndicatedPageInsert &&
					o.IndicatedPageUpdate == IndicatedPageUpdate &&
					o.PagePageTypeInsert == PagePageTypeInsert &&
					o.PagePageTypeUpdate == PagePageTypeUpdate &&
					o.PageNameInsert == PageNameInsert &&
					o.PageNameUpdate == PageNameUpdate 
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
				throw new ArgumentException("Argument is not of type __ImportLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportLog a, __ImportLog b)
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
		/// <param name="a">The first <see cref="__ImportLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportLog a, __ImportLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportLog"/> object to compare with the current <see cref="__ImportLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportLog))
			{
				return false;
			}
			
			return this == (__ImportLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __ImportLog.SortColumn.ImportLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportLogID = "ImportLogID";	
			public const string ImportDate = "ImportDate";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string BarCode = "BarCode";	
			public const string ImportResult = "ImportResult";	
			public const string TitleInsert = "TitleInsert";	
			public const string TitleUpdate = "TitleUpdate";	
			public const string CreatorInsert = "CreatorInsert";	
			public const string CreatorUpdate = "CreatorUpdate";	
			public const string TitleCreatorInsert = "TitleCreatorInsert";	
			public const string TitleCreatorUpdate = "TitleCreatorUpdate";	
			public const string TitleTagInsert = "TitleTagInsert";	
			public const string TitleTagUpdate = "TitleTagUpdate";	
			public const string TitleTitleIdentifierInsert = "TitleTitleIdentifierInsert";	
			public const string TitleTitleIdentifierUpdate = "TitleTitleIdentifierUpdate";	
			public const string TitleAssociationInsert = "TitleAssociationInsert";	
			public const string TitleAssociationTitleIdentifierInsert = "TitleAssociationTitleIdentifierInsert";	
			public const string TitleVariantInsert = "TitleVariantInsert";	
			public const string ItemInsert = "ItemInsert";	
			public const string ItemUpdate = "ItemUpdate";	
			public const string TitleItemInsert = "TitleItemInsert";	
			public const string PageInsert = "PageInsert";	
			public const string PageUpdate = "PageUpdate";	
			public const string IndicatedPageInsert = "IndicatedPageInsert";	
			public const string IndicatedPageUpdate = "IndicatedPageUpdate";	
			public const string PagePageTypeInsert = "PagePageTypeInsert";	
			public const string PagePageTypeUpdate = "PagePageTypeUpdate";	
			public const string PageNameInsert = "PageNameInsert";	
			public const string PageNameUpdate = "PageNameUpdate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
