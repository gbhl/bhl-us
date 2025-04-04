SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemRelationshipSelectByItemID]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	r.RelationshipID,
		r.ParentID,
		r.ChildID,
		r.SequenceOrder,
		r.CreationDate,
		b.BookID,
		s.SegmentID,
		CAST(1 AS SMALLINT) AS IsParent,
		CAST(0 AS SMALLINT) AS IsChild,
		r.LastModifiedDate,
		r.CreationUserID,
		r.LastModifiedUserID
FROM	dbo.Item i
		INNER JOIN dbo.ItemRelationship r ON i.ItemID = r.ParentID
		LEFT JOIN dbo.Book b ON r.ChildID = b.ItemID
		LEFT JOIN dbo.Segment s ON r.ChildID = s.ItemID
WHERE	i.ItemID = @ItemID

UNION

SELECT	r.RelationshipID,
		r.ParentID,
		r.ChildID,
		r.SequenceOrder,
		r.CreationDate,
		b.BookID,
		s.SegmentID,
		CAST(0 AS SMALLINT) AS IsParent,
		CAST(1 AS SMALLINT) AS IsChild,
		r.LastModifiedDate,
		r.CreationUserID,
		r.LastModifiedUserID
FROM	dbo.Item i
		INNER JOIN dbo.ItemRelationship r ON i.ItemID = r.ChildID
		LEFT JOIN dbo.Book b ON r.ParentID = b.ItemID
		LEFT JOIN dbo.Segment s ON r.ParentID = s.ItemID
WHERE	i.ItemID = @ItemID

END


GO
