CREATE PROCEDURE dbo.ApplicationCacheInsertAuto

@CacheKey NVARCHAR(100),
@CacheData NVARCHAR(MAX),
@AbsoluteExpirationDate DATETIME = null,
@SlidingExpirationDuration INT = null,
@LastAccessDate DATETIME

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ApplicationCache]
( 	[CacheKey],
	[CacheData],
	[AbsoluteExpirationDate],
	[SlidingExpirationDuration],
	[LastAccessDate],
	[CreationDate] )
VALUES
( 	@CacheKey,
	@CacheData,
	@AbsoluteExpirationDate,
	@SlidingExpirationDuration,
	@LastAccessDate,
	getdate() )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ApplicationCacheInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
