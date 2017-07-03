
// Generated 6/29/2017 4:37:03 PM
// Do not modify the contents of this code file.
// This abstract class __MaterialType is based upon dbo.MaterialType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class MaterialType : __MaterialType
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
	public abstract class __MaterialType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __MaterialType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="materialTypeID"></param>
		/// <param name="materialTypeName"></param>
		/// <param name="materialTypeLabel"></param>
		/// <param name="mARCCode"></param>
		public __MaterialType(int materialTypeID, 
			string materialTypeName, 
			string materialTypeLabel, 
			string mARCCode) : this()
		{
			_MaterialTypeID = materialTypeID;
			MaterialTypeName = materialTypeName;
			MaterialTypeLabel = materialTypeLabel;
			MARCCode = mARCCode;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__MaterialType()
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
					case "MaterialTypeID" :
					{
						_MaterialTypeID = (int)column.Value;
						break;
					}
					case "MaterialTypeName" :
					{
						_MaterialTypeName = (string)column.Value;
						break;
					}
					case "MaterialTypeLabel" :
					{
						_MaterialTypeLabel = (string)column.Value;
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
		
		#region MaterialTypeID
		
		private int _MaterialTypeID = default(int);
		
		/// <summary>
		/// Column: MaterialTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MaterialTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int MaterialTypeID
		{
			get
			{
				return _MaterialTypeID;
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
		
		#endregion MaterialTypeID
		
		#region MaterialTypeName
		
		private string _MaterialTypeName = string.Empty;
		
		/// <summary>
		/// Column: MaterialTypeName;
		/// DBMS data type: nvarchar(60);
		/// </summary>
		[ColumnDefinition("MaterialTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=60)]
		public string MaterialTypeName
		{
			get
			{
				return _MaterialTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 60);
				if (_MaterialTypeName != value)
				{
					_MaterialTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MaterialTypeName
		
		#region MaterialTypeLabel
		
		private string _MaterialTypeLabel = string.Empty;
		
		/// <summary>
		/// Column: MaterialTypeLabel;
		/// DBMS data type: nvarchar(60);
		/// </summary>
		[ColumnDefinition("MaterialTypeLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=60)]
		public string MaterialTypeLabel
		{
			get
			{
				return _MaterialTypeLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 60);
				if (_MaterialTypeLabel != value)
				{
					_MaterialTypeLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MaterialTypeLabel
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__MaterialType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__MaterialType"/>, 
		/// returns an instance of <see cref="__MaterialType"/>; otherwise returns null.</returns>
		public static new __MaterialType FromArray(byte[] byteArray)
		{
			__MaterialType o = null;
			
			try
			{
				o = (__MaterialType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__MaterialType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__MaterialType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __MaterialType)
			{
				__MaterialType o = (__MaterialType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MaterialTypeID == MaterialTypeID &&
					GetComparisonString(o.MaterialTypeName) == GetComparisonString(MaterialTypeName) &&
					GetComparisonString(o.MaterialTypeLabel) == GetComparisonString(MaterialTypeLabel) &&
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
				throw new ArgumentException("Argument is not of type __MaterialType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__MaterialType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MaterialType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__MaterialType a, __MaterialType b)
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
		/// <param name="a">The first <see cref="__MaterialType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MaterialType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__MaterialType a, __MaterialType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__MaterialType"/> object to compare with the current <see cref="__MaterialType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __MaterialType))
			{
				return false;
			}
			
			return this == (__MaterialType) obj;
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
		/// list.Sort(SortOrder.Ascending, __MaterialType.SortColumn.MaterialTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MaterialTypeID = "MaterialTypeID";	
			public const string MaterialTypeName = "MaterialTypeName";	
			public const string MaterialTypeLabel = "MaterialTypeLabel";	
			public const string MARCCode = "MARCCode";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

