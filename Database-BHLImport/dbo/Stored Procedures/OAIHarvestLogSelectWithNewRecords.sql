CREATE PROCEDURE dbo.OAIHarvestLogSelectWithNewRecords

AS

BEGIN

SET NOCOUNT ON 

SELECT	l.HarvestLogID 
FROM 	dbo.OAIRecord r 
		INNER JOIN OAIHarvestLog l ON r.HarvestLogID = l.HarvestLogID
WHERE	r.OAIRecordStatusID = 10	-- New
AND 	r.ProductionTitleID IS NULL 
AND 	r.ProductionItemID IS NULL 
AND 	r.ProductionSegmentID IS NULL
GROUP BY l.HarvestLogID
ORDER BY l.HarvestLogID

END

GO
