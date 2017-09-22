CREATE PROCEDURE [import].[ImportRecordPageSelectAuto]

@ImportRecordPageID INT

AS 

SET NOCOUNT ON

SELECT	
	[ImportRecordPageID],
	[ImportRecordID],
	[PageID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[import].[ImportRecordPage]
WHERE	
	[ImportRecordPageID] = @ImportRecordPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
