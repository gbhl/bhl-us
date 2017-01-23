CREATE PROCEDURE [dbo].[ItemInFlickrByTitleId]

@TitleId int

AS 

SET NOCOUNT ON

SELECT	i.ItemID,
		MAX(CASE WHEN f.PageFlickrID IS NOT NULL THEN 1 ELSE 0 END) AS HasFlickrImages
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON p.pageid = f.pageid
WHERE	p.Active = 1
AND		t.TitleID = @TitleID
GROUP BY i.ItemID
