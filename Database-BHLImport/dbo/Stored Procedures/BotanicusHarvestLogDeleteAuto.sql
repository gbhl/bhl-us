
-- BotanicusHarvestLogDeleteAuto PROCEDURE
-- Generated 1/17/2008 3:54:35 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for BotanicusHarvestLog

CREATE PROCEDURE BotanicusHarvestLogDeleteAuto

@BotanicusHarvestLogID INT

AS 

DELETE FROM [dbo].[BotanicusHarvestLog]

WHERE

	[BotanicusHarvestLogID] = @BotanicusHarvestLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

