CREATE PROCEDURE [dbo].[ExportSegment]

AS

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID, 
		s.ItemID, 
		c.Contributors AS ContributorName, 
		s.SequenceOrder, 
		g.GenreName AS SegmentType, 
		s.Title, 
		s.ContainerTitle, 
        CASE WHEN s.PublicationDetails <> '' THEN s.PublicationDetails ELSE LTRIM(s.PublisherPlace + ' ' + s.PublisherName) END AS PublicationDetails, 
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
		ISNULL(CONVERT(nvarchar(20), StartPageID), '') AS StartPageID,
        ISNULL(l.LanguageName, N'') AS LanguageName, 
		'https://www.biodiversitylibrary.org/segment/' + CONVERT(NVARCHAR(20), s.SegmentID) AS SegmentUrl, 
		s.Url AS ExternalUrl, 
		s.DownloadUrl, 
		s.RightsStatus, 
		s.RightsStatement, 
		s.LicenseName, 
		s.LicenseUrl,
		c.HasLocalContent,
		c.HasExternalContent
FROM	dbo.vwSegment s WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
		INNER JOIN dbo.SegmentGenre g WITH (NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID 
		LEFT JOIN dbo.Language l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
WHERE	s.SegmentStatusID IN (10, 20)
AND     (s.ItemID IS NOT NULL OR ISNULL(s.Url, '') <> '')

END
