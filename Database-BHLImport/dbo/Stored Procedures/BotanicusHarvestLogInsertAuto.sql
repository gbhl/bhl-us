
-- BotanicusHarvestLogInsertAuto PROCEDURE
-- Generated 1/17/2008 3:54:35 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for BotanicusHarvestLog

CREATE PROCEDURE BotanicusHarvestLogInsertAuto

@BotanicusHarvestLogID INT OUTPUT,
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

INSERT INTO [dbo].[BotanicusHarvestLog]
(
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
)
VALUES
(
	@HarvestStartDate,
	@HarvestEndDate,
	@AutomaticHarvest,
	@SuccessfulHarvest,
	@Title,
	@TitleTag,
	@TitleCreator,
	@Creator,
	@Item,
	@Page,
	@IndicatedPage,
	@PagePageType,
	@PageName
)

SET @BotanicusHarvestLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BotanicusHarvestLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

