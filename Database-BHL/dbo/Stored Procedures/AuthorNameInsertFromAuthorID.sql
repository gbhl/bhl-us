CREATE PROCEDURE [dbo].[AuthorNameInsertFromAuthorID]

@FromAuthorID int,
@ToAuthorID int,
@UserID int

AS

BEGIN

/*
 *	"Copy" any author names associated with the FromAuthorID to the ToAuthorID.  Only 
 * copy author names that are not already associated with the destination author.
 */

SET NOCOUNT ON

INSERT	dbo.AuthorName (AuthorID, FullName, LastName, FirstName, FullerForm, IsPreferredName, CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
SELECT	@ToAuthorID, f.FullName, f.LastName, f.FirstName, f.FullerForm, 0, GETDATE(), GETDATE(), @UserID, @UserID
FROM	(SELECT AuthorNameID, FullName, LastName, FirstName, FullerForm FROM dbo.AuthorName WHERE AuthorID = @FromAuthorID) f
		LEFT JOIN (SELECT AuthorNameID, FullName, LastName, FirstName, FullerForm FROM dbo.AuthorName WHERE AuthorID = @ToAuthorID) t
			ON dbo.fnFilterString(f.FullName, '[a-zA-Z]', '') = dbo.fnFilterString(t.FullName, '[a-zA-Z]', '')
			AND dbo.fnFilterString(f.LastName, '[a-zA-Z]', '') = dbo.fnFilterString(t.LastName, '[a-zA-Z]', '')
			AND dbo.fnFilterString(f.FirstName, '[a-zA-Z]', '') = dbo.fnFilterString(t.FirstName, '[a-zA-Z]', '')
			AND dbo.fnFilterString(f.FullerForm, '[a-zA-Z]', '') = dbo.fnFilterString(t.FullerForm, '[a-zA-Z]', '')
WHERE	t.AuthorNameID IS NULL

END
