CREATE PROCEDURE SegmentGenreInsertAuto

@SegmentGenreID INT OUTPUT,
@GenreName NVARCHAR(50),
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@GenreDescription NVARCHAR(500)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentGenre]
( 	[GenreName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[GenreDescription] )
VALUES
( 	@GenreName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@GenreDescription )

SET @SegmentGenreID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentGenreInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentGenreID],
		[GenreName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[GenreDescription]	
	FROM [dbo].[SegmentGenre]
	WHERE
		[SegmentGenreID] = @SegmentGenreID
	
	RETURN -- insert successful
END
GO
