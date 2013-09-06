
CREATE FUNCTION [dbo].[fnCOinSGetPageCountForItem] 
(
	@ItemID int
)
RETURNS int
AS 

BEGIN
	DECLARE @PageCount int
	SET @PageCount = 0

	SELECT	@PageCount = COUNT(p.PageID)
	FROM	dbo.Page p --LEFT JOIN dbo.Page_PageType ppt
				--ON p.PageID = ppt.PageID
			--LEFT JOIN dbo.PageType pt
				--ON ppt.PageTypeID = pt.PageTypeID
				--AND PageTypeName <> 'Cover'
	WHERE	p.ItemID = @ItemID

	RETURN COALESCE(@PageCount, 0)
END

