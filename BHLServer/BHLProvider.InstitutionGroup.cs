using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        private InstitutionGroupDAL _institutionGroupDAL = null;
        private InstitutionGroupInstitutionDAL _institutionGroupInstitutionDAL = null;

        private InstitutionGroupDAL GetInstitutionGroupDalInstance()
        {
            _institutionGroupDAL =_institutionGroupDAL ?? new InstitutionGroupDAL();
            return _institutionGroupDAL;
        }

        private InstitutionGroupInstitutionDAL GetInstitutionGroupInstitutionDalInstance()
        {
            _institutionGroupInstitutionDAL = _institutionGroupInstitutionDAL ?? new InstitutionGroupInstitutionDAL();
            return _institutionGroupInstitutionDAL;
        }


        public List<InstitutionGroup> InstitutionGroupSelectAll()
        {
            return GetInstitutionGroupDalInstance().InstitutionGroupSelectAll(null, null);
        }

        public void DeleteInstitutionGroup(int institutionGroupID)
        {
            GetInstitutionGroupDalInstance().InstitutionGroupDelete(null, null, institutionGroupID);
        }

        public void SaveInstitutionGroup(int? institutionGroupID, string institutionGroupName, string institutionGroupDescription, int? userID)
        {
            if (institutionGroupID == null || institutionGroupID == 0)
            {
                GetInstitutionGroupDalInstance().InstitutionGroupInsertAuto(null, null, institutionGroupName, institutionGroupDescription, userID, userID);
            }
            else
            {
                GetInstitutionGroupDalInstance().InstitutionGroupUpdateAuto(null, null, (int)institutionGroupID, institutionGroupName, institutionGroupDescription, userID);
            }
        }

        public InstitutionGroup GetInstitutionGroup(int institutionGroupId)
        {
            return GetInstitutionGroupDalInstance().InstitutionGroupSelectAuto(null, null, institutionGroupId);
        }

        public List<InstitutionGroupInstitution> GetInstitutionsForGroup(int institutionGroupID)
        {
            return GetInstitutionGroupDalInstance().InstitutionGroupSelectInstitutions(null, null, institutionGroupID);
        }

        public void InstitutionGroupInstitutionsInsert(int institutionGroupID, List<string> institutionCodes)
        {
            GetInstitutionGroupInstitutionDalInstance().InstitutionGroupInstitutionsInsert(null, null, institutionGroupID, institutionCodes);
        }
    }
}
