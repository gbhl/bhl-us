
// Generated 1/5/2021 3:36:29 PM
// Do not modify the contents of this code file.
// This abstract class __Annotation is based upon annotation.Annotation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Annotation : __Annotation
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
	public abstract class __Annotation : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Annotation()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="annotationTextDescription"></param>
		/// <param name="annotationText"></param>
		/// <param name="annotationTextClean"></param>
		/// <param name="annotationTextDisplay"></param>
		/// <param name="annotationTextCorrected"></param>
		/// <param name="comment"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Annotation(int annotationID, 
			int annotationSourceID, 
			string externalIdentifier, 
			int sequenceNumber, 
			string annotationTextDescription, 
			string annotationText, 
			string annotationTextClean, 
			string annotationTextDisplay, 
			string annotationTextCorrected, 
			string comment, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationID = annotationID;
			AnnotationSourceID = annotationSourceID;
			ExternalIdentifier = externalIdentifier;
			SequenceNumber = sequenceNumber;
			AnnotationTextDescription = annotationTextDescription;
			AnnotationText = annotationText;
			AnnotationTextClean = annotationTextClean;
			AnnotationTextDisplay = annotationTextDisplay;
			AnnotationTextCorrected = annotationTextCorrected;
			Comment = comment;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Annotation()
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
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "AnnotationSourceID" :
					{
						_AnnotationSourceID = (int)column.Value;
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
					case "AnnotationTextDescription" :
					{
						_AnnotationTextDescription = (string)column.Value;
						break;
					}
					case "AnnotationText" :
					{
						_AnnotationText = (string)column.Value;
						break;
					}
					case "AnnotationTextClean" :
					{
						_AnnotationTextClean = (string)column.Value;
						break;
					}
					case "AnnotationTextDisplay" :
					{
						_AnnotationTextDisplay = (string)column.Value;
						break;
					}
					case "AnnotationTextCorrected" :
					{
						_AnnotationTextCorrected = (string)column.Value;
						break;
					}
					case "Comment" :
					{
						_Comment = (string)column.Value;
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
		
		#region AnnotationID
		
		private int _AnnotationID = default(int);
		
		/// <summary>
		/// Column: AnnotationID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotationID
		{
			get
			{
				return _AnnotationID;
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
		
		#endregion AnnotationID
		
		#region AnnotationSourceID
		
		private int _AnnotationSourceID = default(int);
		
		/// <summary>
		/// Column: AnnotationSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationSourceID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotationSourceID
		{
			get
			{
				return _AnnotationSourceID;
			}
			set
			{
				if (_AnnotationSourceID != value)
				{
					_AnnotationSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationSourceID
		
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
		
		#region AnnotationTextDescription
		
		private string _AnnotationTextDescription = string.Empty;
		
		/// <summary>
		/// Column: AnnotationTextDescription;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("AnnotationTextDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string AnnotationTextDescription
		{
			get
			{
				return _AnnotationTextDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_AnnotationTextDescription != value)
				{
					_AnnotationTextDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationTextDescription
		
		#region AnnotationText
		
		private string _AnnotationText = string.Empty;
		
		/// <summary>
		/// Column: AnnotationText;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("AnnotationText", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=1073741823)]
		public string AnnotationText
		{
			get
			{
				return _AnnotationText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_AnnotationText != value)
				{
					_AnnotationText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationText
		
		#region AnnotationTextClean
		
		private string _AnnotationTextClean = string.Empty;
		
		/// <summary>
		/// Column: AnnotationTextClean;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("AnnotationTextClean", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=1073741823)]
		public string AnnotationTextClean
		{
			get
			{
				return _AnnotationTextClean;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_AnnotationTextClean != value)
				{
					_AnnotationTextClean = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationTextClean
		
		#region AnnotationTextDisplay
		
		private string _AnnotationTextDisplay = string.Empty;
		
		/// <summary>
		/// Column: AnnotationTextDisplay;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("AnnotationTextDisplay", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1073741823)]
		public string AnnotationTextDisplay
		{
			get
			{
				return _AnnotationTextDisplay;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_AnnotationTextDisplay != value)
				{
					_AnnotationTextDisplay = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationTextDisplay
		
		#region AnnotationTextCorrected
		
		private string _AnnotationTextCorrected = string.Empty;
		
		/// <summary>
		/// Column: AnnotationTextCorrected;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("AnnotationTextCorrected", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=1073741823)]
		public string AnnotationTextCorrected
		{
			get
			{
				return _AnnotationTextCorrected;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_AnnotationTextCorrected != value)
				{
					_AnnotationTextCorrected = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationTextCorrected
		
		#region Comment
		
		private string _Comment = string.Empty;
		
		/// <summary>
		/// Column: Comment;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Comment", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=1073741823)]
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
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=11)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Annotation"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Annotation"/>, 
		/// returns an instance of <see cref="__Annotation"/>; otherwise returns null.</returns>
		public static new __Annotation FromArray(byte[] byteArray)
		{
			__Annotation o = null;
			
			try
			{
				o = (__Annotation) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Annotation"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Annotation"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Annotation)
			{
				__Annotation o = (__Annotation) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationID == AnnotationID &&
					o.AnnotationSourceID == AnnotationSourceID &&
					GetComparisonString(o.ExternalIdentifier) == GetComparisonString(ExternalIdentifier) &&
					o.SequenceNumber == SequenceNumber &&
					GetComparisonString(o.AnnotationTextDescription) == GetComparisonString(AnnotationTextDescription) &&
					GetComparisonString(o.AnnotationText) == GetComparisonString(AnnotationText) &&
					GetComparisonString(o.AnnotationTextClean) == GetComparisonString(AnnotationTextClean) &&
					GetComparisonString(o.AnnotationTextDisplay) == GetComparisonString(AnnotationTextDisplay) &&
					GetComparisonString(o.AnnotationTextCorrected) == GetComparisonString(AnnotationTextCorrected) &&
					GetComparisonString(o.Comment) == GetComparisonString(Comment) &&
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
				throw new ArgumentException("Argument is not of type __Annotation");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Annotation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Annotation"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Annotation a, __Annotation b)
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
		/// <param name="a">The first <see cref="__Annotation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Annotation"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Annotation a, __Annotation b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Annotation"/> object to compare with the current <see cref="__Annotation"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Annotation))
			{
				return false;
			}
			
			return this == (__Annotation) obj;
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
		/// list.Sort(SortOrder.Ascending, __Annotation.SortColumn.AnnotationID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationID = "AnnotationID";	
			public const string AnnotationSourceID = "AnnotationSourceID";	
			public const string ExternalIdentifier = "ExternalIdentifier";	
			public const string SequenceNumber = "SequenceNumber";	
			public const string AnnotationTextDescription = "AnnotationTextDescription";	
			public const string AnnotationText = "AnnotationText";	
			public const string AnnotationTextClean = "AnnotationTextClean";	
			public const string AnnotationTextDisplay = "AnnotationTextDisplay";	
			public const string AnnotationTextCorrected = "AnnotationTextCorrected";	
			public const string Comment = "Comment";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

