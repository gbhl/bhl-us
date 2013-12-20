
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectBySegmentDetailFT]

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

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.OpenUrlCitationSelectBySegmentDetail @ArticleTitle, @ContainerTitle, 
			@AuthorLast, @AuthorFirst, @Volume, @Issue, @Year, @StartPage
	RETURN
END


DECLARE @SearchArticleTitle nvarchar(4000)
DECLARE @SearchContainerTitle nvarchar(4000)
DECLARE @SearchAuthor nvarchar(4000)
DECLARE @SearchVolume nvarchar(4000)
DECLARE @SearchIssue nvarchar(4000)
DECLARE @SearchYear nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @SearchArticleTitle = dbo.fnGetFullTextSearchString(@ArticleTitle)
SELECT @SearchContainerTitle = dbo.fnGetFullTextSearchString(@ContainerTitle)
SELECT @SearchAuthor = dbo.fnGetFullTextSearchString(@AuthorFirst + ' ' + @AuthorLast)
SELECT @SearchVolume = dbo.fnGetFullTextSearchString(@Volume)
SELECT @SearchIssue = dbo.fnGetFullTextSearchString(@Issue)
SELECT @SearchYear = dbo.fnGetFullTextSearchString(@Year)

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

	-- UPDATE STATISTICS and OPTION (RECOMPILE) used here to optimize the insert into the temp table
	-- http://sqlblog.com/blogs/paul_white/archive/2012/08/15/temporary-tables-in-stored-procedures.aspx

	UPDATE STATISTICS #tmpOpenUrlCitation

	-- Get the basic segment metadata
	INSERT INTO #tmpOpenUrlCitation	(
		PageID, ItemID, TitleID, SegmentID, SegmentTitle, ContainerTitle, PublisherPlace, 
		PublisherName, Date, LanguageName, Volume, EditionStatement, CurrentPublicationFrequency, 
		Genre, Authors, Subjects, StartPage
		)
	SELECT	ISNULL(s.StartPageID, 0) AS PageID,
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
			c.Authors,
			c.Subjects,
			s.StartPageNumber AS StartPage
	FROM	SearchCatalogSegment c WITH (NOLOCK)
			INNER JOIN dbo.Segment s WITH (NOLOCK) ON c.SegmentID = s.SegmentID
			INNER JOIN dbo.SegmentGenre g WITH (NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID
			LEFT JOIN dbo.Language l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
	WHERE	s.SegmentStatusID IN (10, 20)
	AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)
	AND		(CONTAINS((c.Title), @SearchArticleTitle) OR @SearchArticleTitle = '"**"')
	AND		(CONTAINS((c.ContainerTitle), @SearchContainerTitle) OR @SearchContainerTitle = '"**"')
	AND		(CONTAINS(c.Authors, @SearchAuthor) OR @SearchAuthor = '"**"')
	AND		(CONTAINS(c.Volume, @SearchVolume) OR @SearchVolume = '"**"')
	AND		(CONTAINS(c.Issue, @SearchIssue) OR @SearchIssue = '"**"')
	AND		(CONTAINS(c.Date, @SearchYear) OR @SearchYear = '"**"')
	OPTION (RECOMPILE)

	-- Find the specified page
	INSERT INTO #tmpOpenUrlCitationPage	(
		PageID, ItemID, TitleID, SegmentID, SegmentTitle, ContainerTitle, PublisherPlace, 
		PublisherName, Date, LanguageName, Volume, EditionStatement, CurrentPublicationFrequency, 
		Genre, Authors, Subjects, StartPage, EndPage, Pages, ISSN, ISBN, LCCN, OCLC, Abbreviation
		)
	SELECT	p.PageID,
			t.ItemID,
			t.TitleID,
			t.SegmentID,
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
			INNER JOIN dbo.Segment s ON t.SegmentID = s.SegmentID
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


