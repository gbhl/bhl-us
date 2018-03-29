CREATE PROCEDURE srchindex.SearchCatalogDelete

@TitleID int,
@ItemID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalog
WHERE	TitleID = @TitleID
AND		ItemID = @ItemID

END
