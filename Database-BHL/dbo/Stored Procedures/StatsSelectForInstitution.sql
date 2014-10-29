CREATE PROCEDURE [dbo].[StatsSelectForInstitution]

@InstitutionCode nvarchar(10)

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleCount int,
		@VolumeCount int,
		@PageCount int

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

-- Get Title and Item counts
SELECT @TitleCount = COUNT(DISTINCT TitleID) FROM #Books
SELECT @VolumeCount = COUNT(DISTINCT ItemID) FROM #Books

-- Get Page count
SELECT	@PageCount = COUNT(p.PageID) 
FROM	Page p INNER JOIN #Books b ON p.ItemID = b.ItemID 
WHERE	p.Active=1

-- Return final result set
SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@VolumeCount, 0) AS VolumeCount,
		ISNULL(@PageCount, 0) AS [PageCount]
END

