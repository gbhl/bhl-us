CREATE PROCEDURE [reqlog].[RequestLogSelectDetails]
	@StartDate datetime,
	@EndDate datetime,
	@RequestTypeID as int,
	@IPAddress as varchar(15),
	@UserID as int,
	@OrderBy int,
	@ApplicationID int
AS

SELECT TOP 6000
		RL.*,
		SPACE(0) as [UserName],
		RT.RequestTypeName
FROM	reqlog.RequestLog RL WITH (NOLOCK)
		INNER JOIN reqlog.RequestType RT WITH (NOLOCK) ON (RL.RequestTypeID = RT.RequestTypeID)
WHERE 	RL.ApplicationID = @ApplicationID
AND		RL.CreationDate > @StartDate
AND		RL.CreationDate < @EndDate
AND		(RL.RequestTypeID = @RequestTypeID OR @RequestTypeID is null)
AND		(RL.IPAddress = @IPAddress OR @IPAddress IS NULL)
AND		(RL.UserID = @UserID OR @UserID IS NULL)
ORDER BY
		CASE WHEN @OrderBy = 2 THEN IPAddress END ASC,
		CASE WHEN @OrderBy = -2 THEN IPAddress END DESC,
		CASE WHEN @OrderBy = 4 THEN RL.CreationDate END ASC,
		CASE WHEN @OrderBy = -4 THEN RL.CreationDate END DESC,
		CASE WHEN @OrderBy = 6 THEN Detail END ASC,
		CASE WHEN @OrderBy = -6 THEN Detail END DESC,
		CASE WHEN @OrderBy = 8 THEN RequestTypeName END ASC,
		CASE WHEN @OrderBy = -8 THEN RequestTypeName END DESC
