
// Generated 1/5/2021 2:13:31 PM
// Do not modify the contents of this code file.
// This abstract class __Creator is based upon dbo.Creator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class Creator : __Creator
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
	public abstract class __Creator : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Creator()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="creatorID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="creatorName"></param>
		/// <param name="firstNameFirst"></param>
		/// <param name="simpleName"></param>
		/// <param name="dOB"></param>
		/// <param name="dOD"></param>
		/// <param name="biography"></param>
		/// <param name="creatorNote"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="mARCCreator_Full"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="mARCCreator_q"></param>
		public __Creator(int creatorID, 
			int importStatusID, 
			int? importSourceID, 
			string creatorName, 
			string firstNameFirst, 
			string simpleName, 
			string dOB, 
			string dOD, 
			string biography, 
			string creatorNote, 
			string mARCDataFieldTag, 
			string mARCCreator_a, 
			string mARCCreator_b, 
			string mARCCreator_c, 
			string mARCCreator_d, 
			string mARCCreator_Full, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate, 
			string mARCCreator_q) : this()
		{
			_CreatorID = creatorID;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			CreatorName = creatorName;
			FirstNameFirst = firstNameFirst;
			SimpleName = simpleName;
			DOB = dOB;
			DOD = dOD;
			Biography = biography;
			CreatorNote = creatorNote;
			MARCDataFieldTag = mARCDataFieldTag;
			MARCCreator_a = mARCCreator_a;
			MARCCreator_b = mARCCreator_b;
			MARCCreator_c = mARCCreator_c;
			MARCCreator_d = mARCCreator_d;
			MARCCreator_Full = mARCCreator_Full;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
			MARCCreator_q = mARCCreator_q;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Creator()
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
					case "CreatorID" :
					{
						_CreatorID = (int)column.Value;
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
					case "CreatorName" :
					{
						_CreatorName = (string)column.Value;
						break;
					}
					case "FirstNameFirst" :
					{
						_FirstNameFirst = (string)column.Value;
						break;
					}
					case "SimpleName" :
					{
						_SimpleName = (string)column.Value;
						break;
					}
					case "DOB" :
					{
						_DOB = (string)column.Value;
						break;
					}
					case "DOD" :
					{
						_DOD = (string)column.Value;
						break;
					}
					case "Biography" :
					{
						_Biography = (string)column.Value;
						break;
					}
					case "CreatorNote" :
					{
						_CreatorNote = (string)column.Value;
						break;
					}
					case "MARCDataFieldTag" :
					{
						_MARCDataFieldTag = (string)column.Value;
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
					case "MARCCreator_Full" :
					{
						_MARCCreator_Full = (string)column.Value;
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
					case "MARCCreator_q" :
					{
						_MARCCreator_q = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region CreatorID
		
		private int _CreatorID = default(int);
		
		/// <summary>
		/// Column: CreatorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("CreatorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int CreatorID
		{
			get
			{
				return _CreatorID;
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
		
		#endregion CreatorID
		
		#region ImportStatusID
		
		private int _ImportStatusID = default(int);
		
		/// <summary>
		/// Column: ImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
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
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region CreatorName
		
		private string _CreatorName = string.Empty;
		
		/// <summary>
		/// Column: CreatorName;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("CreatorName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=255)]
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
		
		#region FirstNameFirst
		
		private string _FirstNameFirst = null;
		
		/// <summary>
		/// Column: FirstNameFirst;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("FirstNameFirst", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=255, IsNullable=true)]
		public string FirstNameFirst
		{
			get
			{
				return _FirstNameFirst;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_FirstNameFirst != value)
				{
					_FirstNameFirst = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FirstNameFirst
		
		#region SimpleName
		
		private string _SimpleName = null;
		
		/// <summary>
		/// Column: SimpleName;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("SimpleName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=255, IsNullable=true)]
		public string SimpleName
		{
			get
			{
				return _SimpleName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_SimpleName != value)
				{
					_SimpleName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SimpleName
		
		#region DOB
		
		private string _DOB = null;
		
		/// <summary>
		/// Column: DOB;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("DOB", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50, IsNullable=true)]
		public string DOB
		{
			get
			{
				return _DOB;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOB != value)
				{
					_DOB = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOB
		
		#region DOD
		
		private string _DOD = null;
		
		/// <summary>
		/// Column: DOD;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("DOD", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50, IsNullable=true)]
		public string DOD
		{
			get
			{
				return _DOD;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOD != value)
				{
					_DOD = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOD
		
		#region Biography
		
		private string _Biography = null;
		
		/// <summary>
		/// Column: Biography;
		/// DBMS data type: ntext; Nullable;
		/// </summary>
		[ColumnDefinition("Biography", DbTargetType=SqlDbType.NText, Ordinal=9, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Biography
		{
			get
			{
				return _Biography;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Biography != value)
				{
					_Biography = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Biography
		
		#region CreatorNote
		
		private string _CreatorNote = null;
		
		/// <summary>
		/// Column: CreatorNote;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("CreatorNote", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=255, IsNullable=true)]
		public string CreatorNote
		{
			get
			{
				return _CreatorNote;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_CreatorNote != value)
				{
					_CreatorNote = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorNote
		
		#region MARCDataFieldTag
		
		private string _MARCDataFieldTag = null;
		
		/// <summary>
		/// Column: MARCDataFieldTag;
		/// DBMS data type: nvarchar(3); Nullable;
		/// </summary>
		[ColumnDefinition("MARCDataFieldTag", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=3, IsNullable=true)]
		public string MARCDataFieldTag
		{
			get
			{
				return _MARCDataFieldTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 3);
				if (_MARCDataFieldTag != value)
				{
					_MARCDataFieldTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCDataFieldTag
		
		#region MARCCreator_a
		
		private string _MARCCreator_a = null;
		
		/// <summary>
		/// Column: MARCCreator_a;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_a", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=450, IsNullable=true)]
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
		[ColumnDefinition("MARCCreator_b", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=450, IsNullable=true)]
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
		[ColumnDefinition("MARCCreator_c", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=450, IsNullable=true)]
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
		[ColumnDefinition("MARCCreator_d", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=450, IsNullable=true)]
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
		
		#region MARCCreator_Full
		
		private string _MARCCreator_Full = null;
		
		/// <summary>
		/// Column: MARCCreator_Full;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_Full", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_Full
		{
			get
			{
				return _MARCCreator_Full;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_Full != value)
				{
					_MARCCreator_Full = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_Full
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=17, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=18, IsNullable=true)]
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
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=19, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=20)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=21)]
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
		
		#region MARCCreator_q
		
		private string _MARCCreator_q = null;
		
		/// <summary>
		/// Column: MARCCreator_q;
		/// DBMS data type: nvarchar(450); Nullable;
		/// </summary>
		[ColumnDefinition("MARCCreator_q", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=450, IsNullable=true)]
		public string MARCCreator_q
		{
			get
			{
				return _MARCCreator_q;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_MARCCreator_q != value)
				{
					_MARCCreator_q = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCreator_q
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Creator"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Creator"/>, 
		/// returns an instance of <see cref="__Creator"/>; otherwise returns null.</returns>
		public static new __Creator FromArray(byte[] byteArray)
		{
			__Creator o = null;
			
			try
			{
				o = (__Creator) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Creator"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Creator"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Creator)
			{
				__Creator o = (__Creator) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.CreatorID == CreatorID &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.CreatorName) == GetComparisonString(CreatorName) &&
					GetComparisonString(o.FirstNameFirst) == GetComparisonString(FirstNameFirst) &&
					GetComparisonString(o.SimpleName) == GetComparisonString(SimpleName) &&
					GetComparisonString(o.DOB) == GetComparisonString(DOB) &&
					GetComparisonString(o.DOD) == GetComparisonString(DOD) &&
					GetComparisonString(o.Biography) == GetComparisonString(Biography) &&
					GetComparisonString(o.CreatorNote) == GetComparisonString(CreatorNote) &&
					GetComparisonString(o.MARCDataFieldTag) == GetComparisonString(MARCDataFieldTag) &&
					GetComparisonString(o.MARCCreator_a) == GetComparisonString(MARCCreator_a) &&
					GetComparisonString(o.MARCCreator_b) == GetComparisonString(MARCCreator_b) &&
					GetComparisonString(o.MARCCreator_c) == GetComparisonString(MARCCreator_c) &&
					GetComparisonString(o.MARCCreator_d) == GetComparisonString(MARCCreator_d) &&
					GetComparisonString(o.MARCCreator_Full) == GetComparisonString(MARCCreator_Full) &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
					o.ProductionDate == ProductionDate &&
					o.CreatedDate == CreatedDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.MARCCreator_q) == GetComparisonString(MARCCreator_q) 
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
				throw new ArgumentException("Argument is not of type __Creator");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Creator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Creator"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Creator a, __Creator b)
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
		/// <param name="a">The first <see cref="__Creator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Creator"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Creator a, __Creator b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Creator"/> object to compare with the current <see cref="__Creator"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Creator))
			{
				return false;
			}
			
			return this == (__Creator) obj;
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
		/// list.Sort(SortOrder.Ascending, __Creator.SortColumn.CreatorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string CreatorID = "CreatorID";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string CreatorName = "CreatorName";	
			public const string FirstNameFirst = "FirstNameFirst";	
			public const string SimpleName = "SimpleName";	
			public const string DOB = "DOB";	
			public const string DOD = "DOD";	
			public const string Biography = "Biography";	
			public const string CreatorNote = "CreatorNote";	
			public const string MARCDataFieldTag = "MARCDataFieldTag";	
			public const string MARCCreator_a = "MARCCreator_a";	
			public const string MARCCreator_b = "MARCCreator_b";	
			public const string MARCCreator_c = "MARCCreator_c";	
			public const string MARCCreator_d = "MARCCreator_d";	
			public const string MARCCreator_Full = "MARCCreator_Full";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string MARCCreator_q = "MARCCreator_q";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

