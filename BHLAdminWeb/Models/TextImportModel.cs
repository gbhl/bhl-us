using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    [Serializable]
    public class TextImportModel
    {
        #region Properties

        private string _itemID = string.Empty;

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private string _fileName = string.Empty;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _filePath = string.Empty;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private string _fileFormat = string.Empty;

        public string FileFormat
        {
            get { return _fileFormat; }
            set { _fileFormat = value; }
        }

        private string _fileFormatName = string.Empty;

        public string FileFormatName
        {
            get { return _fileFormatName; }
            set { _fileFormatName = value; }
        }

        #endregion Properties
    }
}