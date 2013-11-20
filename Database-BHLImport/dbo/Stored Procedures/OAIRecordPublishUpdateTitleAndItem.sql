CREATE PROCEDURE dbo.OAIRecordPublishUpdateTitleAndItem

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionTitleID int
SELECT	@ProductionTitleID = ProductionTitleID
FROM	dbo.OAIRecord
WHERE	OAIRecordID = @OAIRecordID

BEGIN TRAN

exec dbo.OAIRecordPublishUpdateTitle @OAIRecordID, @ProductionTitleID
exec dbo.OAIRecordPublishUpdateItem @OAIRecordID

-- Update the OAIRecordStatus of the just-updated record
UPDATE	OAIRecord
SET		OAIRecordStatusID = 20, LastModifiedDate = GETDATE()
WHERE	OAIRecordID = @OAIRecordID

COMMIT TRAN

END

GO
