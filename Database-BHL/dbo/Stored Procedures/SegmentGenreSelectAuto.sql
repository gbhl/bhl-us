CREATE PROCEDURE SegmentGenreSelectAuto

@SegmentGenreID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentGenreID],
	[GenreName],
	[GenreDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[SegmentGenre]
WHERE	
	[SegmentGenreID] = @SegmentGenreID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentGenreSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
