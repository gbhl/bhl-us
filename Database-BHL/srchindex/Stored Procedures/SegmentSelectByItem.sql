SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[SegmentSelectByItem]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentID
FROM	dbo.Segment s
		INNER JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		INNER JOIN dbo.Book b ON r.ParentID = b.ItemID
WHERE	b.BookID = @ItemID

END


GO
