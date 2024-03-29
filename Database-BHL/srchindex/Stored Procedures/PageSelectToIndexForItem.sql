CREATE PROCEDURE [srchindex].[PageSelectToIndexForItem]

@ItemID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	p.PageID,
		b.ItemID,
		ip.SequenceOrder,
		v.OcrFolderShare,
		i.FileRootFolder,
		b.BarCode,
		p.FileNamePrefix,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS PageIndicators,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes
FROM	dbo.Page p WITH (NOLOCK)
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	p.Active = 1
AND		b.BookID = @ItemID
ORDER BY p.SequenceOrder

END

GO
