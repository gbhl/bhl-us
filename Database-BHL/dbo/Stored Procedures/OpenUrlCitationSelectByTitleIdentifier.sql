
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByTitleIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpOpenUrlCitation
	(
	PageID int NULL,
	ItemID int NULL,
	TitleID int NULL,
	FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
	PublisherPlace nvarchar(150) NOT NULL DEFAULT(''),
	PublisherName nvarchar(255) NOT NULL DEFAULT(''),
	Date nvarchar(20) NOT NULL DEFAULT(''),
	LanguageName nvarchar(20) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
	CurrentPublicationFrequency nvarchar(100) NOT NULL DEFAULT(''),
	Genre nvarchar(20) NOT NULL DEFAULT(''),
	Authors nvarchar(1000) NOT NULL DEFAULT(''),
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
	PageID, ItemID, TitleID, FullTitle, PublisherPlace, PublisherName, Date, LanguageName,
	Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, Subjects,
	StartPage
	)
SELECT	0,
		0,
		t.TitleID,
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
FROM	dbo.Title t WITH (NOLOCK) INNER JOIN dbo.Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierValue = @IdentifierValue
		INNER JOIN dbo.Identifier i WITH (NOLOCK)
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = @IdentifierName
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
WHERE	t.PublishReady = 1

-- Return the final result set
SELECT DISTINCT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END


