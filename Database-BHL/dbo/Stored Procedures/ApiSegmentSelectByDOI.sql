CREATE PROCEDURE [dbo].[ApiSegmentSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
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
		REPLACE(scs.Authors, '|', ';') AS Authors,
		REPLACE(scs.Subjects, '|', ';') as Keywords,
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.Segment s 
		INNER JOIN dbo.DOI d ON s.SegmentID = d.EntityID
		INNER JOIN dbo.DOIEntityType et ON d.DOIEntityTypeID = et.DOIEntityTypeID AND et.DOIEntityTypeName = 'Segment'
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	d.DOIName = @DOIName
AND		s.SegmentStatusID IN (10, 20) -- New, Published

END
