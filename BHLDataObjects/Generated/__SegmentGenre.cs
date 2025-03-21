
// Generated 11/5/2024 11:57:22 AM
// Do not modify the contents of this code file.
// This abstract class __SegmentGenre is based upon dbo.SegmentGenre.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class SegmentGenre : __SegmentGenre
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
	public abstract class __SegmentGenre : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __SegmentGenre()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentGenreID"></param>
		/// <param name="genreName"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="genreDescription"></param>
		public __SegmentGenre(int segmentGenreID, 
			string genreName, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID, 
			string genreDescription) : this()
		{
			_SegmentGenreID = segmentGenreID;
			GenreName = genreName;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
			GenreDescription = genreDescription;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__SegmentGenre()
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
					case "SegmentGenreID" :
					{
						_SegmentGenreID = (int)column.Value;
						break;
					}
					case "GenreName" :
					{
						_GenreName = (string)column.Value;
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
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
					case "GenreDescription" :
					{
						_GenreDescription = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region SegmentGenreID
		
		private int _SegmentGenreID = default(int);
		
		/// <summary>
		/// Column: SegmentGenreID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentGenreID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int SegmentGenreID
		{
			get
			{
				return _SegmentGenreID;
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
		
		#endregion SegmentGenreID
		
		#region GenreName
		
		private string _GenreName = string.Empty;
		
		/// <summary>
		/// Column: GenreName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("GenreName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string GenreName
		{
			get
			{
				return _GenreName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_GenreName != value)
				{
					_GenreName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GenreName
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=3)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		
		#region CreationUserID
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
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
		
		#region LastModifiedUserID
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
		{
			get
			{
				return _LastModifiedUserID;
			}
			set
			{
				if (_LastModifiedUserID != value)
				{
					_LastModifiedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedUserID
		
		#region GenreDescription
		
		private string _GenreDescription = string.Empty;
		
		/// <summary>
		/// Column: GenreDescription;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("GenreDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=500)]
		public string GenreDescription
		{
			get
			{
				return _GenreDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_GenreDescription != value)
				{
					_GenreDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GenreDescription
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__SegmentGenre"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__SegmentGenre"/>, 
		/// returns an instance of <see cref="__SegmentGenre"/>; otherwise returns null.</returns>
		public static new __SegmentGenre FromArray(byte[] byteArray)
		{
			__SegmentGenre o = null;
			
			try
			{
				o = (__SegmentGenre) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__SegmentGenre"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__SegmentGenre"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __SegmentGenre)
			{
				__SegmentGenre o = (__SegmentGenre) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentGenreID == SegmentGenreID &&
					GetComparisonString(o.GenreName) == GetComparisonString(GenreName) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					GetComparisonString(o.GenreDescription) == GetComparisonString(GenreDescription) 
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
				throw new ArgumentException("Argument is not of type __SegmentGenre");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__SegmentGenre"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentGenre"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__SegmentGenre a, __SegmentGenre b)
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
		/// <param name="a">The first <see cref="__SegmentGenre"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentGenre"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__SegmentGenre a, __SegmentGenre b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__SegmentGenre"/> object to compare with the current <see cref="__SegmentGenre"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __SegmentGenre))
			{
				return false;
			}
			
			return this == (__SegmentGenre) obj;
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
		/// list.Sort(SortOrder.Ascending, __SegmentGenre.SortColumn.SegmentGenreID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentGenreID = "SegmentGenreID";	
			public const string GenreName = "GenreName";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string GenreDescription = "GenreDescription";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

