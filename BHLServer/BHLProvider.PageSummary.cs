using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

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
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByItemId( null, null, itemId );

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

    /// <summary>
    /// Return information about all pages in the specified item.
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public List<PageSummaryView> PageSummarySelectAllByItemID( int itemId )
    {
        return new PageSummaryDAL().PageSummarySelectAllByItemID( null, null, itemId );
    }

    public PageSummaryView PageSummarySelectByItemIdAndTitleId(int itemId, int titleId)
    {
        return (new PageSummaryDAL().PageSummarySelectByItemIdAndTitleId(null, null, itemId, titleId));
    }

    public List<PageSummaryView> PageSummarySelectForViewerByItemID(int itemId)
    {
        return (new PageSummaryDAL().PageSummarySelectForViewerByItemID(null, null, itemId));
    }

    public List<PageSummaryView> PageSummarySelectForViewerBySegmentID(int segmentId)
    {
        return (new PageSummaryDAL().PageSummarySelectForViewerBySegmentID(null, null, segmentId));
    }

    /// <summary>
    /// Select values from PageSummaryView by Barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="showPrimaryTitle">Returns details of primary title if true, picks any related title if false.</param>
    /// <returns>Object of type Title.</returns>
    public PageSummaryView PageSummarySelectByBarcode( string barcode, bool showPrimaryTitle )
    {
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByBarcode( null, null, barcode );

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
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByPageId(null, null, pageId);

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

    public PageSummaryView PageSummarySelectByPageId(int pageId, int? titleId)
    {
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySelectByPageId(null, null, pageId);
        if (list != null)
        {
            // Return the row that matches the specified titleId.  If no match, return the primary title.
            PageSummaryView psMatch = null;
            PageSummaryView psPrimary = null;
            foreach(PageSummaryView ps in list)
            {
                if (ps.TitleID == titleId) psMatch = ps;
                if (ps.TitleID == ps.PrimaryTitleID) psPrimary = ps;
            }

            if (psMatch != null) return psMatch;
            if (psPrimary != null) return psPrimary;
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

    public List<PageSummaryView> PageSummarySelectFoldersForTitleID(int titleID)
    {
        return (new PageSummaryDAL().PageSummarySelectFoldersForTitleID(null, null, titleID));
    }

    public List<PageSummaryView> PageSummarySelectBarcodeForTitleID(int titleID)
    {
        return (new PageSummaryDAL().PageSummarySelectBarcodeForTitleID(null, null, titleID));
    }

    public List<PageSummaryView> PageSummarySegmentSelectAllByItemID(int itemID)
    {
        return (new PageSummaryDAL().PageSummarySegmentSelectAllByItemID(null, null, itemID));
    }

    public List<PageSummaryView> PageSummarySegmentSelectBySegmentID(int segmentID)
    {
        return new PageSummaryDAL().PageSummarySegmentSelectBySegmentID(null, null, segmentID);
    }

    public PageSummaryView PageSummarySegmentSelectByPageID(int pageID)
    {
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySegmentSelectByPageID(null, null, pageID);

        // Just return the first row
        PageSummaryView psRow = null;
        if (list != null) psRow = list[0];
        return psRow;
    }

    public PageSummaryView PageSummarySegmentSelectByPageID(int pageId, int? titleId)
    {
        List<PageSummaryView> list = new PageSummaryDAL().PageSummarySegmentSelectByPageID(null, null, pageId);
        if (list != null)
        {
            // Return the row that matches the specified titleId.  If no match, return the primary title.
            PageSummaryView psMatch = null;
            PageSummaryView psPrimary = null;
            foreach (PageSummaryView ps in list)
            {
                if (ps.TitleID == titleId) psMatch = ps;
                if (ps.TitleID == ps.PrimaryTitleID) psPrimary = ps;
            }

            if (psMatch != null) return psMatch;
            if (psPrimary != null) return psPrimary;
        }

        // Just return the first row
        PageSummaryView psRow = null;
        if (list != null) psRow = list[0];
        return psRow;
    }

    public PageSummaryView PageSummarySegmentSelectByItemAndSequence(int itemID, int sequence)
    {
        return new PageSummaryDAL().PageSummarySegmentSelectByItemAndSequence(null, null, itemID, sequence);
    }
    }
}
