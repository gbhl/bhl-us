CREATE PROCEDURE OAIHarvestLogUpdateAuto

@HarvestLogID INT,
@HarvestSetID INT,
@HarvestStartDateTime DATETIME,
@FromDateTime DATETIME,
@UntilDateTime DATETIME,
@ResponseDateTime DATETIME,
@Result NVARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIHarvestLog]

SET

	[HarvestSetID] = @HarvestSetID,
	[HarvestStartDateTime] = @HarvestStartDateTime,
	[FromDateTime] = @FromDateTime,
	[UntilDateTime] = @UntilDateTime,
	[ResponseDateTime] = @ResponseDateTime,
	[Result] = @Result,
	[LastModifiedDate] = getdate()

WHERE
	[HarvestLogID] = @HarvestLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIHarvestLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[OAIHarvestLog]
	
	WHERE
		[HarvestLogID] = @HarvestLogID
	
	RETURN -- update successful
END

