
-- SegmentGenreUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentGenre

CREATE PROCEDURE SegmentGenreUpdateAuto

@SegmentGenreID INT,
@GenreName NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentGenre]

SET

	[GenreName] = @GenreName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentGenreID] = @SegmentGenreID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentGenreUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentGenreID],
		[GenreName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[SegmentGenre]
	
	WHERE
		[SegmentGenreID] = @SegmentGenreID
	
	RETURN -- update successful
END

