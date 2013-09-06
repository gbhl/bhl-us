
CREATE PROCEDURE [dbo].[ApiTitleIdentifierSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.IdentifierName,
		ti.IdentifierValue
FROM	dbo.Title t INNER JOIN dbo.Title_Identifier ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
WHERE	t.PublishReady = 1
AND		t.TitleID = @TitleID
ORDER BY i.IdentifierName

END

