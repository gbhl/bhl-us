CREATE PROCEDURE OAIRecordSubjectInsertAuto

@OAIRecordSubjectID INT OUTPUT,
@OAIRecordID INT,
@Keyword NVARCHAR(50),
@ProductionKeywordID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordSubject]
(
	[OAIRecordID],
	[Keyword],
	[ProductionKeywordID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@Keyword,
	@ProductionKeywordID,
	getdate(),
	getdate()
)

SET @OAIRecordSubjectID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordSubjectInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[OAIRecordSubjectID],
		[OAIRecordID],
		[Keyword],
		[ProductionKeywordID],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[OAIRecordSubject]
	
	WHERE
		[OAIRecordSubjectID] = @OAIRecordSubjectID
	
	RETURN -- insert successful
END

