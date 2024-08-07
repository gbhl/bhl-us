SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentAuthorUpdateAuthorID]

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

UPDATE	dbo.ItemAuthor
SET		AuthorID = @ToAuthorID,
		LastModifiedDate = GETDATE(),
		LastModifiedUserID = @LastModifiedUserID
WHERE	AuthorID = @FromAuthorID

END


GO
