
// Generated 1/5/2021 3:46:42 PM
// Do not modify the contents of this code file.
// This abstract class __SegmentCOinSView is based upon dbo.SegmentCOinSView.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class SegmentCOinSView : __SegmentCOinSView
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
	public abstract class __SegmentCOinSView : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __SegmentCOinSView()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentID"></param>
		/// <param name="rft_atitle"></param>
		/// <param name="rft_jtitle"></param>
		/// <param name="rft_date"></param>
		/// <param name="rft_volume"></param>
		/// <param name="rft_issue"></param>
		/// <param name="rft_spage"></param>
		/// <param name="rft_epage"></param>
		/// <param name="rft_pages"></param>
		/// <param name="rft_language"></param>
		/// <param name="rft_issn"></param>
		/// <param name="rft_aulast"></param>
		/// <param name="rft_aufirst"></param>
		/// <param name="rft_au"></param>
		/// <param name="rft_subject"></param>
		/// <param name="rft_isbn"></param>
		/// <param name="rft_coden"></param>
		/// <param name="rft_genre"></param>
		/// <param name="rft_contributor"></param>
		public __SegmentCOinSView(int segmentID, 
			string rft_atitle, 
			string rft_jtitle, 
			string rft_date, 
			string rft_volume, 
			string rft_issue, 
			string rft_spage, 
			string rft_epage, 
			string rft_pages, 
			string rft_language, 
			string rft_issn, 
			string rft_aulast, 
			string rft_aufirst, 
			string rft_au, 
			string rft_subject, 
			string rft_isbn, 
			string rft_coden, 
			string rft_genre, 
			string rft_contributor) : this()
		{
			SegmentID = segmentID;
			Rft_atitle = rft_atitle;
			Rft_jtitle = rft_jtitle;
			Rft_date = rft_date;
			Rft_volume = rft_volume;
			Rft_issue = rft_issue;
			Rft_spage = rft_spage;
			Rft_epage = rft_epage;
			Rft_pages = rft_pages;
			Rft_language = rft_language;
			Rft_issn = rft_issn;
			Rft_aulast = rft_aulast;
			Rft_aufirst = rft_aufirst;
			Rft_au = rft_au;
			Rft_subject = rft_subject;
			Rft_isbn = rft_isbn;
			Rft_coden = rft_coden;
			Rft_genre = rft_genre;
			Rft_contributor = rft_contributor;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__SegmentCOinSView()
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
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
						break;
					}
					case "rft_atitle" :
					{
						_Rft_atitle = (string)column.Value;
						break;
					}
					case "rft_jtitle" :
					{
						_Rft_jtitle = (string)column.Value;
						break;
					}
					case "rft_date" :
					{
						_Rft_date = (string)column.Value;
						break;
					}
					case "rft_volume" :
					{
						_Rft_volume = (string)column.Value;
						break;
					}
					case "rft_issue" :
					{
						_Rft_issue = (string)column.Value;
						break;
					}
					case "rft_spage" :
					{
						_Rft_spage = (string)column.Value;
						break;
					}
					case "rft_epage" :
					{
						_Rft_epage = (string)column.Value;
						break;
					}
					case "rft_pages" :
					{
						_Rft_pages = (string)column.Value;
						break;
					}
					case "rft_language" :
					{
						_Rft_language = (string)column.Value;
						break;
					}
					case "rft_issn" :
					{
						_Rft_issn = (string)column.Value;
						break;
					}
					case "rft_aulast" :
					{
						_Rft_aulast = (string)column.Value;
						break;
					}
					case "rft_aufirst" :
					{
						_Rft_aufirst = (string)column.Value;
						break;
					}
					case "rft_au" :
					{
						_Rft_au = (string)column.Value;
						break;
					}
					case "rft_subject" :
					{
						_Rft_subject = (string)column.Value;
						break;
					}
					case "rft_isbn" :
					{
						_Rft_isbn = (string)column.Value;
						break;
					}
					case "rft_coden" :
					{
						_Rft_coden = (string)column.Value;
						break;
					}
					case "rft_genre" :
					{
						_Rft_genre = (string)column.Value;
						break;
					}
					case "rft_contributor" :
					{
						_Rft_contributor = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
			}
			set
			{
				if (_SegmentID != value)
				{
					_SegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentID
		
		#region Rft_atitle
		
		private string _Rft_atitle = string.Empty;
		
		/// <summary>
		/// Column: rft_atitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("rft_atitle", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=2000)]
		public string Rft_atitle
		{
			get
			{
				return _Rft_atitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Rft_atitle != value)
				{
					_Rft_atitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_atitle
		
		#region Rft_jtitle
		
		private string _Rft_jtitle = null;
		
		/// <summary>
		/// Column: rft_jtitle;
		/// DBMS data type: nvarchar(2000); Nullable;
		/// </summary>
		[ColumnDefinition("rft_jtitle", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=2000, IsNullable=true)]
		public string Rft_jtitle
		{
			get
			{
				return _Rft_jtitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Rft_jtitle != value)
				{
					_Rft_jtitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_jtitle
		
		#region Rft_date
		
		private string _Rft_date = null;
		
		/// <summary>
		/// Column: rft_date;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("rft_date", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=20, IsNullable=true)]
		public string Rft_date
		{
			get
			{
				return _Rft_date;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Rft_date != value)
				{
					_Rft_date = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_date
		
		#region Rft_volume
		
		private string _Rft_volume = string.Empty;
		
		/// <summary>
		/// Column: rft_volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("rft_volume", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string Rft_volume
		{
			get
			{
				return _Rft_volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Rft_volume != value)
				{
					_Rft_volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_volume
		
		#region Rft_issue
		
		private string _Rft_issue = string.Empty;
		
		/// <summary>
		/// Column: rft_issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("rft_issue", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string Rft_issue
		{
			get
			{
				return _Rft_issue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Rft_issue != value)
				{
					_Rft_issue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_issue
		
		#region Rft_spage
		
		private string _Rft_spage = string.Empty;
		
		/// <summary>
		/// Column: rft_spage;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("rft_spage", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=20)]
		public string Rft_spage
		{
			get
			{
				return _Rft_spage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Rft_spage != value)
				{
					_Rft_spage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_spage
		
		#region Rft_epage
		
		private string _Rft_epage = string.Empty;
		
		/// <summary>
		/// Column: rft_epage;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("rft_epage", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=20)]
		public string Rft_epage
		{
			get
			{
				return _Rft_epage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Rft_epage != value)
				{
					_Rft_epage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_epage
		
		#region Rft_pages
		
		private string _Rft_pages = string.Empty;
		
		/// <summary>
		/// Column: rft_pages;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("rft_pages", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=50)]
		public string Rft_pages
		{
			get
			{
				return _Rft_pages;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Rft_pages != value)
				{
					_Rft_pages = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_pages
		
		#region Rft_language
		
		private string _Rft_language = string.Empty;
		
		/// <summary>
		/// Column: rft_language;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("rft_language", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=10)]
		public string Rft_language
		{
			get
			{
				return _Rft_language;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_Rft_language != value)
				{
					_Rft_language = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_language
		
		#region Rft_issn
		
		private string _Rft_issn = null;
		
		/// <summary>
		/// Column: rft_issn;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_issn", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_issn
		{
			get
			{
				return _Rft_issn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_issn != value)
				{
					_Rft_issn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_issn
		
		#region Rft_aulast
		
		private string _Rft_aulast = null;
		
		/// <summary>
		/// Column: rft_aulast;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("rft_aulast", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=255, IsNullable=true)]
		public string Rft_aulast
		{
			get
			{
				return _Rft_aulast;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Rft_aulast != value)
				{
					_Rft_aulast = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_aulast
		
		#region Rft_aufirst
		
		private string _Rft_aufirst = null;
		
		/// <summary>
		/// Column: rft_aufirst;
		/// DBMS data type: nvarchar(4000); Nullable;
		/// </summary>
		[ColumnDefinition("rft_aufirst", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=4000, IsNullable=true)]
		public string Rft_aufirst
		{
			get
			{
				return _Rft_aufirst;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_Rft_aufirst != value)
				{
					_Rft_aufirst = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_aufirst
		
		#region Rft_au
		
		private string _Rft_au = null;
		
		/// <summary>
		/// Column: rft_au;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("rft_au", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Rft_au
		{
			get
			{
				return _Rft_au;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rft_au != value)
				{
					_Rft_au = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_au
		
		#region Rft_subject
		
		private string _Rft_subject = null;
		
		/// <summary>
		/// Column: rft_subject;
		/// DBMS data type: nvarchar(1024); Nullable;
		/// </summary>
		[ColumnDefinition("rft_subject", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=1024, IsNullable=true)]
		public string Rft_subject
		{
			get
			{
				return _Rft_subject;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1024);
				if (_Rft_subject != value)
				{
					_Rft_subject = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_subject
		
		#region Rft_isbn
		
		private string _Rft_isbn = null;
		
		/// <summary>
		/// Column: rft_isbn;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_isbn", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_isbn
		{
			get
			{
				return _Rft_isbn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_isbn != value)
				{
					_Rft_isbn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_isbn
		
		#region Rft_coden
		
		private string _Rft_coden = null;
		
		/// <summary>
		/// Column: rft_coden;
		/// DBMS data type: nvarchar(125); Nullable;
		/// </summary>
		[ColumnDefinition("rft_coden", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=125, IsNullable=true)]
		public string Rft_coden
		{
			get
			{
				return _Rft_coden;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Rft_coden != value)
				{
					_Rft_coden = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_coden
		
		#region Rft_genre
		
		private string _Rft_genre = null;
		
		/// <summary>
		/// Column: rft_genre;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("rft_genre", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=50, IsNullable=true)]
		public string Rft_genre
		{
			get
			{
				return _Rft_genre;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Rft_genre != value)
				{
					_Rft_genre = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_genre
		
		#region Rft_contributor
		
		private string _Rft_contributor = string.Empty;
		
		/// <summary>
		/// Column: rft_contributor;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("rft_contributor", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=1073741823)]
		public string Rft_contributor
		{
			get
			{
				return _Rft_contributor;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rft_contributor != value)
				{
					_Rft_contributor = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rft_contributor
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__SegmentCOinSView"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__SegmentCOinSView"/>, 
		/// returns an instance of <see cref="__SegmentCOinSView"/>; otherwise returns null.</returns>
		public static new __SegmentCOinSView FromArray(byte[] byteArray)
		{
			__SegmentCOinSView o = null;
			
			try
			{
				o = (__SegmentCOinSView) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__SegmentCOinSView"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__SegmentCOinSView"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __SegmentCOinSView)
			{
				__SegmentCOinSView o = (__SegmentCOinSView) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentID == SegmentID &&
					GetComparisonString(o.Rft_atitle) == GetComparisonString(Rft_atitle) &&
					GetComparisonString(o.Rft_jtitle) == GetComparisonString(Rft_jtitle) &&
					GetComparisonString(o.Rft_date) == GetComparisonString(Rft_date) &&
					GetComparisonString(o.Rft_volume) == GetComparisonString(Rft_volume) &&
					GetComparisonString(o.Rft_issue) == GetComparisonString(Rft_issue) &&
					GetComparisonString(o.Rft_spage) == GetComparisonString(Rft_spage) &&
					GetComparisonString(o.Rft_epage) == GetComparisonString(Rft_epage) &&
					GetComparisonString(o.Rft_pages) == GetComparisonString(Rft_pages) &&
					GetComparisonString(o.Rft_language) == GetComparisonString(Rft_language) &&
					GetComparisonString(o.Rft_issn) == GetComparisonString(Rft_issn) &&
					GetComparisonString(o.Rft_aulast) == GetComparisonString(Rft_aulast) &&
					GetComparisonString(o.Rft_aufirst) == GetComparisonString(Rft_aufirst) &&
					GetComparisonString(o.Rft_au) == GetComparisonString(Rft_au) &&
					GetComparisonString(o.Rft_subject) == GetComparisonString(Rft_subject) &&
					GetComparisonString(o.Rft_isbn) == GetComparisonString(Rft_isbn) &&
					GetComparisonString(o.Rft_coden) == GetComparisonString(Rft_coden) &&
					GetComparisonString(o.Rft_genre) == GetComparisonString(Rft_genre) &&
					GetComparisonString(o.Rft_contributor) == GetComparisonString(Rft_contributor) 
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
				throw new ArgumentException("Argument is not of type __SegmentCOinSView");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__SegmentCOinSView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentCOinSView"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__SegmentCOinSView a, __SegmentCOinSView b)
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
		/// <param name="a">The first <see cref="__SegmentCOinSView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentCOinSView"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__SegmentCOinSView a, __SegmentCOinSView b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__SegmentCOinSView"/> object to compare with the current <see cref="__SegmentCOinSView"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __SegmentCOinSView))
			{
				return false;
			}
			
			return this == (__SegmentCOinSView) obj;
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
		/// list.Sort(SortOrder.Ascending, __SegmentCOinSView.SortColumn.SegmentID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentID = "SegmentID";	
			public const string Rft_atitle = "Rft_atitle";	
			public const string Rft_jtitle = "Rft_jtitle";	
			public const string Rft_date = "Rft_date";	
			public const string Rft_volume = "Rft_volume";	
			public const string Rft_issue = "Rft_issue";	
			public const string Rft_spage = "Rft_spage";	
			public const string Rft_epage = "Rft_epage";	
			public const string Rft_pages = "Rft_pages";	
			public const string Rft_language = "Rft_language";	
			public const string Rft_issn = "Rft_issn";	
			public const string Rft_aulast = "Rft_aulast";	
			public const string Rft_aufirst = "Rft_aufirst";	
			public const string Rft_au = "Rft_au";	
			public const string Rft_subject = "Rft_subject";	
			public const string Rft_isbn = "Rft_isbn";	
			public const string Rft_coden = "Rft_coden";	
			public const string Rft_genre = "Rft_genre";	
			public const string Rft_contributor = "Rft_contributor";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

