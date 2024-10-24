CREATE PROCEDURE [dbo].[ItemSelectRecent]

@Top INT = 25,
@LanguageCode NVARCHAR(10) = '',
@InstitutionCode NVARCHAR(10) = ''

AS
BEGIN
	SET NOCOUNT ON
	
	-- Add these variables to "re-write" the inputs because the SQL optimizer 
	-- kept creating bad plans for this procedure
	DECLARE @NumRows int
	DECLARE @Language nvarchar(10)
	DECLARE @Institution nvarchar(10)
	
	SET @NumRows = @Top
	SET @Language = CASE WHEN LOWER(@LanguageCode) = 'all' THEN '' ELSE @LanguageCode END
	SET @Institution = @InstitutionCode

	SELECT	i.ItemID, i.CreationDate
	INTO	#tmpItem
	FROM	dbo.Item i
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			LEFT JOIN dbo.ItemLanguage il ON i.ItemID = il.ItemID
			LEFT JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
	WHERE	i.ItemStatusID = 40
	AND		(ii.InstitutionCode = @Institution OR 
			@Institution = '')
	AND		(b.LanguageCode = @Language OR
			 ISNULL(il.LanguageCode, '') = @Language OR
			@Language = '')

	SELECT DISTINCT TOP (@NumRows) t.TitleID, 
			b.BookID AS ItemID, t.FullTitle, t.ShortTitle, t.PartNumber, 
			t.PartName, b.Volume, b.ScanningDate, 
			i.CreationDate,	b.Sponsor, 
			b.LicenseUrl, b.Rights, b.DueDiligence, 
			b.CopyrightStatus, '' AS CopyrightRegion,
			'' AS CopyrightComment, '' AS CopyrightEvidence,
			t.PublicationDetails, t.CallNumber,
			b.ExternalUrl 
	INTO	#tmpRecent
	FROM	#tmpItem i
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
			INNER JOIN dbo.Title t ON t.TitleID = it.TitleID
	WHERE	t.PublishReady = 1
	ORDER BY i.CreationDate DESC

	SELECT	t.ItemID, t.FullTitle, t.ShortTitle, 
			ISNULL(t.PartNumber, '') AS PartNumber,
			ISNULL(t.PartName, '') AS PartName, 
			t.Volume, t.ScanningDate, 
			t.CreationDate, t.Sponsor,
			t.LicenseUrl, t.Rights, t.DueDiligence, 
			t.CopyrightStatus, t.CopyrightRegion,
			t.CopyrightComment, t.CopyrightEvidence,
			t.PublicationDetails, t.CallNumber, 
			dbo.fnAuthorStringForTitle(t.TitleID) AS CreatorTextString,
			c.Subjects AS KeywordString,
			c.Associations AS AssociationTextString,
			c.ItemContributors AS ContributorTextString,
			CASE WHEN c.HasLocalContent = 0 AND c.HasExternalContent = 1 THEN t.ExternalUrl ELSE '' END AS ExternalUrl
	FROM	#tmpRecent t
			INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND t.ItemID = c.ItemID
	ORDER BY CreationDate DESC
END

GO
