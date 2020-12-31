using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
	{
		private InstitutionDAL institutionDal = null;

		/// <summary>
		/// Select values from Institution.
		/// </summary>
		/// <returns>Object of type Institution.</returns>
		public List<Institution> InstituationSelectAll()
		{
			return ( GetInstitutionDalInstance().InstitutionSelectAll( null, null ) );
		}

        public Institution InstitutionSelectWithGroups(string institutionCode)
        {
            return GetInstitutionDalInstance().InstitutionSelectWithGroups(null, null, institutionCode);
        }

        public List<Institution> InstitutionSelectByItemID( int itemID )
		{
			return GetInstitutionDalInstance().InstitutionSelectByItemID( null, null, itemID );
		}

        public List<Institution> InstitutionSelectByItemIDAndRole(int itemID, string role)
        {
            return GetInstitutionDalInstance().InstitutionSelectByItemIDAndRole(null, null, itemID, role);
        }

        public List<Institution> InstitutionSelectByTitleID(int titleID)
        {
            return GetInstitutionDalInstance().InstitutionSelectByTitleID(null, null, titleID);
        }

        public List<Institution> InstitutionSelectWithPublishedItems(bool onlyMemberLibraries, string institutionRoleName = null)
        {
            return ( GetInstitutionDalInstance().InstitutionSelectWithPublishedItems(null, null, onlyMemberLibraries, institutionRoleName) );
        }

        public List<Institution> InstitutionSelectWithPublishedSegments(bool onlyMemberLibraries, string institutionRoleName = null)
        {
            return (GetInstitutionDalInstance().InstitutionSelectWithPublishedSegments(null, null, onlyMemberLibraries, institutionRoleName));
        }

        public List<Institution> InstitutionSelectDOIStats(int sortBy, int bhlOnly)
        {
            return (GetInstitutionDalInstance().InstitutionSelectDOIStats(null, null, sortBy, bhlOnly));
        }

		private InstitutionDAL GetInstitutionDalInstance()
		{
			if ( institutionDal == null )
			{
				institutionDal = new InstitutionDAL();
			}

			return institutionDal;
		}

        public List<Institution> ItemHoldingInstitutionSelectByItemID(int itemID)
        {
            return new InstitutionDAL().InstitutionSelectByItemIDAndRole(null, null, itemID, InstitutionRole.HoldingInstitution);
        }

        public List<Institution> TitleHoldingInstitutionSelectByTitleID(int titleID)
        {
            return new InstitutionDAL().InstitutionSelectByTitleIDAndRole(null, null, titleID, InstitutionRole.HoldingInstitution);
        }

        public Institution InstitutionSelectAuto( string institutionCode )
		{
			return ( new InstitutionDAL().InstitutionSelectAuto( null, null, institutionCode ) );
		}

		public void SaveInstitution( Institution institution, int userID )
		{
			new InstitutionDAL().Save( null, null, institution, userID );
		}

        public InstitutionRole InstitutionRoleSelectAuto(int institutionRoleID)
        {
            return new InstitutionRoleDAL().InstitutionRoleSelectAuto(null, null, institutionRoleID);
        }

        public List<InstitutionRole> InstitutionRoleSelectAll()
        {
            return (GetInstitutionDalInstance().InstitutionRoleSelectAll(null, null));
        }


    }
}