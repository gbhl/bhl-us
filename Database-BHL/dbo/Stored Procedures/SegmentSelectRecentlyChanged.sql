CREATE PROCEDURE [dbo].[SegmentSelectRecentlyChanged]

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
FROM	audit.AuditBasic with (nolock)
WHERE	AuditDate >= @StartDate

-- Remove auditing entries related to updates to the LastPageNameLookupDate
DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Page p with (nolock) ON a.EntityKey1 = p.PageID
WHERE	a.EntityName = 'dbo.Page'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), p.LastPageNameLookupDate, 120)

DELETE	#tmpAuditInfo
FROM	#tmpAuditInfo a INNER JOIN dbo.Segment s with (nolock) ON a.EntityKey1 = s.SegmentID
WHERE	a.EntityName = 'dbo.Segment'
AND		CONVERT(nvarchar(19), a.AuditDate, 120) = CONVERT(nvarchar(19), s.LastPageNameLookupDate, 120)


-- Get the IDs of the items associated with each audit record
SELECT DISTINCT s.SegmentID
INTO	#tmpSegment
FROM	dbo.Segment s with (nolock) 
		INNER JOIN #tmpAuditInfo a ON s.SegmentID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.Segment'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.ItemAuthor ia WITH (NOLOCK) 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ia.ItemID = s.ItemID
		INNER JOIN #tmpAuditInfo a ON ia.ItemAuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.ItemAuthor'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.ItemAuthor ia WITH (NOLOCK) 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ia.ItemID = s.ItemID
		INNER JOIN #tmpAuditInfo a ON ia.AuthorID = a.EntityKey1
WHERE	a.EntityName = 'dbo.Author'
AND		Operation <> 'E'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.AuthorName n WITH (NOLOCK) 
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON n.AuthorID = ia.AuthorID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ia.ItemID = s.ItemID
		INNER JOIN #tmpAuditInfo a ON n.AuthorNameID = a.EntityKey1
WHERE	a.EntityName = 'dbo.AuthorName'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.ItemIdentifier ii WITH (NOLOCK) 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ii.ItemID = s.ItemID
		INNER JOIN #tmpAuditInfo a ON ii.ItemIdentifierID = a.EntityKey1 
WHERE	a.EntityName = 'dbo.ItemIdentifier'

UNION

SELECT DISTINCT s.SegmentID
FROM	#tmpAuditInfo a 
		INNER JOIN dbo.ItemKeyword ik WITH (NOLOCK) ON a.EntityKey1 = ik.KeywordID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ik.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.Keyword'
AND		Operation <> 'E'

UNION

SELECT DISTINCT s.SegmentID
FROM	#tmpAuditInfo a 
		INNER JOIN dbo.ItemKeyword ik WITH (NOLOCK) ON a.EntityKey1 = ik.ItemKeywordID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ik.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.ItemKeyword'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.Item i WITH (NOLOCK) 
		INNER JOIN #tmpAuditInfo a ON i.ItemID = a.EntityKey1 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.Item'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.Page p WITH (NOLOCK) 
		INNER JOIN #tmpAuditInfo a ON p.PageID = a.EntityKey1 
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ip.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.Page'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.Page_PageType ppt WITH (NOLOCK) 
		INNER JOIN dbo.Page p WITH (NOLOCK) ON p.PageID = ppt.PageID
		INNER JOIN #tmpAuditInfo a ON ppt.PageID = a.EntityKey1 AND ppt.PageTypeID = a.EntityKey2		
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ip.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.Page_PageType'

UNION

SELECT DISTINCT s.SegmentID
FROM	dbo.IndicatedPage ipg WITH (NOLOCK) 
		INNER JOIN dbo.Page p WITH (NOLOCK) ON p.PageID = ipg.PageID
		INNER JOIN #tmpAuditInfo a ON ipg.PageID = a.EntityKey1 AND ipg.Sequence = a.EntityKey2		
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ip.ItemID = s.ItemID
WHERE	a.EntityName = 'dbo.IndicatedPage'

-- Select a list of distinct segment IDs
SELECT DISTINCT s.SegmentID, s.ItemID, s.BarCode 
FROM	#tmpSegment ts
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ts.SegmentID = s.SegmentID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst WITH (NOLOCK) ON ii.institutioncode = inst.institutioncode
		INNER JOIN dbo.ItemSource src WITH (NOLOCK) ON i.ItemSourceID = src.ItemSourceID
WHERE	i.ItemStatusID IN (30, 40)
AND		inst.BHLMemberLibrary = 1
AND		src.SourceName = 'Internet Archive'	-- we only care about items for which we can upload to IA
AND		r.InstitutionRoleName = 'Contributor'

-- Clean up
DROP TABLE #tmpSegment
DROP TABLE #tmpAuditInfo
END

GO
