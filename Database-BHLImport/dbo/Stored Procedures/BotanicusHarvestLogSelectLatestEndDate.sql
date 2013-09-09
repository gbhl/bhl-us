CREATE PROCEDURE [dbo].[BotanicusHarvestLogSelectLatestEndDate]

AS 

SET NOCOUNT ON

SELECT	
	ISNULL(MAX(HarvestEndDate), '1/1/1980') AS HarvestEndDate
FROM
	dbo.BotanicusHarvestLog
WHERE	
	AutomaticHarvest = 1
AND	SuccessfulHarvest = 1

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogSelectLatestEndDate. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

