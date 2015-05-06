CREATE PROCEDURE [reqlog].[RequestLogMoveRecordsToArchive]

AS 

DECLARE @MinDate datetime
DECLARE @MaxDate datetime
DECLARE @NumRecordsToMove int
DECLARE @NumRecordsMoved int

SELECT @MinDate = MIN(CreationDate) FROM reqlog.RequestLog WITH (NOLOCK)
  
SELECT @MaxDate = DATEADD(dd, -14, DATEDIFF(dd,0,GETDATE()))

SELECT @NumRecordsToMove = COUNT(*) 
FROM reqlog.RequestLog WITH (NOLOCK)  
WHERE CreationDate >= @MinDate
AND CreationDate <= @MaxDate

INSERT INTO [reqlog].[RequestLogArchive]
           ([RequestLogID]
           ,[ApplicationID]
           ,[IPAddress]
           ,[UserID]
           ,[CreationDate]
           ,[RequestTypeID]
           ,[Detail])
SELECT 
			l.[RequestLogID]
           ,l.[ApplicationID]
           ,l.[IPAddress]
           ,l.[UserID]
           ,l.[CreationDate]
           ,l.[RequestTypeID]
           ,l.[Detail]
FROM reqlog.RequestLog l WITH (NOLOCK)
LEFT OUTER JOIN reqlog.RequestLogArchive a WITH (NOLOCK)
	ON (l.RequestLogID = a.RequestLogID)
WHERE l.CreationDate >= @MinDate
AND l.CreationDate <= @MaxDate
AND a.RequestLogID IS NULL

-- ensure all records moved over
SELECT @NumRecordsMoved = COUNT(*) 
FROM reqlog.RequestLogArchive WITH (NOLOCK)  
WHERE CreationDate >= @MinDate
AND CreationDate <= @MaxDate

-- if all records moved over, delete
IF (@NumRecordsToMove = @NumRecordsMoved)
BEGIN
	DELETE 
	FROM reqlog.RequestLog 
	WHERE CreationDate >= @MinDate
	AND CreationDate <= @MaxDate
END

