CREATE PROCEDURE srchindex.SearchCatalogKeywordInsertUpdate

@KeywordID int,
@Keyword nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT SearchCatalogKeywordID FROM dbo.SearchCatalogKeyword WHERE KeywordID = @KeywordID)
BEGIN
	UPDATE	dbo.SearchCatalogKeyword
	SET		Keyword = @Keyword
	WHERE	KeywordID = @KeywordID
END
ELSE
BEGIN
	INSERT	dbo.SearchCatalogKeyword (KeywordID, Keyword)
	VALUES	(@KeywordID, @Keyword)
END

END
