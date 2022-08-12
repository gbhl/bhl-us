
// Generated 1/5/2021 3:25:09 PM
// Do not modify the contents of this code file.
// This abstract class __Configuration is based upon dbo.Configuration.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Configuration : __Configuration
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
	public abstract class __Configuration : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Configuration()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="configurationID"></param>
		/// <param name="configurationName"></param>
		/// <param name="configurationValue"></param>
		public __Configuration(int configurationID, 
			string configurationName, 
			string configurationValue) : this()
		{
			ConfigurationID = configurationID;
			ConfigurationName = configurationName;
			ConfigurationValue = configurationValue;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Configuration()
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
					case "ConfigurationID" :
					{
						_ConfigurationID = (int)column.Value;
						break;
					}
					case "ConfigurationName" :
					{
						_ConfigurationName = (string)column.Value;
						break;
					}
					case "ConfigurationValue" :
					{
						_ConfigurationValue = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ConfigurationID
		
		private int _ConfigurationID = default(int);
		
		/// <summary>
		/// Column: ConfigurationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ConfigurationID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInPrimaryKey=true)]
		public int ConfigurationID
		{
			get
			{
				return _ConfigurationID;
			}
			set
			{
				if (_ConfigurationID != value)
				{
					_ConfigurationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ConfigurationID
		
		#region ConfigurationName
		
		private string _ConfigurationName = string.Empty;
		
		/// <summary>
		/// Column: ConfigurationName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ConfigurationName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string ConfigurationName
		{
			get
			{
				return _ConfigurationName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ConfigurationName != value)
				{
					_ConfigurationName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ConfigurationName
		
		#region ConfigurationValue
		
		private string _ConfigurationValue = string.Empty;
		
		/// <summary>
		/// Column: ConfigurationValue;
		/// DBMS data type: nvarchar(1000);
		/// </summary>
		[ColumnDefinition("ConfigurationValue", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1000)]
		public string ConfigurationValue
		{
			get
			{
				return _ConfigurationValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1000);
				if (_ConfigurationValue != value)
				{
					_ConfigurationValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ConfigurationValue
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Configuration"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Configuration"/>, 
		/// returns an instance of <see cref="__Configuration"/>; otherwise returns null.</returns>
		public static new __Configuration FromArray(byte[] byteArray)
		{
			__Configuration o = null;
			
			try
			{
				o = (__Configuration) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Configuration"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Configuration"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Configuration)
			{
				__Configuration o = (__Configuration) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ConfigurationID == ConfigurationID &&
					GetComparisonString(o.ConfigurationName) == GetComparisonString(ConfigurationName) &&
					GetComparisonString(o.ConfigurationValue) == GetComparisonString(ConfigurationValue) 
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
				throw new ArgumentException("Argument is not of type __Configuration");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Configuration"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Configuration"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Configuration a, __Configuration b)
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
		/// <param name="a">The first <see cref="__Configuration"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Configuration"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Configuration a, __Configuration b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Configuration"/> object to compare with the current <see cref="__Configuration"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Configuration))
			{
				return false;
			}
			
			return this == (__Configuration) obj;
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
		/// list.Sort(SortOrder.Ascending, __Configuration.SortColumn.ConfigurationID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ConfigurationID = "ConfigurationID";	
			public const string ConfigurationName = "ConfigurationName";	
			public const string ConfigurationValue = "ConfigurationValue";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

