using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleKeyword> TitleKeywordSelectByTitleID(int titleID)
        {
            return new TitleKeywordDAL().TitleKeywordSelectByTitleID(null, null, titleID);
        }

        public List<TitleKeyword> TitleKeywordSelectLikeTag(string keyword, string languageCode, int returnCount)
        {
            return new TitleKeywordDAL().TitleKeywordSelectLikeTag(null, null, keyword, languageCode, returnCount);
        }

        public List<TitleKeyword> TitleKeywordSelectKeywordByTitle(int titleID)
        {
            return new TitleKeywordDAL().TitleKeywordSelectKeywordByTitle(null, null, titleID);
        }
    }
}
