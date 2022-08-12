CREATE PROCEDURE [dbo].[TitleSearchPaging]
	@MARCBibID nvarchar(50),
	@TitleID int,
	@Title nvarchar(60),
	@Virtual int,
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
	DECLARE @SQL varchar(4000)
	DECLARE @OrderByClause varchar(1000)
	DECLARE @WhereClause varchar(2500)
	DECLARE @WhereItem int

-- Build Order By clause

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

-- Build Where clause

	SET @WhereItem = 0
	SET @WhereClause = 'WHERE (B.IsVirtual = ' + 
						CASE WHEN @Virtual IS NULL THEN 'NULL' ELSE CONVERT(char(1), @Virtual) END + 
						' OR ' +
						CASE WHEN @Virtual IS NULL THEN 'NULL' ELSE CONVERT(char(1), @Virtual) END + 
						' IS NULL)'

	IF @MarcBibID IS NOT NULL 
	BEGIN
		SET @WhereClause = @WhereClause + ' AND T.MarcBibID COLLATE latin1_general_ci_ai LIKE ''' + @MarcBibID + ''''
	END

	IF @TitleID IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' AND T.TitleID = ' + CONVERT(varchar(10), @TitleID)
	END
	IF @Title IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' AND T.SortTitle COLLATE latin1_general_ci_ai LIKE ''' + @Title + ''''
	END

SET @SQL = 
	' SELECT * FROM
		(
		SELECT
			ROW_NUMBER() OVER(' + @OrderByClause + ') AS RowNum,
			T.MARCBibID,
			T.TitleID,
			T.SortTitle
			FROM (SELECT DISTINCT T.MARCBibID, T.TitleID, T.SortTitle
				FROM dbo.Title T
					LEFT JOIN dbo.ItemTitle IT ON T.TitleID = IT.TitleID
					LEFT JOIN dbo.Item I ON IT.ItemID = I.ItemID
					LEFT JOIN dbo.Book B ON I.ItemID = B.ItemID
'

	SET @Sql = @Sql + @WhereClause

	SET @Sql = @Sql + ') T
			) AS TT 
		WHERE TT.RowNum BETWEEN ' + convert(varchar(10), @StartRow) + ' AND ' + convert(varchar(10), @StartRow + @PageSize - 1)

	--Print 'SQL for Stored Procedure: ' + @SQL

	EXEC(@Sql)

END

GO
