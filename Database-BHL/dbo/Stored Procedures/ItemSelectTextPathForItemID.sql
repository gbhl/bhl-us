SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectTextPathForItemID]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.OcrFolderShare, 
		i.FileRootFolder,
		b.BarCode
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	b.BookID = @ItemID
AND		i.ItemStatusID = 40

END


GO
