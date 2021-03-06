SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [import].[PageSelectRangeForPagesAndItem]

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

SELECT	@StartItemID = b.BookID,
		@StartSeq = ip.SequenceOrder
FROM	dbo.[Page] p WITH (NOLOCK)
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON ip.IteMID = b.ItemID
WHERE	(b.BookID = @ItemID OR @ItemID IS NULL)
AND		p.PageID = @StartPageID

SELECT	@EndItemID = b.BookID,
		@EndSeq = ip.SequenceOrder
FROM	dbo.[Page] p WITH (NOLOCK)
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON ip.IteMID = b.ItemID
WHERE	(b.BookID = @ItemID OR @ItemID IS NULL)
AND		p.PageID = @EndPageID

SELECT	b.BookID AS ItemID, p.PageID
FROM	dbo.[Page] p WITH (NOLOCK)
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON ip.IteMID = b.ItemID
WHERE	b.BookID = @StartItemID
AND		b.BookID = @EndItemID
AND		ip.SequenceOrder BETWEEN @StartSeq AND @EndSeq

END


GO
