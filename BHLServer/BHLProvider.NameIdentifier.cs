using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<NameIdentifier> NameIdentifierSelectForResolvedName(string resolvedName)
        {
            return new NameIdentifierDAL().NameIdentifierSelectForResolvedName(null, null, resolvedName);
        }
    }
}
