CREATE PROCEDURE OAIRecordCreatorSelectAuto

@OAIRecordCreatorID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordCreatorID],
	[OAIRecordID],
	[FullName],
	[Dates],
	[ProductionAuthorID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[OAIRecordCreator]

WHERE
	[OAIRecordCreatorID] = @OAIRecordCreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordCreatorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
