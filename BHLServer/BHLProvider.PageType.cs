using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		public List<PageType> PageTypeSelectAll()
		{
			return ( new PageTypeDAL().PageTypeSelectAll( null, null ) );
		}

		public List<PageType> PageTypeSelectActive()
		{
			List<PageType> types = new PageTypeDAL().PageTypeSelectAll(null, null);
			return types.Where(t => t.Active == 1).ToList();
		}

		public PageType PageTypeSelectAuto( int pageTypeID )
		{
			return ( new PageTypeDAL().PageTypeSelectAuto( null, null, pageTypeID ) );
		}

		public void SavePageType( PageType pageType, int userId )
		{
			new PageTypeDAL().Save( null, null, pageType, userId );
		}
	}
}
