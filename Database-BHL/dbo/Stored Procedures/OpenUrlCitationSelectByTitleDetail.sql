CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByTitleDetail]

@Title nvarchar(2000) = '',
@AuthorLast nvarchar(150) = '',
@AuthorFirst nvarchar(100) = ''

AS

BEGIN

SET NOCOUNT ON

-- Need to check for both "Last, First" and "First Last"
DECLARE @Author nvarchar(255)
DECLARE @AuthorAlt nvarchar(255)
SET @Author = ''
SET @AuthorAlt = ''

IF (@AuthorFirst = '' AND @AuthorLast <> '') SET @Author = @AuthorLast + '%'
IF (@AuthorFirst <> '' AND @AuthorLast = '') SET @Author = @AuthorFirst + '%'
IF (@AuthorFirst <> '' AND @AuthorLast <> '') 
BEGIN
	SET @Author = @AuthorLast + ', ' + @AuthorFirst + '%'
	SET @AuthorAlt = @AuthorFirst + ' ' + @AuthorLast + '%'
END
IF @AuthorAlt = '' SET @AuthorAlt = @Author

CREATE TABLE #tmpOpenUrlCitation
	(
	PageID int NULL,
	ItemID int NULL,
	TitleID int NULL,
	SegmentID int NULL,
	FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
	SegmentTitle nvarchar(2000) NOT NULL DEFAULT(''),
	ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
	PublisherPlace nvarchar(150) NOT NULL DEFAULT(''),
	PublisherName nvarchar(255) NOT NULL DEFAULT(''),
	Date nvarchar(20) NOT NULL DEFAULT(''),
	LanguageName nvarchar(20) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
	CurrentPublicationFrequency nvarchar(100) NOT NULL DEFAULT(''),
	Genre nvarchar(20) NOT NULL DEFAULT(''),
	Authors nvarchar(2000) NOT NULL DEFAULT(''),
	Subjects nvarchar(1000) NOT NULL DEFAULT(''),
	StartPage nvarchar(40) NOT NULL DEFAULT(''),
	EndPage nvarchar(40) NOT NULL DEFAULT(''),
	Pages nvarchar(40) NOT NULL DEFAULT(''),
	ISSN nvarchar(125) NOT NULL DEFAULT(''),
	ISBN nvarchar(125) NOT NULL DEFAULT(''),
	LCCN nvarchar(125) NOT NULL DEFAULT(''),
	OCLC nvarchar(125) NOT NULL DEFAULT(''),
	Abbreviation nvarchar(125) NOT NULL DEFAULT('')
	)

-- Make sure that search criteria were specified
IF (@Title <> '' OR
	@AuthorLast <> '' OR
	@AuthorFirst <> '')
BEGIN
	IF @Title <> '' SET @Title = '%' + @Title + '%'

	-- UPDATE STATISTICS and OPTION (RECOMPILE) used here to optimize the insert into the temp table
	-- http://sqlblog.com/blogs/paul_white/archive/2012/08/15/temporary-tables-in-stored-procedures.aspx

	UPDATE STATISTICS #tmpOpenUrlCitation

	-- Get the basic title/item/page information

	-- First check for titles using the specified author information
	INSERT INTO #tmpOpenUrlCitation	(
		PageID, ItemID, TitleID, SegmentID, FullTitle, PublisherPlace, PublisherName, Date, 
		LanguageName, Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, 
		Subjects, StartPage
		)
	SELECT	0,
			0,
			t.TitleID,
			0,
			ISNULL(t.FullTitle, ''),
			ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
			ISNULL(t.Datafield_260_b, '') AS PublisherName,
			'' AS [Date],
			'' AS LanguageName,
			'' AS Volume,
			ISNULL(t.EditionStatement, ''),
			ISNULL(t.CurrentPublicationFrequency, ''),
			CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
			c.Authors,
			c.Subjects,
			'' AS StartPage
	FROM	dbo.Title t LEFT JOIN dbo.TitleAuthor ta
				ON t.TitleID = ta.TitleID
			LEFT JOIN dbo.Author a
				ON ta.AuthorID = a.AuthorID
			LEFT JOIN dbo.AuthorName n
				ON a.AuthorID = n.AuthorID
			INNER JOIN dbo.SearchCatalog c
				ON t.TitleID = c.TitleID
	WHERE	t.PublishReady = 1
	AND		a.IsActive = 1
	AND		(t.FullTitle LIKE @Title OR @Title = '')
	AND		(ISNULL(n.FullName, '&&&&&') LIKE @Author OR 
				ISNULL(n.FullName, '&&&&&') LIKE @AuthorAlt OR
				(@Author = '' AND @AuthorAlt = ''))
	OPTION (RECOMPILE)
END

-- Return the final result set
SELECT DISTINCT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END

GO
