
// Generated 12/5/2008 3:06:57 PM
// Do not modify the contents of this code file.
// This abstract class __ItemCOinSView is based upon ItemCOinSView.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ItemCOinSView : __ItemCOinSView
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
	public abstract class __ItemCOinSView : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ItemCOinSView()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="lccn"></param>
		/// <param name="oclc"></param>
		/// <param name="rft_title"></param>
		/// <param name="rft_stitle"></param>
		/// <param name="rft_volume"></param>
		/// <param name="rft_language"></param>
		/// <param name="rft_isbn"></param>
		/// <param name="rft_aulast"></param>
		/// <param name="rft_aufirst"></param>
		/// <param name="rft_au_BOOK"></param>
		/// <param name="rft_au_DC"></param>
		/// <param name="rft_aucorp"></param>
		/// <param name="rft_place"></param>
		/// <param name="rft_pub"></param>
		/// <param name="rft_publisher"></param>
		/// <param name="rft_date_ITEM"></param>
		/// <param name="rft_date_TITLE"></param>
		/// <param name="rft_edition"></param>
		/// <param name="rft_tpages"></param>
		/// <param name="rft_issn"></param>
		/// <param name="rft_coden"></param>
		/// <param name="rft_subject"></param>
		/// <param name="rft_contributor_ITEM"></param>
		/// <param name="rft_contributor_TITLE"></param>
		/// <param name="rft_genre"></param>
		public __ItemCOinSView(int titleID, 
			int itemID, 
			string lccn, 
			string oclc, 
			string rft_title, 
			string rft_stitle, 
			string rft_volume, 
			string rft_language, 
			string rft_isbn, 
			string rft_aulast, 
			string rft_aufirst, 
			string rft_au_BOOK, 
			string rft_au_DC, 
			string rft_aucorp, 
			string rft_place, 
			string rft_pub, 
			string rft_publisher, 
			string rft_date_ITEM, 
			string rft_date_TITLE, 
			string rft_edition, 
			int? rft_tpages, 
			string rft_issn, 
			string rft_coden, 
			string rft_subject, 
			string rft_contributor_ITEM, 
			string rft_contributor_TITLE, 
			string rft_genre) : this()
		{
			TitleID = titleID;
			ItemID = itemID;
			Lccn = lccn;
			Oclc = oclc;
			Rft_title = rft_title;
			Rft_stitle = rft_stitle;
			Rft_volume = rft_volume;
			Rft_language = rft_language;
			Rft_isbn = rft_isbn;
			Rft_aulast = rft_aulast;
			Rft_aufirst = rft_aufirst;
			Rft_au_BOOK = rft_au_BOOK;
			Rft_au_DC = rft_au_DC;
			Rft_aucorp = rft_aucorp;
			Rft_place = rft_place;
			Rft_pub = rft_pub;
			Rft_publisher = rft_publisher;
			Rft_date_ITEM = rft_date_ITEM;
			Rft_date_TITLE = rft_date_TITLE;
			Rft_edition = rft_edition;
			Rft_tpages = rft_tpages;
			Rft_issn = rft_issn;
			Rft_coden = rft_coden;
			Rft_subject = rft_subject;
			Rft_contributor_ITEM = rft_contributor_ITEM;
			Rft_contributor_TITLE = rft_contributor_TITLE;
			Rft_genre = rft_genre;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ItemCOinSView()
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
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "lccn" :
					{
						_Lccn = (string)column.Value;
						break;
					}
					case "oclc" :
					{
						_Oclc = (string)column.Value;
						break;
					}
					case "rft_title" :
					{
						_Rft_title = (string)column.Value;
						break;
					}
					case "rft_stitle" :
					{
						_Rft_stitle = (string)column.Value;
						break;
					}
					case "rft_volume" :
					{
						_Rft_volume = (string)column.Value;
						break;
					}
					case "rft_language" :
					{
						_Rft_language = (string)column.Value;
						break;
					}
					case "rft_isbn" :
					{
						_Rft_isbn = (string)column.Value;
						break;
					}
					case "rft_aulast" :
					{
						_Rft_aulast = (string)column.Value;
						break;
					}
					case "rft_aufirst" :
					{
						_Rft_aufirst = (string)column.Value;
						break;
					}
					case "rft_au_BOOK" :
					{
						_Rft_au_BOOK = (string)column.Value;
						break;
					}
					case "rft_au_DC" :
					{
						_Rft_au_DC = (string)column.Value;
						break;
					}
					case "rft_aucorp" :
					{
						_Rft_aucorp = (string)column.Value;
						break;
					}
					case "rft_place" :
					{
						_Rft_place = (string)column.Value;
						break;
					}
					case "rft_pub" :
					{
						_Rft_pub = (string)column.Value;
						break;
					}
					case "rft_publisher" :
					{
						_Rft_publisher = (string)column.Value;
						break;
					}
					case "rft_date_ITEM" :
					{
						_Rft_date_ITEM = (string)column.Value;
						break;
					}
					case "rft_date_TITLE" :
					{
						_Rft_date_TITLE = (string)column.Value;
						break;
					}
					case "rft_edition" :
					{
						_Rft_edition = (string)column.Value;
						break;
					}
					case "rft_tpages" :
					{
						_Rft_tpages = (int?)column.Value;
						break;
					}
					case "rft_issn" :
					{
						_Rft_issn = (string)column.Value;
						break;
					}
					case "rft_coden" :
					{
						_Rft_coden = (string)column.Value;
						break;
					}
					case "rft_subject" :
					{
						_Rft_subject = (string)column.Value;
						break;
					}
					case "rft_contributor_ITEM" :
					{
						_Rft_contributor_ITEM = (string)column.Value;
						break;
					}
					case "rft_contributor_TITLE" :
					{
						_Rft_contributor_TITLE = (string)column.Value;
						break;
					}
					case "rft_genre" :
					{
						_Rft_genre = (string)column.Value;
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
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10)]
		public int TitleID
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
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
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
		
		#region Lccn
		
		private string _Lccn = null;
		
		/// <summary>
		/// Column: lccn;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("lccn", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=125, IsNullable=true)]
		public string Lccn
		{
			get
			{
				return _Lccn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Lccn != value)
				{
					_Lccn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Lccn
		
		#region Oclc
		
		private string _Oclc = null;
		
		/// <summary>
		/// Column: oclc;
		/// DBMS data type: nvarchar(4000); Nullable;
		/// </summary>
		[ColumnDefinition("oclc", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=4000, IsNullable=true)]
		public string Oclc
		{
			get
			{
				return _Oclc;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_Oclc != value)
				{
					_Oclc = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Oclc
		
		#region Rft_title
		
		private string _Rft_title = null;
		
		/// <summary>
		/// Column: rft_title;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("rft_title", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Rft_title
		{
			get
			{
				return _Rft_title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rft_title != value)
				{
					_Rft_title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_title
		
		#region Rft_stitle
		
		private string _Rft_stitle = null;
		
		/// <summary>
		/// Column: rft_stitle;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_stitle", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_stitle
		{
			get
			{
				return _Rft_stitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_stitle != value)
				{
					_Rft_stitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_stitle
		
		#region Rft_volume
		
		private string _Rft_volume = string.Empty;
		
		/// <summary>
		/// Column: rft_volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("rft_volume", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=100)]
		public string Rft_volume
		{
			get
			{
				return _Rft_volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Rft_volume != value)
				{
					_Rft_volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_volume
		
		#region Rft_language
		
		private string _Rft_language = string.Empty;
		
		/// <summary>
		/// Column: rft_language;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("rft_language", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=10)]
		public string Rft_language
		{
			get
			{
				return _Rft_language;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_Rft_language != value)
				{
					_Rft_language = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_language
		
		#region Rft_isbn
		
		private string _Rft_isbn = null;
		
		/// <summary>
		/// Column: rft_isbn;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_isbn", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_isbn
		{
			get
			{
				return _Rft_isbn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_isbn != value)
				{
					_Rft_isbn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_isbn
		
		#region Rft_aulast
		
		private string _Rft_aulast = null;
		
		/// <summary>
		/// Column: rft_aulast;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("rft_aulast", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=255, IsNullable=true)]
		public string Rft_aulast
		{
			get
			{
				return _Rft_aulast;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_aulast != value)
				{
					_Rft_aulast = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_aulast
		
		#region Rft_aufirst
		
		private string _Rft_aufirst = null;
		
		/// <summary>
		/// Column: rft_aufirst;
		/// DBMS data type: nvarchar(4000); Nullable;
		/// </summary>
		[ColumnDefinition("rft_aufirst", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=4000, IsNullable=true)]
		public string Rft_aufirst
		{
			get
			{
				return _Rft_aufirst;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_Rft_aufirst != value)
				{
					_Rft_aufirst = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_aufirst
		
		#region Rft_au_BOOK
		
		private string _Rft_au_BOOK = null;
		
		/// <summary>
		/// Column: rft_au_BOOK;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("rft_au_BOOK", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Rft_au_BOOK
		{
			get
			{
				return _Rft_au_BOOK;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rft_au_BOOK != value)
				{
					_Rft_au_BOOK = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_au_BOOK
		
		#region Rft_au_DC
		
		private string _Rft_au_DC = null;
		
		/// <summary>
		/// Column: rft_au_DC;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("rft_au_DC", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Rft_au_DC
		{
			get
			{
				return _Rft_au_DC;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rft_au_DC != value)
				{
					_Rft_au_DC = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_au_DC
		
		#region Rft_aucorp
		
		private string _Rft_aucorp = null;
		
		/// <summary>
		/// Column: rft_aucorp;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("rft_aucorp", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=255, IsNullable=true)]
		public string Rft_aucorp
		{
			get
			{
				return _Rft_aucorp;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_aucorp != value)
				{
					_Rft_aucorp = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_aucorp
		
		#region Rft_place
		
		private string _Rft_place = string.Empty;
		
		/// <summary>
		/// Column: rft_place;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("rft_place", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=150)]
		public string Rft_place
		{
			get
			{
				return _Rft_place;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_Rft_place != value)
				{
					_Rft_place = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_place
		
		#region Rft_pub
		
		private string _Rft_pub = string.Empty;
		
		/// <summary>
		/// Column: rft_pub;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("rft_pub", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=255)]
		public string Rft_pub
		{
			get
			{
				return _Rft_pub;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_pub != value)
				{
					_Rft_pub = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_pub
		
		#region Rft_publisher
		
		private string _Rft_publisher = string.Empty;
		
		/// <summary>
		/// Column: rft_publisher;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("rft_publisher", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=255)]
		public string Rft_publisher
		{
			get
			{
				return _Rft_publisher;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_publisher != value)
				{
					_Rft_publisher = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_publisher
		
		#region Rft_date_ITEM
		
		private string _Rft_date_ITEM = string.Empty;
		
		/// <summary>
		/// Column: rft_date_ITEM;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("rft_date_ITEM", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=20)]
		public string Rft_date_ITEM
		{
			get
			{
				return _Rft_date_ITEM;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Rft_date_ITEM != value)
				{
					_Rft_date_ITEM = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_date_ITEM
		
		#region Rft_date_TITLE
		
		private string _Rft_date_TITLE = string.Empty;
		
		/// <summary>
		/// Column: rft_date_TITLE;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("rft_date_TITLE", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=20)]
		public string Rft_date_TITLE
		{
			get
			{
				return _Rft_date_TITLE;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Rft_date_TITLE != value)
				{
					_Rft_date_TITLE = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_date_TITLE
		
		#region Rft_edition
		
		private string _Rft_edition = string.Empty;
		
		/// <summary>
		/// Column: rft_edition;
		/// DBMS data type: nvarchar(450);
		/// </summary>
		[ColumnDefinition("rft_edition", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=450)]
		public string Rft_edition
		{
			get
			{
				return _Rft_edition;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_Rft_edition != value)
				{
					_Rft_edition = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_edition
		
		#region Rft_tpages
		
		private int? _Rft_tpages = null;
		
		/// <summary>
		/// Column: rft_tpages;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("rft_tpages", DbTargetType=SqlDbType.Int, Ordinal=21, NumericPrecision=10, IsNullable=true)]
		public int? Rft_tpages
		{
			get
			{
				return _Rft_tpages;
			}
			set
			{
				if (_Rft_tpages != value)
				{
					_Rft_tpages = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_tpages
		
		#region Rft_issn
		
		private string _Rft_issn = null;
		
		/// <summary>
		/// Column: rft_issn;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_issn", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_issn
		{
			get
			{
				return _Rft_issn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_issn != value)
				{
					_Rft_issn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_issn
		
		#region Rft_coden
		
		private string _Rft_coden = null;
		
		/// <summary>
		/// Column: rft_coden;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_coden", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_coden
		{
			get
			{
				return _Rft_coden;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_coden != value)
				{
					_Rft_coden = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_coden
		
		#region Rft_subject
		
		private string _Rft_subject = null;
		
		/// <summary>
		/// Column: rft_subject;
		/// DBMS data type: nvarchar(1024); Nullable;
		/// </summary>
		[ColumnDefinition("rft_subject", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=1024, IsNullable=true)]
		public string Rft_subject
		{
			get
			{
				return _Rft_subject;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1024);
				if (_Rft_subject != value)
				{
					_Rft_subject = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_subject
		
		#region Rft_contributor_ITEM
		
		private string _Rft_contributor_ITEM = null;
		
		/// <summary>
		/// Column: rft_contributor_ITEM;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("rft_contributor_ITEM", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=255, IsNullable=true)]
		public string Rft_contributor_ITEM
		{
			get
			{
				return _Rft_contributor_ITEM;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_contributor_ITEM != value)
				{
					_Rft_contributor_ITEM = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_contributor_ITEM
		
		#region Rft_contributor_TITLE
		
		private string _Rft_contributor_TITLE = null;
		
		/// <summary>
		/// Column: rft_contributor_TITLE;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("rft_contributor_TITLE", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=255, IsNullable=true)]
		public string Rft_contributor_TITLE
		{
			get
			{
				return _Rft_contributor_TITLE;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_contributor_TITLE != value)
				{
					_Rft_contributor_TITLE = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_contributor_TITLE
		
		#region Rft_genre
		
		private string _Rft_genre = string.Empty;
		
		/// <summary>
		/// Column: rft_genre;
		/// DBMS data type: varchar(7);
		/// </summary>
		[ColumnDefinition("rft_genre", DbTargetType=SqlDbType.VarChar, Ordinal=27, CharacterMaxLength=7)]
		public string Rft_genre
		{
			get
			{
				return _Rft_genre;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 7);
				if (_Rft_genre != value)
				{
					_Rft_genre = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_genre
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ItemCOinSView"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ItemCOinSView"/>, 
		/// returns an instance of <see cref="__ItemCOinSView"/>; otherwise returns null.</returns>
		public static new __ItemCOinSView FromArray(byte[] byteArray)
		{
			__ItemCOinSView o = null;
			
			try
			{
				o = (__ItemCOinSView) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ItemCOinSView"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ItemCOinSView"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ItemCOinSView)
			{
				__ItemCOinSView o = (__ItemCOinSView) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleID == TitleID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.Lccn) == GetComparisonString(Lccn) &&
					GetComparisonString(o.Oclc) == GetComparisonString(Oclc) &&
					GetComparisonString(o.Rft_title) == GetComparisonString(Rft_title) &&
					GetComparisonString(o.Rft_stitle) == GetComparisonString(Rft_stitle) &&
					GetComparisonString(o.Rft_volume) == GetComparisonString(Rft_volume) &&
					GetComparisonString(o.Rft_language) == GetComparisonString(Rft_language) &&
					GetComparisonString(o.Rft_isbn) == GetComparisonString(Rft_isbn) &&
					GetComparisonString(o.Rft_aulast) == GetComparisonString(Rft_aulast) &&
					GetComparisonString(o.Rft_aufirst) == GetComparisonString(Rft_aufirst) &&
					GetComparisonString(o.Rft_au_BOOK) == GetComparisonString(Rft_au_BOOK) &&
					GetComparisonString(o.Rft_au_DC) == GetComparisonString(Rft_au_DC) &&
					GetComparisonString(o.Rft_aucorp) == GetComparisonString(Rft_aucorp) &&
					GetComparisonString(o.Rft_place) == GetComparisonString(Rft_place) &&
					GetComparisonString(o.Rft_pub) == GetComparisonString(Rft_pub) &&
					GetComparisonString(o.Rft_publisher) == GetComparisonString(Rft_publisher) &&
					GetComparisonString(o.Rft_date_ITEM) == GetComparisonString(Rft_date_ITEM) &&
					GetComparisonString(o.Rft_date_TITLE) == GetComparisonString(Rft_date_TITLE) &&
					GetComparisonString(o.Rft_edition) == GetComparisonString(Rft_edition) &&
					o.Rft_tpages == Rft_tpages &&
					GetComparisonString(o.Rft_issn) == GetComparisonString(Rft_issn) &&
					GetComparisonString(o.Rft_coden) == GetComparisonString(Rft_coden) &&
					GetComparisonString(o.Rft_subject) == GetComparisonString(Rft_subject) &&
					GetComparisonString(o.Rft_contributor_ITEM) == GetComparisonString(Rft_contributor_ITEM) &&
					GetComparisonString(o.Rft_contributor_TITLE) == GetComparisonString(Rft_contributor_TITLE) &&
					GetComparisonString(o.Rft_genre) == GetComparisonString(Rft_genre) 
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
				throw new ArgumentException("Argument is not of type __ItemCOinSView");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ItemCOinSView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemCOinSView"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ItemCOinSView a, __ItemCOinSView b)
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
		/// <param name="a">The first <see cref="__ItemCOinSView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ItemCOinSView"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ItemCOinSView a, __ItemCOinSView b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ItemCOinSView"/> object to compare with the current <see cref="__ItemCOinSView"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ItemCOinSView))
			{
				return false;
			}
			
			return this == (__ItemCOinSView) obj;
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
		/// list.Sort(SortOrder.Ascending, __ItemCOinSView.SortColumn.TitleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleID = "TitleID";	
			public const string ItemID = "ItemID";	
			public const string Lccn = "Lccn";	
			public const string Oclc = "Oclc";	
			public const string Rft_title = "Rft_title";	
			public const string Rft_stitle = "Rft_stitle";	
			public const string Rft_volume = "Rft_volume";	
			public const string Rft_language = "Rft_language";	
			public const string Rft_isbn = "Rft_isbn";	
			public const string Rft_aulast = "Rft_aulast";	
			public const string Rft_aufirst = "Rft_aufirst";	
			public const string Rft_au_BOOK = "Rft_au_BOOK";	
			public const string Rft_au_DC = "Rft_au_DC";	
			public const string Rft_aucorp = "Rft_aucorp";	
			public const string Rft_place = "Rft_place";	
			public const string Rft_pub = "Rft_pub";	
			public const string Rft_publisher = "Rft_publisher";	
			public const string Rft_date_ITEM = "Rft_date_ITEM";	
			public const string Rft_date_TITLE = "Rft_date_TITLE";	
			public const string Rft_edition = "Rft_edition";	
			public const string Rft_tpages = "Rft_tpages";	
			public const string Rft_issn = "Rft_issn";	
			public const string Rft_coden = "Rft_coden";	
			public const string Rft_subject = "Rft_subject";	
			public const string Rft_contributor_ITEM = "Rft_contributor_ITEM";	
			public const string Rft_contributor_TITLE = "Rft_contributor_TITLE";	
			public const string Rft_genre = "Rft_genre";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
