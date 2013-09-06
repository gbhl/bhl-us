using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CustomDataAccess
{
    public interface IXmlElement
    {
        XmlElement XmlElement(XmlDocument document);
    }

    public class CustomXmlUtility
    {
        public static XmlElement CreateElement(XmlDocument document, string name)
        {
            XmlElement element = document.CreateElement(name);
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, string value)
        {
            XmlElement element = document.CreateElement(name);
            element.InnerText = value;
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, int value)
        {
            XmlElement element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, int? value)
        {
            XmlElement element = document.CreateElement(name);
            if (value != null)
            {
                element.InnerText = value.ToString();
            }
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, bool value)
        {
            XmlElement element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, DateTime value)
        {
            XmlElement element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }

        public static XmlElement CreateElement(XmlDocument document, string name, DateTime? value)
        {
            XmlElement element = document.CreateElement(name);
            if (value != null)
            {
                element.InnerText = value.ToString();
            }
            return element;
        }
    }
}
