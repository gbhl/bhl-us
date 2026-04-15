CREATE PROCEDURE dbo.IAItemSelectProductionIDByIAIdentifier

@IAIdentifier nvarchar(200)

AS

BEGIN

SET NOCOUNT ON 

SELECT	'item' AS ItemType, BookID AS ItemID, Barcode
FROM	dbo.BHLBook
WHERE	Barcode = @IAIdentifier
UNION
SELECT	'segment', SegmentID AS ItemID, Barcode
FROM	dbo.BHLSegment
WHERE	Barcode = @IAIdentifier

END

GO
