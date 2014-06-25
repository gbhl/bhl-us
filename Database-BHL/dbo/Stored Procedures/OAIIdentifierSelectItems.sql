
CREATE PROCEDURE [dbo].[OAIIdentifierSelectItems]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null,
@IncludeLocalContent smallint = 1,
@IncludeExternalContent smallint = 0

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT	TOP(@MaxIdentifiers) i.ItemID AS ID, 'item' AS SetSpec, i.LastModifiedDate
FROM	dbo.Item i INNER JOIN dbo.SearchCatalog c ON i.ItemID = c.ItemID
WHERE	(i.LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(i.LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
AND		i.ItemID > @StartID
AND		i.ItemStatusID = 40
AND		((c.HasLocalContent = 1 AND @IncludeLocalContent = 1)
OR		(c.HasExternalContent = 1 AND @IncludeExternalContent = 1))
ORDER BY i.ItemID

END
