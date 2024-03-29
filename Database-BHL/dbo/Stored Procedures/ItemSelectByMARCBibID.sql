SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectByMARCBibID]

@MARCBibID nvarchar(50)

AS 

SET NOCOUNT ON

SELECT	b.BookID AS ItemID,
		b.[BarCode],
		it.[ItemSequence],
		b.[Volume],
		b.[IsVirtual]
FROM	[dbo].[Item] i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN [dbo].[ItemTitle] it ON i.ItemID = it.ItemID
		INNER JOIN [dbo].[Title] t ON it.TitleID = t.TitleID
WHERE	t.MARCBibID = @MARCBibID
ORDER BY 
		it.ItemSequence

GO
