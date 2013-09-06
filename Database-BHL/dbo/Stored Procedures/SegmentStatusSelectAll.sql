CREATE PROCEDURE SegmentStatusSelectAll

AS

BEGIN

SELECT	SegmentStatusID,
		StatusName,
		StatusDescription,
		CreationDate,
		LastModifiedDate
FROM	dbo.SegmentStatus
ORDER BY
		StatusName

END

