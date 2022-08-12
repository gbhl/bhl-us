
// Generated 1/5/2021 12:29:05 PM
// Do not modify the contents of this code file.
// This abstract class __Collection is based upon dbo.Collection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DataObjects
// {
//		[Serializable]
// 		public class Collection : __Collection
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.IAAnalysis.DataObjects
{
	[Serializable]
	public abstract class __Collection : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Collection()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <param name="creationDate"></param>
		public __Collection(int collectionID, 
			string collectionName, 
			DateTime creationDate) : this()
		{
			_CollectionID = collectionID;
			CollectionName = collectionName;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Collection()
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
					case "CollectionID" :
					{
						_CollectionID = (int)column.Value;
						break;
					}
					case "CollectionName" :
					{
						_CollectionName = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region CollectionID
		
		private int _CollectionID = default(int);
		
		/// <summary>
		/// Column: CollectionID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("CollectionID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int CollectionID
		{
			get
			{
				return _CollectionID;
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
		
		#endregion CollectionID
		
		#region CollectionName
		
		private string _CollectionName = string.Empty;
		
		/// <summary>
		/// Column: CollectionName;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("CollectionName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=200)]
		public string CollectionName
		{
			get
			{
				return _CollectionName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_CollectionName != value)
				{
					_CollectionName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CollectionName
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=3)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Collection"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Collection"/>, 
		/// returns an instance of <see cref="__Collection"/>; otherwise returns null.</returns>
		public static new __Collection FromArray(byte[] byteArray)
		{
			__Collection o = null;
			
			try
			{
				o = (__Collection) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Collection"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Collection"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Collection)
			{
				__Collection o = (__Collection) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.CollectionID == CollectionID &&
					GetComparisonString(o.CollectionName) == GetComparisonString(CollectionName) &&
					o.CreationDate == CreationDate 
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
				throw new ArgumentException("Argument is not of type __Collection");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Collection"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Collection"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Collection a, __Collection b)
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
		/// <param name="a">The first <see cref="__Collection"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Collection"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Collection a, __Collection b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Collection"/> object to compare with the current <see cref="__Collection"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Collection))
			{
				return false;
			}
			
			return this == (__Collection) obj;
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
		/// list.Sort(SortOrder.Ascending, __Collection.SortColumn.CollectionID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string CollectionID = "CollectionID";	
			public const string CollectionName = "CollectionName";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

