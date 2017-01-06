CREATE PROCEDURE [dbo].[ApiSegmentSelectByIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
		dbo.fnContributorStringForSegment(s.SegmentID) AS ContributorName,
		d.DOIName,
		s.SequenceOrder,
		s.SegmentGenreID,
		g.GenreName,
		s.Title,
		s.TranslatedTitle,
		s.ContainerTitle,
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
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.Segment s 
		INNER JOIN dbo.SegmentIdentifier si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Identifier i ON si.IdentifierID = i.IdentifierID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40 -- segment
WHERE	i.IdentifierName = @IdentifierName
AND		si.IdentifierValue = @IdentifierValue
AND		s.SegmentStatusID IN (10, 20) -- New, Published
ORDER BY
		s.ItemID,
		s.SequenceOrder

END
