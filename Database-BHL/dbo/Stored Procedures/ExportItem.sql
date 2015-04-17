CREATE PROCEDURE dbo.ExportItem

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		i.ItemID, 
		i.PrimaryTitleID AS TitleID, 
		i.ThumbnailPageID, 
		i.BarCode, 
		i.MARCItemID, 
		i.CallNumber, 
		i.Volume AS VolumeInfo,
		'http://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL, 
		i.IdentifierBib AS LocalID, 
		i.Year, 
		ins.InstitutionName, 
		i.ZQuery, 
		CONVERT(nvarchar(16), i.CreationDate, 120) AS CreationDate
FROM	dbo.Item i WITH (NOLOCK)
		LEFT JOIN dbo.Institution ins WITH (NOLOCK) ON i.InstitutionCode = ins.InstitutionCode
WHERE	i.ItemStatusID = 40

END
