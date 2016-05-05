using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<KeywordSuspectCharacter> KeywordSelectWithSuspectCharacters(string institutionCode, int maxAge)
        {
            return new KeywordDAL().KeywordSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public CustomGenericList<CustomDataRow> KeywordSelectNewLocations()
        {
            return new KeywordDAL().KeywordSelectNewLocations(null, null);
        }
    }
}
