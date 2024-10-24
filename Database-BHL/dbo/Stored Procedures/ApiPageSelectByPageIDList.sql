CREATE PROCEDURE [dbo].[ApiPageSelectByPageIDList]

@IDList AS dbo.IDListInt READONLY

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID,
		COALESCE(b.BookID, s.SegmentID) AS ItemID,
		p.FileNamePrefix,
		ip.SequenceOrder,
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
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		LEFT JOIN dbo.Book b ON ip.ItemID = b.ItemID
		LEFT JOIN dbo.Segment s ON ip.ItemID = s.ItemID

END

GO
