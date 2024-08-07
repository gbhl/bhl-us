CREATE PROCEDURE [import].[ImportRecordKeywordUpdateAuto]

@ImportRecordKeywordID INT,
@ImportRecordID INT,
@Keyword NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordKeyword]

SET

	[ImportRecordID] = @ImportRecordID,
	[Keyword] = @Keyword,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[ImportRecordKeywordID] = @ImportRecordKeywordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordKeywordUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END


GO
