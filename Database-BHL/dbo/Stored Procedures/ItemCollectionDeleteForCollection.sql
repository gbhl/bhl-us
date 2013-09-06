

CREATE PROCEDURE dbo.ItemCollectionDeleteForCollection

@CollectionID int

AS

BEGIN

SET NOCOUNT ON

DELETE	dbo.ItemCollection
WHERE	CollectionID = @CollectionID


END

