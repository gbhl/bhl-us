using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public TitleAssociation_TitleIdentifier SaveTitleAssociation_TitleIdentifier(int importSourceID, string marcBibID,
            string marcTag, string marcIndicator2, string title,
            string section, string volume, string heading, string publication, string relationship,
            string identifierName, string identifierValue)
        {
            TitleAssociation_TitleIdentifierDAL dal = new TitleAssociation_TitleIdentifierDAL();
            TitleAssociation_TitleIdentifier savedTitleAssociation_TitleIdentifier = dal.TitleAssociation_TitleIdentifierSelectByKey(null, null, 
                marcBibID, marcTag, marcIndicator2, title, section, volume, heading, publication, relationship, identifierName, 
                identifierValue);

            if (savedTitleAssociation_TitleIdentifier == null)
            {
                TitleAssociation_TitleIdentifier newTitleAssociation_TitleIdentifier = new TitleAssociation_TitleIdentifier
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    ImportKey = marcBibID,
                    MARCTag = marcTag,
                    MARCIndicator2 = marcIndicator2,
                    Title = title,
                    Section = section,
                    Volume = volume,
                    Heading = heading,
                    Publication = publication,
                    Relationship = relationship,
                    IdentifierName = identifierName,
                    IdentifierValue = identifierValue
                };
                savedTitleAssociation_TitleIdentifier = dal.TitleAssociation_TitleIdentifierInsertAuto(null, null, newTitleAssociation_TitleIdentifier);
            }

            return savedTitleAssociation_TitleIdentifier;
        }
    }
}
