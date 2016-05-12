CREATE PROCEDURE [dbo].[ApiStatsSelect]

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleCount int,
		@ItemCount int,
		@PageCount int,
		@SegmentCount int

-- Get basic Title/Item/Page/Segment stats
SELECT @TitleCount = count(*) FROM dbo.Title WITH (NOLOCK) WHERE Title.PublishReady = 1
SELECT @ItemCount = count(*) FROM dbo.Item WITH (NOLOCK) WHERE Item.ItemStatusID = 40

SELECT	@PageCount = count(*) 
FROM	dbo.Page p WITH (NOLOCK) INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID 
WHERE	p.Active=1 
AND		i.ItemStatusID = 40

SELECT @SegmentCount = count(*) FROM dbo.Segment s WITH (NOLOCK) WHERE s.SegmentStatusID IN (10, 20)

SELECT	ISNULL(@TitleCount, 0) AS TitleCount,
		ISNULL(@ItemCount, 0) AS ItemCount,
		ISNULL(@PageCount, 0) AS [PageCount],
		ISNULL(@SegmentCount, 0) AS PartCount
END
