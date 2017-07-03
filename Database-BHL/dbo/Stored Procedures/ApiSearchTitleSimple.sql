CREATE PROCEDURE [dbo].[ApiSearchTitleSimple]

@FullTitle varchar(4000)

AS 

BEGIN

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.ApiTitleSelectSearchSimple @FullTitle
	RETURN
END

-- Transform the search term into a full-text search phrase
DECLARE @SearchCondition nvarchar(4000)
SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@FullTitle)

SELECT DISTINCT
		t.TitleID,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		t.EditionStatement,
		t.CurrentPublicationFrequency
FROM	dbo.SearchCatalog s 
		INNER JOIN dbo.Title t ON s.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
WHERE	CONTAINS(s.FullTitle, @SearchCondition)
AND		t.PublishReady = 1
ORDER BY t.SortTitle

END
