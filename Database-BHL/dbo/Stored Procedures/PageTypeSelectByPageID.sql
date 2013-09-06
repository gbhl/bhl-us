CREATE PROCEDURE [dbo].[PageTypeSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT	pt.PageTypeID,
		pt.PageTypeName
FROM	dbo.Page_PageType ppt INNER JOIN dbo.PageType pt
			ON ppt.PageTypeID = pt.PageTypeID
WHERE	ppt.PageID = @PageID

