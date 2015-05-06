CREATE PROCEDURE [reqlog].[RequestLogSelectTopIP]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID int,
	@IncludeWebServices bit = 0
AS

SELECT	RL.IpAddress AS StringColumn01,
		COUNT(*) AS IntColumn01
FROM	reqlog.RequestLog RL WITH (NOLOCK)
WHERE	RL.ApplicationID = @ApplicationID 
AND 	RL.CreationDate > @StartDate 
AND		RL.CreationDate < @EndDate 
AND		RL.IpAddress IS NOT NULL
AND		((@IncludeWebServices = 0 AND RL.RequestTypeID < 1000000) OR @IncludeWebServices = 1)
GROUP BY IpAddress
ORDER BY COUNT(*) DESC
