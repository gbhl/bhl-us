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
                TitleAssociation newTitleAssociation = new TitleAssociation();
                newTitleAssociation.ImportStatusID = 10;
                newTitleAssociation.ImportSourceID = importSourceID;
                newTitleAssociation.ImportKey = marcBibID;
                newTitleAssociation.MARCTag = marcTag;
                newTitleAssociation.MARCIndicator2 = marcIndicator2;
                newTitleAssociation.Title = title;
                newTitleAssociation.Section = section;
                newTitleAssociation.Volume = volume;
                newTitleAssociation.Heading = heading;
                newTitleAssociation.Publication = publication;
                newTitleAssociation.Relationship = relationship;
                newTitleAssociation.Active = active;
                savedTitleAssociation = dal.TitleAssociationInsertAuto(null, null, newTitleAssociation);
            }

            return savedTitleAssociation;
        }
    }
}
