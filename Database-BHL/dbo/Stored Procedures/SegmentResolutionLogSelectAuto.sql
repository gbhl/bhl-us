CREATE PROCEDURE SegmentResolutionLogSelectAuto

@SegmentResolutionLogID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentResolutionLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

