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
FROM	dbo.Title t WITH (NOLOCK) 
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst WITH (NOLOCK) ON ii.InstitutionCode = inst.InstitutionCode
WHERE	t.PublishReady = 1
GROUP BY t.TitleID, t.RedirectTitleID
HAVING SUM(CASE WHEN i.ItemStatusID = 40 THEN 1 ELSE 0 END) = 0

-- Get the orphaned items and add them to the list of orphaned titles
SELECT	'Item' AS [Type],
		i.ItemID AS ID,
		istat.ItemStatusName AS [Status],
		i.RedirectItemID AS ReplacedBy,
		inst.InstitutionName,
		0 AS HasActiveTitles,
		NULL AS HasActiveItems,
		NULL AS HasActiveSegments,
		MAX(t.LastModifiedDate) AS RelatedLastModified
FROM	dbo.Item I WITH (NOLOCK) 
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.titleid = t.titleid
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst WITH (NOLOCK) ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus istat WITH (NOLOCK) ON i.ItemStatusID = istat.ItemStatusID
WHERE	i.ItemStatusID = 40
GROUP BY i.ItemID, istat.ItemStatusName, i.RedirectItemID, inst.InstitutionName 
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
		s.ItemID AS ID,
		istat.ItemStatusName AS [Status],
		i.RedirectItemID AS ReplacedBy,
		inst.InstitutionName,
		NULL AS HasActiveTitles,
		NULL AS HasActiveItems,
		1 AS HasActiveSegments,
		MAX(i.LastModifiedDate) AS RelatedLastModified
FROM	dbo.Segment s WITH (NOLOCK) 
		INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) 
			ON ii.InstitutionRoleID = r.InstitutionRoleID 
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst WITH (NOLOCK) ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus istat WITH (NOLOCK) ON i.ItemStatusID = istat.ItemStatusID
WHERE	s.SegmentStatusID IN (10, 20)
GROUP BY s.ItemID, istat.ItemStatusName, i.RedirectItemID, inst.InstitutionName
HAVING SUM(CASE WHEN i.ItemStatusID = 40 THEN 1 ELSE 0 END) = 0
ORDER BY InstitutionName, [Type], ID

END
