CREATE PROCEDURE dbo.ItemSelectTextPathForItemID

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		i.BarCode
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	i.ItemID = @ItemID
AND		i.ItemStatusID = 40

END
