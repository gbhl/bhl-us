CREATE PROCEDURE [dbo].[ItemCountByCollection]
	@CollectionID int
AS

BEGIN

SET NOCOUNT ON

SELECT	COUNT(*)
FROM	dbo.Item i INNER JOIN dbo.ItemCollection ic
			ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Title t
			ON i.PrimaryTitleID = t.TitleID
WHERE	ic.CollectionID = @CollectionID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

END

