CREATE PROCEDURE OAIRecordStatusUpdateAuto

@OAIRecordStatusID INT,
@RecordStatus NVARCHAR(30),
@StatusDescription NVARCHAR(400)

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordStatus]

SET

	[RecordStatus] = @RecordStatus,
	[StatusDescription] = @StatusDescription

WHERE
	[OAIRecordStatusID] = @OAIRecordStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[OAIRecordStatusID],
		[RecordStatus],
		[StatusDescription]

	FROM [dbo].[OAIRecordStatus]
	
	WHERE
		[OAIRecordStatusID] = @OAIRecordStatusID
	
	RETURN -- update successful
END

GO
