using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MOBOT.BHL.Web2.controls
{
    public partial class ContributorListControl : System.Web.UI.UserControl
    {
        BHLProvider provider = new BHLProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Institution> allInstitutions = null;

                String cacheKey = "contributorBrowse";
                if (Cache[cacheKey] != null)
                {
                    // Use cached version
                    allInstitutions = (List<Institution>)Cache[cacheKey];
                }
                else
                {
                    // Refresh cache

                    // Get the list of all institutions
                    allInstitutions = GetInstitutions();

                    // Cache the list
                    Cache.Add(cacheKey, allInstitutions, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                litNumContributors.Text = allInstitutions.Count.ToString();

                // Separate the institutions into separate lists of BHL members and non-members
                List<Institution> members = new List<Institution>();
                List<Institution> nonMembers = new List<Institution>();
                foreach (Institution institution in allInstitutions)
                {
                    institution.InstitutionName = institution.InstitutionName.Replace("(archive.org)", "").Trim();

                    if (institution.BHLMemberLibrary)
                        members.Add(institution);
                    else
                        nonMembers.Add(institution);
                }

                // Display the institution lists
                dlMembers.DataSource = members;
                dlMembers.DataBind();
                dlNonMembers.DataSource = nonMembers;
                dlNonMembers.DataBind();
            }
        }

        /// <summary>
        /// Get a list of all institutions that have contributed either books or segments
        /// </summary>
        /// <returns></returns>
        private List<Institution> GetInstitutions()
        {
            List<Institution> institutions = new List<Institution>();
            List<Institution> contributors = provider.InstitutionSelectWithPublishedItems(false, "Holding Institution");
            List<Institution> contributorsWithSegments = provider.InstitutionSelectWithPublishedSegments(false, "Contributor");
            List<Institution> rightsHolders = provider.InstitutionSelectWithPublishedItems(false, "Rights Holder");
            List<Institution> rightsHoldersWithSegments = provider.InstitutionSelectWithPublishedSegments(false, "Rights Holder");

            BuildInstitutionList(contributors, institutions);
            BuildInstitutionList(contributorsWithSegments, institutions);
            BuildInstitutionList(rightsHolders, institutions);
            BuildInstitutionList(rightsHoldersWithSegments, institutions);

            InstitutionNameComparer comp = new InstitutionNameComparer();
            institutions.Sort(comp);

            return institutions;
        }

        private void BuildInstitutionList(List<Institution> source, List<Institution> target)
        {
            foreach(Institution newInstitution in source)
            {
                bool exists = false;
                foreach(Institution institution in target)
                {
                    if (institution.InstitutionCode == newInstitution.InstitutionCode) { exists = true; break; }
                }
                if (!exists) target.Add(newInstitution);
            }
        }
    }
}