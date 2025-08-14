CREATE PROCEDURE dbo.WDEntityIdentifierPublishAuthorIDs

AS

BEGIN

-- Get the IDs to add to production
SELECT	a.AuthorID, 
		i.IdentifierID, 
		w.IdentifierValue
INTO	#AuthorIDs
FROM	dbo.WDEntityIdentifier w
		INNER JOIN dbo.BHLAuthor a ON w.BHLEntityID = a.AuthorID
		INNER JOIN dbo.BHLIdentifier i ON w.IdentifierType = i.IdentifierName
		LEFT JOIN dbo.BHLAuthorIdentifier ai ON a.AuthorID = ai.AuthorID AND i.IdentifierID = ai.IdentifierID AND w.IdentifierValue = ai.IdentifierValue
WHERE	w.BHLEntityType = 'Author'
AND		ai.AuthorIdentifierID IS NULL

-- Add the IDs to production
INSERT	dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue)
SELECT	AuthorID, IdentifierID, IdentifierValue FROM #AuthorIDs

-- Return the list of newly added IDs
SELECT AuthorID, IdentifierID, IdentifierValue FROM #AuthorIDs

END
GO
