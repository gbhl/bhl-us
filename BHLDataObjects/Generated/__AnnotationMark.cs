
// Generated 1/5/2021 3:36:34 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationMark is based upon annotation.AnnotationMark.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationMark : __AnnotationMark
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
	public abstract class __AnnotationMark : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationMark()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationMarkID"></param>
		/// <param name="annotationID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="position"></param>
		/// <param name="annotationMarkTypeID"></param>
		/// <param name="content"></param>
		/// <param name="correctedContent"></param>
		/// <param name="comment"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationMark(int annotationMarkID, 
			int annotationID, 
			string externalIdentifier, 
			int sequenceNumber, 
			string position, 
			int? annotationMarkTypeID, 
			string content, 
			string correctedContent, 
			string comment, 
			int? polygonX1, 
			int? polygonY1, 
			int? polygonX2, 
			int? polygonY2, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationMarkID = annotationMarkID;
			AnnotationID = annotationID;
			ExternalIdentifier = externalIdentifier;
			SequenceNumber = sequenceNumber;
			Position = position;
			AnnotationMarkTypeID = annotationMarkTypeID;
			Content = content;
			CorrectedContent = correctedContent;
			Comment = comment;
			PolygonX1 = polygonX1;
			PolygonY1 = polygonY1;
			PolygonX2 = polygonX2;
			PolygonY2 = polygonY2;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationMark()
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
					case "AnnotationMarkID" :
					{
						_AnnotationMarkID = (int)column.Value;
						break;
					}
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "ExternalIdentifier" :
					{
						_ExternalIdentifier = (string)column.Value;
						break;
					}
					case "SequenceNumber" :
					{
						_SequenceNumber = (int)column.Value;
						break;
					}
					case "Position" :
					{
						_Position = (string)column.Value;
						break;
					}
					case "AnnotationMarkTypeID" :
					{
						_AnnotationMarkTypeID = (int?)column.Value;
						break;
					}
					case "Content" :
					{
						_Content = (string)column.Value;
						break;
					}
					case "CorrectedContent" :
					{
						_CorrectedContent = (string)column.Value;
						break;
					}
					case "Comment" :
					{
						_Comment = (string)column.Value;
						break;
					}
					case "PolygonX1" :
					{
						_PolygonX1 = (int?)column.Value;
						break;
					}
					case "PolygonY1" :
					{
						_PolygonY1 = (int?)column.Value;
						break;
					}
					case "PolygonX2" :
					{
						_PolygonX2 = (int?)column.Value;
						break;
					}
					case "PolygonY2" :
					{
						_PolygonY2 = (int?)column.Value;
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
		
		#region AnnotationMarkID
		
		private int _AnnotationMarkID = default(int);
		
		/// <summary>
		/// Column: AnnotationMarkID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationMarkID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AnnotationMarkID
		{
			get
			{
				return _AnnotationMarkID;
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
		
		#endregion AnnotationMarkID
		
		#region AnnotationID
		
		private int _AnnotationID = default(int);
		
		/// <summary>
		/// Column: AnnotationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
		public int AnnotationID
		{
			get
			{
				return _AnnotationID;
			}
			set
			{
				if (_AnnotationID != value)
				{
					_AnnotationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationID
		
		#region ExternalIdentifier
		
		private string _ExternalIdentifier = string.Empty;
		
		/// <summary>
		/// Column: ExternalIdentifier;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ExternalIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string ExternalIdentifier
		{
			get
			{
				return _ExternalIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ExternalIdentifier != value)
				{
					_ExternalIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalIdentifier
		
		#region SequenceNumber
		
		private int _SequenceNumber = default(int);
		
		/// <summary>
		/// Column: SequenceNumber;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SequenceNumber", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10)]
		public int SequenceNumber
		{
			get
			{
				return _SequenceNumber;
			}
			set
			{
				if (_SequenceNumber != value)
				{
					_SequenceNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceNumber
		
		#region Position
		
		private string _Position = string.Empty;
		
		/// <summary>
		/// Column: Position;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Position", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string Position
		{
			get
			{
				return _Position;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Position != value)
				{
					_Position = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Position
		
		#region AnnotationMarkTypeID
		
		private int? _AnnotationMarkTypeID = null;
		
		/// <summary>
		/// Column: AnnotationMarkTypeID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("AnnotationMarkTypeID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? AnnotationMarkTypeID
		{
			get
			{
				return _AnnotationMarkTypeID;
			}
			set
			{
				if (_AnnotationMarkTypeID != value)
				{
					_AnnotationMarkTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationMarkTypeID
		
		#region Content
		
		private string _Content = string.Empty;
		
		/// <summary>
		/// Column: Content;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Content", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=1073741823)]
		public string Content
		{
			get
			{
				return _Content;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Content != value)
				{
					_Content = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Content
		
		#region CorrectedContent
		
		private string _CorrectedContent = string.Empty;
		
		/// <summary>
		/// Column: CorrectedContent;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CorrectedContent", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1073741823)]
		public string CorrectedContent
		{
			get
			{
				return _CorrectedContent;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CorrectedContent != value)
				{
					_CorrectedContent = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CorrectedContent
		
		#region Comment
		
		private string _Comment = string.Empty;
		
		/// <summary>
		/// Column: Comment;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Comment", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=1073741823)]
		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Comment != value)
				{
					_Comment = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Comment
		
		#region PolygonX1
		
		private int? _PolygonX1 = null;
		
		/// <summary>
		/// Column: PolygonX1;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonX1", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? PolygonX1
		{
			get
			{
				return _PolygonX1;
			}
			set
			{
				if (_PolygonX1 != value)
				{
					_PolygonX1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonX1
		
		#region PolygonY1
		
		private int? _PolygonY1 = null;
		
		/// <summary>
		/// Column: PolygonY1;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonY1", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10, IsNullable=true)]
		public int? PolygonY1
		{
			get
			{
				return _PolygonY1;
			}
			set
			{
				if (_PolygonY1 != value)
				{
					_PolygonY1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonY1
		
		#region PolygonX2
		
		private int? _PolygonX2 = null;
		
		/// <summary>
		/// Column: PolygonX2;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonX2", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10, IsNullable=true)]
		public int? PolygonX2
		{
			get
			{
				return _PolygonX2;
			}
			set
			{
				if (_PolygonX2 != value)
				{
					_PolygonX2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonX2
		
		#region PolygonY2
		
		private int? _PolygonY2 = null;
		
		/// <summary>
		/// Column: PolygonY2;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonY2", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10, IsNullable=true)]
		public int? PolygonY2
		{
			get
			{
				return _PolygonY2;
			}
			set
			{
				if (_PolygonY2 != value)
				{
					_PolygonY2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonY2
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=14)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=15)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationMark"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationMark"/>, 
		/// returns an instance of <see cref="__AnnotationMark"/>; otherwise returns null.</returns>
		public static new __AnnotationMark FromArray(byte[] byteArray)
		{
			__AnnotationMark o = null;
			
			try
			{
				o = (__AnnotationMark) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationMark"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationMark"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationMark)
			{
				__AnnotationMark o = (__AnnotationMark) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationMarkID == AnnotationMarkID &&
					o.AnnotationID == AnnotationID &&
					GetComparisonString(o.ExternalIdentifier) == GetComparisonString(ExternalIdentifier) &&
					o.SequenceNumber == SequenceNumber &&
					GetComparisonString(o.Position) == GetComparisonString(Position) &&
					o.AnnotationMarkTypeID == AnnotationMarkTypeID &&
					GetComparisonString(o.Content) == GetComparisonString(Content) &&
					GetComparisonString(o.CorrectedContent) == GetComparisonString(CorrectedContent) &&
					GetComparisonString(o.Comment) == GetComparisonString(Comment) &&
					o.PolygonX1 == PolygonX1 &&
					o.PolygonY1 == PolygonY1 &&
					o.PolygonX2 == PolygonX2 &&
					o.PolygonY2 == PolygonY2 &&
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
				throw new ArgumentException("Argument is not of type __AnnotationMark");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationMark"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationMark"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationMark a, __AnnotationMark b)
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
		/// <param name="a">The first <see cref="__AnnotationMark"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationMark"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationMark a, __AnnotationMark b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationMark"/> object to compare with the current <see cref="__AnnotationMark"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationMark))
			{
				return false;
			}
			
			return this == (__AnnotationMark) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationMark.SortColumn.AnnotationMarkID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationMarkID = "AnnotationMarkID";	
			public const string AnnotationID = "AnnotationID";	
			public const string ExternalIdentifier = "ExternalIdentifier";	
			public const string SequenceNumber = "SequenceNumber";	
			public const string Position = "Position";	
			public const string AnnotationMarkTypeID = "AnnotationMarkTypeID";	
			public const string Content = "Content";	
			public const string CorrectedContent = "CorrectedContent";	
			public const string Comment = "Comment";	
			public const string PolygonX1 = "PolygonX1";	
			public const string PolygonY1 = "PolygonY1";	
			public const string PolygonX2 = "PolygonX2";	
			public const string PolygonY2 = "PolygonY2";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

