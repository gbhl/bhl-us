using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public TitleTag SaveTitleTag(int importSourceID, string marcBibID,
            string tagText, string marcDataFieldTag, string marcSubFieldCode, 
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate)
        {
            TitleTagDAL dal = new TitleTagDAL();
            TitleTag savedTitleTag = dal.TitleTagSelectNewByKeyTagTextAndSource(null, null, marcBibID, tagText, importSourceID);

            if (savedTitleTag == null)
            {
                TitleTag newTitleTag = new TitleTag
                {
                    ImportKey = marcBibID,
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    TagText = tagText,
                    MarcDataFieldTag = marcDataFieldTag,
                    MarcSubFieldCode = marcSubFieldCode,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate
                };
                savedTitleTag = dal.TitleTagInsertAuto(null, null, newTitleTag);
            }
            else
            {
                if (savedTitleTag.MarcDataFieldTag != marcDataFieldTag ||
                    savedTitleTag.MarcSubFieldCode != marcSubFieldCode)
                {
                    savedTitleTag.MarcDataFieldTag = marcDataFieldTag;
                    savedTitleTag.MarcSubFieldCode = marcSubFieldCode;
                    savedTitleTag.ExternalCreationDate = externalCreationDate;
                    savedTitleTag.ExternalLastModifiedDate = externalLastModifiedDate;

                    dal.TitleTagUpdateAuto(null, null, savedTitleTag);
                }
            }

            return savedTitleTag;
        }
    }
}
