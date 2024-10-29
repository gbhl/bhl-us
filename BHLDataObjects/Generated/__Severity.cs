
// Generated 10/25/2024 11:54:37 AM
// Do not modify the contents of this code file.
// This abstract class __Severity is based upon servlog.Severity.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Severity : __Severity
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
	public abstract class __Severity : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Severity()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="severityID"></param>
		/// <param name="name"></param>
		/// <param name="label"></param>
		/// <param name="fGColorHexCode"></param>
		/// <param name="creationDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Severity(int severityID, 
			string name, 
			string label, 
			string fGColorHexCode, 
			DateTime creationDate, 
			int creationUserID, 
			DateTime lastModifiedDate, 
			int lastModifiedUserID) : this()
		{
			_SeverityID = severityID;
			Name = name;
			Label = label;
			FGColorHexCode = fGColorHexCode;
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
		~__Severity()
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
					case "SeverityID" :
					{
						_SeverityID = (int)column.Value;
						break;
					}
					case "Name" :
					{
						_Name = (string)column.Value;
						break;
					}
					case "Label" :
					{
						_Label = (string)column.Value;
						break;
					}
					case "FGColorHexCode" :
					{
						_FGColorHexCode = (string)column.Value;
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
		
		#region SeverityID
		
		private int _SeverityID = default(int);
		
		/// <summary>
		/// Column: SeverityID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SeverityID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int SeverityID
		{
			get
			{
				return _SeverityID;
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
		
		#endregion SeverityID
		
		#region Name
		
		private string _Name = string.Empty;
		
		/// <summary>
		/// Column: Name;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("Name", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Name != value)
				{
					_Name = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Name
		
		#region Label
		
		private string _Label = string.Empty;
		
		/// <summary>
		/// Column: Label;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("Label", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=30)]
		public string Label
		{
			get
			{
				return _Label;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Label != value)
				{
					_Label = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Label
		
		#region FGColorHexCode
		
		private string _FGColorHexCode = string.Empty;
		
		/// <summary>
		/// Column: FGColorHexCode;
		/// DBMS data type: varchar(10);
		/// </summary>
		[ColumnDefinition("FGColorHexCode", DbTargetType=SqlDbType.VarChar, Ordinal=4, CharacterMaxLength=10)]
		public string FGColorHexCode
		{
			get
			{
				return _FGColorHexCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_FGColorHexCode != value)
				{
					_FGColorHexCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FGColorHexCode
		
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10)]
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
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Severity"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Severity"/>, 
		/// returns an instance of <see cref="__Severity"/>; otherwise returns null.</returns>
		public static new __Severity FromArray(byte[] byteArray)
		{
			__Severity o = null;
			
			try
			{
				o = (__Severity) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Severity"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Severity"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Severity)
			{
				__Severity o = (__Severity) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SeverityID == SeverityID &&
					GetComparisonString(o.Name) == GetComparisonString(Name) &&
					GetComparisonString(o.Label) == GetComparisonString(Label) &&
					GetComparisonString(o.FGColorHexCode) == GetComparisonString(FGColorHexCode) &&
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
				throw new ArgumentException("Argument is not of type __Severity");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Severity"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Severity"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Severity a, __Severity b)
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
		/// <param name="a">The first <see cref="__Severity"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Severity"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Severity a, __Severity b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Severity"/> object to compare with the current <see cref="__Severity"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Severity))
			{
				return false;
			}
			
			return this == (__Severity) obj;
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
		/// list.Sort(SortOrder.Ascending, __Severity.SortColumn.SeverityID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SeverityID = "SeverityID";	
			public const string Name = "Name";	
			public const string Label = "Label";	
			public const string FGColorHexCode = "FGColorHexCode";	
			public const string CreationDate = "CreationDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

