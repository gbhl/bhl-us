
#region Using

using System;
using System.Configuration;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PageSummaryView : __PageSummaryView
    {
        #region Properties

        public string OcrTextLocation
        {
            get 
            {
                return String.Format(ConfigurationManager.AppSettings["OCRTextLocation"], this.OCRFolderShare, this.FileRootFolder, this.BarCode, this.FileNamePrefix);
            }
        }

        public string MarcXmlLocation
        {
            get 
            {
                return String.Format(ConfigurationManager.AppSettings["MARCXmlLocation"], this.OCRFolderShare, this.FileRootFolder, this.BarCode); 
            }
        }

        private string _flickrUrl = string.Empty;

        public string FlickrUrl
        {
            get { return _flickrUrl; }
            set { _flickrUrl = value; }
        }

        #endregion Properties

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FlickrUrl":
                        {
                            _flickrUrl = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
