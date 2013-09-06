
CREATE PROCEDURE [dbo].[ApiTitleSelectSearchSimple]

@FullTitle varchar(1000)

AS 

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
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
FROM	dbo.Title t LEFT JOIN dbo.BibliographicLevel b
			ON t.BibliographicLevelID = b.BibliographicLevelID
WHERE	t.FullTitle LIKE '%' + @FullTitle + '%'
AND		t.PublishReady = 1
ORDER BY t.SortTitle

END



