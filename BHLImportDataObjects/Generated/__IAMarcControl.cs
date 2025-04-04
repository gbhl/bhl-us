
// Generated 1/5/2021 2:14:45 PM
// Do not modify the contents of this code file.
// This abstract class __IAMarcControl is based upon dbo.IAMarcControl.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAMarcControl : __IAMarcControl
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
	public abstract class __IAMarcControl : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAMarcControl()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcControlID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAMarcControl(int marcControlID, 
			int marcID, 
			string tag, 
			string value, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcControlID = marcControlID;
			MarcID = marcID;
			Tag = tag;
			Value = value;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAMarcControl()
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
					case "MarcID" :
					{
						_MarcID = (int)column.Value;
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
		
		#region MarcID
		
		private int _MarcID = default(int);
		
		/// <summary>
		/// Column: MarcID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int MarcID
		{
			get
			{
				return _MarcID;
			}
			set
			{
				if (_MarcID != value)
				{
					_MarcID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcID
		
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
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Value", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=2000)]
		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Value != value)
				{
					_Value = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Value
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAMarcControl"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAMarcControl"/>, 
		/// returns an instance of <see cref="__IAMarcControl"/>; otherwise returns null.</returns>
		public static new __IAMarcControl FromArray(byte[] byteArray)
		{
			__IAMarcControl o = null;
			
			try
			{
				o = (__IAMarcControl) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAMarcControl"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAMarcControl"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAMarcControl)
			{
				__IAMarcControl o = (__IAMarcControl) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcControlID == MarcControlID &&
					o.MarcID == MarcID &&
					GetComparisonString(o.Tag) == GetComparisonString(Tag) &&
					GetComparisonString(o.Value) == GetComparisonString(Value) &&
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
				throw new ArgumentException("Argument is not of type __IAMarcControl");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAMarcControl"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcControl"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAMarcControl a, __IAMarcControl b)
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
		/// <param name="a">The first <see cref="__IAMarcControl"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcControl"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAMarcControl a, __IAMarcControl b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAMarcControl"/> object to compare with the current <see cref="__IAMarcControl"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAMarcControl))
			{
				return false;
			}
			
			return this == (__IAMarcControl) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAMarcControl.SortColumn.MarcControlID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcControlID = "MarcControlID";	
			public const string MarcID = "MarcID";	
			public const string Tag = "Tag";	
			public const string Value = "Value";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

