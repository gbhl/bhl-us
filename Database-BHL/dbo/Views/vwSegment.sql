CREATE VIEW [dbo].[vwSegment]
AS
SELECT	s.SegmentID,
		b.BookID,
		s.ItemID,
		s.BarCode,
		iseg.ItemStatusID AS SegmentStatusID,
		ISNULL(ir.SequenceOrder, 1) AS SequenceOrder,
		s.SegmentGenreID,
		s.Title,
		s.TranslatedTitle,
		CASE WHEN ISNULL(preft.FullTitle, '') <> '' THEN preft.FullTitle
			WHEN ISNULL(t.FullTitle, '') <> '' THEN t.FullTitle
			ELSE s.ContainerTitle COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS ContainerTitle,
		CASE WHEN ISNULL(preft.FullTitle, '' ) <> '' THEN ISNULL(preft.PartNumber, '')
			WHEN ISNULL(t.FullTitle, '') <> '' THEN ISNULL(t.PartNumber, '')
			ELSE ''
			END AS ContainerTitlePartNumber,
		CASE WHEN ISNULL(preft.FullTitle, '' ) <> '' THEN ISNULL(preft.PartName, '')
			WHEN ISNULL(t.FullTitle, '') <> '' THEN ISNULL(t.PartName, '')
			ELSE ''
			END AS ContainerTitlePartName,
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
		iseg.Note AS Notes,
		s.Summary,
		CASE WHEN s.Volume <> '' 
			THEN s.Volume
			ELSE ISNULL(b.StartVolume, '')
			END AS Volume,
		CASE WHEN s.Series <> ''
			THEN s.Series
			ELSE ISNULL(b.StartSeries, '')
			END AS Series,
		CASE WHEN s.Issue <> ''
			THEN s.Issue
			ELSE ISNULL(b.StartIssue, '')
			END AS Issue,
		CASE WHEN s.Edition <> ''
			THEN s.Edition
			ELSE ISNULL(t.EditionStatement, '')
			END AS Edition,
		CASE WHEN s.Date <> '' THEN s.Date
			WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS Date,
		s.PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		CASE WHEN s.LanguageCode IS NOT NULL
			THEN s.LanguageCode
			ELSE b.LanguageCode
			END AS LanguageCode,
		s.Url,
		s.DownloadUrl,
		CASE WHEN ISNULL(b.CopyrightStatus, '') <> ''
			THEN b.CopyrightStatus 
			ELSE s.RightsStatus 
			END AS RightsStatus,
		CASE WHEN ISNULL(b.Rights, '') <> ''
			THEN b.Rights
			ELSE s.RightsStatement
			END AS RightsStatement,
		s.LicenseName,
		CASE WHEN ISNULL(b.LicenseURL, '') <> ''
			THEN b.LicenseUrl
			ELSE s.LicenseUrl
			END AS LicenseUrl,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		s.SortTitle,
		s.RedirectSegmentID,
		s.PreferredContainerTitleID
FROM	dbo.Segment s
		INNER JOIN dbo.Item iseg ON s.ItemID = iseg.ItemID
		LEFT JOIN dbo.ItemRelationship ir ON iseg.ItemID = ir.ChildID
		LEFT JOIN dbo.Item iitm ON ir.ParentID = iitm.ItemID
		LEFT JOIN dbo.Book b ON iitm.ItemID = b.ItemID
		LEFT JOIN dbo.ItemTitle it ON iitm.ItemID = it.ItemID AND it.IsPrimary = 1
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.Title preft ON s.PreferredContainerTitleID = preft.TitleID

GO
