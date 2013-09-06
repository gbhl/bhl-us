
-- PDFPageUpdateAuto PROCEDURE
-- Generated 11/24/2008 4:39:21 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for PDFPage

CREATE PROCEDURE PDFPageUpdateAuto

@PdfPageID INT,
@PdfID INT,
@PageID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[PDFPage]

SET

	[PdfID] = @PdfID,
	[PageID] = @PageID

WHERE
	[PdfPageID] = @PdfPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

