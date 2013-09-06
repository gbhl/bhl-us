
// Generated 6/25/2010 5:09:34 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotationPolygon is based upon AnnotationPolygon.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotationPolygon : __AnnotationPolygon
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
	public abstract class __AnnotationPolygon : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotationPolygon()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotationPolygonID"></param>
		/// <param name="annotationID"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotationPolygon(int annotationPolygonID, 
			int annotationID, 
			int? polygonX1, 
			int? polygonY1, 
			int? polygonX2, 
			int? polygonY2, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotationPolygonID = annotationPolygonID;
			AnnotationID = annotationID;
			PolygonX1 = polygonX1;
			PolygonY1 = polygonY1;
			PolygonX2 = polygonX2;
			PolygonY2 = polygonY2;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotationPolygon()
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
					case "AnnotationPolygonID" :
					{
						_AnnotationPolygonID = (int)column.Value;
						break;
					}
					case "AnnotationID" :
					{
						_AnnotationID = (int)column.Value;
						break;
					}
					case "PolygonX1" :
					{
						_PolygonX1 = (int?)column.Value;
						break;
					}
					case "PolygonY1" :
					{
						_PolygonY1 = (int?)column.Value;
						break;
					}
					case "PolygonX2" :
					{
						_PolygonX2 = (int?)column.Value;
						break;
					}
					case "PolygonY2" :
					{
						_PolygonY2 = (int?)column.Value;
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
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region AnnotationPolygonID
		
		private int _AnnotationPolygonID = default(int);
		
		/// <summary>
		/// Column: AnnotationPolygonID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotationPolygonID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AnnotationPolygonID
		{
			get
			{
				return _AnnotationPolygonID;
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
		
		#endregion AnnotationPolygonID
		
		#region AnnotationID
		
		private int _AnnotationID = default(int);
		
		/// <summary>
		/// Column: AnnotationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotationID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotationID
		{
			get
			{
				return _AnnotationID;
			}
			set
			{
				if (_AnnotationID != value)
				{
					_AnnotationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotationID
		
		#region PolygonX1
		
		private int? _PolygonX1 = null;
		
		/// <summary>
		/// Column: PolygonX1;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonX1", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
		public int? PolygonX1
		{
			get
			{
				return _PolygonX1;
			}
			set
			{
				if (_PolygonX1 != value)
				{
					_PolygonX1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonX1
		
		#region PolygonY1
		
		private int? _PolygonY1 = null;
		
		/// <summary>
		/// Column: PolygonY1;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonY1", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? PolygonY1
		{
			get
			{
				return _PolygonY1;
			}
			set
			{
				if (_PolygonY1 != value)
				{
					_PolygonY1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonY1
		
		#region PolygonX2
		
		private int? _PolygonX2 = null;
		
		/// <summary>
		/// Column: PolygonX2;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonX2", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? PolygonX2
		{
			get
			{
				return _PolygonX2;
			}
			set
			{
				if (_PolygonX2 != value)
				{
					_PolygonX2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonX2
		
		#region PolygonY2
		
		private int? _PolygonY2 = null;
		
		/// <summary>
		/// Column: PolygonY2;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PolygonY2", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? PolygonY2
		{
			get
			{
				return _PolygonY2;
			}
			set
			{
				if (_PolygonY2 != value)
				{
					_PolygonY2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PolygonY2
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotationPolygon"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotationPolygon"/>, 
		/// returns an instance of <see cref="__AnnotationPolygon"/>; otherwise returns null.</returns>
		public static new __AnnotationPolygon FromArray(byte[] byteArray)
		{
			__AnnotationPolygon o = null;
			
			try
			{
				o = (__AnnotationPolygon) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotationPolygon"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotationPolygon"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotationPolygon)
			{
				__AnnotationPolygon o = (__AnnotationPolygon) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotationPolygonID == AnnotationPolygonID &&
					o.AnnotationID == AnnotationID &&
					o.PolygonX1 == PolygonX1 &&
					o.PolygonY1 == PolygonY1 &&
					o.PolygonX2 == PolygonX2 &&
					o.PolygonY2 == PolygonY2 &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate 
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
				throw new ArgumentException("Argument is not of type __AnnotationPolygon");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotationPolygon"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationPolygon"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotationPolygon a, __AnnotationPolygon b)
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
		/// <param name="a">The first <see cref="__AnnotationPolygon"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotationPolygon"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotationPolygon a, __AnnotationPolygon b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotationPolygon"/> object to compare with the current <see cref="__AnnotationPolygon"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotationPolygon))
			{
				return false;
			}
			
			return this == (__AnnotationPolygon) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotationPolygon.SortColumn.AnnotationPolygonID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotationPolygonID = "AnnotationPolygonID";	
			public const string AnnotationID = "AnnotationID";	
			public const string PolygonX1 = "PolygonX1";	
			public const string PolygonY1 = "PolygonY1";	
			public const string PolygonX2 = "PolygonX2";	
			public const string PolygonY2 = "PolygonY2";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
