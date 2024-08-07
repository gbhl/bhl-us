SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectFilenames]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	b.BookID AS ItemID,
		BarCode,
		dbo.fnGetTextFilenameForItem(i.ItemID) AS TextFilename,
		dbo.fnGetPDFFilenameForItem(i.ItemID) AS PdfFilename,
		dbo.fnGetImagesFilenameForItem(i.ItemID) AS ImagesFilename,
		dbo.fnGetDjvuFilenameForItem(i.ItemID) AS DjvuFilename,
		dbo.fnGetScandataFilenameForItem(i.ItemID) AS ScandataFilename
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	b.BookID = @ItemID

END 


GO
