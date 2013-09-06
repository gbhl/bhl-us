
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
	SET @Language = @LanguageCode
	SET @Institution = @InstitutionCode

	SELECT DISTINCT TOP (@NumRows) t.TitleID, 
			i.ItemID, t.ShortTitle, i.Volume, i.ScanningDate, 
			i.CreationDate,	i.Sponsor, inst.InstitutionName, 
			i.LicenseUrl, i.Rights, i.DueDiligence, 
			i.CopyrightStatus, i.CopyrightRegion,
			i.CopyrightComment, i.CopyrightEvidence,
			t.PublicationDetails, t.CallNumber 
	INTO	#tmpRecent
	FROM	dbo.Item i WITH (NOLOCK) INNER JOIN dbo.Title t WITH (NOLOCK)
				ON t.TitleID = i.PrimaryTitleID
			LEFT JOIN dbo.Institution inst WITH (NOLOCK)
				ON i.InstitutionCode = inst.InstitutionCode
			LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK)
				ON i.ItemID = il.ItemID
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40
	AND		(i.InstitutionCode = @Institution OR 
			@Institution = '')
	AND		(i.LanguageCode = @Language OR
			 ISNULL(il.LanguageCode, '') = @Language OR
			@Language = '')
	ORDER BY i.CreationDate DESC

	SELECT	t.ItemID, t.ShortTitle, t.Volume, t.ScanningDate, 
			t.CreationDate, t.Sponsor, t.InstitutionName, 
			t.LicenseUrl, t.Rights, t.DueDiligence, 
			t.CopyrightStatus, t.CopyrightRegion,
			t.CopyrightComment, t.CopyrightEvidence,
			t.PublicationDetails, t.CallNumber, 
			dbo.fnAuthorStringForTitle(t.TitleID) AS CreatorTextString,
			c.Subjects AS KeywordString,
			c.Associations AS AssociationTextString
	FROM	#tmpRecent t
			INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND t.ItemID = c.ItemID
	ORDER BY CreationDate DESC
END



