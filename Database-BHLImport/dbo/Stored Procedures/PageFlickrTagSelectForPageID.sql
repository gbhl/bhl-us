CREATE PROCEDURE dbo.PageFlickrTagSelectForPageID

@PageID int

AS

BEGIN

SET NOCOUNT OFF

SELECT	PageFlickrTagID,
		PageID,
		PhotoID,
		IsMachineTag,
		TagValue,
		FlickrAuthorID,
		FlickrAuthorName,
		IsActive,
		CreationDate,
		LastModifiedDate,
		DeleteDate
FROM	dbo.PageFlickrTag
WHERE	PageID = @PageID

END
