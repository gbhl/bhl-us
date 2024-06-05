CREATE PROCEDURE dbo.ItemNormalizeFileNamePrefix 

@ItemID int

AS

BEGIN

-- For every Page record associated with the specified ItemID, this procedure updates the FileNamePrefix
-- values so that page filenames are sequential starting with 1.  For example, a book with ten pages would
-- have filenameprefix values like the following:
--
--		barcode_0001
--		barcode_0002
--		...
--		barcode_0010
--
-- Should be called after new OCR/Text is ingested via the Admin site library/align (Update Text) function.
-- This function handles situations where the number of text files may have changed, so filenames may change.

-- Is NOT necessary if new text is imported via the Admin site "Import Text" function.  This function matches
-- text to existing page records exactly, so existing filenames are used.
UPDATE	p
SET		FileNamePrefix = b.Barcode + '_' + RIGHT('0000' + CONVERT(nvarchar(4), ip.SequenceOrder), 4)
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	b.ItemID = @ItemID

END

GO
