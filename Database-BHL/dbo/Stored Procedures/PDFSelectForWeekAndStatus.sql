SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PDFSelectForWeekAndStatus]

@Year INT,
@Week INT,
@PdfStatusID INT

AS

BEGIN

SET NOCOUNT ON 

SELECT	p.PdfID,
		it.ItemTypeName,
		COALESCE(b.BookID, s.SegmentID) AS ItemID,
		p.EmailAddress,
		p.ImagesOnly,
		p.ArticleTitle,
		p.ArticleCreators,
		p.ArticleTags,
		p.FileUrl,
		p.CreationDate,
		DATEDIFF(minute, p.CreationDate, p.FileGenerationDate) AS [MinutesToGenerate],
		p.NumberImagesMissing,
		p.NumberOcrMissing,
		(SELECT COUNT(*) FROM PDFPage WHERE PdfID = p.PdfID) AS [NumberOfPages]
FROM	dbo.PDF p
		LEFT JOIN dbo.Book b ON p.ItemID = b.ItemID
		LEFT JOIN dbo.Segment s ON p.ItemID = s.ItemID
		INNER JOIN dbo.Item i ON p.ItemID = i.ItemID
		INNER JOIN dbo.ItemType it ON i.ItemTypeID = it.ItemTypeID
WHERE	DATEPART(year, p.CreationDate) = @Year
AND		DATEPART(week, p.CreationDate) = @Week
AND		p.PdfStatusID = @PdfStatusID
ORDER BY 
		p.FileGenerationDate, PdfID

END

GO
