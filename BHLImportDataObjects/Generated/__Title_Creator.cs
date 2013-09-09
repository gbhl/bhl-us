
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This abstract class __Title_Creator is based upon Title_Creator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class Title_Creator : __Title_Creator
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
	public abstract class __Title_Creator : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Title_Creator()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleCreatorID"></param>
		/// <param name="creatorName"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="creatorRoleTypeID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Title_Creator(int titleCreatorID, 
			string creatorName, 
			string mARCCreator_a, 
			string mARCCreator_b, 
			string mARCCreator_c, 
			string mARCCreator_d, 
			int creatorRoleTypeID, 
			string importKey, 
			int importStatusID, 
			int? importSourceID, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			int? externalCreationUser, 
			int? externalLastModifiedUser, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_TitleCreatorID = titleCreatorID;
			CreatorName = creatorName;
			MARCCreator_a = mARCCreator_a;
			MARCCreator_b = mARCCreator_b;
			MARCCreator_c = mARCCreator_c;
			MARCCreator_d = mARCCreator_d;
			CreatorRoleTypeID = creatorRoleTypeID;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ExternalCreationUser = externalCreationUser;
			ExternalLastModifiedUser = externalLastModifiedUser;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Title_Creator()
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
					case "TitleCreatorID" :
					{
						_TitleCreatorID = (int)column.Value;
						break;
					}
					case "CreatorName" :
					{
						_CreatorName = (string)column.Value;
						break;
					}
					case "MARCCreator_a" :
					{
						_MARCCreator_a = (string)column.Value;
						break;
					}
					case "MARCCreator_b" :
					{
						_MARCCreator_b = (string)column.Value;
						break;
					}
					case "MARCCreator_c" :
					{
						_MARCCreator_c = (string)column.Value;
						break;
					}
					case "MARCCreator_d" :
					{
						_MARCCreator_d = (string)column.Value;
						break;
					}
					case "CreatorRoleTypeID" :
					{
						_CreatorRoleTypeID = (int)column.Value;
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
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region TitleCreatorID
		
		private int _TitleCreatorID = default(int);
		
		/// <summary>
		/// Column: TitleCreatorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleCreatorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleCreatorID
		{
			get
			{
				return _TitleCreatorID;
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
		
		#endregion TitleCreatorID
		
		#region CreatorName
		
		private string _CreatorName = string.Empty;
		
		/// <summary>
		/// Column: CreatorName;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("CreatorName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=255)]
		public string CreatorName
		{
			get
			{
				return _CreatorName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_CreatorName != value)
				{
					_CreatorName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorName
		
		#region MARCCreator_a
		
		private string _MARCCreator_a = null;
		
		/// <summary>
		/// Column: MARCCreator_a;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_a", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_a
		{
			get
			{
				return _MARCCreator_a;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_a != value)
				{
					_MARCCreator_a = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_a
		
		#region MARCCreator_b
		
		private string _MARCCreator_b = null;
		
		/// <summary>
		/// Column: MARCCreator_b;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_b", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_b
		{
			get
			{
				return _MARCCreator_b;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_b != value)
				{
					_MARCCreator_b = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_b
		
		#region MARCCreator_c
		
		private string _MARCCreator_c = null;
		
		/// <summary>
		/// Column: MARCCreator_c;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_c", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_c
		{
			get
			{
				return _MARCCreator_c;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_c != value)
				{
					_MARCCreator_c = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_c
		
		#region MARCCreator_d
		
		private string _MARCCreator_d = null;
		
		/// <summary>
		/// Column: MARCCreator_d;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_d", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_d
		{
			get
			{
				return _MARCCreator_d;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_d != value)
				{
					_MARCCreator_d = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_d
		
		#region CreatorRoleTypeID
		
		private int _CreatorRoleTypeID = default(int);
		
		/// <summary>
		/// Column: CreatorRoleTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreatorRoleTypeID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
		public int CreatorRoleTypeID
		{
			get
			{
				return _CreatorRoleTypeID;
			}
			set
			{
				if (_CreatorRoleTypeID != value)
				{
					_CreatorRoleTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorRoleTypeID
		
		#region ImportKey
		
		private string _ImportKey = string.Empty;
		
		/// <summary>
		/// Column: ImportKey;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ImportKey", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50)]
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
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsInForeignKey=true)]
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
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=11, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12, IsNullable=true)]
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
		[ColumnDefinition("ExternalCreationUser", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedUser", DbTargetType=SqlDbType.Int, Ordinal=14, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=15, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=16)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=17)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Title_Creator"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Title_Creator"/>, 
		/// returns an instance of <see cref="__Title_Creator"/>; otherwise returns null.</returns>
		public static new __Title_Creator FromArray(byte[] byteArray)
		{
			__Title_Creator o = null;
			
			try
			{
				o = (__Title_Creator) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Title_Creator"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Title_Creator"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Title_Creator)
			{
				__Title_Creator o = (__Title_Creator) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleCreatorID == TitleCreatorID &&
					GetComparisonString(o.CreatorName) == GetComparisonString(CreatorName) &&
					GetComparisonString(o.MARCCreator_a) == GetComparisonString(MARCCreator_a) &&
					GetComparisonString(o.MARCCreator_b) == GetComparisonString(MARCCreator_b) &&
					GetComparisonString(o.MARCCreator_c) == GetComparisonString(MARCCreator_c) &&
					GetComparisonString(o.MARCCreator_d) == GetComparisonString(MARCCreator_d) &&
					o.CreatorRoleTypeID == CreatorRoleTypeID &&
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
					o.ExternalCreationUser == ExternalCreationUser &&
					o.ExternalLastModifiedUser == ExternalLastModifiedUser &&
					o.ProductionDate == ProductionDate &&
					o.CreatedDate == CreatedDate &&
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
				throw new ArgumentException("Argument is not of type __Title_Creator");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Title_Creator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title_Creator"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Title_Creator a, __Title_Creator b)
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
		/// <param name="a">The first <see cref="__Title_Creator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title_Creator"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Title_Creator a, __Title_Creator b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Title_Creator"/> object to compare with the current <see cref="__Title_Creator"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Title_Creator))
			{
				return false;
			}
			
			return this == (__Title_Creator) obj;
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
		/// list.Sort(SortOrder.Ascending, __Title_Creator.SortColumn.TitleCreatorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleCreatorID = "TitleCreatorID";	
			public const string CreatorName = "CreatorName";	
			public const string MARCCreator_a = "MARCCreator_a";	
			public const string MARCCreator_b = "MARCCreator_b";	
			public const string MARCCreator_c = "MARCCreator_c";	
			public const string MARCCreator_d = "MARCCreator_d";	
			public const string CreatorRoleTypeID = "CreatorRoleTypeID";	
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ExternalCreationUser = "ExternalCreationUser";	
			public const string ExternalLastModifiedUser = "ExternalLastModifiedUser";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
