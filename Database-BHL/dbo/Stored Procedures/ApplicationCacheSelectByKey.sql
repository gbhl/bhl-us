CREATE PROCEDURE [dbo].[ApplicationCacheSelectByKey]

@CacheKey NVARCHAR(100)

AS 

SET NOCOUNT ON

UPDATE	dbo.ApplicationCache
SET		LastAccessDate = GETDATE()
WHERE	CacheKey = @CacheKey

SELECT	CacheKey,
		CacheData,
		AbsoluteExpirationDate,
		SlidingExpirationDuration,
		LastAccessDate,
		CreationDate
FROM	dbo.ApplicationCache
WHERE	CacheKey = @CacheKey
AND		(
		AbsoluteExpirationDate > GETDATE() OR
		DATEADD(mi, SlidingExpirationDuration, LastAccessDate) > GETDATE()
		)
