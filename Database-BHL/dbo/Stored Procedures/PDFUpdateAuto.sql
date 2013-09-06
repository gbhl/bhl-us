
-- PDFUpdateAuto PROCEDURE
-- Generated 1/21/2009 11:41:21 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for PDF

CREATE PROCEDURE PDFUpdateAuto

@PdfID INT,
@ItemID INT,
@EmailAddress NVARCHAR(200),
@ShareWithEmailAddresses NVARCHAR(MAX),
@ImagesOnly BIT,
@ArticleTitle NVARCHAR(MAX),
@ArticleCreators NVARCHAR(MAX),
@ArticleTags NVARCHAR(MAX),
@FileLocation NVARCHAR(200),
@FileUrl NVARCHAR(200),
@FileGenerationDate DATETIME,
@FileDeletionDate DATETIME,
@PdfStatusID INT,
@NumberImagesMissing INT,
@NumberOcrMissing INT,
@Comment NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE [dbo].[PDF]

SET

	[ItemID] = @ItemID,
	[EmailAddress] = @EmailAddress,
	[ShareWithEmailAddresses] = @ShareWithEmailAddresses,
	[ImagesOnly] = @ImagesOnly,
	[ArticleTitle] = @ArticleTitle,
	[ArticleCreators] = @ArticleCreators,
	[ArticleTags] = @ArticleTags,
	[FileLocation] = @FileLocation,
	[FileUrl] = @FileUrl,
	[FileGenerationDate] = @FileGenerationDate,
	[FileDeletionDate] = @FileDeletionDate,
	[PdfStatusID] = @PdfStatusID,
	[NumberImagesMissing] = @NumberImagesMissing,
	[NumberOcrMissing] = @NumberOcrMissing,
	[Comment] = @Comment,
	[LastModifiedDate] = getdate()

WHERE
	[PdfID] = @PdfID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PdfID],
		[ItemID],
		[EmailAddress],
		[ShareWithEmailAddresses],
		[ImagesOnly],
		[ArticleTitle],
		[ArticleCreators],
		[ArticleTags],
		[FileLocation],
		[FileUrl],
		[FileGenerationDate],
		[FileDeletionDate],
		[PdfStatusID],
		[NumberImagesMissing],
		[NumberOcrMissing],
		[Comment],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[PDF]
	
	WHERE
		[PdfID] = @PdfID
	
	RETURN -- update successful
END

