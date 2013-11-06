CREATE PROCEDURE OAIRecordStatusInsertAuto

@OAIRecordStatusID INT OUTPUT,
@RecordStatus NVARCHAR(30),
@StatusDescription NVARCHAR(400)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordStatus]
(
	[RecordStatus],
	[StatusDescription]
)
VALUES
(
	@RecordStatus,
	@StatusDescription
)

SET @OAIRecordStatusID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

