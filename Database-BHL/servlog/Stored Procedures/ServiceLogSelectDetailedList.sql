CREATE PROCEDURE servlog.ServiceLogSelectDetailedList
	@ServiceID int = NULL,
	@SeverityID int = NULL,
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@NumRows int = 100,
	@StartRow int = 1,
	@SortColumn nvarchar(150) = 'CreationDate',
	@SortDirection nvarchar(5) = 'DESC'
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @SortCDateDirection nvarchar(4) = 'DESC'
	IF @SortDirection <> 'ASC' AND @SortDirection <> 'DESC' SET @SortDirection = 'ASC'
	IF @SortColumn = 'CreationDate' 
	BEGIN
		SET @SortColumn = ''
		SET @SortCDateDirection = @SortDirection
		SET @SortDirection = ''
	END
	IF @SortDirection <> '' SET @SortDirection = @SortDirection + ', '
	SET @ServiceID = ISNULL(@ServiceID, 0)
	SET @SeverityID = ISNULL(@SeverityID, 0)
	SET @StartDate = ISNULL(@StartDate, '1/1/2007')
	SET @EndDate = ISNULL(@EndDate, DATEADD(DAY, 1, GETDATE()))

	DECLARE @SQL nvarchar(max)
	SET @SQL =
		'SELECT	ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortDirection + 'CreationDate ' + @SortCDateDirection + ') AS RowNumber ' +
				',ServiceLogID ' +
		'FROM	servlog.ServiceLog ' +
		'WHERE	(ServiceID = ' + CONVERT(varchar(10), @ServiceID) + ' OR ' + CONVERT(varchar(10), @ServiceID) + ' = 0) ' +
		'AND	(SeverityID = ' + CONVERT(varchar(10), @SeverityID) + ' OR ' + CONVERT(varchar(10), @SeverityID) + ' = 0) '+
		'AND	CreationDate BETWEEN ''' + CONVERT(varchar(20), @StartDate, 101) + ''' AND ''' + CONVERT(varchar(20), @EndDate, 101) + ''''

	CREATE TABLE #Log
		(
		RowNumber int NOT NULL,
		ServiceLogID int NOT NULL
		)

	INSERT #Log EXEC (@SQL)
	
	SELECT TOP (@NumRows)
			t.RowNumber
			,l.ServiceLogID
			,l.ServiceID
			,s.[Name]
			,s.[Param]
			,ISNULL(f.[Label], '') AS FrequencyLabel
			,f.IntervalInMinutes
			,DATEDIFF(minute, l.CreationDate, GETDATE()) AS MinutesElapsedSinceLog	-- Do this calc here or in calling code?
			,l.SeverityID
			,sv.[Label] AS SeverityLabel
			,sv.FGColorHexCode
			--,ErrorNumber
			--,[Procedure]
			--,Line
			,[Message]
			--,StackTrace
			,l.CreationDate
	FROM	#Log t
			INNER JOIN servlog.ServiceLog l ON t.ServiceLogID = l.ServiceLogID
			INNER JOIN servlog.[Service] s ON l.ServiceID = s.ServiceID
			LEFT JOIN servlog.Frequency f ON s.FrequencyID = f.FrequencyID
			INNER JOIN servlog.Severity sv ON l.SeverityID = sv.SeverityID
	WHERE	RowNumber >= @StartRow
	ORDER BY RowNumber
END
GO
