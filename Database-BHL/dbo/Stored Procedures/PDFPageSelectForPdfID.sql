CREATE PROCEDURE dbo.PDFPageSelectForPdfID
@PdfID INT
AS
BEGIN

SET NOCOUNT ON

SELECT	pp.PdfPageID,
		pp.PageID, 
		p.FileNamePrefix, 
		ip.PagePrefix, 
		ip.PageNumber, 
		pt.PageTypeName
FROM	dbo.PDFPage pp INNER JOIN dbo.Page p
			ON pp.PageID = p.PageID
		LEFT JOIN dbo.IndicatedPage ip
			ON p.PageID = ip.pageID
		LEFT JOIN dbo.Page_PageType ppt
			ON p.PageID = ppt.PageID
		LEFT JOIN dbo.PageType pt
			ON ppt.PageTypeID = pt.PageTypeID
WHERE	pp.PdfID = @PdfID
ORDER BY
		pp.PdfID, p.SequenceOrder

END
