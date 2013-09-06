using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<BibliographicLevel> BibliographicLevelSelectAll()
        {
            return new BibliographicLevelDAL().BibliographicLevelSelectAll(null, null);
        }

        public BibliographicLevel BibliographicLevelSelect(int bibliographicLevelId)
        {
            return new BibliographicLevelDAL().BibliographicLevelSelectAuto(null, null, bibliographicLevelId);
        }
    }
}
