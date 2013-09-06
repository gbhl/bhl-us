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

        public CustomGenericList<CustomDataRow> KeywordSelectCountByInstitution(int numberToReturn, string institutionCode,
            string languageCode)
        {
            return new KeywordDAL().KeywordSelectCountByInstitution(null, null, numberToReturn, institutionCode, languageCode);
        }
    }
}
