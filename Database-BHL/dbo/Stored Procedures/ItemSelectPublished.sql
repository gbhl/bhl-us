

CREATE PROCEDURE dbo.ItemSelectPublished

AS

BEGIN

SELECT	ItemID,
		BarCode,
		Volume
FROM	dbo.Item i INNER JOIN dbo.Title t
			ON i.PrimaryTitleID = t.TitleID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40

END

