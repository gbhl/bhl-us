CREATE PROCEDURE [dbo].[SegmentSelectAllBibTeXCitations]
AS
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	CitationKey nvarchar(20) NOT NULL,
	Url nvarchar(50) NOT NULL,
	Note nvarchar(MAX) NOT NULL,
	[Type] nvarchar(50) NOT NULL,
	Title nvarchar(2000) NOT NULL,
	Journal nvarchar(2000) NOT NULL,
	Publisher nvarchar(400) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	Series nvarchar(100) NOT NULL,
	Issue nvarchar(100) NOT NULL,
	CopyrightStatus nvarchar(500) NOT NULL,
	PageRange nvarchar(20) NOT NULL,
	Authors nvarchar(max) NOT NULL,
	Keywords nvarchar(max) NOT NULL,
	Pages int NOT NULL
	)

INSERT INTO #tmpSegment
SELECT	s.SegmentID,
		'bhlpart' + CONVERT(nvarchar(10), s.SegmentID) AS CitationKey,
		'http://www.biodiversitylibrary.org/part/' + CONVERT(nvarchar(20), s.SegmentID) AS Url,
		s.Notes AS Note,
		g.GenreName AS [Type],
		s.Title,
		s.ContainerTitle AS Journal,
		s.PublicationDetails AS Publisher,
		s.[Date] AS [Year],
		s.Volume,
		s.Series,
		s.Issue,
		s.RightsStatus AS CopyrightStatus,
		LEFT(CASE 
			WHEN s.PageRange <> '' THEN s.PageRange
			WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
			WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
			ELSE s.EndPageNumber 
		END, 20) AS PageRange,
		scs.Authors,
		scs.Subjects as 'Keywords',
		0 AS Pages
FROM	dbo.vwSegment s INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentStatusID IN (10, 20)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)

/*
UPDATE	#tmpSegment
SET		Pages = dbo.fnGetPageCountForSegment(SegmentID)
*/

SELECT	CitationKey, Url, Note, [Type], Title, Journal, Publisher, [Year], 
		Volume, Series, Issue, CopyrightStatus, PageRange, Authors, Keywords,
		Pages
FROM	#tmpSegment

END




