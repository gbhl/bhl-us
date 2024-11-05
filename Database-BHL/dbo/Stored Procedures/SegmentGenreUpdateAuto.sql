CREATE PROCEDURE SegmentGenreUpdateAuto

@SegmentGenreID INT,
@GenreName NVARCHAR(50),
@LastModifiedUserID INT,
@GenreDescription NVARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentGenre]
SET
	[GenreName] = @GenreName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[GenreDescription] = @GenreDescription
WHERE
	[SegmentGenreID] = @SegmentGenreID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentGenreUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
