CREATE PROCEDURE [import].[ImportRecordKeywordInsertAuto]

@ImportRecordKeywordID INT OUTPUT,
@ImportRecordID INT,
@Keyword NVARCHAR(50),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordKeyword]
(
	[ImportRecordID],
	[Keyword],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@ImportRecordID,
	@Keyword,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @ImportRecordKeywordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordKeywordInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ImportRecordKeywordID],
		[ImportRecordID],
		[Keyword],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [import].[ImportRecordKeyword]
	
	WHERE
		[ImportRecordKeywordID] = @ImportRecordKeywordID
	
	RETURN -- insert successful
END


GO
