CREATE PROCEDURE import.ImportRecordErrorLogInsertAuto

@ImportRecordErrorLogID INT OUTPUT,
@ImportRecordID INT,
@ErrorDate DATETIME,
@ErrorMessage NVARCHAR(MAX),
@CreationUserID INT,
@LastModifiedUserID INT,
@Severity NVARCHAR(40)

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordErrorLog]
( 	[ImportRecordID],
	[ErrorDate],
	[ErrorMessage],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[Severity] )
VALUES
( 	@ImportRecordID,
	@ErrorDate,
	@ErrorMessage,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@Severity )

SET @ImportRecordErrorLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordErrorLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
