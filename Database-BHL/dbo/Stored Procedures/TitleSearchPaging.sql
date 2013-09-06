
CREATE Procedure [dbo].[TitleSearchPaging]
	@MARCBibID nvarchar(50),
	@TitleID int,
	@Title nvarchar(60),
	@StartRow int,
	@PageSize int,
	@OrderBy int
AS

SET NOCOUNT ON

DECLARE @SortDirection varchar(4)
IF (@OrderBy >= 0) 
BEGIN
	SET @SortDirection = 'ASC'
END
ELSE 
BEGIN
	SET @SortDirection = 'DESC'
	SELECT @OrderBy = 0 - @OrderBy
END

BEGIN
	--Print 'Building Stored Procedure ' + @spName
--	BEGIN TRAN

	DECLARE @SQL varchar(4000)
	DECLARE @OrderByClause varchar(1000)
	DECLARE @WhereClause varchar(2500)
	DECLARE @WhereItem int

	IF (@OrderBy = 1)
	BEGIN
		SELECT @OrderByClause = ' ORDER BY T.TitleID ' + @SortDirection
	END
	ELSE IF (@OrderBy = 2)
	BEGIN
		SELECT @OrderByClause = ' ORDER BY T.MarcBibID ' + @SortDirection
	END
	ELSE IF (@OrderBy = 3)
	BEGIN
		SELECT @OrderByClause = ' ORDER BY T.SortTitle ' + @SortDirection
	END
	ELSE
	BEGIN
		SELECT @OrderByClause = ' ORDER BY T.SortTitle ' + @SortDirection
	END

	SELECT @OrderByClause = @OrderByClause + ' '

	SET @WhereItem = 0
	SET @WhereClause = 'WHERE '		

-- Build Where clause


	IF @MarcBibID IS NOT NULL 
	BEGIN
		SET @WhereClause = @WhereClause + 'T.MarcBibID COLLATE latin1_general_ci_ai LIKE ''' + @MarcBibID + ''' AND '
	END

	IF @TitleID IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + 'T.TitleID = ' + CONVERT(varchar(10), @TitleID) + ' AND '
	END
	IF @Title IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + 'T.SortTitle COLLATE latin1_general_ci_ai LIKE ''' + @Title + ''' AND '
	END

SET @SQL = 
	' SELECT * FROM
		(
SELECT
	ROW_NUMBER() OVER(' + @OrderByClause + ') AS RowNum,
	T.MARCBibID,
	T.TitleID,
	T.SortTitle
FROM dbo.Title T
'

	SET @Sql = @Sql + @WhereClause
	IF LEN(@WhereClause) > 6 
	BEGIN
		SET @Sql = LEFT(@Sql, LEN(@SQL) - 4 )
	END
	ELSE
	BEGIN
		SET @Sql = LEFT( @Sql, LEN(@Sql) - 6 )
	END

	SET @Sql = @Sql + ') AS TT 
		WHERE TT.RowNum BETWEEN ' + convert(varchar(10), @StartRow) + ' AND ' + convert(varchar(10), @StartRow + @PageSize - 1)

--	Print 'SQL for Stored Procedure: ' + @SQL

	EXEC(@Sql)
--	COMMIT

END

executeProcedure:
--	EXEC @spName @MarcBibID, @TitleID, @Title, @StartRow,	@PageSize



