CREATE PROCEDURE dbo.WDEntityIdentifierPublishAuthorIDs

AS

BEGIN

INSERT	dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue)
SELECT	a.AuthorID, 
		i.IdentifierID, 
		w.IdentifierValue
FROM	dbo.WDEntityIdentifier w
		INNER JOIN dbo.BHLAuthor a ON w.BHLEntityID = a.AuthorID
		INNER JOIN dbo.BHLIdentifier i ON w.IdentifierType = i.IdentifierName
		LEFT JOIN dbo.BHLAuthorIdentifier ai ON a.AuthorID = ai.AuthorID AND i.IdentifierID = ai.IdentifierID AND w.IdentifierValue = ai.IdentifierValue
WHERE	w.BHLEntityType = 'Author'
AND		ai.AuthorIdentifierID IS NULL

END
GO
