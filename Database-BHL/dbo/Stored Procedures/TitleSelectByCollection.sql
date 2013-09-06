
CREATE PROCEDURE [dbo].[TitleSelectByCollection]

@CollectionID int

AS

BEGIN

SELECT	t.TitleID,
		t.ShortTitle,
		t.PublicationDetails,
		i.InstitutionName
FROM	dbo.Title t INNER JOIN dbo.TitleCollection tc
			ON t.TitleID = tc.TitleID
		LEFT JOIN dbo.Institution i
			ON t.InstitutionCode = i.InstitutionCode
WHERE	tc.CollectionID = @CollectionID
ORDER BY
		t.FullTitle

END



