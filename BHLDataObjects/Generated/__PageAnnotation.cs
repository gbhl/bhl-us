
// Generated 5/11/2010 1:52:21 PM
// Do not modify the contents of this code file.
// This abstract class __PageAnnotation is based upon PageAnnotation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PageAnnotation : __PageAnnotation
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
	public abstract class __PageAnnotation : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageAnnotation()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <param name="pageColumn"></param>
		public __PageAnnotation(int annotatedPageID, 
			int annotationID, 
			string pageColumn) : this()
		{
			AnnotatedPageID = annotatedPageID;
			AnnotationID = annotationID;
			PageColumn = pageColumn;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PageAnnotation()
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
					case "AnnotatedPageID" :
					{
						_AnnotatedPageID = (int)column.Value;
						break;
					}
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "PageColumn" :
					{
						_PageColumn = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region AnnotatedPageID
		
		private int _AnnotatedPageID = default(int);
		
		/// <summary>
		/// Column: AnnotatedPageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotatedPageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotatedPageID
		{
			get
			{
				return _AnnotatedPageID;
			}
			set
			{
				if (_AnnotatedPageID != value)
				{
					_AnnotatedPageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedPageID
		
		#region AnnotationID
		
		private int _AnnotationID = default(int);
		
		/// <summary>
		/// Column: AnnotationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
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
		
		#region PageColumn
		
		private string _PageColumn = string.Empty;
		
		/// <summary>
		/// Column: PageColumn;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("PageColumn", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=20)]
		public string PageColumn
		{
			get
			{
				return _PageColumn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_PageColumn != value)
				{
					_PageColumn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageColumn
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PageAnnotation"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageAnnotation"/>, 
		/// returns an instance of <see cref="__PageAnnotation"/>; otherwise returns null.</returns>
		public static new __PageAnnotation FromArray(byte[] byteArray)
		{
			__PageAnnotation o = null;
			
			try
			{
				o = (__PageAnnotation) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageAnnotation"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageAnnotation"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageAnnotation)
			{
				__PageAnnotation o = (__PageAnnotation) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedPageID == AnnotatedPageID &&
					o.AnnotationID == AnnotationID &&
					GetComparisonString(o.PageColumn) == GetComparisonString(PageColumn) 
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
				throw new ArgumentException("Argument is not of type __PageAnnotation");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageAnnotation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageAnnotation"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageAnnotation a, __PageAnnotation b)
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
		/// <param name="a">The first <see cref="__PageAnnotation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageAnnotation"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageAnnotation a, __PageAnnotation b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageAnnotation"/> object to compare with the current <see cref="__PageAnnotation"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageAnnotation))
			{
				return false;
			}
			
			return this == (__PageAnnotation) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageAnnotation.SortColumn.AnnotatedPageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedPageID = "AnnotatedPageID";	
			public const string AnnotationID = "AnnotationID";	
			public const string PageColumn = "PageColumn";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
