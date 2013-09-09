using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Title_TitleIdentifier SaveTitleTitleIdentifier(int importSourceID, string marcBibID, 
            string identifierName, string identifierValue,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate)
        {
            Title_TitleIdentifierDAL dal = new Title_TitleIdentifierDAL();
            Title_TitleIdentifier savedTitleTitleIdentifier = dal.Title_TitleIdentifierSelectByKeyNameAndValue(null, null, 
                marcBibID, identifierName, identifierValue);

            if (savedTitleTitleIdentifier == null)
            {
                Title_TitleIdentifier newTitleTitleIdentifier = new Title_TitleIdentifier();
                newTitleTitleIdentifier.ImportStatusID = 10;
                newTitleTitleIdentifier.ImportSourceID = importSourceID;
                newTitleTitleIdentifier.ImportKey = marcBibID;
                newTitleTitleIdentifier.IdentifierName = identifierName;
                newTitleTitleIdentifier.IdentifierValue = identifierValue;
                newTitleTitleIdentifier.ExternalCreationDate = externalCreationDate;
                newTitleTitleIdentifier.ExternalLastModifiedDate = externalLastModifiedDate;
                savedTitleTitleIdentifier = dal.Title_TitleIdentifierInsertAuto(null, null, newTitleTitleIdentifier);
            }

            return savedTitleTitleIdentifier;
        }

    }
}
