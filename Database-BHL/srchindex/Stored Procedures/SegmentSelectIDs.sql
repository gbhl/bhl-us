CREATE PROCEDURE [srchindex].[SegmentSelectIDs]

@StartID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID 
FROM	dbo.Segment s
WHERE	SegmentStatusID IN(10, 20)
AND		s.SegmentID >= @StartID
ORDER BY s.SegmentID

END
