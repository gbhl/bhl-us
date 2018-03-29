CREATE PROCEDURE srchindex.SearchCatalogCreatorInsertUpdate

@AuthorID int,
@AuthorName nvarchar(2000)

AS

BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT SearchCatalogCreatorID FROM dbo.SearchCatalogCreator WHERE CreatorID = @AuthorID)
BEGIN
	UPDATE	dbo.SearchCatalogCreator
	SET		CreatorName = @AuthorName
	WHERE	CreatorID = @AuthorID
END
ELSE
BEGIN
	INSERT	dbo.SearchCatalogCreator (CreatorID, CreatorName)
	VALUES	(@AuthorID, @AuthorName)
END

END
