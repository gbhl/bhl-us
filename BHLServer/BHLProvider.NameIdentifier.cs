using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<NameIdentifier> NameIdentifierSelectForResolvedName(string resolvedName)
        {
            return new NameIdentifierDAL().NameIdentifierSelectForResolvedName(null, null, resolvedName);
        }
    }
}
