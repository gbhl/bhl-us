CREATE PROCEDURE OAIRecordStatusSelectAuto

@OAIRecordStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordStatusID],
	[RecordStatus],
	[StatusDescription]

FROM [dbo].[OAIRecordStatus]

WHERE
	[OAIRecordStatusID] = @OAIRecordStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

