
-- SegmentGenreSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentGenre

CREATE PROCEDURE SegmentGenreSelectAuto

@SegmentGenreID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentGenreSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

