CREATE PROCEDURE [reqlog].[RequestLogSelectTypesByDate]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID int,
	@IncludeWebServices bit = 0
AS

SELECT @StartDate = FLOOR( CAST( @StartDate AS FLOAT ) )
SELECT @EndDate = FLOOR( CAST( @EndDate AS FLOAT ) )

SELECT 	RequestTypeName AS StringColumn01,
		RT.RequestTypeID AS IntColumn02,
		COUNT(*) AS IntColumn01
FROM	reqlog.RequestLog RL WITH (NOLOCK)
		INNER JOIN reqlog.RequestType RT WITH (NOLOCK) ON (RL.RequestTypeID = RT.RequestTypeID)
WHERE 	RL.ApplicationID = @ApplicationID 
AND		RL.CreationDate > @StartDate 
AND		RL.CreationDate < @EndDate
AND		((@IncludeWebServices = 0 AND RL.RequestTypeID < 1000000) or @IncludeWebServices = 1)
GROUP BY RequestTypeName, RT.RequestTypeID
ORDER BY COUNT(*) DESC 
