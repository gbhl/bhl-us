CREATE PROCEDURE [dbo].[OAIIdentifierSelectTitles]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null

AS

BEGIN

SET NOCOUNT ON

SELECT	TOP(@MaxIdentifiers) TitleID AS ID, 'title' AS SetSpec, LastModifiedDate
FROM	dbo.Title
WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
AND		(TitleID > @StartID)
AND		(PublishReady = 1)
ORDER BY TitleID

END
