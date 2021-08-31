CREATE PROCEDURE [import].[PageSelectBySegmentAndPageNumber]

@SegmentID int,
@PageNumber nvarchar(20)

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID
FROM	dbo.IndicatedPage ipg
		INNER JOIN dbo.Page p ON ipg.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Segment s ON ip.ItemID = s.ItemID
WHERE	s.SegmentID = @SegmentID
AND		p.Active = 1
AND		ipg.PageNumber = @PageNumber
AND		(
		ipg.PagePrefix IN ('', 'Page', 'p.') OR
		ipg.PagePrefix LIKE '% Page'
		)

END

GO
