
-- PDFPageInsertAuto PROCEDURE
-- Generated 11/24/2008 4:39:21 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PDFPage

CREATE PROCEDURE PDFPageInsertAuto

@PdfPageID INT OUTPUT,
@PdfID INT,
@PageID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PDFPage]
(
	[PdfID],
	[PageID]
)
VALUES
(
	@PdfID,
	@PageID
)

SET @PdfPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PdfPageID],
		[PdfID],
		[PageID]	

	FROM [dbo].[PDFPage]
	
	WHERE
		[PdfPageID] = @PdfPageID
	
	RETURN -- insert successful
END

