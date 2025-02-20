SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleItemSelectByTitle]
@TitleID INT
AS
BEGIN

SET NOCOUNT ON

SELECT	it.ItemTitleID,
		it.ItemSequence,
		it.TitleID,
		it.IsPrimary,
		b.BookID,
		b.ItemID,
		b.BarCode,
		b.Volume,
		i.ItemStatusID,
		pt.TitleID AS PrimaryTitleID
FROM    dbo.ItemTitle it 
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	it.TitleID = @TitleID
ORDER BY
		it.ItemSequence

END


GO
