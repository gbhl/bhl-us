using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public TitleAssociation TitleAssociationSelectAuto(int titleAssociationID)
        {
            return new TitleAssociationDAL().TitleAssociationSelectAuto(null, null, titleAssociationID);
        }

        public List<TitleAssociation> TitleAssociationSelectByTitleId(int titleID, bool active)
        {
            return new TitleAssociationDAL().TitleAssociationSelectByTitleID(null, null, titleID, active);
        }

        public List<TitleAssociation> TitleAssociationSelectByTitleId(int titleID)
        {
            return new TitleAssociationDAL().TitleAssociationSelectByTitleID(null, null, titleID, null);
        }

        public TitleAssociation TitleAssociationSelectExtended(int titleAssociationID)
        {
            return new TitleAssociationDAL().TitleAssociationSelectExtended(null, null, titleAssociationID);
        }

        public List<TitleAssociation> TitleAssociationSelectExtendedForTitle(int titleID)
        {
            return new TitleAssociationDAL().TitleAssociationSelectExtendedForTitle(null, null, titleID);
        }

        public List<TitleAssociationSuspectCharacter> TitleAssociationSelectWithSuspectCharacters(String institutionCode, int maxAge)
        {
            return new TitleAssociationDAL().TitleAssociationSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }
    }
}
