SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BHLItemCollectionInsertSeedCatalogItems]

AS

BEGIN

SET NOCOUNT ON

-- Get the ID for the "Seed & Nursery Catalogs" collection
DECLARE @CollectionID int
SELECT @CollectionID = CollectionID FROM dbo.BHLCollection WHERE CollectionURL = 'seedcatalogs'

-- Insert new items from IA's usda-nurseryandseedcatalog collection into BHL's Seed & Nursery Catalogs collection
INSERT	dbo.BHLItemCollection (ItemID, CollectionID)
SELECT	b.ItemID, @CollectionID
FROM	dbo.BHLBook b 
		LEFT JOIN dbo.BHLItemCollection ic ON b.ItemID = ic.ItemID AND ic.CollectionID = @CollectionID
WHERE	ic.CollectionID IS NULL
AND		b.Barcode IN (
			SELECT	i.IAIdentifier
			FROM	dbo.IASet s 
					INNER JOIN dbo.IAItemSet iset ON s.SetID = iset.SetID
					INNER JOIN dbo.IAItem i ON iset.ItemID = i.ItemID
			WHERE	SetSpecification = 'collection:usda-nurseryandseedcatalog'
			OR		SetSpecification = 'collection:seedcatalogs'
			)

END


GO
