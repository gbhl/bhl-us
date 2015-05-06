CREATE PROCEDURE [reqlog].[RequestHistorySelectByDateRangeAndRequestType]
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationID as int,
	@RequestTypeID as int
AS

IF (@RequestTypeID = 0)
BEGIN
	SELECT 
		datepart(year, RequestDate) AS Year, 
		datepart(month, RequestDate) AS Month, 
		datepart(day, RequestDate) AS Day, 
		NumRequests
	FROM reqlog.RequestHistory WITH (NOLOCK)
	WHERE ApplicationID = @ApplicationID
	AND RequestDate >= @StartDate
	AND RequestDate <= @EndDate
	ORDER BY RequestDate
END

ELSE
BEGIN

	DECLARE @AllDays TABLE
	(
	  RequestDate datetime
	)
	
	DECLARE @NumDays int
	SELECT @NumDays = datediff(day, @StartDate, @EndDate)

	DECLARE @Count int
	SELECT @Count = 0
	
	WHILE (@Count <= @NumDays)
	BEGIN
		INSERT INTO @AllDays (RequestDate) 
		SELECT dateadd(day, @Count, @StartDate)
	
		SELECT @Count = @Count + 1
	END
	
	SELECT 
		datepart(year, a.RequestDate) AS Year, 
		datepart(month, a.RequestDate) AS Month, 
		datepart(day, a.RequestDate) AS Day, 
		ISNULL(h.NumRequests, 0) AS NumRequests
	FROM @AllDays a
	LEFT OUTER JOIN reqlog.RequestHistoryByType h WITH (NOLOCK)
		on (a.RequestDate = h.RequestDate and 
			h.ApplicationID = @ApplicationID and
			h.RequestTypeID = @RequestTypeID)
	ORDER BY h.RequestDate
END
