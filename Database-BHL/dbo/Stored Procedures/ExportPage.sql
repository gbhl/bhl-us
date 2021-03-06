SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExportPage]

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		p.PageID, 
		b.BookID AS ItemID, 
		ipg.SequenceOrder, 
		p.[Year], 
		p.Volume, 
		p.Issue,
		ip.PagePrefix, 
		ip.PageNumber,
		pt.PageTypeName, 
		c.HasLocalContent,
		c.HasExternalContent,
		CONVERT(nvarchar(16), p.CreationDate, 120) AS CreationDate
FROM    dbo.Page p WITH (NOLOCK) 
		LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
        LEFT JOIN dbo.Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
        LEFT JOIN dbo.PageType pt WITH (NOLOCK) ON ppt.PageTypeID = pt.PageTypeID
		INNER JOIN dbo.ItemPage ipg ON p.PageID = ipg.PageID
		INNER JOIN dbo.Book b ON ipg.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON b.BookID = c.ItemID
WHERE	p.Active = 1
ORDER BY
		b.BookID, ipg.SequenceOrder

END


GO
