CREATE PROCEDURE [dbo].[ItemInFlickrByItemId]

@ItemId int

AS 

SET NOCOUNT ON

SELECT	i.ItemID,
		MAX(CASE WHEN f.PageFlickrID IS NOT NULL THEN 1 ELSE 0 END) AS HasFlickrImages
FROM	dbo.Item i
		INNER JOIN dbo.Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON p.pageid = f.pageid
WHERE	p.Active = 1
AND		i.ItemID = @ItemID
GROUP BY i.ItemID
