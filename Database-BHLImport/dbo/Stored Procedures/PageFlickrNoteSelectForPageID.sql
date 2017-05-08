CREATE PROCEDURE dbo.PageFlickrNoteSelectForPageID

@PageID int

AS

BEGIN

SET NOCOUNT OFF

SELECT	PageFlickrNoteID,
		PageID,
		PhotoID,
		FlickrNoteID,
		FlickrAuthorID,
		FlickrAuthorName,
		FlickrAuthorRealName,
		AuthorIsPro,
		XCoord,
		YCoord,
		Width,
		Height,
		NoteValue,
		IsActive,
		CreationDate,
		LastModifiedDate,
		DeleteDate
FROM	dbo.PageFlickrNote
WHERE	PageID = @PageID

END
