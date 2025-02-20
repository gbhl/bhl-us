﻿CREATE PROCEDURE dbo.TitleAuthorUpdateAuthorID

@FromAuthorID int,
@ToAuthorID int,
@LastModifiedUserID int

AS

BEGIN

/*
 *	"Move" any titles associated with the FromAuthorID to the ToAuthorID by changing
 * the AuthorID values.
 */

SET NOCOUNT ON

UPDATE	dbo.TitleAuthor
SET		AuthorID = @ToAuthorID,
		LastModifiedDate = GETDATE(),
		LastModifiedUserID = @LastModifiedUserID
WHERE	AuthorID = @FromAuthorID

END


