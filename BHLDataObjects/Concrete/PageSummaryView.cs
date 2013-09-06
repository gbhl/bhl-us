
#region Using

using System;
using System.Configuration;

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

        #endregion Properties
    }
}
