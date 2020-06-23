CREATE PROCEDURE [dbo].[ApplicationCacheSelectAuto]

@CacheKey NVARCHAR(100)

AS 

SET NOCOUNT ON

SELECT	
	[CacheKey],
	[CacheData],
	[AbsoluteExpirationDate],
	[SlidingExpirationDuration],
	[LastAccessDate],
	[CreationDate]
FROM	
	[dbo].[ApplicationCache]
WHERE	
	[CacheKey] = @CacheKey

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ApplicationCacheSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
GO
