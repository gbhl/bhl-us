
CREATE PROCEDURE [dbo].[ApiTitleSelectByKeyword]

@Keyword nvarchar(50)

AS
SET NOCOUNT ON

-- Get titles tied directly to the specified Keyword
SELECT DISTINCT 
		t.TitleID,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.CallNumber,
		t.EditionStatement,
		t.Datafield_260_a as PublisherPlace,
		t.Datafield_260_b as PublisherName,
		t.Datafield_260_c as PublicationDate,
		t.CurrentPublicationFrequency AS PublicationFrequency
FROM	dbo.Keyword k INNER JOIN dbo.TitleKeyword tk
			ON k.KeywordID = tk.KeywordID
		INNER JOIN dbo.Title t
			ON tk.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b
			ON t.BibliographicLevelID = b.BibliographicLevelID
WHERE	k.Keyword = @Keyword
AND		t.PublishReady=1


