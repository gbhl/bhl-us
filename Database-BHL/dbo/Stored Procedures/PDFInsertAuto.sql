
-- PDFInsertAuto PROCEDURE
-- Generated 1/21/2009 11:41:21 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PDF

CREATE PROCEDURE PDFInsertAuto

@PdfID INT OUTPUT,
@ItemID INT,
@EmailAddress NVARCHAR(200),
@ShareWithEmailAddresses NVARCHAR(MAX),
@ImagesOnly BIT,
@ArticleTitle NVARCHAR(MAX),
@ArticleCreators NVARCHAR(MAX),
@ArticleTags NVARCHAR(MAX),
@FileLocation NVARCHAR(200),
@FileUrl NVARCHAR(200),
@FileGenerationDate DATETIME = null,
@FileDeletionDate DATETIME = null,
@PdfStatusID INT,
@NumberImagesMissing INT,
@NumberOcrMissing INT,
@Comment NVARCHAR(MAX)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PDF]
(
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
)
VALUES
(
	@ItemID,
	@EmailAddress,
	@ShareWithEmailAddresses,
	@ImagesOnly,
	@ArticleTitle,
	@ArticleCreators,
	@ArticleTags,
	@FileLocation,
	@FileUrl,
	@FileGenerationDate,
	@FileDeletionDate,
	@PdfStatusID,
	@NumberImagesMissing,
	@NumberOcrMissing,
	@Comment,
	getdate(),
	getdate()
)

SET @PdfID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

