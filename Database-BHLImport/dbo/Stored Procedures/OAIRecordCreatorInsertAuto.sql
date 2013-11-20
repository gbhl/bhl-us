CREATE PROCEDURE OAIRecordCreatorInsertAuto

@OAIRecordCreatorID INT OUTPUT,
@OAIRecordID INT,
@CreatorType NVARCHAR(50),
@FullName NVARCHAR(300),
@Dates NVARCHAR(50),
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@ProductionAuthorID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordCreator]
(
	[OAIRecordID],
	[CreatorType],
	[FullName],
	[Dates],
	[StartDate],
	[EndDate],
	[ProductionAuthorID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@CreatorType,
	@FullName,
	@Dates,
	@StartDate,
	@EndDate,
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
		[CreatorType],
		[FullName],
		[Dates],
		[StartDate],
		[EndDate],
		[ProductionAuthorID],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[OAIRecordCreator]
	
	WHERE
		[OAIRecordCreatorID] = @OAIRecordCreatorID
	
	RETURN -- insert successful
END

GO
 
