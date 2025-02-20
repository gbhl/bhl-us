SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
FROM	dbo.Author a 
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.ItemAuthor ia ON a.AuthorID = ia.AuthorID
		INNER JOIN dbo.Item i ON ia.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID 
WHERE	s.SegmentID = @SegmentID
AND		a.IsActive = 1
AND		i.ItemStatusID IN (30, 40)
ORDER BY ia.SequenceOrder, n.FullName


GO
