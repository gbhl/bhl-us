
// Generated 11/5/2013 11:13:30 AM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordCreator is based upon OAIRecordCreator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordCreator : __OAIRecordCreator
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
	public abstract class __OAIRecordCreator : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordCreator()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="creatorType"></param>
		/// <param name="fullName"></param>
		/// <param name="dates"></param>
		/// <param name="productionAuthorID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecordCreator(int oAIRecordCreatorID, 
			int oAIRecordID, 
			string creatorType, 
			string fullName, 
			string dates, 
			int? productionAuthorID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordCreatorID = oAIRecordCreatorID;
			OAIRecordID = oAIRecordID;
			CreatorType = creatorType;
			FullName = fullName;
			Dates = dates;
			ProductionAuthorID = productionAuthorID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordCreator()
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
					case "OAIRecordCreatorID" :
					{
						_OAIRecordCreatorID = (int)column.Value;
						break;
					}
					case "OAIRecordID" :
					{
						_OAIRecordID = (int)column.Value;
						break;
					}
					case "CreatorType" :
					{
						_CreatorType = (string)column.Value;
						break;
					}
					case "FullName" :
					{
						_FullName = (string)column.Value;
						break;
					}
					case "Dates" :
					{
						_Dates = (string)column.Value;
						break;
					}
					case "ProductionAuthorID" :
					{
						_ProductionAuthorID = (int?)column.Value;
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
		
		#region OAIRecordCreatorID
		
		private int _OAIRecordCreatorID = default(int);
		
		/// <summary>
		/// Column: OAIRecordCreatorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordCreatorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int OAIRecordCreatorID
		{
			get
			{
				return _OAIRecordCreatorID;
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
		
		#endregion OAIRecordCreatorID
		
		#region OAIRecordID
		
		private int _OAIRecordID = default(int);
		
		/// <summary>
		/// Column: OAIRecordID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("OAIRecordID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int OAIRecordID
		{
			get
			{
				return _OAIRecordID;
			}
			set
			{
				if (_OAIRecordID != value)
				{
					_OAIRecordID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIRecordID
		
		#region CreatorType
		
		private string _CreatorType = string.Empty;
		
		/// <summary>
		/// Column: CreatorType;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CreatorType", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string CreatorType
		{
			get
			{
				return _CreatorType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CreatorType != value)
				{
					_CreatorType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatorType
		
		#region FullName
		
		private string _FullName = string.Empty;
		
		/// <summary>
		/// Column: FullName;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("FullName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=300)]
		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_FullName != value)
				{
					_FullName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FullName
		
		#region Dates
		
		private string _Dates = string.Empty;
		
		/// <summary>
		/// Column: Dates;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Dates", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string Dates
		{
			get
			{
				return _Dates;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Dates != value)
				{
					_Dates = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Dates
		
		#region ProductionAuthorID
		
		private int? _ProductionAuthorID = null;
		
		/// <summary>
		/// Column: ProductionAuthorID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionAuthorID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? ProductionAuthorID
		{
			get
			{
				return _ProductionAuthorID;
			}
			set
			{
				if (_ProductionAuthorID != value)
				{
					_ProductionAuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionAuthorID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordCreator"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordCreator"/>, 
		/// returns an instance of <see cref="__OAIRecordCreator"/>; otherwise returns null.</returns>
		public static new __OAIRecordCreator FromArray(byte[] byteArray)
		{
			__OAIRecordCreator o = null;
			
			try
			{
				o = (__OAIRecordCreator) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordCreator"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordCreator"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordCreator)
			{
				__OAIRecordCreator o = (__OAIRecordCreator) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordCreatorID == OAIRecordCreatorID &&
					o.OAIRecordID == OAIRecordID &&
					GetComparisonString(o.CreatorType) == GetComparisonString(CreatorType) &&
					GetComparisonString(o.FullName) == GetComparisonString(FullName) &&
					GetComparisonString(o.Dates) == GetComparisonString(Dates) &&
					o.ProductionAuthorID == ProductionAuthorID &&
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
				throw new ArgumentException("Argument is not of type __OAIRecordCreator");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordCreator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordCreator"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordCreator a, __OAIRecordCreator b)
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
		/// <param name="a">The first <see cref="__OAIRecordCreator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordCreator"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordCreator a, __OAIRecordCreator b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordCreator"/> object to compare with the current <see cref="__OAIRecordCreator"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordCreator))
			{
				return false;
			}
			
			return this == (__OAIRecordCreator) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordCreator.SortColumn.OAIRecordCreatorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordCreatorID = "OAIRecordCreatorID";	
			public const string OAIRecordID = "OAIRecordID";	
			public const string CreatorType = "CreatorType";	
			public const string FullName = "FullName";	
			public const string Dates = "Dates";	
			public const string ProductionAuthorID = "ProductionAuthorID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
