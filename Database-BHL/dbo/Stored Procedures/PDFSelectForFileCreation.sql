
CREATE PROCEDURE [dbo].[PDFSelectForFileCreation]
AS
BEGIN
/*
 * Because it affects the status of the selected PDF request, this procedure should 
 * only be called by the PDF Generator process (which will reset the status when it
 * has processed the request).
 */

SET NOCOUNT ON

DECLARE @PDFID int

-- Decline requests from known spam accounts
UPDATE	dbo.PDF
SET		PdfStatusID = 50 -- declined
WHERE	PdfStatusID = 10
AND		EmailAddress = 'ocio-soc@si.edu'

-- Get the identifier of the next PDF request to be processed
SELECT	@PdfID = MIN(PdfID)
FROM	dbo.PDF
WHERE	PdfStatusID = 10

-- Update the status of the PDF request to "Processing"
UPDATE	dbo.PDF
SET		PdfStatusID = 20
WHERE	PdfID = @PdfID
AND		PdfStatusID = 10

-- If no status update was made, then clear out the selected PDF request.
-- This ensures that only one generator process will handle the request (the
-- one that is able to update the status to "Processing").
IF (@@ROWCOUNT = 0) SET @PdfID = 0

-- Return the details of the selected PDF request
SELECT	PdfID,
		ItemID,
		EmailAddress,
		ShareWithEmailAddresses,
		ImagesOnly,
		ArticleTitle,
		ArticleCreators,
		ArticleTags
FROM	dbo.PDF
WHERE	PdfID = @PdfID

END


