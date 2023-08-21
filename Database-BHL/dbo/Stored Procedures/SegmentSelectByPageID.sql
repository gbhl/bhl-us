CREATE PROCEDURE [dbo].[SegmentSelectByPageID]

@PageID int

AS 

SET NOCOUNT ON

BEGIN

SELECT DISTINCT
		s.SegmentID,
		s.RedirectSegmentID,
		s.ItemID,
		i.ItemStatusID
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	p.PageID = @PageID

END 

GO
