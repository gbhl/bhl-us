CREATE PROCEDURE servlog.SeveritySelect24HourStats

AS

BEGIN

WITH CTE AS (
	SELECT	SeverityID, COUNT(*) AS TotalRecords
	FROM	servlog.ServiceLog
	WHERE	CreationDate >= DATEADD(hour, -24, GETDATE())
	GROUP BY SeverityID
	)
SELECT	sv.SeverityID,
		[Name],	
		[Label],
		FGColorHexCode,
		ISNULL(cte.TotalRecords, 0) AS TotalRecords
FROM	servlog.Severity sv
		LEFT JOIN CTE ON sv.SeverityID = cte.SeverityID
ORDER BY
		[Label]

END

GO
