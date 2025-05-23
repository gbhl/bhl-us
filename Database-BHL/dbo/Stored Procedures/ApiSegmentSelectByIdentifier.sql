CREATE PROCEDURE [dbo].[ApiSegmentSelectByIdentifier]

@IdentifierType nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

BEGIN

SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI';

SELECT DISTINCT
		s.SegmentID,
		s.BookID AS ItemID,
		ISNULL(isrc.SourceName, '') AS SourceName,
		ISNULL(s.Barcode, '') AS Barcode,
		dbo.fnContributorStringForSegment(s.SegmentID) AS ContributorName,
		d.IdentifierValue AS DOIName,
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
		REPLACE(scs.Subjects, '|', ';') AS Keywords,
		CAST(NULL AS DateTime) AS ContributorCreationDate,
		CAST(NULL AS DateTime) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.vwSegment s 
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID
		LEFT JOIN dbo.Identifier id ON ii.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.ItemIdentifier d ON s.ItemID = d.ItemID AND d.IdentifierID = @IdentifierIDDOI
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.ItemSource isrc ON i.ItemSourceID = isrc.ItemSourceID
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
WHERE	(id.IdentifierType = @IdentifierType AND ii.IdentifierValue = @IdentifierValue)
AND		s.SegmentStatusID IN (30, 40) -- New, Published
ORDER BY
		s.BookID,
		s.SequenceOrder

END

GO
