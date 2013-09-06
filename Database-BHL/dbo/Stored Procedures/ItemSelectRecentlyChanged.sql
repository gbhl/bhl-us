
CREATE PROCEDURE [dbo].[ItemSelectRecentlyChanged]
	@StartDate datetime
AS
BEGIN
	SET NOCOUNT ON

-- Get IDs of all items updated since a specified date/time

CREATE TABLE #tmpAuditInfo 
(
	AuditDate datetime NOT NULL,
	EntityName nvarchar(50) NOT NULL, 
	EntityKey1 nvarchar(100) NULL,
	EntityKey2 nvarchar(100) NULL,
	EntityKey3 nvarchar(100) NULL
)

-- Get any recent auditing entries related to the entities/keys that we just collected
-- Current auditing table (last 15 days)
INSERT #tmpAuditInfo
SELECT	AuditDate, EntityName, EntityKey1, EntityKey2, EntityKey3
FROM	audit.AuditBasic with (nolock)
WHERE	AuditDate >= @StartDate

-- Remove auditing entries related to updates to the LastPageNameLookupDate
DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Page p with (nolock) ON a.EntityKey1 = p.PageID
WHERE	a.EntityName = 'dbo.Page'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), p.LastPageNameLookupDate, 120)

DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Item i with (nolock) ON a.EntityKey1 = i.ItemID
WHERE	a.EntityName = 'dbo.Item'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), i.LastPageNameLookupDate, 120)


-- Get the IDs of the items associated with each audit record
SELECT DISTINCT i.ItemID 
INTO	#tmpItem
FROM	dbo.Title t with (nolock) INNER JOIN dbo.Item i with (nolock) ON t.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON t.TitleID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Title'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.TitleAuthor ta WITH (NOLOCK) INNER JOIN dbo.Item i WITH (NOLOCK) ON ta.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON ta.TitleAuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.TitleAuthor'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.TitleAuthor ta WITH (NOLOCK) INNER JOIN dbo.Item i WITH (NOLOCK) ON ta.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON ta.AuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.Author'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.AuthorName n WITH (NOLOCK) INNER JOIN dbo.TitleAuthor ta ON n.AuthorID = ta.AuthorID
		INNER JOIN dbo.Item i ON ta.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON n.AuthorNameID = a.EntityKey1
WHERE	a.EntityName = 'dbo.AuthorName'

UNION
/*
-- For METS creation, nothing in the TitleItem table is relevant, so ignore audit entries for this table
SELECT DISTINCT i.ItemID 
FROM	dbo.TitleItem ti INNER JOIN dbo.Item i ON ti.TitleID = i.PrimaryTitleID AND ti.ItemID = i.ItemID
		INNER JOIN #tmpAuditInfo a ON ti.TitleItemID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleItem'

UNION
*/
SELECT DISTINCT i.ItemID 
FROM	dbo.TitleAssociation ta with (nolock) INNER JOIN dbo.Item i with (nolock) ON ta.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON ta.TitleAssociationID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleAssociation'

UNION

SELECT DISTINCT i.ItemID 
FROM	dbo.TitleAssociation ta with (nolock) INNER JOIN dbo.Item i with (nolock) ON ta.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.TitleAssociation_TitleIdentifier tati with(nolock) ON ta.TitleAssociationID = tati.TitleAssociationID
		INNER JOIN #tmpAuditInfo a ON tati.TitleAssociation_TitleIdentifierID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleAssociation_TitleIdentifier'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.TitleLanguage tl with (nolock) INNER JOIN dbo.Item i with (nolock) ON tl.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON tl.TitleLanguageID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleLanguage'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.Title_Identifier ti with (nolock) INNER JOIN dbo.Item i with (nolock) ON ti.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON ti.TitleIdentifierID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Title_Identifier'

UNION

SELECT DISTINCT i.ItemID
FROM	dbo.TitleVariant tv with (nolock) INNER JOIN dbo.Item i with (nolock) ON tv.TitleID = i.PrimaryTitleID
		INNER JOIN #tmpAuditInfo a ON tv.TitleVariantID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleVariant'

UNION

SELECT DISTINCT i.ItemID
FROM	#tmpAuditInfo a INNER JOIN dbo.TitleKeyword k ON a.EntityKey1 = k.KeywordID
		INNER JOIN dbo.Item i ON k.TitleID = i.PrimaryTitleID
WHERE	a.EntityName = 'dbo.Keyword'

UNION

SELECT DISTINCT i.ItemID
FROM	#tmpAuditInfo a INNER JOIN dbo.TitleKeyword k ON a.EntityKey1 = k.TitleKeywordID
		INNER JOIN dbo.Item i ON k.TitleID = i.PrimaryTitleID
WHERE	a.EntityName = 'dbo.TitleKeyword'

UNION

SELECT DISTINCT i.ItemID 
FROM	dbo.Item i with (nolock) INNER JOIN #tmpAuditInfo a ON i.ItemID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Item'

UNION

SELECT DISTINCT il.ItemID
FROM	dbo.ItemLanguage il with (nolock) INNER JOIN #tmpAuditInfo a ON il.ItemLanguageID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.ItemLanguage'

UNION

SELECT DISTINCT p.ItemID
FROM	dbo.Page p with (nolock) INNER JOIN #tmpAuditInfo a ON p.PageID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Page'

UNION

SELECT DISTINCT p.ItemID
FROM	dbo.Page_PageType ppt with (nolock) INNER JOIN dbo.Page p with (nolock) ON p.PageID = ppt.PageID
		INNER JOIN #tmpAuditInfo a ON ppt.PageID = a.EntityKey1 AND ppt.PageTypeID = a.EntityKey2		
WHERE	a.EntityName = 'dbo.Page_PageType'

UNION

SELECT DISTINCT p.ItemID
FROM	dbo.IndicatedPage ip with (nolock) INNER JOIN dbo.Page p with(nolock) ON p.PageID = ip.PageID
		INNER JOIN #tmpAuditInfo a ON ip.PageID = a.EntityKey1 AND ip.Sequence = a.EntityKey2		
WHERE	a.EntityName = 'dbo.IndicatedPage'

-- Select a list of distinct item IDs
SELECT DISTINCT ti.ItemID, PrimaryTitleID, BarCode FROM #tmpItem ti
INNER JOIN Item i with (nolock) on ti.ItemID = i.ItemID
inner join institution inst with (nolock) on i.institutioncode = inst.institutioncode
where itemstatusid = 40 and inst.bhlmemberlibrary = 1
--SELECT DISTINCT i.ItemID, PrimaryTitleID, BarCode FROM Item i with (nolock)
--inner join institution inst with (nolock) on i.institutioncode = inst.institutioncode
--where itemstatusid = 40 and inst.bhlmemberlibrary = 1

-- Clean up
DROP TABLE #tmpItem
DROP TABLE #tmpAuditInfo
END




