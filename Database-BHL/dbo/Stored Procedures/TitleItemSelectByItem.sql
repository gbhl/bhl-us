CREATE PROCEDURE [dbo].[TitleItemSelectByItem]
@ItemID INT
AS
BEGIN

SET NOCOUNT ON;

SELECT	it.ItemTitleID,
		t.MARCBibID, 
		t.TitleID, 
		t.ShortTitle,
		b.BookID,
		b.ItemID,
		it.ItemSequence,
		it.IsPrimary,
		t.PublishReady
FROM    dbo.Title t 
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID 
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	b.BookID = @ItemID

END

GO
