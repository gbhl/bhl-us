CREATE PROCEDURE import.ImportRecordPageUpdateAuto

@ImportRecordPageID INT,
@ImportRecordID INT,
@PageID INT,
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordPage]
SET
	[ImportRecordID] = @ImportRecordID,
	[PageID] = @PageID,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ImportRecordPageID] = @ImportRecordPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
