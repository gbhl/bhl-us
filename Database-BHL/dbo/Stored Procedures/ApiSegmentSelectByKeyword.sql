CREATE PROCEDURE [dbo].[ApiSegmentSelectByKeyword]

@Keyword nvarchar(50)

AS
SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

-- Get segments tied directly to the specified Keyword
SELECT	s.SegmentID,
		s.BookID AS ItemID,
		s.SequenceOrder,
		s.SegmentGenreID,
		g.GenreName,
		s.Title,
		s.TranslatedTitle,
		s.ContainerTitle,
		s.ContainerTitlePartNumber,
		s.ContainerTitlePartName,
		s.PublicationDetails,
		s.PublisherName,
		s.PublisherPlace,
		s.Notes,
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
		s.LanguageCode,
		l.LanguageName,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		ISNULL(ii.IdentifierValue, '') AS DOIName,
		REPLACE(scs.Authors, '|', ';') AS Authors,
		REPLACE(scs.Subjects, '|', ';') AS Keywords,
		CAST(NULL AS DateTime) AS ContributorCreationDate,
		CAST(NULL AS DateTime) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.Keyword k 
		INNER JOIN dbo.ItemKeyword ik ON k.KeywordID = ik.KeywordID
		INNER JOIN dbo.vwSegment s ON ik.ItemID = s.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierIDDOI
WHERE	k.Keyword = @Keyword
AND		s.SegmentStatusID IN (30, 40) -- New, Published
ORDER BY s.SortTitle

GO
