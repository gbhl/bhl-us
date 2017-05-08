CREATE PROCEDURE [dbo].[PageFlickrNoteSelectAuto]

@PageFlickrNoteID INT

AS 

SET NOCOUNT ON

SELECT	
	[PageFlickrNoteID],
	[PageID],
	[PhotoID],
	[FlickrNoteID],
	[FlickrAuthorID],
	[FlickrAuthorName],
	[FlickrAuthorRealName],
	[AuthorIsPro],
	[XCoord],
	[YCoord],
	[Width],
	[Height],
	[NoteValue],
	[IsActive],
	[CreationDate],
	[LastModifiedDate],
	[DeleteDate]
FROM	
	[dbo].[PageFlickrNote]
WHERE	
	[PageFlickrNoteID] = @PageFlickrNoteID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageFlickrNoteSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
