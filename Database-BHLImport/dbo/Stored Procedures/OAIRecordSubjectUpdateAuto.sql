CREATE PROCEDURE OAIRecordSubjectUpdateAuto

@OAIRecordSubjectID INT,
@OAIRecordID INT,
@Keyword NVARCHAR(50),
@ProductionKeywordID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordSubject]

SET

	[OAIRecordID] = @OAIRecordID,
	[Keyword] = @Keyword,
	[ProductionKeywordID] = @ProductionKeywordID,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordSubjectID] = @OAIRecordSubjectID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordSubjectUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

