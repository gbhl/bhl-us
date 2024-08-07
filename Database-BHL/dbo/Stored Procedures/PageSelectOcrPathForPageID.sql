CREATE PROCEDURE [dbo].[PageSelectOcrPathForPageID]

@PageID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		COALESCE(b.BarCode, s.BarCode) AS BarCode, 
		p.FileNamePrefix
FROM	dbo.Item i 
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		INNER JOIN dbo.Vault v ON i.VaultID = v.VaultID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40

END

GO
