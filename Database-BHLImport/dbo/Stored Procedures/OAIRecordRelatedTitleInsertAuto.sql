CREATE PROCEDURE OAIRecordRelatedTitleInsertAuto

@OAIRecordRelatedTitleID INT OUTPUT,
@OAIRecordID INT,
@TitleType NVARCHAR(50),
@Title NVARCHAR(300),
@ProductionTitleAssociationID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordRelatedTitle]
(
	[OAIRecordID],
	[TitleType],
	[Title],
	[ProductionTitleAssociationID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@TitleType,
	@Title,
	@ProductionTitleAssociationID,
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
		[ProductionTitleAssociationID],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[OAIRecordRelatedTitle]
	
	WHERE
		[OAIRecordRelatedTitleID] = @OAIRecordRelatedTitleID
	
	RETURN -- insert successful
END

GO
