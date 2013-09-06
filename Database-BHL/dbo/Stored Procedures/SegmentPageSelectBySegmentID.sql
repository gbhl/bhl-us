
CREATE PROCEDURE [dbo].[SegmentPageSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	sp.SegmentPageID,
		sp.PageID,
		sp.SequenceOrder,
		p.SequenceOrder AS PageSequenceOrder,
		dbo.fnPageTypeStringForPage(sp.PageID) AS PageTypes,
		dbo.fnIndicatedPageStringForPage(sp.PageID) AS IndicatedPages
FROM	dbo.SegmentPage sp 
		INNER JOIN dbo.Page p ON sp.PageID = p.PageID
WHERE	sp.SegmentID = @SegmentID
ORDER BY
		sp.SequenceOrder
		
END 


