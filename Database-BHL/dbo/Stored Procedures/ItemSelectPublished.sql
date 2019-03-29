CREATE PROCEDURE [dbo].[ItemSelectPublished]

AS

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID,
		BarCode,
		i.Volume,
		HasLocalContent,
		HasExternalContent
FROM	dbo.Item i 
		INNER JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40

END

