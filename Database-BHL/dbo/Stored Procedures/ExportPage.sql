CREATE PROCEDURE dbo.ExportPage

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		p.PageID, 
		p.ItemID, 
		p.SequenceOrder, 
		p.[Year], 
		p.Volume, 
		p.Issue,
		ip.PagePrefix, 
		ip.PageNumber,
		pt.PageTypeName, 
		CONVERT(nvarchar(16), p.CreationDate, 120) AS CreationDate
FROM    dbo.Page p WITH (NOLOCK) 
		LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
        LEFT JOIN dbo.Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
        LEFT JOIN dbo.PageType pt WITH (NOLOCK) ON ppt.PageTypeID = pt.PageTypeID
WHERE	p.Active = 1
ORDER BY
		p.ItemID, p.SequenceOrder

END

