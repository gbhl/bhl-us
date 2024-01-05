CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByTitleDetailFT]

@Title nvarchar(2000) = '',
@AuthorLast nvarchar(150) = '',
@AuthorFirst nvarchar(100) = ''

AS

BEGIN

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.OpenUrlCitationSelectByTitleDetail @Title, @AuthorLast, @AuthorFirst
	RETURN
END


DECLARE @SearchTitle nvarchar(4000)
DECLARE @SearchAuthor nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @SearchTitle = dbo.fnGetFullTextSearchString(@Title)
SELECT @SearchAuthor = dbo.fnGetFullTextSearchString(@AuthorFirst + ' ' + @AuthorLast)

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
			CASE WHEN t.BibliographicLevelID IN (1, 4) THEN 'Book' ELSE 'Journal' END AS Genre,
			s.Authors,
			s.Subjects,
			'' AS StartPage
	FROM	SearchCatalog s
			INNER JOIN dbo.Title t ON s.TitleID = t.TitleID
	WHERE	t.PublishReady = 1
	AND		(CONTAINS((s.FullTitle, s.UniformTitle), @SearchTitle) OR @SearchTitle = '"**"')
	AND		(CONTAINS(s.SearchAuthors, @SearchAuthor) OR @SearchAuthor = '"**"')
	OPTION (RECOMPILE)
END

-- Return the final result set
SELECT DISTINCT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, SegmentTitle, Volume, Date, StartPage

END
GO
