CREATE PROCEDURE dbo.OAIRecordSelectForOAIIdentifierAndDateStamp

@OAIIdentifier nvarchar(100),
@OAIDateStamp nvarchar(30)

AS

BEGIN

SET NOCOUNT ON 

SELECT	OAIRecordID
FROM	dbo.OAIRecord
WHERE	OAIIdentifier = @OAIIdentifier
AND		OAIDateStamp = @OAIDateStamp

END

GO