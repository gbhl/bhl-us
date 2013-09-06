
CREATE PROCEDURE [dbo].[ApiTitleSelectByAuthor]

@AuthorID int

AS

SET NOCOUNT ON

-- Get titles tied directly to the specified author
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
FROM	dbo.TitleAuthor ta INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
WHERE	ta.AuthorID = @AuthorID
AND		t.PublishReady = 1


