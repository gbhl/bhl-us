SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnCOinSGetPageCountForItem] 
(
	@BookID int
)
RETURNS int
AS 

BEGIN
	DECLARE @PageCount int
	SET @PageCount = 0

	SELECT	@PageCount = COUNT(p.PageID)
	FROM	dbo.Page p
			INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
			INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
	WHERE	b.BookID = @BookID

	RETURN COALESCE(@PageCount, 0)
END


GO
