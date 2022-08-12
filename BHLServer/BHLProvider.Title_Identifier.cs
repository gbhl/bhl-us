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

        /// <summary>
        /// Select identifiers that match the specified identifier name and title identifier
        /// </summary>
        /// <param name="identifierName"></param>
        /// <param name="titleID"></param>
        /// <returns>List of Title_Identifier objects</returns>
        public List<Title_Identifier> Title_IdentifierSelectByNameAndID(string identifierName, int titleID)
        {
            return (new Title_IdentifierDAL().Title_IdentifierSelectByNameAndID(null, null, identifierName, titleID));
        }
    }
}
