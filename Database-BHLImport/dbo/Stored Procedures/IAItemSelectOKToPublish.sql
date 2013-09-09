CREATE PROCEDURE dbo.IAItemSelectOKToPublish

@ItemID int,
@Days int

AS
BEGIN

SET NOCOUNT ON

-- If the specified item has matching nonzero numbers of page and scandata records,
-- and was added to IA more than the specified number of days ago, then it is ok
-- to publish to production.
SELECT IAIdentifier
FROM (
      SELECT IAIdentifier,
            (SELECT COUNT(*) FROM IAPage WHERE ItemID = i.ItemID) AS NumPages,
            (SELECT COUNT(*) FROM IAScandata WHERE ItemID = i.ItemID) AS NumScandata,
            IAAddedDate,
			CreatedDate
      FROM IAItem i
      WHERE ItemID = @ItemID
      ) x
WHERE NumPages = NumScandata AND NumScandata > 0
AND DATEDIFF(d, ISNULL(IAAddedDate, CreatedDate), GETDATE()) > @Days

END
