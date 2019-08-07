CREATE PROCEDURE dbo.ItemSelectFilenames

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	ItemID,
		BarCode,
		dbo.fnGetTextFilenameForItem(ItemID) AS TextFilename,
		dbo.fnGetPDFFilenameForItem(ItemID) AS PdfFilename,
		dbo.fnGetImagesFilenameForItem(ItemID) AS ImagesFilename,
		dbo.fnGetDjvuFilenameForItem(ItemID) AS DjvuFilename,
		dbo.fnGetScandataFilenameForItem(ItemID) AS ScandataFilename
FROM	dbo.Item
WHERE	ItemID = @ItemID

END 
