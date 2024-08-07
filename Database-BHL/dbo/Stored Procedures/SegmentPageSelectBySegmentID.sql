SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentPageSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	sp.ItemPageID,
		sp.ItemID,
		sp.PageID,
		sp.SequenceOrder,
		bp.SequenceOrder AS PageSequenceOrder,
		dbo.fnPageTypeStringForPage(sp.PageID) AS PageTypes,
		dbo.fnIndicatedPageStringForPage(sp.PageID) AS IndicatedPages
FROM	dbo.Segment s
		LEFT JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		LEFT JOIN dbo.ItemPage bp ON r.ParentID = bp.ItemID
		INNER JOIN dbo.ItemPage sp ON s.ItemID = sp.ItemID
		INNER JOIN dbo.Page p ON sp.PageID = p.PageID
WHERE	s.SegmentID = @SegmentID
AND		(bp.PageID = sp.PageID OR bp.PageID IS NULL)
AND		p.Active = 1
ORDER BY
		sp.SequenceOrder
		
END 

GO
