
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemNameFileLog : __ItemNameFileLog
    {
        #region Properties

        private string _ocrFolderShare = string.Empty;
        private string _fileRootFolder = string.Empty;
        private string _barCode = string.Empty;

        public string OcrFolderShare
        {
            get { return _ocrFolderShare; }
            set { _ocrFolderShare = value; }
        }

        public string FileRootFolder
        {
            get { return _fileRootFolder; }
            set { _fileRootFolder = value; }
        }

        public string BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "OCRFolderShare":
                        {
                            OcrFolderShare = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FileRootFolder":
                        {
                            FileRootFolder = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BarCode":
                        {
                            BarCode = Utility.EmptyIfNull(column.Value); ;
                            break;
                        }
                }
            }

            base.SetValues(row);

        }

        #endregion


    }
}
