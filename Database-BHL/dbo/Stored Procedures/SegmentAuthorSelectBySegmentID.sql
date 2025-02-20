SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentAuthorSelectBySegmentID]

@SegmentID int

AS

BEGIN

SELECT	ia.ItemAuthorID,
		s.SegmentID,
		ia.ItemID,
		ia.AuthorID,
		n.FullName,
		a.StartDate,
		a.EndDate,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		n.FullerForm,
		ia.SequenceOrder,
		ia.CreationDate,
		ia.LastModifiedDate,
		ia.CreationUserID,
		ia.LastModifiedUserID
FROM	dbo.Segment s
		INNER JOIN dbo.ItemAuthor ia ON s.itemID = ia.ItemID
		INNER JOIN dbo.Author a ON a.AuthorID = ia.AuthorID
		INNER JOIN dbo.AuthorName n ON A.AuthorID = n.AuthorID
WHERE	s.SegmentID = @SegmentID
AND		n.IsPreferredName = 1
ORDER BY
		ia.SequenceOrder

END


GO
