CREATE PROCEDURE dbo.ItemSelectOAIDetail

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	i.ItemID,
		i.PrimaryTitleID,
		i.Volume,
		i.LanguageCode,
		i.[Year],
		i.LicenseUrl,
		i.Rights,
		i.CopyrightStatus,
		c.FirstPageID AS ThumbnailPageID
FROM	dbo.Item i 
		INNER JOIN dbo.SearchCatalog c ON i.ItemID = c.ItemID
WHERE	i.ItemID = @ItemID
