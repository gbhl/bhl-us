SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
SELECT	it.TitleID, i.ItemID
FROM	dbo.Item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	ii.InstitutionCode = @InstitutionCode
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		r.InstitutionRoleName IN ('Holding Institution', 'Rights Holder')

-- Get Title and Item counts
SELECT @TitleCount = COUNT(DISTINCT TitleID) FROM #Books
SELECT @VolumeCount = COUNT(DISTINCT ItemID) FROM #Books

-- Get Segment count
SELECT	@SegmentCount = COUNT(s.SegmentID) 
FROM	dbo.vwSegment s
		INNER JOIN dbo.ItemInstitution ii ON s.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	s.SegmentStatusID IN (30, 40) 
AND		ii.InstitutionCode = @InstitutionCode
AND		r.InstitutionRoleName IN ('Contributor', 'Rights Holder')

-- Get Page count
SELECT	@PageCount = COUNT(p.PageID) 
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN (SELECT DISTINCT ItemID FROM #Books) b ON ip.ItemID = b.ItemID 
WHERE	p.Active=1

-- Return final result set
SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@VolumeCount, 0) AS VolumeCount,
		ISNULL(@PageCount, 0) AS [PageCount],
		ISNULL(@SegmentCount, 0) AS SegmentCount
END


GO
