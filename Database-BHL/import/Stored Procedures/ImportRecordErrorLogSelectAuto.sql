CREATE PROCEDURE [import].[ImportRecordErrorLogSelectAuto]

@ImportRecordErrorLogID INT

AS 

SET NOCOUNT ON

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
FROM	
	[import].[ImportRecordErrorLog]
WHERE	
	[ImportRecordErrorLogID] = @ImportRecordErrorLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordErrorLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
