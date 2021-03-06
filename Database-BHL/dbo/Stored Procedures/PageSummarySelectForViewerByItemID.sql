SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSummarySelectForViewerByItemID]

@ItemID int

AS 

BEGIN

SELECT	DISTINCT
		ISNULL(v.ExternalBaseURL, '') AS ExternalBaseURL,
		ISNULL(REPLACE(RIGHT(v.ExternalUrl, 9), '.jp2', '.jpg'), '') AS ExternalURL,
        v.BarCode,
		v.PageProgression,
		CONVERT(int, x.SequenceOrder) AS SequenceOrder,
		ISNULL(f.FlickrUrl, '') AS FlickrUrl
FROM	dbo.PageSummaryView v WITH (NOLOCK) INNER JOIN (
				-- Computing alternate sequence order which is ensured to be exactly sequential (no gaps)
				SELECT	p.PageID, 
						ROW_NUMBER() OVER (PARTITION BY b.BookID ORDER BY ip.SequenceOrder) AS SequenceOrder
				FROM	dbo.Page p WITH (NOLOCK)
						INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
						INNER JOIN dbo.Book b WITH (NOLOCK) ON ip.ItemID = b.ItemID
				WHERE	BookID = @ItemID
				AND		Active = 1
				) x
		ON v.PageID = x.PageID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON v.PageID = f.PageID
WHERE	v.BookID = @ItemID
AND		v.Active = 1
ORDER BY
        SequenceOrder

END

GO
