using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomDataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CustomDataTransporter<T>
    {
        #region Constructors

        public CustomDataTransporter(T load)
        {
            if (load != null)
            {
                _Load = toArray(load);
            }
        }

        public CustomDataTransporter(bool etx)
        {
            _ETX = etx;
        }

        public CustomDataTransporter(T load, int? returnCode, Exception exceptionCaught) : this(load)
        {
            _ReturnCode = returnCode;
            _ExceptionCaught = exceptionCaught;
        }

        #endregion Constructors

        #region Destructor

        ~CustomDataTransporter()
        {
        }

        #endregion Destructor

        #region From / To Array serialization

        /// <summary>
        /// Serializes the current load and returns a byte array.
        /// </summary>
        /// <returns>The current load serialized as a byte array.</returns>
        private byte[] toArray(T load)
        {
            byte[] byteArray = null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, load);
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
        private T fromArray(byte[] byteArray)
        {
            T o = default(T);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    o = (T) binaryFormatter.Deserialize(memoryStream);
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

        #region Properties

        private byte[] _Load = null;

        public T Load
        {
            get
            {
                if (_Load != null)
                {
                    return fromArray(_Load);
                }
                else
                {
                    return default(T);
                }
            }
        }

        private bool _ETX = false;

        public bool ETX
        {
            get
            {
                return _ETX;
            }
        }

        private DateTime _Occurred = DateTime.Now;

        public DateTime Occurred
        {
            get
            {
                return _Occurred;
            }
        }

        private Guid _InternalID = Guid.NewGuid();

        public Guid InternalID
        {
            get
            {
                return _InternalID;
            }
        }

        private Exception _ExceptionCaught = null;

        public Exception ExceptionCaught
        {
            get
            {
                return _ExceptionCaught;
            }
        }

        private int? _ReturnCode = null;

        public int? ReturnCode
        {
            get
            {
                return _ReturnCode;
            }
        }

        #endregion Properties
    }
}