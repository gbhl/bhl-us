
CREATE PROCEDURE [dbo].[SegmentSelectForSegmentID]

@SegmentID int

AS

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
		scs.IsPrimary,
		scs.SegmentClusterID,
		s.SegmentStatusID,
		st.StatusName,
		d.DOIName,
		s.ContributorCode,
		inst.InstitutionName AS ContributorName,
		s.ContributorSegmentID,
		s.SequenceOrder,
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
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		dbo.fnAuthorStringForSegment(s.SegmentID, ' ') AS Authors
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		LEFT JOIN dbo.Institution inst ON s.ContributorCode = inst.InstitutionCode
		LEFT JOIN dbo.[Language] l ON s.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.SegmentClusterSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40 -- segment
WHERE	s.SegmentID = @SegmentID


