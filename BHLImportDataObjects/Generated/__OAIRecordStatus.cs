
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordStatus is based upon OAIRecordStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordStatus : __OAIRecordStatus
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
	public abstract class __OAIRecordStatus : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordStatus()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="recordStatus"></param>
		/// <param name="statusDescription"></param>
		public __OAIRecordStatus(int oAIRecordStatusID, 
			string recordStatus, 
			string statusDescription) : this()
		{
			_OAIRecordStatusID = oAIRecordStatusID;
			RecordStatus = recordStatus;
			StatusDescription = statusDescription;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordStatus()
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
					case "OAIRecordStatusID" :
					{
						_OAIRecordStatusID = (int)column.Value;
						break;
					}
					case "RecordStatus" :
					{
						_RecordStatus = (string)column.Value;
						break;
					}
					case "StatusDescription" :
					{
						_StatusDescription = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region OAIRecordStatusID
		
		private int _OAIRecordStatusID = default(int);
		
		/// <summary>
		/// Column: OAIRecordStatusID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordStatusID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int OAIRecordStatusID
		{
			get
			{
				return _OAIRecordStatusID;
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
		
		#endregion OAIRecordStatusID
		
		#region RecordStatus
		
		private string _RecordStatus = string.Empty;
		
		/// <summary>
		/// Column: RecordStatus;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("RecordStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string RecordStatus
		{
			get
			{
				return _RecordStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_RecordStatus != value)
				{
					_RecordStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RecordStatus
		
		#region StatusDescription
		
		private string _StatusDescription = string.Empty;
		
		/// <summary>
		/// Column: StatusDescription;
		/// DBMS data type: nvarchar(400);
		/// </summary>
		[ColumnDefinition("StatusDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=400)]
		public string StatusDescription
		{
			get
			{
				return _StatusDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 400);
				if (_StatusDescription != value)
				{
					_StatusDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatusDescription
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordStatus"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordStatus"/>, 
		/// returns an instance of <see cref="__OAIRecordStatus"/>; otherwise returns null.</returns>
		public static new __OAIRecordStatus FromArray(byte[] byteArray)
		{
			__OAIRecordStatus o = null;
			
			try
			{
				o = (__OAIRecordStatus) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordStatus"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordStatus"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordStatus)
			{
				__OAIRecordStatus o = (__OAIRecordStatus) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordStatusID == OAIRecordStatusID &&
					GetComparisonString(o.RecordStatus) == GetComparisonString(RecordStatus) &&
					GetComparisonString(o.StatusDescription) == GetComparisonString(StatusDescription) 
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
				throw new ArgumentException("Argument is not of type __OAIRecordStatus");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordStatus"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordStatus a, __OAIRecordStatus b)
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
		/// <param name="a">The first <see cref="__OAIRecordStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordStatus"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordStatus a, __OAIRecordStatus b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordStatus"/> object to compare with the current <see cref="__OAIRecordStatus"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordStatus))
			{
				return false;
			}
			
			return this == (__OAIRecordStatus) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordStatus.SortColumn.OAIRecordStatusID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordStatusID = "OAIRecordStatusID";	
			public const string RecordStatus = "RecordStatus";	
			public const string StatusDescription = "StatusDescription";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
