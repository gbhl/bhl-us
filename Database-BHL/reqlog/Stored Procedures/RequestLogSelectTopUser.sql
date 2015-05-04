CREATE procedure [reqlog].[RequestLogSelectTopUser]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID int,
	@IncludeWebServices bit = 0
AS

SELECT	RL.UserID AS IntColumn02,
		SPACE(0) as StringColumn01,
		COUNT(*) AS IntColumn01
FROM	reqlog.RequestLog RL WITH (NOLOCK)
WHERE	RL.ApplicationID = @ApplicationID 
AND 	RL.CreationDate > @StartDate 
AND		RL.CreationDate < @EndDate 
AND		RL.UserID IS NOT NULL
AND		((@IncludeWebServices = 0 AND RL.RequestTypeID < 1000000) or @IncludeWebServices = 1)
GROUP BY RL.UserID
ORDER BY COUNT(*) DESC
