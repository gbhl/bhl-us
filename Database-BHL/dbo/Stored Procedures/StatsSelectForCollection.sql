SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StatsSelectForCollection]

@CollectionID int

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

-- Get items for an item-based collection
INSERT	#Books
SELECT	it.TitleID, ic.ItemID
FROM	dbo.ItemCollection ic 
		INNER JOIN dbo.Collection c ON ic.CollectionID = c.CollectionID
		INNER JOIN dbo.Item i ON ic.ItemID = i.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	c.CollectionID = @CollectionID
AND		c.CanContainItems = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

-- Get items for a title-based collection
INSERT	#Books
SELECT DISTINCT tc.TitleID, i.ItemID
FROM	dbo.Collection c 
		INNER JOIN dbo.TitleCollection tc ON c.CollectionID = tc.CollectionID
		INNER JOIN dbo.Title t ON tc.TitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
WHERE	c.CollectionID = @CollectionID
AND		c.CanContainTitles = 1
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40

-- Get Title and Item counts
SELECT @TitleCount = COUNT(DISTINCT TitleID) FROM #Books
SELECT @VolumeCount = COUNT(DISTINCT ItemID) FROM #Books

-- Get Page count
SELECT	@PageCount = COUNT(p.PageID) 
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN (SELECT DISTINCT ItemID FROM #Books) b ON ip.ItemID = b.ItemID 
WHERE	p.Active=1

-- Return final result set
SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@VolumeCount, 0) AS VolumeCount,
		ISNULL(@PageCount, 0) AS [PageCount]
END


GO
