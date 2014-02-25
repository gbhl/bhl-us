CREATE PROCEDURE [dbo].[PageDetailInsertUpdate]

@PageID int,
@PageDetailStatusID int,
@Height int,
@Width int,
@PixelDepth int,
@AbbyyHasImage smallint,
@ContrastHasImage smallint,
@PercentCoverage decimal(5,2)

AS

BEGIN

SET NOCOUNT ON

DECLARE @PageDetailID int
SET @PageDetailID = NULL

SELECT @PageDetailID = PageDetailID FROM dbo.PageDetail WHERE PageID = @PageID

IF @PageDetailID IS NULL
BEGIN
	-- No prior record, so insert a new record
	INSERT	dbo.PageDetail
		(
		PageID,
		PageDetailStatusID,
		Height,
		Width,
		PixelDepth,
		AbbyyHasImage,
		ContrastHasImage,
		PercentCoverage
		)
	VALUES
		(
		@PageID,
		@PageDetailStatusID,
		@Height,
		@Width,
		@PixelDepth,
		@AbbyyHasImage,
		@ContrastHasImage,
		@PercentCoverage
		)

	SELECT @PageDetailID = SCOPE_IDENTITY()
END
ELSE
BEGIN
	-- Update the existing record
	UPDATE	dbo.PageDetail
	SET		PageDetailStatusID = CASE WHEN @AbbyyHasImage = 1 OR @ContrastHasImage = 1 
								THEN 10 -- Extracted
								ELSE 60 -- NoImageFound
								END,
			Height = @Height,
			Width = @Width,
			PixelDepth = @PixelDepth,
			AbbyyHasImage = @AbbyyHasImage,
			ContrastHasImage = @ContrastHasImage,
			PercentCoverage = @PercentCoverage,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = 1
	WHERE	PageDetailID = @PageDetailID

	-- Remove any attached PageIllustration records (they will be replaced)
	DELETE FROM dbo.PageIllustration WHERE PageDetailID = @PageDetailID
END

-- Return the new/updated record
SELECT	PageDetailID,
		PageID,
		PageDetailStatusID,
		StatusDate,
		Height,
		Width,
		PixelDepth,
		AbbyyHasImage,
		ContrastHasImage,
		PercentCoverage,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.PageDetail
WHERE	PageDetailID = @PageDetailID

END

