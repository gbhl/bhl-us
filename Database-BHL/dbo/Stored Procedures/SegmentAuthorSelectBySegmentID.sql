
CREATE PROCEDURE [dbo].[SegmentAuthorSelectBySegmentID]

@SegmentID int

AS

BEGIN

SELECT	sa.SegmentAuthorID,
		sa.SegmentID,
		sa.AuthorID,
		n.FullName,
		a.StartDate,
		a.EndDate,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		n.FullerForm,
		sa.SequenceOrder,
		sa.CreationDate,
		sa.LastModifiedDate,
		sa.CreationUserID,
		sa.LastModifiedUserID
FROM	dbo.SegmentAuthor sa
		INNER JOIN dbo.Author a ON a.AuthorID = sa.AuthorID
		INNER JOIN dbo.AuthorName n ON A.AuthorID = n.AuthorID
WHERE	sa.SegmentID = @SegmentID
AND		n.IsPreferredName = 1
ORDER BY
		sa.SequenceOrder

END



