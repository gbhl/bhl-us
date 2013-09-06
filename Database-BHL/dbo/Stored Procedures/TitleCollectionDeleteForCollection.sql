CREATE PROCEDURE dbo.TitleCollectionDeleteForCollection

@CollectionID int

AS

BEGIN

SET NOCOUNT ON

DELETE	dbo.TitleCollection
WHERE	CollectionID = @CollectionID


END

