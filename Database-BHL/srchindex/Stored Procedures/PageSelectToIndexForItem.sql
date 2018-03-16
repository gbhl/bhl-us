CREATE PROCEDURE [srchindex].[PageSelectToIndexForItem]

@ItemID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	p.PageID,
		p.ItemID,
		p.SequenceOrder,
		v.OcrFolderShare,
		i.FileRootFolder,
		i.BarCode,
		p.FileNamePrefix,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS PageIndicators,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes
FROM	dbo.Page p WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	p.Active = 1
AND		p.ItemID = @ItemID
ORDER BY p.SequenceOrder

END
