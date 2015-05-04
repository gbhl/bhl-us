CREATE PROCEDURE [reqlog].[RequestLogMoveRecordsToHistory]

AS 

DECLARE @LastHistoryDate datetime

SELECT @LastHistoryDate = MAX(RequestDate) FROM reqlog.RequestHistory WITH (NOLOCK)
SELECT @LastHistoryDate = DATEADD(day, 1, @LastHistoryDate)

INSERT INTO reqlog.RequestHistory
(ApplicationID, RequestDate, NumRequests)
SELECT 
	ApplicationID, 
	DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)),
	COUNT(*)
FROM reqlog.RequestLog WITH (NOLOCK)
WHERE CreationDate < DATEADD(dd, 0, DATEDIFF(dd,0,GETDATE()))	-- before current day
AND CreationDate >= @LastHistoryDate							-- after last history date
GROUP BY 
	ApplicationID, 
	DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate))
ORDER BY
	DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate))

----------------------------------------

INSERT INTO reqlog.RequestHistoryByType
(ApplicationID, RequestDate, RequestTypeID, NumRequests)
SELECT 
	ApplicationID, 
	DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)),
	RequestTypeID,
	COUNT(*)
FROM reqlog.RequestLog WITH (NOLOCK)
WHERE CreationDate < DATEADD(dd, 0, DATEDIFF(dd,0,GETDATE()))
AND CreationDate >= @LastHistoryDate							-- after last history date
GROUP BY 
	ApplicationID, 
	DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)),
	RequestTypeID;

----------------------------------------

WITH TopUsers AS
(
	SELECT 
		ApplicationID, 
		DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)) as RequestDate,
		IPAddress,
		UserID,
		ROW_NUMBER() OVER (
			PARTITION BY ApplicationID, 
			DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate))
			ORDER BY ApplicationID, 
			DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)),
			COUNT(*) DESC) AS 'RowNumber',
		COUNT(*) As NumRequests
	FROM reqlog.RequestLog WITH (NOLOCK)
	WHERE CreationDate < DATEADD(dd, 0, DATEDIFF(dd,0,GETDATE()))
	AND CreationDate >= @LastHistoryDate							-- after last history date
	GROUP BY 
		ApplicationID, 
		DATEADD(dd, 0, DATEDIFF(dd,0,CreationDate)),
		IPAddress,
		UserID
)

INSERT INTO reqlog.RequestHistoryByTopUsers
(ApplicationID, RequestDate, RankForDate, IPAddress, UserID, NumRequests)

SELECT  
	ApplicationID, RequestDate, RowNumber, IPAddress, UserID, NumRequests
FROM TopUsers
WHERE RowNumber BETWEEN 1 and 20
ORDER BY ApplicationID, RequestDate, RowNumber
