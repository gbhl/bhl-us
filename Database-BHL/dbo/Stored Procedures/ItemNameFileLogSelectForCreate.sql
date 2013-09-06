CREATE PROCEDURE dbo.ItemNameFileLogSelectForCreate

AS

BEGIN

SET NOCOUNT ON

SELECT	l.LogID,
		l.ItemID,
		v.OCRFolderShare,
		i.FileRootFolder,
		i.BarCode
FROM	dbo.ItemNameFileLog l INNER JOIN dbo.Item i
			ON l.ItemID = i.ItemID
		INNER JOIN dbo.Vault v
			ON i.VaultID = v.VaultID
WHERE	l.DoCreate = 1

END

