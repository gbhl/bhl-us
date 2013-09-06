CREATE PROCEDURE [dbo].[ItemSelectByMARCBibID]
@MARCBibID nvarchar(50)
AS 

SET NOCOUNT ON

SELECT	i.[ItemID],
		i.[BarCode],
		ti.[ItemSequence],
		i.[Volume]
FROM	[dbo].[Item] i INNER JOIN [dbo].[TitleItem] ti
			ON i.ItemID = ti.ItemID
		INNER JOIN [dbo].[Title] t
			ON ti.TitleID = t.TitleID
WHERE	t.MARCBibID = @MARCBibID
ORDER BY 
		ti.ItemSequence

