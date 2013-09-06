
CREATE PROCEDURE [dbo].[PDFSelectDuplicateForPdfID]

@PdfID INT

AS

BEGIN
/***********************************************************
 *
 * Get identifiers and file locations for existing (generated)
 * PDFs that contain exactly the same pages as the specified
 * PDF.
 *
 ***********************************************************/

SET NOCOUNT ON

-- Get set of pages for the PDF we're about to generate
SELECT	PdfID, 
		PageID, 
		ROW_NUMBER() OVER (PARTITION BY PdfID ORDER BY PageID) AS RowNum
INTO	#tmpControl 
FROM	PDFPage 
WHERE	PdfID = @PdfID

-- Get the number of pages in the PDF we're about to generate
DECLARE @NumPages INT
SELECT @NumPages = COUNT(*) FROM #tmpControl WHERE PdfID = @PdfID

-- Get the ImageOnly flag for the PDF we're about to generate
DECLARE @ImagesOnly BIT
SELECT @ImagesOnly = ImagesOnly FROM PDF WHERE PdfID = @PdfID

-- Find all generated pdfs that have the same number of pages as the
-- PDF we're about the generate
SELECT	pp.PdfID
INTO	#tmpPdf
FROM	PDFPage pp INNER JOIN PDF p
			ON pp.PdfID = p.PdfID
WHERE	pp.PdfID <> @PdfID
AND		p.PdfStatusID = 30 -- Generated
AND		p.ImagesOnly = @ImagesOnly
-- find generated, not deleted pdfs with article info (won't be deleted)
AND		p.FileGenerationDate IS NOT NULL
AND		p.FileDeletionDate IS NULL
AND		(p.ArticleTitle <> '' OR
		 p.ArticleCreators <> '' OR
		 p.ArticleTags <> '')
GROUP BY pp.PdfID HAVING COUNT(*) = @NumPages

-- Get the PDF and page identifiers for the generated pdfs that have the
-- same number of pages as the PDF we're about to generate
SELECT	t.PdfID, 
		pp.PageID, 
		ROW_NUMBER() OVER (PARTITION BY t.PdfID ORDER BY pp.PageID) AS RowNum
INTO	#tmpCompare
FROM	#tmpPdf t INNER JOIN PDF p
			ON t.PdfID = p.PdfID
		INNER JOIN PDFPage pp
			ON t.PdfID = pp.PdfID

-- Get the identifers and file locations for generated pdfs that match
-- exactly the pages in the PDF we're about to generate
SELECT	PdfID, 
		FileLocation,
		FileUrl
FROM	PDF 
WHERE	PdfID IN (
			SELECT	com.PdfID
			FROM	#tmpControl con INNER JOIN #tmpCompare com
						ON con.PageID = com.PageID
						AND con.RowNum = com.RowNum
			GROUP BY com.PdfID HAVING COUNT(*) = @NumPages
			)
AND		NumberImagesMissing = 0
AND		NumberOcrMissing = 0

DROP TABLE #tmpCompare
DROP TABLE #tmpPdf
DROP TABLE #tmpControl

END

