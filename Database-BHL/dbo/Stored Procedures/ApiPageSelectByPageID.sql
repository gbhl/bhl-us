CREATE PROCEDURE [dbo].[ApiPageSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT DISTINCT
		p.PageID,
		COALESCE(b.BookID, s.BookID) AS ItemID,
		p.FileNamePrefix,
		--ip.SequenceOrder,
		p.PageDescription,
		p.Illustration,
		p.Note,
		p.FileSize_Temp,
		p.FileExtension,
		p.Active,
		p.Year,
		p.Series,
		p.Volume,
		p.Issue,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		p.LastPageNameLookupDate,
		p.PaginationUserID,
		p.PaginationDate,
		p.CreationDate,
		p.LastModifiedDate,
		p.CreationUserID,
		p.LastModifiedUserID
FROM	dbo.Page p
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		LEFT JOIN dbo.Book b ON ip.ItemID = b.ItemID
		LEFT JOIN dbo.vwSegment s ON ip.ItemID = s.ItemID
WHERE	p.PageID = @PageID

GO
