
// Generated 11/12/2008 3:12:29 PM
// Do not modify the contents of this code file.
// This abstract class __MarcControl is based upon MarcControl.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DataObjects
// {
//		[Serializable]
// 		public class MarcControl : __MarcControl
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
	public abstract class __MarcControl : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __MarcControl()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcControlID"></param>
		/// <param name="itemID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <param name="creationDate"></param>
		public __MarcControl(int marcControlID, 
			int itemID, 
			string tag, 
			string value, 
			DateTime creationDate) : this()
		{
			_MarcControlID = marcControlID;
			ItemID = itemID;
			Tag = tag;
			Value = value;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__MarcControl()
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
					case "MarcControlID" :
					{
						_MarcControlID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "Tag" :
					{
						_Tag = (string)column.Value;
						break;
					}
					case "Value" :
					{
						_Value = (string)column.Value;
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
		
		#region MarcControlID
		
		private int _MarcControlID = default(int);
		
		/// <summary>
		/// Column: MarcControlID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcControlID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int MarcControlID
		{
			get
			{
				return _MarcControlID;
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
		
		#endregion MarcControlID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				if (_ItemID != value)
				{
					_ItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemID
		
		#region Tag
		
		private string _Tag = string.Empty;
		
		/// <summary>
		/// Column: Tag;
		/// DBMS data type: nchar(3);
		/// </summary>
		[ColumnDefinition("Tag", DbTargetType=SqlDbType.NChar, Ordinal=3, CharacterMaxLength=3)]
		public string Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 3);
				if (_Tag != value)
				{
					_Tag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Tag
		
		#region Value
		
		private string _Value = string.Empty;
		
		/// <summary>
		/// Column: Value;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Value", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=200)]
		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Value != value)
				{
					_Value = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Value
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__MarcControl"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__MarcControl"/>, 
		/// returns an instance of <see cref="__MarcControl"/>; otherwise returns null.</returns>
		public static new __MarcControl FromArray(byte[] byteArray)
		{
			__MarcControl o = null;
			
			try
			{
				o = (__MarcControl) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__MarcControl"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__MarcControl"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __MarcControl)
			{
				__MarcControl o = (__MarcControl) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcControlID == MarcControlID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.Tag) == GetComparisonString(Tag) &&
					GetComparisonString(o.Value) == GetComparisonString(Value) &&
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
				throw new ArgumentException("Argument is not of type __MarcControl");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__MarcControl"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcControl"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__MarcControl a, __MarcControl b)
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
		/// <param name="a">The first <see cref="__MarcControl"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcControl"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__MarcControl a, __MarcControl b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__MarcControl"/> object to compare with the current <see cref="__MarcControl"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __MarcControl))
			{
				return false;
			}
			
			return this == (__MarcControl) obj;
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
		/// list.Sort(SortOrder.Ascending, __MarcControl.SortColumn.MarcControlID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcControlID = "MarcControlID";	
			public const string ItemID = "ItemID";	
			public const string Tag = "Tag";	
			public const string Value = "Value";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
