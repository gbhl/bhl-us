CREATE PROCEDURE [dbo].[TitleBibTeXSelectAllItemCitations]

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpItem
	(
	TitleID int NOT NULL,
	ItemID int NOT NULL,
	CitationKey nvarchar(17) NOT NULL,
	Url nvarchar(50) NOT NULL,
	Note nvarchar(max) NOT NULL,
	Title nvarchar(2500) NOT NULL,
	Publisher nvarchar(405) NOT NULL,
	[Year] nvarchar(100) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Authors nvarchar(max) NOT NULL,
	Pages int NOT NULL,
	Keywords nvarchar(max) NOT NULL,
	HasLocalContent smallint NOT NULL,
	HasExternalContent smallint NOT NULL
	)

INSERT INTO #tmpItem
SELECT	t.TitleID, i.ItemID, 'bhlitem' + CONVERT(NVARCHAR(10), i.ItemID) AS CitationKey,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), i.ItemID) AS Url,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) + dbo.fnNoteStringForTitle(t.TitleID, ' --- ') AS Note,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.Datafield_260_a, '') + ISNULL(t.Datafield_260_b, '') AS Publisher,
		CASE WHEN i.Year IS NULL THEN ISNULL(t.Datafield_260_c, '') ELSE i.Year END AS [Year],
		ISNULL(i.Volume, '') AS Volume , ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
		c.Authors, 0, c.Subjects AS Keywords, c.HasLocalContent, c.HasExternalContent
FROM	dbo.Title t  WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK)
			ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40

UPDATE	#tmpItem
SET		Pages = dbo.fnCOinSGetPageCountForItem(ItemID)

SELECT	CitationKey, Url, Note, Title, Publisher, [Year], Volume, CopyrightStatus, Authors, Pages, Keywords, HasLocalContent, HasExternalContent
FROM	#tmpItem

END
