CREATE PROCEDURE [dbo].[PdfStatsSelectExpanded]
AS
BEGIN

SET NOCOUNT ON 

-- Detailed stats (by week and status)
SELECT	DATEPART(year, CreationDate) AS [Year],
		DATEPART(week, CreationDate) AS [Week],
		MIN(CONVERT(varchar(10), CreationDate, 121)) AS WeekStartDate,
		s.PdfStatusID,
		s.PdfStatusName, 
		COUNT(*) as [NumberOfPDFs],
		SUM(CASE WHEN ImagesOnly = 1 THEN 0 ELSE 1 END) AS [PDFsWithOCR],
		SUM(CASE WHEN ArticleTitle <> '' OR ArticleCreators <> '' OR ArticleTags <> '' THEN 1 ELSE 0 END) AS [PDFsWithArticleMetadata],
		SUM(DATEDIFF(minute, CreationDate, FileGenerationDate)) AS [TotalMinutesToGenerate],
		CONVERT(decimal(10,2), AVG(CONVERT(decimal, DATEDIFF(minute, CreationDate, FileGenerationDate)))) AS [AvgMinutesToGenerate],
		SUM(CASE WHEN NumberImagesMissing > 0 THEN 1 ELSE 0 END) AS [PDFsWithMissingImages],
		SUM(NumberImagesMissing) AS [TotalMissingImages],
		SUM(CASE WHEN NumberOCRMissing > 0 THEN 1 ELSE 0 END) AS [PDFsWithMissingOCR],
		SUM(NumberOCRMissing) AS [TotalMissingOCR]
FROM	dbo.PDF p INNER JOIN dbo.PDFStatus s
			ON p.PdfStatusID = s.PdfStatusID
GROUP BY
		s.PdfStatusId,
		s.PdfStatusName,
		DATEPART(year, CreationDate),
		DATEPART(week, CreationDate)
ORDER BY
		[Year] desc, [Week] desc, s.PdfStatusID
END
