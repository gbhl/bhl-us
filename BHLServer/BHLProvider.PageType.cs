using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		public CustomGenericList<PageType> PageTypeSelectAll()
		{
			return ( new PageTypeDAL().PageTypeSelectAll( null, null ) );
		}

		public PageType PageTypeSelectAuto( int pageTypeID )
		{
			return ( new PageTypeDAL().PageTypeSelectAuto( null, null, pageTypeID ) );
		}

		public void SavePageType( PageType pageType )
		{
			new PageTypeDAL().Save( null, null, pageType );
		}
	}
}
