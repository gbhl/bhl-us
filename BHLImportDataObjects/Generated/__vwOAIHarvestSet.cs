
// Generated 10/16/2013 11:35:24 AM
// Do not modify the contents of this code file.
// This abstract class __vwOAIHarvestSet is based upon vwOAIHarvestSet.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class vwOAIHarvestSet : __vwOAIHarvestSet
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{	
	[Serializable]
	public abstract class __vwOAIHarvestSet : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __vwOAIHarvestSet()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="harvestSetID"></param>
		/// <param name="repositoryName"></param>
		/// <param name="baseUrl"></param>
		/// <param name="harvestSetName"></param>
		/// <param name="setName"></param>
		/// <param name="setSpec"></param>
		/// <param name="prefix"></param>
		/// <param name="namespace"></param>
		/// <param name="schema"></param>
		/// <param name="assemblyName"></param>
		/// <param name="isActive"></param>
		public __vwOAIHarvestSet(int harvestSetID, 
			string repositoryName, 
			string baseUrl, 
			string harvestSetName, 
			string setName, 
			string setSpec, 
			string prefix, 
			string oainamespace, 
			string schema, 
			string assemblyName, 
			short isActive) : this()
		{
			HarvestSetID = harvestSetID;
			RepositoryName = repositoryName;
			BaseUrl = baseUrl;
			HarvestSetName = harvestSetName;
			SetName = setName;
			SetSpec = setSpec;
			Prefix = prefix;
			Namespace = oainamespace;
			Schema = schema;
			AssemblyName = assemblyName;
			IsActive = isActive;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__vwOAIHarvestSet()
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
					case "HarvestSetID" :
					{
						_HarvestSetID = (int)column.Value;
						break;
					}
					case "RepositoryName" :
					{
						_RepositoryName = (string)column.Value;
						break;
					}
					case "BaseUrl" :
					{
						_BaseUrl = (string)column.Value;
						break;
					}
					case "HarvestSetName" :
					{
						_HarvestSetName = (string)column.Value;
						break;
					}
					case "SetName" :
					{
						_SetName = (string)column.Value;
						break;
					}
					case "SetSpec" :
					{
						_SetSpec = (string)column.Value;
						break;
					}
					case "Prefix" :
					{
						_Prefix = (string)column.Value;
						break;
					}
					case "Namespace" :
					{
						_Namespace = (string)column.Value;
						break;
					}
					case "Schema" :
					{
						_Schema = (string)column.Value;
						break;
					}
					case "AssemblyName" :
					{
						_AssemblyName = (string)column.Value;
						break;
					}
					case "IsActive" :
					{
						_IsActive = (short)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region HarvestSetID
		
		private int _HarvestSetID = default(int);
		
		/// <summary>
		/// Column: HarvestSetID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("HarvestSetID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10)]
		public int HarvestSetID
		{
			get
			{
				return _HarvestSetID;
			}
			set
			{
				if (_HarvestSetID != value)
				{
					_HarvestSetID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestSetID
		
		#region RepositoryName
		
		private string _RepositoryName = string.Empty;
		
		/// <summary>
		/// Column: RepositoryName;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("RepositoryName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=100)]
		public string RepositoryName
		{
			get
			{
				return _RepositoryName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_RepositoryName != value)
				{
					_RepositoryName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RepositoryName
		
		#region BaseUrl
		
		private string _BaseUrl = string.Empty;
		
		/// <summary>
		/// Column: BaseUrl;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("BaseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=150)]
		public string BaseUrl
		{
			get
			{
				return _BaseUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_BaseUrl != value)
				{
					_BaseUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BaseUrl
		
		#region HarvestSetName
		
		private string _HarvestSetName = string.Empty;
		
		/// <summary>
		/// Column: HarvestSetName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("HarvestSetName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=150)]
		public string HarvestSetName
		{
			get
			{
				return _HarvestSetName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_HarvestSetName != value)
				{
					_HarvestSetName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestSetName
		
		#region SetName
		
		private string _SetName = null;
		
		/// <summary>
		/// Column: SetName;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("SetName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100, IsNullable=true)]
		public string SetName
		{
			get
			{
				return _SetName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_SetName != value)
				{
					_SetName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SetName
		
		#region SetSpec
		
		private string _SetSpec = null;
		
		/// <summary>
		/// Column: SetSpec;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("SetSpec", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50, IsNullable=true)]
		public string SetSpec
		{
			get
			{
				return _SetSpec;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SetSpec != value)
				{
					_SetSpec = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SetSpec
		
		#region Prefix
		
		private string _Prefix = string.Empty;
		
		/// <summary>
		/// Column: Prefix;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Prefix", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=20)]
		public string Prefix
		{
			get
			{
				return _Prefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Prefix != value)
				{
					_Prefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Prefix
		
		#region Namespace
		
		private string _Namespace = string.Empty;
		
		/// <summary>
		/// Column: Namespace;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("Namespace", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=150)]
		public string Namespace
		{
			get
			{
				return _Namespace;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_Namespace != value)
				{
					_Namespace = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Namespace
		
		#region Schema
		
		private string _Schema = string.Empty;
		
		/// <summary>
		/// Column: Schema;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("Schema", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=150)]
		public string Schema
		{
			get
			{
				return _Schema;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_Schema != value)
				{
					_Schema = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Schema
		
		#region AssemblyName
		
		private string _AssemblyName = string.Empty;
		
		/// <summary>
		/// Column: AssemblyName;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("AssemblyName", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=100)]
		public string AssemblyName
		{
			get
			{
				return _AssemblyName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_AssemblyName != value)
				{
					_AssemblyName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AssemblyName
		
		#region IsActive
		
		private short _IsActive = default(short);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.SmallInt, Ordinal=11, NumericPrecision=5)]
		public short IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				if (_IsActive != value)
				{
					_IsActive = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsActive
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__vwOAIHarvestSet"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__vwOAIHarvestSet"/>, 
		/// returns an instance of <see cref="__vwOAIHarvestSet"/>; otherwise returns null.</returns>
		public static new __vwOAIHarvestSet FromArray(byte[] byteArray)
		{
			__vwOAIHarvestSet o = null;
			
			try
			{
				o = (__vwOAIHarvestSet) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__vwOAIHarvestSet"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__vwOAIHarvestSet"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __vwOAIHarvestSet)
			{
				__vwOAIHarvestSet o = (__vwOAIHarvestSet) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.HarvestSetID == HarvestSetID &&
					GetComparisonString(o.RepositoryName) == GetComparisonString(RepositoryName) &&
					GetComparisonString(o.BaseUrl) == GetComparisonString(BaseUrl) &&
					GetComparisonString(o.HarvestSetName) == GetComparisonString(HarvestSetName) &&
					GetComparisonString(o.SetName) == GetComparisonString(SetName) &&
					GetComparisonString(o.SetSpec) == GetComparisonString(SetSpec) &&
					GetComparisonString(o.Prefix) == GetComparisonString(Prefix) &&
					GetComparisonString(o.Namespace) == GetComparisonString(Namespace) &&
					GetComparisonString(o.Schema) == GetComparisonString(Schema) &&
					GetComparisonString(o.AssemblyName) == GetComparisonString(AssemblyName) &&
					o.IsActive == IsActive 
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
				throw new ArgumentException("Argument is not of type __vwOAIHarvestSet");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__vwOAIHarvestSet"/> object to compare.</param>
		/// <param name="b">The second <see cref="__vwOAIHarvestSet"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__vwOAIHarvestSet a, __vwOAIHarvestSet b)
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
		/// <param name="a">The first <see cref="__vwOAIHarvestSet"/> object to compare.</param>
		/// <param name="b">The second <see cref="__vwOAIHarvestSet"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__vwOAIHarvestSet a, __vwOAIHarvestSet b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__vwOAIHarvestSet"/> object to compare with the current <see cref="__vwOAIHarvestSet"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __vwOAIHarvestSet))
			{
				return false;
			}
			
			return this == (__vwOAIHarvestSet) obj;
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
		/// list.Sort(SortOrder.Ascending, __vwOAIHarvestSet.SortColumn.HarvestSetID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string HarvestSetID = "HarvestSetID";	
			public const string RepositoryName = "RepositoryName";	
			public const string BaseUrl = "BaseUrl";	
			public const string HarvestSetName = "HarvestSetName";	
			public const string SetName = "SetName";	
			public const string SetSpec = "SetSpec";	
			public const string Prefix = "Prefix";	
			public const string Namespace = "Namespace";	
			public const string Schema = "Schema";	
			public const string AssemblyName = "AssemblyName";	
			public const string IsActive = "IsActive";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
