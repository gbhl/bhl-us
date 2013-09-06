CREATE PROCEDURE dbo.PDFUpdateGenerationInfo

@PdfID INT,
@FileLocation NVARCHAR(200),
@FileUrl NVARCHAR(200),
@PdfStatusID INT,
@NumberImagesMissing INT,
@NumberOcrMissing INT

AS 

SET NOCOUNT ON

UPDATE	dbo.PDF
SET		FileLocation = @FileLocation,
		FileUrl = @FileUrl,
		FileGenerationDate = GETDATE(),
		PdfStatusID = @PdfStatusID,
		NumberImagesMissing = @NumberImagesMissing,
		NumberOcrMissing = @NumberOcrMissing,
		LastModifiedDate = GETDATE()
WHERE	PdfID = @PdfID
	
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFUpdateGenerationInfo. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		PdfID,
		ItemID,
		EmailAddress,
		ShareWithEmailAddresses,
		ImagesOnly,
		ArticleTitle,
		ArticleCreators,
		ArticleTags,
		FileLocation,
		FileUrl,
		FileGenerationDate,
		FileDeletionDate,
		PdfStatusID,
		NumberImagesMissing,
		NumberOcrMissing,
		Comment,
		CreationDate,
		LastModifiedDate

	FROM dbo.PDF
	
	WHERE
		PdfID = @PdfID
	
	RETURN -- update successful
END

