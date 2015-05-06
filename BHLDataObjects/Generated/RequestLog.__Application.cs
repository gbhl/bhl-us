
// Generated 5/1/2015 6:06:26 PM
// Do not modify the contents of this code file.
// This abstract class __Application is based upon Application.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.RequestLog.DataObjects
// {
//		[Serializable]
// 		public class Application : __Application
//		{
//		}
// }

#endregion How To Implement

using System;
using System.Data;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DataObjects
{	
	[Serializable]
	public abstract class __Application : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Application()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="applicationID"></param>
		/// <param name="applicationName"></param>
		public __Application(int applicationID, 
			string applicationName) : this()
		{
			ApplicationID = applicationID;
			ApplicationName = applicationName;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Application()
		{
		}
		
		#endregion Destructor
		
		#region Set Values
		
		/// <summary>
		/// Set the property values of this instance from the specified <see cref="DataItemRow"/>.
		/// </summary>
		public virtual void SetValues(CustomDataRow row)
		{
			foreach (CustomDataColumn column in row)
			{
				switch (column.Name)
				{
					case "ApplicationID" :
					{
						_ApplicationID = (int)column.Value;
						break;
					}
					case "ApplicationName" :
					{
						_ApplicationName = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region ApplicationID
		
		private int _ApplicationID = default(int);
		
		/// <summary>
		/// Column: ApplicationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ApplicationID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ApplicationID
		{
			get
			{
				return _ApplicationID;
			}
			set
			{
				if (_ApplicationID != value)
				{
					_ApplicationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ApplicationID
		
		#region ApplicationName
		
		private string _ApplicationName = string.Empty;
		
		/// <summary>
		/// Column: ApplicationName;
		/// DBMS data type: varchar(50);
		/// </summary>
		[ColumnDefinition("ApplicationName", DbTargetType=SqlDbType.VarChar, Ordinal=2, CharacterMaxLength=50)]
		public string ApplicationName
		{
			get
			{
				return _ApplicationName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ApplicationName != value)
				{
					_ApplicationName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ApplicationName
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Application"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Application"/>, 
		/// returns an instance of <see cref="__Application"/>; otherwise returns null.</returns>
		public static new __Application FromArray(byte[] byteArray)
		{
			__Application o = null;
			
			try
			{
				o = (__Application) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Application"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Application"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Application)
			{
				__Application o = (__Application) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ApplicationID == ApplicationID &&
					GetComparisonString(o.ApplicationName) == GetComparisonString(ApplicationName) 
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
				throw new ArgumentException("Argument is not of type __Application");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Application"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Application"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Application a, __Application b)
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
		/// <param name="a">The first <see cref="__Application"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Application"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Application a, __Application b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Application"/> object to compare with the current <see cref="__Application"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Application))
			{
				return false;
			}
			
			return this == (__Application) obj;
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
		/// For example where list is a instance of <see cref="DataCollection">, 
		/// list.Sort(SortOrder.Ascending, __Application.SortColumn.ApplicationID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ApplicationID = "ApplicationID";	
			public const string ApplicationName = "ApplicationName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
