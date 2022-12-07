CREATE PROCEDURE [audit].[AuditBasicSelectForDOIQueue]

@StartDate datetime = NULL,
@EndDate datetime = NULL

AS

BEGIN

SET NOCOUNT ON

-- Get the date parameters, if none were supplied
IF (@StartDate IS NULL)
BEGIN
	SELECT @StartDate = MAX(LastAuditDate) FROM dbo.SearchIndexQueueLog
	IF (@StartDate IS NULL) SELECT TOP 1 @StartDate = AuditDate FROM audit.AuditBasic ORDER BY AuditBasicID
END

IF (@EndDate IS NULL) SET @EndDate = GETDATE()

-- Get the bibliographic levels that are valid for DOI assignment
SELECT	BibliographicLevelID
INTO	#BibliographicLevel
FROM	dbo.BibliographicLevel
WHERE	BibliographicLevelName NOT IN ('Collection', 'Subunit', 'Integrating resource')

-- Book
SELECT	AuditBasicID AS AuditID, Operation, EntityName, 'title' AS IndexEntity, t.TitleID AS EntityID1, NULL AS EntityID2, AuditDate
INTO	#Raw
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ab.EntityKey1 = t.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemiD
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Title'
AND		i.ItemStatusID = 40
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'title', t.TitleID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON ab.EntityKey1 = ta.AuthorID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON ta.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Author'
AND		Operation <> 'E'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'title', t.TitleID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON ab.EntityKey1 = n.AuthorNameID
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON n.AuthorID = ta.AuthorID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON ta.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorName'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'title', t.TitleID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorIdentifier ai WITH (NOLOCK) ON ab.EntityKey1 = ai.AuthorIdentifierID
		INNER JOIN dbo.Identifier id WITH (NOLOCK) ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'ORCID'
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON ai.AuthorID = ta.AuthorID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON ta.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorIdentifier'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'title', t.TitleID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON ab.EntityKey1 = ta.TitleAuthorID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON ta.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleAuthor'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'title', t.TitleID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title_Identifier tid WITH (NOLOCK) ON ab.EntityKey1 = tid.TitleIdentifierID
		INNER JOIN dbo.Identifier id WITH (NOLOCK) ON tid.IdentifierID = id.IdentifierID AND id.IdentifierType IN ('ISBN', 'ISSN')
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON tid.TitleID = it.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Title_Identifier'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		t.BibliographicLevelID IN (SELECT BibliographicLevelID FROM #BibliographicLevel)

UNION

-- Segment

SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', ab.EntityKey1 AS SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.Segment'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON ab.EntityKey1 = ia.AuthorID
		INNER JOIN dbo.vwSegment s ON ia.ItemID = s.ItemID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Author'
AND		s.SegmentStatusID IN (30, 40)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON ab.EntityKey1 = n.AuthorNameID
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON n.AuthorID = ia.AuthorID
		INNER JOIN dbo.vwSegment s ON ia.ItemID = s.ItemID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorName'
AND		s.SegmentStatusID IN (30, 40)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorIdentifier i WITH (NOLOCK) ON ab.EntityKey1 = i.AuthorIdentifierID
		INNER JOIN dbo.Identifier id WITH (NOLOCK) ON i.IdentifierID = id.IdentifierID AND id.IdentifierName = 'ORCID'
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON i.AuthorID = ia.AuthorID
		INNER JOIN dbo.vwSegment s ON ia.ItemID = s.ItemID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorIdentifier'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON ab.EntityKey1 = ia.ItemAuthorID
		INNER JOIN dbo.vwSegment s ON ia.ItemID = s.ItemID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.ItemAuthor'
AND		s.SegmentStatusID IN (30, 40)


-- ### Get initial reduced list of changed entities ###
SELECT	MIN(AuditID) AS AuditID,
		CASE 
			WHEN Operation IN ('I', 'U', 'E') THEN 'put'
			WHEN Operation = 'D' AND EntityName <> 'dbo.' + IndexEntity THEN 'put'
			ELSE 'delete' -- Operation = 'd' AND EntityName = IndexEntity
		END AS Operation,
		IndexEntity, 
		EntityID1, 
		EntityID2,
		'Search' AS [Queue],
		MIN(AuditDate) AS AuditDate
INTO	#DOI
FROM	#Raw
GROUP BY
		CASE 
			WHEN Operation IN ('I', 'U', 'E') THEN 'put'
			WHEN Operation = 'D' AND EntityName <> 'dbo.' + IndexEntity THEN 'put'
			ELSE 'delete'
		END,
		IndexEntity, 
		EntityID1,
		EntityID2


-- ### Final result set ###

-- ## Determine affected titles/segments that have BHL-assigned DOIs, and return entries for the DOI queue

-- Order by Date so that entities are indexed in the order they were changed.
SELECT	d.AuditID,
		d.Operation,
		d.IndexEntity, 
		ISNULL(CONVERT(NVARCHAR(100), d.EntityID1), '') AS EntityID1,
		NULL AS EntityID2,
		'DOI' AS [Queue],
		d.AuditDate
FROM	#DOI d
		INNER JOIN dbo.Title_Identifier ti ON d.EntityID1 = ti.TitleID AND ti.IdentifierValue LIKE '%10.5962%'
		INNER JOIN dbo.Identifier id ON ti.IdentifierID = id.IdentifierID AND id.IdentifierName = 'DOI'
WHERE	d.IndexEntity = 'title'
UNION
SELECT	d.AuditID,
		d.Operation,
		d.IndexEntity, 
		ISNULL(CONVERT(NVARCHAR(100), d.EntityID1), '') AS EntityID1,
		NULL AS EntityID2,
		'DOI' AS [Queue],
		d.AuditDate
FROM	#DOI d
		INNER JOIN dbo.Segment s ON d.EntityID1 = s.SegmentID
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierValue LIKE '%10.5962%'
		INNER JOIN dbo.Identifier id ON ii.IdentifierID = id.IdentifierID AND id.IdentifierName = 'DOI'
WHERE	d.IndexEntity = 'segment'
ORDER BY AuditDate,
		AuditID

END

GO
