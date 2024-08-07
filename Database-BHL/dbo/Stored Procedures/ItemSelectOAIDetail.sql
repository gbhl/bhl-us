SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectOAIDetail]

@BookID INT

AS 

SET NOCOUNT ON

SELECT	b.BookID,
		b.ItemID,
		pt.TitleID AS PrimaryTitleID,
		b.Volume,
		b.LanguageCode,
		b.[StartYear] AS [Year],
		b.LicenseUrl,
		b.Rights,
		b.CopyrightStatus,
		c.FirstPageID AS ThumbnailPageID
FROM	dbo.Item i 
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
WHERE	b.BookID = @BookID


GO
