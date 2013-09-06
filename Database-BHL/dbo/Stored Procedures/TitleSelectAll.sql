
CREATE PROCEDURE [dbo].[TitleSelectAll]
@IsPublished	bit = null
AS 

SET NOCOUNT ON

SELECT 

	Title.[TitleID],
	Title.[FullTitle]
FROM [dbo].[Title]
WHERE Title.PublishReady = ISNULL(@IsPublished,Title.PublishReady)
ORDER BY SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
