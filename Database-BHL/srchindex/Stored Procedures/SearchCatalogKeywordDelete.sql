CREATE PROCEDURE srchindex.SearchCatalogKeywordDelete

@KeywordID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalogKeyword
WHERE	KeywordID = @KeywordID

END
