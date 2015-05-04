CREATE PROCEDURE [reqlog].[RequestLogArchiveDeleteOld]

AS 

DECLARE @MaxDate datetime

SELECT @MaxDate = DATEADD(dd, -90, DATEDIFF(dd,0,GETDATE()))

DELETE FROM reqlog.RequestLogArchive
WHERE CreationDate < @MaxDate

GO

