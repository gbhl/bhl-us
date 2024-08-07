CREATE PROCEDURE [dbo].[ItemSelectRecentlyChanged]

@StartDate datetime

AS

BEGIN

SET NOCOUNT ON

-- Get IDs of all items updated since a specified date/time

CREATE TABLE #tmpAuditInfo 
(
	AuditDate datetime NOT NULL,
	Operation nchar(1) NOT NULL,
	EntityName nvarchar(50) NOT NULL, 
	EntityKey1 nvarchar(100) NULL,
	EntityKey2 nvarchar(100) NULL,
	EntityKey3 nvarchar(100) NULL
)

-- Get any recent auditing entries related to the entities/keys that we just collected
-- Current auditing table (last 15 days)
INSERT #tmpAuditInfo
SELECT	AuditDate, Operation, EntityName, EntityKey1, EntityKey2, EntityKey3
FROM	audit.AuditBasic
WHERE	AuditDate >= @StartDate

-- Remove auditing entries related to updates to the LastPageNameLookupDate
DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Page p ON a.EntityKey1 = p.PageID
WHERE	a.EntityName = 'dbo.Page'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), p.LastPageNameLookupDate, 120)

DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Book b ON a.EntityKey1 = b.BookID
WHERE	a.EntityName = 'dbo.Book'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), b.LastPageNameLookupDate, 120)


-- Get the IDs of the items associated with each audit record
SELECT DISTINCT b.BookID
INTO	#tmpItem
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON t.TitleID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Title'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleAuthor ta
		INNER JOIN dbo.ItemTitle it ON ta.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON ta.TitleAuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.TitleAuthor'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleAuthor ta
		INNER JOIN dbo.ItemTitle it ON ta.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON ta.AuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.Author'
AND		Operation <> 'E'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.AuthorName n
		INNER JOIN dbo.TitleAuthor ta ON n.AuthorID = ta.AuthorID
		INNER JOIN dbo.ItemTitle it ON ta.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON n.AuthorNameID = a.EntityKey1
WHERE	a.EntityName = 'dbo.AuthorName'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleAssociation ta
		INNER JOIN dbo.ItemTitle it ON ta.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON ta.TitleAssociationID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleAssociation'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleAssociation ta
		INNER JOIN dbo.ItemTitle it ON ta.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.TitleAssociation_TitleIdentifier tati ON ta.TitleAssociationID = tati.TitleAssociationID
		INNER JOIN #tmpAuditInfo a ON tati.TitleAssociation_TitleIdentifierID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleAssociation_TitleIdentifier'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleLanguage tl
		INNER JOIN dbo.ItemTitle it ON tl.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON tl.TitleLanguageID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleLanguage'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.Title_Identifier ti
		INNER JOIN dbo.ItemTitle it ON ti.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.Identifier i ON ti.IdentifierID = i.IdentifierID
		INNER JOIN #tmpAuditInfo a ON ti.TitleIdentifierID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Title_Identifier'
-- Only consider changes to identifiers that are part of MODS/METS outputs
AND		i.IdentifierName IN ('ISBN', 'ISSN', 'EISSN', 'OCLC', 'NLM', 'DLC', 'DOI')

UNION

SELECT DISTINCT b.BookID
FROM	dbo.TitleVariant tv
		INNER JOIN dbo.ItemTitle it ON tv.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN #tmpAuditInfo a ON tv.TitleVariantID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.TitleVariant'

UNION

SELECT DISTINCT b.BookID
FROM	#tmpAuditInfo a 
		INNER JOIN dbo.TitleKeyword k ON a.EntityKey1 = k.KeywordID
		INNER JOIN dbo.ItemTitle it ON k.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.Keyword'
AND		Operation <> 'E'

UNION

SELECT DISTINCT b.BookID
FROM	#tmpAuditInfo a 
		INNER JOIN dbo.TitleKeyword k ON a.EntityKey1 = k.TitleKeywordID
		INNER JOIN dbo.ItemTitle it ON k.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.TitleKeyword'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.Item i
		INNER JOIN #tmpAuditInfo a ON i.ItemID = a.EntityKey1 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.Item'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.Book b INNER JOIN #tmpAuditInfo a ON b.BookID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Book'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.ItemLanguage il
		INNER JOIN #tmpAuditInfo a ON il.ItemLanguageID = a.EntityKey1 
		INNER JOIN dbo.Book b ON il.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.ItemLanguage'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.Page p
		INNER JOIN #tmpAuditInfo a ON p.PageID = a.EntityKey1 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.Page'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.Page_PageType ppt
		INNER JOIN dbo.Page p ON p.PageID = ppt.PageID
		INNER JOIN #tmpAuditInfo a ON ppt.PageID = a.EntityKey1 AND ppt.PageTypeID = a.EntityKey2		
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.Page_PageType'

UNION

SELECT DISTINCT b.BookID
FROM	dbo.IndicatedPage ipg INNER JOIN dbo.Page p ON p.PageID = ipg.PageID
		INNER JOIN #tmpAuditInfo a ON ipg.PageID = a.EntityKey1 AND ipg.Sequence = a.EntityKey2		
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	a.EntityName = 'dbo.IndicatedPage'

-- Select a list of distinct item IDs
SELECT DISTINCT b.BookID, b.ItemID, pt.TitleID AS PrimaryTitleID, b.BarCode 
FROM	#tmpItem ti
		INNER JOIN dbo.Book b ON ti.BookID = b.BookID
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst ON ii.institutioncode = inst.institutioncode
		INNER JOIN dbo.ItemSource src ON i.ItemSourceID = src.ItemSourceID
WHERE	i.ItemStatusID = 40 
AND		inst.BHLMemberLibrary = 1
AND		src.SourceName = 'Internet Archive'	-- we only care about items for which we can upload to IA
AND		r.InstitutionRoleName = 'Holding Institution'

-- Clean up
DROP TABLE #tmpItem
DROP TABLE #tmpAuditInfo
END

GO
