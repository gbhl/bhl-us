SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSelectOcrPathForPageID]

@PageID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		b.BarCode, 
		p.FileNamePrefix
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON i.ItemID = ip.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40

END


GO
