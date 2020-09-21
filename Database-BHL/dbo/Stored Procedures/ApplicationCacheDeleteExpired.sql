CREATE PROCEDURE dbo.ApplicationCacheDeleteExpired

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.ApplicationCache
WHERE	AbsoluteExpirationDate < GETDATE()
OR		DATEADD(mi, SlidingExpirationDuration, LastAccessDate) < GETDATE()

END
