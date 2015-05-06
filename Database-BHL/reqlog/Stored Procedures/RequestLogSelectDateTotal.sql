CREATE PROCEDURE [reqlog].[RequestLogSelectDateTotal]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID int,
	@IncludeWebServices bit = 0
AS

SELECT @StartDate = FLOOR( CAST( @StartDate AS FLOAT ) )
SELECT @EndDate = FLOOR( CAST( @EndDate AS FLOAT ) )

SELECT	COUNT(*) 
FROM	reqlog.RequestLog RL WITH (NOLOCK) 
		INNER JOIN reqlog.RequestType RT WITH (NOLOCK) ON (RL.RequestTypeID = RT.RequestTypeID)
WHERE	RL.ApplicationID = @ApplicationID
AND		RL.CreationDate >= @StartDate 
AND		RL.CreationDate <= @EndDate
AND		((@IncludeWebServices = 0 AND RL.RequestTypeID < 1000000) or @IncludeWebServices = 1)

