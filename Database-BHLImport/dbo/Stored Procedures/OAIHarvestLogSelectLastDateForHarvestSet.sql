CREATE PROCEDURE dbo.OAIHarvestLogSelectLastDateForHarvestSet

@HarvestSetID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @LastHarvestDate datetime

SELECT	@LastHarvestDate = CASE WHEN UntilDateTime IS NULL THEN HarvestStartDateTime ELSE UntilDateTime END
FROM	dbo.OAIHarvestLog
WHERE	HarvestLogID IN (	SELECT	MAX(HarvestLogID)
							FROM	dbo.OAIHarvestLog
							WHERE	HarvestSetID = @HarvestSetID
							AND		Result = 'OK'
						)

IF (@LastHarvestDate IS NULL)
BEGIN
	SELECT	@LastHarvestDate = r.EarliestDateStamp
	FROM	dbo.OAIHarvestSet hs
			INNER JOIN dbo.OAIRepositoryFormat rf ON hs.RepositoryFormatID = rf.RepositoryFormatID
			INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
	WHERE	hs.HarvestSetID = @HarvestSetID

END		

SELECT	@LastHarvestDate AS UntilDateTime

END
