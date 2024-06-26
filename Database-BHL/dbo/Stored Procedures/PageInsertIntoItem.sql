SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageInsertIntoItem]

@Barcode nvarchar(200),
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
SELECT @ItemID = ItemID FROM dbo.Book WHERE Barcode = @Barcode
IF (@ItemID IS NULL) SELECT @ItemID = ItemID FROM dbo.Segment WHERE Barcode = @Barcode

-- Only proceed if the item was found
IF (@ItemID IS NOT NULL)
BEGIN
	-- Get the sequence order of the Pages to be 'moved' to make room for inserted Pages
	SELECT	@SequenceOrder = ip.SequenceOrder
	FROM	dbo.Page p
			INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID 
	WHERE	p.PageID = @PageID 
	AND		ip.ItemID = @ItemID

	-- Only proceed with the insert if the specified page was found in the item, or an 
	-- insert-at-end was specified (PageID = 0)
	IF (@SequenceOrder IS NOT NULL OR @PageID = 0)
	BEGIN

		-- Insert at the end
		IF (@PageID = 0) 
		BEGIN
			SELECT	@SequenceOrder = MAX(ip.SequenceOrder) + 1 
			FROM	dbo.Page p
					INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID 
			WHERE	p.Active = 1 
			AND		ip.ItemID = @ItemID
		END

		-- Realign page records with images by adjusting the sequence order values, 
		-- the ocr pointers, and the image pointers of the page records
		UPDATE	p
		SET		FileNamePrefix = @Barcode + '_' + right('000' + convert(varchar(4), (ip.SequenceOrder + @NumPagesToAdd)), 4),
				ExternalUrl = '/download/' + @Barcode + '/page/n' + convert(varchar(4), (ip.SequenceOrder + @NumPagesToAdd - 1))
		FROM	dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		WHERE	ip.ItemID = @ItemID and p.Active = 1 and ip.SequenceOrder >= @SequenceOrder

		UPDATE	ip
		SET		ip.SequenceOrder = ip.SequenceOrder + @NumPagesToAdd
		FROM	dbo.ItemPage ip INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	ip.ItemID = @ItemID and p.Active = 1 and ip.SequenceOrder >= @SequenceOrder

		-- Add new page records for an item
		DECLARE @NewPageID int
		DECLARE @Count int
		SET @Count = 0
		WHILE (@Count < @NumPagesToAdd)
		BEGIN
			INSERT	dbo.Page 
					(
					FileNamePrefix, 
					Illustration, CreationUserID, LastModifiedUserID, Active, 
					ExternalUrl)
			VALUES	(
					@Barcode + '_' + RIGHT('0000' + CONVERT(nvarchar(4), @SequenceOrder + @Count), 4), 
					0, 1, 1, 1, 
					'/download/' + @Barcode + '/page/n' + CONVERT(nvarchar(4), @SequenceOrder + @Count - 1))

			SET @NewPageID = SCOPE_IDENTITY()

			INSERT	dbo.ItemPage
					(
					ItemID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID
					)
			VALUES	(
					@ItemID, @NewPageID, @SequenceOrder + @Count, 1, 1
					)			

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

GO
