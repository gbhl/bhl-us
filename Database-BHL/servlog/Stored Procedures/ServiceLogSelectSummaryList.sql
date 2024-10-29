CREATE PROCEDURE servlog.ServiceLogSelectSummaryList
AS
BEGIN
	SET NOCOUNT ON;

	WITH CTE AS (
		SELECT	s.ServiceID
				,MAX(ServiceLogID) AS ServiceLogID				
		FROM	servlog.Service s
				LEFT JOIN servlog.ServiceLog l on s.ServiceID = l.ServiceID
		GROUP BY s.ServiceID
		)
	SELECT	s.Disabled
			,s.ServiceID
			,s.[Name]
			,s.[Param]
			,ISNULL(f.[Label], '') AS FrequencyLabel
			,f.IntervalInMinutes
			,DATEDIFF(minute, l.CreationDate, GETDATE()) AS MinutesElapsedSinceLog	-- Do this calc here or in calling code?
			,l.CreationDate AS LogCreationDate
			,sv.[Label] AS SeverityLabel
			,sv.FGColorHexCode
	FROM	CTE 
			LEFT JOIN servlog.ServiceLog l ON CTE.ServiceLogID = l.ServiceLogID
			INNER JOIN servlog.[Service] s ON cte.ServiceID = s.ServiceID AND s.Display = 1
			LEFT JOIN servlog.Frequency f ON s.FrequencyID = f.FrequencyID
			LEFT JOIN servlog.Severity sv ON l.SeverityID = sv.SeverityID
	ORDER BY
			s.[Name]
			,s.[Param]
END
GO
