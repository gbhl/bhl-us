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
		b.BookID AS ItemID,
		t.TitleID,
		ISNULL(t.FullTitle, ''),
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS PublisherName,
		ISNULL(p.Year, ISNULL(b.StartYear, CONVERT(nvarchar(20), ISNULL(t.StartYear, '')))) AS [Date],
		ISNULL(l.LanguageName, ''),
		ISNULL(b.Volume, ''),
		ISNULL(t.EditionStatement, ''),
		ISNULL(t.CurrentPublicationFrequency, ''),
		CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
		c.Authors,
		c.Subjects,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS StartPage
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	p.PageID = @PageID
AND		p.Active = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

-- Get the title identifiers
UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'ISSN'

UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'eISSN'
WHERE	t.ISSN = ''

UPDATE	#tmpOpenUrlCitation
SET		ISBN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'ISBN'

UPDATE	#tmpOpenUrlCitation
SET		LCCN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'DLC'

UPDATE	#tmpOpenUrlCitation
SET		OCLC = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'OCLC'

UPDATE	#tmpOpenUrlCitation
SET		Abbreviation = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'Abbreviation'


-- Return the final result set
SELECT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END

GO
