CREATE PROCEDURE [dbo].[TitleSelectByAuthorPaged]

@AuthorId	int,
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalTitles int OUTPUT

AS

BEGIN

SET NOCOUNT ON

-- Get the total number of titles for the author
SELECT @TotalTitles = COUNT(DISTINCT t.TitleID)
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON t.TitleID = ta.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		ta.AuthorID = @AuthorId

CREATE TABLE #Title (TitleID int NOT NULL, AuthorID int NOT NULL)

IF (@SortColumn = 'title')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID, @AuthorId AS AuthorID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle
				FROM		dbo.Title t WITH (NOLOCK)
							INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON t.TitleID = ta.TitleID
							INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
							INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
							INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE		t.PublishReady = 1
				AND			ta.AuthorID = @AuthorId
				) x
	ORDER BY	x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID, @AuthorId AS AuthorID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle, c.Authors
				FROM		dbo.Title t WITH (NOLOCK)
							INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON t.TitleID = ta.TitleID
							INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
							INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
							INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE		t.PublishReady = 1
				AND			ta.AuthorID = @AuthorId
				) x
	ORDER BY	x.Authors, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'year')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID, @AuthorId AS AuthorID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle, t.StartYear
				FROM		dbo.Title t WITH (NOLOCK)
							INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON t.TitleID = ta.TitleID
							INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
							INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
							INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
				WHERE		t.PublishReady = 1
				AND			ta.AuthorID = @AuthorId
				) x
	ORDER BY	x.StartYear, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

SELECT 	v.TitleID,
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
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, itm.ItemID) AS Collections
INTO	#Final
FROM	#Title v WITH (NOLOCK) 
		INNER JOIN dbo.Title t WITH (NOLOCK) ON v.TitleID = t.TitleID
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
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON v.AuthorID = n.AuthorId
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON x.TitleID = it.TitleID AND x.MinSeq = it.ItemSequence
		INNER JOIN dbo.Item itm WITH (NOLOCK) ON it.ItemID = itm.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON itm.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	n.IsPreferredName = 1

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY [Year], SortTitle OPTION (RECOMPILE)

END
