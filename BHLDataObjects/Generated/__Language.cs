
// Generated 1/5/2021 3:25:59 PM
// Do not modify the contents of this code file.
// This abstract class __Language is based upon dbo.Language.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Language : __Language
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
	public abstract class __Language : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Language()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="languageCode"></param>
		/// <param name="languageName"></param>
		/// <param name="note"></param>
		public __Language(string languageCode, 
			string languageName, 
			string note) : this()
		{
			LanguageCode = languageCode;
			LanguageName = languageName;
			Note = note;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Language()
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
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "LanguageName" :
					{
						_LanguageName = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region LanguageCode
		
		private string _LanguageCode = string.Empty;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=1, CharacterMaxLength=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public string LanguageCode
		{
			get
			{
				return _LanguageCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_LanguageCode != value)
				{
					_LanguageCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LanguageCode
		
		#region LanguageName
		
		private string _LanguageName = string.Empty;
		
		/// <summary>
		/// Column: LanguageName;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("LanguageName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=20)]
		public string LanguageName
		{
			get
			{
				return _LanguageName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_LanguageName != value)
				{
					_LanguageName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LanguageName
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=255, IsNullable=true)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Language"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Language"/>, 
		/// returns an instance of <see cref="__Language"/>; otherwise returns null.</returns>
		public static new __Language FromArray(byte[] byteArray)
		{
			__Language o = null;
			
			try
			{
				o = (__Language) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Language"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Language"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Language)
			{
				__Language o = (__Language) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.LanguageName) == GetComparisonString(LanguageName) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) 
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
				throw new ArgumentException("Argument is not of type __Language");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Language"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Language"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Language a, __Language b)
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
		/// <param name="a">The first <see cref="__Language"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Language"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Language a, __Language b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Language"/> object to compare with the current <see cref="__Language"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Language))
			{
				return false;
			}
			
			return this == (__Language) obj;
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
		/// list.Sort(SortOrder.Ascending, __Language.SortColumn.LanguageCode);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string LanguageCode = "LanguageCode";	
			public const string LanguageName = "LanguageName";	
			public const string Note = "Note";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

