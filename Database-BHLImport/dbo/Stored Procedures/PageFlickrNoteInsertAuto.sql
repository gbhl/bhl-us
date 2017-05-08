CREATE PROCEDURE dbo.PageFlickrNoteInsertAuto

@PageFlickrNoteID INT OUTPUT,
@PageID INT,
@PhotoID NVARCHAR(50),
@FlickrNoteID NVARCHAR(100),
@FlickrAuthorID NVARCHAR(100),
@FlickrAuthorName NVARCHAR(150),
@FlickrAuthorRealName NVARCHAR(150),
@AuthorIsPro SMALLINT,
@XCoord INT = null,
@YCoord INT = null,
@Width INT = null,
@Height INT = null,
@NoteValue NVARCHAR(MAX),
@IsActive TINYINT,
@DeleteDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageFlickrNote]
( 	[PageID],
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
	[DeleteDate] )
VALUES
( 	@PageID,
	@PhotoID,
	@FlickrNoteID,
	@FlickrAuthorID,
	@FlickrAuthorName,
	@FlickrAuthorRealName,
	@AuthorIsPro,
	@XCoord,
	@YCoord,
	@Width,
	@Height,
	@NoteValue,
	@IsActive,
	getdate(),
	getdate(),
	@DeleteDate )

SET @PageFlickrNoteID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageFlickrNoteInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
