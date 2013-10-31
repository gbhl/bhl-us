CREATE PROCEDURE OAIRecordCreatorInsertAuto

@OAIRecordCreatorID INT OUTPUT,
@OAIRecordID INT,
@FullName NVARCHAR(300),
@Dates NVARCHAR(50),
@ProductionAuthorID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordCreator]
(
	[OAIRecordID],
	[FullName],
	[Dates],
	[ProductionAuthorID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@FullName,
	@Dates,
	@ProductionAuthorID,
	getdate(),
	getdate()
)

SET @OAIRecordCreatorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordCreatorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

