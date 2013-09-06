
CREATE PROCEDURE [dbo].[PDFSelectForDeletion]
AS
BEGIN

SET NOCOUNT ON

-- Select PDFs generated more than 45 days ago that
-- do not have article information and have not yet
-- been deleted
SELECT	PdfID,
		FileLocation
INTO	#PDF
FROM	PDF
WHERE	ArticleTitle = ''
AND		ArticleCreators = ''
AND		ArticleTags = ''
AND		DATEDIFF(d, FileGenerationDate, GETDATE()) > 45
AND		FileDeletionDate IS NULL

-- Make sure that the PDFs we've selected aren't 
-- associated with other requests that DO have article
-- information.  If any are, remove them from the list.
DELETE	#PDF
FROM	#PDF t INNER JOIN dbo.PDF p
			ON t.FileLocation = p.FileLocation
			AND (p.ArticleTitle <> '' OR p.ArticleCreators <> '' OR ArticleTags <> '')

-- Return the final result set
SELECT * FROM #PDF

END


