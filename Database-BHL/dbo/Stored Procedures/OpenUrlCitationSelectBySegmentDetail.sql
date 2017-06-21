CREATE PROCEDURE [dbo].[OpenUrlCitationSelectBySegmentDetail]

@ArticleTitle nvarchar(2000) = '',
@ContainerTitle nvarchar(2000) = '',
@AuthorLast nvarchar(150) = '',
@AuthorFirst nvarchar(100) = '',
@Volume nvarchar(100) = '',
@Issue nvarchar(20) = '',
@Year nvarchar(20) = '',
@StartPage nvarchar(20) = ''

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
	PageID int NULL DEFAULT(0),
	ItemID int NULL DEFAULT(0),
	TitleID int NULL DEFAULT(0),
	SegmentID int NULL DEFAULT(0),
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

CREATE TABLE #tmpOpenUrlCitationPage
	(
	PageID int NULL DEFAULT(0),
	ItemID int NULL DEFAULT(0),
	TitleID int NULL DEFAULT(0),
	SegmentID int NULL DEFAULT(0),
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
IF (@ArticleTitle <> '' OR
	@AuthorLast <> '' OR
	@AuthorFirst <> '')
BEGIN
	IF @ArticleTitle <> '' SET @ArticleTitle = '%' + @ArticleTitle + '%'
	IF @ContainerTitle <> '' SET @ContainerTitle = '%' + @ContainerTitle + '%'
	IF @Volume <> '' SET @Volume = '%' + @Volume + '%'
	IF @Issue <> '' SET @Issue = '%' + @Issue + '%'
	IF @Year <> '' SET @Year = '%' + @Year + '%'

	-- UPDATE STATISTICS and OPTION (RECOMPILE) used here to optimize the insert into the temp table
	-- http://sqlblog.com/blogs/paul_white/archive/2012/08/15/temporary-tables-in-stored-procedures.aspx

	UPDATE STATISTICS #tmpOpenUrlCitation

	-- Get the basic segment metadata
	INSERT INTO #tmpOpenUrlCitation	(
		PageID, ItemID, TitleID, SegmentID, SegmentTitle, ContainerTitle, PublisherPlace, 
		PublisherName, Date, LanguageName, Volume, EditionStatement, CurrentPublicationFrequency, 
		Genre, Authors, Subjects, StartPage
		)
	SELECT	ISNULL(StartPageID, 0) AS PageID,
			0 AS ItemID,
			0 AS TitleID,
			s.SegmentID,
			ISNULL(s.Title, '') AS SegmentTitle,
			ISNULL(s.ContainerTitle, '') AS ContainerTitle,
			ISNULL(s.PublisherPlace, '') AS PublisherPlace,
			ISNULL(s.PublisherName, '') AS PublisherName,
			ISNULL(s.Date, '') AS [Date],
			ISNULL(l.LanguageName, '') AS LanguageName,
			ISNULL(s.Volume, '') AS Volume,
			'' AS EditionStatement,
			'' AS CurrentPublicationFrequency,
			ISNULL(g.GenreName, '') AS Genre,
			scs.Authors,
			scs.Subjects,
			s.StartPageNumber AS StartPage
	FROM	dbo.vwSegment s WITH (NOLOCK) 
			INNER JOIN dbo.SegmentGenre g WITH (NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID
			LEFT JOIN dbo.SegmentAuthor sa WITH (NOLOCK) ON s.SegmentID = sa.SegmentID
			LEFT JOIN dbo.Author a WITH (NOLOCK) ON sa.AuthorID = a.AuthorID
			LEFT JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
			LEFT JOIN dbo.Language l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
			INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20)
	AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
	AND		(s.Title LIKE @ArticleTitle OR @ArticleTitle = '')
	AND		(s.ContainerTitle LIKE @ContainerTitle OR @ContainerTitle = '')
	AND		(ISNULL(n.FullName, '&&&&&') LIKE @Author OR 
				ISNULL(n.FullName, '&&&&&') LIKE @AuthorAlt OR
				(@Author = '' AND @AuthorAlt = ''))
	AND		(s.Volume LIKE @Volume OR @Volume = '')
	AND		(s.Issue LIKE @Issue OR @Issue = '')
	AND		(s.Date LIKE @Year OR @Year = '')
	OPTION (RECOMPILE)

	-- Find the specified page
	INSERT INTO #tmpOpenUrlCitationPage (
		PageID, ItemID, TitleID, SegmentID, FullTitle, SegmentTitle, ContainerTitle, PublisherPlace, 
		PublisherName, Date, LanguageName, Volume, EditionStatement, CurrentPublicationFrequency, 
		Genre, Authors, Subjects, StartPage, EndPage, Pages, ISSN, ISBN, LCCN, OCLC, Abbreviation
		)
	SELECT	p.PageID,
			t.ItemID,
			t.TitleID,
			t.SegmentID,
			t.FullTitle,
			t.SegmentTitle,
			t.ContainerTitle,
			t.PublisherPlace,
			t.PublisherName,
			t.[Date],
			t.LanguageName,
			t.Volume,
			t.EditionStatement,
			t.CurrentPublicationFrequency,
			t.Genre,
			t.Authors,
			t.Subjects,
			LTRIM(ip.PagePrefix + ' ' + ip.PageNumber) AS StartPage,
			t.EndPage,
			t.Pages,
			t.ISSN,
			t.ISBN,
			t.LCCN,
			t.OCLC,
			t.Abbreviation
	FROM	#tmpOpenUrlCitation t
			INNER JOIN dbo.vwSegment s ON t.SegmentID = s.SegmentID
			INNER JOIN dbo.SegmentPage sp ON s.SegmentID = sp.SegmentID
			INNER JOIN dbo.Page p ON sp.PageID = p.PageID AND p.Active = 1
			INNER JOIN dbo.IndicatedPage ip ON p.PageID = ip.PageID 
	WHERE	ip.PageNumber = @StartPage
	AND		@StartPage <> ''
END

-- Return the final result set
IF EXISTS(SELECT PageID FROM #tmpOpenUrlCitationPage)
BEGIN
	SELECT DISTINCT * FROM #tmpOpenUrlCitationPage ORDER BY SegmentTitle, Volume, Date, StartPage
END
ELSE
BEGIN
	SELECT DISTINCT * FROM #tmpOpenUrlCitation ORDER BY SegmentTitle, Volume, Date, StartPage
END

END
