
// Generated 1/5/2021 3:27:24 PM
// Do not modify the contents of this code file.
// This abstract class __TitleNote is based upon dbo.TitleNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleNote : __TitleNote
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
	public abstract class __TitleNote : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleNote()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleNoteID"></param>
		/// <param name="titleID"></param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteSequence"></param>
		/// <param name="creationDate"></param>
		/// <param name="creationUserID"></param>
		public __TitleNote(int titleNoteID, 
			int titleID, 
			int? noteTypeID, 
			string noteText, 
			short? noteSequence, 
			DateTime creationDate, 
			int? creationUserID) : this()
		{
			_TitleNoteID = titleNoteID;
			TitleID = titleID;
			NoteTypeID = noteTypeID;
			NoteText = noteText;
			NoteSequence = noteSequence;
			CreationDate = creationDate;
			CreationUserID = creationUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleNote()
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
					case "TitleNoteID" :
					{
						_TitleNoteID = (int)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "NoteTypeID" :
					{
						_NoteTypeID = (int?)column.Value;
						break;
					}
					case "NoteText" :
					{
						_NoteText = (string)column.Value;
						break;
					}
					case "NoteSequence" :
					{
						_NoteSequence = (short?)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region TitleNoteID
		
		private int _TitleNoteID = default(int);
		
		/// <summary>
		/// Column: TitleNoteID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleNoteID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleNoteID
		{
			get
			{
				return _TitleNoteID;
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
		
		#endregion TitleNoteID
		
		#region TitleID
		
		private int _TitleID = default(int);
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region NoteTypeID
		
		private int? _NoteTypeID = null;
		
		/// <summary>
		/// Column: NoteTypeID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("NoteTypeID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? NoteTypeID
		{
			get
			{
				return _NoteTypeID;
			}
			set
			{
				if (_NoteTypeID != value)
				{
					_NoteTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteTypeID
		
		#region NoteText
		
		private string _NoteText = string.Empty;
		
		/// <summary>
		/// Column: NoteText;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("NoteText", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
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
		
		#region NoteSequence
		
		private short? _NoteSequence = null;
		
		/// <summary>
		/// Column: NoteSequence;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("NoteSequence", DbTargetType=SqlDbType.SmallInt, Ordinal=5, NumericPrecision=5, IsNullable=true)]
		public short? NoteSequence
		{
			get
			{
				return _NoteSequence;
			}
			set
			{
				if (_NoteSequence != value)
				{
					_NoteSequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteSequence
		
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
		
		#region CreationUserID
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
		{
			get
			{
				return _CreationUserID;
			}
			set
			{
				if (_CreationUserID != value)
				{
					_CreationUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationUserID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleNote"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleNote"/>, 
		/// returns an instance of <see cref="__TitleNote"/>; otherwise returns null.</returns>
		public static new __TitleNote FromArray(byte[] byteArray)
		{
			__TitleNote o = null;
			
			try
			{
				o = (__TitleNote) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleNote"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleNote"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleNote)
			{
				__TitleNote o = (__TitleNote) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleNoteID == TitleNoteID &&
					o.TitleID == TitleID &&
					o.NoteTypeID == NoteTypeID &&
					GetComparisonString(o.NoteText) == GetComparisonString(NoteText) &&
					o.NoteSequence == NoteSequence &&
					o.CreationDate == CreationDate &&
					o.CreationUserID == CreationUserID 
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
				throw new ArgumentException("Argument is not of type __TitleNote");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleNote"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleNote a, __TitleNote b)
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
		/// <param name="a">The first <see cref="__TitleNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleNote"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleNote a, __TitleNote b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleNote"/> object to compare with the current <see cref="__TitleNote"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleNote))
			{
				return false;
			}
			
			return this == (__TitleNote) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleNote.SortColumn.TitleNoteID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleNoteID = "TitleNoteID";	
			public const string TitleID = "TitleID";	
			public const string NoteTypeID = "NoteTypeID";	
			public const string NoteText = "NoteText";	
			public const string NoteSequence = "NoteSequence";	
			public const string CreationDate = "CreationDate";	
			public const string CreationUserID = "CreationUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

