
// Generated 10/16/2024 5:39:53 PM
// Do not modify the contents of this code file.
// This abstract class __Service is based upon servlog.Service.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Service : __Service
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
	public abstract class __Service : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Service()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="serviceID"></param>
		/// <param name="name"></param>
		/// <param name="param"></param>
		/// <param name="frequencyID"></param>
		/// <param name="disabled"></param>
		/// <param name="display"></param>
		/// <param name="creationDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Service(int serviceID, 
			string name, 
			string param, 
			int? frequencyID, 
			byte disabled, 
			byte display, 
			DateTime creationDate, 
			int creationUserID, 
			DateTime lastModifiedDate, 
			int lastModifiedUserID) : this()
		{
			_ServiceID = serviceID;
			Name = name;
			Param = param;
			FrequencyID = frequencyID;
			Disabled = disabled;
			Display = display;
			CreationDate = creationDate;
			CreationUserID = creationUserID;
			LastModifiedDate = lastModifiedDate;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Service()
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
					case "ServiceID" :
					{
						_ServiceID = (int)column.Value;
						break;
					}
					case "Name" :
					{
						_Name = (string)column.Value;
						break;
					}
					case "Param" :
					{
						_Param = (string)column.Value;
						break;
					}
					case "FrequencyID" :
					{
						_FrequencyID = (int?)column.Value;
						break;
					}
					case "Disabled" :
					{
						_Disabled = (byte)column.Value;
						break;
					}
					case "Display" :
					{
						_Display = (byte)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime)column.Value;
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
		
		#region ServiceID
		
		private int _ServiceID = default(int);
		
		/// <summary>
		/// Column: ServiceID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ServiceID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ServiceID
		{
			get
			{
				return _ServiceID;
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
		
		#endregion ServiceID
		
		#region Name
		
		private string _Name = string.Empty;
		
		/// <summary>
		/// Column: Name;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Name", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=200)]
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Name != value)
				{
					_Name = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Name
		
		#region Param
		
		private string _Param = string.Empty;
		
		/// <summary>
		/// Column: Param;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("Param", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=30)]
		public string Param
		{
			get
			{
				return _Param;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Param != value)
				{
					_Param = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Param
		
		#region FrequencyID
		
		private int? _FrequencyID = null;
		
		/// <summary>
		/// Column: FrequencyID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("FrequencyID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? FrequencyID
		{
			get
			{
				return _FrequencyID;
			}
			set
			{
				if (_FrequencyID != value)
				{
					_FrequencyID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FrequencyID
		
		#region Disabled
		
		private byte _Disabled = default(byte);
		
		/// <summary>
		/// Column: Disabled;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("Disabled", DbTargetType=SqlDbType.TinyInt, Ordinal=5, NumericPrecision=3)]
		public byte Disabled
		{
			get
			{
				return _Disabled;
			}
			set
			{
				if (_Disabled != value)
				{
					_Disabled = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Disabled
		
		#region Display
		
		private byte _Display = default(byte);
		
		/// <summary>
		/// Column: Display;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("Display", DbTargetType=SqlDbType.TinyInt, Ordinal=6, NumericPrecision=3)]
		public byte Display
		{
			get
			{
				return _Display;
			}
			set
			{
				if (_Display != value)
				{
					_Display = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Display
		
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
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Service"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Service"/>, 
		/// returns an instance of <see cref="__Service"/>; otherwise returns null.</returns>
		public static new __Service FromArray(byte[] byteArray)
		{
			__Service o = null;
			
			try
			{
				o = (__Service) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Service"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Service"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Service)
			{
				__Service o = (__Service) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ServiceID == ServiceID &&
					GetComparisonString(o.Name) == GetComparisonString(Name) &&
					GetComparisonString(o.Param) == GetComparisonString(Param) &&
					o.FrequencyID == FrequencyID &&
					o.Disabled == Disabled &&
					o.Display == Display &&
					o.CreationDate == CreationDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedDate == LastModifiedDate &&
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
				throw new ArgumentException("Argument is not of type __Service");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Service"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Service"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Service a, __Service b)
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
		/// <param name="a">The first <see cref="__Service"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Service"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Service a, __Service b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Service"/> object to compare with the current <see cref="__Service"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Service))
			{
				return false;
			}
			
			return this == (__Service) obj;
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
		/// list.Sort(SortOrder.Ascending, __Service.SortColumn.ServiceID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ServiceID = "ServiceID";	
			public const string Name = "Name";	
			public const string Param = "Param";	
			public const string FrequencyID = "FrequencyID";	
			public const string Disabled = "Disabled";	
			public const string Display = "Display";	
			public const string CreationDate = "CreationDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

