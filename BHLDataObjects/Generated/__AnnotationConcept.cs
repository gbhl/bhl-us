
// Generated 1/7/2011 3:13:11 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationConcept is based upon AnnotationConcept.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationConcept : __AnnotationConcept
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
	public abstract class __AnnotationConcept : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationConcept()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="conceptText"></param>
		/// <param name="parentConceptCode"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationConcept(string annotationConceptCode, 
			int annotationSourceID, 
			string conceptText, 
			string parentConceptCode, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			AnnotationConceptCode = annotationConceptCode;
			AnnotationSourceID = annotationSourceID;
			ConceptText = conceptText;
			ParentConceptCode = parentConceptCode;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationConcept()
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
					case "AnnotationConceptCode" :
					{
						_AnnotationConceptCode = (string)column.Value;
						break;
					}
					case "AnnotationSourceID" :
					{
						_AnnotationSourceID = (int)column.Value;
						break;
					}
					case "ConceptText" :
					{
						_ConceptText = (string)column.Value;
						break;
					}
					case "ParentConceptCode" :
					{
						_ParentConceptCode = (string)column.Value;
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
		
		#region AnnotationConceptCode
		
		private string _AnnotationConceptCode = string.Empty;
		
		/// <summary>
		/// Column: AnnotationConceptCode;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("AnnotationConceptCode", DbTargetType=SqlDbType.NVarChar, Ordinal=1, CharacterMaxLength=20, IsInForeignKey=true, IsInPrimaryKey=true)]
		public string AnnotationConceptCode
		{
			get
			{
				return _AnnotationConceptCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_AnnotationConceptCode != value)
				{
					_AnnotationConceptCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationConceptCode
		
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
		
		#region ConceptText
		
		private string _ConceptText = string.Empty;
		
		/// <summary>
		/// Column: ConceptText;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("ConceptText", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string ConceptText
		{
			get
			{
				return _ConceptText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ConceptText != value)
				{
					_ConceptText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ConceptText
		
		#region ParentConceptCode
		
		private string _ParentConceptCode = null;
		
		/// <summary>
		/// Column: ParentConceptCode;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("ParentConceptCode", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=20, IsInForeignKey=true, IsNullable=true)]
		public string ParentConceptCode
		{
			get
			{
				return _ParentConceptCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_ParentConceptCode != value)
				{
					_ParentConceptCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ParentConceptCode
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationConcept"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationConcept"/>, 
		/// returns an instance of <see cref="__AnnotationConcept"/>; otherwise returns null.</returns>
		public static new __AnnotationConcept FromArray(byte[] byteArray)
		{
			__AnnotationConcept o = null;
			
			try
			{
				o = (__AnnotationConcept) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationConcept"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationConcept"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationConcept)
			{
				__AnnotationConcept o = (__AnnotationConcept) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.AnnotationConceptCode) == GetComparisonString(AnnotationConceptCode) &&
					o.AnnotationSourceID == AnnotationSourceID &&
					GetComparisonString(o.ConceptText) == GetComparisonString(ConceptText) &&
					GetComparisonString(o.ParentConceptCode) == GetComparisonString(ParentConceptCode) &&
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
				throw new ArgumentException("Argument is not of type __AnnotationConcept");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationConcept"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationConcept"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationConcept a, __AnnotationConcept b)
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
		/// <param name="a">The first <see cref="__AnnotationConcept"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationConcept"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationConcept a, __AnnotationConcept b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationConcept"/> object to compare with the current <see cref="__AnnotationConcept"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationConcept))
			{
				return false;
			}
			
			return this == (__AnnotationConcept) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationConcept.SortColumn.AnnotationConceptCode);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationConceptCode = "AnnotationConceptCode";	
			public const string AnnotationSourceID = "AnnotationSourceID";	
			public const string ConceptText = "ConceptText";	
			public const string ParentConceptCode = "ParentConceptCode";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
