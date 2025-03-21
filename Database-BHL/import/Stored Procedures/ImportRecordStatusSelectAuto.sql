CREATE PROCEDURE [import].[ImportRecordStatusSelectAuto]

@ImportRecordStatusID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
