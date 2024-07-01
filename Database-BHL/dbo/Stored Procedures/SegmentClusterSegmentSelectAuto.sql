CREATE PROCEDURE SegmentClusterSegmentSelectAuto

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT 

	[SegmentID],
	[SegmentClusterID],
	[IsPrimary],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[SegmentClusterSegment]

WHERE
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
