
-- BotanicusHarvestLogUpdateAuto PROCEDURE
-- Generated 1/17/2008 3:54:35 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for BotanicusHarvestLog

CREATE PROCEDURE BotanicusHarvestLogUpdateAuto

@BotanicusHarvestLogID INT,
@HarvestStartDate DATETIME,
@HarvestEndDate DATETIME,
@AutomaticHarvest BIT,
@SuccessfulHarvest BIT,
@Title INT,
@TitleTag INT,
@TitleCreator INT,
@Creator INT,
@Item INT,
@Page INT,
@IndicatedPage INT,
@PagePageType INT,
@PageName INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[BotanicusHarvestLog]

SET

	[HarvestStartDate] = @HarvestStartDate,
	[HarvestEndDate] = @HarvestEndDate,
	[AutomaticHarvest] = @AutomaticHarvest,
	[SuccessfulHarvest] = @SuccessfulHarvest,
	[Title] = @Title,
	[TitleTag] = @TitleTag,
	[TitleCreator] = @TitleCreator,
	[Creator] = @Creator,
	[Item] = @Item,
	[Page] = @Page,
	[IndicatedPage] = @IndicatedPage,
	[PagePageType] = @PagePageType,
	[PageName] = @PageName

WHERE
	[BotanicusHarvestLogID] = @BotanicusHarvestLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

