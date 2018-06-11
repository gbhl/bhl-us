CREATE PROCEDURE [dbo].[ApiSegmentSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectItemID FROM dbo.Item WHERE ItemID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec [dbo].[ApiSegmentSelectByItemID] @RedirItemID
ELSE
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
			ISNULL(d.DOIName, '') AS DOIName,
			REPLACE(scs.Authors, '|', ';') AS Authors,
			REPLACE(scs.Subjects, '|', ';') AS Keywords,
			s.ContributorCreationDate,
			s.ContributorLastModifiedDate,
			s.CreationDate,
			s.LastModifiedDate,
			s.CreationUserID,
			s.LastModifiedUserID
	FROM	dbo.vwSegment s 
			INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
			LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
			INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
			INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
			LEFT JOIN dbo.DOI d 
				ON s.SegmentID = d.EntityID 
				AND d.DOIEntityTypeID = 40 -- segment
				AND d.DOIStatusID IN (100, 200)
	WHERE	s.ItemID = @ItemID
	AND		s.SegmentStatusID IN (10, 20)  -- New, Published
	ORDER BY
			s.SequenceOrder
