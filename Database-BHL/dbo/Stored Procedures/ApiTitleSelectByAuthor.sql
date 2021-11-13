CREATE PROCEDURE [dbo].[ApiTitleSelectByAuthor]

@AuthorID int

AS

SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

DECLARE @DOIEntityTypeTitleID int
SELECT @DOIEntityTypeTitleID = DOIEntityTypeID FROM dbo.DOIEntityType WITH (NOLOCK) WHERE DOIEntityTypeName = 'Title'

-- Get titles tied directly to the specified author
SELECT DISTINCT 
		t.TitleID,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		t.FullTitle,
		t.ShortTitle,
		t.PartNumber,
		t.PartName,
		t.CallNumber,
		t.PublicationDetails,
		t.StartYear,
		t.EndYear,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		t.LanguageCode,
		t.EditionStatement,
		t.[OriginalCatalogingSource],
		t.[EditionStatement],
		t.[CurrentPublicationFrequency],
		ti.IdentifierValue AS [DOIName]
FROM	dbo.TitleAuthor ta 
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
		LEFT JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @IdentifierIDDOI
WHERE	ta.AuthorID = @AuthorID
AND		t.PublishReady = 1

GO
