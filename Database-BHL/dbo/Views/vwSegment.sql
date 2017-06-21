CREATE VIEW dbo.vwSegment
AS
SELECT	s.SegmentID,
		s.ItemID,
		s.SegmentStatusID,
		s.SequenceOrder,
		s.SegmentGenreID,
		s.Title,
		s.TranslatedTitle,
		CASE WHEN ISNULL(t.FullTitle, '') <> ''
			THEN t.FullTitle
			ELSE s.ContainerTitle COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS ContainerTitle,
		CASE WHEN ISNULL(t.PublicationDetails, '') <> ''
			THEN t.PublicationDetails
			ELSE s.PublicationDetails COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublicationDetails,
		CASE WHEN ISNULL(t.Datafield_260_b, '') <> ''
			THEN t.Datafield_260_b
			ELSE s.PublisherName COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublisherName,
		CASE WHEN ISNULL(t.Datafield_260_a, '') <> ''
			THEN t.Datafield_260_a
			ELSE s.PublisherPlace COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublisherPlace,
		s.Notes,
		s.Summary,
		CASE WHEN s.Volume <> '' 
			THEN s.Volume
			ELSE ISNULL(i.StartVolume, '')
			END AS Volume,
		CASE WHEN s.Series <> ''
			THEN s.Series
			ELSE ISNULL(i.StartSeries, '')
			END AS Series,
		CASE WHEN s.Issue <> ''
			THEN s.Issue
			ELSE ISNULL(i.StartIssue, '')
			END AS Issue,
		CASE WHEN s.Edition <> ''
			THEN s.Edition
			ELSE ISNULL(t.EditionStatement, '')
			END AS Edition,
		CASE WHEN s.Date <> '' THEN s.Date
			WHEN ISNULL(i.Year, '') <> '' THEN i.Year
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS Date,
		s.PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		CASE WHEN s.LanguageCode IS NOT NULL
			THEN s.LanguageCode
			ELSE i.LanguageCode
			END AS LanguageCode,
		s.Url,
		s.DownloadUrl,
		CASE WHEN ISNULL(i.CopyrightStatus, '') <> ''
			THEN i.CopyrightStatus 
			ELSE s.RightsStatus 
			END AS RightsStatus,
		CASE WHEN ISNULL(i.Rights, '') <> ''
			THEN i.Rights
			ELSE s.RightsStatement
			END AS RightsStatement,
		s.LicenseName,
		CASE WHEN ISNULL(i.LicenseURL, '') <> ''
			THEN i.LicenseUrl
			ELSE s.LicenseUrl
			END AS LicenseUrl,
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		s.SortTitle,
		s.RedirectSegmentID
FROM	dbo.Segment s
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
