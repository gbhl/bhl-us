CREATE PROCEDURE dbo.ItemSelectFilenamesBySegmentID

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID AS ItemID,
		s.BarCode,
		dbo.fnGetTextFilenameForItem(i.ItemID) AS TextFilename,
		dbo.fnGetPDFFilenameForItem(i.ItemID) AS PdfFilename,
		dbo.fnGetImagesFilenameForItem(i.ItemID) AS ImagesFilename,
		dbo.fnGetDjvuFilenameForItem(i.ItemID) AS DjvuFilename,
		dbo.fnGetScandataFilenameForItem(i.ItemID) AS ScandataFilename
FROM	dbo.Item i
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	s.SegmentID = @SegmentID

END 

GO
