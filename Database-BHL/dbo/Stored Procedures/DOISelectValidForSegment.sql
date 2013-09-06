CREATE PROCEDURE [dbo].[DOISelectValidForSegment]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.DOIID,
		d.DOIBatchID,
		d.DOIName
FROM	dbo.DOI d INNER JOIN dbo.DOIEntityType t
			ON d.DOIEntityTypeID = t.DOIEntityTypeID
			AND t.DOIEntityTypeName = 'Segment'
WHERE	d.EntityID = @SegmentID
AND		d.IsValid = 1

END


