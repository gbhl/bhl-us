SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentSelectPublished]

AS

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		c.HasLocalContent,
		c.HasExternalContent
FROM	dbo.vwSegment s 
		INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
WHERE	SegmentStatusID IN (30, 40)

END


GO
