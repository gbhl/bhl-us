SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiSegmentSelectUnpublished]

AS

SET NOCOUNT ON

SELECT	SegmentID
FROM	dbo.vwSegment
WHERE	SegmentStatusID NOT IN (30, 40)
ORDER BY SegmentID


GO
