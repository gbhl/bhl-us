
CREATE PROCEDURE dbo.ItemCollectionInsertItemsForCollection

@CollectionID int

AS

BEGIN

DECLARE @InstitutionCode nvarchar(10)
DECLARE @LanguageCode nvarchar(10)

-- Get the institution and language for this collection
SELECT	@InstitutionCode = InstitutionCode,
		@LanguageCode = LanguageCode
FROM	dbo.Collection
WHERE	CanContainItems = 1
AND		CollectionID = @CollectionID

-- Only add items if an institution or language was specified
IF (@InstitutionCode IS NOT NULL OR @LanguageCode IS NOT NULL)
BEGIN
	-- Only add items not already in the collection
	INSERT	dbo.ItemCollection (ItemID, CollectionID)
	SELECT	i.ItemID, @CollectionID
	FROM	dbo.Item i LEFT JOIN dbo.ItemCollection ic
				ON i.ItemID = ic.ItemID
				AND ic.CollectionID = @CollectionID
	WHERE	(i.InstitutionCode = @InstitutionCode OR @InstitutionCode IS NULL)
	AND		(i.LanguageCode = @LanguageCode OR @LanguageCode IS NULL)
	AND		ic.ItemCollectionID IS NULL
END

END

