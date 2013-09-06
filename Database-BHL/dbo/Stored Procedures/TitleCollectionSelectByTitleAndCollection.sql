

CREATE PROCEDURE dbo.TitleCollectionSelectByTitleAndCollection

@TitleID int,
@CollectionID int

AS

BEGIN

SET NOCOUNT ON

SELECT	TitleCollectionID,
		TitleID,
		CollectionID,
		CreationDate
FROM	dbo.TitleCollection 
WHERE	TitleID = @TitleID
AND		CollectionID = @CollectionID

END

