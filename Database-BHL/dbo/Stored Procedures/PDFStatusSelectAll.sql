CREATE PROCEDURE PDFStatusSelectAll

AS 

SET NOCOUNT ON

SELECT 	PdfStatusID,
		PdfStatusName
FROM	dbo.PDFStatus
ORDER BY 
		PdfStatusID

