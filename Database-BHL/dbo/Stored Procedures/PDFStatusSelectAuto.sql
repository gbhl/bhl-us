
-- PDFStatusSelectAuto PROCEDURE
-- Generated 1/23/2009 8:46:39 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for PDFStatus

CREATE PROCEDURE PDFStatusSelectAuto

@PdfStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[PdfStatusID],
	[PdfStatusName]

FROM [dbo].[PDFStatus]

WHERE
	[PdfStatusID] = @PdfStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

