CREATE PROCEDURE dbo.SegmentSelectHasNonOcrText

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

-- Count the number of pages in the specified segment where the text has a non-OCR source
SELECT	COUNT(*) AS NumNonOcr
FROM	dbo.Segment s
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID AND p.Active = 1
		LEFT JOIN dbo.PageTextLog l ON p.PageID = l.PageID
WHERE	ISNULL(l.TextSource, 'OCR') <> 'OCR'
AND		s.SegmentID = @SegmentID

END
GO
