
// Generated 2/15/2011 3:11:04 PM
// Do not modify the contents of this code file.
// This abstract class __TitleVariantType is based upon TitleVariantType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleVariantType : __TitleVariantType
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
	public abstract class __TitleVariantType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleVariantType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="titleVariantTypeName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleVariantLabel"></param>
		public __TitleVariantType(int titleVariantTypeID, 
			string titleVariantTypeName, 
			string mARCTag, 
			string mARCIndicator2, 
			string titleVariantLabel) : this()
		{
			_TitleVariantTypeID = titleVariantTypeID;
			TitleVariantTypeName = titleVariantTypeName;
			MARCTag = mARCTag;
			MARCIndicator2 = mARCIndicator2;
			TitleVariantLabel = titleVariantLabel;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleVariantType()
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
					case "TitleVariantTypeID" :
					{
						_TitleVariantTypeID = (int)column.Value;
						break;
					}
					case "TitleVariantTypeName" :
					{
						_TitleVariantTypeName = (string)column.Value;
						break;
					}
					case "MARCTag" :
					{
						_MARCTag = (string)column.Value;
						break;
					}
					case "MARCIndicator2" :
					{
						_MARCIndicator2 = (string)column.Value;
						break;
					}
					case "TitleVariantLabel" :
					{
						_TitleVariantLabel = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region TitleVariantTypeID
		
		private int _TitleVariantTypeID = default(int);
		
		/// <summary>
		/// Column: TitleVariantTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleVariantTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int TitleVariantTypeID
		{
			get
			{
				return _TitleVariantTypeID;
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
		
		#endregion TitleVariantTypeID
		
		#region TitleVariantTypeName
		
		private string _TitleVariantTypeName = string.Empty;
		
		/// <summary>
		/// Column: TitleVariantTypeName;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("TitleVariantTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string TitleVariantTypeName
		{
			get
			{
				return _TitleVariantTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_TitleVariantTypeName != value)
				{
					_TitleVariantTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleVariantTypeName
		
		#region MARCTag
		
		private string _MARCTag = string.Empty;
		
		/// <summary>
		/// Column: MARCTag;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("MARCTag", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=20)]
		public string MARCTag
		{
			get
			{
				return _MARCTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_MARCTag != value)
				{
					_MARCTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCTag
		
		#region MARCIndicator2
		
		private string _MARCIndicator2 = string.Empty;
		
		/// <summary>
		/// Column: MARCIndicator2;
		/// DBMS data type: nvarchar(1);
		/// </summary>
		[ColumnDefinition("MARCIndicator2", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1)]
		public string MARCIndicator2
		{
			get
			{
				return _MARCIndicator2;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_MARCIndicator2 != value)
				{
					_MARCIndicator2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCIndicator2
		
		#region TitleVariantLabel
		
		private string _TitleVariantLabel = string.Empty;
		
		/// <summary>
		/// Column: TitleVariantLabel;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("TitleVariantLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=30)]
		public string TitleVariantLabel
		{
			get
			{
				return _TitleVariantLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_TitleVariantLabel != value)
				{
					_TitleVariantLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleVariantLabel
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleVariantType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleVariantType"/>, 
		/// returns an instance of <see cref="__TitleVariantType"/>; otherwise returns null.</returns>
		public static new __TitleVariantType FromArray(byte[] byteArray)
		{
			__TitleVariantType o = null;
			
			try
			{
				o = (__TitleVariantType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleVariantType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleVariantType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleVariantType)
			{
				__TitleVariantType o = (__TitleVariantType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleVariantTypeID == TitleVariantTypeID &&
					GetComparisonString(o.TitleVariantTypeName) == GetComparisonString(TitleVariantTypeName) &&
					GetComparisonString(o.MARCTag) == GetComparisonString(MARCTag) &&
					GetComparisonString(o.MARCIndicator2) == GetComparisonString(MARCIndicator2) &&
					GetComparisonString(o.TitleVariantLabel) == GetComparisonString(TitleVariantLabel) 
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
				throw new ArgumentException("Argument is not of type __TitleVariantType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleVariantType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleVariantType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleVariantType a, __TitleVariantType b)
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
		/// <param name="a">The first <see cref="__TitleVariantType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleVariantType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleVariantType a, __TitleVariantType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleVariantType"/> object to compare with the current <see cref="__TitleVariantType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleVariantType))
			{
				return false;
			}
			
			return this == (__TitleVariantType) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleVariantType.SortColumn.TitleVariantTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleVariantTypeID = "TitleVariantTypeID";	
			public const string TitleVariantTypeName = "TitleVariantTypeName";	
			public const string MARCTag = "MARCTag";	
			public const string MARCIndicator2 = "MARCIndicator2";	
			public const string TitleVariantLabel = "TitleVariantLabel";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
