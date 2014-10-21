CREATE PROCEDURE dbo.PageInsertIntoItem

@Barcode nvarchar(40),
@PageID int,
@NumPagesToAdd int

AS
---------------------------------------------------------------------------------------
-- INSERT the specified number of pages before the specified page in the specified item.
-- Specify PageID 0 to insert at the end.
---------------------------------------------------------------------------------------
BEGIN

DECLARE @ItemID int
DECLARE @SequenceOrder int

-- Get the Item ID for the barcode
SELECT @ItemID = i.ItemID FROM dbo.Item i WHERE Barcode = @Barcode

-- Only proceed if the item was found
IF (@ItemID IS NOT NULL)
BEGIN

	-- Get the sequence order of the Pages to be 'moved' to make room for inserted Pages
	SELECT @SequenceOrder = SequenceOrder FROM dbo.Page WHERE PageID = @PageID AND ItemID = @ItemID

	-- Only proceed with the insert if the specified page was found in the item, or an 
	-- insert-at-end was specified (PageID = 0)
	IF (@SequenceOrder IS NOT NULL OR @PageID = 0)
	BEGIN

		-- Insert at the end
		IF (@PageID = 0) SELECT @SequenceOrder = MAX(SequenceOrder) + 1 FROM dbo.Page WHERE Active = 1 AND ItemID = @ItemID

		-- Realign page records with images by adjusting the sequence order values, 
		-- the ocr pointers, and the image pointers  of the page records
		UPDATE	dbo.Page
		SET		SequenceOrder = SequenceOrder + @NumPagesToAdd,
				FileNamePrefix = @Barcode + '_' + right('000' + convert(varchar(4), (SequenceOrder + @NumPagesToAdd)), 4),
				AltExternalUrl = '/download/' + @Barcode + '/page/n' + convert(varchar(4), (SequenceOrder + (@NumPagesToAdd - 1)))
		WHERE	ItemID = @ItemID and Active = 1 and SequenceOrder >= @SequenceOrder

		-- Add new page records for an item
		DECLARE @Count int
		SET @Count = 0
		WHILE (@Count < @NumPagesToAdd)
		BEGIN
			INSERT	dbo.Page 
					(ItemID, 
					FileNamePrefix, 
					SequenceOrder, Illustration, CreationUserID, LastModifiedUserID, Active, 
					AltExternalUrl)
			VALUES	(@ItemID, 
					@Barcode + '_' + RIGHT('0000' + CONVERT(nvarchar(4), @SequenceOrder + @Count), 4), 
					@SequenceOrder + @Count, 0, 1, 1, 1, 
					'/download/' + @Barcode + '/page/n' + CONVERT(nvarchar(4), @SequenceOrder + @Count - 1))

			SET @Count = @Count + 1
		END

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
