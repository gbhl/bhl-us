CREATE PROCEDURE dbo.DOIInsertEligibleEntities

AS

/*
Insert into the DOI table entities (such as Titles and Segments) that are eligible
for DOI assignment.
*/

BEGIN

SET NOCOUNT ON

DECLARE @MaxDate DATETIME
SELECT @MaxDate = GETDATE() - 14 -- days

-- Year for sliding copyright window
DECLARE @YearLimit int
SELECT @YearLimit = DATEPART(year, GETDATE()) - 95

DECLARE @DOIStatusQueued int
SELECT @DOIStatusQueued = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

DECLARE @DOIEntityTypeTitle int
SELECT @DOIEntityTypeTitle = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'

INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID)
SELECT	DISTINCT
		@DOIEntityTypeTitle AS DOIEntityTypeID,
		t.TitleID AS EntityID,
		@DOIStatusQueued AS DOIStatusID
FROM	dbo.Title t WITH (NOLOCK)
		LEFT JOIN dbo.DOI d WITH (NOLOCK) ON t.TitleID = d.EntityID AND d.DOIEntityTypeID = @DOIEntityTypeTitle
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON it.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		ISNULL(t.StartYear, 9999) < @YearLimit -- Only out-of-copyright titles
AND		t.CreationDate <= @MaxDate -- Only select titles older than specified # of days
AND		c.HasLocalContent = 1 -- Make sure that Page records exist (no items without scans)
AND		t.BibliographicLevelID IN (1, 4) -- Monographic component part, Monograph/Item
AND		t.FullTitle NOT LIKE '%Supplementary material in Charles Darwin''s copy%'
AND		d.DOIID IS NULL

/*
DECLARE @ISSNID int
SELECT @ISSNID = IdentifierID FROM dbo.Identifier WITH (NOLOCK) WHERE IdentifierName = 'ISSN'

DECLARE @TreatmentID int
SELECT @TreatmentID = SegmentGenreID FROM dbo.SegmentGenre WITH (NOLOCK) WHERE GenreName = 'Treatment'

DECLARE @DOIEntityTypeSegment int
SELECT @DOIEntityTypeSegment = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID)
SELECT	DISTINCT
		@DOIEntityTypeSegment AS DOIEntityTypeID,
		s.SegmentID AS EntityID,
		@DOIStatusQueued AS DOIStatusID
FROM	dbo.vwSegment s WITH (NOLOCK) 
		INNER JOIN dbo.ItemInstitution sinst WITH (NOLOCK) ON s.ItemID = sinst.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON sinst.InstitutionRoleID = r.InstitutionRoleID AND r.InstitutionRoleName = 'Contributor'
		LEFT JOIN dbo.DOI d WITH (NOLOCK) ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = @DOIEntityTypeSegment
		INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON s.BookID = b.BookID
		INNER JOIN dbo.vwItemPrimaryTitle pt WITH (NOLOCK) ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title_Identifier ti WITH (NOLOCK) ON pt.TitleID = ti.TitleID AND ti.IdentifierID = @ISSNID
		LEFT JOIN dbo.DOI td ON pt.TitleID = td.EntityID AND td.IsValid = 1 AND td.DOIEntityTypeID = @DOIEntityTypeTitle
WHERE	s.SegmentStatusID = 40 -- Published
AND		s.CreationDate <= @MaxDate -- Only select segments older than specified # of days
AND		c.HasLocalContent = 1 -- Make sure that the segment content is held within BHL
AND		sinst.InstitutionCode <> 'USER'  -- No user-contributed segments
AND		(ti.IdentifierValue IS NOT NULL OR td.DOIName IS NOT NULL) -- Make sure that an ISSN or DOI exists for the segment container
AND		s.SegmentGenreID <> @TreatmentID	-- Any segment types except treatments
AND		d.DOIID IS NULL
*/

END

GO
