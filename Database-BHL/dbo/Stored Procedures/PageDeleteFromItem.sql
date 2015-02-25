CREATE PROCEDURE [dbo].[PageDeleteFromItem]

@Barcode nvarchar(40),
@PageID int,
@NumPagesToDelete int

AS
---------------------------------------------------------------------------------------
-- DELETE the specified number of pages from the specified book, starting with the 
-- specified page.
---------------------------------------------------------------------------------------
BEGIN

DECLARE @ItemID int
DECLARE @SequenceOrder int

-- Get the Item ID for the barcode
SELECT @ItemID = ItemID FROM dbo.Item WHERE Barcode = @Barcode

-- Only proceed if the item was found
IF (@ItemID IS NOT NULL)
BEGIN

	-- Get the sequence order of the first Page to be deleted
	SELECT @SequenceOrder = SequenceOrder FROM dbo.Page WHERE PageID = @PageID AND ItemID = @ItemID

	-- Only proceed with the delete if the specified page was found in the item
	IF (@SequenceOrder IS NOT NULL)
	BEGIN

		-- Deactivate page records
		UPDATE	dbo.Page 
		SET		Active = 0 
		WHERE	SequenceOrder >= @SequenceOrder 
		AND		SequenceOrder < (@SequenceOrder + @NumPagesToDelete)
		AND		ItemID = @ItemID

		-- Realign remaining page records with images by adjusting the sequence order values, 
		-- the ocr pointers, and the image pointers  of the page records
		UPDATE	dbo.Page
		SET		SequenceOrder = SequenceOrder - @NumPagesToDelete,
				FileNamePrefix = @Barcode + '_' + right('000' + convert(varchar(4), (SequenceOrder - @NumPagesToDelete)), 4),
				AltExternalUrl = '/download/' + @Barcode + '/page/n' + convert(varchar(4), (SequenceOrder - @NumPagesToDelete - 1))
		WHERE	ItemID = @ItemID and Active = 1 and SequenceOrder >= (@SequenceOrder + @NumPagesToDelete)

		-- Clear the name records for this book, as they are probably attached to the wrong pages
		exec dbo.NamePageDeleteByItemID @ItemID

	END
	ELSE
	BEGIN
		RAISERROR('Page not found in Item.', 16, 1)
	END

END
ELSE
BEGIN
	RAISERROR('Item not found.', 16, 1)
END

END
