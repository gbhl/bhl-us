CREATE PROCEDURE [dbo].[ApiSegmentIdentifierSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.IdentifierName,
		si.IdentifierValue
FROM	dbo.Segment s INNER JOIN dbo.SegmentIdentifier si
			ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Identifier i
			ON si.IdentifierID = i.IdentifierID
WHERE	s.SegmentStatusID IN (10, 20)
AND		s.SegmentID = @SegmentID
ORDER BY i.IdentifierName

END


