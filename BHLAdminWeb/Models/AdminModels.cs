using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class InstitutionGroupsModel
    {
        public List<InstitutionGroup> Groups { get; set; }

        public InstitutionGroupsModel ()
        {
            Groups = new BHLProvider().InstitutionGroupSelectAll();
        }
    }

    public class InstitutionGroupModel
    {
        public int? InstitutionGroupID { get; set; }

        public string InstitutionGroupName { get; set; }

        public string InstitutionGroupDescription { get; set; }

        public string[] SelectedInstitutions { get; set; }
        public string[] SelectedGroupInstitutions { get; set; }
        public List<SelectListItem> InstitutionList { get; set; }
        public List<SelectListItem> GroupInstitutionList { get; set; }

        public InstitutionGroupModel()
        {

        }

        public InstitutionGroupModel(int id)
        {
            this.InstitutionGroupID = id;
            InstitutionGroup group = new BHLProvider().GetInstitutionGroup(id);
            if (group != null) {
                this.InstitutionGroupName = group.InstitutionGroupName;
                this.InstitutionGroupDescription = group.InstitutionGroupDescription;
            }
        }

        public void GetInstitutions(int id)
        {
            this.GetGroupInstitutions(id);

            this.InstitutionList = new List<SelectListItem>();
            List<Institution> institutions = new BHLProvider().InstituationSelectAll();
            foreach(Institution institution in institutions)
            {
                var found = false;
                foreach(SelectListItem groupInstitution in this.GroupInstitutionList)
                {
                    if (institution.InstitutionCode == groupInstitution.Value) { found = true; break; }
                }

                if (!found) this.InstitutionList.Add(new SelectListItem { Value = institution.InstitutionCode, Text = institution.InstitutionName });
            }
        }

        private void GetGroupInstitutions(int id)
        {
            this.GroupInstitutionList = new List<SelectListItem>();
            List<InstitutionGroupInstitution> institutions = new BHLProvider().GetInstitutionsForGroup(id);
            foreach (InstitutionGroupInstitution institution in institutions)
            {
                this.GroupInstitutionList.Add(new SelectListItem
                {
                    Value = institution.InstitutionCode,
                    Text = institution.InstitutionName
                });
            }
        }

        public void Save(int userId)
        {
            new BHLProvider().SaveInstitutionGroup(InstitutionGroupID, InstitutionGroupName ?? string.Empty, InstitutionGroupDescription ?? string.Empty, userId);
        }

        public void SaveGroupInstitutions(List<string> institutionCodes)
        {
            if (this.InstitutionGroupID != null)
            {
                new BHLProvider().InstitutionGroupInstitutionsInsert((int)this.InstitutionGroupID, institutionCodes);
            }
        }
    }

    public class InstitutionGroupInstitutionModel
    {
        public int? ID { get; set; }
        public int InstitutionGroupID { get; set; }
        public string InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
    }
}
