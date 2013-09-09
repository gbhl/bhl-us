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
                TitleAssociation_TitleIdentifier newTitleAssociation_TitleIdentifier = new TitleAssociation_TitleIdentifier();
                newTitleAssociation_TitleIdentifier.ImportStatusID = 10;
                newTitleAssociation_TitleIdentifier.ImportSourceID = importSourceID;
                newTitleAssociation_TitleIdentifier.ImportKey = marcBibID;
                newTitleAssociation_TitleIdentifier.MARCTag = marcTag;
                newTitleAssociation_TitleIdentifier.MARCIndicator2 = marcIndicator2;
                newTitleAssociation_TitleIdentifier.Title = title;
                newTitleAssociation_TitleIdentifier.Section = section;
                newTitleAssociation_TitleIdentifier.Volume = volume;
                newTitleAssociation_TitleIdentifier.Heading = heading;
                newTitleAssociation_TitleIdentifier.Publication = publication;
                newTitleAssociation_TitleIdentifier.Relationship = relationship;
                newTitleAssociation_TitleIdentifier.IdentifierName = identifierName;
                newTitleAssociation_TitleIdentifier.IdentifierValue = identifierValue;
                savedTitleAssociation_TitleIdentifier = dal.TitleAssociation_TitleIdentifierInsertAuto(null, null, newTitleAssociation_TitleIdentifier);
            }

            return savedTitleAssociation_TitleIdentifier;
        }
    }
}
