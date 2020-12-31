using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<KeywordSuspectCharacter> KeywordSelectWithSuspectCharacters(string institutionCode, int maxAge)
        {
            return new KeywordDAL().KeywordSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public List<CustomDataRow> KeywordSelectNewLocations()
        {
            return new KeywordDAL().KeywordSelectNewLocations(null, null);
        }
    }
}
