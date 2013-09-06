CREATE PROCEDURE [dbo].[DOISelectSubmitted]

@MinutesSinceSubmit int = 60

AS

BEGIN

SET NOCOUNT ON

SELECT	d.DOIID,
		d.DOIEntityTypeID,
		d.EntityID,
		d.DOIStatusID,
		d.DOIBatchID,
		d.DOIName,
		d.StatusMessage
FROM	dbo.DOI d
WHERE	(d.DOIStatusID = 50 -- DOI Submitted
OR		d.DOIStatusID = 70) -- Get Submit Log Error
AND		DATEDIFF(n, d.StatusDate, GETDATE()) > @MinutesSinceSubmit -- only select if older than specified # of minutes

END


