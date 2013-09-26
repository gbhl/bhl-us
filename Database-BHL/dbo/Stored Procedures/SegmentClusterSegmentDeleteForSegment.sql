CREATE PROCEDURE [dbo].[SegmentClusterSegmentDeleteForSegment]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentClusterID
INTO	#Clusters
FROM	dbo.SegmentClusterSegment
WHERE	SegmentID = @SegmentID

DELETE	dbo.SegmentClusterSegment
WHERE	SegmentClusterID IN (SELECT	SegmentClusterID
							FROM	#Clusters)

DELETE	dbo.SegmentCluster
WHERE	SegmentClusterID IN (SELECT	SegmentClusterID
							FROM	#Clusters)

END

GO


