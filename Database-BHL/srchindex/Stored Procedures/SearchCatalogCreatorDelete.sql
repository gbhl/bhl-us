CREATE PROCEDURE srchindex.SearchCatalogCreatorDelete

@AuthorID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalogCreator
WHERE	CreatorID = @AuthorID

END
