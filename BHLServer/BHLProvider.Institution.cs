using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		private InstitutionDAL institutionDal = null;

		/// <summary>
		/// Select values from Institution.
		/// </summary>
		/// <returns>Object of type Institution.</returns>
		public CustomGenericList<Institution> InstituationSelectAll()
		{
			return ( GetInstitutionDalInstance().InstitutionSelectAll( null, null ) );
		}

		public CustomGenericList<Institution> InstitutionSelectByItemID( int itemID )
		{
			return GetInstitutionDalInstance().InstitutionSelectByItemID( null, null, itemID );
		}

        public CustomGenericList<Institution> InstitutionSelectByItemIDAndRole(int itemID, string role)
        {
            return GetInstitutionDalInstance().InstitutionSelectByItemIDAndRole(null, null, itemID, role);
        }

        public CustomGenericList<Institution> InstitutionSelectWithPublishedItems(bool onlyMemberLibraries, string institutionRoleName = null)
        {
            return ( GetInstitutionDalInstance().InstitutionSelectWithPublishedItems(null, null, onlyMemberLibraries, institutionRoleName) );
        }

        public CustomGenericList<Institution> InstitutionSelectWithPublishedSegments(bool onlyMemberLibraries, string institutionRoleName = null)
        {
            return (GetInstitutionDalInstance().InstitutionSelectWithPublishedSegments(null, null, onlyMemberLibraries, institutionRoleName));
        }

        public CustomGenericList<Institution> InstitutionSelectDOIStats(int sortBy, int bhlOnly)
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

        public CustomGenericList<Institution> ItemContributorSelectByItemID(int itemID)
        {
            return new InstitutionDAL().InstitutionSelectByItemIDAndRole(null, null, itemID, InstitutionRole.Contributor);
        }

        public CustomGenericList<Institution> TitleContributorSelectByTitleID(int titleID)
        {
            return new InstitutionDAL().InstitutionSelectByTitleIDAndRole(null, null, titleID, InstitutionRole.Contributor);
        }

        public Institution InstitutionSelectAuto( string institutionCode )
		{
			return ( new InstitutionDAL().InstitutionSelectAuto( null, null, institutionCode ) );
		}

		public void SaveInstitution( Institution institution, int userID )
		{
			new InstitutionDAL().Save( null, null, institution, userID );
		}
	}
}