SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemInFlickrByTitleId]

@TitleId int

AS 

SET NOCOUNT ON

SELECT	b.ItemID,
		MAX(CASE WHEN f.PageFlickrID IS NOT NULL THEN 1 ELSE 0 END) AS HasFlickrImages
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON b.ItemID = ip.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON p.pageid = f.pageid
WHERE	p.Active = 1
AND		t.TitleID = @TitleID
GROUP BY b.ItemID


GO
