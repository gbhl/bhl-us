CREATE PROCEDURE dbo.ExportIAIdentifiers

AS

BEGIN

SET NOCOUNT ON

-- Export the list of barcodes (identifiers) for all
-- active items contributed via Internet Archive
SELECT	BarCode
FROM	dbo.Item
WHERE	ItemSourceID = 1	-- Internet Archive
AND		ItemStatusID = 40	-- Active
ORDER BY
		BarCode
		
END

