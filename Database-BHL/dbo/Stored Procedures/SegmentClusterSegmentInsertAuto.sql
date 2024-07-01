CREATE PROCEDURE SegmentClusterSegmentInsertAuto

@SegmentID INT,
@SegmentClusterID INT,
@IsPrimary SMALLINT,
@SequenceOrder SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentClusterSegment]
(
	[SegmentID],
	[SegmentClusterID],
	[IsPrimary],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@SegmentClusterID,
	@IsPrimary,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

GO