
// Generated 12/29/2021 11:15:07 AM
// Do not modify the contents of this code file.
// This abstract class __Identifier is based upon dbo.Identifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Identifier : __Identifier
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
	public abstract class __Identifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Identifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="identifierID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierLabel"></param>
		/// <param name="prefix"></param>
		/// <param name="patternExpression"></param>
		/// <param name="patternDescription"></param>
		/// <param name="maximumPerEntity"></param>
		/// <param name="display"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Identifier(int identifierID, 
			string identifierType, 
			string identifierName, 
			string identifierLabel, 
			string prefix, 
			string patternExpression, 
			string patternDescription, 
			int maximumPerEntity, 
			short display, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_IdentifierID = identifierID;
			IdentifierType = identifierType;
			IdentifierName = identifierName;
			IdentifierLabel = identifierLabel;
			Prefix = prefix;
			PatternExpression = patternExpression;
			PatternDescription = patternDescription;
			MaximumPerEntity = maximumPerEntity;
			Display = display;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Identifier()
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
					case "IdentifierID" :
					{
						_IdentifierID = (int)column.Value;
						break;
					}
					case "IdentifierType" :
					{
						_IdentifierType = (string)column.Value;
						break;
					}
					case "IdentifierName" :
					{
						_IdentifierName = (string)column.Value;
						break;
					}
					case "IdentifierLabel" :
					{
						_IdentifierLabel = (string)column.Value;
						break;
					}
					case "Prefix" :
					{
						_Prefix = (string)column.Value;
						break;
					}
					case "PatternExpression" :
					{
						_PatternExpression = (string)column.Value;
						break;
					}
					case "PatternDescription" :
					{
						_PatternDescription = (string)column.Value;
						break;
					}
					case "MaximumPerEntity" :
					{
						_MaximumPerEntity = (int)column.Value;
						break;
					}
					case "Display" :
					{
						_Display = (short)column.Value;
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
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region IdentifierID
		
		private int _IdentifierID = default(int);
		
		/// <summary>
		/// Column: IdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("IdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int IdentifierID
		{
			get
			{
				return _IdentifierID;
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
		
		#endregion IdentifierID
		
		#region IdentifierType
		
		private string _IdentifierType = string.Empty;
		
		/// <summary>
		/// Column: IdentifierType;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierType", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=40)]
		public string IdentifierType
		{
			get
			{
				return _IdentifierType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierType != value)
				{
					_IdentifierType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierType
		
		#region IdentifierName
		
		private string _IdentifierName = string.Empty;
		
		/// <summary>
		/// Column: IdentifierName;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40)]
		public string IdentifierName
		{
			get
			{
				return _IdentifierName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierName != value)
				{
					_IdentifierName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierName
		
		#region IdentifierLabel
		
		private string _IdentifierLabel = string.Empty;
		
		/// <summary>
		/// Column: IdentifierLabel;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("IdentifierLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string IdentifierLabel
		{
			get
			{
				return _IdentifierLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_IdentifierLabel != value)
				{
					_IdentifierLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierLabel
		
		#region Prefix
		
		private string _Prefix = string.Empty;
		
		/// <summary>
		/// Column: Prefix;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Prefix", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string Prefix
		{
			get
			{
				return _Prefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Prefix != value)
				{
					_Prefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Prefix
		
		#region PatternExpression
		
		private string _PatternExpression = string.Empty;
		
		/// <summary>
		/// Column: PatternExpression;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("PatternExpression", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
		public string PatternExpression
		{
			get
			{
				return _PatternExpression;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_PatternExpression != value)
				{
					_PatternExpression = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PatternExpression
		
		#region PatternDescription
		
		private string _PatternDescription = string.Empty;
		
		/// <summary>
		/// Column: PatternDescription;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("PatternDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=500)]
		public string PatternDescription
		{
			get
			{
				return _PatternDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_PatternDescription != value)
				{
					_PatternDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PatternDescription
		
		#region MaximumPerEntity
		
		private int _MaximumPerEntity = default(int);
		
		/// <summary>
		/// Column: MaximumPerEntity;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MaximumPerEntity", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int MaximumPerEntity
		{
			get
			{
				return _MaximumPerEntity;
			}
			set
			{
				if (_MaximumPerEntity != value)
				{
					_MaximumPerEntity = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MaximumPerEntity
		
		#region Display
		
		private short _Display = default(short);
		
		/// <summary>
		/// Column: Display;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("Display", DbTargetType=SqlDbType.SmallInt, Ordinal=9, NumericPrecision=5)]
		public short Display
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
		
		#region CreationUserID
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		#region LastModifiedUserID
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__Identifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Identifier"/>, 
		/// returns an instance of <see cref="__Identifier"/>; otherwise returns null.</returns>
		public static new __Identifier FromArray(byte[] byteArray)
		{
			__Identifier o = null;
			
			try
			{
				o = (__Identifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Identifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Identifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Identifier)
			{
				__Identifier o = (__Identifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.IdentifierID == IdentifierID &&
					GetComparisonString(o.IdentifierType) == GetComparisonString(IdentifierType) &&
					GetComparisonString(o.IdentifierName) == GetComparisonString(IdentifierName) &&
					GetComparisonString(o.IdentifierLabel) == GetComparisonString(IdentifierLabel) &&
					GetComparisonString(o.Prefix) == GetComparisonString(Prefix) &&
					GetComparisonString(o.PatternExpression) == GetComparisonString(PatternExpression) &&
					GetComparisonString(o.PatternDescription) == GetComparisonString(PatternDescription) &&
					o.MaximumPerEntity == MaximumPerEntity &&
					o.Display == Display &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
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
				throw new ArgumentException("Argument is not of type __Identifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Identifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Identifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Identifier a, __Identifier b)
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
		/// <param name="a">The first <see cref="__Identifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Identifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Identifier a, __Identifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Identifier"/> object to compare with the current <see cref="__Identifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Identifier))
			{
				return false;
			}
			
			return this == (__Identifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __Identifier.SortColumn.IdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string IdentifierID = "IdentifierID";	
			public const string IdentifierType = "IdentifierType";	
			public const string IdentifierName = "IdentifierName";	
			public const string IdentifierLabel = "IdentifierLabel";	
			public const string Prefix = "Prefix";	
			public const string PatternExpression = "PatternExpression";	
			public const string PatternDescription = "PatternDescription";	
			public const string MaximumPerEntity = "MaximumPerEntity";	
			public const string Display = "Display";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

