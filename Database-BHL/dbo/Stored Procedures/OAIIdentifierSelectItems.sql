SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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

SELECT DISTINCT	TOP(@MaxIdentifiers) 
		b.BookID AS ID, 
		'item' AS SetSpec, 
		CASE WHEN i.LastModifiedDate > b.LastModifiedDate THEN i.LastModifiedDate ELSE b.LastModifiedDate END AS LastModifiedDate
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
WHERE	(b.LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(b.LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
AND		b.BookID > @StartID
AND		i.ItemStatusID = 40
AND		((c.HasLocalContent = 1 AND @IncludeLocalContent = 1)
OR		(c.HasExternalContent = 1 AND @IncludeExternalContent = 1))
ORDER BY b.BookID

END

GO
