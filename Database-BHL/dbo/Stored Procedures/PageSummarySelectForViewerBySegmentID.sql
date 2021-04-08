CREATE PROCEDURE dbo.PageSummarySelectForViewerBySegmentID

@SegmentID int

AS 

BEGIN

SELECT	DISTINCT
		ISNULL(v.ExternalBaseURL, '') AS ExternalBaseURL,
		ISNULL(REPLACE(RIGHT(v.ExternalUrl, 9), '.jp2', '.jpg'), '') AS ExternalURL,
        v.BarCode,
		v.PageProgression,
		CONVERT(int, x.SequenceOrder) AS SequenceOrder,
		ISNULL(f.FlickrUrl, '') AS FlickrUrl
FROM	dbo.PageSummarySegmentView v WITH (NOLOCK) INNER JOIN (
				-- Computing alternate sequence order which is ensured to be exactly sequential (no gaps)
				SELECT	p.PageID, 
						ROW_NUMBER() OVER (PARTITION BY s.SegmentID ORDER BY ip.SequenceOrder) AS SequenceOrder
				FROM	dbo.Page p WITH (NOLOCK)
						INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
						INNER JOIN dbo.vwSegment s WITH (NOLOCK) ON ip.ItemID = s.ItemID
				WHERE	SegmentID = @SegmentID
				AND		Active = 1
				) x
		ON v.PageID = x.PageID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON v.PageID = f.PageID
WHERE	v.BookID = @SegmentID
AND		v.Active = 1
ORDER BY
        SequenceOrder

END

GO
