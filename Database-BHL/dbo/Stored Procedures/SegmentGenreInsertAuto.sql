
-- SegmentGenreInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentGenre

CREATE PROCEDURE SegmentGenreInsertAuto

@SegmentGenreID INT OUTPUT,
@GenreName NVARCHAR(50),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentGenre]
(
	[GenreName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@GenreName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @SegmentGenreID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentGenreInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

