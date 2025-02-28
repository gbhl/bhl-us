CREATE PROCEDURE [dbo].[ExportSegmentPage]

AS

BEGIN

SET NOCOUNT ON

SELECT 	s.SegmentID,
		ip.PageID,
		s.BookID,
		ip.SequenceOrder,
		CONVERT(NVARCHAR(16), ip.CreationDate, 120) AS CreationDate,
		c.HasLocalContent,
		c.HasExternalContent
FROM	dbo.vwSegment s
		INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	(i.ItemStatusID = 40 OR i.ItemStatusID IS NULL)
AND     (s.BookID IS NOT NULL OR ISNULL(s.Url, '') <> '')
ORDER BY
		s.SegmentID,
		ip.SequenceOrder

END

GO
