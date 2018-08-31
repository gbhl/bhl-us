CREATE PROCEDURE [dbo].[ApiNameIdentifierSelectByNameResolvedID]

@NameResolvedID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.IdentifierName,
		ni.IdentifierValue
FROM	dbo.NameIdentifier ni
		INNER JOIN dbo.Identifier i ON ni.IdentifierID = i.IdentifierID
WHERE	ni.NameResolvedID = @NameResolvedID
ORDER BY i.IdentifierName

END
