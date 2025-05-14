CREATE PROCEDURE [dbo].[ApiSegmentSelectForSegmentID]

@SegmentID int

AS

SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	s.SegmentID,
		s.BookID AS ItemID,
		ISNULL(isrc.SourceName, '') AS SourceName,
		ISNULL(s.Barcode, '') AS Barcode,
		scs.IsPrimary,
		scs.SegmentClusterID,
		s.SegmentStatusID,
		st.ItemStatusName AS StatusName,
		ii.IdentifierValue AS DOIName,
		dbo.fnContributorStringForSegment(s.SegmentID) AS ContributorName,
		s.SequenceOrder,
		s.SegmentGenreID,
		g.GenreName,
		s.Title,
		s.SortTitle,
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
		s.[Date],
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
		CAST(NULL AS DATETIME) AS ContributorCreationDate,
		CAST(NULL AS DATETIME) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		dbo.fnAuthorStringForSegment(s.SegmentID, ' ') AS Authors,
		s.RedirectSegmentID
FROM	dbo.vwSegment s
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		LEFT JOIN dbo.[Language] l ON s.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.SegmentClusterSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierIDDOI
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemSource isrc ON i.ItemSourceID = isrc.ItemSourceID
WHERE	s.SegmentID = @SegmentID

GO
