
-- PDFSelectAuto PROCEDURE
-- Generated 1/21/2009 11:41:21 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for PDF

CREATE PROCEDURE PDFSelectAuto

@PdfID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

