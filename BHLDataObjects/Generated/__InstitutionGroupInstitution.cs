
// Generated 1/5/2021 3:25:24 PM
// Do not modify the contents of this code file.
// This abstract class __InstitutionGroupInstitution is based upon dbo.InstitutionGroupInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class InstitutionGroupInstitution : __InstitutionGroupInstitution
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
	public abstract class __InstitutionGroupInstitution : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __InstitutionGroupInstitution()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationDate"></param>
		/// <param name="creationUserID"></param>
		public __InstitutionGroupInstitution(int institutionGroupInstitutionID, 
			int institutionGroupID, 
			string institutionCode, 
			DateTime? creationDate, 
			int? creationUserID) : this()
		{
			_InstitutionGroupInstitutionID = institutionGroupInstitutionID;
			InstitutionGroupID = institutionGroupID;
			InstitutionCode = institutionCode;
			CreationDate = creationDate;
			CreationUserID = creationUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__InstitutionGroupInstitution()
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
					case "InstitutionGroupInstitutionID" :
					{
						_InstitutionGroupInstitutionID = (int)column.Value;
						break;
					}
					case "InstitutionGroupID" :
					{
						_InstitutionGroupID = (int)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime?)column.Value;
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
		
		#region InstitutionGroupInstitutionID
		
		private int _InstitutionGroupInstitutionID = default(int);
		
		/// <summary>
		/// Column: InstitutionGroupInstitutionID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("InstitutionGroupInstitutionID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int InstitutionGroupInstitutionID
		{
			get
			{
				return _InstitutionGroupInstitutionID;
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
		
		#endregion InstitutionGroupInstitutionID
		
		#region InstitutionGroupID
		
		private int _InstitutionGroupID = default(int);
		
		/// <summary>
		/// Column: InstitutionGroupID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("InstitutionGroupID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int InstitutionGroupID
		{
			get
			{
				return _InstitutionGroupID;
			}
			set
			{
				if (_InstitutionGroupID != value)
				{
					_InstitutionGroupID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionGroupID
		
		#region InstitutionCode
		
		private string _InstitutionCode = string.Empty;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=10, IsInForeignKey=true)]
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
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4, IsNullable=true)]
		public DateTime? CreationDate
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__InstitutionGroupInstitution"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__InstitutionGroupInstitution"/>, 
		/// returns an instance of <see cref="__InstitutionGroupInstitution"/>; otherwise returns null.</returns>
		public static new __InstitutionGroupInstitution FromArray(byte[] byteArray)
		{
			__InstitutionGroupInstitution o = null;
			
			try
			{
				o = (__InstitutionGroupInstitution) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__InstitutionGroupInstitution"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__InstitutionGroupInstitution"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __InstitutionGroupInstitution)
			{
				__InstitutionGroupInstitution o = (__InstitutionGroupInstitution) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.InstitutionGroupInstitutionID == InstitutionGroupInstitutionID &&
					o.InstitutionGroupID == InstitutionGroupID &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
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
				throw new ArgumentException("Argument is not of type __InstitutionGroupInstitution");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__InstitutionGroupInstitution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionGroupInstitution"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__InstitutionGroupInstitution a, __InstitutionGroupInstitution b)
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
		/// <param name="a">The first <see cref="__InstitutionGroupInstitution"/> object to compare.</param>
		/// <param name="b">The second <see cref="__InstitutionGroupInstitution"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__InstitutionGroupInstitution a, __InstitutionGroupInstitution b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__InstitutionGroupInstitution"/> object to compare with the current <see cref="__InstitutionGroupInstitution"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __InstitutionGroupInstitution))
			{
				return false;
			}
			
			return this == (__InstitutionGroupInstitution) obj;
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
		/// list.Sort(SortOrder.Ascending, __InstitutionGroupInstitution.SortColumn.InstitutionGroupInstitutionID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string InstitutionGroupInstitutionID = "InstitutionGroupInstitutionID";	
			public const string InstitutionGroupID = "InstitutionGroupID";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string CreationDate = "CreationDate";	
			public const string CreationUserID = "CreationUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

