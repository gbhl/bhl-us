
-- PDFPageDeleteAuto PROCEDURE
-- Generated 11/24/2008 4:39:21 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PDFPage

CREATE PROCEDURE PDFPageDeleteAuto

@PdfPageID INT

AS 

DELETE FROM [dbo].[PDFPage]

WHERE

	[PdfPageID] = @PdfPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFPageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

