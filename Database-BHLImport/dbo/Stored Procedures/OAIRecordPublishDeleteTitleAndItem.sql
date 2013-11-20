CREATE PROCEDURE dbo.OAIRecordPublishDeleteTitleAndItem

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionTitleID int
DECLARE @ProductionItemID int

SELECT	@ProductionTitleID = ProductionTitleID, 
		@ProductionItemID = ProductionItemID
FROM	dbo.OAIRecord 
WHERE	OAIRecordID = @OAIRecordID

UPDATE	dbo.BHLItem
SET		ItemStatusID = 5 -- Removed
WHERE	ItemID = @ProductionItemID

-- Only deactivate the title if there are no other active items associated with it
IF NOT EXISTS(	SELECT	i.ItemID 
				FROM	dbo.BHLTitleItem ti INNER JOIN dbo.BHLItem i ON ti.ItemID = i.ItemID
				WHERE	ti.TitleID = @ProductionTitleID
				AND		i.ItemStatusID = 40	)  -- Published
BEGIN
	UPDATE	dbo.BHLTitle
	SET		PublishReady = 0
	WHERE	TitleID = @ProductionTitleID
END

END

GO


