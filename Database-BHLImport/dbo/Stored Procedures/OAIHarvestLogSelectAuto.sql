CREATE PROCEDURE OAIHarvestLogSelectAuto

@HarvestLogID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIHarvestLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

