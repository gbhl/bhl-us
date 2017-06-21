CREATE PROCEDURE [dbo].[SegmentSelectBibTeXForSegmentID]

@SegmentID INT,
@IncludeNoContent SMALLINT = 0

AS

BEGIN

SET NOCOUNT ON

SELECT	'bhlpart' + CONVERT(nvarchar(10), s.SegmentID) AS CitationKey,
		'http://www.biodiversitylibrary.org/part/' + CONVERT(nvarchar(20), s.SegmentID) AS Url,
		'' AS Note,
		g.GenreName AS [Type],
		s.Title,
		s.ContainerTitle AS Journal,
		s.PublicationDetails AS Publisher,
		s.[Date] AS [Year],
		s.Volume,
		s.Series,
		s.Issue,
		s.Notes as Note,
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
WHERE	s.SegmentStatusID IN (10, 20)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL or @IncludeNoContent = 1)
AND		s.SegmentID = @SegmentID

END
