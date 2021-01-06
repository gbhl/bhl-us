
// Generated 1/5/2021 2:10:51 PM
// Do not modify the contents of this code file.
// This abstract class __BotanicusHarvestLog is based upon dbo.BotanicusHarvestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class BotanicusHarvestLog : __BotanicusHarvestLog
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
	public abstract class __BotanicusHarvestLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __BotanicusHarvestLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="botanicusHarvestLogID"></param>
		/// <param name="harvestStartDate"></param>
		/// <param name="harvestEndDate"></param>
		/// <param name="automaticHarvest"></param>
		/// <param name="successfulHarvest"></param>
		/// <param name="title"></param>
		/// <param name="titleTag"></param>
		/// <param name="titleCreator"></param>
		/// <param name="creator"></param>
		/// <param name="item"></param>
		/// <param name="page"></param>
		/// <param name="indicatedPage"></param>
		/// <param name="pagePageType"></param>
		/// <param name="pageName"></param>
		public __BotanicusHarvestLog(int botanicusHarvestLogID, 
			DateTime harvestStartDate, 
			DateTime harvestEndDate, 
			bool automaticHarvest, 
			bool successfulHarvest, 
			int title, 
			int titleTag, 
			int titleCreator, 
			int creator, 
			int item, 
			int page, 
			int indicatedPage, 
			int pagePageType, 
			int pageName) : this()
		{
			_BotanicusHarvestLogID = botanicusHarvestLogID;
			HarvestStartDate = harvestStartDate;
			HarvestEndDate = harvestEndDate;
			AutomaticHarvest = automaticHarvest;
			SuccessfulHarvest = successfulHarvest;
			Title = title;
			TitleTag = titleTag;
			TitleCreator = titleCreator;
			Creator = creator;
			Item = item;
			Page = page;
			IndicatedPage = indicatedPage;
			PagePageType = pagePageType;
			PageName = pageName;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__BotanicusHarvestLog()
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
					case "BotanicusHarvestLogID" :
					{
						_BotanicusHarvestLogID = (int)column.Value;
						break;
					}
					case "HarvestStartDate" :
					{
						_HarvestStartDate = (DateTime)column.Value;
						break;
					}
					case "HarvestEndDate" :
					{
						_HarvestEndDate = (DateTime)column.Value;
						break;
					}
					case "AutomaticHarvest" :
					{
						_AutomaticHarvest = (bool)column.Value;
						break;
					}
					case "SuccessfulHarvest" :
					{
						_SuccessfulHarvest = (bool)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (int)column.Value;
						break;
					}
					case "TitleTag" :
					{
						_TitleTag = (int)column.Value;
						break;
					}
					case "TitleCreator" :
					{
						_TitleCreator = (int)column.Value;
						break;
					}
					case "Creator" :
					{
						_Creator = (int)column.Value;
						break;
					}
					case "Item" :
					{
						_Item = (int)column.Value;
						break;
					}
					case "Page" :
					{
						_Page = (int)column.Value;
						break;
					}
					case "IndicatedPage" :
					{
						_IndicatedPage = (int)column.Value;
						break;
					}
					case "PagePageType" :
					{
						_PagePageType = (int)column.Value;
						break;
					}
					case "PageName" :
					{
						_PageName = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region BotanicusHarvestLogID
		
		private int _BotanicusHarvestLogID = default(int);
		
		/// <summary>
		/// Column: BotanicusHarvestLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("BotanicusHarvestLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int BotanicusHarvestLogID
		{
			get
			{
				return _BotanicusHarvestLogID;
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
		
		#endregion BotanicusHarvestLogID
		
		#region HarvestStartDate
		
		private DateTime _HarvestStartDate;
		
		/// <summary>
		/// Column: HarvestStartDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("HarvestStartDate", DbTargetType=SqlDbType.DateTime, Ordinal=2)]
		public DateTime HarvestStartDate
		{
			get
			{
				return _HarvestStartDate;
			}
			set
			{
				if (_HarvestStartDate != value)
				{
					_HarvestStartDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestStartDate
		
		#region HarvestEndDate
		
		private DateTime _HarvestEndDate;
		
		/// <summary>
		/// Column: HarvestEndDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("HarvestEndDate", DbTargetType=SqlDbType.DateTime, Ordinal=3)]
		public DateTime HarvestEndDate
		{
			get
			{
				return _HarvestEndDate;
			}
			set
			{
				if (_HarvestEndDate != value)
				{
					_HarvestEndDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestEndDate
		
		#region AutomaticHarvest
		
		private bool _AutomaticHarvest = false;
		
		/// <summary>
		/// Column: AutomaticHarvest;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("AutomaticHarvest", DbTargetType=SqlDbType.Bit, Ordinal=4)]
		public bool AutomaticHarvest
		{
			get
			{
				return _AutomaticHarvest;
			}
			set
			{
				if (_AutomaticHarvest != value)
				{
					_AutomaticHarvest = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AutomaticHarvest
		
		#region SuccessfulHarvest
		
		private bool _SuccessfulHarvest = false;
		
		/// <summary>
		/// Column: SuccessfulHarvest;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("SuccessfulHarvest", DbTargetType=SqlDbType.Bit, Ordinal=5)]
		public bool SuccessfulHarvest
		{
			get
			{
				return _SuccessfulHarvest;
			}
			set
			{
				if (_SuccessfulHarvest != value)
				{
					_SuccessfulHarvest = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SuccessfulHarvest
		
		#region Title
		
		private int _Title = default(int);
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10)]
		public int Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region TitleTag
		
		private int _TitleTag = default(int);
		
		/// <summary>
		/// Column: TitleTag;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleTag", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
		public int TitleTag
		{
			get
			{
				return _TitleTag;
			}
			set
			{
				if (_TitleTag != value)
				{
					_TitleTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleTag
		
		#region TitleCreator
		
		private int _TitleCreator = default(int);
		
		/// <summary>
		/// Column: TitleCreator;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleCreator", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int TitleCreator
		{
			get
			{
				return _TitleCreator;
			}
			set
			{
				if (_TitleCreator != value)
				{
					_TitleCreator = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleCreator
		
		#region Creator
		
		private int _Creator = default(int);
		
		/// <summary>
		/// Column: Creator;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Creator", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int Creator
		{
			get
			{
				return _Creator;
			}
			set
			{
				if (_Creator != value)
				{
					_Creator = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Creator
		
		#region Item
		
		private int _Item = default(int);
		
		/// <summary>
		/// Column: Item;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Item", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
		public int Item
		{
			get
			{
				return _Item;
			}
			set
			{
				if (_Item != value)
				{
					_Item = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Item
		
		#region Page
		
		private int _Page = default(int);
		
		/// <summary>
		/// Column: Page;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Page", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10)]
		public int Page
		{
			get
			{
				return _Page;
			}
			set
			{
				if (_Page != value)
				{
					_Page = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Page
		
		#region IndicatedPage
		
		private int _IndicatedPage = default(int);
		
		/// <summary>
		/// Column: IndicatedPage;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("IndicatedPage", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10)]
		public int IndicatedPage
		{
			get
			{
				return _IndicatedPage;
			}
			set
			{
				if (_IndicatedPage != value)
				{
					_IndicatedPage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IndicatedPage
		
		#region PagePageType
		
		private int _PagePageType = default(int);
		
		/// <summary>
		/// Column: PagePageType;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PagePageType", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10)]
		public int PagePageType
		{
			get
			{
				return _PagePageType;
			}
			set
			{
				if (_PagePageType != value)
				{
					_PagePageType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePageType
		
		#region PageName
		
		private int _PageName = default(int);
		
		/// <summary>
		/// Column: PageName;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageName", DbTargetType=SqlDbType.Int, Ordinal=14, NumericPrecision=10)]
		public int PageName
		{
			get
			{
				return _PageName;
			}
			set
			{
				if (_PageName != value)
				{
					_PageName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageName
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__BotanicusHarvestLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__BotanicusHarvestLog"/>, 
		/// returns an instance of <see cref="__BotanicusHarvestLog"/>; otherwise returns null.</returns>
		public static new __BotanicusHarvestLog FromArray(byte[] byteArray)
		{
			__BotanicusHarvestLog o = null;
			
			try
			{
				o = (__BotanicusHarvestLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__BotanicusHarvestLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__BotanicusHarvestLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __BotanicusHarvestLog)
			{
				__BotanicusHarvestLog o = (__BotanicusHarvestLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.BotanicusHarvestLogID == BotanicusHarvestLogID &&
					o.HarvestStartDate == HarvestStartDate &&
					o.HarvestEndDate == HarvestEndDate &&
					o.AutomaticHarvest == AutomaticHarvest &&
					o.SuccessfulHarvest == SuccessfulHarvest &&
					o.Title == Title &&
					o.TitleTag == TitleTag &&
					o.TitleCreator == TitleCreator &&
					o.Creator == Creator &&
					o.Item == Item &&
					o.Page == Page &&
					o.IndicatedPage == IndicatedPage &&
					o.PagePageType == PagePageType &&
					o.PageName == PageName 
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
				throw new ArgumentException("Argument is not of type __BotanicusHarvestLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__BotanicusHarvestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BotanicusHarvestLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__BotanicusHarvestLog a, __BotanicusHarvestLog b)
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
		/// <param name="a">The first <see cref="__BotanicusHarvestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BotanicusHarvestLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__BotanicusHarvestLog a, __BotanicusHarvestLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__BotanicusHarvestLog"/> object to compare with the current <see cref="__BotanicusHarvestLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __BotanicusHarvestLog))
			{
				return false;
			}
			
			return this == (__BotanicusHarvestLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __BotanicusHarvestLog.SortColumn.BotanicusHarvestLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string BotanicusHarvestLogID = "BotanicusHarvestLogID";	
			public const string HarvestStartDate = "HarvestStartDate";	
			public const string HarvestEndDate = "HarvestEndDate";	
			public const string AutomaticHarvest = "AutomaticHarvest";	
			public const string SuccessfulHarvest = "SuccessfulHarvest";	
			public const string Title = "Title";	
			public const string TitleTag = "TitleTag";	
			public const string TitleCreator = "TitleCreator";	
			public const string Creator = "Creator";	
			public const string Item = "Item";	
			public const string Page = "Page";	
			public const string IndicatedPage = "IndicatedPage";	
			public const string PagePageType = "PagePageType";	
			public const string PageName = "PageName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

