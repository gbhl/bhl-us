
-- PDFStatusDeleteAuto PROCEDURE
-- Generated 1/23/2009 8:46:39 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PDFStatus

CREATE PROCEDURE PDFStatusDeleteAuto

@PdfStatusID INT

AS 

DELETE FROM [dbo].[PDFStatus]

WHERE

	[PdfStatusID] = @PdfStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

