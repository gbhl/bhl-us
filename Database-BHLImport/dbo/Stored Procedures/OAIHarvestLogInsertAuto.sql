CREATE PROCEDURE OAIHarvestLogInsertAuto

@HarvestLogID INT OUTPUT,
@HarvestSetID INT,
@HarvestStartDateTime DATETIME = null,
@FromDateTime DATETIME = null,
@UntilDateTime DATETIME = null,
@ResponseDateTime DATETIME = null,
@Result NVARCHAR(200),
@NumberHarvested INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIHarvestLog]
(
	[HarvestSetID],
	[HarvestStartDateTime],
	[FromDateTime],
	[UntilDateTime],
	[ResponseDateTime],
	[Result],
	[NumberHarvested],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@HarvestSetID,
	@HarvestStartDateTime,
	@FromDateTime,
	@UntilDateTime,
	@ResponseDateTime,
	@Result,
	@NumberHarvested,
	getdate(),
	getdate()
)

SET @HarvestLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIHarvestLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[HarvestLogID],
		[HarvestSetID],
		[HarvestStartDateTime],
		[FromDateTime],
		[UntilDateTime],
		[ResponseDateTime],
		[Result],
		[NumberHarvested],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[OAIHarvestLog]
	
	WHERE
		[HarvestLogID] = @HarvestLogID
	
	RETURN -- insert successful
END
