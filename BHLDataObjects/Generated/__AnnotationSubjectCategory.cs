
// Generated 5/12/2010 3:45:46 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationSubjectCategory is based upon AnnotationSubjectCategory.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationSubjectCategory : __AnnotationSubjectCategory
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
	public abstract class __AnnotationSubjectCategory : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationSubjectCategory()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="subjectCategoryCode"></param>
		/// <param name="subjectCategoryName"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationSubjectCategory(int annotationSubjectCategoryID, 
			int annotationSourceID, 
			string subjectCategoryCode, 
			string subjectCategoryName, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationSubjectCategoryID = annotationSubjectCategoryID;
			AnnotationSourceID = annotationSourceID;
			SubjectCategoryCode = subjectCategoryCode;
			SubjectCategoryName = subjectCategoryName;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationSubjectCategory()
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
					case "AnnotationSubjectCategoryID" :
					{
						_AnnotationSubjectCategoryID = (int)column.Value;
						break;
					}
					case "AnnotationSourceID" :
					{
						_AnnotationSourceID = (int)column.Value;
						break;
					}
					case "SubjectCategoryCode" :
					{
						_SubjectCategoryCode = (string)column.Value;
						break;
					}
					case "SubjectCategoryName" :
					{
						_SubjectCategoryName = (string)column.Value;
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
		
		#region AnnotationSubjectCategoryID
		
		private int _AnnotationSubjectCategoryID = default(int);
		
		/// <summary>
		/// Column: AnnotationSubjectCategoryID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationSubjectCategoryID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotationSubjectCategoryID
		{
			get
			{
				return _AnnotationSubjectCategoryID;
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
		
		#endregion AnnotationSubjectCategoryID
		
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
		
		#region SubjectCategoryCode
		
		private string _SubjectCategoryCode = string.Empty;
		
		/// <summary>
		/// Column: SubjectCategoryCode;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("SubjectCategoryCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=20)]
		public string SubjectCategoryCode
		{
			get
			{
				return _SubjectCategoryCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_SubjectCategoryCode != value)
				{
					_SubjectCategoryCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SubjectCategoryCode
		
		#region SubjectCategoryName
		
		private string _SubjectCategoryName = string.Empty;
		
		/// <summary>
		/// Column: SubjectCategoryName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("SubjectCategoryName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string SubjectCategoryName
		{
			get
			{
				return _SubjectCategoryName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SubjectCategoryName != value)
				{
					_SubjectCategoryName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SubjectCategoryName
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationSubjectCategory"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationSubjectCategory"/>, 
		/// returns an instance of <see cref="__AnnotationSubjectCategory"/>; otherwise returns null.</returns>
		public static new __AnnotationSubjectCategory FromArray(byte[] byteArray)
		{
			__AnnotationSubjectCategory o = null;
			
			try
			{
				o = (__AnnotationSubjectCategory) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationSubjectCategory"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationSubjectCategory"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationSubjectCategory)
			{
				__AnnotationSubjectCategory o = (__AnnotationSubjectCategory) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationSubjectCategoryID == AnnotationSubjectCategoryID &&
					o.AnnotationSourceID == AnnotationSourceID &&
					GetComparisonString(o.SubjectCategoryCode) == GetComparisonString(SubjectCategoryCode) &&
					GetComparisonString(o.SubjectCategoryName) == GetComparisonString(SubjectCategoryName) &&
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
				throw new ArgumentException("Argument is not of type __AnnotationSubjectCategory");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationSubjectCategory"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationSubjectCategory"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationSubjectCategory a, __AnnotationSubjectCategory b)
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
		/// <param name="a">The first <see cref="__AnnotationSubjectCategory"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationSubjectCategory"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationSubjectCategory a, __AnnotationSubjectCategory b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationSubjectCategory"/> object to compare with the current <see cref="__AnnotationSubjectCategory"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationSubjectCategory))
			{
				return false;
			}
			
			return this == (__AnnotationSubjectCategory) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationSubjectCategory.SortColumn.AnnotationSubjectCategoryID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationSubjectCategoryID = "AnnotationSubjectCategoryID";	
			public const string AnnotationSourceID = "AnnotationSourceID";	
			public const string SubjectCategoryCode = "SubjectCategoryCode";	
			public const string SubjectCategoryName = "SubjectCategoryName";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
