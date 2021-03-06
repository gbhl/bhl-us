SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemNameFileLogSelectForCreate]

AS

BEGIN

SET NOCOUNT ON

SELECT	l.LogID,
		b.BookID AS ItemID,
		v.OCRFolderShare,
		i.FileRootFolder,
		b.BarCode
FROM	dbo.ItemNameFileLog l 
		INNER JOIN dbo.Item i ON l.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.Vault v ON i.VaultID = v.VaultID
WHERE	l.DoCreate = 1

END


GO
