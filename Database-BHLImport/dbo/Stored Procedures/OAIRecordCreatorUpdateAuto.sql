CREATE PROCEDURE OAIRecordCreatorUpdateAuto

@OAIRecordCreatorID INT,
@OAIRecordID INT,
@CreatorType NVARCHAR(50),
@FullName NVARCHAR(300),
@Dates NVARCHAR(50),
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@ProductionAuthorID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordCreator]

SET

	[OAIRecordID] = @OAIRecordID,
	[CreatorType] = @CreatorType,
	[FullName] = @FullName,
	[Dates] = @Dates,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate,
	[ProductionAuthorID] = @ProductionAuthorID,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordCreatorID] = @OAIRecordCreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordCreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
 
