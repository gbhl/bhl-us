
CREATE PROCEDURE dbo.CollectionSelectByNameAndAllowedContent

@CollectionName nvarchar(50),
@CanContainTitles smallint,
@CanContainItems smallint

AS

BEGIN

SELECT	CollectionID,
		CollectionName
FROM	dbo.[Collection]
WHERE	CollectionName = @CollectionName
AND		CanContainTitles = @CanContainTitles
AND		CanContainItems = @CanContainItems

END

