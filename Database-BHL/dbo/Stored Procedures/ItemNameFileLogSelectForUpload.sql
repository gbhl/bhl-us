CREATE PROCEDURE [dbo].[ItemNameFileLogSelectForUpload]

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
WHERE	l.DoUpload = 1

END


