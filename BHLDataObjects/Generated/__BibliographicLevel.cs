
// Generated 1/2/2020 3:40:22 PM
// Do not modify the contents of this code file.
// This abstract class __BibliographicLevel is based upon dbo.BibliographicLevel.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class BibliographicLevel : __BibliographicLevel
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
	public abstract class __BibliographicLevel : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __BibliographicLevel()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="bibliographicLevelName"></param>
		/// <param name="bibliographicLevelLabel"></param>
		/// <param name="mARCCode"></param>
		public __BibliographicLevel(int bibliographicLevelID, 
			string bibliographicLevelName, 
			string bibliographicLevelLabel, 
			string mARCCode) : this()
		{
			_BibliographicLevelID = bibliographicLevelID;
			BibliographicLevelName = bibliographicLevelName;
			BibliographicLevelLabel = bibliographicLevelLabel;
			MARCCode = mARCCode;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__BibliographicLevel()
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
					case "BibliographicLevelID" :
					{
						_BibliographicLevelID = (int)column.Value;
						break;
					}
					case "BibliographicLevelName" :
					{
						_BibliographicLevelName = (string)column.Value;
						break;
					}
					case "BibliographicLevelLabel" :
					{
						_BibliographicLevelLabel = (string)column.Value;
						break;
					}
					case "MARCCode" :
					{
						_MARCCode = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region BibliographicLevelID
		
		private int _BibliographicLevelID = default(int);
		
		/// <summary>
		/// Column: BibliographicLevelID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("BibliographicLevelID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int BibliographicLevelID
		{
			get
			{
				return _BibliographicLevelID;
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
		
		#endregion BibliographicLevelID
		
		#region BibliographicLevelName
		
		private string _BibliographicLevelName = string.Empty;
		
		/// <summary>
		/// Column: BibliographicLevelName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("BibliographicLevelName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string BibliographicLevelName
		{
			get
			{
				return _BibliographicLevelName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_BibliographicLevelName != value)
				{
					_BibliographicLevelName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BibliographicLevelName
		
		#region BibliographicLevelLabel
		
		private string _BibliographicLevelLabel = string.Empty;
		
		/// <summary>
		/// Column: BibliographicLevelLabel;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("BibliographicLevelLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string BibliographicLevelLabel
		{
			get
			{
				return _BibliographicLevelLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_BibliographicLevelLabel != value)
				{
					_BibliographicLevelLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BibliographicLevelLabel
		
		#region MARCCode
		
		private string _MARCCode = string.Empty;
		
		/// <summary>
		/// Column: MARCCode;
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("MARCCode", DbTargetType=SqlDbType.NChar, Ordinal=4, CharacterMaxLength=1)]
		public string MARCCode
		{
			get
			{
				return _MARCCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_MARCCode != value)
				{
					_MARCCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCCode
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__BibliographicLevel"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__BibliographicLevel"/>, 
		/// returns an instance of <see cref="__BibliographicLevel"/>; otherwise returns null.</returns>
		public static new __BibliographicLevel FromArray(byte[] byteArray)
		{
			__BibliographicLevel o = null;
			
			try
			{
				o = (__BibliographicLevel) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__BibliographicLevel"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__BibliographicLevel"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __BibliographicLevel)
			{
				__BibliographicLevel o = (__BibliographicLevel) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.BibliographicLevelID == BibliographicLevelID &&
					GetComparisonString(o.BibliographicLevelName) == GetComparisonString(BibliographicLevelName) &&
					GetComparisonString(o.BibliographicLevelLabel) == GetComparisonString(BibliographicLevelLabel) &&
					GetComparisonString(o.MARCCode) == GetComparisonString(MARCCode) 
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
				throw new ArgumentException("Argument is not of type __BibliographicLevel");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__BibliographicLevel"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BibliographicLevel"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__BibliographicLevel a, __BibliographicLevel b)
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
		/// <param name="a">The first <see cref="__BibliographicLevel"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BibliographicLevel"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__BibliographicLevel a, __BibliographicLevel b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__BibliographicLevel"/> object to compare with the current <see cref="__BibliographicLevel"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __BibliographicLevel))
			{
				return false;
			}
			
			return this == (__BibliographicLevel) obj;
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
		/// list.Sort(SortOrder.Ascending, __BibliographicLevel.SortColumn.BibliographicLevelID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string BibliographicLevelID = "BibliographicLevelID";	
			public const string BibliographicLevelName = "BibliographicLevelName";	
			public const string BibliographicLevelLabel = "BibliographicLevelLabel";	
			public const string MARCCode = "MARCCode";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

