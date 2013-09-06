CREATE PROCEDURE [dbo].[SegmentSelectByStatusID]

@SegmentStatusID int

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		s.SegmentID,
		s.ItemID,
		s.Title,
		s.Date,
		REPLACE(cat.Authors, '|', ' ') AS Authors,
		NULL AS SegmentClusterID,
		d.DOIName
INTO	#tmpSegment
FROM	dbo.Segment s
		INNER JOIN dbo.SearchCatalogSegment cat ON s.SegmentID = cat.SegmentID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40
WHERE	SegmentStatusID = @SegmentStatusID

UPDATE	#tmpSegment
SET		SegmentClusterID = scs.SegmentClusterID
FROM	#tmpSegment t INNER JOIN SegmentClusterSegment scs ON t.SegmentID = scs.SegmentID

SELECT	*
FROM	#tmpSegment


END
