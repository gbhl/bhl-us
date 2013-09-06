CREATE PROCEDURE dbo.PDFUpdatePdfStatus
@PdfID int,
@PdfStatusID int,
@RowsUpdated int OUTPUT
AS
BEGIN

UPDATE	dbo.PDF
SET		PdfStatusID = @PdfStatusID
WHERE	PdfID = @PdfID
AND		PdfStatusID <> @PdfStatusID

SELECT	@RowsUpdated = @@ROWCOUNT

END
