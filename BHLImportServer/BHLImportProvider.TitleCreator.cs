using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Title_Creator SaveTitleCreator(int importSourceID, string marcBibID, 
            string creatorName, string marcCreatorA, string marcCreatorB, string marcCreatorC, 
            string marcCreatorD, int creatorRoleTypeID, DateTime? externalCreationDate, 
            DateTime? externalLastModifiedDate,int? externalCreationUser, int? externalLastModifiedUser)
        {
            Title_CreatorDAL dal = new Title_CreatorDAL();
            Title_Creator savedTitleCreator = dal.Title_CreatorSelectNewByKeyCreatorAndSource(null, null, marcBibID,
                creatorName, marcCreatorA, marcCreatorB, marcCreatorC, marcCreatorD, creatorRoleTypeID, 
                importSourceID);

            if (savedTitleCreator == null)
            {
                Title_Creator newTitleCreator = new Title_Creator();
                newTitleCreator.ImportStatusID = 10;
                newTitleCreator.ImportSourceID = importSourceID;
                newTitleCreator.ImportKey = marcBibID;
                newTitleCreator.CreatorName = creatorName;
                newTitleCreator.MARCCreator_a = marcCreatorA;
                newTitleCreator.MARCCreator_b = marcCreatorB;
                newTitleCreator.MARCCreator_c = marcCreatorC;
                newTitleCreator.MARCCreator_d = marcCreatorD;
                newTitleCreator.CreatorRoleTypeID = creatorRoleTypeID;
                newTitleCreator.ExternalCreationDate = externalCreationDate;
                newTitleCreator.ExternalLastModifiedDate = externalLastModifiedDate;
                newTitleCreator.ExternalCreationUser = externalCreationUser;
                newTitleCreator.ExternalLastModifiedUser = externalLastModifiedUser;
                savedTitleCreator = dal.Title_CreatorInsertAuto(null, null, newTitleCreator);
            }

            return savedTitleCreator;
        }
    }
}
