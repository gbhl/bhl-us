CREATE PROCEDURE [dbo].[ExportPage]

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID, 
		b.BookID AS ItemID, 
		ipg.SequenceOrder, 
		p.[Year], 
		p.Volume, 
		p.Issue,
		ip.PagePrefix, 
		ip.PageNumber,
		pt.PageTypeName, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), MIN(p.CreationDate), 120) AS CreationDate
FROM    dbo.Page p 
		LEFT JOIN dbo.IndicatedPage ip ON p.PageID = ip.PageID
        LEFT JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID
        LEFT JOIN dbo.PageType pt ON ppt.PageTypeID = pt.PageTypeID
		INNER JOIN dbo.ItemPage ipg ON p.PageID = ipg.PageID
		INNER JOIN dbo.Book b ON ipg.ItemID = b.ItemID
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
WHERE	p.Active = 1
GROUP BY
		p.PageID, 
		b.BookID,
		ipg.SequenceOrder, 
		p.[Year], 
		p.Volume, 
		p.Issue,
		ip.PagePrefix, 
		ip.PageNumber,
		pt.PageTypeName

END

GO
