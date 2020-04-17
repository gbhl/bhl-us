using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class AdminService
    {
        public void DeleteInstitutionGroup(int id)
        {
            new BHLProvider().DeleteInstitutionGroup(id);
        }
    }
}