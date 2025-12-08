CREATE PROCEDURE dbo.PdfStatsSelectOverviewBasic

AS

BEGIN

SET NOCOUNT ON

SELECT	s.PdfStatusID,
		s.PdfStatusName, 
		COUNT(*) as [NumberOfPDFs]
FROM	dbo.PDF p INNER JOIN dbo.PDFStatus s
			ON p.PdfStatusID = s.PdfStatusID
GROUP BY
		s.PdfStatusId,
		s.PdfStatusName
ORDER BY
		s.PdfStatusID

END
GO
