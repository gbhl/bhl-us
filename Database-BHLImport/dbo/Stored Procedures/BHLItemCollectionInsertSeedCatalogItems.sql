CREATE PROCEDURE dbo.BHLItemCollectionInsertSeedCatalogItems

AS

BEGIN

SET NOCOUNT ON

-- Get the ID for the "Seed & Nursery Catalogs" collection
DECLARE @CollectionID int
SELECT @CollectionID = CollectionID FROM dbo.BHLCollection WHERE CollectionURL = 'seedcatalogs'

-- Insert new items from IA's usda-nurseryandseedcatalog collection into BHL's Seed & Nursery Catalogs collection
INSERT	dbo.BHLItemCollection (ItemID, CollectionID)
SELECT	i.ItemID, @CollectionID
FROM	dbo.BHLItem i 
		LEFT JOIN dbo.BHLItemCollection ic ON i.ItemID = ic.ItemID AND ic.CollectionID = @CollectionID
WHERE	ic.CollectionID IS NULL
AND		i.Barcode IN (
			SELECT	i.IAIdentifier
			FROM	dbo.IASet s 
					INNER JOIN dbo.IAItemSet iset ON s.SetID = iset.SetID
					INNER JOIN dbo.IAItem i ON iset.ItemID = i.ItemID
			WHERE	SetSpecification = 'collection:usda-nurseryandseedcatalog'
			OR		SetSpecification = 'collection:seedcatalogs'
			)

END
