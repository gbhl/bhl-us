CREATE PROCEDURE dbo.Page_PageTypeDeleteAutoAssignedImages

@PageID int

AS

BEGIN

SET NOCOUNT ON

DELETE	dbo.Page_PageType
FROM	dbo.Page_PageType ppt 
		INNER JOIN dbo.PageType pt ON ppt.PageTypeID = pt.PageTypeID
		INNER JOIN dbo.Page p ON ppt.PageID = p.PageID
		INNER JOIN dbo.Item i ON p.ItemID = i.ItemID
WHERE	p.PageID = @PageID
AND		ppt.LastModifiedUserID IN (1, 50000, 50001)
AND		pt.PageTypeName IN ('Illustration', 'Map', 'Painting/Drawing/Diagram', 'Chart/Table', 'Photograph', 'Bookplate')

END
