CREATE PROCEDURE [dbo].[ItemNameFileLogSelectForCreate]

AS

BEGIN

SET NOCOUNT ON

SELECT	l.LogID,
		i.ItemID,
		v.OCRFolderShare,
		i.FileRootFolder,
		ISNULL(b.BarCode, s.BarCode) AS BarCode
FROM	dbo.ItemNameFileLog l 
		INNER JOIN dbo.Item i ON l.ItemID = i.ItemID
		INNER JOIN dbo.Vault v ON i.VaultID = v.VaultID
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	l.DoCreate = 1

END

GO
