using System;
using System.IO;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApi
{
    [Serializable]
    public class ServiceResponse<T>
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ServiceResponse()
		{
		}

		#endregion Constructors
		
        #region Properties

        private string _Status = "ok";
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        private string _ErrorMessage = null;
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }
        }

        private T _Result = default(T);
        public T Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }

        #endregion Properties

        #region Methods

        public string Serialize(OutputType output)
        {
            if (output == OutputType.Xml)
                return SerializeAsXml();
            else
                return SerializeAsJson();
        }

        private string SerializeAsJson()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            js.MaxJsonLength = 58720256;    // 56 MB of Unicode string data
            return js.Serialize(this);
        }

        private string SerializeAsXml()
        {
            System.Xml.Serialization.XmlRootAttribute xmlRoot = new System.Xml.Serialization.XmlRootAttribute("Response");
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(ServiceResponse<T>), xmlRoot);
            MemoryStream memoryStream = new MemoryStream();
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.UTF8);
            xml.Serialize(writer, this);
            memoryStream = (MemoryStream)writer.BaseStream;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(memoryStream.ToArray());
        }

        #endregion Methods

    }
}
