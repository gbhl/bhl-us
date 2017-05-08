CREATE PROCEDURE dbo.PageFlickrNoteUpdateAuto

@PageFlickrNoteID INT,
@PageID INT,
@PhotoID NVARCHAR(50),
@FlickrNoteID NVARCHAR(100),
@FlickrAuthorID NVARCHAR(100),
@FlickrAuthorName NVARCHAR(150),
@FlickrAuthorRealName NVARCHAR(150),
@AuthorIsPro SMALLINT,
@XCoord INT,
@YCoord INT,
@Width INT,
@Height INT,
@NoteValue NVARCHAR(MAX),
@IsActive TINYINT,
@DeleteDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageFlickrNote]
SET
	[PageID] = @PageID,
	[PhotoID] = @PhotoID,
	[FlickrNoteID] = @FlickrNoteID,
	[FlickrAuthorID] = @FlickrAuthorID,
	[FlickrAuthorName] = @FlickrAuthorName,
	[FlickrAuthorRealName] = @FlickrAuthorRealName,
	[AuthorIsPro] = @AuthorIsPro,
	[XCoord] = @XCoord,
	[YCoord] = @YCoord,
	[Width] = @Width,
	[Height] = @Height,
	[NoteValue] = @NoteValue,
	[IsActive] = @IsActive,
	[LastModifiedDate] = getdate(),
	[DeleteDate] = @DeleteDate
WHERE
	[PageFlickrNoteID] = @PageFlickrNoteID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageFlickrNoteUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[PageFlickrNote]
	WHERE
		[PageFlickrNoteID] = @PageFlickrNoteID
	
	RETURN -- update successful
END
