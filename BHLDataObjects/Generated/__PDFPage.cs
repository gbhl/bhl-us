
// Generated 1/5/2021 3:26:47 PM
// Do not modify the contents of this code file.
// This abstract class __PDFPage is based upon dbo.PDFPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PDFPage : __PDFPage
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
	public abstract class __PDFPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PDFPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pdfPageID"></param>
		/// <param name="pdfID"></param>
		/// <param name="pageID"></param>
		public __PDFPage(int pdfPageID, 
			int pdfID, 
			int pageID) : this()
		{
			_PdfPageID = pdfPageID;
			PdfID = pdfID;
			PageID = pageID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PDFPage()
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
					case "PdfPageID" :
					{
						_PdfPageID = (int)column.Value;
						break;
					}
					case "PdfID" :
					{
						_PdfID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PdfPageID
		
		private int _PdfPageID = default(int);
		
		/// <summary>
		/// Column: PdfPageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PdfPageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PdfPageID
		{
			get
			{
				return _PdfPageID;
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
		
		#endregion PdfPageID
		
		#region PdfID
		
		private int _PdfID = default(int);
		
		/// <summary>
		/// Column: PdfID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PdfID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int PdfID
		{
			get
			{
				return _PdfID;
			}
			set
			{
				if (_PdfID != value)
				{
					_PdfID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PdfID
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int PageID
		{
			get
			{
				return _PageID;
			}
			set
			{
				if (_PageID != value)
				{
					_PageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PDFPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PDFPage"/>, 
		/// returns an instance of <see cref="__PDFPage"/>; otherwise returns null.</returns>
		public static new __PDFPage FromArray(byte[] byteArray)
		{
			__PDFPage o = null;
			
			try
			{
				o = (__PDFPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PDFPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PDFPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PDFPage)
			{
				__PDFPage o = (__PDFPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PdfPageID == PdfPageID &&
					o.PdfID == PdfID &&
					o.PageID == PageID 
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
				throw new ArgumentException("Argument is not of type __PDFPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PDFPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDFPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PDFPage a, __PDFPage b)
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
		/// <param name="a">The first <see cref="__PDFPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDFPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PDFPage a, __PDFPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PDFPage"/> object to compare with the current <see cref="__PDFPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PDFPage))
			{
				return false;
			}
			
			return this == (__PDFPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __PDFPage.SortColumn.PdfPageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PdfPageID = "PdfPageID";	
			public const string PdfID = "PdfID";	
			public const string PageID = "PageID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

