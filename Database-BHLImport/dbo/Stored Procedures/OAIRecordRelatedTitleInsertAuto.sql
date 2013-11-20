CREATE PROCEDURE OAIRecordRelatedTitleInsertAuto

@OAIRecordRelatedTitleID INT OUTPUT,
@OAIRecordID INT,
@TitleType NVARCHAR(50),
@Title NVARCHAR(300),
@ProductionEntityType NVARCHAR(15) = null,
@ProductionEntityID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordRelatedTitle]
(
	[OAIRecordID],
	[TitleType],
	[Title],
	[ProductionEntityType],
	[ProductionEntityID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@TitleType,
	@Title,
	@ProductionEntityType,
	@ProductionEntityID,
	getdate(),
	getdate()
)

SET @OAIRecordRelatedTitleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRelatedTitleInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

GO
