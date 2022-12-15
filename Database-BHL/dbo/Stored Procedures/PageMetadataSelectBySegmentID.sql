CREATE PROCEDURE [dbo].[PageMetadataSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

-- Get Segment Page Info
SELECT	p.PageID,
		s.SegmentID,
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
		s.BarCode,
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		pf.FlickrURL,
		g.GenreName
INTO	#tmpPage
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Vault v ON i.VaultID = v.VaultID
		LEFT JOIN dbo.PageFlickr pf WITH (NOLOCK) ON p.PageID = pf.PageID
WHERE	s.SegmentID = @SegmentID
AND		p.Active = 1

-- Get Child Segment Info (this is unlikely)
SELECT	ip.PageID, MAX(r.SequenceOrder) AS SegmentSequence
INTO	#tmpSegPage
FROM	dbo.vwSegment c
		INNER JOIN dbo.ItemPage ip ON c.ItemID = ip.ItemID
		INNER JOIN dbo.ItemRelationship r ON ip.ItemID = r.ChildID
		INNER JOIN dbo.vwSegment p ON r.ParentID = p.ItemID
WHERE	p.SegmentID = @SegmentID 
AND		c.SegmentStatusID IN (30,40)
GROUP BY 
		ip.PageID

SELECT	s.SegmentID
		,g.GenreName
		,ip.PageID
INTO	#tmpChild
FROM	#tmpSegPage t 
		INNER JOIN dbo.ItemPage ip ON t.PageID = ip.PageID
		INNER JOIN dbo.vwSegment s ON ip.ItemID = s.ItemID 
		INNER JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID AND t.SegmentSequence = r.SequenceOrder
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID

-- Merge Parent and Child Segment Info
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
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		p.FlickrURL,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		MIN(COALESCE(s.SegmentID, p.SegmentID)) AS SegmentID,
		MIN(COALESCE(s.GenreName, p.GenreName)) AS GenreName
FROM	#tmpPage p 
		LEFT JOIN #tmpChild s ON p.PageID = s.PageID
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
GROUP BY
		p.PageID,
		p.SegmentID,
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
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		p.FlickrURL,
		l.TextSource
ORDER BY
		p.SequenceOrder ASC

GO
