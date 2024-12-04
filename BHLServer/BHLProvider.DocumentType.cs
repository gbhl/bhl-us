using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<DocumentType> DocumentTypeSelectAll()
        {
            return new DocumentTypeDAL().DocumentTypeSelectAll(null, null);
        }
    }
}
