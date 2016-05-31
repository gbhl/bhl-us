CREATE PROCEDURE [dbo].[TitleSelectByCollection]

@CollectionID int

AS

BEGIN

SELECT	t.TitleID,
		t.ShortTitle,
		t.PublicationDetails,
		dbo.fnContributorStringForTitle(t.TitleID, 1) AS InstitutionName
FROM	dbo.Title t 
		INNER JOIN dbo.TitleCollection tc ON t.TitleID = tc.TitleID
WHERE	tc.CollectionID = @CollectionID
ORDER BY
		t.FullTitle

END
