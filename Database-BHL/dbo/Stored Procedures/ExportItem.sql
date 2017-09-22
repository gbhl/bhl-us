CREATE PROCEDURE [dbo].[ExportItem]

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		i.ItemID, 
		i.PrimaryTitleID AS TitleID, 
		c.FirstPageID AS ThumbnailPageID, 
		i.BarCode, 
		i.MARCItemID, 
		i.CallNumber, 
		i.Volume AS VolumeInfo,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL, 
		i.IdentifierBib AS LocalID, 
		i.Year, 
		c.ItemContributors AS InstitutionName, 
		i.ZQuery, 
		CONVERT(nvarchar(16), i.CreationDate, 120) AS CreationDate
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON i.ItemID = c.ItemID
WHERE	i.ItemStatusID = 40

END
