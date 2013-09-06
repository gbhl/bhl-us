using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleKeyword> TitleKeywordSelectByTitleID(int titleID)
        {
            return new TitleKeywordDAL().TitleKeywordSelectByTitleID(null, null, titleID);
        }

        public CustomGenericList<TitleKeyword> TitleKeywordSelectLikeTag(string keyword, string languageCode, int returnCount)
        {
            return new TitleKeywordDAL().TitleKeywordSelectLikeTag(null, null, keyword, languageCode, returnCount);
        }

        public CustomGenericList<TitleKeyword> TitleKeywordSelectKeywordByTitle(int titleID)
        {
            return new TitleKeywordDAL().TitleKeywordSelectKeywordByTitle(null, null, titleID);
        }
    }
}
