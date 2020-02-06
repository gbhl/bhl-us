CREATE PROCEDURE [dbo].[ImportLogSelectRecent]

@NumLogs INT = 10

AS
BEGIN

SET NOCOUNT ON

DECLARE @columns NVARCHAR(MAX)
DECLARE @sql NVARCHAR(MAX)

SET @columns = N''

SELECT	@columns += N', p.' + QUOTENAME(Name)
FROM	(SELECT DISTINCT TableName + ' ' + [Action] AS Name FROM dbo.ImportLog) AS x
ORDER BY [Name]

SET @sql = N'
SELECT ImportDate AS ImportDate, BarCode, Result, ' + STUFF(@columns, 1, 2, '') + '
FROM
(
  SELECT CONVERT(nvarchar(20), ImportDate, 120) AS ImportDate, 
    ISNULL(BarCode, '''') AS BarCode, 
	ImportResult AS Result,
	TableName + '' '' + [Action] AS Name, 
	[Rows]
  FROM dbo.ImportLog
  WHERE DATEDIFF(day, ImportDate, GETDATE()) <= ' + CONVERT(varchar(5), @NumLogs) + '
) AS j
PIVOT
(
  SUM([Rows]) FOR Name IN (' + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '') + ')
) AS p
ORDER BY ImportDate DESC'

EXEC sp_executesql @sql
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportLogSelectRecent. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END
