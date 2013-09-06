CREATE PROCEDURE dbo.PageSelectOcrPathForPageID

@PageID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		i.BarCode, 
		p.FileNamePrefix
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40

END
