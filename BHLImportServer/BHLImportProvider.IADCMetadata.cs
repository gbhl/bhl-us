using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IADCMetadata SaveIADCMetadata(int itemID, string elementName, string elementValue, string source)
        {
            IADCMetadataDAL dal = new IADCMetadataDAL();
            IADCMetadata savedDCMetadata = dal.IADCMetadataSelectByItemElementNameAndSource(null, null, itemID, elementName, source);

            if (savedDCMetadata == null)
            {
                IADCMetadata newDCMetadata = new IADCMetadata
                {
                    ItemID = itemID,
                    DCElementName = elementName,
                    DCElementValue = elementValue,
                    Source = source
                };
                savedDCMetadata = dal.IADCMetadataInsertAuto(null, null, newDCMetadata);
            }
            else
            {
                if (savedDCMetadata.DCElementValue != elementValue)
                {
                    savedDCMetadata.DCElementValue = elementValue;
                    dal.IADCMetadataUpdateAuto(null, null, savedDCMetadata);
                }
            }

            return savedDCMetadata;
        }

        public IADCMetadata IADCMetadataInsert(int itemID, string elementName, string elementValue, string source)
        {
            return (new IADCMetadataDAL().IADCMetadataInsertAuto(null, null, itemID, elementName, elementValue, source));
        }

        public void IADCMetadataDeleteForItemAndSource(int itemID, string source)
        {
            new IADCMetadataDAL().IADCMetadataDeleteForItemAndSource(null, null, itemID, source);
        }
    }
}
