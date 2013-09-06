
-- SegmentClusterSegmentUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentClusterSegment

CREATE PROCEDURE SegmentClusterSegmentUpdateAuto

@SegmentID INT,
@SegmentClusterID INT,
@IsPrimary SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentClusterSegment]

SET

	[SegmentID] = @SegmentID,
	[SegmentClusterID] = @SegmentClusterID,
	[IsPrimary] = @IsPrimary,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

