
CREATE PROCEDURE [dbo].[TitleItemSelectByTitle]
@TitleID INT
AS
BEGIN

SET NOCOUNT ON

SELECT	ti.TitleItemID,
		ti.ItemSequence,
		ti.TitleID,
		ti.ItemID,
		i.BarCode,
		i.Volume,
		i.ItemStatusID,
		i.PrimaryTitleID
FROM    dbo.TitleItem ti INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
WHERE	ti.TitleID = @TitleID
ORDER BY
		ti.ItemSequence

END
