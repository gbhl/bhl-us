CREATE PROCEDURE [dbo].[TitleSelectByInstitutionAndStartsWithout]

@InstitutionCode nvarchar(10),
@StartsWith varchar(1000) = '',
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalTitles int OUTPUT

AS 

BEGIN

SET NOCOUNT ON

-- Get the total number of titles
SELECT	@TotalTitles = COUNT(DISTINCT t.TitleID)
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON it.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	ii.InstitutionCode = ISNULL(@InstitutionCode, ii.InstitutionCode)
AND		t.SortTitle NOT LIKE @StartsWith + '%'
AND		r.InstitutionRoleName IN ('Holding Institution', 'Rights Holder')

CREATE TABLE #Title (TitleID int NOT NULL)

-- Get the Title IDs for the requested "page" of data
IF (@SortColumn = 'title')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT
						t.TitleID,
						t.SortTitle
				FROM	dbo.Title t  WITH (NOLOCK)
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
						INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
						INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON it.ItemID = ii.ItemID
						INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ii.InstitutionCode = ISNULL(@InstitutionCode, ii.InstitutionCode)
				AND		t.SortTitle NOT LIKE @StartsWith + '%'
				AND		r.InstitutionRoleName IN ('Holding Institution', 'Rights Holder')
				) x
	ORDER BY	x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT
						t.TitleID, 
						t.SortTitle,
						c.Authors
				FROM	dbo.Title t  WITH (NOLOCK)
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
						INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
						INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON it.ItemID = ii.ItemID
						INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ii.InstitutionCode = ISNULL(@InstitutionCode, ii.InstitutionCode)
				AND		t.SortTitle NOT LIKE @StartsWith + '%'
				AND		r.InstitutionRoleName IN ('Holding Institution', 'Rights Holder')
				) x
	ORDER BY	x.Authors, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'year')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT
						t.TitleID, 
						t.SortTitle,
						t.StartYear
				FROM	dbo.Title t  WITH (NOLOCK)
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
						INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
						INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON it.ItemID = ii.ItemID
						INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ii.InstitutionCode = ISNULL(@InstitutionCode, ii.InstitutionCode)
				AND		t.SortTitle NOT LIKE @StartsWith + '%'
				AND		r.InstitutionRoleName IN ('Holding Institution', 'Rights Holder')
				) x
	ORDER BY	x.StartYear, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

SELECT DISTINCT
		t.TitleID,
		b.BookID AS ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CONVERT(nvarchar(10), t.StartYear) AS [Year],
		t.EditionStatement,
		b.Volume,
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections,
		b.ExternalUrl
INTO	#Final
FROM	#Title tmp  WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN (
				-- Get the first item for each title
				SELECT	tmp.TitleID, MIN(ItemSequence) MinSeq
				FROM	#Title tmp
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON tmp.TitleID = it.TitleID
						INNER JOIN dbo.Item itm WITH (NOLOCK) ON it.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY tmp.TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON x.TitleID = it.TitleID AND x.MinSeq = it.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	i.ItemStatusID = 40

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY [Year], SortTitle OPTION (RECOMPILE)

END

GO
