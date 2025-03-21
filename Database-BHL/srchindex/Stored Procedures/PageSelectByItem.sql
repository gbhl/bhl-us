SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[PageSelectByItem]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID
FROM	dbo.[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	b.BookID = @ItemID

END


GO
