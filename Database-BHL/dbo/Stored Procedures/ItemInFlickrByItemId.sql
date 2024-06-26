SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemInFlickrByItemId]

@ItemId int

AS 

SET NOCOUNT ON

SELECT	b.BookID AS ItemID,
		MAX(CASE WHEN f.PageFlickrID IS NOT NULL THEN 1 ELSE 0 END) AS HasFlickrImages
FROM	dbo.Book b WITH (NOLOCK)
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON b.ItemID = ip.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
		LEFT JOIN dbo.PageFlickr f WITH (NOLOCK) ON p.pageid = f.pageid
WHERE	p.Active = 1
AND		b.BookID = @ItemID
GROUP BY b.BookID


GO
