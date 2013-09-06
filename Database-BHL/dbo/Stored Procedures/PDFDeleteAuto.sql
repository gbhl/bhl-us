
-- PDFDeleteAuto PROCEDURE
-- Generated 1/21/2009 11:41:21 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PDF

CREATE PROCEDURE PDFDeleteAuto

@PdfID INT

AS 

DELETE FROM [dbo].[PDF]

WHERE

	[PdfID] = @PdfID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

