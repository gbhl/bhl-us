CREATE PROCEDURE dbo.SegmentSelectChildSegmentsBySegmentID

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	c.SegmentID,
		c.BookID,
		c.ItemID,
		c.SegmentStatusID,
		st.ItemStatusName AS StatusName,
		c.SequenceOrder,
		c.SegmentGenreID,
		g.GenreName,
		c.Title,
		c.SortTitle,
		c.TranslatedTitle,
		c.ContainerTitle,
		c.PublicationDetails,
		c.PublisherName,
		c.PublisherPlace,
		c.Notes,
		c.Volume,
		c.Series,
		c.Issue,
		c.Date,
		CASE
		WHEN c.PageRange <> '' THEN c.PageRange 
		WHEN c.StartPageNumber <> '' AND c.EndPageNumber <> '' THEN c.StartPageNumber + '--' + c.EndPageNumber
		WHEN c.StartPageNumber <> '' THEN c.StartPageNumber
		ELSE c.EndPageNumber
		END AS PageRange,
		c.StartPageNumber,
		c.EndPageNumber,
		c.StartPageID,
		c.LanguageCode,
		l.LanguageName,
		c.Url,
		c.DownloadUrl,
		c.RightsStatus,
		c.RightsStatement,
		c.LicenseName,
		c.LicenseUrl,
		CAST(NULL AS DATETIME) AS ContributorCreationDate,
		CAST(NULL AS DATETIME) AS ContributorLastModifiedDate,
		c.CreationDate,
		c.LastModifiedDate,
		c.CreationUserID,
		c.LastModifiedUserID,
		scs.Authors
FROM	dbo.vwSegment s
		LEFT JOIN dbo.ItemRelationship ir ON s.ItemID = ir.ParentID
		LEFT JOIN dbo.vwSegment c ON ir.ChildID = c.ItemID
		INNER JOIN dbo.SegmentGenre g ON c.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON c.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus st ON c.SegmentStatusID = st.ItemStatusID
		INNER JOIN dbo.SearchCatalogSegment scs ON c.SegmentID = scs.SegmentID
WHERE	s.SegmentID = @SegmentID
AND		c.SegmentStatusID IN (30, 40)
ORDER BY
		ir.SequenceOrder

END

GO
