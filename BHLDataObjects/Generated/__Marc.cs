
// Generated 1/5/2021 3:26:01 PM
// Do not modify the contents of this code file.
// This abstract class __Marc is based upon dbo.Marc.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Marc : __Marc
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
	public abstract class __Marc : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Marc()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcID"></param>
		/// <param name="marcImportStatusID"></param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="leader"></param>
		/// <param name="titleID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Marc(int marcID, 
			int marcImportStatusID, 
			int marcImportBatchID, 
			string marcFileLocation, 
			string institutionCode, 
			string leader, 
			int? titleID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcID = marcID;
			MarcImportStatusID = marcImportStatusID;
			MarcImportBatchID = marcImportBatchID;
			MarcFileLocation = marcFileLocation;
			InstitutionCode = institutionCode;
			Leader = leader;
			TitleID = titleID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Marc()
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
					case "MarcID" :
					{
						_MarcID = (int)column.Value;
						break;
					}
					case "MarcImportStatusID" :
					{
						_MarcImportStatusID = (int)column.Value;
						break;
					}
					case "MarcImportBatchID" :
					{
						_MarcImportBatchID = (int)column.Value;
						break;
					}
					case "MarcFileLocation" :
					{
						_MarcFileLocation = (string)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "Leader" :
					{
						_Leader = (string)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int?)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region MarcID
		
		private int _MarcID = default(int);
		
		/// <summary>
		/// Column: MarcID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int MarcID
		{
			get
			{
				return _MarcID;
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
		
		#endregion MarcID
		
		#region MarcImportStatusID
		
		private int _MarcImportStatusID = default(int);
		
		/// <summary>
		/// Column: MarcImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int MarcImportStatusID
		{
			get
			{
				return _MarcImportStatusID;
			}
			set
			{
				if (_MarcImportStatusID != value)
				{
					_MarcImportStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcImportStatusID
		
		#region MarcImportBatchID
		
		private int _MarcImportBatchID = default(int);
		
		/// <summary>
		/// Column: MarcImportBatchID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcImportBatchID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int MarcImportBatchID
		{
			get
			{
				return _MarcImportBatchID;
			}
			set
			{
				if (_MarcImportBatchID != value)
				{
					_MarcImportBatchID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcImportBatchID
		
		#region MarcFileLocation
		
		private string _MarcFileLocation = string.Empty;
		
		/// <summary>
		/// Column: MarcFileLocation;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("MarcFileLocation", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=500)]
		public string MarcFileLocation
		{
			get
			{
				return _MarcFileLocation;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_MarcFileLocation != value)
				{
					_MarcFileLocation = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcFileLocation
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=10, IsNullable=true)]
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
		
		#region Leader
		
		private string _Leader = string.Empty;
		
		/// <summary>
		/// Column: Leader;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Leader", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
		public string Leader
		{
			get
			{
				return _Leader;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Leader != value)
				{
					_Leader = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Leader
		
		#region TitleID
		
		private int? _TitleID = null;
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
		public int? TitleID
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
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Marc"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Marc"/>, 
		/// returns an instance of <see cref="__Marc"/>; otherwise returns null.</returns>
		public static new __Marc FromArray(byte[] byteArray)
		{
			__Marc o = null;
			
			try
			{
				o = (__Marc) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Marc"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Marc"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Marc)
			{
				__Marc o = (__Marc) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcID == MarcID &&
					o.MarcImportStatusID == MarcImportStatusID &&
					o.MarcImportBatchID == MarcImportBatchID &&
					GetComparisonString(o.MarcFileLocation) == GetComparisonString(MarcFileLocation) &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.Leader) == GetComparisonString(Leader) &&
					o.TitleID == TitleID &&
					o.CreationDate == CreationDate &&
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
				throw new ArgumentException("Argument is not of type __Marc");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Marc"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Marc"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Marc a, __Marc b)
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
		/// <param name="a">The first <see cref="__Marc"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Marc"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Marc a, __Marc b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Marc"/> object to compare with the current <see cref="__Marc"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Marc))
			{
				return false;
			}
			
			return this == (__Marc) obj;
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
		/// list.Sort(SortOrder.Ascending, __Marc.SortColumn.MarcID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcID = "MarcID";	
			public const string MarcImportStatusID = "MarcImportStatusID";	
			public const string MarcImportBatchID = "MarcImportBatchID";	
			public const string MarcFileLocation = "MarcFileLocation";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string Leader = "Leader";	
			public const string TitleID = "TitleID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

