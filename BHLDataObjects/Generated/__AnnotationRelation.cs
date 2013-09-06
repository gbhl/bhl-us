
// Generated 6/15/2010 1:29:40 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationRelation is based upon AnnotationRelation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationRelation : __AnnotationRelation
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
	public abstract class __AnnotationRelation : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationRelation()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <param name="note"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationRelation(int annotationID, 
			string relatedExternalIdentifier, 
			string note, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			AnnotationID = annotationID;
			RelatedExternalIdentifier = relatedExternalIdentifier;
			Note = note;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationRelation()
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
					case "RelatedExternalIdentifier" :
					{
						_RelatedExternalIdentifier = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
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
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
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
		
		#region RelatedExternalIdentifier
		
		private string _RelatedExternalIdentifier = string.Empty;
		
		/// <summary>
		/// Column: RelatedExternalIdentifier;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("RelatedExternalIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50, IsInPrimaryKey=true)]
		public string RelatedExternalIdentifier
		{
			get
			{
				return _RelatedExternalIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_RelatedExternalIdentifier != value)
				{
					_RelatedExternalIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RelatedExternalIdentifier
		
		#region Note
		
		private string _Note = string.Empty;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1073741823)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationRelation"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationRelation"/>, 
		/// returns an instance of <see cref="__AnnotationRelation"/>; otherwise returns null.</returns>
		public static new __AnnotationRelation FromArray(byte[] byteArray)
		{
			__AnnotationRelation o = null;
			
			try
			{
				o = (__AnnotationRelation) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationRelation"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationRelation"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationRelation)
			{
				__AnnotationRelation o = (__AnnotationRelation) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationID == AnnotationID &&
					GetComparisonString(o.RelatedExternalIdentifier) == GetComparisonString(RelatedExternalIdentifier) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
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
				throw new ArgumentException("Argument is not of type __AnnotationRelation");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationRelation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationRelation"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationRelation a, __AnnotationRelation b)
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
		/// <param name="a">The first <see cref="__AnnotationRelation"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationRelation"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationRelation a, __AnnotationRelation b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationRelation"/> object to compare with the current <see cref="__AnnotationRelation"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationRelation))
			{
				return false;
			}
			
			return this == (__AnnotationRelation) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationRelation.SortColumn.AnnotationID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationID = "AnnotationID";	
			public const string RelatedExternalIdentifier = "RelatedExternalIdentifier";	
			public const string Note = "Note";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
