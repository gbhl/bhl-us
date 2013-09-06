
CREATE VIEW [dbo].[ItemCOinSView]
AS
SELECT DISTINCT 
		t.TitleID
		,i.ItemID
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'DLC') AS 'lccn' -- id for book/journal/dc
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'OCLC') AS 'oclc' -- id for book/journal/dc
		,t.FullTitle AS 'rft_title' -- book (btitle)/journal(jtitle)/dc
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'Abbreviation') AS 'rft_stitle' -- journal
		,ISNULL(i.Volume, '') AS 'rft_volume' -- journal
		,ISNULL(LOWER(t.LanguageCode), '') AS 'rft_language' -- dc
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'ISBN') AS 'rft_isbn' -- book/journal
		,CASE WHEN CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')) > 0
			THEN LTRIM(SUBSTRING(dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100'), 1, CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')) - 1))
			ELSE ISNULL(dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100'), '')
		END AS 'rft_aulast' -- book/journal
		,CASE WHEN CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')) > 0
			THEN LTRIM(REPLACE(SUBSTRING(dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100'), CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')), LEN(dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')) - CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '100')) + 1), ',', ''))
			ELSE ''
		END AS 'rft_aufirst' -- book/journal
		,dbo.fnCOinSAuthorStringForTitle(t.TitleID, 0) AS 'rft_au_BOOK' -- book/journal
		,dbo.fnCOinSAuthorStringForTitle(t.TitleID, 1) AS 'rft_au_DC' -- dc
		,dbo.fnCOinSGetFirstAuthorNameForTitle(t.TitleID, '110') AS 'rft_aucorp' -- book/journal
		,ISNULL(t.Datafield_260_a, '') AS 'rft_place' -- book
		,ISNULL(t.Datafield_260_b, '') AS 'rft_pub' -- book
		,ISNULL(t.Datafield_260_b, '') AS 'rft_publisher' -- dc
		,ISNULL(i.Year, '') AS 'rft_date_ITEM' -- book/journal/dc
		,ISNULL(CONVERT(nvarchar(20), t.StartYear), '') AS 'rft_date_TITLE' -- book/journal/dc
		,ISNULL(t.EditionStatement, '') AS 'rft_edition' -- book
		-- Need to use rft_pages, not rft_tpages???
		-- http://forums.zotero.org/discussion/4292/coins-issues/
		,dbo.fnCOinSGetPageCountForItem (i.ItemID) AS 'rft_tpages' -- book/journal
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'ISSN') AS 'rft_issn' -- book/journal
		,dbo.fnGetIdentifierForTitle(t.TitleID, 'CODEN') AS 'rft_coden' -- journal
		,dbo.fnKeywordStringForTitle(t.TitleID) AS 'rft_subject' -- dc
		,inst.InstitutionName AS 'rft_contributor_ITEM' -- dc
		,inst2.InstitutionName AS 'rft_contributor_TITLE' -- dc
		,CASE WHEN SUBSTRING(t.MARCLeader, 8, 1) IN ('s','b') THEN 'journal'
			WHEN SUBSTRING(t.MARCLeader, 8, 1) IN ('a','m') THEN 'book'
			ELSE 'unknown' END AS 'rft_genre' -- book/journal/dc
FROM	dbo.Title t INNER JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.Institution inst
			ON i.InstitutionCode = inst.InstitutionCode
		LEFT JOIN dbo.Institution inst2
			ON t.InstitutionCode = inst2.InstitutionCode
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40



