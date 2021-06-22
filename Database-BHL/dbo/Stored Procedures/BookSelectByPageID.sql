CREATE PROCEDURE dbo.BookSelectByPageID

@PageID int

AS 

SET NOCOUNT ON

BEGIN

SELECT DISTINCT
		b.BookID,
		b.ItemID,
		b.IsVirtual,
		i.ItemStatusID
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	p.PageID = @PageID

END 

GO

