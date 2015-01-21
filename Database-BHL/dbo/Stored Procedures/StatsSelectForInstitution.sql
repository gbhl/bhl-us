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
		INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
WHERE	i.InstitutionCode = @InstitutionCode
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

-- Get Title and Item counts
SELECT @TitleCount = COUNT(DISTINCT TitleID) FROM #Books
SELECT @VolumeCount = COUNT(DISTINCT ItemID) FROM #Books

-- Get Segment count
SELECT	@SegmentCount = COUNT(SegmentID) 
FROM	dbo.Segment 
WHERE	SegmentStatusID IN (10, 20) 
AND		ContributorCode = @InstitutionCode

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

