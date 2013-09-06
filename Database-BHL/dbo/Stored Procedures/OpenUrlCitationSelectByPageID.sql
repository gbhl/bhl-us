
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByPageID]

@PageID int

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpOpenUrlCitation
	(
	PageID int NULL,
	ItemID int NULL,
	TitleID int NULL,
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
SELECT	p.PageID,
		i.ItemID,
		t.TitleID,
		ISNULL(t.FullTitle, ''),
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS PublisherName,
		ISNULL(p.Year, ISNULL(i.Year, CONVERT(nvarchar(20), ISNULL(t.StartYear, '')))) AS [Date],
		ISNULL(l.LanguageName, ''),
		ISNULL(i.Volume, ''),
		ISNULL(t.EditionStatement, ''),
		ISNULL(t.CurrentPublicationFrequency, ''),
		CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
		c.Authors,
		c.Subjects,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS StartPage
FROM	dbo.Page p WITH (NOLOCK) INNER JOIN dbo.Item i WITH (NOLOCK)
			ON p.ItemID = i.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK)
			ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.Language l WITH (NOLOCK)
			ON i.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND i.ItemID = c.ItemID
WHERE	p.PageID = @PageID
AND		p.Active = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

-- Get the title identifiers
UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 2

UPDATE	#tmpOpenUrlCitation
SET		ISBN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 3

UPDATE	#tmpOpenUrlCitation
SET		LCCN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 5

UPDATE	#tmpOpenUrlCitation
SET		OCLC = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 1

UPDATE	#tmpOpenUrlCitation
SET		Abbreviation = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 6

-- Return the final result set
SELECT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END




