
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordRight is based upon OAIRecordRight.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordRight : __OAIRecordRight
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
	public abstract class __OAIRecordRight : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordRight()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordRightID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="right"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecordRight(int oAIRecordRightID, 
			int oAIRecordID, 
			string right, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordRightID = oAIRecordRightID;
			OAIRecordID = oAIRecordID;
			Right = right;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordRight()
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
					case "OAIRecordRightID" :
					{
						_OAIRecordRightID = (int)column.Value;
						break;
					}
					case "OAIRecordID" :
					{
						_OAIRecordID = (int)column.Value;
						break;
					}
					case "Right" :
					{
						_Right = (string)column.Value;
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
		
		#region OAIRecordRightID
		
		private int _OAIRecordRightID = default(int);
		
		/// <summary>
		/// Column: OAIRecordRightID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordRightID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int OAIRecordRightID
		{
			get
			{
				return _OAIRecordRightID;
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
		
		#endregion OAIRecordRightID
		
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
		
		#region Right
		
		private string _Right = string.Empty;
		
		/// <summary>
		/// Column: Right;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Right", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1073741823)]
		public string Right
		{
			get
			{
				return _Right;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Right != value)
				{
					_Right = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Right
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordRight"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordRight"/>, 
		/// returns an instance of <see cref="__OAIRecordRight"/>; otherwise returns null.</returns>
		public static new __OAIRecordRight FromArray(byte[] byteArray)
		{
			__OAIRecordRight o = null;
			
			try
			{
				o = (__OAIRecordRight) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordRight"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordRight"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordRight)
			{
				__OAIRecordRight o = (__OAIRecordRight) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordRightID == OAIRecordRightID &&
					o.OAIRecordID == OAIRecordID &&
					GetComparisonString(o.Right) == GetComparisonString(Right) &&
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
				throw new ArgumentException("Argument is not of type __OAIRecordRight");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordRight"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordRight"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordRight a, __OAIRecordRight b)
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
		/// <param name="a">The first <see cref="__OAIRecordRight"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordRight"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordRight a, __OAIRecordRight b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordRight"/> object to compare with the current <see cref="__OAIRecordRight"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordRight))
			{
				return false;
			}
			
			return this == (__OAIRecordRight) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordRight.SortColumn.OAIRecordRightID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordRightID = "OAIRecordRightID";	
			public const string OAIRecordID = "OAIRecordID";	
			public const string Right = "Right";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
