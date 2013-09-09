
-- BotanicusHarvestLogSelectAuto PROCEDURE
-- Generated 1/17/2008 3:54:35 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for BotanicusHarvestLog

CREATE PROCEDURE BotanicusHarvestLogSelectAuto

@BotanicusHarvestLogID INT

AS 

SET NOCOUNT ON

SELECT 

	[BotanicusHarvestLogID],
	[HarvestStartDate],
	[HarvestEndDate],
	[AutomaticHarvest],
	[SuccessfulHarvest],
	[Title],
	[TitleTag],
	[TitleCreator],
	[Creator],
	[Item],
	[Page],
	[IndicatedPage],
	[PagePageType],
	[PageName]

FROM [dbo].[BotanicusHarvestLog]

WHERE
	[BotanicusHarvestLogID] = @BotanicusHarvestLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

