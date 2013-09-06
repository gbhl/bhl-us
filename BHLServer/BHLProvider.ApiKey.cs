using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        /// <summary>
        /// Check if a key exists for the specified email address.  If there is an
        /// existing key, return it.  If there is no existing key, create a new
        /// one and return it.
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public APIKey GetApiKey(String contactName, String emailAddress)
        {
            APIKey apiKey = null;

            apiKey = new APIKeyDAL().ApiKeySelectByEmail(null, null, emailAddress);
            if (apiKey == null)
            {
                apiKey = new APIKeyDAL().ApiKeyInsert(null, null, contactName, emailAddress);
            }

            return apiKey;
        }

        public CustomGenericList<APIKey> ApiKeySelectAll()
        {
            return new APIKeyDAL().ApiKeySelectAll(null, null);
        }
    }
}
