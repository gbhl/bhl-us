
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByItemID]

@ItemID int

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
	PageID, ItemID, TitleID, FullTitle, PublisherPlace, PublisherName, Date, LanguageName,
	Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, Subjects,
	StartPage
	)
SELECT	0,
		i.ItemID,
		t.TitleID,
		ISNULL(t.FullTitle, ''),
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS PublisherName,
		'' AS [Date],
		'' AS LanguageName,
		i.Volume AS Volume,
		ISNULL(t.EditionStatement, ''),
		ISNULL(t.CurrentPublicationFrequency, ''),
		CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
		c.Authors,
		c.Subjects,
		'' AS StartPage
FROM	dbo.Item i WITH (NOLOCK) INNER JOIN dbo.Title t WITH (NOLOCK)
			ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		i.ItemID = @ItemID

-- Return the final result set
SELECT * FROM #tmpOpenUrlCitation ORDER BY FullTitle, Volume, Date, StartPage

END







