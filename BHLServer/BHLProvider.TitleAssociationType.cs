using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleAssociationType> TitleAssociationTypeSelectAll()
        {
            return new TitleAssociationTypeDAL().SelectAll(null, null);
        }

    }
}
