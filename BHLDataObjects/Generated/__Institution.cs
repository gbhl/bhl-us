
// Generated 10/15/2009 4:11:33 PM
// Do not modify the contents of this code file.
// This abstract class __Institution is based upon Institution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Institution : __Institution
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
	public abstract class __Institution : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Institution()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		public __Institution(string institutionCode, 
			string institutionName, 
			string note, 
			string institutionUrl, 
			bool bHLMemberLibrary) : this()
		{
			InstitutionCode = institutionCode;
			InstitutionName = institutionName;
			Note = note;
			InstitutionUrl = institutionUrl;
			BHLMemberLibrary = bHLMemberLibrary;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Institution()
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
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "InstitutionName" :
					{
						_InstitutionName = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "InstitutionUrl" :
					{
						_InstitutionUrl = (string)column.Value;
						break;
					}
					case "BHLMemberLibrary" :
					{
						_BHLMemberLibrary = (bool)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region InstitutionCode
		
		private string _InstitutionCode = string.Empty;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10);
		/// Description: Code for Institution providing assistance.
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=1, Description="Code for Institution providing assistance.", CharacterMaxLength=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region InstitutionName
		
		private string _InstitutionName = string.Empty;
		
		/// <summary>
		/// Column: InstitutionName;
		/// DBMS data type: nvarchar(255);
		/// Description: Name for the Institution.
		/// </summary>
		[ColumnDefinition("InstitutionName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, Description="Name for the Institution.", CharacterMaxLength=255)]
		public string InstitutionName
		{
			get
			{
				return _InstitutionName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_InstitutionName != value)
				{
					_InstitutionName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionName
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// Description: Notes about this Institution.
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=3, Description="Notes about this Institution.", CharacterMaxLength=255, IsNullable=true)]
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
		
		#region InstitutionUrl
		
		private string _InstitutionUrl = null;
		
		/// <summary>
		/// Column: InstitutionUrl;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=255, IsNullable=true)]
		public string InstitutionUrl
		{
			get
			{
				return _InstitutionUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_InstitutionUrl != value)
				{
					_InstitutionUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionUrl
		
		#region BHLMemberLibrary
		
		private bool _BHLMemberLibrary = false;
		
		/// <summary>
		/// Column: BHLMemberLibrary;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("BHLMemberLibrary", DbTargetType=SqlDbType.Bit, Ordinal=5)]
		public bool BHLMemberLibrary
		{
			get
			{
				return _BHLMemberLibrary;
			}
			set
			{
				if (_BHLMemberLibrary != value)
				{
					_BHLMemberLibrary = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLMemberLibrary
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Institution"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Institution"/>, 
		/// returns an instance of <see cref="__Institution"/>; otherwise returns null.</returns>
		public static new __Institution FromArray(byte[] byteArray)
		{
			__Institution o = null;
			
			try
			{
				o = (__Institution) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Institution"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Institution"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Institution)
			{
				__Institution o = (__Institution) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.InstitutionName) == GetComparisonString(InstitutionName) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					GetComparisonString(o.InstitutionUrl) == GetComparisonString(InstitutionUrl) &&
					o.BHLMemberLibrary == BHLMemberLibrary 
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
				throw new ArgumentException("Argument is not of type __Institution");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Institution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Institution"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Institution a, __Institution b)
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
		/// <param name="a">The first <see cref="__Institution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Institution"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Institution a, __Institution b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Institution"/> object to compare with the current <see cref="__Institution"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Institution))
			{
				return false;
			}
			
			return this == (__Institution) obj;
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
		/// list.Sort(SortOrder.Ascending, __Institution.SortColumn.InstitutionCode);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string InstitutionCode = "InstitutionCode";	
			public const string InstitutionName = "InstitutionName";	
			public const string Note = "Note";	
			public const string InstitutionUrl = "InstitutionUrl";	
			public const string BHLMemberLibrary = "BHLMemberLibrary";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
