SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PDFPageSummaryViewSelectByPdfID]

@PdfID int

AS
BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		psv.MARCBibID,
		psv.FullTitle,
		psv.TitleID,
		psv.BookID,
		psv.ItemID,
		psv.Volume,
		psv.PageID,
		psv.BarCode,
		psv.FileNamePrefix,
		psv.OCRFolderShare,
		psv.WebVirtualDirectory,
		psv.ExternalURL,
		CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL,
		psv.FileRootFolder,
		psv.RareBooks,
		psv.Illustration,
		psv.SequenceOrder
FROM	PDFPage ppg	
		INNER JOIN PDF p ON ppg.PDFID = p.PDFID
		INNER JOIN PageSummaryView psv ON ppg.PageID = psv.PageID AND p.ItemID = psv.ItemID
		INNER JOIN ItemTitle it ON psv.TitleID = it.TitleID AND psv.ItemID = it.ItemID AND it.IsPrimary = 1		
WHERE	ppg.PdfID = @PdfID
UNION
SELECT DISTINCT
		psv.MARCBibID,
		psv.FullTitle,
		psv.TitleID,
		psv.BookID,
		psv.ItemID,
		psv.Volume COLLATE SQL_Latin1_General_CP1_CI_AI,
		psv.PageID,
		psv.BarCode,
		psv.FileNamePrefix,
		psv.OCRFolderShare,
		psv.WebVirtualDirectory,
		psv.ExternalURL,
		CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL,
		psv.FileRootFolder,
		psv.RareBooks,
		psv.Illustration,
		psv.SequenceOrder
FROM	dbo.PDFPage ppg	
		INNER JOIN PDF p ON ppg.PDFID = p.PDFID
		INNER JOIN dbo.PageSummarySegmentView psv ON ppg.PageID = psv.PageID AND p.ItemID = psv.ItemID
WHERE	ppg.PdfID = @PdfID
ORDER BY
		psv.SequenceOrder

END

GO
