CREATE PROCEDURE [dbo].[SegmentSelectWithoutSubmittedDOI]

@NumberToReturn int = 10

AS

BEGIN

SET NOCOUNT ON

DECLARE @NumToReturn int
SET @NumToReturn = @NumberToReturn

SELECT TOP (@NumToReturn)
		d.DOIID,
		d.DOIEntityTypeID,
		d.EntityID,
		d.DOIStatusID,
		d.DOIName
FROM	dbo.DOI d WITH (NOLOCK)
		INNER JOIN dbo.vwSegment s WITH (NOLOCK) ON d.EntityID = s.SegmentID AND d.DOIEntityTypeID = 40 -- Segment
--WHERE	s.SegmentStatusID = 40 -- Published
WHERE	d.DOIStatusID = 30 -- Queued

END

GO
