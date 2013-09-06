CREATE PROCEDURE SegmentResolutionLogInsertAuto

@SegmentResolutionLogID INT OUTPUT,
@SegmentID INT,
@MatchingSegmentID INT,
@Score NUMERIC(9,8) = null,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentResolutionLog]
(
	[SegmentID],
	[MatchingSegmentID],
	[Score],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@MatchingSegmentID,
	@Score,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @SegmentResolutionLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentResolutionLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentResolutionLogID],
		[SegmentID],
		[MatchingSegmentID],
		[Score],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[SegmentResolutionLog]
	
	WHERE
		[SegmentResolutionLogID] = @SegmentResolutionLogID
	
	RETURN -- insert successful
END

