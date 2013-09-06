
// Generated 4/16/2009 11:23:06 AM
// Do not modify the contents of this code file.
// This abstract class __MarcImportBatch is based upon MarcImportBatch.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class MarcImportBatch : __MarcImportBatch
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
	public abstract class __MarcImportBatch : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __MarcImportBatch()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcImportBatchID"></param>
		/// <param name="fileName"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationDate"></param>
		public __MarcImportBatch(int marcImportBatchID, 
			string fileName, 
			string institutionCode, 
			DateTime creationDate) : this()
		{
			_MarcImportBatchID = marcImportBatchID;
			FileName = fileName;
			InstitutionCode = institutionCode;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__MarcImportBatch()
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
					case "MarcImportBatchID" :
					{
						_MarcImportBatchID = (int)column.Value;
						break;
					}
					case "FileName" :
					{
						_FileName = (string)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region MarcImportBatchID
		
		private int _MarcImportBatchID = default(int);
		
		/// <summary>
		/// Column: MarcImportBatchID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcImportBatchID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int MarcImportBatchID
		{
			get
			{
				return _MarcImportBatchID;
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
		
		#endregion MarcImportBatchID
		
		#region FileName
		
		private string _FileName = string.Empty;
		
		/// <summary>
		/// Column: FileName;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("FileName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=500)]
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_FileName != value)
				{
					_FileName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileName
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=10, IsNullable=true)]
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
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__MarcImportBatch"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__MarcImportBatch"/>, 
		/// returns an instance of <see cref="__MarcImportBatch"/>; otherwise returns null.</returns>
		public static new __MarcImportBatch FromArray(byte[] byteArray)
		{
			__MarcImportBatch o = null;
			
			try
			{
				o = (__MarcImportBatch) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__MarcImportBatch"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__MarcImportBatch"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __MarcImportBatch)
			{
				__MarcImportBatch o = (__MarcImportBatch) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcImportBatchID == MarcImportBatchID &&
					GetComparisonString(o.FileName) == GetComparisonString(FileName) &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					o.CreationDate == CreationDate 
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
				throw new ArgumentException("Argument is not of type __MarcImportBatch");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__MarcImportBatch"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcImportBatch"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__MarcImportBatch a, __MarcImportBatch b)
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
		/// <param name="a">The first <see cref="__MarcImportBatch"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcImportBatch"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__MarcImportBatch a, __MarcImportBatch b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__MarcImportBatch"/> object to compare with the current <see cref="__MarcImportBatch"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __MarcImportBatch))
			{
				return false;
			}
			
			return this == (__MarcImportBatch) obj;
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
		/// list.Sort(SortOrder.Ascending, __MarcImportBatch.SortColumn.MarcImportBatchID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcImportBatchID = "MarcImportBatchID";	
			public const string FileName = "FileName";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
