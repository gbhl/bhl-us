
CREATE FUNCTION [dbo].[fnCollectionStringForTitleAndItem]
(
	@TitleID int,
	@ItemID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @CollectionString nvarchar(MAX)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	SELECT	@CollectionString = COALESCE(@CollectionString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE ' | ' END) +  CollectionName,
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			SELECT	c.CollectionName
			FROM	dbo.TitleCollection tc INNER JOIN dbo.[Collection] c
						ON tc.CollectionID = c.CollectionID
			WHERE	tc.TitleID = @TitleID
			AND		c.Active = 1
			AND		c.CollectionTarget IN ('BHL', 'All')
			UNION
			SELECT	c.CollectionName
			FROM	dbo.ItemCollection ic INNER JOIN dbo.[Collection] c
						ON ic.CollectionID = c.CollectionID
			WHERE	ic.ItemID = @ItemID
			AND		c.Active = 1
			AND		c.CollectionTarget IN ('BHL', 'All')
			) x
	ORDER BY CollectionName			

	RETURN LTRIM(RTRIM(COALESCE(@CollectionString, '')))
END


