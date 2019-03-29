CREATE PROCEDURE [dbo].[SegmentSelectPublished]

AS

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		c.HasLocalContent,
		c.HasExternalContent
FROM	dbo.Segment s 
		INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
WHERE	SegmentStatusID IN (10, 20)

END

