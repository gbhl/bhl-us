CREATE PROCEDURE OAIRecordRelatedTitleSelectAuto

@OAIRecordRelatedTitleID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordRelatedTitleID],
	[OAIRecordID],
	[TitleType],
	[Title],
	[ProductionEntityType],
	[ProductionEntityID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[OAIRecordRelatedTitle]

WHERE
	[OAIRecordRelatedTitleID] = @OAIRecordRelatedTitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRelatedTitleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
