CREATE PROCEDURE [dbo].[ItemSelectWithoutPageNames]

AS 

SET NOCOUNT ON

DECLARE @ItemSourceIA int
SELECT @ItemSourceIA = ItemSourceID FROM dbo.ItemSource WHERE SourceName = 'Internet Archive'

SELECT DISTINCT
		i.ItemID,
		ISNULL(b.BarCode, s.BarCode) AS BarCode
FROM	dbo.Item i 
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID AND b.LastPageNameLookupDate IS NULL
		LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID AND s.LastPageNameLookupDate IS NULL
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID	-- Only items with local content (pages)
AND		i.ItemStatusID = 40
AND		i.ItemSourceID = @ItemSourceIA
AND		(b.BookID IS NOT NULL OR s.SegmentID IS NOT NULL)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectWithoutPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
