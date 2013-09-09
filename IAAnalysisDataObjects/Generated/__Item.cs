
// Generated 3/24/2009 1:57:09 PM
// Do not modify the contents of this code file.
// This abstract class __Item is based upon Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DataObjects
// {
//		[Serializable]
// 		public class Item : __Item
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.IAAnalysis.DataObjects
{	
	[Serializable]
	public abstract class __Item : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Item()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="identifier"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="sponsor"></param>
		/// <param name="contributor"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="collectionLibrary"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="curationState"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="volume"></param>
		/// <param name="scanDate"></param>
		/// <param name="addedDate"></param>
		/// <param name="publicDate"></param>
		/// <param name="updateDate"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="metaGetStatus"></param>
		/// <param name="marcGetStatus"></param>
		/// <param name="creationDate"></param>
		public __Item(int itemID, 
			string identifier, 
			string mARCLeader, 
			string sponsor, 
			string contributor, 
			string scanningCenter, 
			string collectionLibrary, 
			string callNumber, 
			int? imageCount, 
			string curationState, 
			string possibleCopyrightStatus, 
			string volume, 
			string scanDate, 
			DateTime? addedDate, 
			DateTime? publicDate, 
			DateTime? updateDate, 
			string sponsorDate, 
			int itemStatusID, 
			string metaGetStatus, 
			string marcGetStatus, 
			DateTime creationDate) : this()
		{
			_ItemID = itemID;
			Identifier = identifier;
			MARCLeader = mARCLeader;
			Sponsor = sponsor;
			Contributor = contributor;
			ScanningCenter = scanningCenter;
			CollectionLibrary = collectionLibrary;
			CallNumber = callNumber;
			ImageCount = imageCount;
			CurationState = curationState;
			PossibleCopyrightStatus = possibleCopyrightStatus;
			Volume = volume;
			ScanDate = scanDate;
			AddedDate = addedDate;
			PublicDate = publicDate;
			UpdateDate = updateDate;
			SponsorDate = sponsorDate;
			ItemStatusID = itemStatusID;
			MetaGetStatus = metaGetStatus;
			MarcGetStatus = marcGetStatus;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Item()
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
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "Identifier" :
					{
						_Identifier = (string)column.Value;
						break;
					}
					case "MARCLeader" :
					{
						_MARCLeader = (string)column.Value;
						break;
					}
					case "Sponsor" :
					{
						_Sponsor = (string)column.Value;
						break;
					}
					case "Contributor" :
					{
						_Contributor = (string)column.Value;
						break;
					}
					case "ScanningCenter" :
					{
						_ScanningCenter = (string)column.Value;
						break;
					}
					case "CollectionLibrary" :
					{
						_CollectionLibrary = (string)column.Value;
						break;
					}
					case "CallNumber" :
					{
						_CallNumber = (string)column.Value;
						break;
					}
					case "ImageCount" :
					{
						_ImageCount = (int?)column.Value;
						break;
					}
					case "CurationState" :
					{
						_CurationState = (string)column.Value;
						break;
					}
					case "PossibleCopyrightStatus" :
					{
						_PossibleCopyrightStatus = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "ScanDate" :
					{
						_ScanDate = (string)column.Value;
						break;
					}
					case "AddedDate" :
					{
						_AddedDate = (DateTime?)column.Value;
						break;
					}
					case "PublicDate" :
					{
						_PublicDate = (DateTime?)column.Value;
						break;
					}
					case "UpdateDate" :
					{
						_UpdateDate = (DateTime?)column.Value;
						break;
					}
					case "SponsorDate" :
					{
						_SponsorDate = (string)column.Value;
						break;
					}
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "MetaGetStatus" :
					{
						_MetaGetStatus = (string)column.Value;
						break;
					}
					case "MarcGetStatus" :
					{
						_MarcGetStatus = (string)column.Value;
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
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
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
		
		#endregion ItemID
		
		#region Identifier
		
		private string _Identifier = string.Empty;
		
		/// <summary>
		/// Column: Identifier;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Identifier", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string Identifier
		{
			get
			{
				return _Identifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Identifier != value)
				{
					_Identifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Identifier
		
		#region MARCLeader
		
		private string _MARCLeader = string.Empty;
		
		/// <summary>
		/// Column: MARCLeader;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("MARCLeader", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=200)]
		public string MARCLeader
		{
			get
			{
				return _MARCLeader;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_MARCLeader != value)
				{
					_MARCLeader = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCLeader
		
		#region Sponsor
		
		private string _Sponsor = string.Empty;
		
		/// <summary>
		/// Column: Sponsor;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Sponsor", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string Sponsor
		{
			get
			{
				return _Sponsor;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Sponsor != value)
				{
					_Sponsor = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Sponsor
		
		#region Contributor
		
		private string _Contributor = string.Empty;
		
		/// <summary>
		/// Column: Contributor;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Contributor", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string Contributor
		{
			get
			{
				return _Contributor;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Contributor != value)
				{
					_Contributor = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Contributor
		
		#region ScanningCenter
		
		private string _ScanningCenter = string.Empty;
		
		/// <summary>
		/// Column: ScanningCenter;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ScanningCenter", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50)]
		public string ScanningCenter
		{
			get
			{
				return _ScanningCenter;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ScanningCenter != value)
				{
					_ScanningCenter = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningCenter
		
		#region CollectionLibrary
		
		private string _CollectionLibrary = string.Empty;
		
		/// <summary>
		/// Column: CollectionLibrary;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("CollectionLibrary", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=20)]
		public string CollectionLibrary
		{
			get
			{
				return _CollectionLibrary;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_CollectionLibrary != value)
				{
					_CollectionLibrary = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionLibrary
		
		#region CallNumber
		
		private string _CallNumber = string.Empty;
		
		/// <summary>
		/// Column: CallNumber;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50)]
		public string CallNumber
		{
			get
			{
				return _CallNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CallNumber != value)
				{
					_CallNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CallNumber
		
		#region ImageCount
		
		private int? _ImageCount = null;
		
		/// <summary>
		/// Column: ImageCount;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImageCount", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? ImageCount
		{
			get
			{
				return _ImageCount;
			}
			set
			{
				if (_ImageCount != value)
				{
					_ImageCount = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImageCount
		
		#region CurationState
		
		private string _CurationState = string.Empty;
		
		/// <summary>
		/// Column: CurationState;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CurationState", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=50)]
		public string CurationState
		{
			get
			{
				return _CurationState;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CurationState != value)
				{
					_CurationState = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CurationState
		
		#region PossibleCopyrightStatus
		
		private string _PossibleCopyrightStatus = string.Empty;
		
		/// <summary>
		/// Column: PossibleCopyrightStatus;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("PossibleCopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=50)]
		public string PossibleCopyrightStatus
		{
			get
			{
				return _PossibleCopyrightStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_PossibleCopyrightStatus != value)
				{
					_PossibleCopyrightStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PossibleCopyrightStatus
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=200)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region ScanDate
		
		private string _ScanDate = string.Empty;
		
		/// <summary>
		/// Column: ScanDate;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ScanDate", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=50)]
		public string ScanDate
		{
			get
			{
				return _ScanDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ScanDate != value)
				{
					_ScanDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanDate
		
		#region AddedDate
		
		private DateTime? _AddedDate = null;
		
		/// <summary>
		/// Column: AddedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("AddedDate", DbTargetType=SqlDbType.DateTime, Ordinal=14, IsNullable=true)]
		public DateTime? AddedDate
		{
			get
			{
				return _AddedDate;
			}
			set
			{
				if (_AddedDate != value)
				{
					_AddedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AddedDate
		
		#region PublicDate
		
		private DateTime? _PublicDate = null;
		
		/// <summary>
		/// Column: PublicDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PublicDate", DbTargetType=SqlDbType.DateTime, Ordinal=15, IsNullable=true)]
		public DateTime? PublicDate
		{
			get
			{
				return _PublicDate;
			}
			set
			{
				if (_PublicDate != value)
				{
					_PublicDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicDate
		
		#region UpdateDate
		
		private DateTime? _UpdateDate = null;
		
		/// <summary>
		/// Column: UpdateDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("UpdateDate", DbTargetType=SqlDbType.DateTime, Ordinal=16, IsNullable=true)]
		public DateTime? UpdateDate
		{
			get
			{
				return _UpdateDate;
			}
			set
			{
				if (_UpdateDate != value)
				{
					_UpdateDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion UpdateDate
		
		#region SponsorDate
		
		private string _SponsorDate = null;
		
		/// <summary>
		/// Column: SponsorDate;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("SponsorDate", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=50, IsNullable=true)]
		public string SponsorDate
		{
			get
			{
				return _SponsorDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SponsorDate != value)
				{
					_SponsorDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SponsorDate
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=18, NumericPrecision=10)]
		public int ItemStatusID
		{
			get
			{
				return _ItemStatusID;
			}
			set
			{
				if (_ItemStatusID != value)
				{
					_ItemStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemStatusID
		
		#region MetaGetStatus
		
		private string _MetaGetStatus = string.Empty;
		
		/// <summary>
		/// Column: MetaGetStatus;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("MetaGetStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=30)]
		public string MetaGetStatus
		{
			get
			{
				return _MetaGetStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_MetaGetStatus != value)
				{
					_MetaGetStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MetaGetStatus
		
		#region MarcGetStatus
		
		private string _MarcGetStatus = string.Empty;
		
		/// <summary>
		/// Column: MarcGetStatus;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("MarcGetStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=30)]
		public string MarcGetStatus
		{
			get
			{
				return _MarcGetStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_MarcGetStatus != value)
				{
					_MarcGetStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcGetStatus
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=21)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Item"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Item"/>, 
		/// returns an instance of <see cref="__Item"/>; otherwise returns null.</returns>
		public static new __Item FromArray(byte[] byteArray)
		{
			__Item o = null;
			
			try
			{
				o = (__Item) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Item"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Item"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Item)
			{
				__Item o = (__Item) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemID == ItemID &&
					GetComparisonString(o.Identifier) == GetComparisonString(Identifier) &&
					GetComparisonString(o.MARCLeader) == GetComparisonString(MARCLeader) &&
					GetComparisonString(o.Sponsor) == GetComparisonString(Sponsor) &&
					GetComparisonString(o.Contributor) == GetComparisonString(Contributor) &&
					GetComparisonString(o.ScanningCenter) == GetComparisonString(ScanningCenter) &&
					GetComparisonString(o.CollectionLibrary) == GetComparisonString(CollectionLibrary) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					o.ImageCount == ImageCount &&
					GetComparisonString(o.CurationState) == GetComparisonString(CurationState) &&
					GetComparisonString(o.PossibleCopyrightStatus) == GetComparisonString(PossibleCopyrightStatus) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.ScanDate) == GetComparisonString(ScanDate) &&
					o.AddedDate == AddedDate &&
					o.PublicDate == PublicDate &&
					o.UpdateDate == UpdateDate &&
					GetComparisonString(o.SponsorDate) == GetComparisonString(SponsorDate) &&
					o.ItemStatusID == ItemStatusID &&
					GetComparisonString(o.MetaGetStatus) == GetComparisonString(MetaGetStatus) &&
					GetComparisonString(o.MarcGetStatus) == GetComparisonString(MarcGetStatus) &&
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
				throw new ArgumentException("Argument is not of type __Item");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Item a, __Item b)
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
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Item a, __Item b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Item"/> object to compare with the current <see cref="__Item"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Item))
			{
				return false;
			}
			
			return this == (__Item) obj;
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
		/// list.Sort(SortOrder.Ascending, __Item.SortColumn.ItemID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemID = "ItemID";	
			public const string Identifier = "Identifier";	
			public const string MARCLeader = "MARCLeader";	
			public const string Sponsor = "Sponsor";	
			public const string Contributor = "Contributor";	
			public const string ScanningCenter = "ScanningCenter";	
			public const string CollectionLibrary = "CollectionLibrary";	
			public const string CallNumber = "CallNumber";	
			public const string ImageCount = "ImageCount";	
			public const string CurationState = "CurationState";	
			public const string PossibleCopyrightStatus = "PossibleCopyrightStatus";	
			public const string Volume = "Volume";	
			public const string ScanDate = "ScanDate";	
			public const string AddedDate = "AddedDate";	
			public const string PublicDate = "PublicDate";	
			public const string UpdateDate = "UpdateDate";	
			public const string SponsorDate = "SponsorDate";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string MetaGetStatus = "MetaGetStatus";	
			public const string MarcGetStatus = "MarcGetStatus";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
