CREATE PROCEDURE dbo.ApplicationCacheUpdateAuto

@CacheKey NVARCHAR(100),
@CacheData NVARCHAR(MAX),
@AbsoluteExpirationDate DATETIME,
@SlidingExpirationDuration INT,
@LastAccessDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[ApplicationCache]
SET
	[CacheKey] = @CacheKey,
	[CacheData] = @CacheData,
	[AbsoluteExpirationDate] = @AbsoluteExpirationDate,
	[SlidingExpirationDuration] = @SlidingExpirationDuration,
	[LastAccessDate] = @LastAccessDate
WHERE
	[CacheKey] = @CacheKey
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ApplicationCacheUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[CacheKey],
		[CacheData],
		[AbsoluteExpirationDate],
		[SlidingExpirationDuration],
		[LastAccessDate],
		[CreationDate]
	FROM [dbo].[ApplicationCache]
	WHERE
		[CacheKey] = @CacheKey
	
	RETURN -- update successful
END
GO
