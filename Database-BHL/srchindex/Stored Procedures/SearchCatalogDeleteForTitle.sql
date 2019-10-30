CREATE PROCEDURE srchindex.SearchCatalogDeleteForTitle

@TitleID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalog
WHERE	TitleID = @TitleID

END 
