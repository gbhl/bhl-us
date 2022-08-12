using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
	{
        public void PageFlickrSave(PageFlickr pageFlickr, int userId)
        {
            new PageFlickrDAL().PageFlickrSave(null, null, pageFlickr, userId);
        }

        public List<PageFlickr> PageFlickrSelectAll()
        {
            return new PageFlickrDAL().PageFlickrSelectAll(null, null);
        }

        public PageFlickr PageFlickrSelectByPage(int pageId)
        {
            return new PageFlickrDAL().PageFlickrSelectByPage(null, null, pageId);
        }

        public List<PageFlickr> PageFlickrSelectRandom(int numberToReturn)
        {
            return new PageFlickrDAL().PageFlickrSelectRandom(null, null, numberToReturn);
        }

		public void PageFlickrDelete(List<int> pages, int userId)
		{
			TransactionController transactionController = new TransactionController();
			try
			{
				transactionController.BeginTransaction();

				PageFlickrDAL dal = new PageFlickrDAL();
				foreach (int pageID in pages) dal.PageFlickrDeleteByPageID(transactionController.Connection, transactionController.Transaction, pageID);

				transactionController.CommitTransaction();
			}
			catch
			{
				transactionController.RollbackTransaction();
			}
			finally
			{
				transactionController.Dispose();
			}
		}
	}
}
