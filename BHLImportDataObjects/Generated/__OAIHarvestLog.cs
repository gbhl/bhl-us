
// Generated 10/16/2013 3:40:53 PM
// Do not modify the contents of this code file.
// This abstract class __OAIHarvestLog is based upon OAIHarvestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIHarvestLog : __OAIHarvestLog
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
	public abstract class __OAIHarvestLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIHarvestLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="harvestLogID"></param>
		/// <param name="harvestSetID"></param>
		/// <param name="harvestStartDateTime"></param>
		/// <param name="fromDateTime"></param>
		/// <param name="untilDateTime"></param>
		/// <param name="responseDateTime"></param>
		/// <param name="result"></param>
		/// <param name="numberHarvested"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIHarvestLog(int harvestLogID, 
			int harvestSetID, 
			DateTime? harvestStartDateTime, 
			DateTime? fromDateTime, 
			DateTime? untilDateTime, 
			DateTime? responseDateTime, 
			string result, 
			int numberHarvested, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_HarvestLogID = harvestLogID;
			HarvestSetID = harvestSetID;
			HarvestStartDateTime = harvestStartDateTime;
			FromDateTime = fromDateTime;
			UntilDateTime = untilDateTime;
			ResponseDateTime = responseDateTime;
			Result = result;
			NumberHarvested = numberHarvested;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIHarvestLog()
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
					case "HarvestLogID" :
					{
						_HarvestLogID = (int)column.Value;
						break;
					}
					case "HarvestSetID" :
					{
						_HarvestSetID = (int)column.Value;
						break;
					}
					case "HarvestStartDateTime" :
					{
						_HarvestStartDateTime = (DateTime?)column.Value;
						break;
					}
					case "FromDateTime" :
					{
						_FromDateTime = (DateTime?)column.Value;
						break;
					}
					case "UntilDateTime" :
					{
						_UntilDateTime = (DateTime?)column.Value;
						break;
					}
					case "ResponseDateTime" :
					{
						_ResponseDateTime = (DateTime?)column.Value;
						break;
					}
					case "Result" :
					{
						_Result = (string)column.Value;
						break;
					}
					case "NumberHarvested" :
					{
						_NumberHarvested = (int)column.Value;
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
		
		#region HarvestLogID
		
		private int _HarvestLogID = default(int);
		
		/// <summary>
		/// Column: HarvestLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("HarvestLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int HarvestLogID
		{
			get
			{
				return _HarvestLogID;
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
		
		#endregion HarvestLogID
		
		#region HarvestSetID
		
		private int _HarvestSetID = default(int);
		
		/// <summary>
		/// Column: HarvestSetID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("HarvestSetID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int HarvestSetID
		{
			get
			{
				return _HarvestSetID;
			}
			set
			{
				if (_HarvestSetID != value)
				{
					_HarvestSetID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestSetID
		
		#region HarvestStartDateTime
		
		private DateTime? _HarvestStartDateTime = null;
		
		/// <summary>
		/// Column: HarvestStartDateTime;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("HarvestStartDateTime", DbTargetType=SqlDbType.DateTime, Ordinal=3, IsNullable=true)]
		public DateTime? HarvestStartDateTime
		{
			get
			{
				return _HarvestStartDateTime;
			}
			set
			{
				if (_HarvestStartDateTime != value)
				{
					_HarvestStartDateTime = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestStartDateTime
		
		#region FromDateTime
		
		private DateTime? _FromDateTime = null;
		
		/// <summary>
		/// Column: FromDateTime;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("FromDateTime", DbTargetType=SqlDbType.DateTime, Ordinal=4, IsNullable=true)]
		public DateTime? FromDateTime
		{
			get
			{
				return _FromDateTime;
			}
			set
			{
				if (_FromDateTime != value)
				{
					_FromDateTime = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FromDateTime
		
		#region UntilDateTime
		
		private DateTime? _UntilDateTime = null;
		
		/// <summary>
		/// Column: UntilDateTime;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("UntilDateTime", DbTargetType=SqlDbType.DateTime, Ordinal=5, IsNullable=true)]
		public DateTime? UntilDateTime
		{
			get
			{
				return _UntilDateTime;
			}
			set
			{
				if (_UntilDateTime != value)
				{
					_UntilDateTime = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion UntilDateTime
		
		#region ResponseDateTime
		
		private DateTime? _ResponseDateTime = null;
		
		/// <summary>
		/// Column: ResponseDateTime;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ResponseDateTime", DbTargetType=SqlDbType.DateTime, Ordinal=6, IsNullable=true)]
		public DateTime? ResponseDateTime
		{
			get
			{
				return _ResponseDateTime;
			}
			set
			{
				if (_ResponseDateTime != value)
				{
					_ResponseDateTime = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ResponseDateTime
		
		#region Result
		
		private string _Result = string.Empty;
		
		/// <summary>
		/// Column: Result;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Result", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=200)]
		public string Result
		{
			get
			{
				return _Result;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Result != value)
				{
					_Result = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Result
		
		#region NumberHarvested
		
		private int _NumberHarvested = default(int);
		
		/// <summary>
		/// Column: NumberHarvested;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("NumberHarvested", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int NumberHarvested
		{
			get
			{
				return _NumberHarvested;
			}
			set
			{
				if (_NumberHarvested != value)
				{
					_NumberHarvested = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NumberHarvested
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIHarvestLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIHarvestLog"/>, 
		/// returns an instance of <see cref="__OAIHarvestLog"/>; otherwise returns null.</returns>
		public static new __OAIHarvestLog FromArray(byte[] byteArray)
		{
			__OAIHarvestLog o = null;
			
			try
			{
				o = (__OAIHarvestLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIHarvestLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIHarvestLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIHarvestLog)
			{
				__OAIHarvestLog o = (__OAIHarvestLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.HarvestLogID == HarvestLogID &&
					o.HarvestSetID == HarvestSetID &&
					o.HarvestStartDateTime == HarvestStartDateTime &&
					o.FromDateTime == FromDateTime &&
					o.UntilDateTime == UntilDateTime &&
					o.ResponseDateTime == ResponseDateTime &&
					GetComparisonString(o.Result) == GetComparisonString(Result) &&
					o.NumberHarvested == NumberHarvested &&
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
				throw new ArgumentException("Argument is not of type __OAIHarvestLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIHarvestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIHarvestLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIHarvestLog a, __OAIHarvestLog b)
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
		/// <param name="a">The first <see cref="__OAIHarvestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIHarvestLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIHarvestLog a, __OAIHarvestLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIHarvestLog"/> object to compare with the current <see cref="__OAIHarvestLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIHarvestLog))
			{
				return false;
			}
			
			return this == (__OAIHarvestLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIHarvestLog.SortColumn.HarvestLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string HarvestLogID = "HarvestLogID";	
			public const string HarvestSetID = "HarvestSetID";	
			public const string HarvestStartDateTime = "HarvestStartDateTime";	
			public const string FromDateTime = "FromDateTime";	
			public const string UntilDateTime = "UntilDateTime";	
			public const string ResponseDateTime = "ResponseDateTime";	
			public const string Result = "Result";	
			public const string NumberHarvested = "NumberHarvested";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
