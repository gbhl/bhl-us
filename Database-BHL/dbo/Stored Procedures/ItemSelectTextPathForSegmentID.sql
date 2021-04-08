CREATE PROCEDURE dbo.ItemSelectTextPathForSegmentID

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		s.BarCode
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	s.SegmentID = @SegmentID
AND		i.ItemStatusID IN (30, 40)

END

GO
