
CREATE PROCEDURE [dbo].[OAIIdentifierSelectSegments]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null

AS

BEGIN

SET NOCOUNT ON

SELECT	TOP(@MaxIdentifiers) SegmentID AS ID, 'part' AS SetSpec, LastModifiedDate
FROM	dbo.Segment
WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
AND		(SegmentID > @StartID)
AND		(SegmentStatusID IN (10, 20))
AND		(ItemID IS NOT NULL OR ISNULL(Url, '') <> '')
ORDER BY SegmentID 

END




