CREATE PROCEDURE import.ImportRecordPageInsertAuto

@ImportRecordPageID INT OUTPUT,
@ImportRecordID INT,
@PageID INT,
@SequenceOrder SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordPage]
( 	[ImportRecordID],
	[PageID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ImportRecordID,
	@PageID,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ImportRecordPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ImportRecordPageID],
		[ImportRecordID],
		[PageID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [import].[ImportRecordPage]
	WHERE
		[ImportRecordPageID] = @ImportRecordPageID
	
	RETURN -- insert successful
END
