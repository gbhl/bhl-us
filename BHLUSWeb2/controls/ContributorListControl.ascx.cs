using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2.controls
{
    public partial class ContributorListControl : System.Web.UI.UserControl
    {
        BHLProvider provider = new BHLProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomGenericList<Institution> allInstitutions = null;

                String cacheKey = "contributorBrowse";
                if (Cache[cacheKey] != null)
                {
                    // Use cached version
                    allInstitutions = (CustomGenericList<Institution>)Cache[cacheKey];
                }
                else
                {
                    // Refresh cache

                    // Get the list of all institutions
                    allInstitutions = provider.InstitutionSelectWithPublishedItems(false);

                    // Cache the list
                    Cache.Add(cacheKey, allInstitutions, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                litNumContributors.Text = allInstitutions.Count.ToString();

                // Separate the institutions into separate lists of BHL members and non-members
                CustomGenericList<Institution> members = new CustomGenericList<Institution>();
                CustomGenericList<Institution> nonMembers = new CustomGenericList<Institution>();
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
    }
}