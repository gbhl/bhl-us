
CREATE PROCEDURE [dbo].[ApiAuthorSelectBySegmentID]

@SegmentID int

AS 

SET NOCOUNT ON

SELECT	a.AuthorID ,
		FullName ,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		FullerForm,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates
FROM	dbo. Author a INNER JOIN dbo. SegmentAuthor sa
			ON a.AuthorID = sa.AuthorID
		INNER JOIN dbo.AuthorName n
			ON a.AuthorID = n.AuthorID
			AND n.IsPreferredName = 1
		INNER JOIN dbo.Segment s
			ON sa.SegmentID = s.SegmentID
			AND s.SegmentStatusID IN (10, 20)
WHERE	s.SegmentID = @SegmentID
AND		a.IsActive = 1
ORDER BY n.FullName

