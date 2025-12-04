CREATE PROCEDURE dbo.BookSelectHasNonOcrText

@BookID int

AS

BEGIN

SET NOCOUNT ON

-- Count the number of pages in the specified book where the text has a non-OCR source
SELECT	COUNT(*) AS NumNonOcr
FROM	dbo.Book b
		INNER JOIN dbo.ItemPage ip ON b.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID AND p.Active = 1
		LEFT JOIN dbo.PageTextLog l ON p.PageID = l.PageID
WHERE	ISNULL(l.TextSource, 'OCR') <> 'OCR'
AND		b.BookID = @BookID

END
GO
