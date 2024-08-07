CREATE PROCEDURE [dbo].[PageMetadataSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

-- Get Page Info
SELECT	p.PageID,
		ip.SequenceOrder,
		p.Year,
		p.Series,
		p.Volume,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		p.FileNamePrefix,
		v.FolderShare,
		v.WebVirtualDirectory,
		b.BarCode,
		t.MARCBibID,
		t.RareBooks,
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		pf.FlickrURL
INTO	#tmpPage
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.Vault v ON i.VaultID = v.VaultID
		LEFT JOIN dbo.PageFlickr pf WITH (NOLOCK) ON p.PageID = pf.PageID
WHERE	b.BookID = @ItemID
AND		p.Active = 1

-- Get Segment Info (assumes segment is attached to item/pages)
SELECT	ip.PageID, MAX(r.SequenceOrder) AS SegmentSequence
INTO	#tmpSegPage
FROM	dbo.Segment s
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemRelationship r ON i.ItemID = r.ChildID
		INNER JOIN dbo.Book b ON r.ParentID = b.ItemID
WHERE	b.BookID = @ItemID
AND		i.ItemStatusID IN (30,40)
GROUP BY 
		ip.PageID

SELECT	s.SegmentID
		,g.GenreName
		,ip.PageID
INTO	#tmpSegment
FROM	#tmpSegPage t 
		INNER JOIN dbo.ItemPage ip ON t.PageID = ip.PageID
		INNER JOIN dbo.Segment s ON ip.ItemID = s.ItemID 
		INNER JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID AND t.SegmentSequence = r.SequenceOrder
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID

-- Merge Page and Segment Info
SELECT	p.PageID,
		p.SequenceOrder,
		p.Year,
		p.Series,
		p.Volume,
		p.Issue,
		p.IndicatedPages,
		p.PageTypes,
		p.FileNamePrefix,
		p.FolderShare,
		p.WebVirtualDirectory,
		p.BarCode,
		p.MARCBibID,
		p.RareBooks,
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		p.FlickrURL,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		MIN(s.SegmentID) AS SegmentID,
		MIN(s.GenreName) AS GenreName
FROM	#tmpPage p 
		LEFT JOIN #tmpSegment s ON p.PageID = s.PageID
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
GROUP BY
		p.PageID,
		p.SequenceOrder,
		p.Year,
		p.Series,
		p.Volume,
		p.Issue,
		p.IndicatedPages,
		p.PageTypes,
		p.FileNamePrefix,
		p.FolderShare,
		p.WebVirtualDirectory,
		p.BarCode,
		p.MARCBibID,
		p.RareBooks,
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		p.FlickrURL,
		l.TextSource
ORDER BY
		p.SequenceOrder ASC

GO
