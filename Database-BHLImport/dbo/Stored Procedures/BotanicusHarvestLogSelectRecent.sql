
CREATE PROCEDURE [dbo].[BotanicusHarvestLogSelectRecent]

@NumLogs INT = 10

AS
BEGIN

SET NOCOUNT ON

SELECT	BotanicusHarvestLogID,
		HarvestStartDate,
		HarvestEndDate,
		AutomaticHarvest,
		SuccessfulHarvest,
		Title,
		Creator,
		TitleCreator,
		TitleTag,
		Item,
		Page,
		IndicatedPage,
		PagePageType,
		PageName
FROM	dbo.BotanicusHarvestLog
WHERE	DATEDIFF(day, HarvestEndDate, GETDATE()) <= @NumLogs
ORDER BY
		BotanicusHarvestLogID DESC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogSelectRecent. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

