CREATE PROCEDURE [dbo].[ItemSelectFilenamesByItemID]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID,
		ISNULL(b.BarCode, s.BarCode) AS BarCode,
		dbo.fnGetTextFilenameForItem(i.ItemID) AS TextFilename,
		dbo.fnGetPDFFilenameForItem(i.ItemID) AS PdfFilename,
		dbo.fnGetImagesFilenameForItem(i.ItemID) AS ImagesFilename,
		dbo.fnGetDjvuFilenameForItem(i.ItemID) AS DjvuFilename,
		dbo.fnGetScandataFilenameForItem(i.ItemID) AS ScandataFilename
FROM	dbo.Item i
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	i.ItemID = @ItemID

END 

GO
