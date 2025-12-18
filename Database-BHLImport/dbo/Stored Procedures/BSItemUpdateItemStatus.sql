CREATE PROCEDURE dbo.BSItemUpdateItemStatus

@ItemID int,
@ItemStatusID int

AS

BEGIN

SET NOCOUNT ON

UPDATE	dbo.BSItem
SET		ItemStatusID = @ItemStatusID,
		LastModifiedDate = GETDATE()
WHERE	ItemID = @ItemID

END
GO
