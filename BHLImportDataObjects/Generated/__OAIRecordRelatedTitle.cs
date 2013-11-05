
// Generated 11/5/2013 11:13:30 AM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordRelatedTitle is based upon OAIRecordRelatedTitle.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordRelatedTitle : __OAIRecordRelatedTitle
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
	public abstract class __OAIRecordRelatedTitle : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordRelatedTitle()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="titleType"></param>
		/// <param name="title"></param>
		/// <param name="productionTitleAssociationID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecordRelatedTitle(int oAIRecordRelatedTitleID, 
			int oAIRecordID, 
			string titleType, 
			string title, 
			int? productionTitleAssociationID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordRelatedTitleID = oAIRecordRelatedTitleID;
			OAIRecordID = oAIRecordID;
			TitleType = titleType;
			Title = title;
			ProductionTitleAssociationID = productionTitleAssociationID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordRelatedTitle()
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
					case "OAIRecordRelatedTitleID" :
					{
						_OAIRecordRelatedTitleID = (int)column.Value;
						break;
					}
					case "OAIRecordID" :
					{
						_OAIRecordID = (int)column.Value;
						break;
					}
					case "TitleType" :
					{
						_TitleType = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "ProductionTitleAssociationID" :
					{
						_ProductionTitleAssociationID = (int?)column.Value;
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
		
		#region OAIRecordRelatedTitleID
		
		private int _OAIRecordRelatedTitleID = default(int);
		
		/// <summary>
		/// Column: OAIRecordRelatedTitleID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordRelatedTitleID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int OAIRecordRelatedTitleID
		{
			get
			{
				return _OAIRecordRelatedTitleID;
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
		
		#endregion OAIRecordRelatedTitleID
		
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
		
		#region TitleType
		
		private string _TitleType = string.Empty;
		
		/// <summary>
		/// Column: TitleType;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("TitleType", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string TitleType
		{
			get
			{
				return _TitleType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_TitleType != value)
				{
					_TitleType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleType
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=300)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region ProductionTitleAssociationID
		
		private int? _ProductionTitleAssociationID = null;
		
		/// <summary>
		/// Column: ProductionTitleAssociationID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionTitleAssociationID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? ProductionTitleAssociationID
		{
			get
			{
				return _ProductionTitleAssociationID;
			}
			set
			{
				if (_ProductionTitleAssociationID != value)
				{
					_ProductionTitleAssociationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionTitleAssociationID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordRelatedTitle"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordRelatedTitle"/>, 
		/// returns an instance of <see cref="__OAIRecordRelatedTitle"/>; otherwise returns null.</returns>
		public static new __OAIRecordRelatedTitle FromArray(byte[] byteArray)
		{
			__OAIRecordRelatedTitle o = null;
			
			try
			{
				o = (__OAIRecordRelatedTitle) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordRelatedTitle"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordRelatedTitle"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordRelatedTitle)
			{
				__OAIRecordRelatedTitle o = (__OAIRecordRelatedTitle) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordRelatedTitleID == OAIRecordRelatedTitleID &&
					o.OAIRecordID == OAIRecordID &&
					GetComparisonString(o.TitleType) == GetComparisonString(TitleType) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					o.ProductionTitleAssociationID == ProductionTitleAssociationID &&
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
				throw new ArgumentException("Argument is not of type __OAIRecordRelatedTitle");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordRelatedTitle"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordRelatedTitle"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordRelatedTitle a, __OAIRecordRelatedTitle b)
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
		/// <param name="a">The first <see cref="__OAIRecordRelatedTitle"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordRelatedTitle"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordRelatedTitle a, __OAIRecordRelatedTitle b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordRelatedTitle"/> object to compare with the current <see cref="__OAIRecordRelatedTitle"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordRelatedTitle))
			{
				return false;
			}
			
			return this == (__OAIRecordRelatedTitle) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordRelatedTitle.SortColumn.OAIRecordRelatedTitleID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordRelatedTitleID = "OAIRecordRelatedTitleID";	
			public const string OAIRecordID = "OAIRecordID";	
			public const string TitleType = "TitleType";	
			public const string Title = "Title";	
			public const string ProductionTitleAssociationID = "ProductionTitleAssociationID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
