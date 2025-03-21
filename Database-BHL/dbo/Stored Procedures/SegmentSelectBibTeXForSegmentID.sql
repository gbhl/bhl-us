CREATE PROCEDURE [dbo].[SegmentSelectBibTeXForSegmentID]

@SegmentID INT,
@TitleID INT = NULL,
@IncludeNoContent SMALLINT = 0

AS

BEGIN

SET NOCOUNT ON

SELECT	'bhlpart' + CONVERT(nvarchar(10), s.SegmentID) AS CitationKey,
		'https://www.biodiversitylibrary.org/part/' + CONVERT(nvarchar(20), s.SegmentID) AS Url,
		g.GenreName AS [Type],
		s.Title,
		ISNULL(t.FullTitle, s.ContainerTitle) AS Journal,
		ISNULL(t.PublicationDetails, s.PublicationDetails) AS Publisher,
		s.[Date] AS [Year],
		s.Volume,
		s.Series,
		s.Issue,
		ISNULL(s.Notes, '') as Note,
		s.RightsStatus AS CopyrightStatus,
		CASE 
			WHEN s.PageRange <> '' THEN s.PageRange
			WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
			WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
			ELSE s.EndPageNumber 
		END AS PageRange,
		scs.Authors,
		scs.Subjects AS 'Keywords',
		dbo.fnGetPageCountForSegment(s.SegmentID) AS 'Pages'
FROM	dbo.vwSegment s INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID AND ((it.IsPrimary = 1 AND @TitleID IS NULL) OR it.TitleID = @TitleID)
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	s.SegmentStatusID IN (30, 40)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL or @IncludeNoContent = 1)
AND		s.SegmentID = @SegmentID

END

GO
