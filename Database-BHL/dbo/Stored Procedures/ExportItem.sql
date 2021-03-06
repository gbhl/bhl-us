SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExportItem]

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		b.BookID AS ItemID, 
		pt.TitleID, 
		c.FirstPageID AS ThumbnailPageID, 
		b.BarCode, 
		b.MARCItemID, 
		b.CallNumber, 
		b.Volume AS VolumeInfo,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL, 
		b.IdentifierBib AS LocalID, 
		b.StartYear AS Year, 
		c.ItemContributors AS InstitutionName, 
		b.ZQuery,
		c.HasLocalContent,
		c.HasExternalContent,
		CONVERT(nvarchar(16), i.CreationDate, 120) AS CreationDate
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON b.BookID = c.ItemID
WHERE	i.ItemStatusID = 40

END


GO
