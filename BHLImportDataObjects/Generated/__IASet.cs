
// Generated 5/27/2008 11:38:08 AM
// Do not modify the contents of this code file.
// This abstract class __IASet is based upon IASet.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IASet : __IASet
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
	public abstract class __IASet : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IASet()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="setID"></param>
		/// <param name="setSpecification"></param>
		/// <param name="downloadAll"></param>
		/// <param name="lastDownloadDate"></param>
		/// <param name="lastFullDownloadDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IASet(int setID, 
			string setSpecification, 
			bool downloadAll, 
			DateTime? lastDownloadDate, 
			DateTime? lastFullDownloadDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_SetID = setID;
			SetSpecification = setSpecification;
			DownloadAll = downloadAll;
			LastDownloadDate = lastDownloadDate;
			LastFullDownloadDate = lastFullDownloadDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IASet()
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
					case "SetID" :
					{
						_SetID = (int)column.Value;
						break;
					}
					case "SetSpecification" :
					{
						_SetSpecification = (string)column.Value;
						break;
					}
					case "DownloadAll" :
					{
						_DownloadAll = (bool)column.Value;
						break;
					}
					case "LastDownloadDate" :
					{
						_LastDownloadDate = (DateTime?)column.Value;
						break;
					}
					case "LastFullDownloadDate" :
					{
						_LastFullDownloadDate = (DateTime?)column.Value;
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
		
		#region SetID
		
		private int _SetID = default(int);
		
		/// <summary>
		/// Column: SetID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SetID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int SetID
		{
			get
			{
				return _SetID;
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
		
		#endregion SetID
		
		#region SetSpecification
		
		private string _SetSpecification = string.Empty;
		
		/// <summary>
		/// Column: SetSpecification;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("SetSpecification", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=200)]
		public string SetSpecification
		{
			get
			{
				return _SetSpecification;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_SetSpecification != value)
				{
					_SetSpecification = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SetSpecification
		
		#region DownloadAll
		
		private bool _DownloadAll = false;
		
		/// <summary>
		/// Column: DownloadAll;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("DownloadAll", DbTargetType=SqlDbType.Bit, Ordinal=3)]
		public bool DownloadAll
		{
			get
			{
				return _DownloadAll;
			}
			set
			{
				if (_DownloadAll != value)
				{
					_DownloadAll = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DownloadAll
		
		#region LastDownloadDate
		
		private DateTime? _LastDownloadDate = null;
		
		/// <summary>
		/// Column: LastDownloadDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastDownloadDate", DbTargetType=SqlDbType.DateTime, Ordinal=4, IsNullable=true)]
		public DateTime? LastDownloadDate
		{
			get
			{
				return _LastDownloadDate;
			}
			set
			{
				if (_LastDownloadDate != value)
				{
					_LastDownloadDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastDownloadDate
		
		#region LastFullDownloadDate
		
		private DateTime? _LastFullDownloadDate = null;
		
		/// <summary>
		/// Column: LastFullDownloadDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastFullDownloadDate", DbTargetType=SqlDbType.DateTime, Ordinal=5, IsNullable=true)]
		public DateTime? LastFullDownloadDate
		{
			get
			{
				return _LastFullDownloadDate;
			}
			set
			{
				if (_LastFullDownloadDate != value)
				{
					_LastFullDownloadDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastFullDownloadDate
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__IASet"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IASet"/>, 
		/// returns an instance of <see cref="__IASet"/>; otherwise returns null.</returns>
		public static new __IASet FromArray(byte[] byteArray)
		{
			__IASet o = null;
			
			try
			{
				o = (__IASet) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IASet"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IASet"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IASet)
			{
				__IASet o = (__IASet) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SetID == SetID &&
					GetComparisonString(o.SetSpecification) == GetComparisonString(SetSpecification) &&
					o.DownloadAll == DownloadAll &&
					o.LastDownloadDate == LastDownloadDate &&
					o.LastFullDownloadDate == LastFullDownloadDate &&
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
				throw new ArgumentException("Argument is not of type __IASet");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IASet"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASet"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IASet a, __IASet b)
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
		/// <param name="a">The first <see cref="__IASet"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASet"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IASet a, __IASet b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IASet"/> object to compare with the current <see cref="__IASet"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IASet))
			{
				return false;
			}
			
			return this == (__IASet) obj;
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
		/// list.Sort(SortOrder.Ascending, __IASet.SortColumn.SetID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SetID = "SetID";	
			public const string SetSpecification = "SetSpecification";	
			public const string DownloadAll = "DownloadAll";	
			public const string LastDownloadDate = "LastDownloadDate";	
			public const string LastFullDownloadDate = "LastFullDownloadDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
