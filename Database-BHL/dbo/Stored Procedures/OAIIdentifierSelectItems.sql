
CREATE PROCEDURE [dbo].[OAIIdentifierSelectItems]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null

AS

BEGIN

SET NOCOUNT ON

SELECT	TOP(@MaxIdentifiers) ItemID AS ID, 'item' AS SetSpec, LastModifiedDate
FROM	dbo.Item
WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
AND		(ItemID > @StartID)
AND		(ItemStatusID = 40)
ORDER BY ItemID

END

