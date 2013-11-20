CREATE PROCEDURE OAIRecordRelatedTitleUpdateAuto

@OAIRecordRelatedTitleID INT,
@OAIRecordID INT,
@TitleType NVARCHAR(50),
@Title NVARCHAR(300),
@ProductionEntityType NVARCHAR(15),
@ProductionEntityID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordRelatedTitle]

SET

	[OAIRecordID] = @OAIRecordID,
	[TitleType] = @TitleType,
	[Title] = @Title,
	[ProductionEntityType] = @ProductionEntityType,
	[ProductionEntityID] = @ProductionEntityID,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordRelatedTitleID] = @OAIRecordRelatedTitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRelatedTitleUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

GO
