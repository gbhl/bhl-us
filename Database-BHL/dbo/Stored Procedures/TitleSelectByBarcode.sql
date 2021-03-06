SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleSelectByBarcode]
(@Barcode	varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	t.MARCBibID, t.TitleID, t.RareBooks, b.BookID AS ItemID, b.BarCode, ip.PageID, p.FileNamePrefix, p.PageDescription, 
            ip.SequenceOrder, CAST(NULL AS INT) AS PDFSize, t.ShortTitle, b.Volume
	FROM	dbo.Title t
			INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
			INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
			INNER JOIN dbo.ItemPage ip ON b.ItemID = ip.ItemID
			INNER JOIN dbo.Page p ON ip.PageID = p.PageID
	WHERE	ip.SequenceOrder = 1
	AND		b.BarCode = @Barcode
END


GO
