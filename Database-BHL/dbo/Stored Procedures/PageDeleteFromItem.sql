SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageDeleteFromItem]

@Barcode nvarchar(200),
@PageID int,
@NumPagesToDelete int

AS
---------------------------------------------------------------------------------------
-- DELETE the specified number of pages from the specified item, starting with the 
-- specified page.
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

	-- Get the sequence order of the first Page to be deleted
	SELECT	@SequenceOrder = ip.SequenceOrder
	FROM	dbo.Page p
			INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID 
	WHERE	p.PageID = @PageID 
	AND		ip.ItemID = @ItemID

	-- Only proceed with the delete if the specified page was found in the item
	IF (@SequenceOrder IS NOT NULL)
	BEGIN

		-- Deactivate page records
		UPDATE	p
		SET		Active = 0 
		FROM	dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		WHERE	ip.SequenceOrder >= @SequenceOrder 
		AND		ip.SequenceOrder < (@SequenceOrder + @NumPagesToDelete)
		AND		ip.ItemID = @ItemID

		-- Realign remaining page records with images by adjusting the sequence order values, 
		-- the ocr pointers, and the image pointers  of the page records
		UPDATE	p
		SET		FileNamePrefix = @Barcode + '_' + right('000' + convert(varchar(4), (ip.SequenceOrder - @NumPagesToDelete)), 4),
				ExternalUrl = '/download/' + @Barcode + '/page/n' + convert(varchar(4), (ip.SequenceOrder - @NumPagesToDelete - 1))
		FROM	dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		WHERE	ip.ItemID = @ItemID and p.Active = 1 and ip.SequenceOrder >= (@SequenceOrder + @NumPagesToDelete)

		UPDATE	ip
		SET		ip.SequenceOrder = ip.SequenceOrder - @NumPagesToDelete
		FROM	dbo.ItemPage ip INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	ip.ItemID = @ItemID and p.Active = 1 and ip.SequenceOrder >= (@SequenceOrder + @NumPagesToDelete)

		-- If the specified ThumbnailPageID for the Item was just deactivated, clear it
		UPDATE	dbo.Book
		SET		ThumbnailPageID = NULL
		WHERE	ItemID = @ItemID
		AND		ThumbnailPageID IN (SELECT p.PageID FROM dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID WHERE ip.ItemID = @ItemID AND p.Active = 0)

		UPDATE	dbo.Segment
		SET		ThumbnailPageID = NULL
		WHERE	ItemID = @ItemID
		AND		ThumbnailPageID IN (SELECT p.PageID FROM dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID WHERE ip.ItemID = @ItemID AND p.Active = 0)
		
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
