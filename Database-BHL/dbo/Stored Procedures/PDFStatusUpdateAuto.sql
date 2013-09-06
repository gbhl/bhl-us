
-- PDFStatusUpdateAuto PROCEDURE
-- Generated 1/23/2009 8:46:39 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for PDFStatus

CREATE PROCEDURE PDFStatusUpdateAuto

@PdfStatusID INT,
@PdfStatusName NCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [dbo].[PDFStatus]

SET

	[PdfStatusID] = @PdfStatusID,
	[PdfStatusName] = @PdfStatusName

WHERE
	[PdfStatusID] = @PdfStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PdfStatusID],
		[PdfStatusName]

	FROM [dbo].[PDFStatus]
	
	WHERE
		[PdfStatusID] = @PdfStatusID
	
	RETURN -- update successful
END

