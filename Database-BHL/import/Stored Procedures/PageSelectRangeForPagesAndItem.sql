CREATE PROCEDURE import.PageSelectRangeForPagesAndItem

@StartPageID int,
@EndPageID int,
@ItemID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @StartItemID int
DECLARE @EndItemID int
DECLARE @StartSeq int
DECLARE @EndSeq int

SELECT	@StartItemID = ItemID,
		@StartSeq = SequenceOrder
FROM	dbo.[Page] WITH (NOLOCK)
WHERE	(ItemID = @ItemID OR @ItemID IS NULL)
AND		PageID = @StartPageID

SELECT	@EndItemID = ItemID,
		@EndSeq = SequenceOrder
FROM	dbo.[Page] WITH (NOLOCK)
WHERE	(ItemID = @ItemID OR @ItemID IS NULL)
AND		PageID = @EndPageID

SELECT	ItemID, PageID
FROM	dbo.[Page] WITH (NOLOCK)
WHERE	ItemID = @StartItemID
AND		ItemID = @EndItemID
AND		SequenceOrder BETWEEN @StartSeq AND @EndSeq

END
