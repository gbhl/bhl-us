﻿
CREATE PROCEDURE [dbo].[IAItemInsertFromIAAnalysis]

@LocalFileFolder nvarchar(200)

AS

BEGIN

SET NOCOUNT ON

DECLARE @StartDate datetime
DECLARE @HarvestDate datetime
DECLARE @SuccessfulHarvest tinyint
DECLARE @ItemCount int

SET @HarvestDate = GETDATE()
SET @ItemCount = 0

SELECT	@StartDate = ISNULL(MAX(HarvestDate), '10/12/2009') 
FROM	dbo.IAAnalysisHarvestLog 
WHERE	SuccessfulHarvest = 1

BEGIN TRY
	CREATE TABLE #tmpItem (
		IAIdentifier nvarchar(50) NOT NULL,
		ItemStatusID int NOT NULL,
		LocalFileFolder nvarchar(200) NOT NULL,
		IADateStamp datetime NULL,
		CreationDate datetime NOT NULL
		)

	INSERT INTO #tmpItem (IAIdentifier, ItemStatusID, LocalFileFolder, IADateStamp, CreationDate)
	SELECT	i.Identifier AS IAIdentifier, 
			10 AS ItemStatusID, 
			@LocalFileFolder AS LocalFileFolder,
			CASE WHEN ISNULL(AddedDate, '1/1/1980') > ISNULL(PublicDate, '1/1/1980') AND ISNULL(AddedDate, '1/1/1980') > ISNULL(UpdateDate, '1/1/1980') THEN AddedDate 
				WHEN ISNULL(PublicDate, '1/1/1980') > ISNULL(AddedDate, '1/1/1980') AND ISNULL(PublicDate, '1/1/1980') > ISNULL(UpdateDate, '1/1/1980') THEN PublicDate
				ELSE UpdateDate END	AS IADateStamp,
			r.CreationDate
	FROM	IAAnalysisrptCombined r INNER JOIN IAAnalysisItem i
				ON r.ItemID = i.ItemID
	WHERE	r.CreationDate > @StartDate
	AND		i.Sponsor <> 'Google'

	-- Eliminate any items in the "lendinglibrary" collection (they are non-downloadable)
	DELETE FROM #tmpItem WHERE IAIdentifier IN (
		SELECT	i.Identifier
		FROM	IAAnalysisItem i INNER JOIN IAAnalysisItemCollection ic
					ON i.ItemID = ic.ItemID
				INNER JOIN IAAnalysisCollection c
					ON ic.CollectionID = c.CollectionID
		WHERE	c.CollectionName LIKE '%lendinglibrary%'
	)

	SELECT @HarvestDate = MAX(CreationDate) FROM #tmpItem

	INSERT INTO dbo.IAItem (IAIdentifier, ItemStatusID, LocalFileFolder, IADateStamp)
	SELECT IAIdentifier, ItemStatusID, LocalFileFolder, IADateStamp FROM #tmpItem

	SET @ItemCount = @@ROWCOUNT
	SET @SuccessfulHarvest = 1
END TRY
BEGIN CATCH
	SET @SuccessfulHarvest = 0
END CATCH

IF (@ItemCount > 0 OR @SuccessfulHarvest = 0)
BEGIN
	INSERT dbo.IAAnalysisHarvestLog (HarvestDate, SuccessfulHarvest, Item) 
	VALUES (@HarvestDate, @SuccessfulHarvest, @ItemCount)
END

END

