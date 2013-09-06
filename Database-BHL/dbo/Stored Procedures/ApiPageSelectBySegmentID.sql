
CREATE PROCEDURE [dbo].[ApiPageSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT	sp.PageID,
		sp.SequenceOrder,
		p.[Year],
		p.Series,
		p.Volume,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(sp.PageID) AS PageNumbers,
		dbo.fnPageTypeStringForPage(sp.PageID) AS PageTypeName
FROM	dbo.SegmentPage sp 
		INNER JOIN dbo.Page p ON sp.PageID = p.PageID
WHERE	sp.SegmentID = @SegmentID
ORDER BY
		sp.SequenceOrder ASC


