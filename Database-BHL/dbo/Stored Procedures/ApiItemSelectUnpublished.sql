SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiItemSelectUnpublished]

AS

SET NOCOUNT ON

-- Get all book that are unpublished and not redirected to another book
SELECT	b.BookID AS ItemID
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	i.ItemStatusID <> 40
AND		b.RedirectBookID IS NULL

UNION

-- Get the books that are unpublished and redirected to other books
SELECT	x.OrigItemID as ItemID
FROM	(
		-- Get the replacement BookID for the unpublished and redirected books.
		-- Look at up to 10 levels of redirection.
		SELECT	i1.BookID AS OrigItemID,
				COALESCE(i10.BookID, i9.BookID, i8.BookID, i7.BookID, i6.BookID,
						i5.BookID, i4.BookID, i3.BookID, i2.BookID) AS ReplacementBookID
		FROM	dbo.Item i 
				INNER JOIN dbo.Book i1 ON i.ItemID = i1.ItemID
				LEFT JOIN dbo.Book i2 ON i1.RedirectBookID = i2.BookID
				LEFT JOIN dbo.Book i3 ON i2.RedirectBookID = i3.BookID
				LEFT JOIN dbo.Book i4 ON i3.RedirectBookID = i4.BookID
				LEFT JOIN dbo.Book i5 ON i4.RedirectBookID = i5.BookID
				LEFT JOIN dbo.Book i6 ON i5.RedirectBookID = i6.BookID
				LEFT JOIN dbo.Book i7 ON i6.RedirectBookID = i7.BookID
				LEFT JOIN dbo.Book i8 ON i7.RedirectBookID = i8.BookID
				LEFT JOIN dbo.Book i9 ON i8.RedirectBookID = i9.BookID
				LEFT JOIN dbo.Book i10 ON i9.RedirectBookID = i10.BookID
		WHERE	i.ItemStatusID <> 40
		AND		i1.RedirectBookID IS NOT NULL
		) x 
		INNER JOIN dbo.Book b ON x.ReplacementBookID = b.BookID
		INNER JOIN dbo.Item itm ON b.ItemID = itm.ItemID
WHERE	itm.ItemStatusID <> 40
ORDER BY ItemID


GO
