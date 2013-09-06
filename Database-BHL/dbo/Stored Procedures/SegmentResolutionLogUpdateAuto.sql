CREATE PROCEDURE SegmentResolutionLogUpdateAuto

@SegmentResolutionLogID INT,
@SegmentID INT,
@MatchingSegmentID INT,
@Score NUMERIC(9,8),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentResolutionLog]

SET

	[SegmentID] = @SegmentID,
	[MatchingSegmentID] = @MatchingSegmentID,
	[Score] = @Score,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentResolutionLogID] = @SegmentResolutionLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentResolutionLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

