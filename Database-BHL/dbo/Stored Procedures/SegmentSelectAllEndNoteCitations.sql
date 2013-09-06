
CREATE PROCEDURE [dbo].[SegmentSelectAllEndNoteCitations]
AS
BEGIN

SET NOCOUNT ON

SELECT 	s.SegmentID,
		CASE g.GenreName
			WHEN 'Article' THEN 'Journal Article'
			WHEN 'Chapter' THEN 'Book Section'
			WHEN 'Treatment' THEN 'Book Section'
			WHEN 'Book' THEN 'Book'
			WHEN 'BookItem' THEN 'Book'
			WHEN 'Journal' THEN 'Serial'
			WHEN 'Issue' THEN 'Serial'
			ELSE 'Book'
		END AS PublicationType,
		REPLACE(scs.Authors, '|', ' ') AS Authors,
		CASE 
			WHEN s.[Date] <> '' THEN s.[Date]
			ELSE ISNULL(i.[Year], '')
		END AS [Year],
		s.Title,
		s.ContainerTitle AS Journal,
		CASE 
			WHEN s.PublicationDetails <> '' THEN s.PublicationDetails COLLATE SQL_Latin1_General_CP1_CI_AI
			ELSE ISNULL(t.PublicationDetails, '') 
		END AS Publisher,
		CASE
			WHEN s.Volume <> '' THEN s.Volume COLLATE SQL_Latin1_General_CP1_CI_AI
			ELSE ISNULL(i.Volume, '')
		END AS Volume,
		s.Series,
		s.Issue,
		CASE 
			WHEN s.PageRange <> '' THEN s.PageRange
			WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
			WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
			ELSE s.EndPageNumber 
		END AS PageRange,
		s.StartPageNumber,
		REPLACE(scs.Subjects, '|', ' ') AS 'Keywords',
		s.Notes AS Note,
		ISNULL(d.DOIName, '') AS DOI
FROM	dbo.Segment s INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40 -- Segment
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentStatusID IN (10, 20)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)

END




