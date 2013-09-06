CREATE PROCEDURE dbo.ItemCollectionSelectByItemAndCollection

@ItemID int,
@CollectionID int

AS

BEGIN

SET NOCOUNT ON

SELECT	ItemCollectionID,
		ItemID,
		CollectionID,
		CreationDate
FROM	dbo.ItemCollection 
WHERE	ItemID = @ItemID
AND		CollectionID = @CollectionID

END

