
CREATE PROCEDURE [dbo].[ApiTitleSelectAuto]

@TitleID INT

AS 

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.ApiTitleSelectAuto @RedirID
ELSE
	SELECT	t.[TitleID],
			ISNULL(b.[BibliographicLevelName], '') AS BibliographicLevelName,
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
			t.[InstitutionCode],
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
			d.[DOIName]
	FROM	[dbo].[Title] t LEFT JOIN [dbo].[BibliographicLevel] b
				ON t.BibliographicLevelID = b.BibliographicLevelID
			LEFT JOIN [dbo].[DOI] d
				ON t.TitleID = d.EntityID
				AND d.IsValid = 1
			LEFT JOIN [dbo].[DOIEntityType] et
				ON d.DOIEntityTypeID = et.DOIEntityTypeID
				AND et.DOIEntityTypeName = 'Title'
	WHERE	t.[TitleID] = @TitleID
	AND		t.[PublishReady] = 1


