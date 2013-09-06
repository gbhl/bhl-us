CREATE PROCEDURE [dbo].[TitleSelectByBarcode]
(@Barcode	varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
	SELECT     Title.MARCBibID, Title.TitleID, Title.RareBooks, Item.ItemID, Item.BarCode, Page.PageID, Page.FileNamePrefix, Page.PageDescription, 
                      Page.SequenceOrder, Item.PDFSize, Title.ShortTitle, Item.Volume
	FROM         Title INNER JOIN
                      Item ON Title.TitleID = Item.PrimaryTitleID INNER JOIN
                      Page ON Item.ItemID = Page.ItemID
	WHERE     (Page.SequenceOrder = 1) AND (Item.BarCode = @Barcode)
END


