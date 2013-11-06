CREATE PROCEDURE [dbo].[OAIHarvestLogSelectLastDateForHarvestSet]

@HarvestSetID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @LastHarvestDate datetime

SELECT	@LastHarvestDate = MAX(ISNULL(UntilDateTime, ResponseDateTime))
FROM	dbo.OAIHarvestLog
WHERE	HarvestSetID = @HarvestSetID
AND		Result = 'ok'

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

