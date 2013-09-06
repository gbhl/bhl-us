
-- PDFStatusInsertAuto PROCEDURE
-- Generated 1/23/2009 8:46:39 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PDFStatus

CREATE PROCEDURE PDFStatusInsertAuto

@PdfStatusID INT,
@PdfStatusName NCHAR(10)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PDFStatus]
(
	[PdfStatusID],
	[PdfStatusName]
)
VALUES
(
	@PdfStatusID,
	@PdfStatusName
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PdfStatusID],
		[PdfStatusName]	

	FROM [dbo].[PDFStatus]
	
	WHERE
		[PdfStatusID] = @PdfStatusID
	
	RETURN -- insert successful
END

