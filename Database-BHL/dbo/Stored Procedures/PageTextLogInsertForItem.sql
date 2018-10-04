CREATE PROCEDURE dbo.PageTextLogInsertForItem

@ItemID int,
@TextSource nvarchar(50),
@UserID int

AS

BEGIN

SET NOCOUNT ON

INSERT	dbo.PageTextLog (PageID, TextSource, CreationUserID)
SELECT	PageID,
		@TextSource,
		@UserID
FROM	dbo.Page
WHERE	Active = 1
AND		ItemID = @ItemID

END
