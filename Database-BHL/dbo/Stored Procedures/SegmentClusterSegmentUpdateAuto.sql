CREATE PROCEDURE SegmentClusterSegmentUpdateAuto

@SegmentID INT,
@SegmentClusterID INT,
@IsPrimary SMALLINT,
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentClusterSegment]

SET

	[SegmentID] = @SegmentID,
	[SegmentClusterID] = @SegmentClusterID,
	[IsPrimary] = @IsPrimary,
	[SequenceOrder] = @SequenceOrder,
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
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[SegmentClusterSegment]
	
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- update successful
END

GO
