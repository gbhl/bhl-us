CREATE PROCEDURE [dbo].[SegmentSelectByItemID]

@ItemID int,
@ShowAll smallint = 0

AS

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
		s.SegmentStatusID,
		st.StatusName,
		s.SequenceOrder,
		i.PrimaryTitleID AS TitleID,
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
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		scs.Authors
FROM	dbo.Segment s 
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.ItemID = @ItemID
AND		(@ShowAll = 1 OR s.SegmentStatusID IN (10, 20))  -- New, Published
ORDER BY
		s.SequenceOrder

END
