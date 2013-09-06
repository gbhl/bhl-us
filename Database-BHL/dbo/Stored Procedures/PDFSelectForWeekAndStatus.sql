
CREATE PROCEDURE [dbo].[PDFSelectForWeekAndStatus]
@Year INT,
@Week INT,
@PdfStatusID INT
AS
BEGIN

SET NOCOUNT ON 

SELECT	p.PdfID,
		p.ItemID,
		p.EmailAddress,
--		p.ShareWithEmailAddresses,
		p.ImagesOnly,
		p.ArticleTitle,
		p.ArticleCreators,
		p.ArticleTags,
--		p.FileLocation,
		p.FileUrl,
--		p.Creationdate,
--		p.FileGenerationDate,
		DATEDIFF(minute, p.CreationDate, p.FileGenerationDate) AS [MinutesToGenerate],
--		p.FileDeletionDate,
		p.NumberImagesMissing,
		p.NumberOcrMissing,
--		p.Comment,
		(SELECT COUNT(*) FROM PDFPage WHERE PdfID = p.PdfID) AS [NumberOfPages]
FROM	PDF p
WHERE	DATEPART(year, p.CreationDate) = @Year
AND		DATEPART(week, p.CreationDate) = @Week
AND		p.PdfStatusID = @PdfStatusID
ORDER BY 
		p.FileGenerationDate, PdfID

END

