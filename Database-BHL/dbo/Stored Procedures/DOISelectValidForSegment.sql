CREATE PROCEDURE [dbo].[DOISelectValidForSegment]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.DOIID,
		d.DOIEntityTypeID,
		d.EntityID,
		d.DOIStatusID,
		d.DOIBatchID,
		d.DOIName,
		d.StatusDate,
		d.StatusMessage,
		d.IsValid,
		d.CreationDate,
		d.LastModifiedDate,
		d.CreationUserID,
		d.LastModifiedUserID
FROM	dbo.DOI d INNER JOIN dbo.DOIEntityType t
			ON d.DOIEntityTypeID = t.DOIEntityTypeID
			AND t.DOIEntityTypeName = 'Segment'
WHERE	d.EntityID = @SegmentID
AND		d.IsValid = 1

END
