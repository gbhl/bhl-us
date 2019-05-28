CREATE PROCEDURE dbo.ApiPageSelectByPageIDList

@IDList AS dbo.IDListInt READONLY

AS

BEGIN

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
		INNER JOIN @IDList list ON p.PageID = list.ID

END
