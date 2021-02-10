CREATE PROCEDURE [dbo].[SegmentSelectWithoutDOIByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIEntityTypeSegmentID int
SELECT @DOIEntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

SELECT	s.SegmentID,
		s.BookID,
		s.ItemID,
		s.SegmentStatusID,
		st.ItemStatusName AS StatusName,
		s.SequenceOrder,
		it.TitleID,
		s.SegmentGenreID,
		g.GenreName,
		s.Title,
		s.SortTitle,
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
		CAST(NULL AS DATETIME) AS ContributorCreationDate,
		CAST(NULL AS DATETIME) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		scs.Authors
FROM	dbo.vwSegment s 
		INNER JOIN dbo.ItemRelationship ir ON s.ItemID = ir.ChildID
		INNER JOIN dbo.ItemTitle it ON ir.ParentID = it.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		LEFT JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = @DOIEntityTypeSegmentID
WHERE	it.TitleID = @TitleID
AND		s.SegmentStatusID IN (30, 40)  -- New, Published
AND		d.DOIID IS NULL	-- Does not have a DOI
ORDER BY
		it.TitleID

END

GO
