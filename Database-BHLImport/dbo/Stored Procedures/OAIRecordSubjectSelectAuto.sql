CREATE PROCEDURE OAIRecordSubjectSelectAuto

@OAIRecordSubjectID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordSubjectSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
