
// Generated 5/6/2009 2:57:04 PM
// Do not modify the contents of this code file.
// This abstract class __TitleAssociationType is based upon TitleAssociationType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleAssociationType : __TitleAssociationType
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
	public abstract class __TitleAssociationType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleAssociationType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="titleAssociationName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleAssociationLabel"></param>
		public __TitleAssociationType(int titleAssociationTypeID, 
			string titleAssociationName, 
			string mARCTag, 
			string mARCIndicator2, 
			string titleAssociationLabel) : this()
		{
			_TitleAssociationTypeID = titleAssociationTypeID;
			TitleAssociationName = titleAssociationName;
			MARCTag = mARCTag;
			MARCIndicator2 = mARCIndicator2;
			TitleAssociationLabel = titleAssociationLabel;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__TitleAssociationType()
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
					case "TitleAssociationTypeID" :
					{
						_TitleAssociationTypeID = (int)column.Value;
						break;
					}
					case "TitleAssociationName" :
					{
						_TitleAssociationName = (string)column.Value;
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
					case "TitleAssociationLabel" :
					{
						_TitleAssociationLabel = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region TitleAssociationTypeID
		
		private int _TitleAssociationTypeID = default(int);
		
		/// <summary>
		/// Column: TitleAssociationTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleAssociationTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int TitleAssociationTypeID
		{
			get
			{
				return _TitleAssociationTypeID;
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
		
		#endregion TitleAssociationTypeID
		
		#region TitleAssociationName
		
		private string _TitleAssociationName = string.Empty;
		
		/// <summary>
		/// Column: TitleAssociationName;
		/// DBMS data type: nvarchar(60);
		/// </summary>
		[ColumnDefinition("TitleAssociationName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=60)]
		public string TitleAssociationName
		{
			get
			{
				return _TitleAssociationName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 60);
				if (_TitleAssociationName != value)
				{
					_TitleAssociationName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleAssociationName
		
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
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("MARCIndicator2", DbTargetType=SqlDbType.NChar, Ordinal=4, CharacterMaxLength=1)]
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
		
		#region TitleAssociationLabel
		
		private string _TitleAssociationLabel = string.Empty;
		
		/// <summary>
		/// Column: TitleAssociationLabel;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("TitleAssociationLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=30)]
		public string TitleAssociationLabel
		{
			get
			{
				return _TitleAssociationLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_TitleAssociationLabel != value)
				{
					_TitleAssociationLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleAssociationLabel
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleAssociationType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleAssociationType"/>, 
		/// returns an instance of <see cref="__TitleAssociationType"/>; otherwise returns null.</returns>
		public static new __TitleAssociationType FromArray(byte[] byteArray)
		{
			__TitleAssociationType o = null;
			
			try
			{
				o = (__TitleAssociationType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleAssociationType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleAssociationType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleAssociationType)
			{
				__TitleAssociationType o = (__TitleAssociationType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleAssociationTypeID == TitleAssociationTypeID &&
					GetComparisonString(o.TitleAssociationName) == GetComparisonString(TitleAssociationName) &&
					GetComparisonString(o.MARCTag) == GetComparisonString(MARCTag) &&
					GetComparisonString(o.MARCIndicator2) == GetComparisonString(MARCIndicator2) &&
					GetComparisonString(o.TitleAssociationLabel) == GetComparisonString(TitleAssociationLabel) 
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
				throw new ArgumentException("Argument is not of type __TitleAssociationType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleAssociationType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAssociationType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleAssociationType a, __TitleAssociationType b)
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
		/// <param name="a">The first <see cref="__TitleAssociationType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAssociationType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleAssociationType a, __TitleAssociationType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleAssociationType"/> object to compare with the current <see cref="__TitleAssociationType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleAssociationType))
			{
				return false;
			}
			
			return this == (__TitleAssociationType) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleAssociationType.SortColumn.TitleAssociationTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleAssociationTypeID = "TitleAssociationTypeID";	
			public const string TitleAssociationName = "TitleAssociationName";	
			public const string MARCTag = "MARCTag";	
			public const string MARCIndicator2 = "MARCIndicator2";	
			public const string TitleAssociationLabel = "TitleAssociationLabel";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
