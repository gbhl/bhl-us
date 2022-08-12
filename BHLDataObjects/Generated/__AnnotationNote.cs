
// Generated 1/5/2021 3:36:36 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationNote is based upon annotation.AnnotationNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationNote : __AnnotationNote
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
	public abstract class __AnnotationNote : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationNote()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationNoteID"></param>
		/// <param name="annotationID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteTextClean"></param>
		/// <param name="noteTextDisplay"></param>
		/// <param name="isAlternate"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationNote(int annotationNoteID, 
			int annotationID, 
			string noteText, 
			string noteTextClean, 
			string noteTextDisplay, 
			byte isAlternate, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationNoteID = annotationNoteID;
			AnnotationID = annotationID;
			NoteText = noteText;
			NoteTextClean = noteTextClean;
			NoteTextDisplay = noteTextDisplay;
			IsAlternate = isAlternate;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationNote()
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
					case "AnnotationNoteID" :
					{
						_AnnotationNoteID = (int)column.Value;
						break;
					}
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "NoteText" :
					{
						_NoteText = (string)column.Value;
						break;
					}
					case "NoteTextClean" :
					{
						_NoteTextClean = (string)column.Value;
						break;
					}
					case "NoteTextDisplay" :
					{
						_NoteTextDisplay = (string)column.Value;
						break;
					}
					case "IsAlternate" :
					{
						_IsAlternate = (byte)column.Value;
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
		
		#region AnnotationNoteID
		
		private int _AnnotationNoteID = default(int);
		
		/// <summary>
		/// Column: AnnotationNoteID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationNoteID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AnnotationNoteID
		{
			get
			{
				return _AnnotationNoteID;
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
		
		#endregion AnnotationNoteID
		
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
		
		#region NoteText
		
		private string _NoteText = string.Empty;
		
		/// <summary>
		/// Column: NoteText;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("NoteText", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1073741823)]
		public string NoteText
		{
			get
			{
				return _NoteText;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_NoteText != value)
				{
					_NoteText = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteText
		
		#region NoteTextClean
		
		private string _NoteTextClean = string.Empty;
		
		/// <summary>
		/// Column: NoteTextClean;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("NoteTextClean", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
		public string NoteTextClean
		{
			get
			{
				return _NoteTextClean;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_NoteTextClean != value)
				{
					_NoteTextClean = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteTextClean
		
		#region NoteTextDisplay
		
		private string _NoteTextDisplay = string.Empty;
		
		/// <summary>
		/// Column: NoteTextDisplay;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("NoteTextDisplay", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string NoteTextDisplay
		{
			get
			{
				return _NoteTextDisplay;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_NoteTextDisplay != value)
				{
					_NoteTextDisplay = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteTextDisplay
		
		#region IsAlternate
		
		private byte _IsAlternate = default(byte);
		
		/// <summary>
		/// Column: IsAlternate;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("IsAlternate", DbTargetType=SqlDbType.TinyInt, Ordinal=6, NumericPrecision=3)]
		public byte IsAlternate
		{
			get
			{
				return _IsAlternate;
			}
			set
			{
				if (_IsAlternate != value)
				{
					_IsAlternate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsAlternate
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationNote"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationNote"/>, 
		/// returns an instance of <see cref="__AnnotationNote"/>; otherwise returns null.</returns>
		public static new __AnnotationNote FromArray(byte[] byteArray)
		{
			__AnnotationNote o = null;
			
			try
			{
				o = (__AnnotationNote) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationNote"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationNote"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationNote)
			{
				__AnnotationNote o = (__AnnotationNote) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationNoteID == AnnotationNoteID &&
					o.AnnotationID == AnnotationID &&
					GetComparisonString(o.NoteText) == GetComparisonString(NoteText) &&
					GetComparisonString(o.NoteTextClean) == GetComparisonString(NoteTextClean) &&
					GetComparisonString(o.NoteTextDisplay) == GetComparisonString(NoteTextDisplay) &&
					o.IsAlternate == IsAlternate &&
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
				throw new ArgumentException("Argument is not of type __AnnotationNote");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationNote"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationNote a, __AnnotationNote b)
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
		/// <param name="a">The first <see cref="__AnnotationNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationNote"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationNote a, __AnnotationNote b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationNote"/> object to compare with the current <see cref="__AnnotationNote"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationNote))
			{
				return false;
			}
			
			return this == (__AnnotationNote) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationNote.SortColumn.AnnotationNoteID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationNoteID = "AnnotationNoteID";	
			public const string AnnotationID = "AnnotationID";	
			public const string NoteText = "NoteText";	
			public const string NoteTextClean = "NoteTextClean";	
			public const string NoteTextDisplay = "NoteTextDisplay";	
			public const string IsAlternate = "IsAlternate";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

