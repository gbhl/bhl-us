
// Generated 1/5/2021 3:26:49 PM
// Do not modify the contents of this code file.
// This abstract class __PDFStatus is based upon dbo.PDFStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PDFStatus : __PDFStatus
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
	public abstract class __PDFStatus : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PDFStatus()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pdfStatusID"></param>
		/// <param name="pdfStatusName"></param>
		public __PDFStatus(int pdfStatusID, 
			string pdfStatusName) : this()
		{
			PdfStatusID = pdfStatusID;
			PdfStatusName = pdfStatusName;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PDFStatus()
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
					case "PdfStatusID" :
					{
						_PdfStatusID = (int)column.Value;
						break;
					}
					case "PdfStatusName" :
					{
						_PdfStatusName = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PdfStatusID
		
		private int _PdfStatusID = default(int);
		
		/// <summary>
		/// Column: PdfStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PdfStatusID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int PdfStatusID
		{
			get
			{
				return _PdfStatusID;
			}
			set
			{
				if (_PdfStatusID != value)
				{
					_PdfStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PdfStatusID
		
		#region PdfStatusName
		
		private string _PdfStatusName = string.Empty;
		
		/// <summary>
		/// Column: PdfStatusName;
		/// DBMS data type: nchar(10);
		/// </summary>
		[ColumnDefinition("PdfStatusName", DbTargetType=SqlDbType.NChar, Ordinal=2, CharacterMaxLength=10)]
		public string PdfStatusName
		{
			get
			{
				return _PdfStatusName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_PdfStatusName != value)
				{
					_PdfStatusName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PdfStatusName
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PDFStatus"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PDFStatus"/>, 
		/// returns an instance of <see cref="__PDFStatus"/>; otherwise returns null.</returns>
		public static new __PDFStatus FromArray(byte[] byteArray)
		{
			__PDFStatus o = null;
			
			try
			{
				o = (__PDFStatus) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PDFStatus"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PDFStatus"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PDFStatus)
			{
				__PDFStatus o = (__PDFStatus) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PdfStatusID == PdfStatusID &&
					GetComparisonString(o.PdfStatusName) == GetComparisonString(PdfStatusName) 
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
				throw new ArgumentException("Argument is not of type __PDFStatus");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PDFStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDFStatus"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PDFStatus a, __PDFStatus b)
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
		/// <param name="a">The first <see cref="__PDFStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PDFStatus"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PDFStatus a, __PDFStatus b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PDFStatus"/> object to compare with the current <see cref="__PDFStatus"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PDFStatus))
			{
				return false;
			}
			
			return this == (__PDFStatus) obj;
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
		/// list.Sort(SortOrder.Ascending, __PDFStatus.SortColumn.PdfStatusID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PdfStatusID = "PdfStatusID";	
			public const string PdfStatusName = "PdfStatusName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

