CREATE PROCEDURE [dbo].[StatsSelectForInstitution]

@InstitutionCode nvarchar(10)

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleCount int,
		@VolumeCount int,
		@PageCount int,
		@SegmentCount int

CREATE TABLE #Books (
	TitleID int NOT NULL, 
	ItemID int NOT NULL
)

-- Get items for an institution
INSERT	#Books
SELECT	ti.TitleID, i.ItemID
FROM	dbo.Item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
WHERE	ii.InstitutionCode = @InstitutionCode
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		r.InstitutionRoleName IN ('Contributor', 'Rights Holder')

-- Get Title and Item counts
SELECT @TitleCount = COUNT(DISTINCT TitleID) FROM #Books
SELECT @VolumeCount = COUNT(DISTINCT ItemID) FROM #Books

-- Get Segment count
SELECT	@SegmentCount = COUNT(s.SegmentID) 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentInstitution si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	s.SegmentStatusID IN (10, 20) 
AND		si.InstitutionCode = @InstitutionCode
AND		r.InstitutionRoleName IN ('Contributor', 'Rights Holder')

-- Get Page count
SELECT	@PageCount = COUNT(p.PageID) 
FROM	Page p INNER JOIN (SELECT DISTINCT ItemID FROM #Books) b ON p.ItemID = b.ItemID 
WHERE	p.Active=1

-- Return final result set
SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@VolumeCount, 0) AS VolumeCount,
		ISNULL(@PageCount, 0) AS [PageCount],
		ISNULL(@SegmentCount, 0) AS SegmentCount
END
