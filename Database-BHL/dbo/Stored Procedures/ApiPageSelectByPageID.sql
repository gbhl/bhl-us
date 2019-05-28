CREATE PROCEDURE dbo.ApiPageSelectByPageID

@PageID INT

AS 

SET NOCOUNT ON

SELECT	p.PageID,
		p.ItemID,
		p.FileNamePrefix,
		p.SequenceOrder,
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
WHERE	p.PageID = @PageID

