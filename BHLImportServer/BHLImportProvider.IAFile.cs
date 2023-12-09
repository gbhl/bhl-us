using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IAFile SaveIAFile(int itemID, string remoteFileName, string source, string format, string original)
        {
            IAFileDAL dal = new IAFileDAL();
            IAFile savedFile = dal.IAFileSelectByItemAndRemoteFileName(null, null, itemID, remoteFileName);

            if (savedFile == null)
            {
                IAFile newFile = new IAFile
                {
                    ItemID = itemID,
                    RemoteFileName = remoteFileName,
                    Source = source,
                    Format = format,
                    Original = original
                };
                savedFile = dal.IAFileInsertAuto(null, null, newFile);
            }
            else
            {
                if ((savedFile.Source != source) || (savedFile.Format != format) || (savedFile.Original != original))
                {
                    savedFile.Source = source;
                    savedFile.Format = format;
                    savedFile.Original = original;
                    dal.IAFileUpdateAuto(null, null, savedFile);
                }
            }

            return savedFile;
        }

        public List<IAFile> IAFileSelectForDownload(int itemID)
        {
            return (new IAFileDAL().IAFileSelectForDownload(null, null, itemID));
        }

        public List<IAFile> IAFileSelectByItem(int itemID)
        {
            return (new IAFileDAL().IAFileSelectByItem(null, null, itemID));
        }

        public IAFile IAFileSelectByItemAndFormat(int itemID, string format)
        {
            return (new IAFileDAL().IAFileSelectByItemAndFormat(null, null, itemID, format));
        }

        public IAFile IAFileSelectByItemAndRemoteFileName(int itemID, string remoteFilename)
        {
            return (new IAFileDAL().IAFileSelectByItemAndRemoteFileName(null, null, itemID, remoteFilename));
        }

        public void IAFileDeleteAuto(int fileID)
        {
            new IAFileDAL().IAFileDeleteAuto(null, null, fileID);
        }

        public IAFile SaveIAFileWithDownloadInfo(int fileID, string localFileName, DateTime remoteFileLastModifiedDate)
        {
            IAFileDAL dal = new IAFileDAL();
            IAFile savedFile = dal.IAFileSelectAuto(null, null, fileID);

            if (savedFile != null)
            {
                if (savedFile.LocalFileName != localFileName || 
                    DateTime.Compare((DateTime)savedFile.RemoteFileLastModifiedDate, remoteFileLastModifiedDate) != 0)
                {
                    savedFile.LocalFileName = localFileName;
                    savedFile.RemoteFileLastModifiedDate = remoteFileLastModifiedDate;
                    savedFile = dal.IAFileUpdateAuto(null, null, savedFile);
                }
            }
            else
            {
                throw new Exception("Could not find existing File record.");
            }

            return savedFile;
        }
    }
}
