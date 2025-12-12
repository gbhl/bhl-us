
// Generated 12/12/2025 1:03:44 PM
// Do not modify the contents of this code file.
// This abstract class __BSSegmentAuthor is based upon dbo.BSSegmentAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class BSSegmentAuthor : __BSSegmentAuthor
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
	public abstract class __BSSegmentAuthor : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __BSSegmentAuthor()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentAuthorID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bioStorID"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="vIAFIdentifier"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __BSSegmentAuthor(int segmentAuthorID, 
			int importSourceID, 
			int segmentID, 
			string bioStorID, 
			string lastName, 
			string firstName, 
			int sequenceOrder, 
			string vIAFIdentifier, 
			int? bHLAuthorID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_SegmentAuthorID = segmentAuthorID;
			ImportSourceID = importSourceID;
			SegmentID = segmentID;
			BioStorID = bioStorID;
			LastName = lastName;
			FirstName = firstName;
			SequenceOrder = sequenceOrder;
			VIAFIdentifier = vIAFIdentifier;
			BHLAuthorID = bHLAuthorID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__BSSegmentAuthor()
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
					case "SegmentAuthorID" :
					{
						_SegmentAuthorID = (int)column.Value;
						break;
					}
					case "ImportSourceID" :
					{
						_ImportSourceID = (int)column.Value;
						break;
					}
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
						break;
					}
					case "BioStorID" :
					{
						_BioStorID = (string)column.Value;
						break;
					}
					case "LastName" :
					{
						_LastName = (string)column.Value;
						break;
					}
					case "FirstName" :
					{
						_FirstName = (string)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (int)column.Value;
						break;
					}
					case "VIAFIdentifier" :
					{
						_VIAFIdentifier = (string)column.Value;
						break;
					}
					case "BHLAuthorID" :
					{
						_BHLAuthorID = (int?)column.Value;
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
		
		#region SegmentAuthorID
		
		private int _SegmentAuthorID = default(int);
		
		/// <summary>
		/// Column: SegmentAuthorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentAuthorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int SegmentAuthorID
		{
			get
			{
				return _SegmentAuthorID;
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
		
		#endregion SegmentAuthorID
		
		#region ImportSourceID
		
		private int _ImportSourceID = default(int);
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportSourceID
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
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
			}
			set
			{
				if (_SegmentID != value)
				{
					_SegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentID
		
		#region BioStorID
		
		private string _BioStorID = string.Empty;
		
		/// <summary>
		/// Column: BioStorID;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("BioStorID", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=100)]
		public string BioStorID
		{
			get
			{
				return _BioStorID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_BioStorID != value)
				{
					_BioStorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BioStorID
		
		#region LastName
		
		private string _LastName = string.Empty;
		
		/// <summary>
		/// Column: LastName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("LastName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=150)]
		public string LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_LastName != value)
				{
					_LastName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastName
		
		#region FirstName
		
		private string _FirstName = string.Empty;
		
		/// <summary>
		/// Column: FirstName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FirstName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=150)]
		public string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_FirstName != value)
				{
					_FirstName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FirstName
		
		#region SequenceOrder
		
		private int _SequenceOrder = default(int);
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
		public int SequenceOrder
		{
			get
			{
				return _SequenceOrder;
			}
			set
			{
				if (_SequenceOrder != value)
				{
					_SequenceOrder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceOrder
		
		#region VIAFIdentifier
		
		private string _VIAFIdentifier = string.Empty;
		
		/// <summary>
		/// Column: VIAFIdentifier;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("VIAFIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=20)]
		public string VIAFIdentifier
		{
			get
			{
				return _VIAFIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_VIAFIdentifier != value)
				{
					_VIAFIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VIAFIdentifier
		
		#region BHLAuthorID
		
		private int? _BHLAuthorID = null;
		
		/// <summary>
		/// Column: BHLAuthorID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("BHLAuthorID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? BHLAuthorID
		{
			get
			{
				return _BHLAuthorID;
			}
			set
			{
				if (_BHLAuthorID != value)
				{
					_BHLAuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLAuthorID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=11)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__BSSegmentAuthor"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__BSSegmentAuthor"/>, 
		/// returns an instance of <see cref="__BSSegmentAuthor"/>; otherwise returns null.</returns>
		public static new __BSSegmentAuthor FromArray(byte[] byteArray)
		{
			__BSSegmentAuthor o = null;
			
			try
			{
				o = (__BSSegmentAuthor) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__BSSegmentAuthor"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__BSSegmentAuthor"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __BSSegmentAuthor)
			{
				__BSSegmentAuthor o = (__BSSegmentAuthor) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentAuthorID == SegmentAuthorID &&
					o.ImportSourceID == ImportSourceID &&
					o.SegmentID == SegmentID &&
					GetComparisonString(o.BioStorID) == GetComparisonString(BioStorID) &&
					GetComparisonString(o.LastName) == GetComparisonString(LastName) &&
					GetComparisonString(o.FirstName) == GetComparisonString(FirstName) &&
					o.SequenceOrder == SequenceOrder &&
					GetComparisonString(o.VIAFIdentifier) == GetComparisonString(VIAFIdentifier) &&
					o.BHLAuthorID == BHLAuthorID &&
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
				throw new ArgumentException("Argument is not of type __BSSegmentAuthor");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__BSSegmentAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BSSegmentAuthor"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__BSSegmentAuthor a, __BSSegmentAuthor b)
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
		/// <param name="a">The first <see cref="__BSSegmentAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BSSegmentAuthor"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__BSSegmentAuthor a, __BSSegmentAuthor b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__BSSegmentAuthor"/> object to compare with the current <see cref="__BSSegmentAuthor"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __BSSegmentAuthor))
			{
				return false;
			}
			
			return this == (__BSSegmentAuthor) obj;
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
		/// list.Sort(SortOrder.Ascending, __BSSegmentAuthor.SortColumn.SegmentAuthorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentAuthorID = "SegmentAuthorID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string SegmentID = "SegmentID";	
			public const string BioStorID = "BioStorID";	
			public const string LastName = "LastName";	
			public const string FirstName = "FirstName";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string VIAFIdentifier = "VIAFIdentifier";	
			public const string BHLAuthorID = "BHLAuthorID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

