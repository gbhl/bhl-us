SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiStatsSelect]

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleCount int,
		@ItemCount int,
		@PageCount int,
		@SegmentCount int,
		@BookType int,
		@SegmentType int

SELECT @BookType = ItemTypeID FROM dbo.ItemType WHERE ItemTypeLabel = 'Book'
SELECT @SegmentType = ItemTypeID FROM dbo.ItemType WHERE ItemTypeLabel = 'Segment'

-- Get basic Title/Item/Page/Segment stats
SELECT @TitleCount = count(*) FROM dbo.Title WITH (NOLOCK) WHERE Title.PublishReady = 1
SELECT @ItemCount = count(*) FROM dbo.Item WITH (NOLOCK) WHERE Item.ItemStatusID = 40 AND ItemTypeID = @BookType

SELECT	@PageCount = count(*) 
FROM	dbo.Page p WITH (NOLOCK) 
		INNER JOIN dbo.ItemPage ip  WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ip.ItemID = i.ItemID 
WHERE	p.Active=1 
AND		i.ItemStatusID = 40
AND		i.ItemTypeID = @BookType

SELECT @SegmentCount = count(*) FROM dbo.Item i WITH (NOLOCK) WHERE i.ItemStatusID IN (30, 40) AND ItemTypeID = @SegmentType

SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@ItemCount, 0) AS ItemCount,
		ISNULL(@PageCount, 0) AS [PageCount],
		ISNULL(@SegmentCount, 0) AS PartCount
END


GO
