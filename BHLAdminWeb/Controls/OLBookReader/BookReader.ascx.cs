using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Configuration;
using System.Web.UI;

namespace MOBOT.BHL.AdminWeb.Controls.OLBookReader
{
    public partial class BookReader : System.Web.UI.UserControl
    {
        private string _objectType = "Book";
        public string ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        private int? _objectID = null;
        public int? ObjectID
        {
            get { return _objectID; }
            set { _objectID = value; }
        }

        private int? _numPages = null;
        public int? NumPages
        {
            get { return _numPages; }
            set { _numPages = value; }
        }

        private int? _startPage = null;
        public int? StartPage
        {
            get { return _startPage; }
            set { _startPage = value; }
        }

        private int _fixedImageWidth = 0;
        public int FixedImageWidth
        {
            get { return _fixedImageWidth; }
            set { _fixedImageWidth = value; }
        }

        private int _fixedImageHeight = 0;
        public int FixedImageHeight
        {
            get { return _fixedImageHeight; }
            set { _fixedImageHeight = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Add references to the javascript and CSS needed by the book reader
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "/js/jquery.easing.1.3.js");
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "/Controls/OLBookReader/BookReader/BookReader.js?v=1.4bhl");
            ControlGenerator.AddLinkControl(Page.Master.Page.Header.Controls, "/Controls/OLBookReader/BookReader/BookReader.css?v=1.3bhl");
        }

    }
}