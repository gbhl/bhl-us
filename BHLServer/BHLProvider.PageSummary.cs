using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
  public partial class BHLProvider
  {
      /// <summary>
      /// Select values from PageSummaryView by Item identifier.
      /// </summary>
      /// <param name="itemId"></param>
      /// <param name="showPrimaryTitle">Returns details of primary title if true, picks any related title if false.</param>
      /// <returns></returns>
    public PageSummaryView PageSummarySelectByItemId( int itemId, bool showPrimaryTitle )
    {
        CustomGenericList<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByItemId( null, null, itemId );

        if (list != null)
        {
            if (showPrimaryTitle)
            {
                // Return the first row that includes the details of the primary title
                foreach (PageSummaryView ps in list)
                {
                    if (ps.TitleID == ps.PrimaryTitleID) return ps;
                }
            }
            // Just return the first row
            return list[0];
        }
        else
        {
            return null;
        }
    }

    public PageSummaryView PageSummarySelectByItemIdAndTitleId(int itemId, int titleId)
    {
        return (new PageSummaryDAL().PageSummarySelectByItemIdAndTitleId(null, null, itemId, titleId));
    }

    public CustomGenericList<PageSummaryView> PageSummarySelectForViewerByItemID(int itemId)
    {
        return (new PageSummaryDAL().PageSummarySelectForViewerByItemID(null, null, itemId));
    }

    /// <summary>
    /// Select values from PageSummaryView by Barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="showPrimaryTitle">Returns details of primary title if true, picks any related title if false.</param>
    /// <returns>Object of type Title.</returns>
    public PageSummaryView PageSummarySelectByBarcode( string barcode, bool showPrimaryTitle )
    {
        CustomGenericList<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByBarcode( null, null, barcode );

        if (list != null)
        {
            if (showPrimaryTitle)
            {
                // Return the first row that includes the details of the primary title
                foreach (PageSummaryView ps in list)
                {
                    if (ps.TitleID == ps.PrimaryTitleID) return ps;
                }
            }
            // Just return the first row
            return list[0];
        }
        else
        {
            return null;
        }
    }

    public PageSummaryView PageSummarySelectByTitleId(int titleId)
    {
        return (new PageSummaryDAL().PageSummarySelectByTitleId(null, null, titleId));
    }

    /// <summary>
    /// Select values from PageSummaryView by MARC Bib Id.
    /// </summary>
    /// <param name="bibID"></param>
    /// <returns>Object of type PageSummaryView.</returns>
    public PageSummaryView PageSummarySelectByBibID(string bibID)
    {
        return (new PageSummaryDAL().PageSummarySelectByBibId(null, null, bibID));
    }

    /// <summary>
    /// Select values from PageSummaryView by Page Id.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>Object of type PageSummaryView.</returns>
    public PageSummaryView PageSummarySelectByPageId( int pageId )
    {
        return this.PageSummarySelectByPageId(pageId, false);
    }

    public PageSummaryView PageSummarySelectByPageId(int pageId, bool showPrimaryTitle)
    {
        CustomGenericList<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByPageId(null, null, pageId);

        if (showPrimaryTitle && list != null)
        {
            // Return the first row that includes the details of the primary title
            foreach (PageSummaryView ps in list)
            {
                if (ps.TitleID == ps.PrimaryTitleID) return ps;
            }
        }
        // Just return the first row
        PageSummaryView psRow = null;
        if (list != null) psRow = list[0];
        return psRow;
    }

    /// <summary>
    /// Select values from PageSummaryView by Item and Sequence.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="sequence"></param>
    /// <returns>Object of type Title.</returns>
    public PageSummaryView PageSummarySelectByItemAndSequence( int itemID, int sequence )
    {
      return ( new PageSummaryDAL().PageSummarySelectByItemAndSequence( null, null, itemID, sequence ) );
    }

    public CustomGenericList<PageSummaryView> PageSummarySelectFoldersForTitleID(int titleID)
    {
        return (new PageSummaryDAL().PageSummarySelectFoldersForTitleID(null, null, titleID));
    }

    public CustomGenericList<PageSummaryView> PageSummarySelectBarcodeForTitleID(int titleID)
    {
        return (new PageSummaryDAL().PageSummarySelectBarcodeForTitleID(null, null, titleID));
    }
  }
}
