CREATE PROCEDURE [dbo].[SegmentSelectWithoutDOIByItemID]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIStatusQueuedID int
SELECT @DOIStatusQueuedID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

DECLARE @DOIEntityTypeSegmentID int
SELECT @DOIEntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment';

WITH DOI_CTE
AS
(
		-- Get the DOIs on the queue
		SELECT	DOIID, DOIEntityTypeID, EntityID
		FROM	dbo.DOI d 
		WHERE	d.DOIStatusID = @DOIStatusQueuedID
)
SELECT	s.SegmentID,
		s.BookID,
		s.ItemID,
		s.SegmentStatusID,
		st.ItemStatusName AS StatusName,
		s.SequenceOrder,
		pt.TitleID,
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
		LEFT JOIN dbo.ItemRelationship ir ON s.ItemID = ir.ChildID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON ir.ParentID = pt.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		LEFT JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN DOI_CTE d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = @DOIEntityTypeSegmentID
WHERE	s.BookID = @ItemID
AND		s.SegmentStatusID IN (30, 40)  -- New, Published
AND		d.DOIID IS NULL -- Is not already on the queue
ORDER BY
		s.SequenceOrder

END

GO
