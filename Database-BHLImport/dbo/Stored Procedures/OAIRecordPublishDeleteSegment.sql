SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OAIRecordPublishDeleteSegment]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

UPDATE	dbo.BHLItem
SET		ItemStatusID = 5	-- Removed
FROM	dbo.BHLSegment s 
		INNER JOIN dbo.OAIRecord o ON s.SegmentID = o.ProductionSegmentID
		INNER JOIN dbo.BHLItem i ON s.ItemID = i.ItemID
WHERE	o.OAIRecordID = @OAIRecordID

END


GO
