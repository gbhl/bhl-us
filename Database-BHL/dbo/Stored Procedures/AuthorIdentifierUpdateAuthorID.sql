CREATE PROCEDURE [dbo].[AuthorIdentifierUpdateAuthorID]

@FromAuthorID int,
@ToAuthorID int,
@UserID int

AS

BEGIN

/*
 *	"Move" any identifiers associated with the FromAuthorID to the ToAuthorID by changing
 * the AuthorID values.  Only move identifiers that are not already associated with the 
 * destination author.
 */

SET NOCOUNT ON

UPDATE	dbo.AuthorIdentifier
SET		AuthorID = @ToAuthorID,
		LastModifiedDate = GETDATE(),
		LastModifiedUserID = @UserID
WHERE	AuthorIdentifierID IN (
		SELECT	f.AuthorIdentifierID
		FROM	(SELECT AuthorIdentifierID, IdentifierID, IdentifierValue FROM dbo.AuthorIdentifier WHERE AuthorID = @FromAuthorID) f
				LEFT JOIN (SELECT AuthorIdentifierID, IdentifierID, IdentifierValue FROM dbo.AuthorIdentifier WHERE AuthorID = @ToAuthorID) t
					ON f.IdentifierID = t.IdentifierID
					AND f.IdentifierValue = t.IdentifierValue
		WHERE	t.AuthorIdentifierID IS NULL
		)

END