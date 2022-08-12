
// Generated 1/5/2021 3:36:42 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationSubject is based upon annotation.AnnotationSubject.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationSubject : __AnnotationSubject
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
	public abstract class __AnnotationSubject : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationSubject()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationSubjectID"></param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <param name="subjectText"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationSubject(int annotationSubjectID, 
			int annotationID, 
			int? annotationSubjectCategoryID, 
			int annotationKeywordTargetID, 
			string subjectText, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationSubjectID = annotationSubjectID;
			AnnotationID = annotationID;
			AnnotationSubjectCategoryID = annotationSubjectCategoryID;
			AnnotationKeywordTargetID = annotationKeywordTargetID;
			SubjectText = subjectText;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationSubject()
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
					case "AnnotationSubjectID" :
					{
						_AnnotationSubjectID = (int)column.Value;
						break;
					}
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "AnnotationSubjectCategoryID" :
					{
						_AnnotationSubjectCategoryID = (int?)column.Value;
						break;
					}
					case "AnnotationKeywordTargetID" :
					{
						_AnnotationKeywordTargetID = (int)column.Value;
						break;
					}
					case "SubjectText" :
					{
						_SubjectText = (string)column.Value;
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
		
		#region AnnotationSubjectID
		
		private int _AnnotationSubjectID = default(int);
		
		/// <summary>
		/// Column: AnnotationSubjectID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationSubjectID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AnnotationSubjectID
		{
			get
			{
				return _AnnotationSubjectID;
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
		
		#endregion AnnotationSubjectID
		
		#region AnnotationID
		
		private int _AnnotationID = default(int);
		
		/// <summary>
		/// Column: AnnotationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
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
		
		#region AnnotationSubjectCategoryID
		
		private int? _AnnotationSubjectCategoryID = null;
		
		/// <summary>
		/// Column: AnnotationSubjectCategoryID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("AnnotationSubjectCategoryID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? AnnotationSubjectCategoryID
		{
			get
			{
				return _AnnotationSubjectCategoryID;
			}
			set
			{
				if (_AnnotationSubjectCategoryID != value)
				{
					_AnnotationSubjectCategoryID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationSubjectCategoryID
		
		#region AnnotationKeywordTargetID
		
		private int _AnnotationKeywordTargetID = default(int);
		
		/// <summary>
		/// Column: AnnotationKeywordTargetID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationKeywordTargetID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotationKeywordTargetID
		{
			get
			{
				return _AnnotationKeywordTargetID;
			}
			set
			{
				if (_AnnotationKeywordTargetID != value)
				{
					_AnnotationKeywordTargetID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationKeywordTargetID
		
		#region SubjectText
		
		private string _SubjectText = string.Empty;
		
		/// <summary>
		/// Column: SubjectText;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("SubjectText", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=150)]
		public string SubjectText
		{
			get
			{
				return _SubjectText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_SubjectText != value)
				{
					_SubjectText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SubjectText
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationSubject"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationSubject"/>, 
		/// returns an instance of <see cref="__AnnotationSubject"/>; otherwise returns null.</returns>
		public static new __AnnotationSubject FromArray(byte[] byteArray)
		{
			__AnnotationSubject o = null;
			
			try
			{
				o = (__AnnotationSubject) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationSubject"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationSubject"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationSubject)
			{
				__AnnotationSubject o = (__AnnotationSubject) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationSubjectID == AnnotationSubjectID &&
					o.AnnotationID == AnnotationID &&
					o.AnnotationSubjectCategoryID == AnnotationSubjectCategoryID &&
					o.AnnotationKeywordTargetID == AnnotationKeywordTargetID &&
					GetComparisonString(o.SubjectText) == GetComparisonString(SubjectText) &&
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
				throw new ArgumentException("Argument is not of type __AnnotationSubject");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationSubject"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationSubject"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationSubject a, __AnnotationSubject b)
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
		/// <param name="a">The first <see cref="__AnnotationSubject"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationSubject"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationSubject a, __AnnotationSubject b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationSubject"/> object to compare with the current <see cref="__AnnotationSubject"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationSubject))
			{
				return false;
			}
			
			return this == (__AnnotationSubject) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationSubject.SortColumn.AnnotationSubjectID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationSubjectID = "AnnotationSubjectID";	
			public const string AnnotationID = "AnnotationID";	
			public const string AnnotationSubjectCategoryID = "AnnotationSubjectCategoryID";	
			public const string AnnotationKeywordTargetID = "AnnotationKeywordTargetID";	
			public const string SubjectText = "SubjectText";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

