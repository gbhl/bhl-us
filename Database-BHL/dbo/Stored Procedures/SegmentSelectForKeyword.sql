CREATE PROCEDURE [dbo].[SegmentSelectForKeyword]

@Keyword nvarchar(50)

AS

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
		scs.Contributors AS ContributorName,
		g.GenreName,
		s.Title,
		s.SortTitle,
		s.TranslatedTitle,
		s.ContainerTitle,
		s.PublicationDetails,
		s.PublisherName,
		s.PublisherPlace,
		s.Volume,
		s.Series,
		s.Issue,
		s.Date,
		CASE
		WHEN s.PageRange <> '' THEN s.PageRange 
		WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
		WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
		ELSE s.EndPageNumber
		END AS PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		ISNULL(l.LanguageName, '') AS LanguageName,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		scs.Authors,
		scs.Subjects
FROM	dbo.Keyword k
		INNER JOIN dbo.SegmentKeyword sk ON k.KeywordID = sk.KeywordID
		INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	k.Keyword = @Keyword
AND		s.SegmentStatusID IN (10, 20)  -- New, Published
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
ORDER BY
		s.SortTitle,
		s.SequenceOrder
