CREATE PROCEDURE [import].[ImportRecordStatusInsertAuto]

@ImportRecordStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordStatus]
(
	[ImportRecordStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@ImportRecordStatusID,
	@StatusName,
	@StatusDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END


GO
