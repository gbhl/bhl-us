CREATE PROCEDURE [dbo].[ItemSelectByCollectionAndStartsWith]

@CollectionID int,
@StartsWith nvarchar(255),
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalItems int OUTPUT

AS

BEGIN

SET NOCOUNT ON

-- Get the total number of items
SELECT	@TotalItems = COUNT(DISTINCT b.BookID)
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) on i.ItemID = b.ItemID
		INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID AND t.SortTitle LIKE @StartsWith + '%'
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	ic.CollectionID = @CollectionID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

CREATE TABLE #Book (BookID int NOT NULL)

-- Get the Book IDs for the requested "page" of data
IF (@SortColumn = 'title')
BEGIN
	INSERT		#Book
	SELECT		x.BookID
	FROM		(
				SELECT DISTINCT b.BookID, t.SortTitle, it.ItemSequence
				FROM	dbo.Item i WITH (NOLOCK)
						INNER JOIN dbo.Book b WITH (NOLOCK) on i.ItemID = b.ItemID
						INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
						INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
							AND t.SortTitle LIKE @StartsWith + '%'
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) 
							ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ic.CollectionID = @CollectionID
				AND		i.ItemStatusID = 40
				AND		t.PublishReady = 1
				) x
	ORDER BY	x.SortTitle, x.ItemSequence
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Book
	SELECT		x.BookID
	FROM		(
				SELECT DISTINCT b.BookID, c.Authors, t.SortTitle, it.ItemSequence
				FROM	dbo.Item i WITH (NOLOCK)
						INNER JOIN dbo.Book b WITH (NOLOCK) on i.ItemID = b.ItemID
						INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
						INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
							AND t.SortTitle LIKE @StartsWith + '%'
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) 
							ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ic.CollectionID = @CollectionID
				AND		i.ItemStatusID = 40
				AND		t.PublishReady = 1
				) x
	ORDER BY	x.Authors, x.SortTitle, x.ItemSequence
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

IF (@SortColumn = 'year')
BEGIN
	INSERT		#Book
	SELECT		x.BookID
	FROM		(
				SELECT DISTINCT 
						b.BookID, 
						CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.StartYear END AS StartYear,
						t.SortTitle, it.ItemSequence
				FROM	dbo.Item i WITH (NOLOCK)
						INNER JOIN dbo.Book b WITH (NOLOCK) on i.ItemID = b.ItemID
						INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
						INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
							AND t.SortTitle LIKE @StartsWith + '%'
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) 
							ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE	ic.CollectionID = @CollectionID
				AND		i.ItemStatusID = 40
				AND		t.PublishReady = 1
				) x
	ORDER BY	x.StartYear, x.SortTitle, x.ItemSequence
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

SELECT	t.TitleID,
		b.BookID AS ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.StartYear END AS [Year],
		t.EditionStatement,
		b.Volume,
		b.ExternalUrl,
		c.ItemContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections,
		it.ItemSequence
INTO	#Final
FROM	#Book tmp
		INNER JOIN dbo.Book b WITH (NOLOCK) ON tmp.BookID = b.BookID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON b.ItemID = i.ItemID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID AND t.SortTitle LIKE @StartsWith + '%'
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID

IF (@SortColumn = 'title') 
BEGIN
	SELECT TitleID, ItemID, FullTitle, SortTitle, PartNumber, PartName, PublicationDetails, [Year], EditionStatement, Volume, ExternalUrl, InstitutionName, Authors, Collections
	FROM #Final ORDER BY SortTitle, ItemSequence
	OPTION (RECOMPILE)
END
IF (@SortColumn = 'author') 
BEGIN
	SELECT TitleID, ItemID, FullTitle, SortTitle, PartNumber, PartName, PublicationDetails, [Year], EditionStatement, Volume, ExternalUrl, InstitutionName, Authors, Collections
	FROM #Final ORDER BY Authors, SortTitle, ItemSequence
	OPTION (RECOMPILE)
END
IF (@SortColumn = 'year') 
BEGIN
	SELECT TitleID, ItemID, FullTitle, SortTitle, PartNumber, PartName, PublicationDetails, [Year], EditionStatement, Volume, ExternalUrl, InstitutionName, Authors, Collections
	FROM #Final ORDER BY [Year], SortTitle, ItemSequence
	OPTION (RECOMPILE)
END

END

GO
