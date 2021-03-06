SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[SegmentSelectIDs]

@StartID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID 
FROM	dbo.Segment s
		INNER JOIN dbo.Item I on s.ItemID = i.ItemID
WHERE	i.ItemStatusID IN (30, 40)
AND		s.SegmentID >= @StartID
ORDER BY s.SegmentID

END


GO
