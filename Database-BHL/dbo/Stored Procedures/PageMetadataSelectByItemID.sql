CREATE PROCEDURE [dbo].[PageMetadataSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

-- Get Page Info
SELECT	p.PageID,
		p.SequenceOrder,
		p.Year,
		p.Series,
		p.Volume,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		p.FileNamePrefix,
		v.FolderShare,
		v.WebVirtualDirectory,
		i.BarCode,
		t.MARCBibID,
		t.RareBooks,
		p.Illustration,
		p.ExternalURL,
		p.AltExternalURL,
		p.IssuePrefix,
		pf.FlickrURL
INTO	#tmpPage
FROM	dbo.Page p
		INNER JOIN dbo.Item i ON (p.ItemID = i.ItemID)
		INNER JOIN dbo.Title t ON (i.PrimaryTitleID = t.TitleID)
		LEFT JOIN dbo.Vault v ON (i.VaultID = v.VaultID)
		LEFT JOIN dbo.PageFlickr pf with (nolock) ON (p.PageID = pf.PageID)
WHERE	p.ItemID = @ItemID
AND		Active = 1

-- Get Segment Info (assumes segment is attached to item/pages)
SELECT	sp.PageID, MAX(s.SequenceOrder) AS SegmentSequence
INTO	#tmpSegPage
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentPage sp ON s.SegmentID = sp.SegmentID
WHERE	s.ItemID = @ItemID
AND		s.SegmentStatusID IN (10,20)
GROUP BY 
		sp.PageID

/*
-- Add this to include segment author information
SELECT	s.SegmentID, dbo.fnAuthorSearchStringForSegment(s.SegmentID, 1) AS AuthorName
INTO	#tmpSegAuth
FROM	dbo.Segment s
WHERE	s.ItemID = @ItemID
*/

SELECT	sp.SegmentID
		--,sa.AuthorName
		,g.GenreName
		,sp.PageID
INTO	#tmpSegment
FROM	#tmpSegPage t 
		INNER JOIN dbo.SegmentPage sp ON t.PageID = sp.PageID
		INNER JOIN dbo.Segment s ON sp.SegmentID = s.SegmentID AND t.SegmentSequence = s.SequenceOrder
		--INNER JOIN #tmpSegAuth sa ON s.SegmentID = sa.SegmentID
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
		MIN(s.SegmentID) AS SegmentID,
		MIN(s.GenreName) AS GenreName
FROM	#tmpPage p LEFT JOIN #tmpSegment s ON p.PageID = s.PageID
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
		p.FlickrURL
ORDER BY
		p.SequenceOrder ASC

