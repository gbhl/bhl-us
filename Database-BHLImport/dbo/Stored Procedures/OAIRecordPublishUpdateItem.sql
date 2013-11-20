CREATE PROCEDURE dbo.OAIRecordPublishUpdateItem

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionItemID int
DECLARE @CallNumber nvarchar(100)
DECLARE @Date nvarchar(100)
DECLARE @Volume nvarchar(100)
DECLARE @LanguageCode nvarchar(10)
DECLARE @Url nvarchar(200)
DECLARE @Right nvarchar(500)

-- Get the values to be updated		
SELECT	@ProductionItemID = o.ProductionItemID,
		@CallNumber = o.CallNumber,
		@Volume = o.Volume,
		@LanguageCode = l.BHLLanguageCode,
		@Date = o.Date,
		@Url = o.Url
FROM	dbo.OAIRecord o LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
WHERE	o.OAIRecordID = @OAIRecordID

SET @Right = ''
SELECT TOP 1 @Right = [Right] FROM dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID

-- Update item
UPDATE	dbo.BHLItem
SET		CallNumber = @CallNumber,
		Volume = @Volume,
		LanguageCode = @LanguageCode,
		Year = @Date,
		Rights = @Right,
		ExternalUrl = @Url
WHERE	ItemID = @ProductionItemID
AND		LastModifiedUserID = 1

END

GO

