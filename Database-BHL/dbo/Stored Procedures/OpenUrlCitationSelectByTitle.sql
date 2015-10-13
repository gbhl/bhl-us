
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByTitle]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

-- If the title has been redirected to a different title, then use
-- that title instead.  Follow the "redirect" chain up to ten levels.
SELECT	@TitleID = COALESCE(t10.TitleID, t9.TitleID, t8.TitleiD, t7.TitleID, t6.TitleID,
						t5.TitleID, t4.TitleID, t3.TitleID, t2.TitleID, t1.TitleID)
FROM	dbo.Title t1
		LEFT JOIN dbo.Title t2 ON t1.RedirectTitleID = t2.TitleID
		LEFT JOIN dbo.Title t3 ON t2.RedirectTitleID = t3.TitleID
		LEFT JOIN dbo.Title t4 ON t3.RedirectTitleID = t4.TitleID
		LEFT JOIN dbo.Title t5 ON t4.RedirectTitleID = t5.TitleID
		LEFT JOIN dbo.Title t6 ON t5.RedirectTitleID = t6.TitleID
		LEFT JOIN dbo.Title t7 ON t6.RedirectTitleID = t7.TitleID
		LEFT JOIN dbo.Title t8 ON t7.RedirectTitleID = t8.TitleID
		LEFT JOIN dbo.Title t9 ON t8.RedirectTitleID = t9.TitleID
		LEFT JOIN dbo.Title t10 ON t9.RedirectTitleID = t10.TitleID
WHERE	t1.TitleID = @TitleID


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

-- Get the basic title/item/page information
INSERT INTO #tmpOpenUrlCitation	(
	PageID, ItemID, TitleID, SegmentID, FullTitle, PublisherPlace, PublisherName, Date, LanguageName,
	Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, Subjects,
	StartPage
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
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
WHERE	t.PublishReady = 1
AND		t.TitleID = @TitleID

-- Return the final result set
SELECT DISTINCT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END

