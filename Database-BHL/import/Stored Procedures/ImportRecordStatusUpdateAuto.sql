CREATE PROCEDURE [import].[ImportRecordStatusUpdateAuto]

@ImportRecordStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordStatus]

SET

	[ImportRecordStatusID] = @ImportRecordStatusID,
	[StatusName] = @StatusName,
	[StatusDescription] = @StatusDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[ImportRecordStatusID] = @ImportRecordStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ImportRecordStatusID],
		[StatusName],
		[StatusDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [import].[ImportRecordStatus]
	
	WHERE
		[ImportRecordStatusID] = @ImportRecordStatusID
	
	RETURN -- update successful
END


GO
