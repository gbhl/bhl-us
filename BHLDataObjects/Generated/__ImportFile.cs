
// Generated 6/28/2021 12:51:29 PM
// Do not modify the contents of this code file.
// This abstract class __ImportFile is based upon import.ImportFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ImportFile : __ImportFile
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
	public abstract class __ImportFile : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportFile()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importFileID"></param>
		/// <param name="importFileStatusID"></param>
		/// <param name="importFileName"></param>
		/// <param name="contributorCode"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __ImportFile(int importFileID, 
			int importFileStatusID, 
			string importFileName, 
			string contributorCode, 
			int? segmentGenreID, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_ImportFileID = importFileID;
			ImportFileStatusID = importFileStatusID;
			ImportFileName = importFileName;
			ContributorCode = contributorCode;
			SegmentGenreID = segmentGenreID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ImportFile()
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
					case "ImportFileID" :
					{
						_ImportFileID = (int)column.Value;
						break;
					}
					case "ImportFileStatusID" :
					{
						_ImportFileStatusID = (int)column.Value;
						break;
					}
					case "ImportFileName" :
					{
						_ImportFileName = (string)column.Value;
						break;
					}
					case "ContributorCode" :
					{
						_ContributorCode = (string)column.Value;
						break;
					}
					case "SegmentGenreID" :
					{
						_SegmentGenreID = (int?)column.Value;
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
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ImportFileID
		
		private int _ImportFileID = default(int);
		
		/// <summary>
		/// Column: ImportFileID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportFileID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ImportFileID
		{
			get
			{
				return _ImportFileID;
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
		
		#endregion ImportFileID
		
		#region ImportFileStatusID
		
		private int _ImportFileStatusID = default(int);
		
		/// <summary>
		/// Column: ImportFileStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportFileStatusID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportFileStatusID
		{
			get
			{
				return _ImportFileStatusID;
			}
			set
			{
				if (_ImportFileStatusID != value)
				{
					_ImportFileStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportFileStatusID
		
		#region ImportFileName
		
		private string _ImportFileName = string.Empty;
		
		/// <summary>
		/// Column: ImportFileName;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("ImportFileName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=200)]
		public string ImportFileName
		{
			get
			{
				return _ImportFileName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_ImportFileName != value)
				{
					_ImportFileName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportFileName
		
		#region ContributorCode
		
		private string _ContributorCode = null;
		
		/// <summary>
		/// Column: ContributorCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("ContributorCode", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=10, IsNullable=true)]
		public string ContributorCode
		{
			get
			{
				return _ContributorCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_ContributorCode != value)
				{
					_ContributorCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContributorCode
		
		#region SegmentGenreID
		
		private int? _SegmentGenreID = null;
		
		/// <summary>
		/// Column: SegmentGenreID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("SegmentGenreID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? SegmentGenreID
		{
			get
			{
				return _SegmentGenreID;
			}
			set
			{
				if (_SegmentGenreID != value)
				{
					_SegmentGenreID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentGenreID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int CreationUserID
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
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int LastModifiedUserID
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ImportFile"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportFile"/>, 
		/// returns an instance of <see cref="__ImportFile"/>; otherwise returns null.</returns>
		public static new __ImportFile FromArray(byte[] byteArray)
		{
			__ImportFile o = null;
			
			try
			{
				o = (__ImportFile) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportFile"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportFile"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportFile)
			{
				__ImportFile o = (__ImportFile) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportFileID == ImportFileID &&
					o.ImportFileStatusID == ImportFileStatusID &&
					GetComparisonString(o.ImportFileName) == GetComparisonString(ImportFileName) &&
					GetComparisonString(o.ContributorCode) == GetComparisonString(ContributorCode) &&
					o.SegmentGenreID == SegmentGenreID &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID 
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
				throw new ArgumentException("Argument is not of type __ImportFile");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportFile"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportFile a, __ImportFile b)
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
		/// <param name="a">The first <see cref="__ImportFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportFile"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportFile a, __ImportFile b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportFile"/> object to compare with the current <see cref="__ImportFile"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportFile))
			{
				return false;
			}
			
			return this == (__ImportFile) obj;
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
		/// list.Sort(SortOrder.Ascending, __ImportFile.SortColumn.ImportFileID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportFileID = "ImportFileID";	
			public const string ImportFileStatusID = "ImportFileStatusID";	
			public const string ImportFileName = "ImportFileName";	
			public const string ContributorCode = "ContributorCode";	
			public const string SegmentGenreID = "SegmentGenreID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

