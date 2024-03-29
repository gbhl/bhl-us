CREATE PROCEDURE import.ImportRecordErrorLogUpdateAuto

@ImportRecordErrorLogID INT,
@ImportRecordID INT,
@ErrorDate DATETIME,
@ErrorMessage NVARCHAR(MAX),
@LastModifiedUserID INT,
@Severity NVARCHAR(40)

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordErrorLog]
SET
	[ImportRecordID] = @ImportRecordID,
	[ErrorDate] = @ErrorDate,
	[ErrorMessage] = @ErrorMessage,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[Severity] = @Severity
WHERE
	[ImportRecordErrorLogID] = @ImportRecordErrorLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordErrorLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ImportRecordErrorLogID],
		[ImportRecordID],
		[ErrorDate],
		[ErrorMessage],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[Severity]
	FROM [import].[ImportRecordErrorLog]
	WHERE
		[ImportRecordErrorLogID] = @ImportRecordErrorLogID
	
	RETURN -- update successful
END
