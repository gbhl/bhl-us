CREATE PROCEDURE [dbo].[TitleCountByCollection]
	@CollectionID int
AS

BEGIN

SET NOCOUNT ON

SELECT	COUNT(*)
FROM	dbo.Title t INNER JOIN dbo.TitleCollection tc
			ON t.TitleID = tc.TitleID
WHERE	tc.CollectionID = @CollectionID
AND		t.PublishReady = 1

END



