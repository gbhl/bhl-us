CREATE PROCEDURE import.ImportRecordSelectForReviewByImportRecordID

@ImportRecordID int

AS

BEGIN

SET NOCOUNT ON

-- Data for new segment inserts
SELECT	r.ImportRecordID,
		r.SegmentID,
		r.ImportSegmentID,
		r.ImportRecordStatusID, 
		s.StatusName,
		import.fnErrorStringForImportRecord(r.ImportRecordID, 'Error', '+++') AS Errors,
		import.fnErrorStringForImportRecord(r.ImportRecordID, 'Warning', '+++') AS Warnings,
		-- New container info (NC = New Container)
		b.BookID AS NCItemID,
		t.FullTitle AS NCTitle,
		b.StartVolume AS NCVolume,
		b.StartSeries AS NCSeries,
		b.StartIssue AS NCIssue,
		t.EditionStatement AS NCEdition,
		t.PublicationDetails AS NCPublicationDetails,
		t.Datafield_260_b AS NCPublisherName,
		t.Datafield_260_a AS NCPublisherPlace,
		CASE WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS NCYear,
		b.Rights AS NCRights,
		b.CopyrightStatus AS NCCopyrightStatus,
		b.LicenseUrl AS NCLicenseUrl,
		-- Existing container info (EC = Existing container)
		CAST(NULL AS int) AS ECItemID,
		CAST(NULL AS nvarchar(2000)) AS ECTitle,
		CAST(NULL AS nvarchar(10)) AS ECVolume,
		CAST(NULL AS nvarchar(10)) AS ECSeries,
		CAST(NULL AS nvarchar(10)) AS ECIssue,
		CAST(NULL AS nvarchar(450)) AS ECEdition,
		CAST(NULL AS nvarchar(255)) AS ECPublicationDetails,
		CAST(NULL AS nvarchar(255)) AS ECPublisherName,
		CAST(NULL AS nvarchar(150)) AS ECPublisherPlace,
		CAST(NULL AS nvarchar(20)) AS ECYear,
		CAST(NULL AS nvarchar(max)) AS ECRights,
		CAST(NULL AS nvarchar(max)) ECCopyrightStatus,
		CAST(NULL AS nvarchar(max)) AS ECLicenseUrl,
		-- New segment info (NS = New Segment)
		CASE WHEN g.SegmentGenreID IS NULL THEN 'Article' ELSE r.Genre END AS NSGenre,
		r.Title AS NSTitle, 
		r.TranslatedTitle AS NSTranslatedTitle,
		r.JournalTitle AS NSJournalTitle,
		r.Volume AS NSVolume,
		r.Series AS NSSeries,
		r.Issue AS NSIssue,
		r.Edition AS NSEdition,
		r.PublicationDetails AS NSPublicationDetails,
		r.PublisherName AS NSPublisherName,
		r.PublisherPlace AS NSPublisherPlace,
		r.[Year] AS NSYear,
		ISNULL(r.Language, '') AS NSLanguage,
		r.Summary AS NSSummary,
		r.Notes AS NSNotes,
		r.Rights AS NSRights,
		r.CopyrightStatus AS NSCopyrightStatus,
		r.License AS NSLicense,
		r.LicenseUrl AS NSLicenseUrl,
		r.PageRange AS NSPageRange,
		r.StartPage AS NSStartPage,
		r.StartPageID AS NSStartPageID,
		r.EndPage AS NSEndPage,
		r.EndPageID AS NSEndPageID,
		r.Url AS NSUrl,
		r.DownloadUrl AS NSDownloadUrl,
		r.DOI AS NSDOI,
		r.ISSN AS NSISSN,
		r.ISBN AS NSISBN,
		r.OCLC AS NSOCLC,
		r.LCCN AS NSLCCN,
		r.ARK AS NSARK,
		r.Biostor AS NSBiostor,
		r.JSTOR AS NSJSTOR,
		r.TL2 AS NSTL2,
		r.Wikidata AS NSWikidata,
		import.fnAuthorStringForImportRecord(r.ImportRecordID, '+++') AS NSAuthors,
		import.fnKeywordStringForImportRecord(r.ImportRecordID, '+++') AS NSKeywords,
		import.fnContributorStringForImportRecord(r.ImportRecordID, '+++') AS NSContributors,
		import.fnPageStringForImportRecord(r.ImportRecordID, '+++') AS NSPages,
		-- Existing segment info (ES = existing segment)
		CAST(NULL AS nvarchar(50)) AS ESGenre,
		CAST(NULL AS nvarchar(2000)) AS ESTitle, 
		CAST(NULL AS nvarchar(2000)) AS ESTranslatedTitle,
		CAST(NULL AS nvarchar(2000)) AS ESJournalTitle,
		CAST(NULL AS nvarchar(100)) AS ESVolume,
		CAST(NULL AS nvarchar(100)) AS ESSeries,
		CAST(NULL AS nvarchar(100)) AS ESIssue,
		CAST(NULL AS nvarchar(400)) AS ESEdition,
		CAST(NULL AS nvarchar(400)) AS ESPublicationDetails,
		CAST(NULL AS nvarchar(250)) AS ESPublisherName,
		CAST(NULL AS nvarchar(150)) AS ESPublisherPlace,
		CAST(NULL AS nvarchar(20)) AS ESYear,
		CAST(NULL AS nvarchar(30)) AS ESLanguage,
		CAST(NULL AS nvarchar(max)) AS ESSummary,
		CAST(NULL AS nvarchar(max)) AS ESNotes,
		CAST(NULL AS nvarchar(max)) AS ESRights,
		CAST(NULL AS nvarchar(max)) AS ESCopyrightStatus,
		CAST(NULL AS nvarchar(max)) AS ESLicense,
		CAST(NULL AS nvarchar(200)) AS ESLicenseUrl,
		CAST(NULL AS nvarchar(50)) AS ESPageRange,
		CAST(NULL AS nvarchar(20)) AS ESStartPage,
		CAST(NULL AS int) AS ESStartPageID,
		CAST(NULL AS nvarchar(20)) AS ESEndPage,
		CAST(NULL AS int) AS ESEndPageID,
		CAST(NULL AS nvarchar(200)) AS ESUrl,
		CAST(NULL AS nvarchar(200)) AS ESDownloadUrl,
		CAST(NULL AS nvarchar(125)) AS ESDOI,
		CAST(NULL AS nvarchar(125)) AS ESISSN,
		CAST(NULL AS nvarchar(125)) AS ESISBN,
		CAST(NULL AS nvarchar(125)) AS ESOCLC,
		CAST(NULL AS nvarchar(125)) AS ESLCCN,
		CAST(NULL AS nvarchar(125)) AS ESARK,
		CAST(NULL AS nvarchar(125)) AS ESBiostor,
		CAST(NULL AS nvarchar(125)) AS ESJSTOR,
		CAST(NULL AS nvarchar(125)) AS ESTL2,
		CAST(NULL AS nvarchar(125)) AS ESWikidata,
		CAST(NULL AS nvarchar(1024)) AS ESAuthors,
		CAST(NULL AS nvarchar(1024)) AS ESKeywords,
		CAST(NULL AS nvarchar(1024)) AS ESContributors,
		CAST(NULL AS nvarchar(max)) AS ESPages
FROM	import.ImportRecord r
		LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID
		LEFT JOIN dbo.Book b ON r.ItemID = b.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID
WHERE	ImportSegmentID IS NULL
AND		r.ImportRecordID = @ImportRecordID

UNION

-- Data for existing segment updates
SELECT	r.ImportRecordID,
		r.SegmentID,
		r.ImportSegmentID,
		r.ImportRecordStatusID, 
		s.StatusName,
		import.fnErrorStringForImportRecord(r.ImportRecordID, 'Error', '+++') AS Errors,
		import.fnErrorStringForImportRecord(r.ImportRecordID, 'Warning', '+++') AS Warnings,
		-- New container info (NC = New Container)
		b.BookID AS NCItemID,
		t.FullTitle AS NCTitle,
		b.StartVolume AS NCVolume,
		b.StartSeries AS NCSeries,
		b.StartIssue AS NCIssue,
		t.EditionStatement AS NCEdition,
		t.PublicationDetails AS NCPublicationDetails,
		t.Datafield_260_b AS NCPublisherName,
		t.Datafield_260_a AS NCPublisherPlace,
		CASE WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS NCYear,
		b.Rights AS NCRights,
		b.CopyrightStatus AS NCCopyrightStatus,
		b.LicenseUrl AS NCLicenseUrl,
		-- Existing container info (EC = Existing container)
		eb.BookID AS ECItemID,
		et.FullTitle AS ECTitle,
		eb.StartVolume AS ECVolume,
		eb.StartSeries AS ECSeries,
		eb.StartIssue AS ECIssue,
		et.EditionStatement AS ECEdition,
		et.PublicationDetails AS ECPublicationDetails,
		et.Datafield_260_b AS ECPublisherName,
		et.Datafield_260_a AS ECPublisherPlace,
		CASE WHEN ISNULL(eb.StartYear, '') <> '' THEN eb.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), et.StartYear), '')
			END AS ECYear,
		eb.Rights AS ECRights,
		eb.CopyrightStatus ECCopyrightStatus,
		eb.LicenseUrl AS ECLicenseUrl,
		-- New segment info (NS = New Segment)
		CASE WHEN g.SegmentGenreID IS NULL THEN 'Article' ELSE r.Genre END AS NSGenre,
		r.Title AS NSTitle, 
		r.TranslatedTitle AS NSTranslatedTitle,
		r.JournalTitle AS NSJournalTitle,
		r.Volume AS NSVolume,
		r.Series AS NSSeries,
		r.Issue AS NSIssue,
		r.Edition AS NSEdition,
		r.PublicationDetails AS NSPublicationDetails,
		r.PublisherName AS NSPublisherName,
		r.PublisherPlace AS NSPublisherPlace,
		r.[Year] AS NSYear,
		ISNULL(r.Language, '') AS NSLanguage,
		r.Summary AS NSSummary,
		r.Notes AS NSNotes,
		r.Rights AS NSRights,
		r.CopyrightStatus AS NSCopyrightStatus,
		r.License AS NSLicense,
		r.LicenseUrl AS NSLicenseUrl,
		r.PageRange AS NSPageRange,
		r.StartPage AS NSStartPage,
		r.StartPageID AS NSStartPageID,
		r.EndPage AS NSEndPage,
		r.EndPageID AS NSEndPageID,
		r.Url AS NSUrl,
		r.DownloadUrl AS NSDownloadUrl,
		r.DOI AS NSDOI,
		r.ISSN AS NSISSN,
		r.ISBN AS NSISBN,
		r.OCLC AS NSOCLC,
		r.LCCN AS NSLCCN,
		r.ARK AS NSARK,
		r.Biostor AS NSBiostor,
		r.JSTOR AS NSJSTOR,
		r.TL2 AS NSTL2,
		r.Wikidata AS NSWikidata,
		import.fnAuthorStringForImportRecord(r.ImportRecordID, '+++') AS NSAuthors,
		import.fnKeywordStringForImportRecord(r.ImportRecordID, '+++') AS NSKeywords,
		import.fnContributorStringForImportRecord(r.ImportRecordID, '+++') AS NSContributors,
		import.fnPageStringForImportRecord(r.ImportRecordID, '+++') AS NSPages,
		-- Existing segment info (ES = existing segment)
		eg.GenreName AS ESGenre,
		es.Title AS ESTitle, 
		es.TranslatedTitle AS ESTranslatedTitle,
		es.ContainerTitle AS ESJournalTitle,
		es.Volume AS ESVolume,
		es.Series AS ESSeries,
		es.Issue AS ESIssue,
		es.Edition AS ESEdition,
		es.PublicationDetails AS ESPublicationDetails,
		es.PublisherName AS ESPublisherName,
		es.PublisherPlace AS ESPublisherPlace,
		es.[Date] AS ESYear,
		el.LanguageName AS ESLanguage,
		es.Summary AS ESSummary,
		vs.Notes AS ESNotes,
		es.RightsStatement AS ESRights,
		es.RightsStatus AS ESCopyrightStatus,
		es.LicenseName AS ESLicense,
		es.LicenseUrl AS ESLicenseUrl,
		es.PageRange AS ESPageRange,
		es.StartPageNumber AS ESStartPage,
		es.StartPageID AS ESStartPageID,
		es.EndPageNumber AS ESEndPage,
		CAST(NULL AS int) AS ESEndPageID,
		es.Url AS ESUrl,
		es.DownloadUrl AS ESDownloadUrl,
		d.DOIName AS ESDOI,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'ISSN', 0) AS ESISSN,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'ISBN', 0) AS ESISBN,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'OCLC', 0) AS ESOCLC,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'DLC', 0) AS ESLCCN,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'ARK', 0) AS ESARK,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'Biostor', 0) AS ESBiostor,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'JSTOR', 0) AS ESJSTOR,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'TL2', 0) AS ESTL2,
		dbo.fnGetIdentifierForSegment(es.SegmentID, 'Wikidata', 0) AS ESWikidata,
		dbo.fnAuthorInfoForSegment(es.SegmentID, '+++') AS ESAuthors,
		dbo.fnKeywordStringforSegment(es.SegmentID) AS ESKeywords,
		dbo.fnContributorCodesForSegment(es.SegmentID) AS ESContributors,
		dbo.fnPageStringForSegment(es.SegmentID) AS ESPages
FROM	import.ImportRecord r
		LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID
		LEFT JOIN dbo.Book b ON r.ItemID = b.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID
		LEFT JOIN dbo.Segment es ON r.ImportSegmentID = es.SegmentID
		LEFT JOIN dbo.vwSegment vs ON r.ImportSegmentID = vs.SegmentID 
		LEFT JOIN dbo.SegmentGenre eg ON es.SegmentGenreID = eg.SegmentGenreID
		LEFT JOIN dbo.Language el ON es.LanguageCode = el.LanguageCode
		LEFT JOIN dbo.DOI d ON es.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40 AND d.IsValid = 1
		LEFT JOIN dbo.Book eb on vs.BookID = eb.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle ept ON eb.ItemID = ept.ItemID
		LEFT JOIN dbo.Title et ON ept.TitleID = et.TitleID
WHERE	r.ImportSegmentID IS NOT NULL
AND		r.ImportRecordID = @ImportRecordID

END

GO
