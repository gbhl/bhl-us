using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CustomDataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class CustomObjectBase : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomObjectBase()
        {
        }

        #endregion Constructors

        #region Destructor

        ~CustomObjectBase()
        {
            Dispose(false);
        }

        #endregion Destructor

        #region From / To Array serialization

        /// <summary>
        /// Serializes the current instance and returns a byte array.
        /// </summary>
        /// <returns>The current instance serialized as a byte array.</returns>
        public byte[] ToArray()
        {
            byte[] byteArray = null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, this);
                    byteArray = memoryStream.ToArray();
                }
            }
            finally
            {
                binaryFormatter = null;
            }

            return byteArray;
        }

        /// <summary>
        /// Deserializes the byte array and returns an instance of the object.
        /// </summary>
        /// <returns>If the byte array can be deserialized and cast to an instance of the specified object, 
        /// returns an instance of the specified object; otherwise returns null.</returns>
        public static Object FromArray(byte[] byteArray)
        {
            Object o = null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    o = binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                binaryFormatter = null;
            }

            return o;
        }

        #endregion From / To Array serialization

        #region IsDirty

        protected bool _IsDirty = false;

        /// <summary>
        /// Gets a value indicating whether any of the properties of this object is changed.
        /// </summary>
        /// <returns>true if any property is changed; otherwise false.</returns>
        [XmlIgnore]
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
        }

        #endregion IsDirty

        #region IsDeleted

        private bool _IsDeleted = false;

        /// <summary>
        /// Gets or Sets a value whether the object is marked for deletion.
        /// </summary>
        /// <returns>true if the object is marked for deletion; otherwise false.</returns>
        [XmlIgnore]
        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                _IsDeleted = value;
            }
        }

        #endregion IsDeleted

        #region IsNew

        protected bool _IsNew = true;

        /// <summary>
        /// Gets a value indicating whether this object is to be considered new.
        /// </summary>
        /// <returns>true by default</returns>
        [XmlIgnore]
        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                _IsNew = value;
            }
        }

        #endregion IsNew

        #region Compare null string equal to an empty string

        private bool _CompareNullStringAsEmptyString = true;

        /// <summary>
        /// Gets or sets a value indicating whether this object should consider a null string equal to an empty string when doing comparisons. True by default.
        /// </summary>
        [XmlIgnore]
        public bool CompareNullStringAsEmptyString
        {
            get
            {
                return _CompareNullStringAsEmptyString;
            }
            set
            {
                _CompareNullStringAsEmptyString = value;
            }
        }

        /// <summary>
        /// Returns a string ready for comparsion based upon the CompareNullStringAsEmptyString property.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>If CompareNullStringAsEmptyString is true and the value is equal to null returns an empty string otherwise returns the value. If CompareNullStringAsEmptyString is false returns the value.</returns>
        public string GetComparisonString( string value )
        {
            if ( CompareNullStringAsEmptyString )
            {
                if ( value == null )
                {
                    return string.Empty;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        #endregion Compare null string equal to an empty string

        #region Clone

        /// <summary>
        /// Creates a deep copy of the current instance.
        /// </summary>
        /// <returns>New instance of instance.</returns>
        private Object deepCopy()
        {
            Object o = new Object();

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    o = binaryFormatter.Deserialize(memoryStream);
                }
            }
            finally
            {
                binaryFormatter = null;
            }

            return o;
        }

        /// <summary>
        /// Creates a copy of the current instance.
        /// </summary>
        /// <returns>New instance of current instance.</returns>
        public Object Clone()
        {
            return deepCopy();
        }

        #endregion ICloneable Members

        #region HashCode

        private int _hashcode = Guid.NewGuid().GetHashCode();

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance as an integer.</returns>
        public override int GetHashCode()
        {
            return _hashcode;
        }

        #endregion HashCode

        #region Assist functions
        
        /// <summary>
        /// Calibrates the value based upon the maximum character length specified.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maximumCharacterLength"></param>
        /// <returns>If the length of the value exceeds the maximum character length, then returns the truncated value, otherwise returns the value trimmed.</returns>
        public string CalibrateValue(string value, int maximumCharacterLength)
        {
            value = value.Trim();
            if (value.Length > maximumCharacterLength)
            {
                value = value.Substring(0, maximumCharacterLength);
            }
            
            return value;
        }
        
        #endregion Assist functions

        /// <summary>
        /// This <see cref="IDisposable"/> implementation follows Microsoft coding best practices.
        /// Dispose is also initiated by the destructor of this class.
        /// </summary>

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// Because the Dispose is implemented, this class can be used in a 'using' statement.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // if disposing equals true, dispose all managed and unmanaged resources
                if (disposing)
                {
                }
            }

            disposed = true;
        }

        #endregion Dispose
    }
}