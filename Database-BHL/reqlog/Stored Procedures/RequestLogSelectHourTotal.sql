CREATE procedure [reqlog].[RequestLogSelectHourTotal]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID int,
	@IncludeWebServices bit = 0
AS

SELECT @StartDate = FLOOR( CAST( @StartDate AS FLOAT ) )
SELECT @EndDate = FLOOR( CAST( @EndDate AS FLOAT ) )

SELECT	DATEPART(hh, RL.CreationDate) AS IntColumn01,
		COUNT(*) AS IntColumn02
FROM	reqlog.RequestLog RL WITH (NOLOCK)
WHERE	RL.ApplicationID = @ApplicationID 
AND		RL.CreationDate > @StartDate 
AND		RL.CreationDate < @EndDate
AND		((@IncludeWebServices = 0 AND RL.RequestTypeID < 1000000) or @IncludeWebServices = 1)
GROUP BY DATEPART(hh, RL.CreationDate)
ORDER BY DATEPART(hh, RL.CreationDate)
