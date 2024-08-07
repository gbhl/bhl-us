CREATE PROCEDURE [dbo].[ReportSelectOrphanedEntities]

AS

BEGIN

SET NOCOUNT ON

-- Gather the orphaned titles
SELECT	'Title' AS [Type],
		t.TitleID AS ID,
		'Published' AS [Status],
		t.RedirectTitleID AS ReplacedBy,
		NULL AS HasActiveTitles,
		0 AS HasActiveItems,
		NULL AS HasActiveSegments,
		MAX(i.LastModifiedDate) AS RelatedLastModified
INTO	#tmp
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
WHERE	t.PublishReady = 1
GROUP BY t.TitleID, t.RedirectTitleID
HAVING SUM(CASE WHEN i.ItemStatusID = 40 THEN 1 ELSE 0 END) = 0

-- Get the orphaned items and add them to the list of orphaned titles
SELECT	'Item' AS [Type],
		b.BookID AS ID,
		istat.ItemStatusName AS [Status],
		b.RedirectBookID AS ReplacedBy,
		inst.InstitutionName,
		0 AS HasActiveTitles,
		NULL AS HasActiveItems,
		NULL AS HasActiveSegments,
		MAX(t.LastModifiedDate) AS RelatedLastModified
FROM	dbo.Item I
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.titleid = t.titleid
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus istat ON i.ItemStatusID = istat.ItemStatusID
WHERE	i.ItemStatusID = 40
GROUP BY b.BookID, istat.ItemStatusName, b.RedirectBookID, inst.InstitutionName 
HAVING SUM(CAST(t.PublishReady AS INT)) = 0
UNION
-- Get the orphaned titles (and add the title contributors)
SELECT	[Type],
		ID,
		[Status],
		ReplacedBy,
		dbo.fnContributorStringForTitle(ID, 0) AS InstitutionName,
		HasActiveTitles,
		HasActiveItems,
		HasActiveSegments,
		RelatedLastModified
FROM	#tmp
UNION
-- Get inactive items with orphaned segments
SELECT	'Item' AS [Type],
		b.BookID AS ID,
		istat.ItemStatusName AS [Status],
		b.RedirectBookID AS ReplacedBy,
		inst.InstitutionName,
		NULL AS HasActiveTitles,
		NULL AS HasActiveItems,
		1 AS HasActiveSegments,
		MAX(CASE WHEN s.LastModifiedDate > si.LastModifiedDate THEN s.LastModifiedDate ELSE si.LastModifiedDate END) AS RelatedLastModified
FROM	dbo.Segment s
		INNER JOIN dbo.Item si ON s.ItemID = si.ItemID
		INNER JOIN dbo.ItemRelationship ir ON si.ItemID = ir.ChildID
		INNER JOIN dbo.Item bi ON ir.ParentID = bi.ItemID
		INNER JOIN dbo.Book b ON bi.ItemID = b.ItemID
		INNER JOIN dbo.ItemInstitution ii ON bi.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus istat ON bi.ItemStatusID = istat.ItemStatusID
WHERE	si.ItemStatusID IN (30, 40)
GROUP BY b.BookID, istat.ItemStatusName, b.RedirectBookID, inst.InstitutionName
HAVING SUM(CASE WHEN bi.ItemStatusID = 40 THEN 1 ELSE 0 END) = 0
ORDER BY InstitutionName, [Type], ID

END

GO
