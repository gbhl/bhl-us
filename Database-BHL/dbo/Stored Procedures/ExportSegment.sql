CREATE PROCEDURE [dbo].[ExportSegment]

AS

BEGIN

SET NOCOUNT ON

DECLARE @RightsHolderRoleID int
SELECT @RightsHolderRoleID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Rights Holder'

-- Get the initial dataset
SELECT DISTINCT
		s.SegmentID, 
		sc.ItemID,
		b.ItemID AS BookItemID,
		ISNULL(isrc.SourceName, '') AS SourceName,
		ISNULL(s.Barcode, '') AS Barcode,
		c.Contributors AS ContributorName, 
		s.SequenceOrder, 
		g.GenreName AS SegmentType, 
		s.Title, 
		RTRIM(s.ContainerTitle) +
			RTRIM(CASE WHEN ISNULL(s.ContainerTitlePartNumber, '') <> '' THEN '. ' + s.ContainerTitlePartNumber ELSE '' END) +
			RTRIM(CASE WHEN ISNULL(s.ContainerTitlePartNumber, '') <> '' AND ISNULL(s.ContainerTitlePartName, '') <> '' THEN ', ' + s.ContainerTitlePartName ELSE '' END) +
			RTRIM(CASE WHEN ISNULL(s.ContainerTitlePartNumber, '') = '' AND ISNULL(s.ContainerTitlePartName, '') <> '' THEN '. ' + s.ContainerTitlePartName ELSE '' END) AS ContainerTitle,
        CASE WHEN s.PublicationDetails <> '' THEN s.PublicationDetails ELSE LTRIM(s.PublisherPlace + ' ' + s.PublisherName) END AS PublicationDetails, 
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
		ISNULL(CONVERT(nvarchar(20), StartPageID), '') AS StartPageID,
        ISNULL(l.LanguageName, N'') AS LanguageName, 
		'https://www.biodiversitylibrary.org/part/' + CONVERT(NVARCHAR(20), s.SegmentID) AS SegmentUrl, 
		s.Url AS ExternalUrl, 
		s.DownloadUrl, 
		s.RightsStatus, 
		s.RightsStatement, 
		s.LicenseName, 
		s.LicenseUrl,
		c.HasLocalContent,
		c.HasExternalContent,
		SPACE(255) AS RightsHolder
INTO	#segment
FROM	dbo.vwSegment s
		INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID 
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID
		LEFT JOIN dbo.SearchCatalog sc ON it.TitleID = sc.TitleID and b.BookID = sc.ItemID
		LEFT JOIN dbo.Item i ON b.ItemID = i.ItemID
		LEFT JOIN dbo.ItemSource isrc ON i.ItemSourceID = isrc.ItemSourceID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
WHERE	(i.ItemStatusID = 40 OR i.ItemStatusID IS NULL)
AND     (sc.ItemID IS NOT NULL OR ISNULL(s.Url, '') <> '')

-- Add the rights holder... first check for a container(Book) Rights Holder, then fall back to the Segment Rights Holder
UPDATE	s
SET		RightsHolder = inst.InstitutionName
FROM	#segment s
		INNER JOIN dbo.ItemInstitution ii ON s.BookItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = @RightsHolderRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode		

UPDATE	s
SET		RightsHolder = inst.InstitutionName
FROM	#segment s
		INNER JOIN dbo.ItemInstitution ii ON s.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = @RightsHolderRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
WHERE	LTRIM(RTRIM(s.RightsHolder)) = ''

-- Return the final dataset
SELECT	SegmentID, 
		ItemID,
		SourceName,
		Barcode,
		ContributorName, 
		SequenceOrder, 
		SegmentType, 
		Title, 
		ContainerTitle,
        PublicationDetails, 
        Volume, 
		Series, 
		Issue, 
		Date, 
		PageRange,
		StartPageID,
        LanguageName, 
		SegmentUrl, 
		ExternalUrl, 
		DownloadUrl, 
		RightsStatus, 
		RightsStatement, 
		LicenseName, 
		LicenseUrl,
		HasLocalContent,
		HasExternalContent,
		RTRIM(LTRIM(RightsHolder)) AS RightsHolder
FROM	#segment

END

GO
