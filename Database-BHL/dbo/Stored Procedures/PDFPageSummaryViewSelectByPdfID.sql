
CREATE PROCEDURE [dbo].[PDFPageSummaryViewSelectByPdfID]

@PdfID int

AS
BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		psv.MARCBibID,
		psv.FullTitle,
		psv.TitleID,
		psv.ItemID,
		psv.Volume,
		psv.PageID,
		psv.BarCode,
		psv.FileNamePrefix,
		psv.OCRFolderShare,
		psv.WebVirtualDirectory,
		psv.ExternalURL,
		psv.AltExternalURL,
		psv.FileRootFolder,
		psv.RareBooks,
		psv.Illustration,
		psv.SequenceOrder
FROM	PDFPage ppg	INNER JOIN PageSummaryView psv
			ON ppg.PageID = psv.PageID
		INNER JOIN Item i 
			ON psv.TitleID = i.PrimaryTitleID
			AND psv.ItemID = i.ItemID
WHERE	ppg.PdfID = @PdfID
ORDER BY
		psv.SequenceOrder

END


