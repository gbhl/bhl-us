
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordSubject is based upon OAIRecordSubject.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordSubject : __OAIRecordSubject
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
	public abstract class __OAIRecordSubject : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordSubject()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordSubjectID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="productionKeywordID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecordSubject(int oAIRecordSubjectID, 
			int oAIRecordID, 
			string keyword, 
			int? productionKeywordID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordSubjectID = oAIRecordSubjectID;
			OAIRecordID = oAIRecordID;
			Keyword = keyword;
			ProductionKeywordID = productionKeywordID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordSubject()
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
					case "OAIRecordSubjectID" :
					{
						_OAIRecordSubjectID = (int)column.Value;
						break;
					}
					case "OAIRecordID" :
					{
						_OAIRecordID = (int)column.Value;
						break;
					}
					case "Keyword" :
					{
						_Keyword = (string)column.Value;
						break;
					}
					case "ProductionKeywordID" :
					{
						_ProductionKeywordID = (int?)column.Value;
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
		
		#region OAIRecordSubjectID
		
		private int _OAIRecordSubjectID = default(int);
		
		/// <summary>
		/// Column: OAIRecordSubjectID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordSubjectID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int OAIRecordSubjectID
		{
			get
			{
				return _OAIRecordSubjectID;
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
		
		#endregion OAIRecordSubjectID
		
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
		
		#region Keyword
		
		private string _Keyword = string.Empty;
		
		/// <summary>
		/// Column: Keyword;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Keyword", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string Keyword
		{
			get
			{
				return _Keyword;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Keyword != value)
				{
					_Keyword = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Keyword
		
		#region ProductionKeywordID
		
		private int? _ProductionKeywordID = null;
		
		/// <summary>
		/// Column: ProductionKeywordID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionKeywordID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? ProductionKeywordID
		{
			get
			{
				return _ProductionKeywordID;
			}
			set
			{
				if (_ProductionKeywordID != value)
				{
					_ProductionKeywordID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionKeywordID
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordSubject"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordSubject"/>, 
		/// returns an instance of <see cref="__OAIRecordSubject"/>; otherwise returns null.</returns>
		public static new __OAIRecordSubject FromArray(byte[] byteArray)
		{
			__OAIRecordSubject o = null;
			
			try
			{
				o = (__OAIRecordSubject) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordSubject"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordSubject"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordSubject)
			{
				__OAIRecordSubject o = (__OAIRecordSubject) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordSubjectID == OAIRecordSubjectID &&
					o.OAIRecordID == OAIRecordID &&
					GetComparisonString(o.Keyword) == GetComparisonString(Keyword) &&
					o.ProductionKeywordID == ProductionKeywordID &&
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
				throw new ArgumentException("Argument is not of type __OAIRecordSubject");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordSubject"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordSubject"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordSubject a, __OAIRecordSubject b)
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
		/// <param name="a">The first <see cref="__OAIRecordSubject"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordSubject"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordSubject a, __OAIRecordSubject b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordSubject"/> object to compare with the current <see cref="__OAIRecordSubject"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordSubject))
			{
				return false;
			}
			
			return this == (__OAIRecordSubject) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordSubject.SortColumn.OAIRecordSubjectID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordSubjectID = "OAIRecordSubjectID";	
			public const string OAIRecordID = "OAIRecordID";	
			public const string Keyword = "Keyword";	
			public const string ProductionKeywordID = "ProductionKeywordID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
