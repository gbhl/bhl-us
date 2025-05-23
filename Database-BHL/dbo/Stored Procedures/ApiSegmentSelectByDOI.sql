CREATE PROCEDURE [dbo].[ApiSegmentSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	s.SegmentID,
		s.BookID AS ItemID,
		ISNULL(isrc.SourceName, '') AS SourceName,
		ISNULL(s.Barcode, '') AS Barcode,
		dbo.fnContributorStringForSegment(s.SegmentID) AS ContributorName,
		@DOIName as DOIName,
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
		s.Summary,
		s.Volume,
		s.Series,
		s.Issue,
		s.Edition,
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
		REPLACE(scs.Authors, '|', ';') AS Authors,
		REPLACE(scs.Subjects, '|', ';') as Keywords,
		CAST(NULL AS DateTime) AS ContributorCreationDate,
		CAST(NULL AS DateTime) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.vwSegment s 
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierIDDOI
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus it ON s.SegmentStatusID = it.ItemStatusID
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.ItemSource isrc ON i.ItemSourceID = isrc.ItemSourceID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	ii.IdentifierValue = @DOIName
AND		s.SegmentStatusID IN (30, 40) -- New, Published

END

GO
