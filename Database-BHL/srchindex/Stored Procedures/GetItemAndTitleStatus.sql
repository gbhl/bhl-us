CREATE PROCEDURE srchindex.GetItemAndTitleStatus

@TitleID int,
@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	x.PublishReady,
		CAST(CASE WHEN ItemStatusName = 'Published' THEN 1 ELSE 0 END AS BIT) AS IsPublished
FROM	(
		SELECT	t.PublishReady, i.ItemStatusID 
		FROM	dbo.Title t CROSS JOIN dbo.Item i 
		WHERE	t.TitleID = @TitleID
		AND		i.ItemID = @ItemID
		) x
		INNER JOIN dbo.ItemStatus s on x.ItemStatusID = s.ItemStatusID

END
