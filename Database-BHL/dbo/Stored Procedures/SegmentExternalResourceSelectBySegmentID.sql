CREATE PROCEDURE dbo.SegmentExternalResourceSelectBySegmentID

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	r.SegmentExternalResourceID,
		r.SegmentID,
		r.ExternalResourceTypeID,
		rt.ExternalResourceTypeLabel,
		r.UrlText,
		r.Url,
		r.SequenceOrder,
		r.CreationDate,
		r.LastModifiedDate,
		r.CreationUserID,
		r.LastModifiedUserID
FROM	SegmentExternalResource r
		INNER JOIN ExternalResourceType rt ON r.ExternalResourceTypeID = rt.ExternalResourceTypeID
WHERE	r.SegmentID = @SegmentID
ORDER BY
		r.SequenceOrder

END

GO
