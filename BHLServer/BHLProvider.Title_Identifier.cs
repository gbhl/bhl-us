using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        /// <summary>
        /// Select all identifiers for a given title
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public List<Title_Identifier> Title_IdentifierSelectByTitleID(int titleID)
        {
            return (new Title_IdentifierDAL().Title_IdentifierSelectByTitleID(null, null, titleID, null));
        }

        /// <summary>
        /// Select only those identifiers for a given title that have been designated for display
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public List<Title_Identifier> Title_IdentifierSelectForDisplayByTitleID(int titleID)
        {
            return (new Title_IdentifierDAL().Title_IdentifierSelectByTitleID(null, null, titleID, 1));
        }
    }
}
