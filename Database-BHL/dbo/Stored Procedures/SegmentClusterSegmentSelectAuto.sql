
-- SegmentClusterSegmentSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentClusterSegment

CREATE PROCEDURE SegmentClusterSegmentSelectAuto

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT 

	[SegmentID],
	[SegmentClusterID],
	[IsPrimary],
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

