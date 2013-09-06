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

		public Institution InstitutionSelectByItemID( int itemID )
		{
			return GetInstitutionDalInstance().InstitutionSelectByItemID( null, null, itemID );
		}

        public CustomGenericList<Institution> InstitutionSelectWithPublishedItems(bool onlyMemberLibraries)
        {
            return ( GetInstitutionDalInstance().InstitutionSelectWithPublishedItems(null, null, onlyMemberLibraries) );
        }

        public CustomGenericList<Institution> InstitutionSelectDOIStats(int sortBy)
        {
            return (GetInstitutionDalInstance().InstitutionSelectDOIStats(null, null, sortBy));
        }

		private InstitutionDAL GetInstitutionDalInstance()
		{
			if ( institutionDal == null )
			{
				institutionDal = new InstitutionDAL();
			}

			return institutionDal;
		}

		public Institution InstitutionSelectAuto( string institutionCode )
		{
			return ( new InstitutionDAL().InstitutionSelectAuto( null, null, institutionCode ) );
		}

		public void SaveInstitution( Institution institution )
		{
			new InstitutionDAL().Save( null, null, institution );
		}
	}
}