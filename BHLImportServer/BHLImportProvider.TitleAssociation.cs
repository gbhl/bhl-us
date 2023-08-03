using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public TitleAssociation SaveTitleAssociation(int importSourceID, string marcBibID, 
            string marcTag, string marcIndicator2, string title,
            string section, string volume, string heading, string publication, 
            string relationship, bool active)
        {
            TitleAssociationDAL dal = new TitleAssociationDAL();
            TitleAssociation savedTitleAssociation = dal.TitleAssociationSelectByKey(null, null, 
                marcBibID, marcTag, marcIndicator2, title, section, volume, heading, 
                publication, relationship);

            if (savedTitleAssociation == null)
            {
                TitleAssociation newTitleAssociation = new TitleAssociation
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
                    Active = active
                };
                savedTitleAssociation = dal.TitleAssociationInsertAuto(null, null, newTitleAssociation);
            }

            return savedTitleAssociation;
        }
    }
}
