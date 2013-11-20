CREATE PROCEDURE dbo.OAIRecordPublishDeleteSegment

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

UPDATE	dbo.BHLSegment
SET		SegmentStatusID = 30	-- Removed
FROM	dbo.BHLSegment s INNER JOIN dbo.OAIRecord o ON s.SegmentID = o.ProductionSegmentID
WHERE	o.OAIRecordID = @OAIRecordID

END

GO


