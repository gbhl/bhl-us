CREATE PROCEDURE [dbo].[TitleItemSelectByItem]
@ItemID INT
AS
BEGIN

SET NOCOUNT ON;

SELECT	ti.TitleItemID,
		t.MARCBibID, 
		t.TitleID, 
		t.ShortTitle,
		i.ItemID,
		ti.ItemSequence
FROM    Title t INNER JOIN TitleItem ti
			ON t.TitleID = ti.TitleID 
		INNER JOIN Item i
			ON ti.ItemID = i.ItemID
WHERE	i.ItemID = @ItemID

END
