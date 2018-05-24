CREATE PROCEDURE [audit].[AuditBasicSelectForSearchIndexQueue]

@StartDate datetime = NULL,
@EndDate datetime = NULL

AS

BEGIN

SET NOCOUNT ON

/*
-- Decide how to handle UPDATEs in the following tables --
-- Consider omitting audit rows for these tables where Operation = 'U' --
BibliographicLevel
Collection
Institution
Keyword
Language
MaterialType
PageType
SegmentGenre
TitleAssociationType
*/

-- Get the data parameters, if none were supplied
IF (@StartDate IS NULL)
BEGIN
	SELECT @StartDate = MAX(LastAuditDate) FROM dbo.SearchIndexQueueLog
	IF (@StartDate IS NULL) SELECT TOP 1 @StartDate = AuditDate FROM audit.AuditBasic ORDER BY AuditBasicID
END

IF (@EndDate IS NULL) SET @EndDate = GETDATE()


-- Keyword
SELECT	AuditBasicID AS AuditID, Operation, EntityName, 'keyword' AS IndexEntity, EntityKey1 AS EntityID1, NULL AS EntityID2, AuditDate
INTO	#Raw
FROM	audit.AuditBasic 
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Keyword'

UNION

-- Author
SELECT	AuditBasicID, ab.Operation, EntityName, 'author', EntityKey1 AS AuthorID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Author'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'author', n.AuthorID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON ab.EntityKey1 = n.AuthorNameID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorName'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'author', i.AuthorID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorIdentifier i WITH (NOLOCK) ON ab.EntityKey1 = i.AuthorIdentifierID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorIdentifier'

UNION

-- Name
SELECT	AuditBasicID, ab.Operation, EntityName, 'nameresolved', ab.EntityKey1 AS NameResolvedID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.NameResolved'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'nameresolved', n.NameResolvedID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON ab.EntityKey1 = n.NameID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.Name'
AND		n.NameResolvedID IS NOT NULL
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'nameresolved', n.NameResolvedID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.NamePage np WITH (NOLOCK) ON ab.EntityKey1 = np.NamePageID
		INNER JOIN dbo.Name n WITH (NOLOCK) ON np.NameID = n.NameID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.NamePage'
AND		n.NameResolvedID IS NOT NULL

UNION

-- Item
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', ti.TitleID, ab.EntityKey1 AS ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ab.EntityKey1 = ti.ItemID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.Item'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ab.EntityKey1 = ii.ItemInstitutionID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ii.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.ItemInstitution'
AND		ItemStatusID = 40
AND		PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ab.EntityKey1 = ii.InstitutionCode
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ii.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Institution'
AND		ItemStatusID = 40
AND		PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ab.EntityKey1 = i.LanguageCode
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Language'
AND		ItemStatusID = 40
AND		PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemCollection c WITH (NOLOCK) ON ab.EntityKey1 = c.ItemCollectionID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON c.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.ItemCollection'
AND		ItemStatusID = 40
AND		PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ab.EntityKey1 = ti.TitleItemID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleItem'
AND		ItemStatusID = 40
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ab.EntityKey1 = t.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Title'
AND		i.ItemStatusID = 40
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ab.EntityKey1 = t.BibliographicLevelID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.BibliographicLevel'
AND		i.ItemStatusID = 40
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ab.EntityKey1 = t.MaterialTypeID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.MaterialType'
AND		i.ItemStatusID = 40
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON ab.EntityKey1 = ta.AuthorID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ta.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Author'
AND		Operation <> 'E'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON ab.EntityKey1 = n.AuthorNameID
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON n.AuthorID = ta.AuthorID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ta.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorName'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON ab.EntityKey1 = ta.TitleAuthorID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ta.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleAuthor'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleKeyword tk WITH (NOLOCK) ON ab.EntityKey1 = tk.KeywordID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tk.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Keyword'
AND		Operation <> 'E'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleKeyword tk WITH (NOLOCK) ON ab.EntityKey1 = tk.TitleKeywordID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tk.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleKeyword'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleCollection tc WITH (NOLOCK) ON ab.EntityKey1 = tc.CollectionID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tc.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Collection'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON ab.EntityKey1 = ic.CollectionID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ic.ItemID = i.ItemiD
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Collection'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleCollection tc WITH (NOLOCK) ON ab.EntityKey1 = tc.TitleCollectionID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tc.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleCollection'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAssociation ta WITH (NOLOCK) ON ab.EntityKey1 = ta.TitleAssociationID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ta.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleAssociation'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleAssociation ta WITH (NOLOCK) ON ab.EntityKey1 = ta.TitleAssociationTypeID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ta.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleAssociationType'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.TitleVariant tv WITH (NOLOCK) ON ab.EntityKey1 = tv.TitleVariantID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tv.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.TitleVariant'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Title_Identifier tid WITH (NOLOCK) ON ab.EntityKey1 = tid.TitleIdentifierID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON tid.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemiD
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Title_Identifier'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.[Page] p WITH (NOLOCK) ON ab.EntityKey1 = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Page'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.[Page] p WITH (NOLOCK) ON ab.EntityKey1 = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.IndicatedPage'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.[Page] p WITH (NOLOCK) ON ab.EntityKey1 = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Page_PageType'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'item', t.TitleID, i.ItemID, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Page_PageType ppt WITH (NOLOCK) ON ab.EntityKey1 = ppt.PageTypeID
		INNER JOIN dbo.[Page] p WITH (NOLOCK) ON ppt.PageID = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.PageType'
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

UNION

-- Segment
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', ab.EntityKey1 AS SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName LIKE 'dbo.Segment'
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentAuthor sa WITH (NOLOCK) ON ab.EntityKey1 = sa.AuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Author'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON ab.EntityKey1 = n.AuthorNameID
		INNER JOIN dbo.SegmentAuthor sa WITH (NOLOCK) ON n.AuthorID = sa.AuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.AuthorName'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentAuthor sa WITH (NOLOCK) ON ab.EntityKey1 = sa.SegmentAuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.SegmentAuthor'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentKeyword sk WITH (NOLOCK) ON ab.EntityKey1 = sk.KeywordID
		INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Keyword'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentKeyword sk WITH (NOLOCK) ON ab.EntityKey1 = sk.SegmentKeywordID
		INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.SegmentKeyword'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentIdentifier si WITH (NOLOCK) ON ab.EntityKey1 = si.SegmentIdentifierID
		INNER JOIN dbo.Segment s ON si.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.SegmentIdentifier'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentInstitution si WITH (NOLOCK) ON ab.EntityKey1 = si.SegmentInstitutionID
		INNER JOIN dbo.Segment s ON si.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.SegmentInstitution'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ab.EntityKey1 = s.SegmentGenreID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.SegmentGenre'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentInstitution si WITH (NOLOCK) ON ab.EntityKey1 = si.InstitutionCode
		INNER JOIN dbo.Segment s ON si.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Institution'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ab.EntityKey1 = s.LanguageCode
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Language'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentPage sp WITH (NOLOCK) ON ab.EntityKey1 = sp.PageID
		INNER JOIN dbo.Segment s ON sp.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Page'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentPage sp WITH (NOLOCK) ON ab.EntityKey1 = sp.PageID
		INNER JOIN dbo.Segment s ON sp.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.IndicatedPage'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.SegmentPage sp WITH (NOLOCK) ON ab.EntityKey1 = sp.PageID
		INNER JOIN dbo.Segment s ON sp.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.Page_PageType'
AND		s.SegmentStatusID IN (10, 20)
UNION
SELECT	AuditBasicID, ab.Operation, EntityName, 'segment', s.SegmentID, NULL, AuditDate
FROM	audit.AuditBasic ab WITH (NOLOCK)
		INNER JOIN dbo.Page_PageType ppt WITH (NOLOCK) ON ab.EntityKey1 = ppt.PageTypeID
		INNER JOIN dbo.SegmentPage sp WITH (NOLOCK) ON ppt.PageID = sp.PageID
		INNER JOIN dbo.Segment s ON sp.SegmentID = s.SegmentID
WHERE	(AuditDate > @StartDate AND AuditDate <= @EndDate)
AND		EntityName = 'dbo.PageType'
AND		s.SegmentStatusID IN (10, 20)


-- Get the audit date and ID of the last record being processed
DECLARE @LastAuditDate datetime
DECLARE @LastAuditID INT
SELECT @LastAuditDate = MAX(AuditDate), @LastAuditID = MAX(AuditID) FROM #Raw


-- ### Ignore some entries that identify records related to deletes of other records ###

-- Example:  If a TitleInstitution record is deleted, records with an Operation code of "E" will
-- be created for both the related Title and the related Institution.  For indexing purposes, we
-- only care about how the change affects the Title, not the Institution.  Therefore, we can ignore
-- records where Operation = "E" and EntityName = "dbo.Institution".

--DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Author'
--DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Keyword'
DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Collection'
DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Institution'
DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Identifier'
DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Language'
-- Apr 3, 2018: 'E' only relates to 'Page' for SegmentPage and NamePage (these
-- are not important).   If a change means that 'E' relates to 'Page' for 
-- Page_PageType deletes in the future, then we should NOT do the following.
DELETE #Raw WHERE Operation = 'E' AND EntityName = 'dbo.Page'

-- ### Get initial reduced list of entities to be updated in the search indexes ###
SELECT	MIN(AuditID) AS AuditID,
		CASE 
			WHEN Operation IN ('I', 'U', 'E') THEN 'put'
			WHEN Operation = 'D' AND EntityName <> 'dbo.' + IndexEntity THEN 'put'
			ELSE 'delete' -- Operation = 'd' AND EntityName = IndexEntity
		END AS Operation,
		IndexEntity, 
		EntityID1, 
		EntityID2,
		MIN(AuditDate) AS AuditDate
INTO	#Reduced
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


-- ### Flag inactive Items, Segments, Authors, and Names for deletion from the search indexes ###
UPDATE	#Reduced
SET		Operation = 'delete'
FROM	#Reduced r
		INNER JOIN dbo.Item i WITH (NOLOCK) ON r.EntityID2 = i.ItemID
WHERE	r.IndexEntity = 'item'
AND		i.ItemStatusID <> 40

-- Delete active items with no active titles
UPDATE	#Reduced
SET		Operation = 'delete'
WHERE	IndexEntity = 'item'
AND		EntityID2 IN (
			SELECT	r.EntityID2
			FROM	#Reduced r
					INNER JOIN dbo.Item i WITH (NOLOCK) ON r.EntityID2 = i.ItemID
					INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
					INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
			WHERE	r.IndexEntity = 'item'
			AND		i.ItemStatusID = 40
			GROUP BY r.EntityID2 HAVING SUM(CONVERT(smallint, t.PublishReady)) = 0
			)

UPDATE	#Reduced
SET		Operation = 'delete'
FROM	#Reduced r
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON r.EntityID1 = s.SegmentID
WHERE	r.IndexEntity = 'segment'
AND		s.SegmentStatusID NOT IN (10, 20)

UPDATE	#Reduced
SET		Operation = 'delete'
FROM	#Reduced r
		INNER JOIN dbo.Author a WITH (NOLOCK) ON r.EntityID1 = a.AuthorID
WHERE	r.IndexEntity = 'author'
AND		a.IsActive = 0

-- Delete (resolved)names with no active related name records
UPDATE	#Reduced
SET		Operation = 'delete'
WHERE	IndexEntity = 'nameresolved'
AND		EntityID1 IN (
			SELECT	r.EntityID1
			FROM	#Reduced r
					INNER JOIN dbo.Name n WITH (NOLOCK) ON r.EntityID1 = n.NameResolvedID
			WHERE	r.IndexEntity = 'nameresolved'
			GROUP BY r.EntityID1 HAVING SUM(n.IsActive) = 0
			)


-- ## Look for authors/keywords/names with no active related publications, and flag them for deletion ###
UPDATE	#Reduced
SET		Operation = 'delete'
FROM	#Reduced r
		LEFT JOIN dbo.TitleKeyword tk WITH (NOLOCK) ON r.EntityID1 = tk.KeywordID
		LEFT JOIN dbo.SegmentKeyword sk WITH (NOLOCK) ON r.EntityID1 = sk.KeywordID
WHERE	r.IndexEntity = 'keyword'
AND		tk.TitleKeywordID IS NULL
AND		sk.SegmentKeywordID IS NULL

UPDATE	#Reduced
SET		Operation = 'delete'
FROM	#Reduced r
		LEFT JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON r.EntityID1 = ta.AuthorID
		LEFT JOIN dbo.SegmentAuthor sa WITH (NOLOCK) ON r.EntityID1 = sa.AuthorID
WHERE	r.IndexEntity = 'author'
AND		ta.TitleAuthorID IS NULL
AND		sa.SegmentAuthorID IS NULL

UPDATE	#Reduced
SET		Operation = 'delete'
WHERE	IndexEntity = 'nameresolved'
AND		EntityID1 IN (
			SELECT	r.EntityID1
			FROM	#Reduced r
					INNER JOIN dbo.Name n WITH (NOLOCK) ON r.EntityID1 = n.NameResolvedID
					LEFT JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			WHERE	r.IndexEntity = 'nameresolved'
			GROUP BY r.EntityID1 HAVING SUM(CASE WHEN np.NamePageID IS NOT NULL THEN 1 ELSE 0 END) = 0
			)


-- ### Final result set ###

-- Order by Date so that entities are indexed in the order they were changed.

-- Don't want to index an author/keyword before at least one publication to 
-- which it is related is indexed.
SELECT	@LastAuditID AS LastAuditID,
		@LastAuditDate AS LastAuditDate,
		AuditID, 
		AuditDate,
		Operation,
		IndexEntity,
		ISNULL(CONVERT(NVARCHAR(20), EntityID1), '') + 
			CASE WHEN EntityID1 IS NOT NULL AND EntityID2 IS NOT NULL THEN '-' ELSE '' END +
			ISNULL(CONVERT(NVARCHAR(20), EntityID2), '') AS EntityID
FROM	#Reduced 
ORDER BY AuditDate,
		AuditID

END
