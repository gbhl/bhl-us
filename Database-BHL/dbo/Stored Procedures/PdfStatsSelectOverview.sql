CREATE PROCEDURE dbo.PdfStatsSelectOverview
AS
BEGIN

SET NOCOUNT ON

-- Overall stats (grouped by status)
SELECT	s.PdfStatusID,
		s.PdfStatusName, 
		COUNT(*) as [NumberOfPDFs],
		SUM(CASE WHEN ImagesOnly = 1 THEN 0 ELSE 1 END) AS [PDFsWithOCR],
		SUM(CASE WHEN ArticleTitle <> '' OR ArticleCreators <> '' OR ArticleTags <> '' THEN 1 ELSE 0 END) AS [PDFsWithArticleMetadata],
		SUM(CASE WHEN NumberImagesMissing > 0 THEN 1 ELSE 0 END) AS [PDFsWithMissingImages],
		SUM(CASE WHEN NumberOCRMissing > 0 THEN 1 ELSE 0 END) AS [PDFsWithMissingOCR]
FROM	dbo.PDF p INNER JOIN dbo.PDFStatus s
			ON p.PdfStatusID = s.PdfStatusID
GROUP BY
		s.PdfStatusId,
		s.PdfStatusName
ORDER BY
		s.PdfStatusID
END
