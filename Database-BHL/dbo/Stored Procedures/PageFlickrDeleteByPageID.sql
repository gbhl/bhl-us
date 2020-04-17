CREATE PROCEDURE dbo.PageFlickrDeleteByPageID

@PageID INT

AS 

DELETE 
FROM	dbo.PageFlickr
WHERE	PageID = @PageID
