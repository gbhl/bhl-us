CREATE PROCEDURE [dbo].[PageSummarySelectForViewerByItemID]

@ItemID int

AS 

BEGIN

SELECT	DISTINCT
		ISNULL(v.ExternalBaseURL, '') AS ExternalBaseURL,
		ISNULL(REPLACE(RIGHT(v.AltExternalUrl, 9), '.jp2', '.jpg'), '') AS AltExternalURL,
        v.BarCode,
		CONVERT(int, x.SequenceOrder) AS SequenceOrder,
		ISNULL(f.FlickrUrl, '') AS FlickrUrl
FROM	dbo.PageSummaryView v WITH (NOLOCK) INNER JOIN (
				-- Computing alternate sequence order which is ensured to be exactly sequential (no gaps)
				SELECT	PageID, 
						ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY SequenceOrder) AS SequenceOrder
				FROM	Page WITH (NOLOCK)
				WHERE	ItemID = @ItemID
				AND		Active = 1
				) x
		ON v.PageID = x.PageID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON v.PageID = f.PageID
WHERE	v.ItemID = @ItemID
AND		v.Active = 1
ORDER BY
        SequenceOrder

END
