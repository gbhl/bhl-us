CREATE PROCEDURE [dbo].[AuthorIdentifierSelectByAuthorID]

@AuthorID int

AS 

SET NOCOUNT ON

SELECT	ai.AuthorIdentifierID,
		ai.AuthorID,
		ai.IdentifierID,
		i.IdentifierName,
		i.IdentifierLabel,
		i.Prefix,
		ai.IdentifierValue,
		ai.CreationDate,
		ai.LastModifiedDate,
		ai.CreationUserID,
		ai.LastModifiedUserID
FROM	dbo.AuthorIdentifier ai INNER JOIN dbo.Identifier i
			ON ai.IdentifierID = i.IdentifierID
WHERE	AuthorID = @AuthorID
ORDER BY
		i.IdentifierName,
		ai.IdentifierValue
		
GO
