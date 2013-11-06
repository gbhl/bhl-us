CREATE PROCEDURE dbo.OAIRecordSelectForOAIIdentifierAndStatus

@OAIIdentifier nvarchar(100),
@OAIRecordStatusID int

AS

BEGIN

SET NOCOUNT ON 

SELECT	OAIRecordID
FROM	dbo.OAIRecord
WHERE	OAIIdentifier = @OAIIdentifier
AND		OAIRecordStatusID = @OAIRecordStatusID

END

GO