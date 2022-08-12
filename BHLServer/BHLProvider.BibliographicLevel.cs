using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<BibliographicLevel> BibliographicLevelSelectAll()
        {
            return new BibliographicLevelDAL().BibliographicLevelSelectAll(null, null);
        }

        public BibliographicLevel BibliographicLevelSelect(int bibliographicLevelId)
        {
            return new BibliographicLevelDAL().BibliographicLevelSelectAuto(null, null, bibliographicLevelId);
        }
    }
}
