CREATE PROCEDURE [dbo].[ApiTitleSelectAuto]

@TitleID INT

AS 

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.ApiTitleSelectAuto @RedirID
ELSE
	DECLARE @IdentifierIDDOI int
	SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

	SELECT	t.[TitleID],
			ISNULL(b.[BibliographicLevelName], '') AS BibliographicLevelName,
			ISNULL(m.[MaterialTypeLabel], '') AS MaterialTypeLabel,
			t.[MARCBibID],
			t.[MARCLeader],
			t.[TropicosTitleID],
			t.[RedirectTitleID],
			t.[FullTitle],
			t.[ShortTitle],
			t.[UniformTitle],
			t.[SortTitle],
			t.[PartNumber],
			t.[PartName],
			t.[CallNumber],
			t.[PublicationDetails],
			t.[StartYear],
			t.[EndYear],
			t.[Datafield_260_a],
			t.[Datafield_260_b],
			t.[Datafield_260_c],
			t.[LanguageCode],
			t.[TitleDescription],
			t.[TL2Author],
			t.[PublishReady],
			t.[RareBooks],
			t.[Note],
			t.[CreationDate],
			t.[LastModifiedDate],
			t.[CreationUserID],
			t.[LastModifiedUserID],
			t.[OriginalCatalogingSource],
			t.[EditionStatement],
			t.[CurrentPublicationFrequency],
			ti.IdentifierValue AS DOIName
	FROM	[dbo].[Title] t 
			LEFT JOIN [dbo].[BibliographicLevel] b ON t.BibliographicLevelID = b.BibliographicLevelID
			LEFT JOIN [dbo].[MaterialType] m ON t.MaterialTypeID = m.MaterialTypeID
			LEFT JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @IdentifierIDDOI
	WHERE	t.[TitleID] = @TitleID
	AND		t.[PublishReady] = 1

GO
